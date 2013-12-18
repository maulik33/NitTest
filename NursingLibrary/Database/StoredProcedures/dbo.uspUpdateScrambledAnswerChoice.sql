SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateScrambledAnswerChoice]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspUpdateScrambledAnswerChoice]
GO
PRINT 'Creating PROCEDURE uspUpdateScrambledAnswerChoice'
GO

CREATE PROCEDURE [dbo].[uspUpdateScrambledAnswerChoice]   
 @ScrambledAnswerChoice varchar(50),  
 @UserTestID int,  
 @QuestionId int  
AS  
BEGIN 
 SET NOCOUNT ON     
 /*============================================================================================================    
 //Purpose: To Update the ScrambledAnswerChoice in the UserQuestion Table for IT Test                    
 //Created: March 20 2013      
 //Author: Liju  
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
 UPDATE UserQuestions   
 SET ScrambledAnswerChoice = @ScrambledAnswerChoice  
 WHERE UserTestID = @UserTestID 
   AND QID = @QuestionId 
 SET NOCOUNT OFF 
END  
GO
PRINT 'Finished creating PROCEDURE uspUpdateScrambledAnswerChoice'
GO






