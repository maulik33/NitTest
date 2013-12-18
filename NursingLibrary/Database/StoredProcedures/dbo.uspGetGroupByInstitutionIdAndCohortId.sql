IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroupByInstitutionIdAndCohortId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetGroupByInstitutionIdAndCohortId]
GO

/****** Object:  StoredProcedure [dbo].[uspGetGroupByInstitutionIdAndCohortId]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetGroupByInstitutionIdAndCohortId]
@InstitutionID INT,
@CohortIds VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT DISTINCT dbo.NurGroup.GroupID, dbo.NurGroup.GroupName
    FROM dbo.NurGroup
    INNER JOIN dbo.NurCohort ON dbo.NurGroup.CohortID = dbo.NurCohort.CohortID
    INNER JOIN dbo.NurInstitution ON dbo.NurCohort.InstitutionID = dbo.NurInstitution.InstitutionID
    WHERE     (dbo.NurCohort.CohortStatus = 1)
    AND dbo.NurCohort.InstitutionID=@InstitutionID
    AND ( (@CohortIds <> '0' AND dbo.NurGroup.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
		 OR @CohortIds = '0')
	AND dbo.NurCohort.InstitutionID= @InstitutionID
	ORDER BY GroupName asc
		
	SET NOCOUNT OFF
END
GO
