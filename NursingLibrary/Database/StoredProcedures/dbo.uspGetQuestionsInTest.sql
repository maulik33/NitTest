IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionsInTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionsInTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionsInTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionsInTest]
  @TestId as int
 AS
 Begin
  SELECT Questions.Stem,Questions.QuestionID,Questions.QID,TestQuestions.QuestionNumber
  FROM dbo.Tests INNER JOIN dbo.TestQuestions
  ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN dbo.Questions
  ON dbo.TestQuestions.QID = dbo.Questions.QID
  WHERE Tests.TestID =@TestId
  order by TypeOfFileID ASC,QuestionNumber ASC
End
GO
