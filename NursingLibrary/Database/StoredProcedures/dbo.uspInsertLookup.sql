IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertLookup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertLookup]
GO

PRINT 'Creating PROCEDURE uspInsertLookup'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspInsertLookup]
	@OriginalId int,
	@TypeId tinyint,
	@DisplayText varchar(300),
	@SortOrder int,
	@Id int OUTPUT
AS
	INSERT INTO dbo.[Lookup] (TypeId, OriginalId, DisplayText, SortOrder)
	VALUES (@TypeId, @OriginalId, @DisplayText, @SortOrder)
	
	SET @Id = SCOPE_IDENTITY()

GO

PRINT 'Finished creating PROCEDURE uspInsertLookup'
GO

