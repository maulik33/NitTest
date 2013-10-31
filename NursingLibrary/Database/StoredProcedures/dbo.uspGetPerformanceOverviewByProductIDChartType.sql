IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPerformanceOverviewByProductIDChartType]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetPerformanceOverviewByProductIDChartType]
GO

/****** Object:  StoredProcedure [dbo].[uspGetPerformanceOverviewByProductIDChartType]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetPerformanceOverviewByProductIDChartType]

 @ProductID int,
    @UserID int,
 @ChartType int

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;


IF (@ChartType = 1)


 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS Total,
	SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS N_Correct,
    SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC,0 AS UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3

ELSE

IF (@ChartType = 2)

 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS Total,
 SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS N_Correct,
 SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC,0 AS UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID AND dbo.Tests.TestSubGroup=3

ELSE

IF (@ChartType = 3)

 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS Total,
		SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS N_Correct,
        SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC
    ,dbo.UserTests.UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID AND  dbo.Tests.ProductID=@ProductID AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.UserTests.UserTestID


 SET NOCOUNT OFF
END
GO
