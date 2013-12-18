SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspGetConceptsCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspGetConceptsCategory]
GO

PRINT 'Creating PROCEDURE UspGetConceptsCategory'
GO

CREATE PROCEDURE [dbo].[UspGetConceptsCategory]  
AS      
BEGIN      
  SET NOCOUNT ON                
/*============================================================================================================              
 --   Purpose: To retrieve the details of the newly added category(Concepts)     
 --   Author:Liju            
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
SELECT ConceptsID AS [Id], Concepts AS [Description], ProgramofStudyId   
FROM Concepts  
SET NOCOUNT OFF;               
END 
GO

PRINT 'Finished creating PROCEDURE UspGetConceptsCategory'
GO 


