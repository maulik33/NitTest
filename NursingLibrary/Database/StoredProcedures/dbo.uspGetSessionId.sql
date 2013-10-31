IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSessionId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSessionId]
GO

/****** Object:  StoredProcedure [dbo].[uspGetSessionId]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetSessionId]
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT SessionID
	FROM NurStudentInfo
	WHERE userId = @userId

END
GO
