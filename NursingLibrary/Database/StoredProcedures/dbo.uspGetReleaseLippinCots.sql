IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetReleaseLippinCots]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetReleaseLippinCots]
GO

/****** Object:  StoredProcedure [dbo].[uspGetReleaseLippinCots]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspGetReleaseLippinCots]
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle, Lippincot.ReleaseStatus
 FROM dbo.Lippincot LEFT OUTER JOIN dbo.Remediation
 ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 WHERE Lippincot.ReleaseStatus != 'R'
End
GO
