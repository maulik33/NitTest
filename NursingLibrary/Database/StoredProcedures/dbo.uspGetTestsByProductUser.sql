IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByProductUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByProductUser]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsByProductUser]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestsByProductUser]
	@productId int, @userId int, @hour int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@productId != 0 )
		BEGIN
			SELECT UserTests.UserTestID, Tests.TestName + CAST(' (' as varchar) + CAST(DATEADD(hour, @hour, dbo.UserTests.TestStarted) as varchar) + CAST(')' as varchar) as TestName
			FROM Tests INNER JOIN Products ON Tests.ProductID = Products.ProductID
				INNER JOIN UserTests ON Tests.TestID = UserTests.TestID
			WHERE TestStatus = 1 AND UserID = @userId AND Tests.ProductID = @productId
			ORDER BY TestName
		END
	ELSE
		BEGIN
			SELECT UserTests.UserTestID, Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour,dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
			FROM dbo.Tests INNER JOIN Products ON Tests.ProductID = Products.ProductID
				INNER JOIN UserTests ON Tests.TestID = UserTests.TestID
			WHERE TestStatus = 1 AND UserID = @userId
			ORDER BY TestName
		END
END
GO
