IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetEmailMissionDetails]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetEmailMissionDetails]
GO

/****** Object:  StoredProcedure [dbo].[uspGetEmailMissionDetails]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get email mission details (student lists)
-- =============================================
CREATE PROCEDURE [dbo].[uspGetEmailMissionDetails]
	-- Add the parameters for the stored procedure here
	@emailMissionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- get student level data
	SELECT C.username, C.userpass, C.email
	FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurStudentInfo C ON B.PersonID = C.UserID
	WHERE A.EmailMissionId = @emailMissionId AND C.UserDeleteData IS NULL

	UNION ALL

	-- get group level data
	SELECT F.username, F.userpass, F.email
	FROM EmailMission A
		JOIN EmailGroup B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.GroupId = B.GroupId
		JOIN NurStudentInfo F ON SA.StudentID = F.UserID
	WHERE A.EmailMissionId = @emailMissionId
	AND F.UserDeleteData IS NULL AND SA.DeletedDate IS NULL

	UNION ALL

	-- get institution level data
	SELECT D.username, D.userpass, D.email
	FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
		JOIN NurStudentInfo D ON C.InstitutionID = D.InstitutionID
	WHERE A.EmailMissionId = @emailMissionId AND D.UserDeleteData IS NULL

	UNION ALL

	-- get cohort level data
	SELECT E.username, E.userpass, E.email
	FROM EmailMission A
		JOIN EmailCohort B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.CohortId = B.CohortId
		JOIN NurStudentInfo E ON SA.StudentID = E.UserID
	WHERE A.EmailMissionId = @emailMissionId AND E.UserDeleteData IS NULL
END
GO
