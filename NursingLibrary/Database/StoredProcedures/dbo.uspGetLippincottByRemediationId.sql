IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottByRemediationId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincottByRemediationId]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincottByRemediationId]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincottByRemediationId]
@RemediationId INT
AS
BEGIN
	SELECT LippincottID, RemediationID, LippincottTitle,LippincottExplanation,LippincottTitle2,LippincottExplanation2,ReleaseStatus
	FROM dbo.Lippincot
	where (Lippincot.RemediationID = @RemediationId)
END
GO
