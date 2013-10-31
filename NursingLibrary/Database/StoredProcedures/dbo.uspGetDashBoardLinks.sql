
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDashBoardLinks]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetDashBoardLinks]
GO
PRINT 'Creating PROCEDURE uspGetDashBoardLinks'
GO

/****** Object:  StoredProcedure [dbo].[uspGetDashBoardLinks]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE uspGetDashBoardLinks    
(    
  @ProgramId AS INT    
)    
AS    
Begin  
	/*============================================================================      
 --     Modified: 05/28/2013      
 --     Author: Shodhan  
 --     Purpose: Get dashboard links by program id      
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
  SELECT P.ProgramOfStudyId,AssetLocationOrder,AssetName,AssetValue,DL.AssetLocationType     
  FROM NurProgram AS P    
  INNER JOIN  NurProgramProduct AS PP ON P.ProgramId = PP.ProgramID    
  INNER JOIN  NurAssetGroup AS AG ON PP.AssetGroupId = AG.AssetGroupId    
  INNER JOIN NurAsset AS A ON AG.AssetGroupId = A.AssetGroupId    
  INNER JOIN DashboardLinkLocation AS DL ON A.AssetLocationType = DL.AssetLocationType    
  WHERE PP.ProgramID = @ProgramId AND AG.AssetGroupId in(1,7)    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetDashBoardLinks'
GO 

