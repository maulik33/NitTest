IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSystemsCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSystemsCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetSystemsCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetSystemsCategory]
AS
SELECT SystemID AS [Id], [System] AS [Description], ProgramofStudyId FROM Systems
GO
