IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottRemediationById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincottRemediationById]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincottRemediationById]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincottRemediationById]
@LippinCottID INT
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle ,
 LippincottExplanation,LippincottTitle2,LippincottExplanation2 FROM dbo.Lippincot  LEFT OUTER JOIN dbo.Remediation ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 where (Lippincot.LippincottID = @LippinCottID or @LippinCottID = -1 )
END
GO
