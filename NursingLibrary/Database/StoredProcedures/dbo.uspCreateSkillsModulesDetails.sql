SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateSkillsModulesDetails]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateSkillsModulesDetails]
GO
CREATE PROCEDURE [dbo].[uspCreateSkillsModulesDetails]            
  @TestId int,       
  @UserId int,      
  @SMUserId int OUTPUT        
AS         
BEGIN            
   SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: To include a new field IsPageFullyViewed in the SMUserVideoTransaction
 --      Modified: 05/14/2012        
 --     Author:Liju       
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
 IF EXISTS (select TestId from SMVideoMapping  where TestId=@TestId)
	 BEGIN
		 IF EXISTS (Select SMUT.TestId, SMUT.UserId From SMUserTransaction SMUT Where SMUT.TestId = @TestId And SMUT.UserId = @UserId)    
			BEGIN     
				SET @SMUserId = (Select SMUT.SMUserId From SMUserTransaction SMUT Where SMUT.TestId = @TestId And SMUT.UserId = @UserId )   
			END     
		 ELSE    
			BEGIN     
			 INSERT INTO [SMUserTransaction]([UserId],[TestId],[Status],[Count])       
				 VALUES (@UserId,@TestId,0,0);      
			 INSERT INTO [SMUserVideoTransaction]([SMUserId],[IsPageFullyViewed],[SMVideoMappingId],[SMOrder],[Count],[IsVideoFullyViewed])       
				(SELECT SMUT.SMUserId,'False', SMVM.SMVideoMappingId, SMVM.OrderNumber,0,'False' FROM SMUserTransaction SMUT      
				 INNER JOIN SMVideoMapping SMVM ON SMUT.TestId = SMVM.TestId Where SMUT.TestId = @TestId And SMUT.UserId = @UserId)        
				 SET @SMUserId = (Select SMUT.SMUserId From SMUserTransaction SMUT Where SMUT.TestId = @TestId And SMUT.UserId = @UserId )     
			END      
	  END
  ELSE
	  BEGIN
		SET @SMUserId = 0
	  END  
SET NOCOUNT OFF;         
END

GO

PRINT 'Finished creating PROCEDURE uspCreateSkillsModulesDetails'
GO 

