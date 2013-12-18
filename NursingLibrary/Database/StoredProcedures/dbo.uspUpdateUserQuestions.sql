IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateUserQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateUserQuestions]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateUserQuestions]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateUserQuestions] (@QID int, @UserTestID int, @Correct int,
	@TimeSpendForQuestion int, @TimeSpendForRemediation int, @TimeSpendForExplanation int,
	@AnswerTrack varchar(50), @AnswerChanges char(2), @OrderedIndexes varchar(50))
AS

-- Update questions table with metadata
UPDATE UserQuestions SET Correct = @Correct, TimeSpendForQuestion = @TimeSpendForQuestion,
	AnswerTrack=@AnswerTrack,AnswerChanges=@AnswerChanges,OrderedIndexes=@OrderedIndexes
	WHERE UserTestID = @UserTestID AND QID = @QID

-- Delete previous answers
DELETE UserAnswers WHERE UserTestID = @UserTestID and QID = @QID
GO
