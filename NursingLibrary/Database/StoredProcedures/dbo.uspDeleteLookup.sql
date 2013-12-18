IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteLookup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteLookup]
GO

PRINT 'Creating PROCEDURE uspDeleteLookup'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteLookup]
	@Id int
AS
	DELETE dbo.[Lookup]
	WHERE Id = @Id
	AND TypeId IN (15)
	
	IF @@ROWCOUNT = 0
		RAISERROR('Unable to delete Lookup row. This could be due to an attempt to delete a Type Id that otherwise should not be deleted or the Lookup might have already been deleted.', 12, 2);

GO

PRINT 'Finished creating PROCEDURE uspDeleteLookup'
GO

