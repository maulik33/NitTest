IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAppSettings]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAppSettings]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAppSettings]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAppSettings]
	@settingsId int,
	@value varchar(200)
AS
	UPDATE [dbo].[AppSettings]
	SET [Value] = @value
	WHERE [SettingsId] = @settingsId
GO
