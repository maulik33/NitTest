SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteProgramFromProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteProgramFromProduct]
GO

CREATE PROCEDURE [dbo].[uspDeleteProgramFromProduct]  
  @ProgramId int,   
  @ProductId int,   
  @Type int ,
  @AssetGroupId int
AS 
BEGIN  
SET NOCOUNT ON          
/*============================================================================================================        
 --Purpose: To delete an asset from a program
 -- modified: 05/28/2013        
 -- Author:Liju     
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
IF (@ProductId != 0)
BEGIN  
 DELETE FROM NurProgramProduct  
 WHERE ProgramID = @ProgramId  
 AND ProductID = @ProductId  
 AND Type = @Type  
END  
ELSE
BEGIN  
 DELETE FROM NurProgramProduct  
 WHERE ProgramID = @ProgramId  
 AND AssetGroupId = @AssetGroupId 
 AND Type = @Type  
END  
SET NOCOUNT OFF
END
GO
PRINT 'Finished creating PROCEDURE uspDeleteProgramFromProduct'
GO 


