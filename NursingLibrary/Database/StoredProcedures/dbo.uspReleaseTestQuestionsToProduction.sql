IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseTestQuestionsToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseTestQuestionsToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseTestQuestionsToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[uspReleaseTestQuestionsToProduction]
@Id as INT,
@TestId as INT,
@QuestionId as Varchar(50),
@QID as INT,
@QuestionNumber as INT,
@QNorming as Float
AS
Begin
SET IDENTITY_INSERT TestQuestions ON
	Insert Into TestQuestions  (id, TestID, QuestionID, QID, QuestionNumber, Q_Norming)
	values(@Id,@TestId,@QuestionId,@QID,@QuestionNumber,@QNorming)
SET IDENTITY_INSERT TestQuestions OFF
End
GO
