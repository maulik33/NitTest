IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNumberOfCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNumberOfCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNumberOfCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNumberOfCategory]
AS
BEGIN
	
	SELECT count(*) as Number FROM dbo.ClientNeeds
	
END
GO
