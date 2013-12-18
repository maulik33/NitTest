IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionResultsForCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionResultsForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionResultsForCohort]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionResultsForCohort]
@CohortIds VARCHAR(MAX),
@TestTypes VARCHAR(100),
@TestIds VARCHAR(MAX),
@ChartType INT,
@GroupIds VARCHAR(MAX)
AS
BEGIN
		if (@ChartType = 1)
        BEGIN
        	 SELECT  Cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0/ SUM(1) as decimal(4,1)) AS Total
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
			 INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
			 INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
			 LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
			 AND ((@GroupIds <> '' AND G.GroupID IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|')))
             OR (@GroupIds = ''))
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID, dbo.UserTests.CohortID, dbo.UserTests.ProductID
        END
        else if (@ChartType = 2)
        BEGIN
        	 SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS N_Correct,
             SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
             SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
             SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
             SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
             SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
			 INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
			 INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
			 LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
			 AND ((@GroupIds <> '' AND G.GroupID IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|')))
             OR (@GroupIds = ''))
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID,dbo.UserTests.CohortID, dbo.UserTests.ProductID
        END
END
GO
