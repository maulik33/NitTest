SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProgramResultsByNorm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetProgramResultsByNorm]
GO

PRINT 'Creating PROCEDURE uspGetProgramResultsByNorm'
GO

CREATE PROCEDURE [dbo].[uspGetProgramResultsByNorm]   
 @UserTestID int, 
 @testId int    
AS    
BEGIN              
SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose: To include the new categories to comeup in the chart
 --  Purpose: To include the new category(oncepts) to comeup in the chart  
 --  Modified: 05/11/2012 ,10/24/2013         
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
 SET NOCOUNT ON;    
    
   CREATE TABLE #NormResults (    
Correct_N int,    
Total int,    
ItemText varchar(500),    
Norm real,    
ChartType nvarchar(50),    
UserTestID int    
    
 )    
    --LevelOfDifficulty    
    -- create temp table for Norming information    
 SELECT Norm.Norm, LevelOfDifficulty.LevelOfDifficultyID AS ID,Norm.ChartType INTO #lodNorm1    
 FROM Norm INNER JOIN LevelOfDifficulty ON    
  Norm.ChartID = LevelOfDifficulty.LevelOfDifficultyID    
 WHERE Norm.TestID = @testId AND Norm.ChartType = 'LevelOfDifficulty'    
    
  INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
 SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,    
  LevelOfDifficulty.LevelOfDifficulty as ItemText, B.Norm , ISNULL(B.ChartType,'LevelOfDifficulty') AS ChartType,UserQuestions.UserTestID    
 FROM UserQuestions INNER JOIN Questions ON UserQuestions.QID = Questions.QID    
  LEFT OUTER JOIN LevelOfDifficulty ON Questions.LevelOfDifficultyID = LevelOfDifficulty.LevelOfDifficultyID    
  LEFT OUTER JOIN #lodNorm1 B ON B.ID = Questions.LevelOfDifficultyID    
  WHERE (UserQuestions.UserTestID = @UserTestID)    
  GROUP BY LevelOfDifficulty.LevelOfDifficulty, LevelOfDifficulty.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID    
  ORDER BY LevelOfDifficulty.OrderNumber    
    
 DROP TABLE #lodNorm1    
--NursingProcess    
 SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID AS ID ,ChartType INTO #lodNorm2 FROM dbo.Norm INNER JOIN    
                dbo.NursingProcess ON    
                dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID    
                WHERE (dbo.Norm.TestID =  @testId )  and (ChartType='NursingProcess')    
    
             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText, B.Norm , ISNULL(B.ChartType,'NursingProcess') AS ChartType ,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID    
                LEFT OUTER JOIN #lodNorm2 B ON B.ID = dbo.Questions.NursingProcessID    
               WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.NursingProcess.NursingProcess,dbo.NursingProcess.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID    
                ORDER BY dbo.NursingProcess.OrderNumber    
    
       DROP TABLE #lodNorm2    
  --ClinicalConcept    
    SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID AS ID,ChartType  INTO #lodNorm3    
                FROM dbo.Norm INNER JOIN    
                dbo.ClinicalConcept ON    
                dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID    
                WHERE (dbo.Norm.TestID = @testId) and (ChartType= 'ClinicalConcept')    
    
       INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
     SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText, B.Norm ,ISNULL(B.ChartType,'ClinicalConcept') AS ChartType,UserQuestions.UserTestID    
                 FROM  dbo.UserQuestions INNER JOIN    
     dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                 dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID    
                 LEFT OUTER JOIN #lodNorm3 B ON B.ID = dbo.Questions.ClinicalConceptsID    
                WHERE (UserQuestions.UserTestID = @UserTestID)    
                 GROUP BY dbo.ClinicalConcept.ClinicalConcept,dbo.ClinicalConcept.OrderNumber, B.Norm ,B.ChartType,UserQuestions.UserTestID    
                 ORDER BY dbo.ClinicalConcept.OrderNumber    
    
      DROP TABLE #lodNorm3    
    
--ClientNeeds    
       SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID AS ID,ChartType INTO #lodNorm4    
                FROM dbo.Norm INNER JOIN    
                dbo.ClientNeeds ON  dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID    
                WHERE (dbo.Norm.TestID = @testId)  and (ChartType='ClientNeeds' )    
    
          INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
   SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText, B.Norm ,ISNULL(B.ChartType,'ClientNeeds') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID    
                 LEFT OUTER JOIN #lodNorm4 B ON B.ID = dbo.Questions.ClientNeedsID    
               WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,B.Norm ,B.ChartType,UserQuestions.UserTestID    
                ORDER BY dbo.ClientNeeds.ClientNeedsID    
    
DROP TABLE #lodNorm4    
    
--ClientNeedCategory    
    
   SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID AS ID,ChartType INTO #lodNorm5    
                FROM dbo.Norm INNER JOIN    
                dbo.ClientNeedCategory ON    
                dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID    
                WHERE (dbo.Norm.TestID =@testId)  and (ChartType= 'ClientNeedCategory')    
    
             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
   SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText, B.Norm ,ISNULL(B.ChartType,'ClientNeedCategory') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID    
                 LEFT OUTER JOIN #lodNorm5 B ON B.ID = dbo.Questions.ClientNeedsCategoryID    
               WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,B.Norm,B.ChartType,UserQuestions.UserTestID    
                ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID    
    
DROP TABLE #lodNorm5    
--Demographic    
      SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID AS ID ,ChartType INTO #lodNorm6    
                FROM dbo.Norm INNER JOIN    
                dbo.Demographic ON    
                dbo.Norm.ChartID = dbo.Demographic.DemographicID    
              WHERE Norm.TestID = @testId AND Norm.ChartType = 'Demographic'    
    
             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Demographic.Demographic as ItemText, B.Norm ,ISNULL(B.ChartType,'Demographic') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID    
              LEFT OUTER JOIN #lodNorm6 B ON B.ID = dbo.Questions.DemographicID    
                WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.Demographic.Demographic,dbo.Demographic.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID    
                ORDER BY dbo.Demographic.OrderNumber    
    
DROP TABLE #lodNorm6    
--CognitiveLevel    
 SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID AS ID ,ChartType INTO #lodNorm7    
                FROM dbo.Norm INNER JOIN    
                dbo.CognitiveLevel ON    
                dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID    
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'CognitiveLevel'    
    
            INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
   SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText, B.Norm ,ISNULL(B.ChartType,'CognitiveLevel') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID    
                LEFT OUTER JOIN #lodNorm7 B ON B.ID = dbo.Questions.CognitiveLevelID    
               WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,B.Norm,B.ChartType,UserQuestions.UserTestID    
                ORDER BY dbo.CognitiveLevel.CognitiveLevelID    
DROP TABLE #lodNorm7    
    
--CriticalThinking    
    
  SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID AS ID ,ChartType INTO #lodNorm8    
                FROM dbo.Norm INNER JOIN    
                dbo.CriticalThinking ON    
                dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID    
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'CriticalThinking'    
    
              INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText, B.Norm ,ISNULL(B.ChartType,'CriticalThinking') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID    
                LEFT OUTER JOIN #lodNorm8 B ON B.ID = dbo.Questions.CriticalThinkingID    
             WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.CriticalThinking.CriticalThinking, B.Norm,B.ChartType,UserQuestions.UserTestID    
    
DROP TABLE #lodNorm8    
    
--SpecialtyArea    
    
 SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID AS ID ,ChartType INTO #lodNorm9    
                FROM dbo.Norm INNER JOIN    
                dbo.SpecialtyArea ON    
                dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID    
               WHERE Norm.TestID = @testId AND Norm.ChartType = 'SpecialtyArea'    
    
               INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText, B.Norm ,ISNULL(B.ChartType,'SpecialtyArea') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID    
                 LEFT OUTER JOIN #lodNorm9 B ON B.ID = dbo.Questions.SpecialtyAreaID    
              WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.SpecialtyArea.SpecialtyArea, B.Norm  ,B.ChartType,UserQuestions.UserTestID    
DROP TABLE #lodNorm9    
--System    
    
 SELECT dbo.Norm.Norm, dbo.Systems.SystemID  AS ID ,ChartType INTO #lodNorm10    
                FROM dbo.Norm INNER JOIN    
                dbo.Systems ON    
                dbo.Norm.ChartID = dbo.Systems.SystemID    
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'Systems'    
    
 INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
            SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,    
    dbo.Systems.System as ItemText,    
    B.Norm ,ISNULL(B.ChartType,'System') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID    
                LEFT OUTER JOIN #lodNorm10 B ON B.ID = dbo.Questions.SystemID    
              WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.Systems.System, B.Norm,B.ChartType,UserQuestions.UserTestID    
    
DROP TABLE #lodNorm10    
--AccreditationCategories    
    
 SELECT dbo.Norm.Norm, dbo.AccreditationCategories.AccreditationCategoriesID AS ID ,ChartType INTO #lodNorm11    
                FROM dbo.Norm INNER JOIN    
                dbo.AccreditationCategories ON    
                dbo.Norm.ChartID = dbo.AccreditationCategories.AccreditationCategoriesID    
               WHERE Norm.TestID = @testId AND Norm.ChartType = 'AccreditationCategories'    
    
               INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.AccreditationCategories.AccreditationCategories as ItemText, B.Norm ,ISNULL(B.ChartType,'AccreditationCategories') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.AccreditationCategories ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID    
                 LEFT OUTER JOIN #lodNorm11 B ON B.ID = dbo.Questions.AccreditationCategoriesID    
              WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.AccreditationCategories.AccreditationCategories, B.Norm  ,B.ChartType,UserQuestions.UserTestID    
DROP TABLE #lodNorm11  
  
--QSENKSACompetencies    
    
 SELECT dbo.Norm.Norm, dbo.QSENKSACompetencies.QSENKSACompetenciesID AS ID ,ChartType INTO #lodNorm12    
                FROM dbo.Norm INNER JOIN    
                dbo.QSENKSACompetencies ON    
                dbo.Norm.ChartID = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
               WHERE Norm.TestID = @testId AND Norm.ChartType = 'QSENKSACompetencies'    
    
               INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.QSENKSACompetencies.QSENKSACompetencies as ItemText, B.Norm ,ISNULL(B.ChartType,'QSENKSACompetencies') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.QSENKSACompetencies ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
                 LEFT OUTER JOIN #lodNorm12 B ON B.ID = dbo.Questions.QSENKSACompetenciesID    
              WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.QSENKSACompetencies.QSENKSACompetencies, B.Norm  ,B.ChartType,UserQuestions.UserTestID    
DROP TABLE #lodNorm12  

--Concepts    
    
 SELECT dbo.Norm.Norm, dbo.Concepts.ConceptsID AS ID ,ChartType INTO #lodNorm13    
                FROM dbo.Norm INNER JOIN    
                dbo.Concepts ON    
                dbo.Norm.ChartID = dbo.Concepts.ConceptsID    
               WHERE Norm.TestID = @testId AND Norm.ChartType = 'Concepts'    
    
               INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )    
    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Concepts.Concepts as ItemText, B.Norm ,ISNULL(B.ChartType,'Concepts') AS ChartType,UserQuestions.UserTestID    
                FROM  dbo.UserQuestions INNER JOIN    
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
                dbo.Concepts ON dbo.Questions.ConceptsID = dbo.Concepts.ConceptsID    
                 LEFT OUTER JOIN #lodNorm13 B ON B.ID = dbo.Questions.ConceptsID    
              WHERE (UserQuestions.UserTestID = @UserTestID)    
                GROUP BY dbo.Concepts.Concepts, B.Norm  ,B.ChartType,UserQuestions.UserTestID    
DROP TABLE #lodNorm13  
  
       Select * from #NormResults    
       Drop TABLE  #NormResults    
    
SET NOCOUNT OFF   
END 
GO
PRINT 'Finished creating PROCEDURE uspGetProgramResultsByNorm'
GO 



