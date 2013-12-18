

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateLoginContent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspUpdateLoginContent]
GO
PRINT 'Creating PROCEDURE uspUpdateLoginContent'
GO
CREATE PROCEDURE [dbo].[uspUpdateLoginContent]    
 @Id int,     
 @Content nvarchar(max),
 @ReleaseStatus char(1),
 @UpdaetdBy int    
AS   
/*============================================================================================================      
//Purpose: Update login page content by content id.
                  
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
BEGIN    
 SET NOCOUNT ON;    
 IF(@ReleaseStatus = 'R')
	BEGIN   
	   UPDATE LoginContent 
		SET [Content] = @Content,
			ReleasedContent = @Content,
			ReleaseStatus = @ReleaseStatus,
			UpdatedBy = @UpdaetdBy,
			UpdatedOn = getDate(),
			ReleasedBy = @UpdaetdBy,
			ReleasedOn = getDate()
		WHERE Id  = @Id  
	END
	ELSE
	BEGIN
		UPDATE LoginContent 
		SET [Content] = @Content,
			ReleaseStatus = @ReleaseStatus,
			UpdatedBy = @UpdaetdBy,
			UpdatedOn = getDate()
		WHERE Id  = @Id  
	END   
END 

SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE uspUpdateLoginContent'
GO

