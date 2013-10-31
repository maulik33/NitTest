    
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSkillsModulesAvailableQuizzes]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSkillsModulesAvailableQuizzes]
GO

PRINT 'Creating PROCEDURE uspGetSkillsModulesAvailableQuizzes'
GO 
CREATE PROCEDURE [dbo].[uspGetSkillsModulesAvailableQuizzes]     
@UserId int,  
@TimeOffset int,
@ProductId int  
AS    
BEGIN  

SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: Retrieves details for uspGetSkillsModulesAvailableQuizzes.        
 --      Modified: 05/06/2012        
 --     Author:Mohan       
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
 
SELECT   
	UT.UserTestID,
	UT.UserId,
	UT.TestID,
	UT.TestNumber,  
    DATEADD(hour, @TimeOffset, UT.TestStarted) as TestStarted,
    UT.ProductID,
    TS.TestName    
FROM  UserTests UT   
  INNER JOIN Tests TS ON TS.TestId = UT.TestId    
  WHERE UserID= @UserId
  AND UT.ProductID= @ProductId
  AND TS.ReleaseStatus ='F'    
   ORDER BY TS.TestName     
END
GO

PRINT 'Finished creating PROCEDURE uspGetSkillsModulesAvailableQuizzes'
GO 



  