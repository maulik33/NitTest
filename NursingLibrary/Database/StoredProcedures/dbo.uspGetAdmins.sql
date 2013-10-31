
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdmins]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdmins]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdmins]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetAdmins]    Script Date: 05/17/2011  ******/
PRINT 'Creating PROCEDURE uspGetAdmins' 
GO
CREATE PROCEDURE [dbo].[uspGetAdmins]    
 @UserId int,    
 @SearchString Varchar(400)    
AS   

/*============================================================================================================                
 -- Purpose:  retrieve Amin details based on search criteria
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
  SET NOCOUNT ON    
  SELECT UserID,UserName,UserPass,Email,FirstName,LastName,SecurityLevel,UploadAccess    
  FROM NurAdmin    
  WHERE (@UserId = 0 OR UserID  = @UserId)    
  AND (@SearchString = ''    
  OR( UserName like +'%'+ @SearchString + '%' ))    
END  
SET NOCOUNT OFF
GO
 PRINT 'Finished creating PROCEDURE uspGetAdmins' 
GO

