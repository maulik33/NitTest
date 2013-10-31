IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchCohortProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchCohortProgram]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspSearchCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspSearchCohortProgram]
(
	@CohortId int,
	@SearchText varchar(200)
)
AS
BEGIN
	SELECT
		NP.ProgramID,
		NP.ProgramName,
		NP.Description,
		NP.DeletedDate,
		NCP.CohortID,
		NCP.Active
	FROM    dbo.NurCohortPrograms NCP
	INNER JOIN dbo.NurProgram NP
	ON NCP.ProgramID = NP.ProgramID
	WHERE  NP.DeletedDate IS NULL
	AND    NCP.Active=1
	AND NCP.CohortID= @CohortId
	AND (@SearchText = '' OR NP.ProgramName LIKE '%' + @SearchText + '%')
END
GO
