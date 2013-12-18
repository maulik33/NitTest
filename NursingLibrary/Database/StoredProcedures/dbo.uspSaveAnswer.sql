
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAnswer]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAnswer]
GO

PRINT 'Creating PROCEDURE uspSaveAnswer'
GO

CREATE PROCEDURE [dbo].[uspSaveAnswer]    
@QID INT ,    
@AIndex CHAR (1),    
@AText VARCHAR (3000),    
@Correct INT ,    
@AnswerConnectId INT ,    
@ActionType  INT ,    
@InitialPosition INT ,    
@Unit VARCHAR (50) ,    
@AlternateAText VARCHAR (3000),  
@AnswerId INT   
    
AS 
SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose:  Save/update answer choices.
              NURSING-4619(Missing Answer Choices Code Fix)
 -- Modified By: Shodhan
 -- Modified Date : 09/16/2013
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
 BEGIN    
 SET NOCOUNT ON    
  IF @AnswerId = 0      
 BEGIN   
    INSERT INTO AnswerChoices    
    (    
		QID,    
		AIndex,    
		AText,    
		Correct,    
		AnswerConnectID,    
		AType,    
		InitialPos,    
		Unit,    
		AlternateAtext    
    )    
    VALUES    
    (    
		@QID,    
		@AIndex,    
		@AText,    
		@Correct,    
		@AnswerConnectId,    
		@ActionType,    
		@InitialPosition,    
		@Unit,    
		@AlternateAtext    
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
          AType=@ActionType,  
          InitialPos=@InitialPosition,  
          Unit=@Unit,  
          AlternateAtext=@AlternateAtext  
      WHERE AnswerID=@AnswerId   
  END  
 SET NOCOUNT OFF    
 END 
GO
 PRINT 'Finished creating PROCEDURE uspSaveAnswer' 
GO

