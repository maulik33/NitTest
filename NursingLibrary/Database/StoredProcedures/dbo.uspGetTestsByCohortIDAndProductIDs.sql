IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByCohortIDAndProductIDs]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByCohortIDAndProductIDs]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsByCohortIDAndProductIDs]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestsByCohortIDAndProductIDs]
	@ProductIds nVarchar(2000),
	@CohortIds nVarchar(2000)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT DISTINCT Tests.TestName,
	       UserTests.TestID
	FROM   UserTests
	       JOIN Tests
	            ON  UserTests.TestID = Tests.TestID
	WHERE  (
	           (
	               @ProductIds <> ''
	               AND Tests.ProductID IN (SELECT *
	                                       FROM   dbo.funcListToTableInt(@ProductIds, '|'))
	           )
	           OR (@ProductIds = '')
	       )
	       AND (
	               (
	                   @CohortIds <> ''
	                   AND UserTests.CohortID IN (SELECT *
	                                              FROM   dbo.funcListToTableInt(@CohortIds, '|'))
	               )
	               OR (@CohortIds = '')
	           )
	ORDER BY
	       Tests.TestName ASC
	SET NOCOUNT OFF;

END
GO
