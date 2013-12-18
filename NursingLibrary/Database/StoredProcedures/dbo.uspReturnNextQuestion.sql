IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnNextQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnNextQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnNextQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspReturnNextQuestion]    Script Date: 05/29/2011  ******/

CREATE PROCEDURE [dbo].[uspReturnNextQuestion]	
	@UserTestId int, 	
	@QuestionNumber int,
	@TypeOfFileId varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF @TypeOfFileId = '01' OR @TypeOfFileId ='02' OR @TypeOfFileId ='03' OR @TypeOfFileId ='04' OR @TypeOfFileId ='05'
		BEGIN

			SELECT TOP 1
				UQ.QuestionNumber,
				UQ.TimeSpendForRemedation,
				UQ.TimeSpendForExplanation,
				P.ProductName,
				T.TestName,
				U.TestID,
				Q.Stem,
				Q.ListeningFileUrl,
				Q.Explanation,
				Q.RemediationID,
				Q.QuestionType,
				Q.TypeOfFileID,
				Q.TopicTitleID,
				Q.QID,
				Q.QuestionID,
				Q.ItemTitle
			FROM  dbo.Products P
			INNER JOIN  dbo.Tests T
			ON P.ProductID = T.ProductID
			INNER JOIN dbo.UserTests U
			ON T.TestID = U.TestID
			INNER JOIN dbo.UserQuestions UQ
			ON UQ.UserTestID = U.UserTestID
			INNER JOIN dbo.Questions Q
			ON Q.QID = UQ.QID
			WHERE U.UserTestID = @UserTestId
			AND UQ.QuestionNumber>@QuestionNumber
			AND TypeOfFileID = @TypeOfFileId
			AND (Q.Active IS NULL OR Q.Active = 1)
		END

	ELSE
		BEGIN
			SELECT TOP 1
				UQ.QuestionNumber,
				UQ.TimeSpendForRemedation,
				UQ.TimeSpendForExplanation,
				P.ProductName,
				T.TestName,
				U.TestID,
				Q.Stem,
				Q.ListeningFileUrl,
				Q.Explanation,
				Q.RemediationID,
				Q.QuestionType,
				Q.TypeOfFileID,
				Q.TopicTitleID,
				Q.QID,
				Q.QuestionID,
				Q.ItemTitle
			FROM  dbo.Products P
			INNER JOIN  dbo.Tests T
			ON P.ProductID = T.ProductID
			INNER JOIN dbo.UserTests U
			ON T.TestID = U.TestID
			INNER JOIN dbo.UserQuestions UQ
			ON UQ.UserTestID = U.UserTestID
			INNER JOIN dbo.Questions Q
			ON Q.QID = UQ.QID
			WHERE U.UserTestID = @UserTestId
			AND UQ.QuestionNumber > @QuestionNumber 			
		END
		SET NOCOUNT OFF
	END
GO
