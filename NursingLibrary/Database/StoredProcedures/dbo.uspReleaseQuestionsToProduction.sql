SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseQuestionsToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseQuestionsToProduction]
GO

PRINT 'Creating PROCEDURE uspReleaseQuestionsToProduction'
GO 
      
CREATE Proc [dbo].[uspReleaseQuestionsToProduction]            
 @SourceNumber INT,            
 @QID INT ,            
 @XMLQID varchar(50),            
 @QuestionId VARCHAR(50),            
 @QuestionType CHAR (2),            
 @ClientNeedsId VARCHAR(500),            
 @ClientNeedsCategoryId VARCHAR(500),            
 @NursingProcessId VARCHAR(500),            
 @LevelOfDifficultyId VARCHAR(500),            
 @DemographicId VARCHAR(500),            
 @CognitiveLevelId VARCHAR(500),            
 @CriticalThinkingId VARCHAR(500),            
 @IntegratedConceptsId VARCHAR(500),            
 @ClinicalConceptsId VARCHAR(500),            
 @Stimulus VARCHAR(500),            
 @Stem VARCHAR(5000),            
 @Explanation VARCHAR(5000),            
 @Remediation VARCHAR(5000),            
 @RemediationId VARCHAR(5000),            
 @TopicTitleId VARCHAR(500),            
 @SpecialtyAreaId VARCHAR(500),            
 @SystemId VARCHAR(500),            
 @ReadingCategoryId VARCHAR(500),            
 @ReadingId VARCHAR(500),            
 @WritingCategoryId VARCHAR(500),            
 @WritingId VARCHAR(500),            
 @MathCategoryId VARCHAR(500),            
 @MathId VARCHAR(500),            
 @ProductLineId VARCHAR(500),            
 @TypeOfFileId VARCHAR(500),            
 @ItemTitle VARCHAR(500),            
 @Statisctics VARCHAR(500),            
 @CreatorId VARCHAR(500),            
 @DateCreated VARCHAR (50),            
 @EditorId VARCHAR(500),            
 @DateEdited VARCHAR (500),            
 @EditorId2 VARCHAR(500),            
 @DateEdited2 VARCHAR (500),            
 @SourceSBD VARCHAR (500),            
 @WhoOwns VARCHAR(500),            
 @WhereUsed VARCHAR(500),            
 @PointBiserialsId VARCHAR(500),            
 @Feedback VARCHAR (500),            
 @Active INT ,            
 @Deleted INT,            
 @QuestionNumber nChar(10),            
 @TestNumber nchar(10),            
 @QNorming float,            
 @ExhibitTab1 VARCHAR (5000),            
 @ExhibitTab2 VARCHAR (5000),            
 @ExhibitTab3 VARCHAR (5000),            
 @ListeningFileUrl nVarchar(500),            
 @AlternateStem VARCHAR (5000),          
 @AccreditationCategoriesId VARCHAR(500),           
 @QSENKSACompetenciesId VARCHAR(500),          
 @ProgramOfStudyId INT,        
 @CreatedBy INT,         
 @CreatedDate DATETIME,        
 @UpdatedBy INT,        
 @UpdatedDate DATETIME,        
 @ReleasedBy INT,      
 @ReleasedDate DATETIME,
 @ConceptsId VARCHAR(500)   
AS            
BEGIN                      
SET NOCOUNT ON                    
/*============================================================================================================              
 --  Purpose: For adding the new categories while releasing the question to production      
 --  Modification Purpose: Sprint 43: PN RN Unification: Propagating ProgamOfStudyId To Live Site      
 --       on releasing a question as part of changes done for Nursing-2981.   
     Sprint 53: To include question log info as part of NURSING-4683  
 --  Sprint 55: To include new category (Concepts) as part of NURSING-4720   
 --  Modified: 05/11/2012, 05/03/2013  ,09/20/2013, 10/28/2013           
 --  Author: Liju, Atul Gupta ,Shodhan ,Liju         
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
SET IDENTITY_INSERT Questions ON            
if not exists(select QID from Questions where QID = @QID)            
Begin            
 Insert Into Questions            
    (SourceNumber, XMLQID, QID, QuestionID, QuestionType, ClientNeedsID, ClientNeedsCategoryID, NursingProcessID, LevelOfDifficultyID,            
    DemographicID, CognitiveLevelID, CriticalThinkingID, IntegratedConceptsID, ClinicalConceptsID, Stimulus, Stem, Explanation, Remediation,          
    RemediationID, TopicTitleID, SpecialtyAreaID, SystemID, ReadingCategoryID, ReadingID, WritingCategoryID, WritingID, MathCategoryID, MathID,            
    ProductLineID, TypeOfFileID, ItemTitle, Statisctics, CreatorID, DateCreated, EditorID, DateEdited, EditorID_2, DateEdited_2, Source_SBD, WhoOwns,            
    WhereUsed, PointBiserialsID, Feedback, Active, Deleted, QuestionNumber, TestNumber, Q_Norming, ExhibitTab1, ExhibitTab2, ExhibitTab3,ListeningFileUrl,          
    AlternateStem,AccreditationCategoriesId,QSENKSACompetenciesId, ProgramofStudyId,ConceptsID)            
    Values(@SourceNumber,@XMLQID,@QID,@QuestionId,@QuestionType,@ClientNeedsId,@ClientNeedsCategoryId,@NursingProcessId,@LevelOfDifficultyId,            
    @DemographicId,@CognitiveLevelId,@CriticalThinkingId,@IntegratedConceptsId,@ClinicalConceptsId,@Stimulus,@Stem,@Explanation,@Remediation,            
    @RemediationId,@TopicTitleId,@SpecialtyAreaId,@SystemId,@ReadingCategoryId,@ReadingId,@WritingCategoryId,@WritingId,@MathCategoryId,@MathId,            
    @ProductLineId,@TypeOfFileId,@ItemTitle,@Statisctics,@CreatorId,@DateCreated,@EditorId,@DateEdited,@EditorId2,@DateEdited2,@SourceSBD,@WhoOwns,            
    @WhereUsed,@PointBiserialsId,@Feedback,@Active,@Deleted,@QuestionNumber,@TestNumber,@QNorming,@ExhibitTab1,@ExhibitTab2,@ExhibitTab3,@ListeningFileUrl,          
    @AlternateStem,@AccreditationCategoriesId,@QSENKSACompetenciesId, @ProgramOfStudyId, @ConceptsId)         
            
   INSERT INTO QuestionLogs(QId,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,ReleasedDate,ReleasedBy)         
   VALUES (@QID,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy,@ReleasedDate,@ReleasedBy)           
End            
Else            
Begin            
UPDATE Questions            
 SET            
   SourceNumber = @SourceNumber,            
   XMLQID = @XMLQID,            
   QuestionID = @QuestionId,            
   QuestionType = @QuestionType,            
   ClientNeedsID = @ClientNeedsId,            
   ClientNeedsCategoryID = @ClientNeedsCategoryId,            
   NursingProcessID = @NursingProcessId,            
   LevelOfDifficultyID = @LevelOfDifficultyId,            
   DemographicID = @DemographicId,            
   CognitiveLevelID = @CognitiveLevelId,            
   CriticalThinkingID = @CriticalThinkingId,            
   IntegratedConceptsID = @IntegratedConceptsId,            
   ClinicalConceptsID = @ClinicalConceptsId,            
   Stimulus = @Stimulus,            
   Stem = @Stem,            
   Explanation = @Explanation ,            
   Remediation = @Remediation,            
   RemediationID = @RemediationId,            
   TopicTitleID = @TopicTitleId,            
   SpecialtyAreaID = @SpecialtyAreaId,            
   SystemID = @SystemId,            
   ReadingCategoryID = @ReadingCategoryId,            
   ReadingID = @ReadingId,            
   WritingCategoryID = @WritingCategoryId,            
   WritingID = @WritingId,            
   ProductLineID = @ProductLineId,            
   MathCategoryID=@MathCategoryId,            
   MathID = @MathId,            
   TypeOfFileID = @TypeOfFileId,            
   ItemTitle = @ItemTitle,            
   Statisctics = @Statisctics,            
   CreatorID = @CreatorId,            
   DateCreated = @DateCreated,            
   EditorID = @EditorId,            
   DateEdited = @DateEdited,            
   EditorID_2 = @EditorId2,            
   DateEdited_2 = @DateEdited2,            
   Source_SBD = @SourceSBD,            
   WhoOwns = @WhoOwns,            
   WhereUsed = @WhereUsed,            
   PointBiserialsID = @PointBiserialsId,            
   Feedback = @Feedback,            
   Active = @Active,            
   Deleted = @Deleted,            
   QuestionNumber = @QuestionNumber,            
   TestNumber = @TestNumber,            
   Q_Norming = @QNorming,            
   ExhibitTab1 = @ExhibitTab1,            
   ExhibitTab2 = @ExhibitTab2,            
   ExhibitTab3 = @ExhibitTab3,            
   ListeningFileUrl = @ListeningFileUrl,            
   AlternateStem = @AlternateStem,          
   AccreditationCategoriesId = @AccreditationCategoriesId,          
   QSENKSACompetenciesId = @QSENKSACompetenciesId,          
   ProgramofStudyId = @ProgramOfStudyId ,
   ConceptsID = @ConceptsId      
 WHERE  QID = @QID           
         
 UPDATE QuestionLogs        
   SET         
       UpdatedDate = @UpdatedDate,        
       UpdatedBy = @UpdatedBy,        
       ReleasedDate = @ReleasedDate,        
       ReleasedBy = @ReleasedBy        
   WHERE QId = @QID         
END            
SET IDENTITY_INSERT Questions OFF        
SET NOCOUNT OFF       
END   
GO

PRINT 'Finished creating PROCEDURE uspReleaseQuestionsToProduction'
GO 


