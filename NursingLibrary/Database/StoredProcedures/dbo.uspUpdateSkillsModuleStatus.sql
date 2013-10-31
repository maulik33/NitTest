SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateSkillsModuleStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateSkillsModuleStatus]
GO

CREATE PROCEDURE [dbo].[uspUpdateSkillsModuleStatus]    
    @SMUserVideoId INT    
AS     
BEGIN  
SET NOCOUNT ON  
/*============================================================================================================        
 --      Purpose: Updates skills module status.        
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
Declare @Count AS INT  
  
 SELECT @Count = ISNULL(Count,0)  FROM SMUserVideoTransaction  
 WHERE SMUserVideoId = @SMUserVideoId    
  
 SET @Count = @Count + 1  
  
 UPDATE dbo.SMUserVideoTransaction      
 SET IsPageFullyViewed = 1,    
     [Count] = @Count     
 WHERE SMUserVideoId = @SMUserVideoId 
  
END    
GO

PRINT 'Finished creating PROCEDURE uspUpdateSkillsModuleStatus'
GO 


