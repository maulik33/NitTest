IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationExplainationByID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediationExplainationByID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetRemediationExplainationByID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[uspGetRemediationExplainationByID]
@RemediationId INT
AS

BEGIN
	SET NOCOUNT ON;
		SELECT TOP 1 ISNULL(explanation,'')
		FROM Remediation
		WHERE RemediationID = @RemediationId
	SET NOCOUNT OFF
END
GO
