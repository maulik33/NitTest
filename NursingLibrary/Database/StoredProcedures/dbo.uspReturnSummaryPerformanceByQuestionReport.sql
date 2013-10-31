IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnSummaryPerformanceByQuestionReport]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnSummaryPerformanceByQuestionReport]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnSummaryPerformanceByQuestionReport]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[uspReturnSummaryPerformanceByQuestionReport]
    @CohortID varchar(500),
    @ProductID int,
    @TestID int

AS
	SELECT Q.QuestionID,
		Q.QuestionType,
		Answer = CASE
			WHEN Q.QuestionType IN ('05') THEN '1'
			WHEN AC.AIndex = 'A' THEN '1'
			WHEN AC.AIndex = 'B' THEN '2'
			WHEN AC.AIndex = 'C' THEN '3'
			WHEN AC.AIndex = 'D' THEN '4'
			WHEN AC.AIndex = 'E' THEN '5'
			WHEN AC.AIndex = 'F' THEN '6'
			ELSE '7'
		END,
		SUM(CASE WHEN UA.AIndex = 'A' THEN 1 ELSE 0 END) AS Total1,
		SUM(CASE WHEN UA.AIndex = 'B' THEN 1 ELSE 0 END) AS Total2,
		SUM(CASE WHEN UA.AIndex = 'C' THEN 1 ELSE 0 END) AS Total3,
		SUM(CASE WHEN UA.AIndex = 'D' THEN 1 ELSE 0 END) AS Total4,
		SUM(CASE WHEN UA.AIndex = 'E' THEN 1 ELSE 0 END) AS Total5,
		SUM(CASE WHEN UA.AIndex = 'F' THEN 1 ELSE 0 END) AS Total6,
		COUNT(*) AS TotalN,
		SUM(CASE WHEN UQ.Correct = 1 THEN 1 ELSE 0 END)  AS Total#Correct,
		SUM(CASE WHEN UQ.Correct = 1 THEN 0 ELSE 1 END)  AS Total#Wrong,
		CAST(SUM(CASE WHEN UQ.Correct = 1 THEN 1 ELSE 0 END)* 100.0 / COUNT(*) AS decimal(8, 1)) AS CorrectPercent,
		COUNT (DISTINCT UT.UserID) AS StudentNumber
	FROM dbo.Tests AS T
		INNER JOIN dbo.UserTests AS UT ON T.TestID = UT.TestID
		INNER JOIN dbo.UserQuestions AS UQ ON UT.UserTestID = UQ.UserTestID
		INNER JOIN dbo.Questions AS Q ON UQ.QID = Q.QID
		LEFT JOIN dbo.AnswerChoices AS AC ON Q.QID = AC.QID AND Q.QuestionType NOT IN ('05')
		LEFT JOIN dbo.UserAnswers AS UA  ON UQ.QID = UA.QID AND UQ.UserTestID = UA.UserTestID
	WHERE ((@ProductID <> 0 AND T.ProductID = @ProductID) OR @ProductID = 0)
		AND (UT.CohortID IN (SELECT value FROM dbo.funcListToTableInt(@CohortID,'|')))
		AND T.TestID = @TestID
	GROUP BY UQ.QID, UT.TestID, T.TestName, Q.QuestionID, AC.AIndex, AC.Correct, Q.QuestionType
GO
