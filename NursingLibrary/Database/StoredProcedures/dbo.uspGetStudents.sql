SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudents]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudents]
GO
CREATE PROCEDURE [dbo].[uspGetStudents]
@studentId int
AS

BEGIN
SET NOCOUNT ON;
/*============================================================================================================                
 -- Purpose: To retrieve the Student Information by studentId
 -- Modified to return ProgramOfStudyId and ProgramofStudyName to which student belongs to. Nursing-3846
 -- Fixed - If Instituion is not assigned or set to Not Assigned then other student details should be returned.
 -- Fixed NURSING-3870 issue to return programofstudy name
 --Fixed NURSING- 3760 Designate a student as a repeat student
 -- Modified: 10/10/2011, 05/24/2013 ,05/25/2013 ,05/28/2013 ,09/06/2013             
 -- Author:Glenn,Maulik ,Liju
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
	DECLARE @InstitutionID int = 0
	DECLARE @ProgramOfStudyID int = 0
	
	IF EXISTS (SELECT InstitutionID FROM NurStudentInfo WHERE  UserID = @studentId)
	BEGIN
	    SELECT @InstitutionID = InstitutionID FROM NurStudentInfo WHERE  UserID = @studentId
		SELECT @ProgramOfStudyID = ProgramOfStudyId  FROM NurInstitution WHERE InstitutionID = @InstitutionID
	END
		
	SELECT
		S.UserID,
		S.FirstName,
		S.LastName ,
		S.UserName ,
		S.UserPass ,
		S.Telephone ,
		S.Email ,
		S.UserType  ,
		S.InstitutionID ,
		S.CountryCode ,
		S.UserCreateDate ,
		S.UserExpireDate ,
		S.UserStartDate  ,
		S.UserUpdateDate ,
		S.UserDeleteData ,
		S.Integreted  ,
		S.KaplanUserID ,
		S.EnrollmentID ,
		S.ADA ,
		S.SessionID ,
		S.AddressID,
		S.EmergencyPhone,
		S.ContactPerson,
		S.Telephone,
		A.CohortID,
		A.GroupID,
		A.Access,
		A.DeletedDate,
		A.DeletedAdmin,
        P.ProgramOfStudyId,
		P.ProgramofStudyName,
	(SELECT CONVERT(VARCHAR(10),S.RepeatExpiryDate,101) FROM  dbo.NurStudentInfo WHERE UserID=@studentId )AS 'RepeatExpiryDate'   
	FROM  dbo.NurStudentInfo S
	LEFT JOIN dbo.NusStudentAssign  A
	ON S.UserID = A.StudentID
	LEFT JOIN ProgramofStudy P ON P.ProgramofStudyId = @ProgramOfStudyID         
	WHERE UserID=@studentId
SET NOCOUNT OFF
END
GO
PRINT 'Finished creating PROCEDURE uspGetStudents'
GO 

