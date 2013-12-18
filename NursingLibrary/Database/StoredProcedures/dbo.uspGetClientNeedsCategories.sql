IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedsCategories]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetClientNeedsCategories]
GO

/****** Object:  StoredProcedure [dbo].[uspGetClientNeedsCategories]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetClientNeedsCategories]
(
	@ClientNeedId int
)
	
AS
	BEGIN
	SET NOCOUNT ON
		SELECT 	
			ClientNeedID,
			ClientNeedCategoryID,
			ClientNeedCategory
		FROM  ClientNeedCategory
		WHERE (@ClientNeedId = 0 OR ClientNeedID = @ClientNeedId)

	SET NOCOUNT OFF
	END
GO
