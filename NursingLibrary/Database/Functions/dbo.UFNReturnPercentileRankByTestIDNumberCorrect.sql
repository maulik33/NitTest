IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]
GO

/****** Object:  UserDefinedFunction [dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]
(@TestID int,
@NumberCorrect float  )
Returns float
AS
BEGIN
	DECLARE @Result float

	select @Result = ISNULL(PercentileRank,0) from dbo.Norming
	where TestID=@TestID AND NumberCorrect=@NumberCorrect

	RETURN @Result
END
GO
