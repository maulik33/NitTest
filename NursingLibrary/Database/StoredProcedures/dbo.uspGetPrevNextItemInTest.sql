IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPrevNextItemInTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetPrevNextItemInTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetPrevNextItemInTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetPrevNextItemInTest]
	@TestID int, @questionId int, @typeOfFileId varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,2 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
	FROM  dbo.TestQuestions INNER JOIN dbo.Questions
	ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QuestionNumber > @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)
	UNION ALL
	SELECT  dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,1 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
    FROM dbo.TestQuestions INNER JOIN dbo.Questions
    ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QuestionNumber < @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)
	UNION ALL
	SELECT  dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,0 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
	FROM dbo.TestQuestions INNER JOIN dbo.Questions
	ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QID = @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)

	SET NOCOUNT OFF;
END
GO
