
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSMTestsByUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetSMTestsByUserId]
GO
PRINT 'Creating PROCEDURE uspGetSMTestsByUserId'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
     
CREATE PROCEDURE [dbo].[uspGetSMTestsByUserId]      
 @UserId int  
AS      
BEGIN  
SET NOCOUNT ON
	/*============================================================================================================
	--      Purpose: Retirves Skill module details for given user
	--      Modified: 07/04/2012
	--	    Author: Shodhan Kini
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
  SMTestId,      
  UserId,      
  TestId,  
  NewTestId  
 FROM dbo.SMTests      
 WHERE UserId = @UserId      
END 

GO
PRINT 'Finished creating PROCEDURE uspGetSMTestsByUserId'
GO 
