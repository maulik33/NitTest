IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateUserAnswers]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateUserAnswers]
GO

/****** Object:  StoredProcedure [dbo].[uspCreateUserAnswers]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCreateUserAnswers](@QID int, @UserTestID int, @AnswerID int,
	@AText varchar(2000), @Correct int, @AnswerConnectID int, @AType int,
	@AIndex char(1))
AS
INSERT INTO UserAnswers
	(QID, UserTestID, AnswerID, AText, Correct, AnswerConnectID, AType, AIndex)
VALUES
	(@QID, @UserTestID, @AnswerID, @AText, @Correct, @AnswerConnectID, @AType, @AIndex)
GO
