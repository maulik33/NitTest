IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignAdhocStudentTestDates]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignAdhocStudentTestDates]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignAdhocStudentTestDates]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspAssignAdhocStudentTestDates]
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
GO
