SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveHelpfulDocuments]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveHelpfulDocuments]
GO

PRINT 'Creating PROCEDURE uspSaveHelpfulDocuments'
GO


/****** Object:  StoredProcedure [dbo].[uspSaveHelpfulDocuments]    Script Date: 10/10/2011 15:49:07 ******/

CREATE PROC [dbo].[uspSaveHelpfulDocuments]    
(    
  @Id int OUTPUT,    
  @FileName nVarchar(100),    
  @Title nVarchar(100),    
  @Type nVarchar(50),    
  @Size float,    
  @CreatedDate DateTime,    
  @Description nVarchar(1000),    
  @Status int,    
  @GUID as nVarchar(100),    
  @CreatedBy int ,  
  @IsLink bit  
)    
AS
/*============================================================================================================      
//Purpose: Save Helpful document/Link details   
                  
//Modified: March 09 2012      
//Author:Liju      
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
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
SET NOCOUNT ON;        
 IF ISNULL(@Id, 0) = 0    
 BEGIN    
  INSERT INTO HelpfulDocuments    
  ([FileName],    
  [Title],    
  [Type],    
  [Size],    
  CreatedDate,    
  [Description],    
  [Status],    
  [GUID],    
  CreatedBy,  
  IsLink)    
  VALUES    
  (@FileName,    
   @Title,    
   @Type,    
   @Size,    
   @CreatedDate,    
   @Description,    
   @Status,    
   @GUID,    
   @CreatedBy,  
   @IsLink)    
   SET @Id = CONVERT(int, SCOPE_IDENTITY())    
 END    
 ELSE    
 BEGIN    
 IF ISNULL(@Filename,'') = ''  
 BEGIN
 UPDATE HelpfulDocuments  
  SET [Title] = @Title,      
      [Description] = @Description
  WHERE Id = @Id 
  END
  ELSE
  BEGIN
  UPDATE HelpfulDocuments      
  SET [Title] = @Title,      
      [Description] = @Description,
      [FileName] = @FileName   
  WHERE Id = @Id  
  END    
 END   
 END  
 GO  
PRINT 'Finished creating PROCEDURE uspSaveHelpfulDocuments'
GO
