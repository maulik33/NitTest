IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsForQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetTestsForQuestion]    Script Date: 05/27/2011  ******/


CREATE PROCEDURE [dbo].[uspGetTestsForQuestion]
(
 @QuestionId INT
 )
AS
BEGIN
SET NOCOUNT ON
	SELECT
		P.ProductName,
		T.TestName,
		Q.QuestionID,
		Q.QID
	FROM  dbo.Tests T
	INNER JOIN dbo.TestQuestions Q
	ON T.TestID = Q.TestID
	INNER JOIN dbo.Products P
	ON T.ProductID = P.ProductID
	WHERE QID=@QuestionId
END
GO
