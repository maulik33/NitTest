IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNorms]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNorms]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNorms]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNorms]
@TestId as int,
@ChartType as nVarchar(50),
@TestIds as Varchar(4000)
AS
BEGIN
 select * from Norm
 where (TestID = @TestId or @TestId = 0) and (ChartType = @ChartType or @ChartType='')
 and ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|'))) or @TestIds='')
END
GO
