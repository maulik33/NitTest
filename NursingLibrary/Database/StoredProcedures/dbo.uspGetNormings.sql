IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNormings]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNormings]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNormings]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNormings]
@TestId as int,
@TestIds as Varchar(4000)
AS
BEGIN
 Select NumberCorrect,Correct,PercentileRank,Probability,TestID,id
 From Norming
 Where (TestID=@TestId or  @TestId=0)
 and ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')
 Order By NumberCorrect
END
GO
