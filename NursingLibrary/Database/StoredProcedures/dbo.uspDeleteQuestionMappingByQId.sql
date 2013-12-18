SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteQuestionMappingByQId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteQuestionMappingByQId]
GO

CREATE PROCEDURE [dbo].[uspDeleteQuestionMappingByQId]
	@QId int
AS      
BEGIN     
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: To delete the questionMappings in the LookupMappings Table
 --      Created: 10/22/2013          
 --      Author:Liju        
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
	DELETE dbo.[LookupMappings]
	WHERE MappedTo = @QId and TypeId in (18,19,21,22)
	SET NOCOUNT OFF   
END
GO
PRINT 'Finished creating PROCEDURE uspDeleteQuestionMappingByQId'
GO


 

