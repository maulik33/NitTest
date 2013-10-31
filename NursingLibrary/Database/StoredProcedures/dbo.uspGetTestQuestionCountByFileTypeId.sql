IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestionCountByFileTypeId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestQuestionCountByFileTypeId]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestQuestionCountByFileTypeId]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspGetTestQuestionCountByFileTypeId]
(
	@TestId int,
	@TypeOfFileId varchar(500),
	@TotalCount int OUTPUT
)
AS
	SET NOCOUNT ON;

	SELECT @TotalCount = COUNT(*)
	FROM dbo.TestQuestions AS T
	INNER JOIN dbo.Questions AS Q
	ON T.QID = Q.QID
	WHERE TypeOfFileID = @TypeOfFileId
	AND	TestID = @TestId
	AND (Q.Active = 1
	OR Q.Active IS NULL)
	
	SET NOCOUNT OFF
GO
