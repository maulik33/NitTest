SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentListForSetOverride]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudentListForSetOverride]
GO
/****** Object:  StoredProcedure [dbo].[uspGetStudentListForSetOverride]    Script Date: 11/22/2011 16:44:45 ******/
PRINT 'Creating PROCEDURE uspGetStudentListForSetOverride'
GO
  
CREATE PROCEDURE [dbo].[uspGetStudentListForSetOverride]      
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
//Purpose: Getting the Student List for displaying in the SetOverride    
//modified: Dec 22 2011    
//Author:Kannan
//Modified Date: March 12 2013
//Modified By: Atul,Maulik
//Modified For: NURSING-3059 (Removed function call from where clause to optimize setoverride)
//            : NURSING-3590 - Returning Institution Name and CohortName in which student has taken test
//Sample Usage: exec uspGetStudentListForSetOverride @InstitutionId =41, @CohortIds ='',
//				@FirstName ='', @LastName ='', @UserName ='', @TestName ='', @ShowIncompleteOnly = 1

**********************************************************************************************************    
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
*************************************************************************************************************/    
 SET NOCOUNT ON    
 DECLARE @ErrorMsg varchar(255)
 DECLARE @CohortIdsList Table (Value int)    
BEGIN      
 
 INSERT @CohortIdsList(Value)
 SELECT Value FROM dbo.funcListToTableInt(@CohortIds,'|')
 
 SELECT TOP 3001 U.UserTestID,      
  S.FirstName,      
  S.LastName,      
  S.UserName,      
  T.TestName,     
  T.ProductID,     
  U.TestStatus,     
  U.SuspendQuestionNumber,    
  U.NumberOfQuestions,    
  U.TimeRemaining,    
  UQ.AnswerTrack ,  
  UQ.OrderedIndexes,   
  DATEADD(hour, TZ.[Hour], TestStarted) as TestStarted,      
  DATEADD(hour, TZ.[Hour], TestComplited) as TestCompleted,  
  dbo.ReturnGetTestTimeUsed(U.UserTestID) as TimeUsed,  
  dbo.UFNReturnAnsweredQuestionCount(U.UserTestID) as AnsweredCount,
  dbo.UFNGetLastQuestionAnswer(UQ.QID,U.UserTestID) as LastQuestionAnswer,
  POS.ProgramofStudyName,
  I.InstitutionName,
  NC.CohortName
 FROM dbo.NurStudentInfo S      
   INNER JOIN dbo.UserTests U ON S.UserID = U.UserID 
   FULL JOIN @CohortIdsList CL ON (@CohortIds = '' OR  u.CohortID = CL.Value)         
   LEFT JOIN dbo.UserQuestions UQ ON UQ.QuestionNumber =U.SuspendQuestionNumber    
   AND UQ.UserTestID = U.userTestID    
   INNER JOIN dbo.NurCohort NC ON U.CohortID = NC.CohortID 
   AND NC.InstitutionID = S.InstitutionID          
   INNER JOIN dbo.Tests T ON U.TestID = T.TestID      
   INNER JOIN dbo.NurInstitution AS I      
   ON I.InstitutionId = S.InstitutionId      
   INNER JOIN TimeZones TZ      
   ON I.TimeZone = TZ.TimeZoneID
   INNER JOIN ProgramofStudy POS     
   ON POS.ProgramofStudyId = I.ProgramofStudyId    
 WHERE      
  (@InstitutionId = 0      
    OR S.InstitutionId = @InstitutionId)      
  AND (@CohortIds = '' OR u.CohortID = CL.Value)
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
   SET @ErrorMsg = 'Error while retrieving testlist for students'    
  GOTO ERROR    
 END     
SET NOCOUNT OFF    
 RETURN @@ERROR    
ERROR:    
   RAISERROR(60100, 18, 1, @ErrorMsg)    
    RETURN -1 
GO
PRINT 'Finished creating PROCEDURE uspGetStudentListForSetOverride'
GO





