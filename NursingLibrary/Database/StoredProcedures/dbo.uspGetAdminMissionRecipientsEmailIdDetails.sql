

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminMissionRecipientsEmailIdDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAdminMissionRecipientsEmailIdDetails]
GO
PRINT 'Creating PROCEDURE uspGetAdminMissionRecipientsEmailIdDetails'
GO

CREATE PROCEDURE [dbo].[uspGetAdminMissionRecipientsEmailIdDetails]      
   @UserIds VARCHAR(MAX),    
   @InstitutionIds VARCHAR(MAX)    
AS    
 /*============================================================================================================      
//Purpose: Retrieve admin recipient for Email validation   
                  
//Created: Nov 07 2012      
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
  IF  @UserIds != ''  
  Begin  
 -- get Admin level data      
	  SELECT C.Email, 5 AS SelectionLevel, CONVERT(varchar(300), C.FirstName + ' ' + C.LastName) COLLATE database_default AS Name,C.UserId as UserId,C.UserName as UserName     
	  FROM NurAdmin C      
	  WHERE C.UserId in ( select value from dbo.funcListToTableInt(@UserIds,'|')) AND C.AdminDeleteData IS NULL      
  END     
  ELSE IF @InstitutionIds != ''  
   BEGIN        
  -- get institution level data      
	   SELECT F.Email, 1 AS SelectionLevel, C.InstitutionName AS Name,F.UserId as UserId,F.UserName as UserName   
	   FROM  NurAdminInstitution D     
		JOIN NurInstitution C ON C.InstitutionID = D.InstitutionID      
		JOIN NurAdmin F ON D.AdminID = F.UserID      
	   WHERE  D.InstitutionID in (select value from dbo.funcListToTableInt(@InstitutionIds,'|'))     
   END  
END     
SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE uspGetAdminMissionRecipientsEmailIdDetails'
GO

