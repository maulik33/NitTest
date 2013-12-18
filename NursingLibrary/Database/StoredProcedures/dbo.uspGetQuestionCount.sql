
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionCount]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQuestionCount]
GO

PRINT 'Creating PROCEDURE uspGetQuestionCount'
GO 

CREATE PROCEDURE [dbo].[uspGetQuestionCount]  
 @UserTestId int,  
 @TotalCount int Output  
AS  
BEGIN 

SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: Retrieves details for uspGetQuestionCount.        
 --      Modified: 05/08/2012        
 --      Author:Mohan       
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
  
 SELECT @TotalCount =COUNT(QID)FROM UserQuestions  
 WHERE UserTestId =@UserTestId  
END  
GO

PRINT 'Finished creating PROCEDURE uspGetQuestionCount'
GO 

