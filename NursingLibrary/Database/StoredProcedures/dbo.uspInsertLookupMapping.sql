IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertLookupMapping]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertLookupMapping]
GO

PRINT 'Creating PROCEDURE uspInsertLookupMapping'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspInsertLookupMapping]
	@TypeId tinyint,
	@LookupId int,
	@MappedTo int,
	@Id int OUTPUT
AS
	INSERT INTO dbo.[LookupMappings] (TypeId, LookupId, MappedTo)
	VALUES (@TypeId, @LookupId, @MappedTo)
	
	SET @Id = SCOPE_IDENTITY()

GO

PRINT 'Finished creating PROCEDURE uspInsertLookupMapping'
GO
