IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertTestQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertTestQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspInsertTestQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertTestQuestion]
	@QID int, @UserTestID int, @QuestionNumber int
AS

INSERT INTO UserQuestions
	(QID, UserTestID, QuestionNumber, Correct, TimeSpendForQuestion,
		TimeSpendForRemedation, TimeSpendForExplanation, AnswerTrack)
VALUES
	(@QID, @UserTestID, @QuestionNumber, 2, 0, 0, 0, '')
GO
