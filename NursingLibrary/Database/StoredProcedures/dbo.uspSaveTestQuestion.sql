IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveTestQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveTestQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveTestQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveTestQuestion]
	 @TestId as int,
	 @QuestionId as varchar(50),
	 @Qid as int,
	 @QuestionNumber as int
 AS
 Begin
       INSERT INTO TestQuestions (TestID, QuestionID, QID, QuestionNumber)
       VALUES (@TestId, @QuestionId, @Qid, @QuestionNumber)
 End
GO
