IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignStudents]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignStudents]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignStudents]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignStudents]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignStudents]
	@userId VARCHAR(max),
	@cohortId int,
	@groupId int,
	@institutionId int
AS

-- Prevent row count message
SET NOCOUNT ON;

BEGIN TRANSACTION
	-- Update student info table
	UPDATE NurStudentInfo
		SET InstitutionID = @institutionId
	WHERE UserID IN
		  (SELECT value
		  FROM dbo.funcListToTableInt(@userId,'|'))
	-- Update assignment table
	UPDATE NusStudentAssign
		SET CohortID = @cohortId,
		GroupID = @groupId,
		Access = 1
	WHERE StudentID IN
		  (SELECT value
		  FROM dbo.funcListToTableInt(@userId,'|'))

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK
		END
COMMIT TRANSACTION
GO
