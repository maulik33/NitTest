IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignCohortProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignCohortProgram]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspAssignCohortProgram]
(
	@CohortId int ,
	@ProgramId int,
	@Active int
)
AS
BEGIN

BEGIN TRAN
	UPDATE  NurCohortPrograms
	SET Active = 0
    WHERE CohortID = @CohortId
	IF EXISTS
	(
	SELECT ProgramID
	FROM dbo.NurCohortPrograms
	WHERE ProgramID = @ProgramId
	AND CohortID =  @CohortId
	)
		BEGIN
			UPDATE  NurCohortPrograms
			SET Active = @Active
			WHERE CohortID = @CohortId
			AND ProgramID = @ProgramId
		END
	ELSE
		BEGIN
			INSERT INTO NurCohortPrograms
			(
				CohortID,
				ProgramID,
				Active
			)
			VALUES
			(
				@CohortID ,
				@ProgramId,
				@Active
			)
		END
	
	IF @@ERROR != 0
		BEGIN
			ROLLBACK TRAN
		END
	ELSE
		BEGIN
			COMMIT TRAN
		END
END
GO
