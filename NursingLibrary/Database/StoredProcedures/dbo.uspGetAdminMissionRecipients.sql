IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminMissionRecipients]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdminMissionRecipients]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdminMissionRecipients]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAdminMissionRecipients]
 -- Add the parameters for the stored procedure here
 @emailMissionId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

		--SelectionLevel Definition
        --Institution = 1,
        --AdminUser = 5

 -- get Admin level data
 SELECT C.Email, 5 AS SelectionLevel, CONVERT(varchar(300), C.FirstName + ' ' + C.LastName) COLLATE database_default AS Name
 FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
  JOIN NurAdmin C ON B.PersonID = C.UserID
 WHERE A.EmailMissionId = @emailMissionId AND C.AdminDeleteData IS NULL

 UNION ALL

 -- get institution level data
 SELECT F.Email, 1 AS SelectionLevel, C.InstitutionName AS Name
 FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
  JOIN NurAdminInstitution D ON D.InstitutionID = B.InstitutionID
  JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
  JOIN NurAdmin F ON D.AdminID = F.UserID
 WHERE A.EmailMissionId = @emailMissionId

END
GO
