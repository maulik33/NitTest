IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspResultsFromTheCohortsBySubCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspResultsFromTheCohortsBySubCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspResultsFromTheCohortsBySubCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspResultsFromTheCohortsBySubCategory]
 @CohortList varchar(500),
 @SubCategory int,
 @CaseList Varchar(50),
 @ModuleList Varchar(50),
 @InstitutionID int,
 @CategoryID int
AS
BEGIN
Select C.CohortName, cast(SUM(CS.Correct)*100.0/SUM(CS.Total) as decimal(4,1)) As N_Correct
from CaseModuleScore M
 INNER JOIN CaseSubCategory CS ON M.ModuleStudentID = CS.ModuleStudentID
 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
 INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
 INNER JOIN NurCohort C On C.CohortID = A.CohortID
 WHERE UserType='S' AND UserDeleteData is null
 AND (A.DeletedDate IS NULL) AND I.InstitutionID = @InstitutionID
    AND CS.CategoryID = @CategoryID
 AND CS.SubcategoryID = @SubCategory
 AND A.CohortID in (select * from dbo.funcListToTableInt(@CohortList,'|'))
 AND M.CaseID in  (select * from dbo.funcListToTableInt(@CaseList,'|'))
 AND M.ModuleID in (select * from dbo.funcListToTableInt(@ModuleList,'|'))
 Group BY C.CohortName, CS.SubcategoryID
END
GO
