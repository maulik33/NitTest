IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAddress]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAddress]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAddress]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAddress]
	@AddressId INT,
	@Address1 VARCHAR(50),
	@Address2 VARCHAR(50),
	@Address3 VARCHAR(50),
	@CountryId INT,
	@StateName VARCHAR(100),
	@Zip VARCHAR(100),
	@Status SMALLINT,
	@NewAddressId INT OUTPUT
AS

SET NOCOUNT ON

BEGIN
	IF @AddressId = 0
		BEGIN
			INSERT INTO [Address]
			(
			Address1,
			Address2,
			Address3,
			CountryID,
			StateName,
			Zip,
			[Status]
			)
			VALUES
			(
			@Address1,
			@Address2,
			@Address3,
			@CountryID,
			@StateName,
			@Zip,
			@Status
			)
			
			SELECT @NewAddressId = SCOPE_IDENTITY()
		END
ELSE
		BEGIN
			UPDATE [Address]
			SET Address1 = @Address1,
			Address2	 = @Address2,
			Address3	 = @Address3,
			CountryID	 = @CountryID,
			StateName    = @StateName,
			Zip			 = @Zip,
			[Status]	 = @Status
            WHERE AddressID = @AddressId			
			SELECT @NewAddressId = @AddressId
		END
END
GO
