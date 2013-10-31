SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestsToProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignTestsToProgram]
GO

CREATE PROCEDURE [dbo].[uspAssignTestsToProgram]      
(      
@ProgramId INT,      
@TestId INT,      
@Type INT,    
@AssetGroupId INT    
)      
AS   
BEGIN    
SET NOCOUNT ON            
/*============================================================================================================          
 --Purpose: To assign a test to a program 
        Sprint 54 : NURSING-3712(Copy Program Enhancement)
 -- modified: 05/28/2013,10/01/2013          
 -- Author:Liju       
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
      
IF NOT EXISTS (      
 SELECT 1 FROM dbo.NurProgramProduct      
 WHERE ProgramID = @ProgramId      
 AND Type = @Type      
 AND ProductID = @TestId  
 AND AssetGroupId =  @AssetGroupId  
 )      
  BEGIN      
   INSERT INTO NurProgramProduct      
   (      
    ProgramID,      
    ProductID,      
    Type ,    
    AssetGroupId    
   )      
   VALUES      
   (      
    @ProgramId ,      
    @TestId,      
    @Type ,    
    @AssetGroupId    
   )        
 END      
SET NOCOUNT OFF  
END 
GO
PRINT 'Finished creating PROCEDURE uspAssignTestsToProgram'
GO 


