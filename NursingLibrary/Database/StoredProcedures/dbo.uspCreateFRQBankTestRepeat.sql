SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateFRQBankTestRepeat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateFRQBankTestRepeat]
GO

CREATE PROCEDURE [dbo].[uspCreateFRQBankTestRepeat]  
    @OldUserTestId INT  
AS  
BEGIN
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: Create FRQBankTest based on the programofstudyId (In case of a repeat)
 --      Modified: 09/25/2013 - NURSING_4515(Search by Questions by Topic for PN)        
 --      Author:Liju        
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
   Declare @TestName varchar(200)  
   Declare @OldTestId int  
   Declare @UserId int  
   Declare @TestId int  
   Declare @UserTestId int  
   Declare @IsMultiSystem bit  
   Declare @Name varchar(200)  
   Declare @NameCount int  
   Declare @CategoryId int  
   DECLARE @Pos int  
   Declare @NewTestName varchar(200) 
   Declare @ProgramofStudyId int  
     
   Select @OldTestId = TestId,@UserId = UserId from UserTests where UserTestId=@OldUserTestId  
   select @TestName = TestName,@ProgramofStudyId=ProgramofStudyId from Tests where TestId=@OldTestId  
   select @IsMultiSystem =IsMulticategory from customTest where TestId=@OldTestId   
 BEGIN TRY    
    SET @Pos = charindex('.',@TestName)  
    SET @NewTestName = substring(@TestName,1,@Pos - 1)  
   
  BEGIN TRANSACTION  
     IF(@IsMultiSystem = 1)  
      BEGIN  
     select @NameCount = count(ID)   
     from CustomTest  
     where IsMultiCategory = @IsMultiSystem AND IsRemediation = 0 AND UserId = @UserId  
      END  
     ELSE  
    BEGIN  
     select @CategoryId =category from customTest where TestId=@OldTestId  
     select @NameCount = count(ID)   
     from CustomTest  
     where IsMultiCategory = @IsMultiSystem AND IsRemediation = 0   
        AND UserId = @UserId AND Category = @CategoryId  
    END   
      
     SET @NameCount = @NameCount + 1  
     SET @NewTestName = @NewTestName + '.' + CAST(@NameCount as varchar(200))   
  
        
     SELECT @TestId = TestId  
     FROM dbo.Tests  
     WHERE TestName = @NewTestName  AND ProgramofStudyId = @ProgramofStudyId  
     
     DECLARE @CFRTestIdOffset int         
     SELECT @CFRTestIdOffset = CONVERT(int, [Value])  
     FROM dbo.AppSettings  
     WHERE SettingsId = 4  
       
  
     IF (@TestId < @CFRTestIdOffset)  
    RAISERROR('Invalid Test Id generated for CFR Test', 12, 2)  
  
   IF (@TestId IS NULL)  
    BEGIN    
      INSERT INTO Tests  
      (  
        ProductID,  
        TestName,  
        ActiveTest,  
        TestSubGroup,  
        ReleaseStatus,  
        DefaultGroup,
        ProgramofStudyId 
      )  
      Values  
      (  
        3,  
        @NewTestName,  
        1,  
        1,  
        'F',  
        0 ,
        @ProgramofStudyId
       )  
       SET @TestId = CONVERT(int, SCOPE_IDENTITY())  
     END  
    IF @@ERROR <> 0  
    BEGIN  
     -- Rollback transaction  
     ROLLBACK  
     -- Raise error and return  
     RAISERROR('Error in inserting Tests', 16, 1)  
     RETURN  
    END     
               
    INSERT INTO CustomTest  
     (  
      Category,  
      Topic,  
      UserId,  
      TestId,  
      IsMultiCategory,  
      IsRemediation  
      )   
    SELECT  
      Category,  
      Topic,  
      UserId,  
      @TestId,  
      IsMultiCategory,  
      0  
    FROM CustomTest   
    WHERE UserId=@UserId AND TestId = @OldTestId  
     
     IF @@ERROR <> 0        
     BEGIN        
       -- Rollback transaction        
       ROLLBACK        
       -- Raise error and return        
       RAISERROR('Error in inserting into CustomTest table', 16, 1)        
       RETURN        
     END   
                       
    INSERT INTO UserTests      
      (  
     UserId,   
     TestId,   
     TestNumber,  
     CohortId,  
     InsitutionID,  
     ProductId,  
     ProgramId,  
     TestStarted,  
     TestStatus,      
     QuizOrQbank,  
     TimedTest,  
     TutorMode,  
     ReusedMode,  
     NumberOfQuestions,  
     TestName,  
     SuspendQuestionNumber,      
     TimeRemaining,  
     SuspendType,  
     SuspendQID,  
     IsCustomizedFRTest  
       )      
    SELECT     
     userId,  
     @TestId,  
     1,  
     cohortId,  
     insitutionId,  
     productId,  
     programId,  
     GetDate(),  
     0,      
       'B',  
     timedTest,  
     tutorMode,  
     reusedMode,  
     numberOfQuestions,  
     '',  
     0,  
     numberOfQuestions *72,      
       '01',  
     0,  
     1  
    FROM UserTests WHERE  UserTestId=@OldUserTestId    
  
    IF @@ERROR <> 0  
    BEGIN  
     -- Rollback transaction  
     ROLLBACK  
  
     -- Raise error and return  
     RAISERROR('Error in inserting TestInstance into UserTests', 16, 1)  
     RETURN  
    END   
    SET @UserTestId = SCOPE_IDENTITY()   
    INSERT INTO UserQuestions  
     (  
      QID,  
      UserTestID,  
      QuestionNumber,  
      Correct,  
      TimeSpendForQuestion,  
      TimeSpendForRemedation,  
      TimeSpendForExplanation,  
      AnswerTrack  
     )  
    SELECT  
       QID,  
       @UserTestID,  
       QuestionNumber,  
       2,0, 0, 0, ''  
    FROM UserQuestions WHERE UserTestId=@OldUserTestId  
    IF @@ERROR <> 0  
    BEGIN  
     -- Rollback transaction  
     ROLLBACK  
  
     -- Raise error and return  
     RAISERROR('Error in inserting Questions into UserQuestions', 16, 1)  
     RETURN  
    END  
      COMMIT TRANSACTION  
  SELECT @userTestId AS UserTestID, @TestId as TestId     
 END TRY  
 BEGIN CATCH  
  ROLLBACK TRANSACTION  
  RAISERROR('Error in uspCreateFRQBankTestRepeat', 16, 1)  
 END CATCH  
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished Creating PROCEDURE uspCreateFRQBankTestRepeat'
GO 

