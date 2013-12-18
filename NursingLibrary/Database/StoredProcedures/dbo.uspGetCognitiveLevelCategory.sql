IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCognitiveLevelCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCognitiveLevelCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCognitiveLevelCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCognitiveLevelCategory]
AS
SELECT CognitiveLevelID AS [Id], CognitiveLevel AS [Description], ProgramofStudyId 
FROM CognitiveLevel
GO
