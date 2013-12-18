SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationTimeByTestId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetRemediationTimeByTestId]
GO


PRINT 'Creating PROCEDURE uspGetRemediationTimeByTestId'
GO
    
CREATE PROCEDURE [dbo].[uspGetRemediationTimeByTestId]      
@UserTestID INT,      
@TypeOfFileID CHAR(2)      
AS      
BEGIN              
   SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose: To include the details of the newly added categories  
 --  Modified: To include the newly added category (Concepts)
 --  Modified: 05/17/2012 , 10/28/2013         
 --  Author:Liju         
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
       
 SELECT TOP 100 PERCENT dbo.Questions.ClientNeedsCategoryID, dbo.Questions.QID, dbo.Questions.QuestionID, dbo.Questions.QuestionType,      
    dbo.Questions.RemediationID, dbo.Remediation.TopicTitle, dbo.UserQuestions.QuestionNumber, dbo.Questions.TypeOfFileID,      
    dbo.LevelOfDifficulty.LevelOfDifficulty, dbo.NursingProcess.NursingProcess, dbo.ClinicalConcept.ClinicalConcept, dbo.Questions.SpecialtyAreaID,      
    dbo.Demographic.Demographic, dbo.NursingProcess.NursingProcessID, dbo.UserQuestions.TimeSpendForQuestion, dbo.UserQuestions.TimeSpendForExplanation,      
    dbo.UserQuestions.Correct,      
    dbo.Tests.TestName, dbo.UserQuestions.TimeSpendForRemedation, dbo.ClientNeeds.ClientNeeds, dbo.Questions.ClientNeedsID,      
    dbo.Questions.ClinicalConceptsID, dbo.Questions.LevelOfDifficultyID, dbo.Questions.CriticalThinkingID, dbo.CriticalThinking.CriticalThinking,      
    dbo.Questions.CognitiveLevelID, dbo.CognitiveLevel.CognitiveLevel, dbo.SpecialtyArea.SpecialtyArea, dbo.Questions.SystemID, dbo.Systems.[System],      
    dbo.Questions.AccreditationCategoriesID,dbo.Questions.QSENKSACompetenciesID,    
    dbo.AccreditationCategories.AccreditationCategories,dbo.QSENKSACompetencies.QSENKSACompetencies,  
    dbo.ClientNeedCategory.ClientNeedCategory,dbo.Concepts.Concepts      
    FROM  dbo.UserQuestions INNER JOIN      
    dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN      
    dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN      
    dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN      
    dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID LEFT OUTER JOIN      
    dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID LEFT OUTER JOIN      
    dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID LEFT OUTER JOIN      
    dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID LEFT OUTER JOIN      
    dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID LEFT OUTER JOIN      
    dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID LEFT OUTER JOIN      
    dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID LEFT OUTER JOIN      
    dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID LEFT OUTER JOIN      
    dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID LEFT OUTER JOIN      
    dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID LEFT OUTER JOIN      
    dbo.AccreditationCategories ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID LEFT OUTER JOIN      
    dbo.QSENKSACompetencies ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID LEFT OUTER JOIN      
    dbo.Concepts ON dbo.Questions.ConceptsID = dbo.Concepts.ConceptsID LEFT OUTER JOIN 
    dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID      
    WHERE     (dbo.UserQuestions.UserTestID = @UserTestID) AND (dbo.Questions.TypeOfFileID = @TypeOfFileID)      
    ORDER BY dbo.UserQuestions.QuestionNumber      
       
SET NOCOUNT OFF   
END   
GO
PRINT 'Finished creating PROCEDURE uspGetRemediationTimeByTestId'
GO 


