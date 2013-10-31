IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserInfo]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUserInfo]
GO

/****** Object:  StoredProcedure [dbo].[uspGetUserInfo]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetUserInfo]
	@userid int
AS
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 SELECT UserId, FirstName,LastName, UserName, UserPass, NurStudentInfo.Email,TimeZones.Hour AS TimeOffset, dbo.NurStudentInfo.InstitutionID,    
  dbo.NusStudentAssign.CohortID,dbo.NusStudentAssign.GroupID,    
  COALESCE (dbo.NurCohortPrograms.ProgramID, 0) AS ProgramID, Integreted, ADA, UserStartDate, UserExpireDate,    
  EnrollmentId, KaplanUserId, NurInstitution.Ip ,NurProgram.ProgramOfStudyId, NurInstitution.ProctorTrackEnabled       
 FROM dbo.NusStudentAssign    
  INNER JOIN dbo.NurCohortPrograms ON dbo.NusStudentAssign.CohortID = dbo.NurCohortPrograms.CohortID    
  INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
  INNER JOIN dbo.NurInstitution ON  dbo.NurStudentInfo.InstitutionID = dbo.NurInstitution.InstitutionID    
  INNER JOIN TimeZones ON dbo.NurInstitution.TimeZone = TimeZones.TimeZoneID    
  INNER JOIN dbo.NurCohort ON NusStudentAssign.CohortID = dbo.NurCohort.CohortID    
  INNER JOIN  NurProgram ON NurCohortPrograms.ProgramID =NurProgram.ProgramID    
 WHERE (dbo.NurStudentInfo.UserStartDate is null or dbo.NurStudentInfo.UserStartDate<getdate())AND    
  (dbo.NurStudentInfo.UserExpireDate is null or dbo.NurStudentInfo.UserExpireDate>getdate()) AND    
  (dbo.NurCohort.CohortEndDate>getdate())AND (dbo.NurCohort.CohortStartDate<getdate()) AND    
  ((dbo.NurCohortPrograms.Active = 1) OR (dbo.NurCohortPrograms.Active IS NULL))AND    
   (dbo.NurStudentInfo.UserId=@userId)    
   AND (dbo.NurStudentInfo.UserDeleteData is null)    
END 
GO

