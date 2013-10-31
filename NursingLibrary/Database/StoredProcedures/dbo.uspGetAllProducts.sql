IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllProducts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAllProducts]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAllProducts]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAllProducts]
@ProductId int
AS
BEGIN
   -- SET NOCOUNT ON added to prevent extra result sets from
   SET NOCOUNT ON;

   SELECT ProductID,ProductName, ProductType,
		  TakeTestMultipleTimes AS MultiUseTest, TestType, Scramble,
          Remediation, Bundle
   FROM Products
   Where ( @ProductID = 0 OR ProductID = @ProductId)
END
GO
