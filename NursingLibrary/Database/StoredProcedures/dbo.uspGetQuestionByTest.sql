IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionByTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionByTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionByTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionByTest]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT testquestions.QID, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID, CAST(dbo.Questions.QuestionType AS int) AS QuestionType, 0 AS PointerType, Questions.Stem, Questions.ListeningFileUrl, Questions.Explanation, CAST(Questions.RemediationID AS int) AS RemediationID, Questions.TopicTitleId, Questions.ItemTitle,testquestions.QuestionNumber,Questions.Active
	FROM testquestions, Questions
	WHERE testquestions.QID = Questions.QID AND testquestions.testID = @testId
END
GO
