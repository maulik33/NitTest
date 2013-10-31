IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspGetGroup]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspGetGroup]
(
 @GroupId int ,
 @CohortIds varchar(max)
)
AS
SET NOCOUNT ON;
BEGIN

	SELECT	G.GroupID,
			G.GroupName,
			G.CohortID,
			G.GroupUpdateUser,
			G.GroupDeleteUser,
			G.GroupInsertUser,
			G.GroupUpdateDate,
			G.GroupInsertDate,
			G.GroupDeleteDate			
	FROM dbo.NurGroup G			
	WHERE GroupDeleteDate IS NULL
	AND (@GroupId = 0
	OR	G.GroupID = @GroupId)
	AND ( @CohortIds = ''
	OR G.CohortID IN
		(SELECT value
		 FROM dbo.funcListToTableInt(@CohortIds,'|')))
END

--END
GO
