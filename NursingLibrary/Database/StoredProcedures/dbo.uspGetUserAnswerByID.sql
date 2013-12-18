IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserAnswerByID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUserAnswerByID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetUserAnswerByID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[uspGetUserAnswerByID]
	@QuestionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT * FROM AnswerChoices WHERE QID=@QuestionID
END
GO
