SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfQuestionsShowUnique]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfQuestionsShowUnique]
GO

PRINT 'Creating PROCEDURE uspGetListOfQuestionsShowUnique'
GO
  
CREATE PROCEDURE [dbo].[uspGetListOfQuestionsShowUnique]    
 @ProductID as int,    
 @TestID as int,    
 @ClientNeedID as int,    
 @ClientNeedsCategoryID as int,    
 @ClinicalConceptID as int,    
 @CognitiveLevelID as int,    
 @CriticalThinkingID as int,    
 @DemographicID as int,    
 @LevelOfDifficultyID as int,    
 @NursingProcessID as int,    
 @RemediationID as int,    
 @SpecialtyAreaID as int,    
 @SystemID as int,    
 @QuestionID as varchar(100),    
 @TypeOfFileID as varchar(10),    
 @QuestionType as char(2),    
 @Text as varchar(max),    
 @ReleaseStatus as bit,    
 @Active as bit,    
 @AccreditationCategoriesID as int,    
 @QSENKSACompetenciesID as int,    
 @ProgramOfStudy as int = 1,  
 @ConceptsID as int    
AS    
BEGIN    
 SET NOCOUNT ON;    
 /*============================================================================================================            
 --     Purpose: Resolution for NURSING-1645 ,To Include the new category Concepts  
 --     Modified: 05/10/2012,10/24/2013           
 --     Author:Atul ,Liju          
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
if (@ProductID !=0)    
 BEGIN    
 SELECT      
   Distinct Q.QuestionID,Q.QID,Q.QID AS QN,Q.Stem,Q.ReleaseStatus,Q.TypeOfFileID,      
    T.ProductID,      
    CC.ClinicalConcept,      
    D.Demographic ,      
    R.TopicTitle ,      
    NP.NursingProcess,      
    LD.LevelOfDifficulty     
 FROM dbo.Tests T      
  INNER JOIN dbo.TestQuestions TQ ON T.TestID = TQ.TestID      
  RIGHT OUTER JOIN dbo.Questions Q ON TQ.QID = Q.QID      
  LEFT OUTER JOIN dbo.ClinicalConcept CC ON Q.ClinicalConceptsID = CC.ClinicalConceptID      
  LEFT OUTER JOIN dbo.Demographic D ON Q.DemographicID = D.DemographicID      
  LEFT OUTER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID      
  LEFT OUTER JOIN dbo.NursingProcess NP ON Q.NursingProcessID = NP.NursingProcessID      
  LEFT OUTER JOIN dbo.LevelofDifficulty  LD ON Q.LevelOfDifficultyID = LD.LevelOfDifficultyID      
  where T.ProductID=@ProductID      
   AND (@TestID =0 OR T.TestID=@TestID)      
   AND (@ClientNeedID = 0 OR Q.ClientNeedsID=@ClientNeedID)      
   AND (@ClientNeedsCategoryID = 0  OR Q.ClientNeedsCategoryID=@ClientNeedsCategoryID)      
   AND (@ClinicalConceptID  = 0 OR Q.ClinicalConceptsID=@ClinicalConceptID)      
   AND (@CognitiveLevelID = 0 OR Q.CognitiveLevelID=@CognitiveLevelID)      
   AND (@CriticalThinkingID = 0 OR Q.CriticalThinkingID=@CriticalThinkingID)      
   AND (@DemographicID = 0 OR Q.DemographicID=@DemographicID)      
   AND (@LevelOfDifficultyID = 0 OR Q.LevelOfDifficultyID=@LevelOfDifficultyID)      
   AND (@NursingProcessID = 0 OR Q.NursingProcessID=@NursingProcessID)      
   AND (@RemediationID = 0 OR Q.RemediationID=@RemediationID)      
   AND (@SpecialtyAreaID = 0 OR Q.SpecialtyAreaID=@SpecialtyAreaID)      
   AND (@SystemID = 0 OR Q.SystemID=@SystemID)     
   AND (@ConceptsID = 0 OR Q.ConceptsID=@ConceptsID)     
   AND ((@QuestionID = '' OR @QuestionID is null) OR Q.QuestionID like '%' + @QuestionID + '%')      
   AND ((@TypeOfFileID = '' OR @TypeOfFileID is null) OR Q.TypeOfFileID=@TypeOfFileID)      
   AND ((@QuestionType ='0' OR @QuestionType is null) OR Q.QuestionType= @QuestionType)      
   AND ((@Text = '' OR @Text is null) OR (Q.Stem like '%' + @Text + '%' OR Q.Stimulus like '%' + @Text + '%' OR Q.Explanation like '%' + @Text + '%' OR Q.ItemTitle like '%' + @Text + '%'))      
   AND (@ReleaseStatus = 0 OR Q.ReleaseStatus IS NOT NULL)      
   AND (@Active = 1 OR Q.Active = 0)      
   AND (@Active = 0 OR (Q.Active IS NULL OR Q.Active = 1))      
   AND (@AccreditationCategoriesID = 0 OR Q.AccreditationCategoriesID = @AccreditationCategoriesID)      
   AND (@QSENKSACompetenciesID = 0 OR Q.QSENKSACompetenciesID = @QSENKSACompetenciesID)      
      AND Q.ProgramofStudyId = @ProgramOfStudy       
 ORDER BY T.ProductID, Q.TypeOfFileID      
 END      
ELSE      
 BEGIN      
 SELECT Q.QuestionID,Q.QID,Q.QID AS QN,Q.Stem,Q.ReleaseStatus,Q.TypeOfFileID,      
   CC.ClinicalConcept, NP.NursingProcess,      
   R.TopicTitle,  D.Demographic,LD.LevelOfDifficulty     
   FROM  dbo.Questions Q      
   LEFT OUTER JOIN dbo.Demographic D ON Q.DemographicID = D.DemographicID      
   LEFT OUTER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID      
   LEFT OUTER JOIN dbo.NursingProcess NP ON Q.NursingProcessID = NP.NursingProcessID      
   LEFT OUTER JOIN dbo.ClinicalConcept CC ON Q.ClinicalConceptsID = CC.ClinicalConceptID    
   LEFT OUTER JOIN dbo.LevelofDifficulty  LD ON Q.LevelOfDifficultyID = LD.LevelOfDifficultyID     
    where      
   (@ClientNeedID = 0 OR Q.ClientNeedsID=@ClientNeedID)      
  AND (@ClientNeedsCategoryID = 0  OR Q.ClientNeedsCategoryID=@ClientNeedsCategoryID)      
  AND (@ClinicalConceptID  = 0 OR Q.ClinicalConceptsID=@ClinicalConceptID)      
  AND (@CognitiveLevelID = 0 OR Q.CognitiveLevelID=@CognitiveLevelID)      
  AND (@CriticalThinkingID = 0 OR Q.CriticalThinkingID=@CriticalThinkingID)      
  AND (@DemographicID = 0 OR Q.DemographicID=@DemographicID)      
  AND (@LevelOfDifficultyID = 0 OR Q.LevelOfDifficultyID=@LevelOfDifficultyID)      
  AND (@NursingProcessID = 0 OR Q.NursingProcessID=@NursingProcessID)      
  AND (@RemediationID = 0 OR Q.RemediationID=@RemediationID)      
  AND (@SpecialtyAreaID = 0 OR Q.SpecialtyAreaID=@SpecialtyAreaID)      
  AND (@SystemID = 0 OR Q.SystemID=@SystemID)      
  AND (@ConceptsID = 0 OR Q.ConceptsID=@ConceptsID)    
  AND ((@QuestionID = '' OR @QuestionID is null) OR Q.QuestionID like '%' + @QuestionID + '%')      
  AND ((@TypeOfFileID = '' OR @TypeOfFileID is null) OR Q.TypeOfFileID=@TypeOfFileID)      
  AND ((@QuestionType ='0' OR @QuestionType is null) OR Q.QuestionType= @QuestionType)      
  AND ((@Text = '' OR @Text is null) OR (Q.Stem like '%' + @Text + '%' OR Q.Stimulus like '%' + @Text + '%' OR Q.Explanation like '%' + @Text + '%' OR Q.ItemTitle like '%' + @Text + '%'))      
  AND (@ReleaseStatus = 0 OR Q.ReleaseStatus IS NOT NULL)      
  AND (@Active = 1 OR Q.Active = 0)      
  AND (@Active = 0 OR (Q.Active IS NULL OR Q.Active = 1))      
  AND (@AccreditationCategoriesID = 0 OR Q.AccreditationCategoriesID = @AccreditationCategoriesID)      
  AND (@QSENKSACompetenciesID = 0 OR Q.QSENKSACompetenciesID = @QSENKSACompetenciesID)      
     AND Q.ProgramofStudyId = @ProgramOfStudy    
 Order By QuestionID      
 END    
    
SET NOCOUNT OFF;    
END 
GO

PRINT 'Finished creating PROCEDURE uspGetListOfQuestionsShowUnique'
GO


