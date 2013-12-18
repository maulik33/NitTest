IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToStudent]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignTestDateToStudent]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToStudent]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToStudent]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignTestDateToStudent]
(
@CohortId INT,
@GroupId INT,
@TestId INT,
@StudentId Int,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS (
	SELECT 1 FROM NurProductDatesStudent
	WHERE StudentID = @StudentId
	AND  GroupID = @GroupId
	AND CohortID = @CohortId
	AND ProductID = @TestID
	AND Type = @Type
)
	BEGIN
		UPDATE  NurProductDatesStudent
		SET StartDate = @StartDate,
		EndDate = @EndDate,
		UpdateDate = getdate()
		WHERE CohortID= @CohortId
		AND ProductID=@TestId
		AND Type=@Type
		AND GroupID=@GroupId
		AND StudentID=@StudentId
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesStudent
		(
			CohortID,
			ProductID,
			StartDate,
			EndDate,
			UpdateDate,
			Type,
			GroupID,
			StudentID
		)
		VALUES
		(
			@CohortID,
			@TestId,
			@StartDate,
			@EndDate,
			getdate(),
			@Type,
			@GroupId,
			@StudentId
		)
	END
END
GO
