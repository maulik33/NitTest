IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdmin]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAdmin]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdmin]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspSaveAdmin]    Script Date: 05/17/2011  ******/

Create PROCEDURE [dbo].[uspSaveAdmin]
  @UserId INT,
  @UserName VARCHAR(50),
  @UserPass VARCHAR(50),
  @Email VARCHAR(50),
  @FirstName VARCHAR(50),
  @LastName VARCHAR(50),
  @SecurityLevel INT,
  @AdminModifyUser  INT,
  @InstitutionId INT,
  @UploadAccess Bit,
  @NewAdminId  INT OUT
AS
 -- Prevent row count message
SET NOCOUNT ON;
DECLARE @Id INT
SELECT @Id = 0
BEGIN
 IF @UserId = 0
  BEGIN
   SELECT @Id = UserID FROM dbo.NurAdmin
   WHERE  UserName = @UserName
   AND @UserPass = @UserPass
   IF @Id = 0
    BEGIN
     INSERT INTO NurAdmin
      (
       UserName,
       UserPass,
       Email,
       FirstName,
       LastName,
       SecurityLevel,
       AdminCreateUser,
       AdminCreateData,
       UploadAccess
      )
     VALUES
      (
       @UserName,
       @UserPass,
       @Email,
       @FirstName,
       @LastName,
       @SecurityLevel,
       @AdminModifyUser,
       GETDATE(),
       @UploadAccess
      )
     SELECT @NewAdminId = SCOPE_IDENTITY()
     IF @InstitutionId > 0
     BEGIN
      INSERT INTO NurAdminInstitution
      (
       AdminID,
       InstitutionID,
       Active
      )
      VALUES
      (
       @NewAdminId ,
       @InstitutionId,
       1
      )
     END
     RETURN  @NewAdminId
    END

   ELSE
    BEGIN
     SELECT @NewAdminId = -1 -- Id already exist
     RETURN @NewAdminId
    END
  END
 ELSE
  SELECT @Id = UserID FROM dbo.NurAdmin
  WHERE  UserName = @UserName
  AND @UserPass = @UserPass
  AND UserName != @UserName
  BEGIN
   IF @Id = 0
    BEGIN
     UPDATE NurAdmin
     SET UserName = @UserName,
     UserPass = @UserPass,
     Email = @Email,
     FirstName = @FirstName,
     LastName = @LastName,
     SecurityLevel = @SecurityLevel,
     AdminUpdateData = GETDATE(),
     AdminUpdateUser = @AdminModifyUser,
     UploadAccess = @UploadAccess
     WHERE UserID = @UserId

     SELECT @NewAdminId = @UserId
     RETURN @NewAdminId
    END
   ELSE
    BEGIN
    SELECT @NewAdminId = -1
    RETURN @NewAdminId
    END
  END
END
GO
