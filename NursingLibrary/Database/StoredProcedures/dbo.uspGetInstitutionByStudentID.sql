IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionByStudentID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionByStudentID]
GO
PRINT 'Creating PROCEDURE uspGetInstitutionByStudentID'
GO
/****** Object:  StoredProcedure [dbo].[uspGetInstitutionByStudentID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetInstitutionByStudentID]
 @UserID INT
AS
 /*============================================================================================================      
//Purpose: Retrive Helpful doc details   
                  
//Modified: july 05 2013      
//Modified By :Shodhan  
//Purpose : NURSING-4201(On exporting Student Report Card, Institution name is not displayed properly.)    
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
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

SELECT NurStudentInfo.UserID,  
     NurInstitution.InstitutionID,  
     NurInstitution.InstitutionName,
     NurInstitution.ProgramOfStudyId,
     ProgramofStudy.ProgramofStudyName
 FROM   dbo.NurCohort INNER JOIN  
           dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID INNER JOIN  
           dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID INNER JOIN  
           dbo.NurInstitution ON dbo.NurCohort.InstitutionID = dbo.NurInstitution.InstitutionID  INNER JOIN
           ProgramofStudy ON NurInstitution.ProgramOfStudyId = ProgramofStudy.ProgramOfStudyId
 WHERE (dbo.NurStudentInfo.UserID = @UserID) 
END
GO
PRINT 'Finished creating PROCEDURE uspGetInstitutionByStudentID'
GO
