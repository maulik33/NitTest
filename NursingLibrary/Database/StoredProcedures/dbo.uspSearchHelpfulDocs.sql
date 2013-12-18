SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchHelpfulDocs]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchHelpfulDocs]
GO

GO
PRINT 'Creating PROCEDURE uspSearchHelpfulDocs'
GO
CREATE PROCEDURE [dbo].[uspSearchHelpfulDocs]    
 @SearchKeyword VARCHAR(200),    
 @Status INT,  
 @IsLink BIT   
AS   
/*============================================================================================================      
//Purpose: Retrive Helpful document details   
                  
//Modified: March 08 2012      
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
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
SET NOCOUNT ON;    
    
SELECT  [Id],    
  [Title],    
  [Description],    
  [GUID],    
  [FileName],    
  [Type],    
  [Size],    
  [Status],    
  [CreatedDate],    
  [CreatedBy],    
  [LastName],    
  [FirstName],  
  [IsLink]    
FROM HelpfulDocuments SD    
 INNER JOIN NurAdmin A ON SD.CreatedBy = A.UserId    
WHERE [Status] = @Status   
 AND ISNULL(IsLink,0)= @IsLink   
 AND (@SearchKeyword = ''    
 OR [Title] LIKE '%'+ @SearchKeyword + '%'    
 OR [Description] LIKE '%'+ @SearchKeyword + '%'    
 OR A.FirstName LIKE '%'+ @SearchKeyword + '%'    
 OR A.LastName like '%'+ @SearchKeyword + '%')    
    
END 

GO
PRINT 'Finished creating PROCEDURE uspSearchHelpfulDocs'
GO

