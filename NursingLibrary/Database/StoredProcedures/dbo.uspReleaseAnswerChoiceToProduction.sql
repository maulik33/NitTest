
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseAnswerChoiceToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseAnswerChoiceToProduction]
GO

PRINT 'Creating PROCEDURE uspReleaseAnswerChoiceToProduction'
GO

CREATE PROC [dbo].[uspReleaseAnswerChoiceToProduction]      
 @QId as int,      
 @AIndex as char(1),      
 @AText as varchar(3000),      
 @Correct as int,      
 @AnswerConnectId as int,      
 @AType as int,      
 @InitialPos as int,      
 @Unit as char(50),      
 @AnswerId as int,      
 @AlternateAText as varchar(3000)      
AS      
BEGIN      
 SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose: TO release answer choice changes to production environment.
 -- Modified By: Shodhan 09/16/2013
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
SET IDENTITY_INSERT AnswerChoices ON     
IF NOT EXISTS(SELECT AnswerID FROM AnswerChoices WHERE AnswerID = @AnswerId)        
 BEGIN     
    INSERT INTO AnswerChoices      
   (    
    AnswerID,  
    QID,      
    AIndex,      
    AText,      
    Correct,      
    AnswerConnectID,      
    AType,      
    initialPos,      
    Unit,      
    AlternateAText      
   )      
    VALUES      
   (     
    @AnswerId,    
    @QId,      
    @AIndex,      
    @AText,      
    @Correct,      
    @AnswerConnectId,      
    @AType,      
    @InitialPos,      
    @Unit,      
    @AlternateAText      
   )      
 END    
 ELSE    
 BEGIN    
   UPDATE AnswerChoices       
      SET QID=@QID,    
          AIndex = @AIndex,     
          AText=@AText,    
          Correct=@Correct,    
          AnswerConnectID=@AnswerConnectId,    
          AType=@AType,    
          InitialPos=@InitialPos,    
          Unit=@Unit,    
          AlternateAtext=@AlternateAtext    
      WHERE AnswerID=@AnswerId     
 END    
     
 SET IDENTITY_INSERT AnswerChoices OFF        
SET NOCOUNT OFF     
END 
GO
 PRINT 'Finished creating PROCEDURE uspReleaseAnswerChoiceToProduction' 
GO

