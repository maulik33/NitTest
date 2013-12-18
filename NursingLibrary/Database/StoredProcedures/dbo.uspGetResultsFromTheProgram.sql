IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetResultsFromTheProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspGetResultsFromTheProgram]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetResultsFromTheProgram]
@UserTestID INT,
@Charttype INT
AS
BEGIN
	IF(@Charttype = 1)
	BEGIN
		
		SELECT  ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total
		FROM    dbo.UserQuestions
		WHERE   UserTestID = @UserTestID
	END
	ELSE IF(@Charttype = 2)
	BEGIN
		
		SELECT  ISNULL(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END),0)  AS N_Correct,
                ISNULL(SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END),0)  AS N_InCorrect,
                ISNULL(SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END),0)  AS N_NAnswered,
                ISNULL(SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END),0)  AS N_CI,
                ISNULL(SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END),0)  AS N_II,
                ISNULL(SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END),0)  AS N_IC
         FROM    dbo.UserQuestions
         WHERE   UserTestID = @UserTestID
	END
END
GO
