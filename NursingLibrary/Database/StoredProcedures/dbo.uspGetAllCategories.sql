SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllCategories]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAllCategories]
GO
CREATE PROCEDURE [dbo].[uspGetAllCategories]  
@ProgramofStudyId int = 1
AS  
BEGIN           
SET NOCOUNT ON                  
/*============================================================================================================                
 -- Purpose: To retrieve the categories based on the ProgramofStudy
 -- Modified to default input parameter @ProgramofStudyId to RN Program of Study. Changes done for Nursing-3504.
 -- Modified: 04/30/2013, 05/01/2013                
 -- Author:Liju, Atul              
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
SELECT CategoryID, TableName, OrderNumber,ProgramofStudyName,C.programofStudyId
FROM Category C INNER JOIN ProgramofStudy P
ON P.ProgramofStudyId = C.programofStudyId
SET NOCOUNT OFF                    
END 
GO

PRINT 'Finished creating PROCEDURE uspGetAllCategories'
GO 
