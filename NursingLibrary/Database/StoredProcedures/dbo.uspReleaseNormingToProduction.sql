IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseNormingToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseNormingToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseNormingToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspReleaseNormingToProduction]
@Id as INT,
@NumberCorrect as float,
@Correct as float,
@PercentileRank as float,
@Probability as float,
@TestId as INT
AS
Begin
 SET IDENTITY_INSERT Norming ON
   Insert Into Norming(id, NumberCorrect, Correct, PercentileRank, Probability, TestID)
   Values(@Id,@NumberCorrect,@Correct,@PercentileRank,@Probability,@TestId)
 SET IDENTITY_INSERT Norming OFF
End
GO
