

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseLoginContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReleaseLoginContent]
GO
PRINT 'Creating PROCEDURE uspReleaseLoginContent'
GO  
   
CREATE PROCEDURE [dbo].[uspReleaseLoginContent]      
 @Id int,       
 @Content nvarchar(max),  
 @ReleasedBy int   
AS   
/*============================================================================================================      
//Purpose: Release Login content to production environment.
                  
//Created: Jan 29 2013    
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
 SET NOCOUNT ON;      
 UPDATE LoginContent   
    SET [Content] = @Content,  
        ReleasedContent = @Content,
        ReleaseStatus ='R',  
        ReleasedBy= @Releasedby,  
        ReleasedOn = getdate()    
    WHERE Id  = @Id      
END  

SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE uspReleaseLoginContent'
GO

 