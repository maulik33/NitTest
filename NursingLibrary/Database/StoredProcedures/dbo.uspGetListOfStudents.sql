IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfStudents]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfStudents]
GO

/****** Object:  StoredProcedure [dbo].[uspGetListOfStudents]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetListOfStudents]
 @CohortId int,
 @InstitutionId int,
 @CaseId int,
 @sText VARCHAR(MAX)

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 Select Distinct S.UserID,S.LastName,S.FirstName,S.UserName,S.EnrollmentID
   from CaseModuleScore M
 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
 INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
 INNER JOIN NurCohort C ON A.CohortID=C.CohortID
 WHERE UserType='S' AND UserDeleteData is null
 AND (A.DeletedDate IS NULL) AND I.InstitutionID = @InstitutionId
  AND C.CohortID = @CohortId AND M.CaseID=@CaseId AND
 (FirstName like '%'+ @sText + '%' OR LastName like '%' + @sText + '%' OR  UserName like '%' + @sText + '%')

 SET NOCOUNT OFF
END
GO
