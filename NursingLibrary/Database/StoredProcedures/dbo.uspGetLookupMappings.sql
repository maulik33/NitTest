IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLookupMappings]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLookupMappings]
GO

PRINT 'Creating PROCEDURE uspGetLookupMappings'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetLookupMappings]
	@Ids varchar(5000),
	@TypeId tinyint,
	@IsReverseMapping bit
AS

	IF @IsReverseMapping = 1
		SELECT MappingId,
			LookupId AS MappedId,
			MappedTo AS Id
		FROM dbo.LookupMappings
		WHERE TypeId = @TypeId
		AND MappedTo IN (SELECT [value] from dbo.funcListToTableInt(@Ids,'|'))
	ELSE
		SELECT MappingId,
			MappedTo AS MappedId,
			LookupId AS Id
		FROM dbo.LookupMappings
		WHERE TypeId = @TypeId
		AND LookupId IN (SELECT [value] from dbo.funcListToTableInt(@Ids,'|'))

GO

PRINT 'Finished creating PROCEDURE uspGetLookupMappings'
GO
