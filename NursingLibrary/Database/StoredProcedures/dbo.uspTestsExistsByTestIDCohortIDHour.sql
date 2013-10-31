IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspTestsExistsByTestIDCohortIDHour]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDCohortIDHour]
GO

/****** Object:  StoredProcedure [dbo].[uspTestsExistsByTestIDCohortIDHour]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDCohortIDHour]
	
	@TestID int,
    @CohortID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @ID int;
SELECT @ID = dbo.Tests.TestID
FROM  dbo.Tests INNER JOIN dbo.NurProductDatesCohort ON
dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
WHERE (dbo.Tests.TestID = @TestID) AND (dbo.NurProductDatesCohort.CohortID =@CohortID)
 AND (dbo.NurProductDatesCohort.StartDate < DATEADD(hour, @Hour, GETDATE()))
 AND (dbo.NurProductDatesCohort.EndDate > DATEADD(hour, @Hour, GETDATE()))

RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END
GO
