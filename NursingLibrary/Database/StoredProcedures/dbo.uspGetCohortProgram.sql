IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortProgram]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspGetCohortProgram]
(
	@CohortProgramId int,
	@ProgramId int,
	@CohortId int	
)
AS
BEGIN
	SELECT
		CohortProgramID ,
		CohortID ,
		ProgramID ,
		Active
	FROM dbo.NurCohortPrograms
	WHERE Active = 1
	AND ( @CohortId = 0 OR CohortID = @CohortId)
END
GO
