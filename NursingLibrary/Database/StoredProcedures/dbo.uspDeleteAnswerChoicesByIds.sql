
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteAnswerChoicesByIds]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteAnswerChoicesByIds]
GO
PRINT 'Creating PROCEDURE uspDeleteAnswerChoicesByIds'
GO

Create proc [dbo].uspDeleteAnswerChoicesByIds  
 @AnswerIds Varchar(200)  
AS  
BEGIN 
 SET NOCOUNT ON
/*============================================================================================================            
 --  Purpose: Sprint 53: Delete answerchoices by answerchoice Ids.  
              Change done for NURSING-4619(Missing Answer Choices Code Fix) 
 --  Created - 09/20/2013        
 --  Author: Shodhan           
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
   DELETE  AnswerChoices  
   WHERE AnswerID IN (select * from dbo.funcListToTableInt(@AnswerIds,'|'))  
   SET NOCOUNT OFF 
END
GO

PRINT 'Finished creating PROCEDURE uspDeleteAnswerChoicesByIds'
GO 

