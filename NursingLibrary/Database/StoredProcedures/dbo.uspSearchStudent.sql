SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudent]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchStudent]
GO
CREATE PROCEDURE [dbo].[uspSearchStudent]
@programOfStudyId INT,
@InstitutionId INT,
@CohortId INT,
@GroupId INT,
@SearchString VARCHAR(400),
@AssignStudent BIT
AS
BEGIN
SET NOCOUNT ON;
/*============================================================================================================                
 -- Purpose: To search the Student Information by InstitutionId,CohortId,GroupId and specifc searchstring
 -- to select firstname/lastname/username 
 -- Modified to return ProgramOfStudyId and ProgramofStudyName to which student belongs to. Nursing-3846
 -- Added ProgramOfStudy Filter to search student belongs to RN institution / PN institution 
 -- Sprint 46:	Change done for Nursing 3586 (Assign Students). 
 --				Fixed a bug against NURSING-3846 to return unassigned student list
 --				Fixed a bug against NURSING-3846 to return complete unassigned student list for institutionid = 0
 --				Fixed a bug against NURSING-4647 to return student text search based on a programofstudyId
 --Returning ProctorTrackEnabled flag for institution 
 -- Modified: 10/10/2011, 05/24/2013,06/07/2013, 06/26/2013, 06/27/2013,09/11/2013,10/21/2013                   
 -- Author:Glenn,Maulik, Atul, Atul,Liju
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

	IF @AssignStudent = 0
			BEGIN
				SELECT
					S.UserID,
					S.FirstName,
					S.LastName,
					S.UserName,
					S.UserPass,
					S.Telephone,
					S.Email,
					S.UserType,
					S.InstitutionID,
					S.CountryCode,
					S.UserCreateDate,
					S.UserExpireDate,
					S.UserStartDate,
					S.UserUpdateDate,
					S.UserDeleteData,
					S.Integreted,
					S.KaplanUserID,
					S.EnrollmentID,
					S.ADA,
					S.SessionID ,
					A.CohortID,
					A.GroupID,
					I.InstitutionName,
					C.CohortName,
					G.GroupName,
					P.ProgramofStudyId,
					P.ProgramofStudyName,
					I.ProctorTrackEnabled
				FROM NurStudentInfo S
				LEFT OUTER JOIN NusStudentAssign A
				ON S.UserID=A.StudentID
				LEFT JOIN NurInstitution I
				ON S.InstitutionID=I.InstitutionID
				LEFT OUTER JOIN NurCohort C
				ON A.CohortID=C.CohortID
				LEFT OUTER JOIN NurGroup G
				ON A.GroupID=G.GroupID
				LEFT JOIN ProgramofStudy P 
				ON P.ProgramofStudyId = I.ProgramofStudyId 
				WHERE UserType='S' AND UserDeleteData IS NULL
				AND (A.DeletedDate IS NULL)
				AND (@InstitutionId = 0 OR I.InstitutionID = @InstitutionId)
				AND (@CohortId= 0 OR C.CohortID = @CohortId )
				AND (@GroupId = 0 OR G.GroupID = @GroupId  )				
				AND (@ProgramofStudyId = 0 OR I.ProgramofStudyId = @ProgramofStudyId)  
				AND (LEN(LTRIM(RTRIM(@SearchString))) = 0
				OR( FirstName like +'%'+ @SearchString + '%' OR LastName like +'%'+ @SearchString +'%'
				OR UserName like +'%' + @SearchString + '%'))
			END
	ELSE
			BEGIN
				SELECT
					S.UserID,
					S.FirstName,
					S.LastName,
					S.UserName,
					S.UserPass,
					S.Telephone,
					S.Email,
					S.UserType,
					S.InstitutionID,
					S.CountryCode,
					S.UserCreateDate,
					S.UserExpireDate,
					S.UserStartDate,
					S.UserUpdateDate,
					S.UserDeleteData,
					S.Integreted,
					S.KaplanUserID,
					S.EnrollmentID,
					S.ADA,
					S.SessionID ,
					A.CohortID,
					A.GroupID,
					I.InstitutionName,
					C.CohortName,
					G.GroupName,
					P.ProgramofStudyId,
					P.ProgramofStudyName,
					I.ProctorTrackEnabled
					from NurStudentInfo S
				LEFT OUTER JOIN NusStudentAssign A
				ON S.UserID=A.StudentID
				LEFT JOIN NurInstitution I
				ON S.InstitutionID=I.InstitutionID
				LEFT OUTER JOIN NurCohort C
				ON A.CohortID=C.CohortID
				LEFT OUTER JOIN NurGroup G
				ON A.GroupID=G.GroupID
				LEFT JOIN ProgramofStudy P 
				ON P.ProgramofStudyId = I.ProgramofStudyId
				WHERE UserType='S' AND UserDeleteData IS NULL
				AND (A.DeletedDate IS NULL)
				AND S.Integreted=1
				AND (S.InstitutionID=0
				OR A.CohortID =0)
				AND (FirstName like +'%'+ @SearchString + '%'
				OR LastName like +'%'+ @SearchString +'%'
				OR UserName like +'%' + @SearchString + '%')
			END
	SET NOCOUNT OFF
END
GO
PRINT 'Finished creating PROCEDURE uspSearchStudent'
GO 

