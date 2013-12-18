IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortsByStudentID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCohortsByStudentID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortsByStudentID]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCohortsByStudentID]
@StudentId INT
AS
BEGIN
	SELECT dbo.NurCohort.CohortID, dbo.NurCohort.CohortName
    FROM dbo.NurCohort
    INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
    INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
    WHERE  (dbo.NurStudentInfo.UserID = @StudentId)
END
GO
