IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteLookupMapping]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteLookupMapping]
GO

PRINT 'Creating PROCEDURE uspDeleteLookupMapping'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteLookupMapping]
	@Id int
AS
	DELETE dbo.[LookupMappings]
	WHERE MappingId = @Id

GO

PRINT 'Finished creating PROCEDURE uspDeleteLookupMapping'
GO



