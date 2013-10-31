IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCustomEmailDefinition]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCustomEmailDefinition]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCustomEmailDefinition]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/30/2010
-- Description:	Procedure used to get custom email definition
-- =============================================
CREATE PROCEDURE [dbo].[uspGetCustomEmailDefinition]
	-- Add the parameters for the stored procedure here
	@emailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Title, Body FROM Email WHERE EmailId = @emailId
END
GO
