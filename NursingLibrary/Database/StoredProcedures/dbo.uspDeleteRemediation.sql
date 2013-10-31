IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteRemediation]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteRemediation]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteRemediation]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteRemediation]
@RemediationId as int
AS
BEGIN
 DELETE FROM Remediation WHERE RemediationID=@RemediationId
END
GO
