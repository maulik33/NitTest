SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspResetSkillsModuleStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspResetSkillsModuleStatus]
GO

CREATE PROCEDURE [dbo].[uspResetSkillsModuleStatus]  
    @SMUserId INT  
AS  
 BEGIN            
   SET NOCOUNT ON          
/*============================================================================================================        
 --  Purpose: Reset 'IsPageFullyViewed' to 0 when all the pages are viewed one time
 --  Modified: 05/14/2012        
 --  Author:Liju       
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

Declare @Count AS INT

BEGIN TRY 
	SELECT @Count = ISNULL(Count,0)  FROM SMUserTransaction
	     WHERE SMUserId = @SMUserId  
	SET @Count = @Count + 1
	   BEGIN TRANSACTION      
		   UPDATE dbo.SMUserVideoTransaction      
		   SET IsPageFullyViewed = 0    
			   WHERE SMUserId = @SMUserId      
		   UPDATE dbo.SMUserTransaction      
			   SET [Status] = 1,    
				   [Count] = @Count     
			   WHERE SMUserId = @SMUserId      
	  COMMIT TRANSACTION    
 END TRY      
 BEGIN CATCH  
	  ROLLBACK TRANSACTION  
	  RAISERROR('Error in uspResetSkillsModuleStatus', 16, 1)  
 END CATCH  
 SET NOCOUNT OFF 
END 
GO

PRINT 'Finished creating PROCEDURE uspResetSkillsModuleStatus'
GO 


