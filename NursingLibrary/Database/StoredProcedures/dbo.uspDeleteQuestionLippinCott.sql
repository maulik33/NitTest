IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteQuestionLippinCott]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteQuestionLippinCott]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteQuestionLippinCott]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteQuestionLippinCott]
@LippincottId int,
@QID int
AS
BEGIN
 Delete from QuestionLippincott where LippincottID = @LippincottId  and  QID=@QID
END
GO
