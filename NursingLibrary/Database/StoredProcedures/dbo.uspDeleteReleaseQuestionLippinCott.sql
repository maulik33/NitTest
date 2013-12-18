IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteReleaseQuestionLippinCott]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteReleaseQuestionLippinCott]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteReleaseQuestionLippinCott]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspDeleteReleaseQuestionLippinCott]
@LippincottIds Varchar(4000)
AS
BEGIN
 Delete from QuestionLippincott
  where (LippincottID in (select * from dbo.funcListToTableInt(@LippincottIds,'|')))
END
GO
