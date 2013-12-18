IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionsToCreateTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionsToCreateTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionsToCreateTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionsToCreateTest]
	@testId int, @scramble int
AS

IF @scramble = 0 OR @testId = 63
	SELECT TestQuestions.QID, TestQuestions.QuestionNumber
	FROM TestQuestions,Questions
	WHERE TestQuestions.QID=Questions.QID AND TypeOfFileID='03'
		AND TestID = @testId ORDER BY TestQuestions.QuestionNumber
ELSE
	SELECT TestQuestions.QID, TestQuestions.QuestionNumber
	FROM TestQuestions,Questions
	WHERE TestQuestions.QID=Questions.QID AND TypeOfFileID='03'
		AND TestID = @testId ORDER BY NEWID()
GO
