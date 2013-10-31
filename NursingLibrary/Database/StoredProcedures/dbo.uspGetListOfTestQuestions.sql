IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfTestQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfTestQuestions]
GO

/****** Object:  StoredProcedure [dbo].[uspGetListOfTestQuestions]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetListOfTestQuestions]
@TestId int ,
@TestIds Varchar(4000)
AS
BEGIN
 select TestID,QuestionID,QID,QuestionNumber,id,Q_Norming
 from  dbo.TestQuestions
 where (TestID=@TestId or @TestId =0) and
  ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')

END
GO
