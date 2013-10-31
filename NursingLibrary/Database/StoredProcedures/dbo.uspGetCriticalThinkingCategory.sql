IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCriticalThinkingCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCriticalThinkingCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCriticalThinkingCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCriticalThinkingCategory]
AS

SELECT CriticalThinkingID AS [Id], CriticalThinking AS [Description], ProgramofStudyId 
FROM CriticalThinking
GO
