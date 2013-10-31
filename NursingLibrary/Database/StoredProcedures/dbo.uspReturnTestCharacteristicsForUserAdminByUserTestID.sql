IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]
	@UserTestID int,
    @UserType varchar(5)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @TestId INT
	
	SELECT @TestId = TestId
	FROM UserTests
	WHERE UserTestID=@UserTestID

IF (@UserType = 'S' )
BEGIN
	 SELECT Category.TableName FROM  TestCategory INNER JOIN
	 Category ON TestCategory.CategoryID = Category.CategoryID
	 WHERE Student=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END
ELSE
BEGIN
	SELECT Category.TableName FROM  TestCategory INNER JOIN
	Category ON TestCategory.CategoryID = Category.CategoryID
	WHERE [Admin]=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END

SET NOCOUNT OFF
END
GO
