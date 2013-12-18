IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByAnswerChoice]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnStudentSummaryByAnswerChoice]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnStudentSummaryByAnswerChoice]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByAnswerChoice]
	@ProductId int,
	@CohortIds varchar(MAX),
	@TestId int
AS
BEGIN
	 Select UserTests.UserID,Questions.QuestionID, UserAnswers.Correct,s.LastName + ' ' + s.FirstName as StudentName      
     from UserTests, UserAnswers, Questions, NurStudentInfo s    
     where UserTests.UserId = s.UserId     
     AND UserTests.UserTestID=UserAnswers.UserTestID      
     AND UserAnswers.QID=Questions.QID      
     AND dbo.UserTests.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))      
     AND UserTests.TestID= @TestId      
     AND UserTests.ProductID=@ProductId       
     order by StudentName, UserId,Questions.QId    
END
GO
