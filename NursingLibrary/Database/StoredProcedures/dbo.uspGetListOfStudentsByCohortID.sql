IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfStudentsByCohortID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfStudentsByCohortID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetListOfStudentsByCohortID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetListOfStudentsByCohortID]
@CohortID int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

	SELECT  dbo.NurStudentInfo.UserID, dbo.NurStudentInfo.FirstName, dbo.NurStudentInfo.LastName
	, dbo.NurStudentInfo.LastName+','+dbo.NurStudentInfo.FirstName as Name, dbo.NurStudentInfo.UserName
	FROM   dbo.NurStudentInfo
	INNER JOIN	dbo.NusStudentAssign ON dbo.NurStudentInfo.UserID = dbo.NusStudentAssign.StudentID
	WHERE (dbo.NusStudentAssign.DeletedDate IS NULL)
	AND (dbo.NusStudentAssign.CohortID IS NULL OR dbo.NusStudentAssign.CohortID=0 Or dbo.NusStudentAssign.CohortID=@CohortID)
	
 SET NOCOUNT OFF
END
GO
