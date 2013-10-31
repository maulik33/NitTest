IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByProdCohortUserIds]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByProdCohortUserIds]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsByProdCohortUserIds]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[uspGetTestsByProdCohortUserIds]
@tProductIds nVarchar(2000),
@tCohortIds nVarchar(2000),
@tStudentIds nVarchar(2000),
@groupIds VARCHAR(MAX),
@institutionId INT
AS
BEGIN

 SELECT DISTINCT Tests.TestName, UserTests.TestID
	From UserTests
	Join Tests on UserTests.TestID = Tests.TestID AND UserTests.TestStatus = 1
	INNER JOIN Products P ON Tests.ProductId = P.ProductId  AND P.ProductId = @tProductIds
	LEFT JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
	LEFT JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
	LEFT JOIN dbo.NurCohort C ON SA.CohortID = C.CohortID
	LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
	Where ((@tProductIds <> '' AND Tests.ProductID in (select * from dbo.funcListToTableInt(@tProductIds,'|'))) or (@tProductIds=''))
	and ((@tCohortIds <> '' AND UserTests.CohortID in (select * from dbo.funcListToTableInt(@tCohortIds,'|'))) or (@tCohortIds=''))
	and ((@tStudentIds <> '' AND UserTests.UserId in (select * from dbo.funcListToTableInt(@tStudentIds,'|'))) or (@tStudentIds = ''))
	AND ((@GroupIds <> '' AND G.GroupId IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|'))) OR @GroupIds = '')
	AND UserTests.InsitutionID = @institutionId

END
GO
