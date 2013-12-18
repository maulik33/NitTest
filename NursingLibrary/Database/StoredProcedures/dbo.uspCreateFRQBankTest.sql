SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateFRQBankTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateFRQBankTest]
GO
      
CREATE PROCEDURE [dbo].[uspCreateFRQBankTest]      
 @userId int,      
 @productId int,      
 @timedTest int,      
 @tutorMode int,      
 @reuseMode int,      
 @numberOfQuestions int,      
 @programId int,      
 @correct int,      
 @CategoryIds varchar(max),      
 @TopicIds varchar(Max),      
 @Name varchar(200),    
 @ProgramofStudyId int      
AS      
BEGIN  
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: Create FRQBankTest based on the programofstudyId 
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
 DECLARE @cohortId int, @institutionId int, @timeRemaining int,@NameCount int,      
  @testNumber int, @userTestId int, @QuestionID int, @QID int, @QuestionNumber int,@TestId int,@Id int      
      
 DECLARE @IsMultiSystem bit      
      
 BEGIN TRY      
      
  SET @testNumber = 1      
  SET @IsMultiSystem = 0      
      
  SELECT @institutionId = InstitutionID,      
   @cohortId=CohortID,      
   @timeRemaining = @numberOfQuestions * 72      
  FROM NurStudentInfo      
   LEFT JOIN dbo.NusStudentAssign ON NurStudentInfo.UserID = NusStudentAssign.StudentID      
  WHERE UserID = @userId      
      
  IF(@Name = 'Multi-Category')      
   SET @IsMultiSystem = 1      
      
  CREATE TABLE #questionTbl (QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, QID int, Scramble varchar(50))      
      
  BEGIN TRANSACTION      
      
   IF (@IsMultiSystem = 1)      
   BEGIN      
    SELECT @NameCount = COUNT(ID)      
    FROM CustomTest      
    WHERE IsMultiCategory = @IsMultiSystem      
     AND IsRemediation = 0      
     AND UserId = @userId      
   END      
   ELSE      
   BEGIN      
    SELECT @NameCount = COUNT(ID)      
    FROM CustomTest      
    WHERE IsMultiCategory = @IsMultiSystem      
     AND IsRemediation = 0      
     AND UserId = @userId      
     AND Category = @CategoryIds      
   END      
      
   SET @NameCount = @NameCount + 1      
   SET @Name = RTRIM(@Name + '.' + CAST(@NameCount as varchar(200)))   
   
   SELECT @TestId = TestId      
   FROM dbo.Tests      
   WHERE TestName = @Name    
   AND ProgramofStudyId = @ProgramofStudyId      
  
   IF (@TestId IS NULL)    
   BEGIN  
    INSERT INTO Tests(  
     TestName,      
     ProductID,      
     DefaultGroup,      
     ReleaseStatus,      
     ActiveTest,      
     TestSubGroup,    
     ProgramofStudyId)      
    VALUES( 
     @Name,      
     @productId,      
     0,      
     'F',      
     1,      
     1,    
     @ProgramofStudyId)      
      
    SET @TestId = CONVERT(int, SCOPE_IDENTITY())  
   END      
   
   DECLARE @CFRTestIdOffset int      
   SELECT @CFRTestIdOffset = CONVERT(int, [Value])      
   FROM dbo.AppSettings      
   WHERE SettingsId = 4   
   IF (@TestId < @CFRTestIdOffset)    
      RAISERROR('Invalid Test Id generated for CFR Test', 12, 2) 
         
    INSERT INTO dbo.CustomTest(      
      Category,      
      Topic,      
      UserId,      
      TestId,      
      IsMultiCategory,      
      IsRemediation)      
    VALUES(      
      @CategoryIds,      
      @TopicIds,      
      @userId,      
      @TestId,      
      @IsMultiSystem,      
      0) 
    -- Create the new test instance   
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
    IsCustomizedFRTest)      
    VALUES      
    (      
    @userId,      
    @TestId,      
    @testNumber,      
    @cohortId,      
    @institutionId,      
    @productId,      
    @programId,      
    GetDate(),      
    0,      
    'B',      
    @timedTest,      
    @tutorMode,      
    @reuseMode,      
    @numberOfQuestions,      
    '',      
    0,      
    @timeRemaining,      
    '01',      
    0,      
    1)      
    IF @@ERROR <> 0      
    BEGIN      
      -- Rollback transaction      
      ROLLBACK      
      -- Raise error and return      
      RAISERROR('Error in inserting TestInstance into UserTests', 16, 1)      
      RETURN      
    END      
    -- Get the user instance id      
   SET @userTestId = SCOPE_IDENTITY()  
   SET ROWCOUNT @numberOfQuestions      
      
    INSERT INTO #questionTbl      
    EXEC uspGetAvailableCFRQuestions @userId, @CategoryIds, @TopicIds,@reuseMode,@ProgramofStudyId      
    SET ROWCOUNT 0      
      
   IF @@ERROR <> 0      
     BEGIN      
       -- Rollback transaction      
       ROLLBACK      
       -- Raise error and return      
       RAISERROR('Error in inserting TestQuestions into temp table', 16, 1)      
       RETURN      
     END      
      
    DECLARE QS CURSOR FOR      
    SELECT QuestionID, QID      
    FROM #questionTbl      
    OPEN QS      
    FETCH QS INTO @QuestionID, @QID      
       
     WHILE @@FETCH_STATUS = 0      
   BEGIN      
     SET @QuestionNumber = @QuestionID      
    -- insert the question      
     EXEC uspInsertTestQuestion @QID, @userTestId, @QuestionNumber      
    FETCH QS INTO @QuestionID, @QID      
   END      
       
    IF @@ERROR <> 0      
     BEGIN      
   -- Rollback transaction      
   ROLLBACK      
   -- Raise error and return      
   RAISERROR('Error in inserting TestQuestions into UserTestQuestions', 16, 1)      
   RETURN      
     END      
       
    DROP TABLE #questionTbl      
       
    CLOSE QS      
    DEALLOCATE QS      
      
  COMMIT TRANSACTION      
  SELECT @userTestId AS UserTestID, @TimeRemaining AS TimeRemaining, @TestId as TestId      
 END TRY      
 BEGIN CATCH      
  ROLLBACK TRANSACTION      
  RAISERROR('Error in uspCreateFRQBankTest', 16, 1)      
 END CATCH      
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished creating PROCEDURE uspCreateFRQBankTest'
GO 



