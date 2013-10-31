IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQueuedMissions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQueuedMissions]
GO

/****** Object:  StoredProcedure [dbo].[uspGetQueuedMissions]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get queued email missions.
-- =============================================
CREATE PROCEDURE [dbo].[uspGetQueuedMissions]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT M.EmailMissionID, M.EmailID, M.ToAdminOrStudent, A.Email AS CreatorEmail
	FROM EmailMission AS M
	INNER JOIN NurAdmin AS A
	ON A.UserId = M.AdminId
	WHERE State = 1 AND GetDate() > SendTime
END
GO
