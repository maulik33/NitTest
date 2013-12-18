SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsForCohort]
GO

GO
PRINT 'Creating PROCEDURE uspGetTestsForCohort'
GO

CREATE PROCEDURE [dbo].[uspGetTestsForCohort]
(
@ProgramId INT,
@CohortId INT,
@SearchString varchar(1000)
)
AS
/*============================================================================================================      
//Purpose: Retrive Test date details for cohort    
                  
//Modified: March 06 2012, June 04 2013      
//Modified September 26, 2013 to fix bug in join with products table
//Modified on 10/04/2013 : Reverting to previous version for NURSING-4780 resolution in Sprint 54
//Author:Shodhan, Glenn, Atul      
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
	SELECT    
	 PP.ProgramProductID,    
	 PP.ProgramID,    
	 (    
	  SELECT StartDate from NurProductDatesCohort WHERE CohortID = @CohortId    
	  -- AND ProductID=" + TestID    
	  AND ProductID = PP.ProductID    
	  AND Type = PP.Type    
	 ) AS StartDate ,    
	 (    
	  SELECT EndDate from NurProductDatesCohort WHERE CohortID = @CohortId    
	  AND ProductID = PP.ProductID    
	  AND Type = PP.Type    
	 ) AS EndDate ,    
	 PP.ProductID,    
	 PP.Type AS AssignType,    
	 CASE WHEN PP.Type = 0 THEN T.ProductID ELSE 3 END AS TestType,    
	 PP.OrderNo,    
	 CASE WHEN (PP.Type = 1 OR PP.TYPE = 2) THEN P.ProductName ELSE T.TestName END AS TestName    
	 FROM  dbo.NurProgramProduct PP    
	 INNER JOIN   dbo.Tests T    
	 ON PP.ProductID = T.TestID    
	 LEFT OUTER JOIN  dbo.Products P    
	 ON PP.ProductID = P.ProductID    
	 WHERE ProgramID= @programId and T.ActiveTest=1    
	 AND (@SearchString = ''    
	  OR( T.TestName like +'%'+ @SearchString +'%'))    
	 Order By TestType,TestName 
END
GO
PRINT 'Finished creating PROCEDURE uspGetTestsForCohort'
GO
