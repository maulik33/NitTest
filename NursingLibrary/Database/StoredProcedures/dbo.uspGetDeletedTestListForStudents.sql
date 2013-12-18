SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetDeletedTestListForStudents]    Script Date: 11/22/2011 16:35:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDeletedTestListForStudents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetDeletedTestListForStudents]
GO
PRINT 'Creating PROCEDURE uspGetDeletedTestListForStudents'
GO
CREATE PROCEDURE [dbo].[uspGetDeletedTestListForStudents]    
(    
 @InstitutionId int,    
 @CohortIds varchar(8000), 
 @FirstName varchar(100),    
 @LastName varchar(100),    
 @UserName varchar(100),    
 @TestName varchar(100),    
 @ShowIncompleteOnly bit    
)    
AS    
/*============================================================================================================    
//Purpose: Get deleted student list.
//modified: Dec 22 2011,06/14/2013 
//Author: Kannan
//Modified BY : Maulik
//Modified For :NURSING-3590 - Returning Institution Name and CohortName in which student has taken test
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
SET NOCOUNT ON    
DECLARE @ErrorMsg varchar(255)    
BEGIN  

 SELECT TOP 3001 U.UserTestID,    
  S.FirstName,    
  S.LastName,    
  S.UserName,    
  T.TestName,    
  U.TestStatus,    
        U.DeletedBy,    
        U.DeletedDate,    
  DATEADD(hour, TZ.[Hour], TestStarted) as TestStarted,  
  DATEADD(hour, TZ.[Hour], TestComplited) as TestCompleted,  
  dbo.ReturnGetTestTimeUsed(U.UserTestID) as TimeUsed,  
  dbo.UFNReturnAnsweredQuestionCount(U.UserTestID) as AnsweredCount,  
  U.NumberOfQuestions,
  I.InstitutionName,
  NC.CohortName,
  POS.ProgramofStudyName   
 FROM dbo.NurStudentInfo S    
  INNER JOIN dbo.UserTestsHistory U ON S.UserID = U.UserID    
  INNER JOIN dbo.NurCohort NC ON U.CohortID = NC.CohortID 
  AND (@CohortIds =''    
    OR U.CohortId in (select value from  dbo.funcListToTableInt(@CohortIds,'|') ))    
   AND NC.InstitutionID = S.InstitutionID        
  INNER JOIN dbo.Tests T ON U.TestID = T.TestID    
  INNER JOIN dbo.NurInstitution AS I    
  ON I.InstitutionId = S.InstitutionId    
  INNER JOIN TimeZones TZ    
  ON I.TimeZone = TZ.TimeZoneID
  INNER JOIN ProgramofStudy POS     
  ON POS.ProgramofStudyId = I.ProgramofStudyId   
 WHERE    
 (U.DeletedDate Is Not Null and U.DeletedBy Is Not Null)    
 AND (@InstitutionId = 0    
   OR S.InstitutionId = @InstitutionId)    
 AND (@CohortIds =''    
    OR U.CohortId in (select value from  dbo.funcListToTableInt(@CohortIds,'|') ))              
 AND (@FirstName = ''    
   OR S.FirstName  like +'%'+ @FirstName + '%' )    
 AND (@LastName = ''    
   OR S.LastName like +'%'+ @LastName + '%'  )    
 AND (@UserName = ''    
   OR S.UserName like +'%'+ @UserName + '%'  )    
 AND (@TestName = ''    
   OR T.TestName like +'%'+ @TestName + '%'  )    
 AND (@ShowIncompleteOnly = 0    
  OR (@ShowIncompleteOnly = 1 AND ISNULL(U.TestStatus, 0) != 1))    
END    
IF @@ERROR <> 0     
 BEGIN    
   SET @ErrorMsg = 'Error while retrieving deleted list of usertests'    
  GOTO ERROR    
 END     
SET NOCOUNT OFF    
 RETURN @@ERROR    
ERROR:    
   RAISERROR(60100, 18, 1, @ErrorMsg)    
    RETURN -1 
GO
PRINT 'Finished creating PROCEDURE uspGetDeletedTestListForStudents'
GO





