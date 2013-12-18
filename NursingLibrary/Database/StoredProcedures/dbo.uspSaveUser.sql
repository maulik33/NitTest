IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveUser]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveUser]      
	@userId int,      
    @username varchar(255),      
    @userpass varchar(255),      
    @email varchar(255),      
    @institutionId int,      
	@integrated varchar(255),      
    @kaplanUserId varchar(255),      
    @enrollmentId varchar(255),      
	@expireDate datetime,      
    @startDate datetime,      
    @firstname varchar(255),      
    @lastname varchar(255),      
    @ada bit,      
	@cohortId int,      
    @groupId int ,      
    @addressId int,      
    @emergencyPhone varchar(255),      
    @contactPerson varchar(255),      
    @telephone varchar(255),      
    @NewUserId int OUT,      
	@adminUserId int,      
    @adminUserName varchar(255),     
    @repeatExpiryDate DateTime    
AS      

SET NOCOUNT ON;
/*============================================================================================================  
//Purpose: Add Or Update Nursing Student User and Assignment Record
//         Added to log Student's AuditTrail Data when they move between institution/Cohort/Group
//         Checking all possible values of From and To :Institution/Cohort/Group Change for AuditTrail 
//         Appending ProgramOfStudyName to the from and To Institution while logging data to AuditTrail
//         Designating a student as repeat student
//Created/Modified: 10/10/2011,05/14/13,05/21/13 , 06/02/2013 ,09/06/2013  
//Author     :Glenn Maciag
//ModifiedBy :Maulik Shah,Liju
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
BEGIN TRANSACTION      
DECLARE @studentAssignCount INT      
DECLARE @userCount INT      
SELECT @userCount = 0      
      
DECLARE @FromInstitutionName nvarchar(80)      
DECLARE @FromCohartName nvarchar(80)      
DECLARE @FromGroupName nvarchar(80)      
-- Insert or update user record      
IF (@userId = 0)      
 BEGIN      
  SELECT @userCount = UserID      
  FROM dbo.NurStudentInfo      
  WHERE  UserName= @userName      
  AND UserPass = @userPass      
  -- INSERT new student base record      
  IF @userCount = 0      
   BEGIN      
    INSERT INTO NurStudentInfo      
     (UserName,      
      FirstName,      
      LastName,      
      UserPass,      
      Email,      
      InstitutionID,      
      Integreted,      
      UserCreateDate,      
      UserType,      
      UserStartDate,      
      UserExpireDate,      
      KaplanUserID,      
      EnrollmentID,      
      ADA,      
      AddressID,      
      EmergencyPhone,      
      ContactPerson,      
      Telephone)      
    VALUES      
     (@username,      
      @firstname,      
      @lastname,      
      @userpass,      
      @email,      
      @institutionId,      
      @integrated,      
      GetDate(),      
      'S',      
      @startDate,      
      @expireDate,      
      @kaplanUserId,      
      @enrollmentId,      
      @ada,      
      @addressId,      
      @emergencyPhone,      
      @contactPerson,      
      @telephone)      
    SELECT @NewUserId = SCOPE_IDENTITY()      
    INSERT INTO NusStudentAssign      
    (      
     StudentID,      
     CohortID,      
     GroupID,      
     Access      
     )    
    VALUES      
    (      
     @NewUserId,      
     @cohortId,      
     @groupId,      
     1      
    )      
    -- Add AuditTrail Data       
               SELECT @FromInstitutionName = InstitutionName FROM NurInstitution      
      WHERE InstitutionID =  @institutionId      
      
      SELECT @FromCohartName = CohortName FROM NurCohort      
      WHERE CohortID  = @cohortId       
            
      SELECT @FromGroupName = GroupName FROM NurGroup      
      WHERE GroupID = @groupId      
      
      DECLARE @ProgramOfStudyName nvarchar(10)      
      
      SELECT @ProgramOfStudyName = ProgramOfStudyName FROM ProgramOfStudy POS      
      INNER JOIN NurInstitution I ON I.ProgramOfStudyId = POS.ProgramOfStudyId      
      WHERE InstitutionID =  @institutionId      
      
      INSERT INTO NurAuditTrail (StudentId,StudentUserName,FromInstitution,FromCohort,FromGroup,AdminUserId,AdminUserName)      
       VALUES (@NewUserId,@username,@FromInstitutionName + '-'+@ProgramOfStudyName,@FromCohartName,@FromGroupName,@adminUserId,@adminUserName)           
   END      
  ELSE      
   BEGIN      
    SELECT @NewUserId = -1      
   END        
  IF @@ERROR > 0      
   ROLLBACK TRANSACTION      
  ELSE      
   COMMIT TRANSACTION      
  RETURN @NewUserId      
 END      
ELSE      
BEGIN      
    -- Add record to AuditTrail before updating student base record      
   DECLARE @ToInstitutionName nvarchar(80)      
   DECLARE @ToCohartName nvarchar(80)      
   DECLARE @ToGroupName nvarchar(80)      
   DECLARE @ToProgramOfStudyName nvarchar(10)      
   DECLARE @FromProgramOfStudyName nvarchar(10)      
   -- Get Student's Current InstitutionName,CohartName and GroupName       
   SELECT @FromInstitutionName = InstitutionName FROM NurInstitution      
   WHERE InstitutionID IN       
   (      
       SELECT InstitutionId FROM  NurStudentInfo      
    WHERE UserID = @userId      
    )      
          
   SELECT @FromCohartName = CohortName FROM NurCohort      
   WHERE CohortID IN       
   (      
   SELECT CohortID FROM NusStudentAssign      
   WHERE StudentID = @userId      
   )      
      
   SELECT @FromGroupName = GroupName FROM NurGroup      
   WHERE GroupID IN       
   (      
      SELECT GroupID FROM NusStudentAssign      
      WHERE StudentID = @userId      
   )      
      
    -- If InstitutionName/CohartName/GroupName has changed get that record and insert into AuditTrail      
 SELECT @ToInstitutionName = InstitutionName FROM NurInstitution      
 WHERE InstitutionID =  @institutionId      
      
 SELECT @ToCohartName = CohortName FROM NurCohort      
 WHERE CohortID  = @cohortId       
            
 SELECT @ToGroupName = GroupName FROM NurGroup      
 WHERE GroupID = @groupId      
       
 SELECT @FromProgramOfStudyName = ProgramOfStudyName FROM ProgramOfStudy POS      
 INNER JOIN NurInstitution I ON I.ProgramOfStudyId = POS.ProgramOfStudyId      
 WHERE InstitutionID IN       
   (      
       SELECT InstitutionId FROM  NurStudentInfo      
    WHERE UserID = @userId      
    )      
      
 SELECT @ToProgramOfStudyName = ProgramOfStudyName FROM ProgramOfStudy POS      
 INNER JOIN NurInstitution I ON I.ProgramOfStudyId = POS.ProgramOfStudyId      
 WHERE InstitutionID =  @institutionId      
       
    IF ( ((@FromInstitutionName != @ToInstitutionName) OR (@FromInstitutionName IS NULL AND @ToInstitutionName IS         
      NOT NULL) OR (@FromInstitutionName IS NOT NULL AND @ToInstitutionName IS NULL)) OR       
         ((@FromCohartName != @ToCohartName)OR(@FromCohartName IS NULL AND @ToCohartName IS NOT NULL)OR (@FromCohartName IS NOT NULL AND @ToCohartName IS NULL)) OR       
         ((@FromGroupName != @ToGroupName) OR (@FromGroupName IS NULL AND @ToGroupName IS NOT NULL)OR (@FromGroupName IS NOT NULL AND @ToGroupName IS NULL))       
       )      
  BEGIN      
      INSERT INTO NurAuditTrail (StudentId,StudentUserName,FromInstitution,FromCohort,FromGroup,ToInstitution,ToCohort,ToGroup,DateMoved,AdminUserId,AdminUserName)      
      VALUES (@userId,@username,@FromInstitutionName+'-'+@FromProgramOfStudyName,@FromCohartName,@FromGroupName,@ToInstitutionName+ '-'+@ToProgramOfStudyName,@ToCohartName,@ToGroupName, GetDate(),@adminUserId,@adminUserName)        
     END   
 -- UPDATE existing student base record     
 
 UPDATE NurStudentInfo      
 SET   
  
  UserPass = @userpass,      
  UserName = @username,      
  Email = @email,      
  InstitutionId = @institutionId,      
  EnrollmentId = @enrollmentId,      
  ADA = @ada,      
  Integreted = @integrated,      
  FirstName = @firstname,      
  LastName = @lastname,      
  UserUpdateDate = GetDate(),      
  UserStartDate = @startDate,      
  KaplanUserID = @kaplanUserId,      
  UserExpireDate = @expireDate,      
  AddressID=@addressId,      
  EmergencyPhone=@emergencyPhone,      
  ContactPerson=@contactPerson,      
  Telephone=@telephone,  
  RepeatExpiryDate = @repeatExpiryDate   
 WHERE UserID = @userId      
SELECT @NewUserId = @userId      
 -- INSERT / UPDATE student assignment record      
SET @studentAssignCount = (      
   SELECT COUNT(*) FROM NusStudentAssign      
   WHERE StudentID = @userId      
        )      
 IF (@studentAssignCount IS NOT NULL AND @studentAssignCount > 0)      
  BEGIN      
   UPDATE NusStudentAssign      
   SET CohortID = @cohortId,      
    GroupID = @groupId,      
    Access = 1      
   WHERE StudentID = @userId;      
  END      
 ELSE      
  BEGIN      
   INSERT INTO NusStudentAssign      
      (      
      StudentID,      
      CohortID,      
      GroupID,      
      Access      
      )      
   VALUES      
      (      
       SCOPE_IDENTITY(),      
       @cohortId,      
       @groupId,      
       1      
      )      
  END      
  IF @@ERROR > 0      
   ROLLBACK TRANSACTION      
  ELSE      
   COMMIT TRANSACTION      
  RETURN @NewUserId      
  RETURN @NewUserId      
   SET NOCOUNT OFF       
 END 
 GO
PRINT 'Finished creating PROCEDURE uspSaveUser'
GO 


