
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAssetGroups]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAssetGroups]
GO
PRINT 'Creating PROCEDURE uspGetAssetGroups'
GO

/****** Object:  StoredProcedure [dbo].[uspGetAssetGroups]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO  
CREATE PROCEDURE uspGetAssetGroups      
(      
  @ProgramofStudyId AS INT      
)      
AS

BEGIN   
/*============================================================================      
 --     Modified: 05/28/2013      
 --     Author: Shodhan Kini
 --     Purpose: Get active Asset Groups for a Program Of Study (PN/RN).
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

	SELECT	AssetGroupId,
			AssetGroupName,
			ProgramOfStudyId,
			ProductId
	FROM	NurAssetGroup
	WHERE	ProgramOfStudyId = @ProgramofStudyId
	AND		Active = 1
END
GO
PRINT 'Finished creating PROCEDURE uspGetAssetGroups'
GO 

