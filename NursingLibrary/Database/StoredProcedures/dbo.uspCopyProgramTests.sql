
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCopyProgramTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCopyProgramTests]
GO

PRINT 'Creating PROCEDURE uspCopyProgramTests'
GO

CREATE PROCEDURE [dbo].[uspCopyProgramTests]    
 @OriginalProgramId INT,    
 @NewProgramId INT    
AS      
BEGIN   
SET NOCOUNT ON              
/*============================================================================================================            
 -- Purpose: Assign tests to copied program based on existing program.    
 -- modified: 10/07/2013            
 -- Author:Shodhan   
 -- Modified : As part of NURSING-3712(Copy Program Enhancement)       
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
 INSERT INTO NurProgramProduct (          
    ProgramID,          
    ProductID,          
    Type ,        
    AssetGroupId)    
  SELECT
   @NewProgramId, 
   ProductID,
   Type, 
   AssetGroupId 
  FROM NurProgramProduct    
  WHERE ProgramID = @OriginalProgramId  
  SET NOCOUNT OFF
END 

GO
 PRINT 'Finished creating PROCEDURE uspCopyProgramTests' 
GO



