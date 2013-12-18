IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentNumberByCohortTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentNumberByCohortTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentNumberByCohortTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStudentNumberByCohortTest]
@ProductId INT,
@TestId INT,
@CohortId INT
AS
BEGIN
	select ISNULL(count(testID),0) AS Number
	from dbo.UserTests
	WHERE TestStatus = 1
	AND ProductID= @ProductId
	AND TestID= @TestId
	AND CohortID=@CohortId
	GROUP BY TestID
END
GO
