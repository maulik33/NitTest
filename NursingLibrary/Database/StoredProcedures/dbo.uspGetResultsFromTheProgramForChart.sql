SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheProgramForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheProgramForChart]
GO

PRINT 'Creating PROCEDURE uspGetResultsFromTheProgramForChart'
GO

CREATE PROCEDURE [dbo].[uspGetResultsFromTheProgramForChart]        
@UserTestID INT,        
@ChartType VARCHAR(30)      
AS      
BEGIN                
SET NOCOUNT ON              
/*============================================================================================================            
 --  Purpose: To include the details of the newly added Categories    
 --  Modified: 05/17/2012 ,10/24/2013          
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
 DECLARE @TestId INT        
         
 SELECT @TestId = TestID        
 FROM UserTests        
 WHERE UserTestID = @UserTestId        
         
 IF (@ChartType = 'LevelOfDifficulty')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText, B.Norm        
        FROM  dbo.UserQuestions INNER JOIN        
        dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
        dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID        
        LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.LevelOfDifficulty.LevelOfDifficultyID FROM dbo.Norm INNER JOIN        
        dbo.LevelOfDifficulty ON  dbo.Norm.ChartID = dbo.LevelOfDifficulty.LevelOfDifficultyID        
   WHERE (dbo.Norm.TestID = @TestId ) and (ChartType= @ChartType)) B ON B.LevelOfDifficultyID = dbo.Questions.LevelOfDifficultyID        
        WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)        
        GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty,dbo.LevelOfDifficulty.OrderNumber, B.Norm        
        ORDER BY dbo.LevelOfDifficulty.OrderNumber        
        
 END        
 ELSE IF (@ChartType = 'NursingProcess')        
 BEGIN        
  SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID        
  FROM dbo.Norm INNER JOIN        
  dbo.NursingProcess ON        
  dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID        
  WHERE (dbo.Norm.TestID =  @TestId )  and (ChartType = @ChartType )) B ON B.NursingProcessID = dbo.Questions.NursingProcessID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestId )        
  GROUP BY dbo.NursingProcess.NursingProcess,dbo.NursingProcess.OrderNumber, B.Norm        
  ORDER BY dbo.NursingProcess.OrderNumber        
 END        
 ELSE IF (@ChartType = 'ClinicalConcept')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID       
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID        
  FROM dbo.Norm INNER JOIN        
  dbo.ClinicalConcept ON        
  dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID        
  WHERE (dbo.Norm.TestID =  @TestId ) and (ChartType = @ChartType)) B ON B.ClinicalConceptID = dbo.Questions.ClinicalConceptsID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.ClinicalConcept.ClinicalConcept,dbo.ClinicalConcept.OrderNumber, B.Norm        
  ORDER BY dbo.ClinicalConcept.OrderNumber        
 END        
 ELSE IF (@ChartType = 'ClientNeeds')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID        
  FROM dbo.Norm INNER JOIN        
  dbo.ClientNeeds ON        
  dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID        
  WHERE (dbo.Norm.TestID = @TestId )  and (ChartType = @ChartType) ) B ON B.ClientNeedsID = dbo.Questions.ClientNeedsID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,B.Norm        
  ORDER BY dbo.ClientNeeds.ClientNeedsID        
 END        
 ELSE IF (@ChartType = 'ClientNeedCategory')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID        
  FROM dbo.Norm INNER JOIN        
  dbo.ClientNeedCategory ON        
  dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID        
  WHERE (dbo.Norm.TestID =  @TestId)  and (ChartType=@ChartType)) B ON B.ClientNeedCategoryID = dbo.Questions.ClientNeedsCategoryID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,B.Norm        
  ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID        
 END        
 ELSE IF (@ChartType = 'Demographic')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Demographic.Demographic as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID        
  FROM dbo.Norm INNER JOIN        
  dbo.Demographic ON        
  dbo.Norm.ChartID = dbo.Demographic.DemographicID        
  WHERE (dbo.Norm.TestID =  @TestId)  and (ChartType=@ChartType)) B ON B.DemographicID = dbo.Questions.DemographicID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.Demographic.Demographic,dbo.Demographic.OrderNumber, B.Norm        
  ORDER BY dbo.Demographic.OrderNumber        
 END        
 ELSE IF (@ChartType = 'CognitiveLevel')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID        
  FROM dbo.Norm INNER JOIN        
  dbo.CognitiveLevel ON        
  dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType= @ChartType)) B ON B.CognitiveLevelID = dbo.Questions.CognitiveLevelID        
  WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)        
  GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,B.Norm        
  ORDER BY dbo.CognitiveLevel.CognitiveLevelID        
 END        
 ELSE IF (@ChartType = 'CriticalThinking')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID        
  FROM dbo.Norm INNER JOIN  dbo.CriticalThinking ON dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.CriticalThinkingID = dbo.Questions.CriticalThinkingID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.CriticalThinking.CriticalThinking, B.Norm        
 END        
 ELSE IF (@ChartType = 'SpecialtyArea')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID        
  FROM dbo.Norm INNER JOIN dbo.SpecialtyArea ON dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType= @ChartType)) B ON B.SpecialtyAreaID = dbo.Questions.SpecialtyAreaID        
  WHERE     (dbo.UserQuestions.UserTestID =@UserTestID)        
  GROUP BY dbo.SpecialtyArea.SpecialtyArea, B.Norm        
 END        
 ELSE IF (@ChartType = 'Systems')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Systems.System as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Systems.SystemID        
  FROM dbo.Norm INNER JOIN dbo.Systems ON dbo.Norm.ChartID = dbo.Systems.SystemID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.SystemID = dbo.Questions.SystemID        
  WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)        
  GROUP BY dbo.Systems.System, B.Norm        
 END     
 ELSE IF (@ChartType = 'AccreditationCategories')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.AccreditationCategories.AccreditationCategories as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.AccreditationCategories ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.AccreditationCategories.AccreditationCategoriesID        
  FROM dbo.Norm INNER JOIN  dbo.AccreditationCategories ON dbo.Norm.ChartID = dbo.AccreditationCategories.AccreditationCategoriesID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.AccreditationCategoriesID = dbo.Questions.AccreditationCategoriesID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.AccreditationCategories.AccreditationCategories, B.Norm        
 END      
  ELSE IF (@ChartType = 'QSENKSACompetencies')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.QSENKSACompetencies.QSENKSACompetencies as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.QSENKSACompetencies ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.QSENKSACompetencies.QSENKSACompetenciesID        
  FROM dbo.Norm INNER JOIN dbo.QSENKSACompetencies ON dbo.Norm.ChartID = dbo.QSENKSACompetencies.QSENKSACompetenciesID        
  WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.QSENKSACompetenciesID = dbo.Questions.QSENKSACompetenciesID        
  WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)        
  GROUP BY dbo.QSENKSACompetencies.QSENKSACompetencies, B.Norm        
 END      
  ELSE IF (@ChartType = 'Concepts')        
 BEGIN        
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Concepts.Concepts as ItemText, B.Norm        
  FROM  dbo.UserQuestions INNER JOIN        
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN        
  dbo.Concepts ON dbo.Questions.ConceptsID = dbo.Concepts.ConceptsID        
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Concepts.ConceptsID        
  FROM dbo.Norm INNER JOIN        
  dbo.Concepts ON        
  dbo.Norm.ChartID = dbo.Concepts.ConceptsID        
  WHERE (dbo.Norm.TestID = @TestId )  and (ChartType = @ChartType) ) B ON B.ConceptsID = dbo.Questions.ConceptsID        
  WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)        
  GROUP BY dbo.Concepts.Concepts,dbo.Concepts.ConceptsID,B.Norm        
  ORDER BY dbo.Concepts.ConceptsID   
 END           
 SET NOCOUNT OFF     
END  
GO

PRINT 'Finished creating PROCEDURE uspGetResultsFromTheProgramForChart'
GO 



