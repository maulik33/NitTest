SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestions]
GO
 
PRINT 'Creating PROCEDURE uspGetQuestions'
GO
 
CREATE PROCEDURE [dbo].[uspGetQuestions]      
@QuestionId as varchar(50),      
@QID as int,      
@RemediationId as varchar(5000) ,      
@ForEdit bit ,      
@ReleaseStatus char(1)      
AS      
BEGIN      
  SET NOCOUNT ON                
/*============================================================================================================              
 --   Purpose: To include the newly added columns for the category      
 --   Purpose: Sprint 43: PN RN Unification. Returning ProgramofStudyName as part of changes done for Nursing-2981      
      Purpose:Sprint 53 Retriving question log information as part of change - NURSING-4683 
 --   Purpose:Sprint 55 To include the category C as part Concepts 
 --   as apart of change - NURSING-4720  
 --   Modified: 05/03/2013, 05/07/2013,09/20/2013,10/28/2013             
 --   Author:Liju       
 --   Modified BY: Shodhan, Atul,Shodhan,Liju           
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
  IF @ForEdit = 0      
   BEGIN      
    SELECT      
    '' AS RE,      
    Q.Remediation,      
    Q.QuestionType,      
    Q.QID,      
    Q.QuestionID,      
    Q.Stimulus,      
    Q.Stem,      
    Q.ListeningFileUrl,      
    Q.Explanation,      
    Q.ProductLineID,      
    Q.PointBiserialsID,      
    Q.Statisctics,      
    Q.CreatorID,      
    Q.DateCreated,      
    Q.EditorID,      
    Q.DateEdited,      
    Q.EditorID_2,      
    Q.DateEdited_2,      
    Q.Source_SBD,      
    Q.Feedback,      
    Q.WhoOwns,      
    Q.ItemTitle,      
    Q.TypeOfFileID,      
    Q.QuestionType,      
    Q.active,      
    Q.Q_Norming,      
    Q.RemediationID,      
    Q.ClinicalConceptsID,      
    Q.CriticalThinkingID,      
    Q.SystemID,      
    Q.SpecialtyAreaID,      
    Q.CognitiveLevelID,      
    Q.DemographicID,      
    Q.LevelOfDifficultyID,      
    Q.NursingProcessID,      
    Q.ClientNeedsID,      
    Q.ClientNeedsCategoryID,   
    Q.ConceptsID,   
    Q.ExhibitTab1,      
    Q.ExhibitTab2,      
    Q.ExhibitTab3,      
    Q.ListeningFileUrl,      
    Q.ExhibitTitle1,      
    Q.ExhibitTitle2,      
    Q.ExhibitTitle3,      
    Q.TopicTitleID,      
    Q.XMLQID,      
    Q.IntegratedConceptsID,      
    Q.ReadingCategoryID,      
    Q.ReadingID,      
    Q.WritingCategoryID,      
    Q.WritingID,      
    Q.MathCategoryID,      
    Q.MathID,      
    Q.WhereUsed,      
    Q.Deleted,      
    Q.QuestionNumber,      
    Q.TestNumber,      
    Q.ReleaseStatus,      
    Q.AlternateStem,      
    Q.AccreditationCategoriesID,      
    Q.QSENKSACompetenciesID,      
    R.TopicTitle AS RemediationTopicTitle,        
    Q.ProgramofStudyId,      
    PS.ProgramofStudyName,    
    QL.CreatedDate,    
    QL.CreatedBy,    
    QL.UpdatedDate,    
    QL.UpdatedBy,    
    QL.ReleasedBy    
    FROM dbo.Questions Q      
    INNER JOIN ProgramofStudy PS      
    ON Q.ProgramofStudyId = PS.ProgramofStudyId      
 LEFT OUTER JOIN dbo.Remediation  R      
 ON Q.RemediationID = R.RemediationID    
 LEFT JOIN dbo.QuestionLogs QL ON Q.QID = QL.QID      
    WHERE      
   (Q.QuestionID=@QuestionId or @QuestionId ='')      
    AND (Q.QID = @QID or @QID = 0)      
    AND (      
    Q.RemediationID=@RemediationId      
    OR @RemediationId=''      
     ) AND (Q.ReleaseStatus = @ReleaseStatus or @ReleaseStatus='')      
  END      
 ELSE      
  BEGIN      
   SELECT R.Explanation AS RE,      
    Q.Remediation,      
    Q.QuestionType,      
    Q.QID,      
    Q.QuestionID,      
    Q.Stimulus,      
    Q.Stem,      
    Q.ListeningFileUrl,      
    Q.Explanation,      
    Q.ProductLineID,      
    Q.PointBiserialsID,      
    Q.Statisctics,      
    Q.CreatorID,      
    Q.DateCreated,      
    Q.EditorID,      
    Q.DateEdited,      
    Q.EditorID_2,      
    Q.DateEdited_2,      
    Q.Source_SBD,      
    Q.Feedback,      
    Q.WhoOwns,      
    Q.ItemTitle,      
    Q.TypeOfFileID,      
    Q.QuestionType,      
    Q.active,      
    Q.Q_Norming,      
    Q.RemediationID,      
    Q.ClinicalConceptsID,      
    Q.CriticalThinkingID,      
    Q.SystemID,      
    Q.SpecialtyAreaID,      
    Q.CognitiveLevelID,      
    Q.DemographicID,      
    Q.LevelOfDifficultyID,      
    Q.NursingProcessID,      
    Q.ClientNeedsID,      
    Q.ClientNeedsCategoryID,  
    Q.ConceptsID,    
    Q.ExhibitTab1,      
    Q.ExhibitTab2,      
    Q.ExhibitTab3,      
    Q.ListeningFileUrl,      
    Q.ExhibitTitle1,      
    Q.ExhibitTitle2,      
    Q.ExhibitTitle3,      
    Q.TopicTitleID,      
    Q.XMLQID,      
    Q.IntegratedConceptsID,      
    Q.ReadingCategoryID,      
    Q.ReadingID,      
    Q.WritingCategoryID,      
    Q.WritingID,      
    Q.MathCategoryID,      
    Q.MathID,      
    Q.WhereUsed,      
    Q.Deleted,      
    Q.QuestionNumber,      
    Q.TestNumber,      
    Q.ReleaseStatus,      
    Q.AlternateStem,      
    Q.AccreditationCategoriesID,      
    Q.QSENKSACompetenciesID,      
    R.TopicTitle AS RemediationTopicTitle,        
    Q.ProgramofStudyId,      
    PS.ProgramofStudyName,    
    QL.CreatedDate,    
    QL.CreatedBy,    
    QL.UpdatedDate,    
    QL.UpdatedBy,    
    QL.ReleasedBy           
   FROM      
   dbo.Questions Q      
   INNER JOIN ProgramofStudy PS ON      
   Q.ProgramofStudyId = PS.ProgramofStudyId      
   LEFT OUTER JOIN dbo.Remediation  R      
   ON Q.RemediationID = R.RemediationID      
   LEFT JOIN dbo.QuestionLogs QL     
   ON Q.QID = QL.QID    
   WHERE Q.QID = @QID      
  END      
SET NOCOUNT OFF;               
END 
GO

PRINT 'Finished creating PROCEDURE uspGetQuestions'
GO 



