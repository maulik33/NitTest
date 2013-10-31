IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveNorming]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveNorming]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveNorming]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveNorming]
@TestID as int,
@NumberCorrect float,
@Correct float,
@PercentileRank float,
@Id as int,
@Probability as int
AS
BEGIN
	If(@Id = 0)
	 Begin
		INSERT INTO Norming(TestID,NumberCorrect,Correct,PercentileRank,Probability)
		 values(@TestID,@NumberCorrect,@Correct,@PercentileRank,@Probability)
	 End
	Else
	 Begin
		 Update Norming set NumberCorrect=@NumberCorrect,Correct=@Correct,PercentileRank=@PercentileRank,Probability=@Probability
		 where TestID=@TestID and id=@Id
	 End
END
GO
