
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USPSearchUnAssignedUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USPSearchUnAssignedUserDetails]
GO
PRINT 'Creating PROCEDURE USPSearchUnAssignedUserDetails'
GO
 
 CREATE PROCEDURE [dbo].[USPSearchUnAssignedUserDetails]       
 @SearchText Varchar(400),  
 @Usertype varchar(4)  
     
AS        
BEGIN                   
SET NOCOUNT ON  
/*============================================================================================================                
 -- Purpose: To get the details of Unassigned admin and user with encrypted password  -Nursing-4360              
 -- Created: 8/8/2013                        
 -- Author:Karthik CS 
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
IF(@UserType=1)      
 BEGIN            
 SELECT UserID,      
 UserName,      
 UserPass,      
 FirstName,      
 LastName,      
 i.institutionname FROM NurAdmin   
LEFT JOIN NurAdminInstitution ON NurAdmin.UserID=NurAdminInstitution.AdminID   
 LEFT JOIN NurInstitution i ON NurAdminInstitution.InstitutionID=i.InstitutionID       
 where UserID NOT IN(select adminid  from NurAdminInstitution)   
  AND AdminDeleteData is null   
  AND  (@SearchText = '' OR (UserID LIKE '%' + @SearchText + '%'        
    OR UserName LIKE '%' + @SearchText + '%'        
    OR UserPass LIKE '%' + @SearchText + '%'        
    OR FirstName LIKE '%' + @SearchText + '%'        
    OR LastName LIKE '%' + @SearchText + '%'))  
   END        
 ELSE       
 BEGIN   
 SELECT UserID,      
 UserName,      
 UserPass,      
 FirstName,      
 LastName,      
 I.Institutionname      
 FROM NurStudentInfo LEFT JOIN NurInstitution i ON NurStudentInfo.InstitutionID=i.InstitutionID        
 WHERE (@SearchText = '' OR(UserID LIKE '%' + @SearchText + '%'        
    OR UserName LIKE '%' + @SearchText + '%'        
    OR UserPass LIKE '%' + @SearchText + '%'        
    OR FirstName LIKE '%' + @SearchText + '%'        
    OR LastName LIKE '%' + @SearchText + '%'))    
   AND NurStudentInfo.InstitutionID=0     
  END      
 SET NOCOUNT OFF                            
END   
GO
PRINT 'Finished creating PROCEDURE USPSearchUnAssignedUserDetails'
GO

      
      
  
  