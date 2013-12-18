    
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQIDForEnglishNursingTracking]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetQIDForEnglishNursingTracking]
GO

PRINT 'Creating PROCEDURE uspGetQIDForEnglishNursingTracking'
GO
CREATE PROCEDURE [dbo].[uspGetQIDForEnglishNursingTracking]         
@CohortIds VARCHAR(MAX),        
@StudentIds VARCHAR(MAX),       
@Testids VARCHAR(MAX)      
AS        
BEGIN        
         
 SET NOCOUNT ON        
/*============================================================================================================    
 --      Purpose: Retrieves questions for given cohort ids,     
 --      student ids and Testids. Used by English Nursing Tracking.    
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
      
Select Distinct    
  TQ.QID, QU.QuestionID      
From       
  Questions QU       
  Inner Join TestQuestions TQ on QU.QID = TQ.QID    
  Inner Join UserTests UT on UT.TestID = TQ.TestID      
Where QU.AlternateStem is not null      
  AND (@CohortIds <> '0' AND UT.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))      
  AND (@StudentIds <> '0' AND UT.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))      
  AND (@Testids <> '0' AND UT.TestID IN (select value from  dbo.funcListToTableInt(@Testids,'|')))      
       
END 
GO

PRINT 'Finished creating PROCEDURE uspGetQIDForEnglishNursingTracking'
GO 

