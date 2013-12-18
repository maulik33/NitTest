
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateQuestionLog]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateQuestionLog]
GO

CREATE PROC dbo.uspUpdateQuestionLog        
 @QId AS INT,        
 @UpdatedDate AS DateTime,        
 @UpdatedBy AS INT,        
 @ReleasedDate AS DateTime,        
 @ReleasedBy AS INT        
 AS        
 BEGIN   
   SET NOCOUNT ON                
/*============================================================================================================              
 --  Purpose: Sprint 53:Retrieve the question log as part of NURSING-4683   
 --  Modified as part Sprint 53 ticket number NURSING-4619    
 --  Modified On - 09/20/2013 , 09/25/2013       
 --  Author: Shodhan  ,Shodhan          
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
  UPDATE QuestionLogs        
   SET         
       UpdatedDate = @UpdatedDate,        
       UpdatedBy = @UpdatedBy,        
       ReleasedDate = @ReleasedDate,        
       ReleasedBy = @ReleasedBy        
   WHERE QId = @QID      
   SET NOCOUNT OFF      
 END  
 GO

PRINT 'Finished creating PROCEDURE uspUpdateQuestionLog'
GO 

