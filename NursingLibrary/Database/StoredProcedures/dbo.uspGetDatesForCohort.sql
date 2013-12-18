IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDatesForCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetDatesForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetDatesForCohort]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetDatesForCohort]    Script Date: 05/08/2011  ******/
CREATE PROCEDURE [dbo].[uspGetDatesForCohort]
	@cohortId int
AS
SET NOCOUNT ON
BEGIN
	SELECT  CohortStartDate ,
			CohortEndDate
	FROM    dbo.NurCohort
	WHERE CohortID = @cohortId
END
GO
