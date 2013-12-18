IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestRank]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestRank]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestRank]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestRank]
@TestId INT
AS
BEGIN
	SELECT   ISNULL(AVG(PercentileRank),0) AS Rank
	FROM         dbo.Norming
	WHERE     TestID = @TestId
END
GO
