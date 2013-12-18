IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseQuestionLippincotToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseQuestionLippincotToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseQuestionLippincotToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspReleaseQuestionLippincotToProduction]
 @LippincottId as int,
 @QID as int
As
Begin
  Insert Into QuestionLippincott(QID, LippincottID)
  Values(@QID,@LippincottId)
End
GO
