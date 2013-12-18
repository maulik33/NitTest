IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionsAndLippincotts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionsAndLippincotts]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionsAndLippincotts]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionsAndLippincotts]
@LippincottId int
AS
BEGIN
	SELECT Lippincot.LippincottID, Questions.QuestionID, Questions.QID
    FROM Lippincot INNER JOIN QuestionLippincott ON Lippincot.LippincottID = QuestionLippincott.LippincottID
    INNER JOIN Questions ON QuestionLippincott.QID = Questions.QID
    WHERE (Lippincot.LippincottID =@LippincottId)
END
GO
