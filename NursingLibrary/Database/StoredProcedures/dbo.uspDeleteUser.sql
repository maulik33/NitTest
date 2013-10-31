IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteUser]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteUser]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspSaveUser]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspDeleteUser]
	@userId int
AS

-- Prevent row count message
SET NOCOUNT ON;

BEGIN TRANSACTION
	-- Update student info table
	UPDATE NurStudentInfo
		SET UserDeleteData = GetDate()
	WHERE UserID = @userId;

IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR('Error in updating UserDeleteData in NurStudentInfo', 16, 1)
		RETURN
	END

	-- Update assignment table
	UPDATE NusStudentAssign
		SET DeletedDate = GetDate()
	WHERE StudentID = @userId;

IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR('Error in updating DeletedDate in NurStudentInfo', 16, 1)
		RETURN
	END

COMMIT TRANSACTION
GO
