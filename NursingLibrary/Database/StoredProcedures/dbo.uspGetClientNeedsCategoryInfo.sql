SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedsCategoryInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetClientNeedsCategoryInfo]
GO

PRINT 'Creating PROCEDURE uspGetClientNeedsCategoryInfo'
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsCategoryInfo]
@UserID int,
@ProgramofStudyId int	
AS
BEGIN   
 SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose: Retrieves the ClientNeedcategoryInfo based on ProgramofStudyId
 -- modified On:	06/07/2013
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
 
SELECT dbo.ClientNeeds.ClientNeedsID,dbo.ClientNeedCategory.ClientNeedCategoryID,
dbo.ClientNeedCategory.ClientNeedCategory,
dbo.ReturnAllQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@ProgramofStudyId ) AS TotQCount,
dbo.ReturnUnUsedIncorrectQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID,@ProgramofStudyId) AS UnUsedIncorrectQCount,
dbo.ReturnUnUsedQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID,@ProgramofStudyId) AS UnUsedQCount,
dbo.ReturnIncorrectQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID,@ProgramofStudyId) AS InCorrectQCount
FROM dbo.ClientNeeds INNER JOIN  dbo.ClientNeedCategory
ON dbo.ClientNeeds.ClientNeedsID = dbo.ClientNeedCategory.ClientNeedID
WHERE dbo.ClientNeeds.ProgramofStudyId = @ProgramofStudyId

SET NOCOUNT OFF    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetClientNeedsCategoryInfo'
GO 


