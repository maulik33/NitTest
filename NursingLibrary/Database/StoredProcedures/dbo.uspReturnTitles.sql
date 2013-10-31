IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTitles]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnTitles]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnTitles]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnTitles]
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT distinct TopicTitle,RemediationId
	FROM Remediation
	
	RETURN
		
	SET NOCOUNT OFF
END
GO
