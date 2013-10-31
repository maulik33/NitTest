IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStates]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStates]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStates]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStates]
(
 @CountryId INT,
 @StateId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			StateID,
			CountryID,
			StateName,
			StateStatus
		FROM dbo.CountryState
		WHERE (@CountryId = 0
			  OR(CountryID = @CountryId))
			  AND (@StateId = 0
			  OR (StateID = @StateId))
			
	END
GO
