
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchUnAssignedAdmins]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchUnAssignedAdmins]
GO

PRINT 'Creating PROCEDURE USPSearchUnAssignedAdmins'
GO

CREATE PROCEDURE [dbo].[USPSearchUnAssignedAdmins]   
 @SearchString Varchar(400)
AS    
BEGIN               
SET NOCOUNT ON  
/*============================================================================================================
--  Purpose  : Retrieving all the admins who are not assigned any Institutions  
--  Created : 07/23/2013
--	Author   :Liju Mathews
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
    SELECT A.UserId,      
        A.FirstName,      
        A.LastName,      
        A.UserName,      
        A.UserPass ,      
        '' AS InstitutionName,     
        '' AS ProgramofStudyName,      
        S.AdminType  
    FROM dbo.NurAdmin A  
    LEFT JOIN dbo.NurAdminSecurity S      
    ON A.SecurityLevel = S.SecurityLevel        
    WHERE Userid not in (select AdminId from NuradminInstitution)  
    AND AdminDeleteData is null
    AND (@SearchString = '' OR( UserName like +'%'+ @SearchString + '%' OR FirstName like + '%'+ @SearchString + '%' OR LastName like + '%'+ @SearchString + '%' ))        
  END      
  SET NOCOUNT OFF     
END 
GO
PRINT 'Finished creating PROCEDURE USPSearchUnAssignedAdmins'
GO 

