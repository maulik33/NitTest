IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDemographicCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetDemographicCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetDemographicCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetDemographicCategory]
AS
SELECT DemographicID AS [Id], Demographic AS [Description], ProgramofStudyId 
FROM Demographic
GO
