SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateTest]
GO

CREATE PROCEDURE [dbo].[uspCreateTest]      
 @userId int, @productId int, @programId int, @timedTest int,@testId int      
AS      
BEGIN     
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: To Include the new field SecondPerQuestion while creating a test  
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
 DECLARE @userTestId int, @testNumber int, @firstQuestionId int, @QuestionID int, @QID int, @QuestionNumber int      
      
    Declare @numberOfQuestions int, @cohortId int,@institutionId int, @scramble int, @TimeRemaining int ,@SecondPerQuestion int    
    exec [dbo].[uspGetTestQuestionCountByFileTypeId] @TestId = @testId, @TypeOfFileId = '03', @TotalCount = @numberOfQuestions OUTPUT      
      
 Select @SecondPerQuestion =  SecondPerQuestion from Tests where TestID=@testId   
 if ((@SecondPerQuestion = 0) or (@SecondPerQuestion is null))
        SET @SecondPerQuestion = 72  
 SET @TimeRemaining = @SecondPerQuestion * @numberOfQuestions      
      
   SELECT  @institutionId = InstitutionID,@cohortId=CohortID      
 FROM NurStudentInfo LEFT JOIN dbo.NusStudentAssign      
  ON NurStudentInfo.UserID = NusStudentAssign.StudentID      
 WHERE UserID = @userId      
      
    SELECT  @scramble = Scramble FROM Products WHERE ProductID = @productId      
      
 -- Create temp table for iteration of questions      
 CREATE TABLE #questionTbl (QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, QID int, QuestionNumber int)      
      
 -- Check for existing test      
 SET @testNumber = (SELECT COUNT(TestNumber) FROM dbo.UserTests WHERE UserID = @userId AND TestID = @testId)      
 IF(@testNumber IS NULL)      
  SET @testNumber = 1      
 ELSE      
  SET @testNumber = @testNumber + 1      
 END      
      
 BEGIN TRANSACTION      
  -- Create the new test instance      
  INSERT INTO UserTests      
   (UserId, TestId, TestNumber, CohortId, InsitutionID, ProductId, ProgramId, TestStarted, TestStatus,      
    QuizOrQbank, TimedTest, TutorMode, ReusedMode, NumberOfQuestions, TestName, SuspendQuestionNumber,      
    TimeRemaining, SuspendType, SuspendQID)      
  VALUES      
   (@userId, @testId, @testNumber, @cohortId, @institutionId, @productId, @programId, GetDate(), 0,      
    'Q', @timedTest, 0, 0, @numberOfQuestions, NULL, 0, @TimeRemaining, '03', 0)      
      
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
      
  INSERT INTO #questionTbl      
   EXEC uspGetQuestionsToCreateTest @testId, @scramble      
      
  IF @@ERROR <> 0      
   BEGIN      
    -- Rollback transaction      
    ROLLBACK      
      
    -- Raise error and return      
    RAISERROR('Error in inserting TestQuestions into temp table', 16, 1)      
    RETURN      
   END      
      
  DECLARE QS CURSOR FOR      
  SELECT QuestionID, QID, QuestionNumber      
  FROM #questionTbl      
      
  OPEN QS      
  FETCH QS INTO @QuestionID, @QID, @QuestionNumber      
      
  SET @firstQuestionId = @QID      
      
  WHILE @@FETCH_STATUS = 0      
   BEGIN      
    IF @scramble = 1      
     SET @QuestionNumber = @QuestionID      
      
    -- insert the question      
    EXEC uspInsertTestQuestion @QID, @userTestId, @QuestionNumber      
      
    FETCH QS INTO @QuestionID, @QID, @QuestionNumber      
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
      
-- Return First Question FileType : Can be refactored later.      
 SELECT UserTestID, @numberOfQuestions AS NumberOfQuestions,      
  @TimeRemaining AS TimeRemaining,      
  CAST(Q.TypeOfFileID AS int) AS TypeOfFileID      
 FROM UserQuestions UQ INNER JOIN Questions Q      
 ON Q.QId = UQ.QId      
 WHERE UQ.UserTestId = @userTestId      
 AND UQ.QuestionNumber = 1   
GO

PRINT 'Finished creating PROCEDURE uspCreateTest'
GO 


