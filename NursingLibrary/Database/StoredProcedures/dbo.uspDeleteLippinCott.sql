IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteLippinCott]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteLippinCott]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteLippinCott]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteLippinCott]
@LippincottId int
AS
BEGIN
	Delete from QuestionLippincott where LippincottID = @LippincottId
    Delete from dbo.Lippincot where LippincottID = @LippincottId
END
GO
