IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAnswerChoices]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAnswerChoices]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAnswerChoices]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetAnswerChoices]    Script Date: 05/23/2011  ******/

CREATE PROCEDURE [dbo].[uspGetAnswerChoices]
(
 @QuestionId int,
 @ActionType int,
 @QIds Varchar(4000)
)
AS
 BEGIN
 SET NOCOUNT ON
 SELECT
  QID ,
  AnswerID ,
  AIndex ,
  AText ,
  Correct ,
  AnswerConnectID ,
  AType ,
  initialPos ,
  Unit ,
  AlternateAText
 FROM AnswerChoices
 WHERE (QID = @QuestionId or @QuestionId = 0)  AND (AType=@ActionType or @ActionType = 0) And ((QID in (select * from dbo.funcListToTableInt(@QIds,'|'))) or  @QIds='')
 SET NOCOUNT OFF
END
GO
