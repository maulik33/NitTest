IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertQuestionLippinCott]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertQuestionLippinCott]
GO

/****** Object:  StoredProcedure [dbo].[uspInsertQuestionLippinCott]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertQuestionLippinCott]
@QID as int,
@LippincottID as int
AS
BEGIN
if Not Exists(select QID from  QuestionLippincott where QID= @QID and LippincottID=@LippincottID)
 Begin
 Insert into dbo.QuestionLippincott(QID,LippincottID) values(@QID,@LippincottID)
 End
End
GO
