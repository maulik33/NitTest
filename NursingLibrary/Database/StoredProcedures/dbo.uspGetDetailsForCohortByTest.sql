IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDetailsForCohortByTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetDetailsForCohortByTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetDetailsForCohortByTest]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetDetailsForCohortByTest]
@TestIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@GroupIds VARCHAR(MAX),
@InstitutionId int,
@ProductIds VARCHAR(MAX)
AS
BEGIN

	 DECLARE @TestXml xml,@CohortXml xml,@GroupXml xml,@ProductXml xml
	 DECLARE @TestName as VARCHAR(MAX),@CohortName as VARCHAR(MAX),@GroupName as VARCHAR(MAX),@ProductName as VARCHAR(MAX)
		
   	 SET @TestName = '<R><N>' + REPLACE(@TestIds,'|','</N><N>') + '</N></R>';
	 SET @CohortName = '<R><N>' + REPLACE(@CohortIds,'|','</N><N>') + '</N></R>';
	 SET @GroupName = '<R><N>' + REPLACE(@GroupIds,'|','</N><N>') + '</N></R>';
     SET @ProductName = '<R><N>' + REPLACE(@ProductIds,'|','</N><N>') + '</N></R>';
	 SET @TestXml = (select cast(@TestName as xml))
	 SET @CohortXml = (select cast(@CohortName as xml))
	 SET @GroupXml = (select cast(@GroupName as xml))
	 SET @ProductXml = (select cast(@ProductName as xml))
		

      SELECT Cast (100.0*SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)/ SUM(1) as decimal(4,1)) AS Percantage,
      dbo.Tests.TestID,dbo.Tests.ProductID,dbo.NurCohort.InstitutionID,dbo.Tests.TestName,
      COUNT( DISTINCT dbo.UserTests.UserID) as NStudents,
      dbo.NurCohort.CohortName CohortName,
      dbo.NurCohort.CohortId,
      ISNULL(N.Norm,0) NormedPercCorrect
      FROM   dbo.UserQuestions
      INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
      INNER JOIN dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID
      INNER JOIN dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID
      INNER JOIN Products P ON Tests.ProductId = P.ProductId
      INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
      INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
      LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
      LEFT JOIN dbo.Norm N ON dbo.Tests.TestId = N.TestId AND N.ChartType = 'OverAll' AND N.ChartId = 0
      WHERE  dbo.NurCohort.InstitutionID = @InstitutionId AND TestStatus = 1
            AND ((@TestIds <> '0' AND dbo.UserTests.TestId IN ( select c.value('(.)[1]','char(10)') [col] from @TestXml.nodes('//N') as tab(c)))
				OR @TestIds = '0')
            AND ((@CohortIds <> '0' AND dbo.NurCohort.CohortID IN ( select c.value('(.)[1]','char(10)') [col] from @CohortXml.nodes('//N') as tab(c)))
            OR (@CohortIds = '0'))
            AND ((@GroupIds <> '' AND G.GroupID IN ( select c.value('(.)[1]','char(10)') [col] from @GroupXml.nodes('//N') as tab(c)))
            OR (@GroupIds = ''))
			AND ((@ProductIds <> '0' AND dbo.Tests.ProductID IN ( select c.value('(.)[1]','char(10)') [col] from @ProductXml.nodes('//N') as tab(c)))
            OR (@ProductIds = '0'))
        GROUP BY dbo.NurCohort.InstitutionID,dbo.Tests.ProductID,dbo.Tests.TestID,dbo.Tests.TestName
                  ,dbo.NurCohort.CohortName ,dbo.NurCohort.CohortId,ISNULL(N.Norm,0)

END

GO
