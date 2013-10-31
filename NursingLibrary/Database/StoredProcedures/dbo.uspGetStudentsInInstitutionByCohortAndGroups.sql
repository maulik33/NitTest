IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentsInInstitutionByCohortAndGroups]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentsInInstitutionByCohortAndGroups]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentsInInstitutionByCohortAndGroups]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStudentsInInstitutionByCohortAndGroups]
@InstitutionIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@GroupIds VARCHAR(MAX),
@SearchText VARCHAR(100)
AS
BEGIN
	
	SET NOCOUNT ON
	
	SELECT dbo.NurStudentInfo.UserID,dbo.NurStudentInfo.LastName+','+dbo.NurStudentInfo.FirstName as NAME  
			,NurStudentInfo.UserName ,NurStudentInfo.Email,NurStudentInfo.LastName,NurStudentInfo.FirstName,NurInstitution.Status  
            FROM   dbo.NurInstitution  
            INNER JOIN dbo.NurStudentInfo  
            INNER JOIN dbo.NusStudentAssign ON dbo.NurStudentInfo.UserID = dbo.NusStudentAssign.StudentID  
            LEFT OUTER JOIN dbo.NurCohort ON dbo.NusStudentAssign.CohortID = dbo.NurCohort.CohortID  
            ON dbo.NurInstitution.InstitutionID = dbo.NurStudentInfo.InstitutionID  
            AND dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID  
            LEFT OUTER JOIN dbo.NurGroup ON dbo.NusStudentAssign.GroupID = dbo.NurGroup.GroupID AND dbo.NurCohort.CohortID = dbo.NurGroup.CohortID  
            LEFT OUTER JOIN dbo.NurProgram  
            INNER JOIN dbo.NurCohortPrograms ON dbo.NurProgram.ProgramID = dbo.NurCohortPrograms.ProgramID ON  
            dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID  
            WHERE     (dbo.NusStudentAssign.DeletedDate IS NULL)  
            AND (dbo.NurCohort.CohortStatus = 1 OR dbo.NurCohort.CohortStatus IS NULL)  
            AND (dbo.NurCohortPrograms.Active IS NULL OR dbo.NurCohortPrograms.Active = 1)  
            AND (dbo.NurProgram.DeletedDate IS NULL) 
            AND ( (@InstitutionIds <> '' AND dbo.NurInstitution.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))  
					OR @InstitutionIds = '')  
            AND ( (@CohortIds <> '' AND dbo.NusStudentAssign.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))  
					OR @CohortIds = '')  
			AND ( (@GroupIds <> '' AND dbo.NusStudentAssign.GroupID IN (select value from  dbo.funcListToTableInt(@GroupIds,'|')))  
					OR @GroupIds = '')  
			AND ( LEN(@SearchText) = 0 OR (LEN(@SearchText) > 0  
			AND (NurStudentInfo.UserName LIKE '%' + @SearchText + '%'  
					OR NurStudentInfo.Email LIKE '%' + @SearchText + '%'  
					OR NurStudentInfo.LastName LIKE '%' + @SearchText + '%'  
					OR NurStudentInfo.FirstName LIKE '%' + @SearchText + '%')))  
            ORDER BY Name ASC  

    SET NOCOUNT OFF

END
GO
