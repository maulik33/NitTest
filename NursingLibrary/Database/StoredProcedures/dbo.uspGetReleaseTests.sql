SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetReleaseTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetReleaseTests]
GO
PRINT 'Creating PROCEDURE uspGetReleaseTests'
GO

CREATE PROCEDURE [dbo].[uspGetReleaseTests]    
@ReleaseStatus AS CHAR(1)    
AS  
 BEGIN            
   SET NOCOUNT ON         
/*============================================================================================================  
//Purpose: To include the newly added column 'SecondPerQuestion'
--     Modification Purpose:	1.	Sprint 43: PN RN Unification: Returning ProgramOfStudyName 
--									as part of changes done for Nursing-2981 on CMS -> Release Tests Screen.
--								2.	Sprint 43: PN RN Unification: Returning ProgramOfStudyId required to be propagated 
--									while releasing	a test to the live site as part of changes done for Nursing-2981.
//Modified: June 06/12, 05/03/2013, 05/09/2013  
//Author: Liju, Atul, Atul  
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
 SELECT    
 TestID,    
 ProductID,    
 TestName,    
 TestNumber,    
 ActivationTime,    
 TimeActivated,    
 SecureTest_S,    
 SecureTest_D,    
 Scrambling_S,    
 Scrambling_D,    
 Remediation_S,    
 Remediation_D,    
 Explanation_D,    
 Explanation_S,    
 LevelOfDifficulty_S,    
 LevelOfDifficulty_D,    
 NursingProcess_S,    
 NursingProcess_D,    
 ClinicalConcepts_S,    
 ClinicalConcepts_D,    
 Demographics_S,    
 Demographics_D,    
 ClientNeeds_S,    
 ClientNeeds_D,    
 Blooms_S,    
 Blooms_D,    
 Topic_S,    
 Topic_D,    
 SpecialtyArea_S,    
 SpecialtyArea_D,    
 System_S,    
 System_D,    
 CriticalThinking_S,    
 CriticalThinking_D,    
 Reading_S,    
 Reading_D,    
 Math_S,    
 Math_D,    
 Writing_S,    
 Writing_D,    
 RemedationTime_S,    
 RemedationTime_D,    
 ExplanationTime_S,    
 ExplanationTime_D,    
 TimeStamp_S,    
 TimeStamp_D,    
 ActiveTest,    
 TestSubGroup,    
 Url,    
 PopHeight,    
 PopWidth,    
 DefaultGroup ,    
 ReleaseStatus ,  
 SecondPerQuestion,
 PS.ProgramofStudyName AS 'ProgramOfStudyName',
 Tests.ProgramofStudyId
 FROM Tests
 INNER JOIN ProgramofStudy PS
 ON TESTS.ProgramofStudyId = PS.ProgramofStudyId    
 WHERE ReleaseStatus  = @ReleaseStatus    
 ORDER BY ProductID  
 SET NOCOUNT OFF   
END 
GO

PRINT 'Finished creating PROCEDURE uspGetReleaseTests'
GO

