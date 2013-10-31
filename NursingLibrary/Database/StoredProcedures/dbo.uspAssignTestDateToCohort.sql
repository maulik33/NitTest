IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignTestDateToCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToCohort]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToCohort]    Script Date: 05/17/2011  ******/
CREATE PROCEDURE [dbo].[uspAssignTestDateToCohort]
(
@CohortId INT,
@ProductId INT,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS (
SELECT 1 FROM NurProductDatesCohort
WHERE CohortID = @CohortID  AND ProductID = @ProductId AND  Type = @Type
)
	BEGIN
		UPDATE  NurProductDatesCohort
		SET StartDate= @StartDate,
			EndDate= @EndDate,
			UpdateDate = GETDATE()
		WHERE CohortID=@CohortId
		AND ProductID=@ProductId
		AND Type=@Type
	
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesCohort
		(
		CohortID,
		ProductID,
		StartDate,
		EndDate,
		UpdateDate,
		Type
		)
		VALUES
		(
		@CohortID,
		@ProductID,
		@StartDate,
		@EndDate,
		GETDATE(),
		@Type
		)
	END
END
GO
