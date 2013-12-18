IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPerformanceDetailsByProductIDChartType]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetPerformanceDetailsByProductIDChartType]
GO

/****** Object:  StoredProcedure [dbo].[uspGetPerformanceDetailsByProductIDChartType]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetPerformanceDetailsByProductIDChartType]
	
	@ProductID int,
    @UserID int,
	@ChartType int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON


IF (@ChartType = 1)
	
	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText
	 FROM  dbo.UserQuestions INNER JOIN
	 dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
	 dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
	 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
	 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
	 WHERE   UserID =@UserID
	 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
	 GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty

ELSE

IF (@ChartType = 2)

	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText
	FROM  dbo.UserQuestions INNER JOIN
	dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
	dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
	INNER JOIN
	dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
	INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
	WHERE   UserID =@UserID
	AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
	GROUP BY dbo.NursingProcess.NursingProcess
ELSE

IF (@ChartType = 3)

	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText
    FROM  dbo.UserQuestions INNER JOIN
    dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
    dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
    INNER JOIN
    dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
    INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
    WHERE   UserID =@UserID
    AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
    GROUP BY dbo.ClinicalConcept.ClinicalConcept
ELSE
IF (@ChartType = 4)
SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText
 FROM  dbo.UserQuestions INNER JOIN
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
  dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
 INNER JOIN
 dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID
 ORDER BY dbo.ClientNeeds.ClientNeedsID
ELSE
IF (@ChartType = 5)
SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText
 FROM  dbo.UserQuestions INNER JOIN
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
  dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
 INNER JOIN
 dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID
 ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID
	SET NOCOUNT OFF
END
GO
