IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckPercentileRankExists]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCheckPercentileRankExists]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckPercentileRankExists]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCheckPercentileRankExists]
	-- Add the parameters for the stored procedure here
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT PercentileRank
	FROM Norming
	WHERE TestID = @testId
END
GO
