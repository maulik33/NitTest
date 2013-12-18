
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentByUserName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentByUserName]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
PRINT 'Creating PROCEDURE uspGetStudentByUserName' 
GO
CREATE PROCEDURE [dbo].[uspGetStudentByUserName]      
  @UserName VARCHAR(100)    
AS   
SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose:  Get Student detail by username
 -- Modified By: Shodhan 09/12/2013
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
  BEGIN    
        SELECT UserId,UserName  
        FROM NurStudentInfo    
        WHERE UserName = @UserName    
  END 
  SET NOCOUNT OFF
GO
 PRINT 'Finished creating PROCEDURE uspGetStudentByUserName' 
GO
