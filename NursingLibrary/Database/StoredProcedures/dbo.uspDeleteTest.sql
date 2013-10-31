SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteTest]
GO
PRINT 'Creating PROCEDURE uspDeleteTest'
GO

CREATE PROCEDURE [dbo].[uspDeleteTest]
	@TestId INT,
    @UserName VARCHAR(50)
AS
BEGIN
 SET NOCOUNT ON
 /*============================================================================================================    
 //Purpose: To add the ScrambledAnswerChoice in the UserQuestionHistory Table                    
 //Updated: March 21 2013      
 //Author: Liju  
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
	BEGIN TRANSACTION
	BEGIN TRY

        INSERT INTO UserQuestionsHistory
        (
            ID,
            UserTestID,
            QID,
            QuestionNumber,
            Correct,
            TimeSpendForQuestion,
            TimeSpendForRemedation,
            TimeSpendForExplanation,
            AnswerTrack,
            IncorrecCorrect,
            FirstChoice,
            SecondChoice,
            AnswerChanges,
            OrderedIndexes,
			ScrambledAnswerChoice
         )
        SELECT
            ID,
            UserTestID,
            QID,
            QuestionNumber,
            Correct,
            TimeSpendForQuestion,
            TimeSpendForRemedation,
            TimeSpendForExplanation,
            AnswerTrack,
            IncorrecCorrect,
            FirstChoice,
            SecondChoice,
            AnswerChanges,
            OrderedIndexes,
			ScrambledAnswerChoice
        FROM  UserQuestions
        WHERE UserTestID = @TestId

        UPDATE UserQuestionsHistory
        SET DeletedBy = @UserName,
            DeletedDate = GETDATE()
        WHERE UserTestID = @TestId


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
            Override
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
            Override
        FROM  UserTests
        WHERE UserTestID = @TestId

        UPDATE UserTestsHistory
        SET DeletedBy = @UserName,
            DeletedDate = GETDATE()
        WHERE UserTestID = @TestId

		DELETE UserQuestions
		WHERE UserTestID = @TestId

		DELETE UserTests
		WHERE UserTestID = @TestId

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

 SET NOCOUNT OFF
END
GO
PRINT 'Finished creating PROCEDURE uspDeleteTest'
GO


