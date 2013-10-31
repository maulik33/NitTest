IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAppSettings]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAppSettings]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAppSettings]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAppSettings]
AS

SELECT [SettingsId], [Value] FROM [dbo].[AppSettings]
GO
