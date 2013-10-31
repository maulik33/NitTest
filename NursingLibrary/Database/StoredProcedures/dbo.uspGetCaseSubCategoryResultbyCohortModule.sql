IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseSubCategoryResultbyCohortModule]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCaseSubCategoryResultbyCohortModule]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCaseSubCategoryResultbyCohortModule]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCaseSubCategoryResultbyCohortModule]
	@CohortIds VARCHAR(MAX),
	@CaseID int,
	@ModuleIds varchar(max),
	@CategoryName varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	
	Select CS.SubcategoryID, SUM(CS.Correct) AS Correct, SUM(CS.Total) As Total
	from CaseModuleScore M
	INNER JOIN CaseSubCategory CS ON M.ModuleStudentID = CS.ModuleStudentID
	INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
	WHERE A.CohortID IN (SELECT VALUE FROM dbo.funcListToTableInt(@CohortIds,'|'))
	AND UserType='S'
	AND UserDeleteData is null
	AND (A.DeletedDate IS NULL)
    AND M.CaseID=@CaseID
    AND M.ModuleID  IN (SELECT VALUE FROM dbo.funcListToTableInt(@ModuleIds,'|'))
    AND CS.CategoryName = @CategoryName
	Group BY CS.SubcategoryID

	SET NOCOUNT OFF
END
GO
