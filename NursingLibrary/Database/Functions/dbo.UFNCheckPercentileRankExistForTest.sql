IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNCheckPercentileRankExistForTest]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNCheckPercentileRankExistForTest]
GO

/****** Object:  UserDefinedFunction [dbo].[UFNCheckPercentileRankExistForTest]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[UFNCheckPercentileRankExistForTest]
(@TestID int)
RETURNS INT
AS
BEGIN

DECLARE @Result int

	SELECT @Result = ISNULL(PercentileRank,0) FROM Norming WHERE TestID=@TestID

	RETURN @Result

END
GO
