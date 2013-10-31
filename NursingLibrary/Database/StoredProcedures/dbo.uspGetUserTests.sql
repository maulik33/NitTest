IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUserTests]
GO

/****** Object:  StoredProcedure [dbo].[uspGetUserTests]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetUserTests]
	@userId int, @productId int, @testSubGroup int, @timeOffset int, @testStatus int
AS
BEGIN
SET NOCOUNT ON;
    IF (@productId = 4 OR @productId = 5)
	BEGIN
		SELECT DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.Tests.TestID, SuspendType, UserTestID,
			dbo.Tests.TestName as TN, dbo.Tests.TestNumber,IsCustomizedFRTest
         FROM dbo.Tests INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
			INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
         WHERE UserID = @userId AND TestStatus = @testStatus AND dbo.Tests.ProductID = @productId
			AND dbo.Tests.TestSubGroup = @testSubGroup
         ORDER BY dbo.Tests.TestNumber ASC
	END
	ELSE
	BEGIN
		SELECT DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.Tests.TestID, SuspendType, UserTestID,
			dbo.Tests.TestName as TN, dbo.Tests.TestNumber,IsCustomizedFRTest
         FROM  dbo.Tests INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
			INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
         WHERE UserID = @userId AND TestStatus = @testStatus AND dbo.Tests.ProductID = @productId
			AND dbo.Tests.TestSubGroup = @testSubGroup
         ORDER BY dbo.Tests.TestName ASC
	END
END
GO
   
