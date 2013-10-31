  
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLoginContentById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetLoginContentById]
GO
PRINT 'Creating PROCEDURE uspGetLoginContentById'
GO
CREATE PROCEDURE [dbo].[uspGetLoginContentById]  
 @Id int  
AS  
BEGIN  
/*============================================================================================================      
//Purpose: Retrive login page content.
                  
//Created: Jan 24 2013    
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
  Select Id,
		 [Content],
		 ReleaseStatus,
	     ReleasedContent
  from dbo.LoginContent    
  Where Id = @Id     
END  

SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE uspGetLoginContentById'
GO

