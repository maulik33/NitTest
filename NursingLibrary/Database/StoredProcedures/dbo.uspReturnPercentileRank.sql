IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnPercentileRank]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReturnPercentileRank]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnPercentileRank]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspReturnPercentileRank]
@UserTestId INT,
@NumberCorrect FLOAT
AS
BEGIN
	
	DECLARE @TestId INT
	
	SELECT @TestId = TestId
	FROM UserTests
	WHERE UserTestID=@UserTestID
	
	SELECT PercentileRank
	FROM Norming
	WHERE TestID= @TestId
	AND NumberCorrect= @NumberCorrect
	
END
GO
