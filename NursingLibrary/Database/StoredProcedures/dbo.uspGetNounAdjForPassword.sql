IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNounAdjForPassword]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNounAdjForPassword]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNounAdjForPassword]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNounAdjForPassword]
AS
BEGIN
	SET NOCOUNT ON;
		 SELECT TOP 1 Noun,Adj
		 FROM KapA_PassList
		 ORDER BY NEWID()
END
GO
