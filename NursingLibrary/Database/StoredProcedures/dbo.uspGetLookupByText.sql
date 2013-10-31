IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLookupByText]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLookupByText]
GO

PRINT 'Creating PROCEDURE uspGetLookupByText'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetLookupByText]
	@MatchText varchar(300),
	@TypeId tinyint
AS
	SELECT Id,
		OriginalId
	FROM dbo.[Lookup]
	WHERE DisplayText = @MatchText

GO

PRINT 'Finished creating PROCEDURE uspGetLookupByText'
GO

