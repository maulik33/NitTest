
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUniqueUserName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUniqueUserName]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
PRINT 'Creating PROCEDURE uspGetUniqueUserName' 
GO
CREATE PROCEDURE [dbo].[uspGetUniqueUserName]             
 @FirstName VARCHAR(80),      
 @LastName VARCHAR(80)        
AS     
SET NOCOUNT ON
 /*============================================================================================================        
//Purpose: Get unique username based on first and last name                   
//Created: 9/12/2013        
//Author:Shodhan        
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
DECLARE @BaseUserName VARCHAR(100),@likeBaseUserName VARCHAR(100), @NewUsername VARCHAR(80)    
DECLARE @seq INT,@baseUserLength as int     
      
  SET @BaseUserName = SUBSTRING (@FirstName, 1, 1) + @LastName    
  SET @likeBaseUserName = @BaseUserName +'%'    
  SET @baseUserLength =LEN(@BaseUserName)+1    
      
   SELECT @seq = (    
   SELECT TOP 1 CAST(RIGHT(UserName,(PATINDEX('%[^0-9]%',REVERSE(UserName)) - 1))AS INT) AS sequence    
   FROM dbo.NurStudentInfo    
   WHERE username like @likeBaseUserName AND (ISNUMERIC(SUBSTRING(UserName, @baseUserLength, LEN(UserName))) = 1)     
   ORDER BY sequence DESC    
   )    
  set @NewUsername = @BaseUserName + CONVERT(varchar,(isnull(@seq,0) + 1))    
        
 select @NewUsername as username    
 End  
 SET NOCOUNT OFF
GO
 PRINT 'Finished creating PROCEDURE uspGetUniqueUserName' 
GO

