IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveInstitutionContact]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveInstitutionContact]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveInstitutionContact]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspSaveInstitutionContact]
(
 @ContactId int OUTPUT,
 @InstitutionId int,
 @ContactType smallint,
 @Name varchar(100),
 @PhoneNumber varchar(50),
 @Email varchar(100),
 @Status int,
 @CreatedBy int,
 @CreatedDate smalldateTime,
 @DeletedBy int,
 @DeletedDate smalldateTime
)
AS
 IF ISNULL(@ContactId, 0) = 0
 BEGIN
  INSERT INTO
   dbo.InstitutionContacts
   (
     InstitutionId,
     ContactType,
     Name,
     PhoneNumber,
     Email,
     Status,
     CreatedBy,
     CreatedDate
    )
  VALUES
   (
   @InstitutionId,
   @ContactType,
   @Name,
   @PhoneNumber,
   @Email,
   @Status,
   @CreatedBy,
   @CreatedDate
   )

  SET @ContactId = CONVERT(int, SCOPE_IDENTITY())
 END
 ELSE
 Begin
  UPDATE dbo.InstitutionContacts
  SET InstitutionId = @InstitutionId,
   ContactType = @ContactType,
   Name = @Name,
   PhoneNumber = @PhoneNumber,
   Email = @Email,
   Status = @Status,
   DeletedBy = @DeletedBy,
   DeletedDate = @DeletedDate
  WHERE ContactID = @ContactId
 END
GO
