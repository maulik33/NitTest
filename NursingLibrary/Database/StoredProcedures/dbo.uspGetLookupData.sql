IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLookupData]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLookupData]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLookupData]    Script Date: 10/12/2011 15:17:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspGetLookupData]
	@TypeIds varchar(200)
AS
	SELECT Id, DisplayText
	FROM dbo.[Lookup]
	WHERE TypeId IN (select value from  dbo.funcListToTableInt(@TypeIds, '|'))
GO
