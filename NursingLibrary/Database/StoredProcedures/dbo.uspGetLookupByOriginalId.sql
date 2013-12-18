IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLookupByOriginalId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLookupByOriginalId]
GO

PRINT 'Creating PROCEDURE uspGetLookupByOriginalId'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetLookupByOriginalId]
	@OriginalId int,
	@TypeId int
AS
	IF @TypeId = 15
	BEGIN
		RAISERROR('This Stored procedure is not supported for this Type Id.', 12, 2)
	END

	SELECT TypeId,
		Id,
		DisplayText,
		SortOrder
	FROM dbo.[Lookup]
	WHERE OriginalId = @OriginalId
	AND TypeId = @TypeId

GO

PRINT 'Finished creating PROCEDURE uspGetLookupByOriginalId'
GO

