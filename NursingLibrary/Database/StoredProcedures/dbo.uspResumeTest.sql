SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[uspResumeTest]    Script Date: 11/22/2011 16:13:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspResumeTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspResumeTest]
GO
PRINT 'Creating PROCEDURE uspResumeTest'
GO
CREATE PROCEDURE [dbo].[uspResumeTest] 
 @UserTestID VARCHAR(8000),  
 @UserName VARCHAR(50)     
AS    
/*============================================================================================================  
//Purpose: To Resume a Completed Test for Integarted Testing(Only in case of Quit   
//         while taking a test  
//Created: Nov 22 2011  
//Author: Kannan  
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
 BEGIN TRY  
  BEGIN TRANSACTION   
  INSERT INTO UserTestsHistory    
        (    
            UserTestID,    
            UserID,    
            TestID,    
            TestNumber,    
            InsitutionID,    
            CohortID,    
            ProgramID,    
            TestStarted,    
            TestComplited,    
            TestStarted_R,    
            TestComplited_R,    
            TestStatus,    
            TimedTest,    
            TutorMode,    
            ReusedMode,    
            NumberOfQuestions,    
            QuizOrQBank,    
            TimeRemaining,    
            SuspendQuestionNumber,    
            SuspendQID,    
            SuspendType,    
            ProductID,    
            TestName,    
            Override,  
            ResumedDate,  
            ResumedBy  
        )    
        SELECT    
            UserTestID,    
            UserID,    
            TestID,    
            TestNumber,    
            InsitutionID,    
            CohortID,    
            ProgramID,    
            TestStarted,    
            TestComplited,    
            TestStarted_R,    
            TestComplited_R,    
            TestStatus,    
            TimedTest,    
            TutorMode,    
            ReusedMode,    
            NumberOfQuestions,    
            QuizOrQBank,    
            TimeRemaining,    
            SuspendQuestionNumber,    
            SuspendQID,    
            SuspendType,    
            ProductID,    
            TestName,    
            Override,  
            GETDATE(),  
            @UserName  
        FROM  UserTests    
        WHERE UserTestID IN (SELECT value FROM dbo.funcListToTableInt(@UserTestId,'|'))
                 
  UPDATE UserTests SET TestStatus=0, TestComplited=Null WHERE UserTestID IN (SELECT value FROM dbo.funcListToTableInt(@UserTestId,'|'))    
  
     IF @@ERROR <> 0   
 BEGIN  
   SET @ErrorMsg = 'Error while resuming a test'  
  GOTO ERROR  
 END   
  COMMIT TRANSACTION  
 END TRY    
 BEGIN CATCH  
  ROLLBACK TRANSACTION  
  SET @ErrorMsg = 'Rollbacking the ResumeTest'  
  GOTO ERROR  
 END CATCH   
END  
 SET NOCOUNT OFF  
 RETURN @@ERROR  
ERROR:  
   RAISERROR(60100, 18, 1, @ErrorMsg)  
    RETURN -1  
GO 
PRINT 'Finished creating PROCEDURE uspResumeTest'
GO


