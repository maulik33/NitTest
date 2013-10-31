IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetReleaseQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetReleaseQuestions]
GO

/****** Object:  StoredProcedure [dbo].[uspGetReleaseQuestions]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspGetReleaseQuestions]
AS
BEGIN
/*==========================================================================================================================       
  --	 Sample Usage:			Exec uspGetReleaseQuestions
  --     Modification Purpose:	Sprint 43: PN RN Unification: Returning ProgramOfStudyName 
  --							as part of changes done for Nursing-2981 on CMS -> Release Questions Screen.
  --     Modified On:			05/03/2013       
  --     Modified By:			Atul Gupta
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
	SELECT	Questions.QID,
			Questions.QuestionID,
			Questions.ReleaseStatus,
			dbo.Remediation.TopicTitle,
			ClientNeeds.ClientNeeds,
			dbo.ClientNeedCategory.ClientNeedCategory,
			dbo.Systems.System,
			NursingProcess.NursingProcess,
			PS.ProgramofStudyName As 'ProgramOfStudyName'
	FROM   dbo.Questions
	INNER JOIN dbo.ProgramofStudy PS
	ON Questions.ProgramofStudyId = PS.ProgramofStudyId
	LEFT OUTER JOIN dbo.ClientNeedCategory
	ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
	LEFT OUTER JOIN dbo.Remediation
	ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
	LEFT OUTER JOIN dbo.NursingProcess
	ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
	LEFT OUTER JOIN dbo.ClientNeeds
	ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
	LEFT OUTER JOIN dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
	WHERE Questions.ReleaseStatus != 'R'
	ORDER BY Questions.QuestionID
END
GO

