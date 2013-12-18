IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLookup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLookup]
GO

PRINT 'Creating PROCEDURE uspGetLookup'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetLookup]
	@Id int
AS
	SELECT TypeId,
		OriginalId,
		DisplayText,
		SortOrder
	FROM dbo.[Lookup]
	WHERE Id = @Id

GO

PRINT 'Finished creating PROCEDURE uspGetLookup'
GO

