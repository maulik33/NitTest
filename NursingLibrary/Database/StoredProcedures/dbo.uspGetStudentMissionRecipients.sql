IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentMissionRecipients]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentMissionRecipients]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentMissionRecipients]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get email mission details
-- =============================================
CREATE PROCEDURE [dbo].[uspGetStudentMissionRecipients]
	-- Add the parameters for the stored procedure here
	@emailMissionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		--SelectionLevel Definition
        --Institution = 1,
        --Cohort = 2,
        --Group = 3,
        --StudentUser = 4,
        --AdminUser = 5

	-- get student level data
	SELECT C.Email, 4 AS SelectionLevel, C.FirstName + ' ' + C.LastName AS Name
	FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurStudentInfo C ON B.PersonID = C.UserID
	WHERE A.EmailMissionId = @emailMissionId AND C.UserDeleteData IS NULL

	UNION ALL

	-- get group level data
	SELECT F.Email, 3 AS SelectionLevel, C.GroupName AS Name
	FROM EmailMission A
		JOIN EmailGroup B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.GroupId = B.GroupId
		JOIN NurGroup C ON B.GroupID = C.GroupID
		JOIN NurStudentInfo F ON SA.StudentID = F.UserID
	WHERE A.EmailMissionId = @emailMissionId
	AND F.UserDeleteData IS NULL AND SA.DeletedDate IS NULL

	UNION ALL

	-- get institution level data
	SELECT D.Email, 1 AS SelectionLevel, C.InstitutionName AS Name
	FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
		JOIN NurStudentInfo D ON C.InstitutionID = D.InstitutionID
	WHERE A.EmailMissionId = @emailMissionId AND D.UserDeleteData IS NULL

	UNION ALL

	-- get cohort level data
	SELECT E.Email, 2 AS SelectionLevel, C.CohortName AS Name
	FROM EmailMission A
		JOIN EmailCohort B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.CohortId = B.CohortId
		JOIN NurStudentInfo E ON SA.StudentID = E.UserID
		JOIN NurCohort C ON B.CohortID = C.CohortID
	WHERE A.EmailMissionId = @emailMissionId AND E.UserDeleteData IS NULL
	
END
GO
