
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSMTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSMTest]
GO

PRINT 'Creating PROCEDURE uspGetSMTest'

GO 
CREATE PROCEDURE [dbo].[uspGetSMTest]         
 @NewTestId int,  
 @UserId as int    
AS      
BEGIN  
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: Retrieve skills module test id.        
 --      Modified: 05/23/2012        
 --      Author:Shodhan       
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
  SMT.SMUserId    
 FROM SMTests ST   
      INNER JOIN SMUserTransaction SMT ON ST.TestId = SMT.TestId    
 WHERE   ST.NewTestId = @NewTestId  AND ST.UserId = @UserId AND SMT.userid = @UserId   
END  

GO

PRINT 'Finished creating PROCEDURE uspGetSMTest'
GO 

    