
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspLoginUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspLoginUser]
GO
PRINT 'Creating PROCEDURE uspLoginUser'
GO

/****** Object:  StoredProcedure [dbo].[uspLoginUser]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspLoginUser]    
 -- Add the parameters for the stored procedure here    
 @username varchar(80), @password varchar(50)    
AS    
BEGIN        
 /*============================================================================        
 --     Modified: 07/19/2012 ,05/28/2013,06/10/2013, 08/29/2013   
 --     Author: Mohan,shodhan,Liju, Glenn
 --     Purpose: Return ProctorTrackEnabled flag for Nursing-4484 (Verificient)        
 ******************************************************************************        
 * This software is the confidential and proprietary information of            
 * Kaplan,Inc. ("Confidential Information").  You shall not            
 * disclose such Confidential Information and shall use it only in            
 * accordance with the terms of the license agreement you entered into            
 * with Kaplan, Inc.            
 *            
 * KAPLAN, INC. MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE             
 * SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT             
 * NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR            
 * A PARTICULAR  PURPOSE, OR NON-INFRINGEMENT. KAPLAN, INC. SHALL             
 * NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF             
 * USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.            
 *****************************************************************************/        
         
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
    Declare @UserID int,@FirstName nvarchar(80),@LastName nvarchar(80),@KaplanUserID varchar(50), @InstitutionID int,@CohortID int,@ProgramID int, @Ada bit, @TimeOffset int,@EnrollmentID varchar(50),@Ip varchar(250),@GroupID int,@ManageAccount bit,
	@ProgramofStudyId as int, @ProctorTrackEnabled as int      
 SELECT @UserID =UserID, @FirstName = FirstName,@LastName= LastName,@KaplanUserID = KaplanUserID, @InstitutionID = NurStudentInfo.InstitutionID,@CohortID = NusStudentAssign.CohortID,        
  @ProgramID = COALESCE (dbo.NurCohortPrograms.ProgramID, 0) , @Ada = Ada, @TimeOffset = TimeZones.Hour , @EnrollmentID = EnrollmentID,        
  @Ip =NurInstitution.Ip,@GroupID = NusStudentAssign.GroupID, @ManageAccount = NurInstitution.PayLinkEnabled ,@ProgramofStudyId = NurProgram.ProgramOfStudyId, @ProctorTrackEnabled = NurInstitution.ProctorTrackEnabled      
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
   (dbo.NurStudentInfo.UserPass=@password AND dbo.NurStudentInfo.UserName=@username )        
   AND (dbo.NurStudentInfo.UserDeleteData is null)        
        
Declare @TestExistsIntegrated bit,@TestExistsFocussed bit,@TestExistsNclex bit,@TestExistsQbank bit,        
@TestExistsQbankSample bit,@TestExistsTimedQbank bit,@TestExistsDiagnostic bit,@TestExistsReadiness bit,        
@TestExistsDiagnosticResult bit,@TestExistsReadinessResult bit, @TestExistsSkillsModule bit        
        
EXECUTE [dbo].[uspCheckTestExists] @UserID,1,1,0,@TimeOffset,@TestExistsIntegrated OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,3,1,1,@TimeOffset,@TestExistsFocussed OUTPUT        
IF @TestExistsFocussed = 0 EXECUTE [dbo].[uspCheckTestExists] @UserID,3,1,0,@TimeOffset,@TestExistsFocussed OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,-1,0,@TimeOffset,@TestExistsNclex OUTPUT  
EXECUTE [dbo].[uspCheckQbankExists]@UserID,4,3,@TimeOffset,@TestExistsQbank OUTPUT       
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,2,0,@TimeOffset,@TestExistsQbankSample OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,1,0,@TimeOffset,@TestExistsTimedQbank OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,4,0,@TimeOffset,@TestExistsDiagnostic OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,5,0,@TimeOffset,@TestExistsReadiness OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,6,0,@TimeOffset,@TestExistsDiagnosticResult OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,7,0,@TimeOffset,@TestExistsReadinessResult OUTPUT        
EXECUTE [dbo].[uspCheckTestExists] @UserID,6,1,0,@TimeOffset,@TestExistsSkillsModule OUTPUT        
        
SELECT @UserID AS UserID,@FirstName AS FirstName,@LastName AS LastName,@username AS UserName, @KaplanUserID AS KaplanUserID,@InstitutionID AS InstitutionID        
,@CohortID AS CohortID,@ProgramID AS ProgramID, @Ada AS Ada, @TimeOffset AS TimeOffset,@EnrollmentID AS EnrollmentID,@Ip AS Ip,@ManageAccount AS PayLinkEnabled        
,@TestExistsIntegrated AS TestExistsIntegrated,@TestExistsFocussed AS TestExistsFocussed,@TestExistsNclex AS TestExistsNclex        
,@TestExistsQbank AS TestExistsQbank,@TestExistsQbankSample AS TestExistsQbankSample,@TestExistsTimedQbank AS TestExistsTimedQbank        
,@TestExistsDiagnostic AS TestExistsDiagnostic,@TestExistsReadiness AS TestExistsReadiness        
,@TestExistsDiagnosticResult AS TestExistsDiagnosticResult ,@TestExistsReadinessResult AS TestExistsReadinessResult,@GroupID AS GroupID        
,@TestExistsSkillsModule As TestExistsSkillsModule,@ProgramofStudyId As ProgramofStudyId, @ProctorTrackEnabled as ProctorTrackEnabled      
        
END 
GO
PRINT 'Finished creating PROCEDURE uspLoginUser'
GO 


