IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLevelOfDifficultyCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLevelOfDifficultyCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLevelOfDifficultyCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLevelOfDifficultyCategory]
AS
SELECT LevelOfDifficultyID AS [Id], LevelOfDifficulty AS [Description], ProgramofStudyId 
FROM LevelOfDifficulty
GO
