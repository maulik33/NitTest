IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNursingProcessCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNursingProcessCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNursingProcessCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNursingProcessCategory]
AS

SELECT NursingProcessID AS [Id], NursingProcess AS [Description], ProgramofStudyId 
FROM NursingProcess
GO
