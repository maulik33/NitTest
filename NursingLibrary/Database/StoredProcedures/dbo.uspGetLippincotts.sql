IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincotts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincotts]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincotts]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincotts]
@LippincottId int,
@ReleaseStatus char(1)
AS
BEGIN
 SELECT LippincottID, RemediationID, LippincottTitle, LippincottExplanation, LippincottTitle2, LippincottExplanation2
 from Lippincot
 where (LippincottID = @LippincottId or @LippincottId=0) and (ReleaseStatus = @ReleaseStatus or @ReleaseStatus='')
END
GO
