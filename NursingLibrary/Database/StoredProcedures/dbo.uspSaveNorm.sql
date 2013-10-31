IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveNorm]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveNorm]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveNorm]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveNorm]
@NormId as Int,
@ChartType as nVarchar(50),
@ChartId as int,
@Norm as real,
@TestId as int
AS
BEGIN
Declare @Id as int
set @Id = @NormId
	If(@NormId = 0)
	 Begin
	   Insert into Norm(ChartType,ChartID,Norm,TestID)
	   values(@ChartType,@ChartId,@Norm,@TestId)
     End
    Else
     Begin
       Update Norm
       Set ChartType = @ChartType,ChartID=@ChartId,Norm=@Norm,TestID=@TestId
       where ID = @NormId
     End
END
GO
