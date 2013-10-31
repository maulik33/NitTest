IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestsByProductAndUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnTestsByProductAndUser]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnTestsByProductAndUser]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnTestsByProductAndUser]
	@ProductID int,
    @UserID int
AS

BEGIN
	SET NOCOUNT ON

	DECLARE @Hour INT

	SELECT @Hour = TimeZones.Hour
	 FROM NurStudentInfo INNER JOIN NurInstitution ON NurStudentInfo.InstitutionID = NurInstitution.InstitutionID
	 INNER JOIN TimeZones ON NurInstitution.TimeZone = TimeZones.TimeZoneID
	 WHERE NurStudentInfo.UserID = @UserID

IF (@ProductID != 0 )
BEGIN
	SELECT dbo.UserTests.UserTestID, dbo.Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour, dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
	FROM  dbo.Tests INNER JOIN
	dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID INNER JOIN
	dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
	WHERE  TestStatus=1  AND UserID=@UserID AND dbo.Tests.ProductID=@ProductID
END
ELSE
BEGIN
	SELECT dbo.UserTests.UserTestID, dbo.Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour,dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
	FROM  dbo.Tests INNER JOIN
	dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID INNER JOIN
	dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
	WHERE  TestStatus=1  AND UserID=@UserID
END

SET NOCOUNT OFF

END
GO
