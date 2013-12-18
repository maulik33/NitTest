IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseByCohortResult]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCaseByCohortResult]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCaseByCohortResult]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCaseByCohortResult]
	@InstitutionIds VARCHAR(MAX),
	@CaseID int,
	@ModuleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select CAST(SUM(M.Correct)*100.0/SUM(M.Total) AS numeric(10,1)) AS Percantage,
	count(S.EnrollmentID) As NStudents,
	C.CohortID, C.CohortName ,C.InstitutionId
	from CaseModuleScore M
	INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	INNER JOIN NurCohort C ON A.CohortID=C.CohortID
	WHERE UserType='S' AND UserDeleteData is null
	AND (A.DeletedDate IS NULL)
    AND M.CaseID=@CaseID AND M.ModuleID=@ModuleID
    AND S.InstitutionID IN (SELECT value
		FROM dbo.funcListToTableInt(@InstitutionIds,'|'))
	Group BY C.CohortID,C.CohortName,C.InstitutionId
	
	SET NOCOUNT OFF
END
GO
