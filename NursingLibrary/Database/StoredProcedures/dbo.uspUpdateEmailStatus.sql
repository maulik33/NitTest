IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateEmailStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateEmailStatus]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateEmailStatus]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/30/2010
-- Description:	Procedure used to update email sending status
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateEmailStatus]
	-- Add the parameters for the stored procedure here
	@emailMissionId int, @emailStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE EmailMission SET State = @emailStatus WHERE EmailMissionID = @emailMissionId
END
GO
