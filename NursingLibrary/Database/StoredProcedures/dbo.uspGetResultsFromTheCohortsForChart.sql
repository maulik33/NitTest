SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheCohortsForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheCohortsForChart]
GO

PRINT 'Creating PROCEDURE uspGetResultsFromTheCohortsForChart'
GO 
  
CREATE PROCEDURE [dbo].[uspGetResultsFromTheCohortsForChart]    
@InstitutionId int,    
@SubCategoryId INT,    
@ChartType int,    
@ProductIds nVarchar(1000),    
@Tests nVarchar(1000),    
@CohortIds nVarchar(1000)    
AS    
BEGIN              
SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose:  To get the results of the completed tests  
 --  Modified For: Sprint 47: Modified to check PN ChartTypes (Main Categories)   
 --     for resolving NURSING-4197 (Issue raised against NURSING-3615 -   
 --     PN/RN Unification changes on Category Comparison Report) 
 --  Modified For: Sprint 55: Modified to include newly added ChartTypes (Concepts)   
 --  Modified Date: 05/23/2012, 07/08/2013 ,10/28/2013         
 --  Author:  Liju, Atul ,Liju 
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
    
 IF (@ChartType = 1 OR @ChartType = 13)--ClientNeeds    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClientNeeds.ClientNeedsID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.ClientNeeds    
        ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID    
        WHERE dbo.ClientNeeds.ClientNeedsID=@subCategoryId   
        AND TestStatus = 1   
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClientNeeds.ClientNeedsID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
    
 END    
 ELSE IF (@ChartType = 2 OR @ChartType = 14)--'NursingProcess'    
 BEGIN    
    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.NursingProcess.NursingProcessID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID    
        WHERE dbo.NursingProcess.NursingProcessID=@subCategoryId   
        AND TestStatus = 1   
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.NursingProcess.NursingProcessID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
 ELSE IF (@ChartType = 3 OR @ChartType = 15)--'CriticalThinking'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.CriticalThinking.CriticalThinkingID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID    
        WHERE dbo.CriticalThinking.CriticalThinkingID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.CriticalThinking.CriticalThinkingID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
    
 END    
 ELSE IF (@ChartType = 4 OR @ChartType = 16)--'ClinicalConcept'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClinicalConcept.ClinicalConceptID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID    
        WHERE dbo.ClinicalConcept.ClinicalConceptID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClinicalConcept.ClinicalConceptID ) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
    
 END    
 ELSE IF (@ChartType = 5 OR @ChartType = 17)--'Demographic'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.Demographic.DemographicID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID    
        WHERE dbo.Demographic.DemographicID=@subCategoryId    
        AND TestStatus = 1  
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.Demographic.DemographicID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
    
 ELSE IF (@ChartType = 6 OR @ChartType = 18)--'CognitiveLevel'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.CognitiveLevel.CognitiveLevelID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID    
        WHERE dbo.CognitiveLevel.CognitiveLevelID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.CognitiveLevel.CognitiveLevelID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
    
 END    
 ELSE IF (@ChartType = 7 OR @ChartType = 19)--'SpecialtyArea'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.SpecialtyArea.SpecialtyAreaID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests            ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID    
        WHERE dbo.SpecialtyArea.SpecialtyAreaID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.SpecialtyArea.SpecialtyAreaID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
 ELSE IF (@ChartType = 8 OR @ChartType = 20)--'Systems'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.Systems.SystemID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID    
        WHERE dbo.Systems.SystemID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.Systems.SystemID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
 ELSE IF (@ChartType = 9 OR @ChartType = 21)--'LevelOfDifficulty'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.LevelOfDifficulty.LevelOfDifficultyID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  INNER JOIN dbo.LevelOfDifficulty    
        ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID    
        WHERE TestStatus = 1 AND dbo.LevelOfDifficulty.LevelOfDifficultyID =@SubCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.LevelOfDifficulty.LevelOfDifficultyID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
 ELSE IF (@ChartType = 10 OR @ChartType = 22)--'ClientNeedCategory'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClientNeedCategory.ClientNeedCategoryID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID    
        WHERE dbo.ClientNeedCategory.ClientNeedCategoryID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClientNeedCategory.ClientNeedCategoryID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
 ELSE IF (@ChartType = 11)--'AccreditationCategories'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.AccreditationCategories.AccreditationCategoriesID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.AccreditationCategories ON dbo.Questions.AccreditationCategoriesID = dbo.AccreditationCategories.AccreditationCategoriesID    
        WHERE dbo.AccreditationCategories.AccreditationCategoriesID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.AccreditationCategories.AccreditationCategoriesID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END    
  ELSE IF (@ChartType = 12)--'QSENKSACompetencies'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.QSENKSACompetencies.QSENKSACompetenciesID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
        INNER JOIN dbo.QSENKSACompetencies ON dbo.Questions.QSENKSACompetenciesID = dbo.QSENKSACompetencies.QSENKSACompetenciesID    
        WHERE dbo.QSENKSACompetencies.QSENKSACompetenciesID=@subCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.QSENKSACompetencies.QSENKSACompetenciesID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END   
 ELSE IF (@ChartType = 23 OR @ChartType = 24)--'Concepts'    
 BEGIN    
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM    
        dbo.NurCohort LEFT OUTER JOIN    
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,    
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.Concepts.ConceptsID as Id    
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests    
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests    
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions    
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign    
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort    
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  INNER JOIN dbo.Concepts    
        ON dbo.Questions.ConceptsID = dbo.Concepts.ConceptsID    
        WHERE TestStatus = 1 AND dbo.Concepts.ConceptsID =@SubCategoryId    
        AND TestStatus = 1   
  AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))    
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))    
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.Concepts.ConceptsID) AS V    
        ON dbo.NurCohort.CohortID = V.CohortID    
        WHERE NurCohort.InstitutionID=@InstitutionId AND    
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))    
 END     
SET NOCOUNT OFF   
END   
GO

PRINT 'Finished creating PROCEDURE uspGetResultsFromTheCohortsForChart'
GO 



