IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateQuestionExplanation]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateQuestionExplanation]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateQuestionExplanation]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateQuestionExplanation]
	@questionId int, @userTestId int, @timer int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE UserQuestions
	SET TimeSpendForExplanation = @timer
	WHERE QID = @questionId and UserTestID = @userTestId
END
GO
