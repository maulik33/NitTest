IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTestQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteTestQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteTestQuestion]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteTestQuestion]
 @TestId int
As
Begin
  Delete TestQuestions where TestID = @TestId
End
GO
