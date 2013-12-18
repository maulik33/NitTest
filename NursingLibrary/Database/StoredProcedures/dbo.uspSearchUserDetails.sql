
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchUserDetails]
GO
PRINT 'Creating PROCEDURE uspSearchUserDetails'
GO
CREATE PROCEDURE [dbo].[uspSearchUserDetails]        
(        
 @SearchText varchar(200),        
 @UserType varchar(4) ,  
 @ProgramOfStudy int     
)        
AS        
BEGIN                   
SET NOCOUNT ON                          
/*============================================================================================================                  
 -- Purpose: To get the details of the admin and user with encrypted password  -Nursing-4360    
             To get the details of the admin and student with program of study -Nursing-4360
             To get the details of the admin and student with program of study of active admins-Nursing-4360      
 -- Created: 1/8/2013                          
 -- Author:Karthik CS   
 --Modified by:Karthik CS                    
 --Modified Date:7/8/2013 ,14/8/2013
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
 i.institutionname,  
 p.ProgramofStudyName          
 FROM NurAdmin INNER JOIN NurAdminInstitution ON UserID=NurAdminInstitution.AdminID           
 INNER JOIN NurInstitution i ON NurAdminInstitution.InstitutionID=i.InstitutionID          
 LEFT JOIN ProgramofStudy p ON i.ProgramofStudyId = p.ProgramofStudyId               
 WHERE (UserID LIKE '%' + @SearchText + '%'            
    OR UserName LIKE '%' + @SearchText + '%'            
    OR UserPass LIKE '%' + @SearchText + '%'            
    OR FirstName LIKE '%' + @SearchText + '%'            
    OR LastName LIKE '%' + @SearchText + '%')       
    AND i.ProgramOfStudyId=@ProgramOfStudy   
    AND NurAdminInstitution.Active=1
 END            
 ELSE         
 BEGIN         
 SELECT UserID,        
 UserName,        
 UserPass,        
 FirstName,        
 LastName,        
 I.Institutionname,
 p.ProgramofStudyName         
 FROM NurStudentInfo INNER JOIN NurInstitution i ON NurStudentInfo.InstitutionID=i.InstitutionID 
 LEFT JOIN ProgramofStudy p ON i.ProgramofStudyId = p.ProgramofStudyId                   
 WHERE (UserID LIKE '%' + @SearchText + '%'          
    OR UserName LIKE '%' + @SearchText + '%'          
    OR UserPass LIKE '%' + @SearchText + '%'          
    OR FirstName LIKE '%' + @SearchText + '%'          
    OR LastName LIKE '%' + @SearchText + '%')       
    AND i.ProgramOfStudyId=@ProgramOfStudy        
 END   
 SET NOCOUNT OFF 
 END                           
GO
PRINT 'Finished creating PROCEDURE uspSearchUserDetails'
GO

