IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroupsList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetGroupsList]
GO

/****** Object:  StoredProcedure [dbo].[uspGetGroupsList]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetGroupsList]
@InstitutionId as int,
@CohortId as int
AS
BEGIN
	SELECT  dbo.NurGroup.GroupName,
	C.CohortName,
	dbo.NurGroup.GroupID,
	C.CohortStatus,
	C.CohortID,
	I.InstitutionName,
	I.InstitutionID,
	CP.ProgramID,
	P.ProgramName
	FROM dbo.NurGroup
	INNER JOIN  dbo.NurCohort C
	ON dbo.NurGroup.CohortID = C.CohortID
	INNER JOIN dbo.NurInstitution I
	ON C.InstitutionID = I.InstitutionID
	LEFT OUTER JOIN  dbo.NurCohortPrograms CP
	ON C.CohortID = CP.CohortID
	AND dbo.NurGroup.CohortID = CP.CohortID
	INNER JOIN  dbo.NurProgram P
	ON CP.ProgramID = P.ProgramID
	WHERE     (C.CohortStatus = 1)
	AND (CP.Active = 1)
	AND C.InstitutionID= @InstitutionId
	AND dbo.NurGroup.CohortID = @CohortId

END
GO
