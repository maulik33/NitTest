IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTestChildTableRows]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteTestChildTableRows]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteTestChildTableRows]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteTestChildTableRows]
    @TestIds as Varchar(4000)
    As
    Begin
	  Delete from TestQuestions
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from TestCategory
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from Norming
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from Norm
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
    End
GO
