
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveCohort]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspSaveCohort]    Script Date: 06/02/2011  ******/

CREATE PROCEDURE [dbo].[uspSaveCohort]
	 @CohortId int,
	 @CohortName varchar(160),
	 @CohortDescription varchar(1000),
	 @CohortStatus int,
	 @CohortStartDate datetime,
	 @CohortEndDate datetime,
	 @CohortCreateUser int,
	 @InstitutionId  int,
     @NewCohortId int OUT
AS

	-- Prevent row count message
SET NOCOUNT ON;
DECLARE @ProgramId INT
SELECT @ProgramId = 0
SELECT @ProgramId = programID FROM NurInstitution WHERE InstitutionID = @InstitutionID
BEGIN
	IF @CohortId = 0
		BEGIN
			INSERT INTO NurCohort
						(
						CohortName,
						CohortDescription,
						CohortStatus,
						CohortStartDate,
						CohortEndDate,
						CohortCreateDate,
						CohortCreateUser,
						InstitutionID)
			VALUES
						(
						 @CohortName,
						 @CohortDescription,
						 @CohortStatus,
						 @CohortStartDate,
						 @CohortEndDate,
						 GETDATE(),
						 @CohortCreateUser,
						 @InstitutionId
						)
			SELECT @NewCohortId = SCOPE_IDENTITY()
			END
	ELSE
		BEGIN
			UPDATE NurCohort
			SET
				CohortName	= @CohortName,
				CohortDescription= @CohortDescription,
				CohortStatus	= @CohortStatus,
				InstitutionID	= @InstitutionID,
				CohortEndDate	= @CohortEndDate,
				CohortStartDate	= @CohortStartDate,
				CohortUpdateDate	= GETDATE(),
				CohortUpdateUser	= @CohortCreateUser
			WHERE CohortID	= @CohortId
			
			SELECT @NewCohortId = @CohortId
		END
	IF @ProgramId != 0
		BEGIN
			DECLARE @maxCohortId INT
			SELECT @maxCohortId = 0
			SELECT @maxCohortId = MAX(CohortID) FROM NurCohort -- check logic with Gokul

			UPDATE  NurCohortPrograms
			SET Active=0
			WHERE CohortID  = @maxCohortId
			
			DECLARE @CohortProgram INT
			SELECT @CohortProgram = 0;
			
			SELECT @CohortProgram = COUNT(*)
			FROM dbo.NurCohortPrograms
			WHERE ProgramID = @ProgramId
			AND CohortID = @maxCohortId
			
			IF @CohortProgram > 0
				BEGIN
					UPDATE  NurCohortPrograms
					SET Active=1
					WHERE CohortID = @maxCohortId
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
								 @maxCohortId,
								 @ProgramID ,
								 1
								)	
				END

		END
END
GO
