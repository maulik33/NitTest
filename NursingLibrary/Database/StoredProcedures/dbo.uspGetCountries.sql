IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCountries]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCountries]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCountries]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetCountries]    Script Date: 07/27/2011 15:26:02 ******/

CREATE PROCEDURE [dbo].[uspGetCountries]
(
 @CountryId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			CountryID,
			CountryName,
			Status
		FROM dbo.Country
		WHERE (@CountryId = 0
			  OR(CountryID = @CountryId))
	END
GO
