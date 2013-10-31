SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSkillModuleUserVideos]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSkillModuleUserVideos]
GO

CREATE PROCEDURE [dbo].[uspGetSkillModuleUserVideos]      
  @SMUserId int     
 AS      
BEGIN      
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: To include the newly added field(IsPageViewedFully) for SMUserVideoTransaction       
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
 SELECT  UVT.SMUserVideoId,        
   UT.SMUserId,        
   UT.TestId,        
   UVT.IsPageFullyViewed,        
   UVT.SMOrder,    
   UVT.[Count],   
   UVT.IsVideoFullyViewed,        
   V.MP4,        
   V.Title,        
   V.Text,    
   V.Type,    
   V.F4V,    
   V.OGV,    
   V.TextPosition        
 FROM dbo.SMUserTransaction UT        
   INNER JOIN SMUserVideoTransaction UVT ON UT.SMUserId = UVT.SMUSERID        
   INNER JOIN dbo.SMVideoMapping VM on UVT.SMVideoMappingId = VM.SMVideoMappingId        
   INNER JOIN SkillsModuleVideos V ON V.SMVideoId = VM.SMVideoId        
 WHERE UT.SMUserId = @SMUserId    
     
 SET NOCOUNT OFF;      
END 

GO

PRINT 'Finished creating PROCEDURE uspGetSkillModuleUserVideos'
GO 
