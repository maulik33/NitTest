IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveGroup]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveGroup]
@GroupName nvarchar(100),
@CohortID int,
@GroupUserID int,
@GroupID int,
@newGroupID int OUTPUT

AS

SET NOCOUNT ON

BEGIN
	IF @GroupID = 0
		BEGIN
			INSERT INTO NurGroup
			(
			GroupName,
			CohortID,
			GroupInsertDate,
			GroupInsertUser
			)
			VALUES
			(
			@GroupName,
			@CohortID,
			getdate(),
			@GroupUserID
			)
			
			SELECT @newGroupID = SCOPE_IDENTITY()
		END
ELSE
		BEGIN
			UPDATE NurGroup
			SET GroupName = @GroupName,
            GroupUpdateDate = getdate(),
			GroupUpdateUser = @GroupUserID,
			CohortID = @CohortID
            WHERE GroupID = @GroupID
			
			SELECT @newGroupID = @GroupID
		END
END
GO
