IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentTestCharacteristics]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentTestCharacteristics]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentTestCharacteristics]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStudentTestCharacteristics]
	-- Add the parameters for the stored procedure here
	@testId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	   SELECT Category.TableName,Category.CategoryID FROM  TestCategory INNER JOIN    
	   Category ON TestCategory.CategoryID = Category.CategoryID    
	   WHERE Student=1 AND TestID=@TestID ORDER BY Category.OrderNumber   

	SET NOCOUNT OFF
END
GO
