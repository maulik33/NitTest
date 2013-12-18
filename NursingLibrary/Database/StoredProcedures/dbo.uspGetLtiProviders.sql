
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLtiProviders]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLtiProviders]
GO
/****** Object:  StoredProcedure [dbo].[uspGetLtiProviders]    Script Date: 09/13/2013 14:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetLtiProviders]    Script Date: 05/17/2011  ******/
PRINT 'Creating PROCEDURE uspGetLtiProviders' 
GO
CREATE PROCEDURE [dbo].[uspGetLtiProviders]
 @LTIProviderId int
AS

/*============================================================================================================                
 -- Purpose:  retrieve LtiProviders based on provided id (or retrieve all if id = 0
 -- Modified By: Glenn 09/13/2013
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
SET NOCOUNT ON
 BEGIN

  SELECT   L.id, L.name, L.title, L.url, L.[description], L.consumerKey, L.consumerSecret, L.customParameters, L.active   
  FROM dbo.LTIProviders L    
  WHERE (@LTIProviderId = 0    
   OR L.id = @LTIProviderId)

END
GO
PRINT 'uspGetLtiProviders created successfully'
GO



