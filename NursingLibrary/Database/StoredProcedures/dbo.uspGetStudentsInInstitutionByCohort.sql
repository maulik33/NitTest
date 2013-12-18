SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentsInInstitutionByCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentsInInstitutionByCohort]
GO

PRINT 'Creating PROCEDURE uspGetStudentsInInstitutionByCohort'
GO

Create PROCEDURE [dbo].[uspGetStudentsInInstitutionByCohort]  
@InstitutionIds VARCHAR(MAX),  
@CohortIds VARCHAR(MAX)
  
AS  
BEGIN  
   
 SET NOCOUNT ON 
 /*============================================================================================================    
 --      Purpose: Retrieves Students Name     
 --      Used by English Nursing Tracking.    
 --      Modified: 04/05/2012    
 --     Author:Mohan   
 ******************************************************************************    
 * This software is the confidential and proprietary information of    
 * Kaplan,Inc. ("Confidential Information").  You shall not    
 * disclose such Confidential Information and shall use it only in    
 * accordance with the terms of the license agreement you entered into    
 * with Kaplan, Inc.    
 *    
 * KAPLAN, INC. MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE     
 * SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT     
 * NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR    
 * A PARTICULAR  PURPOSE, OR NON-INFRINGEMENT. KAPLAN, INC. SHALL     
 * NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF     
 * USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.    
 *****************************************************************************/    
 
 SELECT		
	NSI.UserID,
	NSI.LastName+','+NSI.FirstName as NAME
FROM  
					dbo.NurInstitution NI 
    JOIN			dbo.NurStudentInfo NSI ON NI.InstitutionID = NSI.InstitutionID 
    JOIN			dbo.NusStudentAssign NSA ON NSI.UserID = NSA.StudentID  
    LEFT OUTER JOIN	dbo.NurCohort NC ON NSA.CohortID = NC.CohortID 
    LEFT OUTER JOIN dbo.NurProgram NP  
    INNER JOIN		dbo.NurCohortPrograms NCP ON NP.ProgramID = NCP.ProgramID ON  
					NC.CohortID = NCP.CohortID  
WHERE     
		(NSA.DeletedDate IS NULL)  
    AND (NC.CohortStatus = 1 OR NC.CohortStatus IS NULL)  
    AND (NCP.Active IS NULL OR NCP.Active = 1)  
    AND (NP.DeletedDate IS NULL) AND (NI.Status = 1)  
    AND ( (@InstitutionIds <> '' AND NI.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))  
	OR  @InstitutionIds = '')  
    AND ( (@CohortIds <> '' AND NSA.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))  
	OR  @CohortIds = '')      
    ORDER BY Name ASC  
    
    SET NOCOUNT OFF  
  
END   
GO

PRINT 'Finished creating PROCEDURE uspGetStudentsInInstitutionByCohort'
GO 

