    
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForEnglishNursingTracking]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsForEnglishNursingTracking]
GO

PRINT 'Creating PROCEDURE uspGetTestsForEnglishNursingTracking'
GO
CREATE PROCEDURE [dbo].[uspGetTestsForEnglishNursingTracking]     
@CohortIds VARCHAR(MAX),    
@StudentIds VARCHAR(MAX)    
AS    
BEGIN    
     
 SET NOCOUNT ON    
 /*============================================================================================================    
 --      Purpose: Retrieves tests for given cohort ids     
 --    and student ids. Used by English Nursing Tracking.    
 --      Modified: 04/02/2012    
 --     Author:Mohan   
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
 Where (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))    
 AND (@StudentIds <> '0' AND dbo.UserTests.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))    
 AND (Tests.productid = 3 AND Isnull(UserTests.IsCustomizedFRTest,0) = 0)  

END


GO

PRINT 'Finished creating PROCEDURE uspGetTestsForEnglishNursingTracking'
GO 

