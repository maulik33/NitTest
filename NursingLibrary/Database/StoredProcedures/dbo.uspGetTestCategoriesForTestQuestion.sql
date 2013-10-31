SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestCategoriesForTestQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestCategoriesForTestQuestion]
GO

CREATE PROCEDURE [dbo].[uspGetTestCategoriesForTestQuestion]
	@TestType int,
	@TestId int
AS
BEGIN
  SET NOCOUNT ON          
/*============================================================================================================        
 --   Purpose: Adding the newly added categories to get the details
 --   Modified: 05/09/2012        
 --   Author:Liju       
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
	 IF (@TestType = 1)--ClientNeeds
	 BEGIN
		  Select Distinct dbo.ClientNeeds.ClientNeeds as [Description], dbo.ClientNeeds.ClientNeedsID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.ClientNeeds
		  ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClientNeeds.ClientNeedsID
	 END
	 ELSE IF (@TestType = 2)--'NursingProcess'
	 BEGIN
		  Select Distinct dbo.NursingProcess.NursingProcess as [Description],dbo.NursingProcess.NursingProcessID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.NursingProcess
		  ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 3)--'CriticalThinking'
	 BEGIN
		  Select Distinct dbo.CriticalThinking.CriticalThinking as [Description],dbo.CriticalThinking.CriticalThinkingID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.CriticalThinking
		  ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.CriticalThinking.CriticalThinkingID
	 END
	 ELSE IF (@TestType = 4)--'ClinicalConcept'
	 BEGIN
		  Select Distinct dbo.ClinicalConcept.ClinicalConcept as [Description], ClinicalConcept.ClinicalConceptID as Id,ClinicalConcept.OrderNumber
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.ClinicalConcept
		  ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClinicalConcept.OrderNumber
	 END
	 ELSE IF (@TestType = 5)--'Demographic'
	 BEGIN
		  Select Distinct dbo.Demographic.Demographic as [Description],Demographic.DemographicID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.Demographic
		  ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END

	 ELSE IF (@TestType = 6)--'CognitiveLevel'
	 BEGIN
		 Select Distinct dbo.CognitiveLevel.CognitiveLevel as [Description],dbo.CognitiveLevel.CognitiveLevelID  as Id
		 FROM dbo.TestQuestions INNER JOIN dbo.Questions
		 ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.CognitiveLevel
		 ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
		 WHERE  dbo.TestQuestions.TestID=@TestId
		 ORDER BY dbo.CognitiveLevel.CognitiveLevelID
	 END
	 ELSE IF (@TestType = 7)--'SpecialtyArea'
	 BEGIN
		 Select Distinct dbo.SpecialtyArea.SpecialtyArea as [Description],dbo.SpecialtyArea.SpecialtyAreaID as Id
		 FROM dbo.TestQuestions INNER JOIN dbo.Questions
		 ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.SpecialtyArea
		 ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
		 WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 8)--'Systems'
	 BEGIN
		  Select Distinct dbo.Systems.System as [Description],Systems.SystemID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID   LEFT OUTER JOIN dbo.Systems
		  ON dbo.Questions.SystemID = dbo.Systems.SystemID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 9)--'LevelOfDifficulty'
	 BEGIN
		  Select Distinct dbo.LevelOfDifficulty.LevelOfDifficulty as [Description], dbo.LevelOfDifficulty.LevelOfDifficultyID as Id  ,LevelOfDifficulty.OrderNumber
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.LevelOfDifficulty
		  ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.LevelOfDifficulty.OrderNumber
	 END
	 ELSE IF (@TestType = 10)--'ClientNeedCategory'
	 BEGIN
		  Select Distinct ClientNeedCategory.ClientNeedCategory as [Description],dbo.ClientNeedCategory.ClientNeedCategoryID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.ClientNeedCategory
		  ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID
	 END
	 ELSE IF (@TestType = 11)--'AccreditationCategories'
	 BEGIN
		  Select Distinct dbo.AccreditationCategories.AccreditationCategories as [Description],dbo.AccreditationCategories.AccreditationCategoriesID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.AccreditationCategories
		  ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.AccreditationCategories.AccreditationCategoriesID
	 END
	 ELSE IF (@TestType = 12)--'QSENKSACompetencies'
	 BEGIN
		  Select Distinct dbo.QSENKSACompetencies.QSENKSACompetencies as [Description],dbo.QSENKSACompetencies.QSENKSACompetenciesID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.QSENKSACompetencies
		  ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.QSENKSACompetencies.QSENKSACompetenciesID
	 END
	
END
GO
PRINT 'Finished creating PROCEDURE uspCreateSkillsModulesDetails'
GO 




