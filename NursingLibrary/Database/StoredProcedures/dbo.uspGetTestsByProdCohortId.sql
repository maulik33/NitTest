

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByProdCohortId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsByProdCohortId]
GO
PRINT 'Creating PROCEDURE uspGetTestsByProdCohortId'
GO
CREATE PROCEDURE [dbo].[uspGetTestsByProdCohortId]  
@ProductIds VARCHAR(200),  
@CohortIds VARCHAR(MAX) 
AS  
BEGIN  
   
 SET NOCOUNT ON  
 /*============================================================================================================    
 //Purpose: Modified to get the results for Essential Nursing Links also    
 //modified: 03/20/2012    
 //Author: Shodhan    
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
  
 SELECT DISTINCT Tests.TestName, UserTests.TestID  
 From UserTests  
 Join Tests on UserTests.TestID = Tests.TestID  
 Where  Tests.ProductID IN (select value from  dbo.funcListToTableInt(@ProductIds,'|')) 
 AND  dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
  
  
 SET NOCOUNT OFF  
END 
GO
PRINT 'Finished creating PROCEDURE uspGetTestsByProdCohortId'
GO 

