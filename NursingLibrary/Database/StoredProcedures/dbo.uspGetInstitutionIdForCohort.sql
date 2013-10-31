IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionIdForCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionIdForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIdForCohort]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIdForCohort]    Script Date: 06/02/2011  ******/

CREATE PROC [dbo].[uspGetInstitutionIdForCohort]
(
 @cohortId int
)
AS
SET NOCOUNT ON;
	BEGIN
SELECT InstitutionID FROM NurCohort WHERE CohortID = @cohortId

END
GO
