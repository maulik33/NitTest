IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignTestDateToGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToGroup]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToGroup]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignTestDateToGroup]
(
@GroupId INT,
@CohortId INT,
@TestId INT,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS
(
	SELECT 1
	FROM NurProductDatesGroup
	WHERE
	GroupID = @GroupId
	AND CohortID = @CohortId
	AND ProductID = @TestId
	AND Type = @Type
)
	BEGIN

		UPDATE  NurProductDatesGroup
			SET StartDate = @StartDate,
				EndDate=@EndDate,
				UpdateDate = getdate()
				WHERE CohortID = @CohortId
				AND ProductID = @TestId
				AND Type=@Type
				AND GroupID=@GroupId
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesGroup
		(
			CohortID,
			ProductID,
			StartDate,
			EndDate,
			UpdateDate,
			Type,
			GroupID
		)
		VALUES
		(
			 @CohortId,
			 @TestId,
			 @StartDate,
			 @EndDate,
			 getdate(),
			 @Type,
			 @GroupId
		)
	END
END
GO
