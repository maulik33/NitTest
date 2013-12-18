IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestAssignment]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestAssignment]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestAssignment]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestAssignment]
@UserTestId INT
AS
BEGIN
	
	DECLARE @TestId INT
	
	SELECT @TestId = TestID
	FROM UserTests
	WHERE UserTestID = @UserTestId

	SELECT Category.TableName, Category.OrderNumber, TestCategory.[Admin]
    FROM   TestCategory
	INNER JOIN Category ON TestCategory.CategoryID = Category.CategoryID
    WHERE     TestCategory.TestID = @TestId

END
GO
