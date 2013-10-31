
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAssetGroupByProgramId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAssetGroupByProgramId]
GO
PRINT 'Creating PROCEDURE uspGetAssetGroupByProgramId'
GO

/****** Object:  StoredProcedure [dbo].[uspGetAssetGroupByProgramId]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO  
CREATE PROCEDURE uspGetAssetGroupByProgramId      
(      
  @ProgramId AS INT      
)      
AS     
BEGIN   
/*============================================================================      
 --     Modified: 05/28/2013      
 --     Author: Shodhan  
 --     Purpose: Get Asset Groups by program Id.
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
Select PP.AssetGroupId, AssetGroupName,Active,ProgramOfStudyId     
FROM NurAssetGroup AS ag INNER JOIN     
NurProgramProduct AS pp ON ag.AssetGroupId = pp.AssetGroupId    
WHERE pp.ProgramID = @ProgramId and ISNULL(PP.ProductID,0) = 0   
END
GO
PRINT 'Finished creating PROCEDURE uspGetAssetGroupByProgramId'
GO 

