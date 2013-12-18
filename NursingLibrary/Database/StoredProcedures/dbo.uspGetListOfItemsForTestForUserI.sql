SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfItemsForTestForUserI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetListOfItemsForTestForUserI]
GO

PRINT 'Creating PROCEDURE uspGetListOfItemsForTestForUserI'
GO

CREATE PROCEDURE [dbo].[uspGetListOfItemsForTestForUserI]    
@UserTestID int, @TypeOfFileID varchar(500)     
AS    
 BEGIN              
 SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose: To include the details of the newly added categories
 --  Modified: To include the newcategory (Concepts) as a part of NURSING_4720 
 --  Modified: 05/18/2012 ,10/28/2013        
 --  Author:Liju ,Liju       
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
SELECT dbo.Questions.QID, dbo.Questions.QuestionID, dbo.Questions.QuestionType,    
 dbo.Questions.RemediationID, dbo.Remediation.TopicTitle, dbo.UserQuestions.QuestionNumber, dbo.Questions.TypeOfFileID,    
 dbo.UserQuestions.TimeSpendForQuestion,dbo.UserQuestions.TimeSpendForRemedation, dbo.UserQuestions.Correct,    
 dbo.Questions.LevelOfDifficultyID AS LevelOfDifficulty, dbo.Questions.NursingProcessID AS NursingProcess,    
 dbo.Questions.ClinicalConceptsID AS ClinicalConcept, dbo.Questions.DemographicID AS Demographic,    
 dbo.Questions.CriticalThinkingID AS CriticalThinking, dbo.Questions.SpecialtyAreaID AS SpecialtyArea,    
 dbo.Questions.SystemID AS Systems, dbo.Questions.CognitiveLevelID AS CognitiveLevel,    
 dbo.Questions.ClientNeedsID AS ClientNeeds, dbo.Questions.ClientNeedsCategoryID AS ClientNeedCategory,  
 dbo.Questions.AccreditationCategoriesID AS AccreditationCategories, dbo.Questions.QSENKSACompetenciesID AS QSENKSACompetencies ,
 dbo.Questions.ConceptsID AS Concepts 
FROM  dbo.UserQuestions INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID    
   LEFT OUTER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID    
WHERE  (dbo.UserQuestions.UserTestID = @UserTestID) AND TypeOfFileID=@TypeOfFileID    
ORDER BY dbo.UserQuestions.QuestionNumber    
SET NOCOUNT OFF   
END   
GO

PRINT 'Finished creating PROCEDURE uspGetListOfItemsForTestForUserI'
GO 


