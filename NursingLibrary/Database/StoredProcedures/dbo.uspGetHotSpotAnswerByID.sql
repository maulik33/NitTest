IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetHotSpotAnswerByID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetHotSpotAnswerByID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetHotSpotAnswerByID]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[uspGetHotSpotAnswerByID]
	@QuestionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   SELECT AnswerChoices.*,Questions.Stimulus FROM AnswerChoices,Questions WHERE Questions.QID=AnswerChoices.QID AND Questions.QID = @QuestionID
END
GO
