IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateQBankTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateQBankTest]
GO

/****** Object:  StoredProcedure [dbo].[uspCreateQBankTest]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[uspCreateQBankTest]
	@userId int, @productId int, @programId int, @timedTest int,
	@tutorMode int, @reuseMode int, @numberOfQuestions int, @testId int,
	@correct int, @options varchar(500)
AS

	-- Gokul - 3/17/2011
	-- This SP is created as a patch for 4/5/2011 release. Functionality has to be merged with uspCreateTest later

	DECLARE @cohortId int, @institutionId int, @scramble int, @timeRemaining int

	SELECT  @institutionId = InstitutionID, @cohortId=CohortID,
		@scramble = 1, @timeRemaining = @numberOfQuestions * 72
	FROM NurStudentInfo LEFT JOIN dbo.NusStudentAssign
		ON NurStudentInfo.UserID = NusStudentAssign.StudentID
	WHERE UserID = @userId

	DECLARE @testNumber int, @userTestId int, @QuestionID int, @QID int, @QuestionNumber int

	-- Create temp table for iteration of questions
	CREATE TABLE #questionTbl (QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, QID int, QuestionNumber int)

-- Check for existing test
	SET @testNumber = (SELECT MAX(TestNumber) FROM dbo.UserTests WHERE UserID = @userId AND TestID = @testId)
	IF(@testNumber IS NULL)
		SET @testNumber = 1
	ELSE
		SET @testNumber = @testNumber + 1

	BEGIN TRANSACTION
	-- Create the new test instance
	INSERT INTO UserTests
		(UserId, TestId, TestNumber, CohortId, InsitutionID, ProductId, ProgramId, TestStarted, TestStatus,
			QuizOrQbank, TimedTest, TutorMode, ReusedMode, NumberOfQuestions, TestName, SuspendQuestionNumber,
			TimeRemaining, SuspendType, SuspendQID)
	VALUES
		(@userId, @testId, @testNumber, @cohortId, @institutionId, @productId, @programId, GetDate(), 0,
			'B', @timedTest, @tutorMode, @reuseMode, @numberOfQuestions, '', 0, @timeRemaining,
			'01', 0)

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
		EXEC uspGetTestQuestionsQbank @numberOfQuestions, @testId, @userId, @institutionId,
			@programId, @cohortId, @correct, @reuseMode, @options

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

	WHILE @@FETCH_STATUS = 0
		BEGIN
			-- check for proper ordering
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

	SELECT @userTestId AS UserTestID, @TimeRemaining AS TimeRemaining
GO
