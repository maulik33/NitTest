IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestionsQbank]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestQuestionsQbank]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestQuestionsQbank]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestQuestionsQbank]
 @rowLimit int, @testId int, @userId int, @institutionId int,
 @programId int, @cohortId int, @correct int, @filter int, @options varchar(500)
AS

IF (@filter = 3)
 BEGIN
	 -- return all questions
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
			ON TestQuestions.QID=Questions.QID
	  WHERE TestQuestions.TestID = @testId
			AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
  END
ELSE IF (@filter = 1)
  BEGIN
	  -- return all unused questions
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID  NOT IN
	   (SELECT distinct UserQuestions.QID
		FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
		WHERE UserTests.UserID = @userId
		AND UserTests.InsitutionID = @institutionId
		AND dbo.UserTests.TestID = @testId)
	    AND TestQuestions.TestID = @testId
	    AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END
ELSE IF (@filter = 4)
   BEGIN
	  -- return all incorrect questions
	  -- @filter = 4, @correct=0: Incorrect only
	
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID IN
	   (
		 SELECT  UserQuestions.QID
			FROM  UserQuestions INNER JOIN UserTests
				ON UserQuestions.UserTestID = UserTests.UserTestID
			WHERE (Correct = 0 OR Correct = 2) AND UserTests.UserID = @userId
				AND UserTests.InsitutionID = @institutionId
			  AND dbo.UserTests.TestID = @testId
	
		  Except
	
		SELECT  UserQuestions.QID
		FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
		WHERE (Correct = 1) AND UserTests.UserID = @userId
			  AND UserTests.InsitutionID = @institutionId
			  AND dbo.UserTests.TestID = @testId
		)
	   AND TestQuestions.TestID = @testId
	   AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END
ELSE
   BEGIN
	  -- return all unused and incorrect/correct questions
	  -- @filter = 2, @correct=0: Unused and Incorrect
	
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID  NOT IN
	   (SELECT distinct UserQuestions.QID
	   FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
	   WHERE Correct = 1 AND UserTests.UserID = @userId
		AND UserTests.InsitutionID = @institutionId
		AND dbo.UserTests.TestID = @testId)
	   AND TestQuestions.TestID = @testId
	   AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END
GO
