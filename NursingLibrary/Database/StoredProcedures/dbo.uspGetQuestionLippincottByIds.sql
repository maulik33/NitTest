IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionLippincottByIds]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionLippincottByIds]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQuestionLippincottByIds]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetQuestionLippincottByIds]
@QID int,
@LippincottId int,
@LippincottIds Varchar(4000)
AS
BEGIN
 select QID,LippincottID from QuestionLippincott
 where (QID=@QID or @QID =0)  and (LippincottID = @LippincottId or @LippincottId=0)
 and   ( (LippincottID in (select * from dbo.funcListToTableInt(@LippincottIds,'|'))) or @LippincottIds ='')
END
GO
