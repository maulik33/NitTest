IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByUserProductSubGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByUserProductSubGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsByUserProductSubGroup]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestsByUserProductSubGroup]
(
	@userId int,
	@productId int,
	@testSubGroup int,
	@timeOffset int
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT UserTestID,
		DATEADD(hour, @timeOffset, TestStarted) AS TestStarted
	FROM dbo.UserTests
		INNER JOIN dbo.Tests
		ON dbo.Tests.TestID = dbo.UserTests.TestID
    WHERE UserID = @userId
		AND dbo.Tests.ProductID = @productId
		AND dbo.Tests.TestSubGroup = @testSubGroup
	ORDER BY TestStarted ASC

	SET NOCOUNT OFF
END
GO
