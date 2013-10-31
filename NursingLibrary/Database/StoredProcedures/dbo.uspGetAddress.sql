IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAddress]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAddress]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAddress]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAddress]
(
 @AddressId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			A.AddressID,
			A.Address1,
			A.Address2,
			A.Address3,
			A.CountryID,
			A.StateName,
			A.Zip,
			C.CountryName,
			A.[Status]
		FROM dbo.[Address] A
		INNER JOIN Country C
		ON A.CountryID = C.CountryID		
		WHERE AddressID = @AddressId
			
	END
GO
