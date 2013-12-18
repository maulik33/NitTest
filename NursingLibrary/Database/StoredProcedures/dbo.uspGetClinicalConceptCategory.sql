IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClinicalConceptCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetClinicalConceptCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetClinicalConceptCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetClinicalConceptCategory]
AS
SELECT ClinicalConceptID AS [Id], ClinicalConcept AS [Description], ProgramofStudyId 
FROM ClinicalConcept
GO
