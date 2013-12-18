SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentsForCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentsForCohort]
GO
CREATE PROCEDURE [dbo].[uspGetStudentsForCohort]
(
@InstitutionId INT,
@CohortId INT,
@GroupId INT
)
 AS
BEGIN
SET NOCOUNT ON;
/*============================================================================================================                
 -- Purpose: To retrieve the Studentlist  by Institution,Cohort and group
 -- Modified to return ProgramofStudyName to append to InstitutionName
 -- Modified: 10/10/2011, 05/30/2013          
 -- Author:Glenn,Maulik       
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
	IF @InstitutionId != -1
		BEGIN
			SELECT  SI.UserID,
			SI.FirstName,
			SI.LastName,
			SI.LastName+','+SI.FirstName as [Name],
			SI.UserName,
			SA.CohortID,
			SA.GroupID,
			SA.DeletedDate,
			C.CohortName,
			C.CohortStatus,
			CP.ProgramID,
			CP.Active,
			P.ProgramName,
			P.DeletedDate AS P_DeletedDate,
			SI.InstitutionID,
			I.InstitutionName,
			I.Status,
			G.GroupName ,
			0 as IsAssigned,
			POS.ProgramofStudyName
			FROM   dbo.NurInstitution I
			INNER JOIN dbo.NurStudentInfo SI
			INNER JOIN dbo.NusStudentAssign SA
			ON SI.UserID = SA.StudentID
			 LEFT OUTER JOIN dbo.NurCohort C
			ON SA.CohortID = C.CohortID
			ON I.InstitutionID = SI.InstitutionID
			AND I.InstitutionID = C.InstitutionID
			LEFT OUTER JOIN dbo.NurGroup G
			ON SA.GroupID = G.GroupID
			AND C.CohortID = G.CohortID
			LEFT OUTER JOIN dbo.NurProgram P
			INNER JOIN dbo.NurCohortPrograms CP
			ON P.ProgramID = CP.ProgramID
			ON C.CohortID = CP.CohortID
			INNER JOIN ProgramofStudy POS 
			ON POS.ProgramofStudyId = I.ProgramofStudyId     
			WHERE     (SA.DeletedDate IS NULL)
			AND (C.CohortStatus = 1
			OR C.CohortStatus IS NULL)
			AND (CP.Active IS NULL
			OR CP.Active = 1)
			AND (P.DeletedDate IS NULL)
			AND (I.Status = 1)
			AND (@InstitutionId = 0
				OR I.InstitutionID = @InstitutionId)		
			AND (@CohortId = 0
				OR(SA.CohortID IS NULL
				   OR SA.CohortID=0
				   OR SA.CohortID = @CohortId))
			AND (@GroupId = 0
				OR(SA.GroupId = @GroupId))
		END
	ELSE
		BEGIN
			SELECT
			SI.UserPass ,
			SI.LastName+','+SI.FirstName as [Name],
			SI.Telephone ,
			SI.Email ,
			SI.UserType  ,
			SI.InstitutionID ,
			SI.CountryCode ,
			SI.UserCreateDate ,
			SI.UserExpireDate ,
			SI.UserStartDate  ,
			SI.UserUpdateDate ,
			SI.UserDeleteData ,
			SI.Integreted  ,
			SI.KaplanUserID ,
			SI.EnrollmentID ,
			SI.ADA ,
			SI.SessionID ,
			SA.CohortID,
			SA.GroupID,
			SA.Access,
			SA.DeletedAdmin,
			SI.UserID,			
			SI.FirstName,
			SI.LastName,
			SI.UserName,
			SA.CohortID,
			SA.GroupID,
			SA.DeletedDate,
			C.CohortName,
			C.CohortStatus,
			CP.ProgramID,
			CP.Active,
			P.ProgramName,
			P.DeletedDate AS P_DeletedDate,
			SI.InstitutionID,
			I.InstitutionName,
			I.Status,
			'' AS GroupName,
			(
			 SELECT count(*) FROM dbo.NusStudentAssign
			 WHERE StudentID = SI.UserID
			 AND  GroupID = @GroupId
			 ) as IsAssigned,
			POS.ProgramofStudyName			
			FROM dbo.NurInstitution I
			INNER JOIN dbo.NurStudentInfo SI
			INNER JOIN dbo.NusStudentAssign SA
			ON SI.UserID = SA.StudentID
			INNER JOIN dbo.NurCohort C
			ON SA.CohortID = C.CohortID
			ON I.InstitutionID = SI.InstitutionID
			AND  I.InstitutionID = C.InstitutionID
			LEFT OUTER JOIN dbo.NurProgram P
			INNER JOIN dbo.NurCohortPrograms CP
			ON P.ProgramID = CP.ProgramID
			ON  C.CohortID = CP.CohortID
			INNER JOIN ProgramofStudy POS 
			ON POS.ProgramofStudyId = I.ProgramofStudyId     
			WHERE (SA.DeletedDate IS NULL)
			AND (C.CohortStatus = 1)
			AND (CP.Active IS NULL OR CP.Active = 1)
			AND (P.DeletedDate IS NULL)
			AND (I.Status = 1)
			AND C.CohortID = @CohortId
			AND (SA.GroupID IS NULL
				OR SA.GroupID = 0
				OR SA.GroupID = @GroupId)
		END
   SET NOCOUNT OFF
  END
GO
PRINT 'Finished creating PROCEDURE uspGetStudentsForCohort'
GO 

