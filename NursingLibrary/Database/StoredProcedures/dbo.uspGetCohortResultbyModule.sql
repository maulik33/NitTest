IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortResultbyModule]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCohortResultbyModule]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortResultbyModule]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCohortResultbyModule]
 @CohortIds VARCHAR(MAX),
 @CaseID int,
 @MID VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	 Select ISNULL(SUM(M.Correct),0) AS Correct, ISNULL(SUM(M.Total),0) As Total
	 from CaseModuleScore M
	 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	 WHERE UserType='S' AND UserDeleteData is NULL
	 AND A.CohortID IN (SELECT VALUE FROM dbo.funcListToTableInt(@CohortIds,'|'))
	 AND (A.DeletedDate IS NULL)
	 AND M.CaseID=@CaseID
	 AND M.ModuleID IN (SELECT VALUE FROM dbo.funcListToTableInt(@MID,'|'))

	SET NOCOUNT OFF
END
GO
