IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspChangeLtiProviderStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspChangeLtiProviderStatus]
GO


/****** Object:  StoredProcedure [dbo].[uspChangeLtiProviderStatus]    Script Date: 09/13/2013 14:14:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[uspChangeLtiProviderStatus]
@LtiProviderId as int
AS

/*============================================================================================================                
 -- Purpose:  change lti provider status from active to inactive (or vice versa)
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
BEGIN
	SET NOCOUNT ON
	UPDATE LtiProviders
	SET Active=~Active,
	lastModifiedOn=getutcdate()
	WHERE id=@LtiProviderId
	SET NOCOUNT OFF
END




GO



print 'uspChangeLtiProviderStatus created successfully'
GO

