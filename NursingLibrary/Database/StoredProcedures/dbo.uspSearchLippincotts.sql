IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchLippincotts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchLippincotts]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchLippincotts]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSearchLippincotts]
@LippinCottTitle varchar(800)
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle
 FROM dbo.Lippincot  LEFT OUTER JOIN dbo.Remediation ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 where (LippincottTitle like '%'+@LippinCottTitle+'%')
END
GO
