IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNormForTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetNormForTest]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNormForTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetNormForTest]
@TestId INT
AS
BEGIN
	
	SELECT top 1 Norm.Norm
	FROM Norm
	WHERE (TestID = @TestId)
	AND (ChartType = 'overall')
	
END
GO
