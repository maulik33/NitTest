IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseNormToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseNormToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseNormToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspReleaseNormToProduction]
@Id as INT,
@ChartType as nVarchar(50),
@ChartId as int,
@Norm as real,
@TestId as INT
AS
Begin
 SET IDENTITY_INSERT Norm ON
   Insert Into Norm(ID, ChartType, ChartID, Norm, TestID)
   Values(@Id,@ChartType,@ChartId,@Norm,@TestId)
 SET IDENTITY_INSERT Norm OFF
End
GO
