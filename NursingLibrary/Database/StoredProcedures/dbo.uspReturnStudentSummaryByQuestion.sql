IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnStudentSummaryByQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnStudentSummaryByQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByQuestion]
	  @InstitutionId INT,
      @ProductID int,
      @CohortIds varchar(MAX),
      @TestID int
AS
BEGIN
SELECT UserTests.UserID,Questions.QuestionID,
          AIndex = CASE UserAnswers.AIndex
                        WHEN 'A' THEN '1'
                        WHEN 'B' THEN '2'
                        WHEN 'C' THEN '3'
                        WHEN 'D' THEN '4'
                        WHEN 'E' THEN '5'
                        WHEN 'F' THEN '6'
                        END,
    UserAnswers.Correct, stu.LastName +' '+ stu.FirstName as StudentName
    FROM  dbo.Tests INNER JOIN
                dbo.UserTests     ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
                dbo.UserQuestions ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN
                dbo.Questions     ON dbo.UserQuestions.QID = dbo.Questions.QID INNER JOIN
                dbo.UserAnswers   ON dbo.UserQuestions.QID = dbo.UserAnswers.QID
                AND dbo.UserQuestions.UserTestID = dbo.UserAnswers.UserTestID
                INNER JOIN dbo.NurCohort ON UserTests.CohortID = dbo.NurCohort.CohortID
    AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
    OR @CohortIds = '0')
    AND UserTests.TestID= @TestID
    AND UserTests.ProductID = @ProductID
    AND dbo.NurCohort.InstitutionID = @InstitutionId
    LEFT JOIN dbo.NurStudentInfo stu ON  UserTests.UserID = stu.UserId    
    order by StudentName,UserTests.UserID

END
GO
