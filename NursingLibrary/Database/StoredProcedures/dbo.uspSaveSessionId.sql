IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveSessionId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveSessionId]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveSessionId]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveSessionId]
	@userId int,
	@sessionid varchar(50)
AS
BEGIN
-- =============================================
-- Author:		Cale Teeter
-- Create date: 05/13/2009
-- Description:	Procedure used to save the current asp.net session id for the user.
-- =============================================

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE NurStudentInfo
	SET SessionId = @sessionId
	WHERE UserId = @userId
END
GO
