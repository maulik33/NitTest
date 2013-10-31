SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForProgram]
GO
 
PRINT 'Creating PROCEDURE uspGetTestsForProgram'
GO 
  
CREATE PROCEDURE [dbo].[uspGetTestsForProgram]    
(    
@ProgramId INT,    
@SearchString varchar(1000)    
)    
AS 
BEGIN  
SET NOCOUNT ON          
/*============================================================================================================        
 --Purpose: To get the assets assigned to a program
 -- modified: 05/29/2013
 -- modified 09/26/2013 to fix bug in join           
 -- Modified on 10/04/2013 : Reverting to previous version for NURSING-4780 resolution in Sprint 54
 -- Author:Liju, Glenn, Atul
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
    
 SELECT PP.ProgramProductID,        
   PP.ProgramID,        
   PP.ProductID,        
   CASE WHEN [Type] IN (1, 2) THEN P.ProductName ELSE        
   (        
    SELECT  ProductName        
    FROM    dbo.Tests TP        
    INNER JOIN dbo.Products PR        
    ON TP.ProductID = PR.ProductID WHERE TestID = PP.ProductID        
   )        
   END AS TestCategory,        
   PP.[Type] AS TestType,        
   CASE WHEN [Type] IN (1, 2) THEN P.ProductID ELSE T.ProductID END ,        
   PP.OrderNo,        
   CASE WHEN [Type] IN (1, 2) AND ISNULL(P.Bundle, 0) = 1 THEN P.ProductName ELSE T.TestName END AS TestName,      
   PP.AssetGroupId,      
   CASE WHEN [Type] = 1 THEN 1  
        WHEN [Type] = 2 THEN 2   
   ELSE  
      CASE WHEN ISNULL(PP.ProductId, 0) = 0 THEN NAG.ProgramofStudyId ELSE T.ProgramofStudyId END   
   END AS ProgramofStudyId,    
   PS.ProgramofStudyName,  
   NAG.AssetGroupName  
 FROM  dbo.NurProgramProduct PP        
 LEFT JOIN dbo.Tests T  ON PP.ProductID = T.TestID  AND T.ActiveTest = 1  AND [Type] = 0        
 LEFT JOIN dbo.Products P ON PP.ProductID = P.ProductID  AND [Type] IN (1, 2)     
 LEFT JOIN dbo.NurAssetGroup NAG ON PP.AssetGroupId = NAG.AssetGroupId        
 LEFT JOIN dbo.programofStudy PS 
 ON PS.ProgramofStudyId =(CASE WHEN [Type] = 1 THEN 1 WHEN [Type] = 2 THEN 2 ELSE  
                          CASE WHEN ISNULL(PP.ProductId, 0) = 0 THEN NAG.ProgramofStudyId ELSE T.ProgramofStudyId END END) 
 WHERE PP.ProgramID = @ProgramId        
 AND (@SearchString = '' OR( T.TestName like +'%'+ @SearchString +'%'))  
SET NOCOUNT OFF    
END    
GO

PRINT 'Finished creating PROCEDURE uspGetTestsForProgram'
GO 




