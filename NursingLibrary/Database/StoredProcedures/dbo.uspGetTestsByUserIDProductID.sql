IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByUserIDProductID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByUserIDProductID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsByUserIDProductID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestsByUserIDProductID]

 @ProductID int,
    @UserID int,
 @Hour int


AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;



IF (@ProductID != 0)

 Select  CONVERT(nvarchar,QInfo.PercentCorrect)  as PercentCorrect , QInfo.QuestionCount,QInfo.UserTestID, UserTestsInfo.TestName,UserTestsInfo.TestStarted,UserTestsInfo.TestID,UserTestsInfo.TestStatus,UserTestsInfo.ProductName,
 UserTestsInfo.QuizOrQBank,UserTestsInfo.TestSubGroup FROM
(SELECT ((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount) AS PercentCorrect, QC.QuestionCount,QC.UserTestID FROM (select dbo.UserQuestions.UserTestID,count(*) AS QuestionCount FROm dbo.UserQuestions
 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID AND dbo.UserTests.ProductID=@ProductID  group by dbo.UserQuestions .UserTestID) QC
 LEFT OUTER JOIN   (SELECT  dbo.UserQuestions.UserTestID,count(*) AS QuestionCorrectCount FROm dbo.UserQuestions  INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID AND dbo.UserTests.ProductID=@ProductID
 AND dbo.UserQuestions.Correct=1  group by dbo.UserQuestions .UserTestID)QCC  ON QC.UserTestID=QCC.UserTestID ) QInfo INNER JOIN (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,DATEADD(hour, @Hour, TestStarted)
 as TestStarted, dbo.UserTests.TestID, dbo.UserTests.TestStatus,  dbo.Products.ProductName,dbo.UserTests.QuizOrQBank , dbo.Tests.TestSubGroup
 FROM  dbo.Tests INNER JOIN
 dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
 dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
 AND (dbo.Tests.ProductID = @ProductID) WHERE (UserID = @UserID)  )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID  ORDER BY UserTestsInfo.ProductName,UserTestsInfo.TestName,UserTestsInfo.TestStarted desc



ELSE

 Select CONVERT(nvarchar,QInfo.PercentCorrect) as PercentCorrect,
  QInfo.QuestionCount,QInfo.UserTestID, UserTestsInfo.TestName,UserTestsInfo.TestStarted,UserTestsInfo.TestID,UserTestsInfo.TestStatus,UserTestsInfo.ProductName,UserTestsInfo.QuizOrQBank,UserTestsInfo.TestSubGroup FROM
	(SELECT ((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount) AS PercentCorrect, QC.QuestionCount,QC.UserTestID FROM (select dbo.UserQuestions.UserTestID,count(*) AS QuestionCount FROm dbo.UserQuestions
	 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID   group by dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN
	(SELECT  dbo.UserQuestions.UserTestID,count(*) AS QuestionCorrectCount FROm dbo.UserQuestions  INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID   AND dbo.UserQuestions.Correct=1
	group by dbo.UserQuestions .UserTestID)QCC  ON QC.UserTestID=QCC.UserTestID ) QInfo INNER JOIN (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,DATEADD(hour, @Hour, TestStarted) as TestStarted,dbo.UserTests.TestID,
	dbo.UserTests.TestStatus,  dbo.Products.ProductName,dbo.UserTests.QuizOrQBank,dbo.Tests.TestSubGroup

 FROM  dbo.Tests INNER JOIN
 dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
 dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
  WHERE (UserID = @UserID) )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID ORDER BY UserTestsInfo.ProductName,UserTestsInfo.TestName,UserTestsInfo.TestStarted desc



 SET NOCOUNT OFF
END
GO
