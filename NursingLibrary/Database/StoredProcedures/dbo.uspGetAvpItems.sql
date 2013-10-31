IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAvpItems]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAvpItems]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAvpItems]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAvpItems]
	-- Add the parameters for the stored procedure here
	@userId int, @productId int, @testSubGroup int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Hour int
	SET @Hour = 0

	SELECT * FROM (
	SELECT DISTINCT
		dbo.NusStudentAssign.StudentID As UserID, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID,
		dbo.NurCohortPrograms.Active, dbo.NurProgramProduct.ProductID,
		dbo.Tests.TestID,dbo.Tests.PopWidth,dbo.Tests.PopHeight,dbo.Tests.TestName,dbo.Tests.Url,dbo.Tests.DefaultGroup,
		COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, dbo.NurProductDatesCohort.StartDate) AS StartDate_All,
		COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, dbo.NurProductDatesCohort.EndDate) AS EndDate_All
	FROM   dbo.NurCohort INNER JOIN
	  dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
	  INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
	  INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
	  INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
	  INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID

		LEFT OUTER JOIN dbo.NurProductDatesCohort ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID
			AND  dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
			AND (
			(dbo.NurProductDatesCohort.StartDate < DATEADD(hour, @Hour, GETDATE()))
			AND (dbo.NurProductDatesCohort.EndDate > DATEADD(hour, @Hour, GETDATE()))
			)
		LEFT OUTER JOIN dbo.NurProductDatesGroup ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesGroup.CohortID
			AND dbo.NusStudentAssign.GroupID = dbo.NurProductDatesGroup.GroupID			
			AND dbo.Tests.TestID = dbo.NurProductDatesGroup.ProductID
			AND (
			dbo.NurProductDatesGroup.StartDate < DATEADD(hour, @Hour, GETDATE())
			AND dbo.NurProductDatesGroup.EndDate > DATEADD(hour, @Hour, GETDATE())
			)
		LEFT OUTER JOIN dbo.NurProductDatesStudent ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesStudent.CohortID
			AND dbo.NusStudentAssign.GroupID = dbo.NurProductDatesStudent.GroupID
			AND dbo.NusStudentAssign.StudentID = dbo.NurProductDatesStudent.StudentID
			AND dbo.Tests.TestID = dbo.NurProductDatesStudent.ProductID
			AND (
			dbo.NurProductDatesStudent.EndDate > DATEADD(hour, @Hour, GETDATE())
			AND dbo.NurProductDatesStudent.StartDate < DATEADD(hour, @Hour, GETDATE())
			)
	WHERE
		dbo.NurProgram.DeletedDate IS NULL AND dbo.NurCohort.CohortStatus = 1 AND dbo.NusStudentAssign.StudentID = @UserID AND dbo.NurCohortPrograms.Active = 1
		AND dbo.Tests.ProductID = @ProductID AND dbo.Tests.TestSubGroup = @TestSubGroup AND dbo.Tests.ActiveTest = 1
	) A WHERE StartDate_All IS NOT NULL AND EndDate_All IS NOT NULL

END
GO
