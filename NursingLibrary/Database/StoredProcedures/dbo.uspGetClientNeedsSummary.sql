SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedsSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetClientNeedsSummary]
GO

PRINT 'Creating PROCEDURE uspGetClientNeedsSummary'
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsSummary]  
 @ProgramofStudyId int    
AS        
 BEGIN   
 SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose: Return ClientNeeds based on ProgramofStudy 
 -- Modified On:	06/05/2013
 -- modified By:	Liju Mathews
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
  
SELECT dbo.ClientNeeds.ClientNeedsID,dbo.ClientNeeds.ClientNeeds, CNC.ClientNeedCategoryCount  
FROM dbo.ClientNeeds 
INNER JOIN  (SELECT dbo.ClientNeedCategory.ClientNeedID,COUNT(*) AS ClientNeedCategoryCount 
			 FROM dbo.ClientNeedCategory 
			 GROUP BY ClientNeedID )CNC  
ON dbo.ClientNeeds.ClientNeedsID= CNC.ClientNeedID  
WHERE programofStudyId = @programofStudyId
ORDER BY dbo.ClientNeeds.ClientNeedsID  
  
SET NOCOUNT OFF    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetClientNeedsSummary'
GO 




