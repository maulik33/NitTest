IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspTestsExistsByTestIDUserIDHour]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDUserIDHour]
GO

/****** Object:  StoredProcedure [dbo].[uspTestsExistsByTestIDUserIDHour]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDUserIDHour]
	
	@TestID int,
    @UserID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
DECLARE @ID int;
SELECT  @ID = dbo.Tests.TestID FROM
dbo.Tests INNER JOIN   dbo.NurProductDatesStudent ON
dbo.Tests.TestID = dbo.NurProductDatesStudent.ProductID WHERE
(dbo.Tests.TestID =  @TestID) AND (dbo.NurProductDatesStudent.EndDate > DATEADD(hour,  @Hour , GETDATE()))
AND  (dbo.NurProductDatesStudent.StartDate < DATEADD(hour, @Hour, GETDATE()))
AND (dbo.NurProductDatesStudent.StudentID = @UserID )



RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END
GO
