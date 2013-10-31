SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveQuestion]
GO

PRINT 'Creating PROCEDURE uspSaveQuestion'
GO 

CREATE PROCEDURE [dbo].[uspSaveQuestion]            
 @QID INT ,            
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
 @RemediationId VARCHAR(5000),            
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
 @Statisctics VARCHAR(500),            
 @CreatorId VARCHAR(500),            
 @DateCreated VARCHAR (50),            
 @EditorId VARCHAR(500),            
 @DateEdited VARCHAR (500),            
 @EditorId_2 VARCHAR(500),            
 @DateEdited_2 VARCHAR (500),            
 @Source_SBD VARCHAR (500),            
 @WhoOwns VARCHAR(500),            
 @Feedback VARCHAR (500),            
 @Active INT ,            
 @PointBiserialsId VARCHAR(500),            
 @ItemTitle VARCHAR (500),            
 @ExhibitTab1 VARCHAR (5000),            
 @ExhibitTab2 VARCHAR (5000),            
 @ExhibitTab3 VARCHAR (5000),            
 @Norming FLOAT(53), -- as per db            
 @ExhibitTitle1 VARCHAR (1000),            
 @ExhibitTitle2 VARCHAR (1000),            
 @ExhibitTitle3 VARCHAR (1000),            
 @ListeningFileUrl VARCHAR (1000),            
 @AlternateStem VARCHAR(5000),            
 @NewQuestionId INT OUT,           
 @AccreditationCategoriesId VARCHAR (500),          
 @QSENKSACompetenciesId VARCHAR (500),          
 @ProgramofStudyId int,          
 @AdminId int ,
 @ConceptsId VARCHAR(500) 
 AS  
BEGIN          
 SET NOCOUNT ON                  
/*============================================================================================================                
 --      Purpose: To include the newly added columns for categories   
 --      Modified: To include the new category Concepts to the questions        
 --      Modified: 05/03/2013 ,10/28/2013               
 --      Author:Liju        
 --      Modified By : Shodhan ,Liju            
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
 SELECT  @NewQuestionId = 0            
 DECLARE @QIDCount INT,@SystemDate DateTime            
 SET @QIDCount = 0   
 SET  @SystemDate = getdate()       
 SELECT  @NewQuestionId = 0            
   IF @QID = 0   
   BEGIN            
  SELECT  @QIDCount = count(QuestionID) from Questions where QuestionID=@QuestionId            
   END            
   ELSE            
   BEGIN            
  SELECT  @QIDCount = count(QuestionID) from Questions where QuestionID=@QuestionId and QID<>@QID            
   END            
  IF @QIDCount > 0            
  BEGIN            
  Select @NewQuestionId = - 1            
  RETURN @NewQuestionId          
  END            
 IF @QID = 0            
  BEGIN            
   INSERT INTO Questions            
   (            
   QuestionID,            
   QuestionType,            
   ClientNeedsID,            
   ClientNeedsCategoryID,            
   NursingProcessID,            
   LevelOfDifficultyID,            
   DemographicID,            
   CognitiveLevelID,            
   CriticalThinkingID,            
   IntegratedConceptsID,            
   ClinicalConceptsID,            
   Stimulus,            
   Stem,            
   Explanation,            
   RemediationID,            
   SpecialtyAreaID,            
   SystemID,            
   ReadingCategoryID,            
   ReadingID,            
   WritingCategoryID,            
   WritingID,            
   MathCategoryID,            
   MathID,            
   ProductLineID,            
   TypeOfFileID,            
   Statisctics,            
   CreatorID,            
   DateCreated,            
   EditorID,            
   DateEdited,            
   EditorID_2,            
   DateEdited_2,            
   Source_SBD,            
   WhoOwns,            
   PointBiserialsID,            
   Feedback,            
   Active,            
   ItemTitle,            
   Q_Norming,            
   ReleaseStatus,            
   ExhibitTab1,            
   ExhibitTab2,            
   ExhibitTab3,            
   ExhibitTitle1,            
   ExhibitTitle2,            
   ExhibitTitle3,            
   ListeningFileUrl,            
   AlternateStem,          
   AccreditationCategoriesId,          
   QSENKSACompetenciesId,          
   ProgramofStudyId,
   ConceptsId          
   )            
   VALUES            
   (            
   @QuestionId,            
   @QuestionType,            
   @ClientNeedsId,            
   @ClientNeedsCategoryId,            
   @NursingProcessId,            
   @LevelOfDifficultyId,            
   @DemographicId,            
   @CognitiveLevelId,            
   @CriticalThinkingId,            
   @IntegratedConceptsId,            
   @ClinicalConceptsId,            
   @Stimulus,            
   @Stem,            
   @Explanation,            
   @RemediationId,            
   @SpecialtyAreaId,            
   @SystemId,            
   @ReadingCategoryId,            
   @ReadingId,            
   @WritingCategoryId,            
   @WritingId,            
   @MathCategoryId,            
   @MathId,            
   @ProductLineId,            
   @TypeOfFileId,            
   @Statisctics,            
   @CreatorId,            
   @DateCreated,            
   @EditorId,            
   @DateEdited,            
   @EditorID_2,            
   @DateEdited_2,            
   @Source_SBD,            
   @WhoOwns,            
   @PointBiserialsId,            
   @Feedback,            
   @active,            
   @ItemTitle,            
   @Norming,            
   'E',            
   @ExhibitTab1,            
   @ExhibitTab2,            
   @ExhibitTab3,            
   @ExhibitTitle1,            
   @ExhibitTitle2,            
   @ExhibitTitle3,            
   @ListeningFileUrl,            
   @AlternateStem ,          
   @AccreditationCategoriesId,          
   @QSENKSACompetenciesId,          
   @ProgramofStudyId,
   @ConceptsId          
   )            
   SELECT @NewQuestionId = SCOPE_IDENTITY()            
           
   INSERT INTO QuestionLogs(QId,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy)         
   VALUES (@NewQuestionId,@SystemDate,@AdminId,@SystemDate,@AdminId)        
           
   RETURN  @NewQuestionId            
  END             
 ELSE            
  BEGIN            
   UPDATE  Questions            
   SET QuestionID=@QuestionId,            
    QuestionType=@QuestionType,            
    ClientNeedsID=@ClientNeedsId,            
    ClientNeedsCategoryID=@ClientNeedsCategoryId,            
    NursingProcessID=@NursingProcessId,            
    LevelOfDifficultyID=@LevelOfDifficultyId,            
    DemographicID=@DemographicId,            
    CognitiveLevelID=@CognitiveLevelId,            
    CriticalThinkingID=@CriticalThinkingId,            
    IntegratedConceptsID=@IntegratedConceptsId,            
    ClinicalConceptsID=@ClinicalConceptsId,            
    Stimulus=@Stimulus,            
    Stem=@Stem,            
    Explanation=@Explanation,            
    RemediationID=@RemediationId,            
    SpecialtyAreaID=@SpecialtyAreaId,            
    SystemID=@SystemId,            
    ReadingCategoryID=@ReadingCategoryId,            
    ReadingID=@ReadingId,            
    WritingCategoryID=@WritingCategoryId,            
    WritingID=@WritingId,            
    MathCategoryID=@MathCategoryId,            
    MathID=@MathId,            
    ProductLineID=@ProductLineId,            
    TypeOfFileID=@TypeOfFileId,       
    Statisctics=@Statisctics,            
    CreatorID=@CreatorId,            
    DateCreated=@DateCreated,            
    EditorID=@EditorId,            
    DateEdited=@DateEdited,            
    EditorID_2=@EditorID_2,            
    DateEdited_2=@DateEdited_2,            
    Source_SBD=@Source_SBD,            
    WhoOwns=@WhoOwns,            
    PointBiserialsID=@PointBiserialsId,            
    Feedback=@Feedback,Active=@active,            
    ItemTitle=@ItemTitle,Q_Norming=@Norming, ReleaseStatus='E',            
    AlternateStem=@AlternateStem,            
    ExhibitTitle1=@ExhibitTitle1,            
    ExhibitTitle2=@ExhibitTitle2,            
    ExhibitTitle3=@ExhibitTitle3,            
    ListeningFileUrl=@ListeningFileUrl,            
    ExhibitTab1=@ExhibitTab1,            
    ExhibitTab2=@ExhibitTab2,            
    ExhibitTab3=@ExhibitTab3,          
    AccreditationCategoriesId =@AccreditationCategoriesId,          
    QSENKSACompetenciesId =@QSENKSACompetenciesId , 
    ConceptsId = @ConceptsId        
   WHERE QID=@QID            
           
   UPDATE QuestionLogs        
   SET UpdatedDate = @SystemDate,        
       UpdatedBy=@AdminId        
   WHERE QId = @QID        
           
   SELECT @NewQuestionId = @QID            
   RETURN  @NewQuestionId            
 END          
SET NOCOUNT OFF;                 
END   

GO

PRINT 'Finished creating PROCEDURE uspSaveQuestion'
GO     
