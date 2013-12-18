IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestionCount]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestQuestionCount]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestQuestionCount]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestQuestionCount]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Declare @Number int
	SELECT @Number = COUNT(*)
	FROM TestQuestions, Questions
	WHERE TestQuestions.QID = dbo.Questions.QID AND TestID = @testId
		AND (Questions.Active = 1 OR Questions.Active IS NULL)

Return  @Number

SET NOCOUNT Off;
END
GO
