SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetHelpfulDocuments]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetHelpfulDocuments]
GO

GO
PRINT 'Creating PROCEDURE uspGetHelpfulDocuments'
GO
CREATE PROCEDURE [dbo].[uspGetHelpfulDocuments]  
 @Id as int,  
 @Guid as nVarchar(100)  
AS  
  /*============================================================================================================      
//Purpose: Retrive Helpful doc details   
                  
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
SET NOCOUNT ON 
BEGIN  
 SELECT Id,  
 Title,  
 [Description],  
 [FileName],  
 [Type],  
 [Size],  
 [Status],  
 CreatedDate,  
 [GUID],  
 [CreatedBy],  
 FirstName,  
 LastName,
 IsLink  
 FROM  dbo.HelpfulDocuments sd  
 INNER JOIN dbo.NurAdmin n  
 ON  n.UserID = sd.CreatedBy  
 WHERE (Id = @Id OR @Id = 0)  
 AND ([GUID] = @Guid OR @Guid = '')  
END  
GO

PRINT 'Finished creating PROCEDURE uspGetHelpfulDocuments'
GO
