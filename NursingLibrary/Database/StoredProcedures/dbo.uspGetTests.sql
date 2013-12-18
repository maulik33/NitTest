IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTests]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTests]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- TODO This SP is going to chnage to remove default test id
/****** Object:  StoredProcedure [dbo].[uspGetTests]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTests]
(
 @ProductId INT,
 @InstitutionIds VARCHAR(1000),
 @QuestionId INT,
 @ForCMS BIT,  
 @ProgramofStudy INT = 1
)
AS
	SET NOCOUNT ON
	IF @ForCMS = 0
	BEGIN
		IF @InstitutionIds = ''
		BEGIN
			SELECT
				'' AS ProductName ,
				TestID ,
				TestName AS [Name] ,
				0 AS TestNumber
			FROM  Tests
			WHERE (ProductID = @ProductId or @ProductId = 0)
			AND ActiveTest=1 AND ISNULL(ReleaseStatus,'R') != 'F'
            AND Tests.ProgramofStudyId = @ProgramofStudy    
		END
		ELSE
		BEGIN
			SELECT T.TestID,
				'' AS ProductName,
				T.TestName AS [Name],
				0 AS TestNumber,
				T.DefaultGroup
			FROM dbo.Tests T
				INNER JOIN
					(SELECT DISTINCT CASE WHEN PP.[Type] = 1 THEN PT.TestId
						ELSE PP.ProductId END AS TestId
					FROM dbo.NurCohort C
						INNER JOIN dbo.NurInstitution I
						ON C.InstitutionID = I.InstitutionID
						INNER JOIN dbo.NurCohortPrograms CP
						ON C.CohortID = CP.CohortID AND CP.[Active] = 1
						INNER JOIN dbo.NurProgram P
						ON CP.ProgramID = P.ProgramID
						INNER JOIN dbo.NurProgramProduct PP
						ON P.ProgramID = PP.ProgramID
						LEFT OUTER JOIN (dbo.Products PR
						INNER JOIN dbo.Tests AS PT
						ON PT.ProductId = PR.ProductId AND PT.DefaultGroup = '1' AND ISNULL(PT.ReleaseStatus,'R') != 'F')
						ON PP.ProductID = PR.ProductID AND PP.[Type] = 1
					WHERE (@InstitutionIds = ''
						OR I.InstitutionId IN
						(SELECT value
						FROM dbo.funcListToTableInt(@InstitutionIds,'|')))) AS AvT
				ON AvT.TestId = T.TestId 
			WHERE T.ActiveTest = 1 AND ISNULL(T.ReleaseStatus,'R') != 'F'
				AND (@ProductId = 0 OR T.ProductID = @ProductId)
                AND T.ProgramofStudyId = @ProgramofStudy  
		END
	END
	ELSE
	BEGIN
		SELECT
			P.ProductName, 				
			T.TestID ,
			T.TestName as [Name],
			(SELECT QuestionNumber FROM TestQuestions
				WHERE QID = @QuestionId AND TestID = T.TestID) AS  TestNumber
		FROM  dbo.Products P
		INNER JOIN dbo.Tests T 
		ON P.ProductID = T.ProductID
        where ISNULL(T.ReleaseStatus,'R') != 'F'
        AND T.ProgramofStudyId = @ProgramofStudy   
	END

GO
