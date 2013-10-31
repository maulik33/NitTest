SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateSMTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateSMTest]
GO 
CREATE PROCEDURE [dbo].[uspCreateSMTest]    
 @UserId int,    
 @ProductId int,    
 @TimedTest int,    
 @TutorMode int,    
 @ReuseMode int,    
 @ProgramId int,    
 @SMTestId int    
AS            
BEGIN           
SET NOCOUNT ON                  
/*============================================================================================================                
 --      Purpose: To insert the SecondPerQuestion value when 'Essential Skill Test' are taken
 --      Modified: 06/15/2012                
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
DECLARE @cohortId int, @institutionId int, @timeRemaining int,@TestCount int,@SkillModuleOffset int,@numberOfQuestions int,                    
  @TestId int, @testNumber int, @userTestId int, @QuestionID int, @QID int, @QuestionNumber int,@Id int, @BaseTestId int,@SecondPerQuestion int                
                     
  Declare @Name nVarchar(200)                    
  DECLARE @IsMultiSystem bit                    
                    
 BEGIN TRY                    
  SET @ProductId = 6                  
  SET @testNumber = 1                    
                
  Select @BaseTestId = TestId from SMTests where NewTestId = @SMTestId                
    set @TestCount = 0                
                    
    if(@BaseTestId is not null)                
    BEGIN                    
  SELECT @TestCount = COUNT(SMTestId)                    
  FROM SMTests                    
  WHERE  UserId = @userId AND  TestId = @BaseTestId               
 END                
 ELSE                
 BEGIN                
 SET @BaseTestId = @SMTestId                
 END                
                            
 SELECT @numberOfQuestions = Count(*)                     
 from TestQuestions                     
 WHERE TestId = @BaseTestId                             
                 
 SELECT @SecondPerQuestion  =  SecondPerQuestion      
 from Tests          
 WHERE TestId = @BaseTestId            
         
 IF ((@SecondPerQuestion = 0) or (@SecondPerQuestion is null))  
    SET @SecondPerQuestion = 72    
                
    SELECT @institutionId = InstitutionID,                    
           @cohortId=CohortID,                    
           @timeRemaining = @numberOfQuestions * @SecondPerQuestion                    
    FROM NurStudentInfo                    
         LEFT JOIN dbo.NusStudentAssign ON NurStudentInfo.UserID = NusStudentAssign.StudentID                    
    WHERE UserID = @userId                    
                    
    CREATE TABLE #questionTbl                     
    (                    
      QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,                     
      QID int,                     
      Scramble varchar(50)                    
  )                    
                    
  BEGIN TRANSACTION                         
      SELECT @Name = TestName                     
   FROM Tests                   
   WHERE  TestID = @BaseTestId                   
           
    SET @TestCount = @TestCount + 1                    
    SET @Name = RTRIM(@Name + '.' + 'Quiz' + '.' + CAST(@TestCount as varchar(200)))                    
                     
    SELECT @TestId = TestId           
    FROM dbo.Tests                    
    WHERE TestName = @Name                    
                     
   IF (@TestId IS NULL)                    
    BEGIN                    
     INSERT INTO Tests(                    
      TestName,                    
	  ProductID,                    
      DefaultGroup,                    
      ReleaseStatus,                    
      ActiveTest,                    
      TestSubGroup,
      SecondPerQuestion)                    
     VALUES(                    
      @Name,                    
      @productId,                    
      0,                    
      'F',                    
      1,                    
      1,
      @SecondPerQuestion)                    
                    
     SET @TestId = CONVERT(int, SCOPE_IDENTITY())                    
     END                    
                    
   SELECT @SkillModuleOffset = CONVERT(int, [Value])                    
   FROM dbo.AppSettings                    
   WHERE SettingsId = 4                    
                    
   IF (@TestId < @SkillModuleOffset)                    
    RAISERROR('Invalid Test Id generated for Skill module Test', 12, 2)                    
                    
                    
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
  IsCustomizedFRTest                  
     )                    
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
  1                  
   )                    
                    
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
                                
            INSERT INTO SMTests                    
            (                    
    UserId,                    
    TestId,                  
    NewTestId                    
            )                    
            VALUES                    
           (                    
    @userId,                    
    @BaseTestId,                  
    @TestId                   
            )                    
                    
    INSERT INTO #questionTbl                    
    Select QID,newId() as Scramble                     
    FROM TestQuestions                     
    WHERE TestID = @BaseTestId                      
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
  RAISERROR('Error in uspCreateSMTest ', 16, 1)                    
 END CATCH         
 SET NOCOUNT OFF                    
END 
GO

PRINT 'Finished creating PROCEDURE uspCreateSMTest'
GO 




   