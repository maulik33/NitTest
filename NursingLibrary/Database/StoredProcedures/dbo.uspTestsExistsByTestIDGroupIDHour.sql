IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspTestsExistsByTestIDGroupIDHour]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDGroupIDHour]
GO

/****** Object:  StoredProcedure [dbo].[uspTestsExistsByTestIDGroupIDHour]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDGroupIDHour]
	
	@TestID int,
    @GroupID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DECLARE @ID int;
 SELECT  @ID = dbo.Tests.TestID FROM
 dbo.Tests INNER JOIN  dbo.NurProductDatesGroup ON dbo.Tests.TestID =
 dbo.NurProductDatesGroup.ProductID WHERE   (dbo.Tests.TestID = @TestID )
 AND (dbo.NurProductDatesGroup.GroupID = @GroupID) AND
 (dbo.NurProductDatesGroup.StartDate < DATEADD(hour, @Hour, GETDATE()))
 AND (dbo.NurProductDatesGroup.EndDate > DATEADD(hour, @Hour , GETDATE()))


RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END
GO
