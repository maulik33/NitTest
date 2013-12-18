IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestQuestions]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestQuestions]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestQuestions]
		@userTestId int, @questionId int, @typeOfFileId varchar(500), @inCorrectOnly bit
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 1 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType ,B.Active
	FROM UserQuestions AS A
		Inner JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND ((@inCorrectOnly = 0 AND A.QuestionNumber = @questionId - 1)
	OR (@inCorrectOnly = 1 AND A.QuestionNumber IN (SELECT MAX(UQ.QuestionNumber)
		FROM UserQuestions AS UQ
		INNER JOIN Questions AS Q ON UQ.QID = Q.QID
		WHERE UQ.Correct <> 1
		AND UQ.UserTestId = @userTestId
		AND UQ.QuestionNumber < @questionId
		AND (Q.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (Q.Active IS NULL OR Q.Active = 1))))

	UNION ALL

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 0 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType,B.Active
	FROM UserQuestions AS A JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId AND A.QID  = @questionId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND (@inCorrectOnly = 0 OR (@inCorrectOnly = 1 AND A.Correct <> 1))

	UNION ALL

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 2 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType,B.Active
	FROM UserQuestions AS A JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND ((@inCorrectOnly = 0 AND A.QuestionNumber = @questionId + 1)
	OR (@inCorrectOnly = 1 AND A.QuestionNumber IN (SELECT MIN(UQ.QuestionNumber)
		FROM UserQuestions AS UQ
		INNER JOIN Questions AS Q ON UQ.QID = Q.QID
		WHERE UQ.Correct <> 1
		AND UQ.UserTestId = @userTestId
		AND UQ.QuestionNumber > @questionId
		AND (Q.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (Q.Active IS NULL OR Q.Active = 1))))

	SET NOCOUNT OFF;
END
GO
