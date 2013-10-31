IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnPreviousQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnPreviousQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnPreviousQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspReturnPreviousQuestion]    Script Date: 05/29/2011  ******/

CREATE PROCEDURE [dbo].[uspReturnPreviousQuestion]	
	@UserTestId int, 	
	@QuestionNumber int,
	@TypeOfFileId varchar(500)
AS
BEGIN

	SELECT
		UQ.QuestionNumber,
		UQ.TimeSpendForRemedation,
		UQ.TimeSpendForExplanation,
		P.ProductName,
		T.TestName,
		UT.TestID,
		Q.Stem,
		Q.Explanation,
		Q.RemediationID,
		Q.QuestionType,
		Q.TypeOfFileID,
		Q.TopicTitleID,
		Q.QID,
		Q.QuestionID,
		Q.ItemTitle,
		Q.ListeningFileUrl
	FROM  dbo.Products P
	INNER JOIN dbo.Tests T
	ON P.ProductID = T.ProductID
	INNER JOIN dbo.UserTests UT
	ON T.TestID = UT.TestID
	INNER JOIN dbo.UserQuestions UQ
	ON UQ.UserTestID = UT.UserTestID
	INNER JOIN dbo.Questions Q
	ON Q.QID = UQ.QID
	WHERE UT.UserTestID=@UserTestID
	AND UQ.QuestionNumber < @QuestionNumber
	AND TypeOfFileID = @TypeOfFileId
	
END
GO
