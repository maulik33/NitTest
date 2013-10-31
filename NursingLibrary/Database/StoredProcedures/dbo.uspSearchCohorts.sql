
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchCohorts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchCohorts]
GO

PRINT 'Creating PROCEDURE uspSearchCohorts'
GO
/****** Object:  StoredProcedure [dbo].[uspSearchCohorts]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspSearchCohorts]    Script Date: 04/22/2011 15:26:31 ******/

CREATE PROCEDURE [dbo].[uspSearchCohorts]
(
 @InstitutionIds VARCHAR(8000),
 @SearchString VARCHAR(1000),
 @TestId INT,
 @DateFrom VARCHAR(15),
 @DateTo VARCHAR(15),
 @CohortStatus INT,
 @ProgramofStudyId INT     
)    
AS    
    
SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose:  Returns Cohort list for the specified search criteria  
 -- Modified For: Sprint 44: PN RN Unification. Changes done for NURSING-3824  
 --     to return Program Of Study Name for the institution.
                  sprint 51-Ability to designate the students as "repeating students" for  NUSRING-3760
 -- Modified On: 05/24/2013  ,4/9/2013
 -- Modified By: Atul Gupta  ,Karthik CS
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
    
 Declare @currentdate AS DateTime 
  SET @currentdate= getDate();   
 IF (@DateTo != ''  OR @DateFrom != '')      
  BEGIN      
  SELECT  
  C.CohortID,  
  C.CohortDescription,  
  C.CohortName,  
  C.CohortStartDate,  
  C.CohortEndDate,  
  I.InstitutionName,  
  I.Annotation,  
  PS.ProgramofStudyName,  
  COUNT(Distinct S.StudentID) As Students,
 (Select COUNT(SI.UserId) FROM Nurstudentinfo AS SI
INNER JOIN NusStudentAssign AS SA
ON SI.UserId = SA.StudentId
WHERE SA.CohortId = C.CohortID AND SI.RepeatExpiryDate >= @currentdate) as RepeatingStudentCount  
   FROM   dbo.NurCohort C      
   LEFT OUTER JOIN dbo.NurInstitution I      
   ON C.InstitutionID = I.InstitutionID  
   INNER JOIN dbo.ProgramofStudy PS  
   ON I.ProgramOfStudyId = PS.ProgramofStudyId  
   LEFT JOIN dbo.NusStudentAssign S      
   ON C.CohortID = S.CohortID      
   LEFT OUTER JOIN dbo.NurCohortPrograms CP      
   ON C.CohortID = CP.CohortID      
   LEFT JOIN dbo.NurProgramProduct PP      
   ON CP.ProgramID = PP.ProgramID      
   LEFT JOIN dbo.NurProductDatesCohort D      
   ON D.CohortID = C.CohortID  
   left join NurStudentInfo on NurStudentInfo.UserID=S.StudentID 
  WHERE C.CohortStatus = @CohortStatus AND S.DeletedDate IS NULL      
  AND (@TestId = 0 OR D.ProductID =  @TestId)      
  AND (@InstitutionIds = '' OR I.InstitutionId IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))      
  AND (@SearchString = '' OR(C.CohortName like +'%'+ @SearchString +'%'))      
  AND (@DateFrom = '' OR D.StartDate  > CAST(CONVERT(VARCHAR(10), @DateFrom, 111) AS DATETIME))      
  AND (@DateTo = '' OR D.EndDate < CAST(CONVERT(VARCHAR(10), @DateTo, 111) AS DATETIME))     
  AND (@ProgramofStudyId = 0 OR I.ProgramOfStudyId = @ProgramofStudyId)    
  GROUP BY C.CohortID, C.CohortDescription, C.CohortName, C.CohortStartDate,      
  C.CohortEndDate, I.InstitutionName, I.Annotation, PS.ProgramofStudyName      
  END      
ELSE      
  BEGIN      
 SELECT      
  C.CohortID,  
  C.CohortDescription,  
  C.CohortName,  
  C.CohortStartDate,  
  C.CohortEndDate,  
  I.InstitutionName,  
  I.Annotation,  
  PS.ProgramofStudyName,  
  COUNT(Distinct S.StudentID) As Students ,
 (Select COUNT(SI.UserId) FROM Nurstudentinfo AS SI
INNER JOIN NusStudentAssign AS SA
ON SI.UserId = SA.StudentId
WHERE SA.CohortId = C.CohortID AND SI.RepeatExpiryDate >= @currentdate) as RepeatingStudentCount  
 FROM dbo.NurCohort C      
  LEFT OUTER JOIN dbo.NurInstitution I      
  ON C.InstitutionID = I.InstitutionID  
  INNER JOIN dbo.ProgramofStudy PS  
  ON I.ProgramOfStudyId = PS.ProgramofStudyId  
  LEFT OUTER JOIN dbo.NusStudentAssign S      
  ON S.CohortID = C.CohortID      
  LEFT OUTER JOIN dbo.NurProductDatesCohort D     
  ON D.CohortID = C.CohortID AND D.ProductID =  @TestId
   left join NurStudentInfo on NurStudentInfo.UserID=S.StudentID 
 WHERE C.CohortStatus = @CohortStatus AND S.DeletedDate IS NULL      
  AND (@InstitutionIds = '' OR I.InstitutionId IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))      
  AND (@SearchString = '' OR( C.CohortName like +'%'+ @SearchString +'%'))    
  AND (@ProgramofStudyId = 0 OR I.ProgramOfStudyId = @ProgramofStudyId)      
  GROUP BY C.CohortID, C.CohortDescription, C.CohortName, C.CohortStartDate,      
  C.CohortEndDate, I.InstitutionName, I.Annotation, PS.ProgramofStudyName   
  END         
END  
SET NOCOUNT OFF
GO
 PRINT 'Finished creating PROCEDURE uspSearchCohorts' 
GO

