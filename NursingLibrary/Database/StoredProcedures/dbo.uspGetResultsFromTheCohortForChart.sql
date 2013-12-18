SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheCohortForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheCohortForChart]
GO

PRINT 'Creating PROCEDURE uspGetResultsFromTheCohortForChart'
GO 
  
CREATE PROCEDURE [dbo].[uspGetResultsFromTheCohortForChart]    
@CohortIds VARCHAR(MAX),    
@TestTypeIds VARCHAR(MAX),    
@TestIds VARCHAR(MAX),    
@ChartType VARCHAR(50),    
@FromDate varchar(10),    
@ToDate VARCHAR(10)    
AS    
BEGIN              
SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose: To pull the details that match with the category comparisons  
 --  Purpose: To include the newly added category 'concepts'
 --  Modified: 05/23/2012 ,10/28/2013         
 --  Author:Liju,Liju         
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
  IF (@ChartType = 'LevelOfDifficulty')  
 BEGIN  
  SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,  
  SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText,  
  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID  
  LEFT OUTER JOIN dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.Category = 15  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.LevelOfDifficulty.LevelOfDifficultyID  
  FROM dbo.Norm  
  INNER JOIN dbo.LevelOfDifficulty ON dbo.Norm.ChartID = dbo.LevelOfDifficulty.LevelOfDifficultyID  
  WHERE dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  and (ChartType=@ChartType) ) B ON B.LevelOfDifficultyID = dbo.Questions.LevelOfDifficultyID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty,  dbo.CategoryPercentage.Percentage,dbo.LevelOfDifficulty.OrderNumber, B.Norm   
 END  
 ELSE IF (@ChartType = 'NursingProcess')  
 BEGIN  
  SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,  
  SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText,  
  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID  
  AND dbo.CategoryPercentage.Category = 16  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID   
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID  
  FROM dbo.Norm  
  INNER JOIN dbo.NursingProcess ON dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  
  and (ChartType=@ChartType)) B  
  ON B.NursingProcessID = dbo.Questions.NursingProcessID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.NursingProcess.NursingProcess, dbo.CategoryPercentage.Percentage,dbo.NursingProcess.OrderNumber, B.Norm  
 END  
 ELSE IF (@ChartType = 'ClinicalConcept')  
 BEGIN  
  SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,  
  SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText,  
  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM         dbo.ClinicalConcept INNER JOIN  
  dbo.UserQuestions INNER JOIN  
  dbo.UserTests ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN  
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID ON  
  dbo.ClinicalConcept.ClinicalConceptID = dbo.Questions.ClinicalConceptsID LEFT OUTER JOIN  
  dbo.CategoryPercentage ON dbo.UserTests.TestID = dbo.CategoryPercentage.TestID AND  
  dbo.ClinicalConcept.ClinicalConceptID = dbo.CategoryPercentage.CategoryFields AND dbo.CategoryPercentage.Category = 4  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID  
  FROM dbo.Norm INNER JOIN  
  dbo.ClinicalConcept ON  
  dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')) )  
  and (ChartType=@ChartType)) B ON B.ClinicalConceptID = dbo.Questions.ClinicalConceptsID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.ClinicalConcept.ClinicalConcept, dbo.CategoryPercentage.Percentage,dbo.ClinicalConcept.OrderNumber, B.Norm  
 END  
 ELSE IF (@ChartType = 'ClientNeeds')  
 BEGIN  
  SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,  
  dbo.ClientNeeds.ClientNeeds as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeeds.ClientNeedsID  
  AND dbo.CategoryPercentage.Category = 1  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID  
  FROM dbo.Norm  
  INNER JOIN dbo.ClientNeeds ON dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  
  and (ChartType=@ChartType)) B ON B.ClientNeedsID = dbo.Questions.ClientNeedsID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,dbo.CategoryPercentage.Percentage, B.Norm  
 END  
 ELSE IF (@ChartType = 'ClientNeedCategory')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,  
  dbo.ClientNeedCategory.ClientNeedCategory as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID  
  AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeedCategory.ClientNeedCategoryID  
  AND dbo.CategoryPercentage.Category = 18  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID  
  FROM dbo.Norm  
  INNER JOIN  dbo.ClientNeedCategory ON dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  
  and (ChartType= @ChartType)) B ON B.ClientNeedCategoryID = dbo.Questions.ClientNeedsCategoryID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,  
  dbo.CategoryPercentage.Percentage, B.Norm  
 END  
 ELSE IF (@ChartType = 'Demographic')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,  
  dbo.Demographic.Demographic as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID  
  AND dbo.CategoryPercentage.CategoryFields = dbo.Demographic.DemographicID  
  AND dbo.CategoryPercentage.Category = 6  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID  
  FROM dbo.Norm  
  INNER JOIN dbo.Demographic ON dbo.Norm.ChartID = dbo.Demographic.DemographicID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  
  and (ChartType=@ChartType)) B ON B.DemographicID = dbo.Questions.DemographicID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.Demographic.Demographic, dbo.CategoryPercentage.Percentage,dbo.Demographic.OrderNumber, B.Norm  
 END  
 ELSE IF (@ChartType = 'CognitiveLevel')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CognitiveLevel.CognitiveLevelID  
  AND dbo.CategoryPercentage.Category = 17  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID  
  FROM dbo.Norm INNER JOIN dbo.CognitiveLevel ON dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  
  and (ChartType= @ChartType)) B ON B.CognitiveLevelID = dbo.Questions.CognitiveLevelID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,dbo.CategoryPercentage.Percentage, B.Norm  
 END  
 ELSE IF (@ChartType = 'CriticalThinking')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CriticalThinking.CriticalThinkingID  
  AND dbo.CategoryPercentage.Category = 5   
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID  
  FROM dbo.Norm INNER JOIN  
  dbo.CriticalThinking ON  
  dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.CriticalThinkingID = dbo.Questions.CriticalThinkingID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.CriticalThinking.CriticalThinking, dbo.CategoryPercentage.Percentage, B.Norm                   
 END  
 ELSE IF (@ChartType = 'SpecialtyArea')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.SpecialtyArea.SpecialtyAreaID  
  AND dbo.CategoryPercentage.Category = 11  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID  
  FROM dbo.Norm INNER JOIN  
  dbo.SpecialtyArea ON  
  dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.SpecialtyAreaID = dbo.Questions.SpecialtyAreaID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.SpecialtyArea.SpecialtyArea, dbo.CategoryPercentage.Percentage, B.Norm  
 END  
 ELSE IF (@ChartType = 'Systems')  
 BEGIN  
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Systems.System as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm  
  FROM  dbo.UserQuestions  
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID  
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN  
  dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID  
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.Systems.SystemID  
  AND dbo.CategoryPercentage.Category = 12  
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Systems.SystemID  
  FROM dbo.Norm INNER JOIN  
  dbo.Systems ON  
  dbo.Norm.ChartID = dbo.Systems.SystemID  
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.SystemID = dbo.Questions.SystemID  
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))  
  AND TestStatus = 1  
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))  
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0  
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))  
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0  
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))  
  GROUP BY dbo.Systems.System, dbo.CategoryPercentage.Percentage, B.Norm  
 END  
 ELSE IF (@ChartType = 'AccreditationCategories')    
 BEGIN    
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.AccreditationCategories.AccreditationCategories as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm    
  FROM  dbo.UserQuestions    
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID    
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
  dbo.AccreditationCategories ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID    
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.AccreditationCategories.AccreditationCategoriesID    
  AND dbo.CategoryPercentage.Category = 5     
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.AccreditationCategories.AccreditationCategoriesID    
  FROM dbo.Norm INNER JOIN    
  dbo.AccreditationCategories ON    
  dbo.Norm.ChartID = dbo.AccreditationCategories.AccreditationCategoriesID    
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.AccreditationCategoriesID = dbo.Questions.AccreditationCategoriesID    
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))    
  AND TestStatus = 1   
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))    
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))    
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0    
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))    
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0    
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))    
  GROUP BY dbo.AccreditationCategories.AccreditationCategories, dbo.CategoryPercentage.Percentage, B.Norm                     
 END    
 ELSE IF (@ChartType = 'QSENKSACompetencies')    
 BEGIN    
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.QSENKSACompetencies.QSENKSACompetencies as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm    
  FROM  dbo.UserQuestions    
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID    
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
  dbo.QSENKSACompetencies ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
  AND dbo.CategoryPercentage.Category = 5     
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.QSENKSACompetencies.QSENKSACompetenciesID    
  FROM dbo.Norm INNER JOIN    
  dbo.QSENKSACompetencies ON    
  dbo.Norm.ChartID = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.QSENKSACompetenciesID = dbo.Questions.QSENKSACompetenciesID    
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))    
  AND TestStatus = 1    
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))    
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))    
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0    
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))    
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0    
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))    
  GROUP BY dbo.QSENKSACompetencies.QSENKSACompetencies, dbo.CategoryPercentage.Percentage, B.Norm                     
 END    
 ELSE IF (@ChartType = 'Concepts')    
 BEGIN    
  SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Concepts.Concepts as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm    
  FROM  dbo.UserQuestions    
  INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID    
  INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN    
  dbo.Concepts ON dbo.Questions.ConceptsID = dbo.Concepts.ConceptsID    
  LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.Concepts.ConceptsID    
  AND dbo.CategoryPercentage.Category = 5     
  LEFT JOIN dbo.NusStudentAssign ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   
  INNER JOIN dbo.NurCohort ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
  LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Concepts.ConceptsID    
  FROM dbo.Norm INNER JOIN    
  dbo.Concepts ON    
  dbo.Norm.ChartID = dbo.Concepts.ConceptsID    
  WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.ConceptsID = dbo.Questions.ConceptsID    
  WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))    
  AND TestStatus = 1    
  AND dbo.NurCohort.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))    
  AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))    
  AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0    
  AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))    
  AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0    
  AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))    
  GROUP BY dbo.Concepts.Concepts, dbo.CategoryPercentage.Percentage, B.Norm                     
 END    
SET NOCOUNT OFF   
END
GO

PRINT 'Finished creating PROCEDURE uspGetResultsFromTheCohortForChart'
GO 



