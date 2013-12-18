IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByQuestionHeader]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnStudentSummaryByQuestionHeader]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnStudentSummaryByQuestionHeader]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByQuestionHeader]
@ProductId int,
@CohortIds varchar(MAX),
@TestId int
AS
BEGIN
SELECT Questions.QID,Questions.QuestionID
				FROM  dbo.Tests INNER JOIN
                dbo.UserTests     ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
                dbo.UserQuestions ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN
                dbo.Questions     ON dbo.UserQuestions.QID = dbo.Questions.QID INNER JOIN
                dbo.UserAnswers   ON dbo.UserQuestions.QID = dbo.UserAnswers.QID
                AND dbo.UserQuestions.UserTestID = dbo.UserAnswers.UserTestID
                WHERE dbo.Tests.ProductID=@ProductID
                AND dbo.UserTests.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
				AND dbo.Tests.TestID = @TestID
Group by Questions.QID, Questions.QuestionID
order by QuestionId

END
GO
