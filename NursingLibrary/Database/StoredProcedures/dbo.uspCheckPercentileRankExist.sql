IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckPercentileRankExist]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCheckPercentileRankExist]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckPercentileRankExist]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCheckPercentileRankExist]
 @UserTestID int
AS
BEGIN
 DECLARE @TestId INT

 SELECT @TestId = TestId
 FROM UserTests
 WHERE UserTestID=@UserTestID

 SELECT PercentileRank
 FROM Norming
 WHERE TestID = @testId
END
GO
