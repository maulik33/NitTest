IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCopyCustomTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCopyCustomTest]
GO

/****** Object:  StoredProcedure [dbo].[uspCopyCustomTest]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCopyCustomTest]
 @OriginalTestID int,
 @NewTestID int
AS

BEGIN
 INSERT INTO TestQuestions (TestID, QuestionID, QID, QuestionNumber, Q_Norming)
  SELECT @NewTestID, QuestionID, QID, QuestionNumber, Q_Norming FROM TestQuestions
  WHERE TestID=@OriginalTestID
END
GO
