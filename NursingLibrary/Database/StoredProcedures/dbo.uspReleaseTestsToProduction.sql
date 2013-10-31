
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseTestsToProduction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReleaseTestsToProduction]
GO
PRINT 'Creating PROCEDURE uspReleaseTestsToProduction'
GO

CREATE PROC [dbo].[uspReleaseTestsToProduction]  
 @TestId int,  
 @ProductId int,  
 @TestName varchar (50),  
 @TestNumber int,  
 @ActivationTime datetime,  
 @TimeActivated int,  
 @SecureTestS int,  
 @SecureTestD int,  
 @ScramblingS int,  
 @ScramblingD int,  
 @RemediationS int,  
 @RemediationD int,  
 @ExplanationD int,  
 @ExplanationS int,  
 @LevelOfDifficultyS int,  
 @LevelOfDifficultyD int,  
 @NursingProcessS int,  
 @NursingProcessD int,  
 @ClinicalConceptsS int,  
 @ClinicalConceptsD int,  
 @DemographicsS int,  
 @DemographicsD int,  
 @ClientNeedsS int,  
 @ClientNeedsD int,  
 @BloomsS int,  
 @BloomsD int,  
 @TopicS int,  
 @TopicD int,  
 @SpecialtyAreaS int,  
 @SpecialtyAreaD int,  
 @SystemS int,  
 @SystemD int,  
 @CriticalThinkingS int,  
 @CriticalThinkingD int,  
 @ReadingS int,  
 @ReadingD int,  
 @MathS int,  
 @MathD int,  
 @WritingS int,  
 @WritingD int,  
 @RemedationTimeS int,  
 @RemedationTimeD int,  
 @ExplanationTimeS int,  
 @ExplanationTimeD int,  
 @TimeStampS int,  
 @TimeStampD int,  
 @ActiveTest int,  
 @TestSubGroup int,  
 @Url nvarchar (200),  
 @PopHeight int,  
 @PopWidth int,  
 @DefaultGroup char(1),
 @SecondPerQuestion int,
 @ProgramofStudyId int 
AS  
 BEGIN            
   SET NOCOUNT ON         
/*===================================================================================================================================  
//Purpose:	To include the newly added column 'SecondPerQuestion' 
			Sprint 42: To carry the ProgramofStudyId (PN/RN) for a test done as part of NURSING-2980. Pre-requisite for NURSING-2985.
//Modified:	June 06/12, April 22/13
//Author:	Liju, Atul
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
if exists(select TestID from Tests where TestID = @TestId)  
 Begin  
  UPDATE Tests  
   SET  
   ProductID = @ProductId,  
   TestName = @TestName,  
   TestNumber = @TestNumber,  
   ActivationTime = @ActivationTime,  
   TimeActivated = @TimeActivated,  
   SecureTest_S = @SecureTestS,  
   SecureTest_D = @SecureTestD,  
   Scrambling_S = @ScramblingS,  
   Scrambling_D = @ScramblingD,  
   Remediation_S = @RemediationS,  
   Remediation_D = @RemediationD,  
   Explanation_D = @ExplanationD,  
   Explanation_S = @ExplanationS,  
   LevelOfDifficulty_S = @LevelOfDifficultyS,  
   LevelOfDifficulty_D = @LevelOfDifficultyD,  
   NursingProcess_S = @NursingProcessS,  
   NursingProcess_D = @NursingProcessD,  
   ClinicalConcepts_S = @ClinicalConceptsS,  
   ClinicalConcepts_D = @ClinicalConceptsD,  
   Demographics_S = @DemographicsS,  
   Demographics_D = @DemographicsD,  
   ClientNeeds_S = @ClientNeedsS,  
   ClientNeeds_D = @ClientNeedsD,  
   Blooms_S = @BloomsS,  
   Blooms_D = @BloomsD,  
   Topic_S = @TopicS,  
   Topic_D = @TopicD,  
   SpecialtyArea_S = @SpecialtyAreaS,  
   SpecialtyArea_D = @SpecialtyAreaD,  
   System_S = @SystemS,  
   System_D = @SystemD,  
   CriticalThinking_S = @CriticalThinkingS,  
   CriticalThinking_D = @CriticalThinkingD,  
   Reading_S = @ReadingS,  
   Reading_D = @ReadingD,  
   Math_S = @MathS,  
   Math_D = @MathD,  
   Writing_S = @WritingS,  
   Writing_D = @WritingD,  
   RemedationTime_S = @RemedationTimeS,  
   RemedationTime_D = @RemedationTimeD,  
   ExplanationTime_S = @ExplanationTimeS,  
   ExplanationTime_D = @ExplanationTimeD,  
   TimeStamp_S = @TimeStampS,  
   TimeStamp_D = @TimeStampD,  
   ActiveTest = @ActiveTest,  
   TestSubGroup = @TestSubGroup,  
   Url = @Url,  
   PopHeight = @PopHeight,  
   PopWidth = @PopWidth,  
   DefaultGroup = @DefaultGroup,
   SecondPerQuestion = @SecondPerQuestion,
   ProgramofStudyId = @ProgramofStudyId
   Where TestID = @TestId  
 End  
 Else  
 Begin  
   SET IDENTITY_INSERT Tests ON  
   Insert Into Tests  
   (TestID, ProductID, TestName, TestNumber, ActivationTime, TimeActivated, SecureTest_S, SecureTest_D, Scrambling_S, Scrambling_D,  
   Remediation_S, Remediation_D, Explanation_D, Explanation_S, LevelOfDifficulty_S, LevelOfDifficulty_D, NursingProcess_S, NursingProcess_D,  
   ClinicalConcepts_S, ClinicalConcepts_D, Demographics_S, Demographics_D, ClientNeeds_S, ClientNeeds_D, Blooms_S, Blooms_D, Topic_S,Topic_D,  
   SpecialtyArea_S, SpecialtyArea_D, System_S, System_D, CriticalThinking_S, CriticalThinking_D, Reading_S, Reading_D, Math_S, Math_D,  
   Writing_S, Writing_D, RemedationTime_S, RemedationTime_D, ExplanationTime_S, ExplanationTime_D, TimeStamp_S, TimeStamp_D, ActiveTest,  
   TestSubGroup, Url, PopHeight, PopWidth, DefaultGroup, SecondPerQuestion, ProgramofStudyId)  
   Values  
   (@TestId,@ProductId,@TestName,@TestNumber,@ActivationTime,@TimeActivated,@SecureTestS,@SecureTestD,@ScramblingS,@ScramblingD,  
    @RemediationS,@RemediationD,@ExplanationD,@ExplanationS,@LevelOfDifficultyS,@LevelOfDifficultyD,@NursingProcessS,@NursingProcessD,  
    @ClinicalConceptsS,@ClinicalConceptsD,@DemographicsS,@DemographicsD,@ClientNeedsS,@ClientNeedsD,@BloomsS,@BloomsD,@TopicS,@TopicD,  
    @SpecialtyAreaS,@SpecialtyAreaD,@SystemS,@SystemD,@CriticalThinkingS,@CriticalThinkingD,@ReadingS,@ReadingD,@MathS,@MathD,  
    @WritingS,@WritingD,@RemedationTimeS,@RemedationTimeD,@ExplanationTimeS,@ExplanationTimeD,@TimeStampS,@TimeStampD,@ActiveTest,  
    @TestSubGroup,@Url,@PopHeight,@PopWidth,@DefaultGroup,@SecondPerQuestion,@ProgramofStudyId)  
    SET IDENTITY_INSERT Tests OFF  
 End  
 SET NOCOUNT OFF 
END 
GO

PRINT 'Finished creating PROCEDURE uspReleaseTestsToProduction'
GO 

