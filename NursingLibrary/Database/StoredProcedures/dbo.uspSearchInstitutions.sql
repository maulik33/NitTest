SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchInstitutions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchInstitutions]
GO
CREATE PROCEDURE [dbo].[uspSearchInstitutions]
(
 @SearchText varchar(200),
 @UserId int
)
AS
BEGIN           
SET NOCOUNT ON                  
/*============================================================================================================        
 -- Purpose: Search Institutions by searchText and UserId
 -- Modified to return ProgramofStudyName. Changes done for Nursing-3583.
 -- Modified: 10/10/2011, 05/07/2013                
 -- Author:Kamal,Glenn,Maulik            
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
 -- Query is same in both Then and Else blocks except that
 --  check SecurityLevel = 0 for user accessing Then block
 --  do a inner join to NurAdminInstitution table in Else block
 IF NOT EXISTS(SELECT 1
  FROM dbo.NurAdminInstitution
  WHERE AdminId = @UserId)
 BEGIN
	  SELECT InstitutionID,
	   InstitutionName,
	   I.[Description],
	   ContactName,
	   ContactPhone,
	   DefaultCohortID,
	   CenterID,
	   TimeZone,
	   TZ.Description TZD,
	   IP,
	   FacilityID,
	   ProgramID,
	   [Status],
	   P.ProgramofStudyName
  FROM dbo.NurInstitution I
	  INNER JOIN TimeZones AS TZ ON TZ.TimeZoneID = I.TimeZone
	  INNER JOIN ProgramofStudy P ON P.ProgramofStudyId = I.ProgramofStudyId      
	  WHERE 0 IN (SELECT SecurityLevel -- Unfortunately 0 is assigned to Super Admin which is very risky.
	   FROM dbo.NurAdmin
	   WHERE UserId = @UserId)
		  AND (I.InstitutionName LIKE '%' + @SearchText + '%'
		  OR I.[Description] LIKE '%' + @SearchText + '%'
		  OR I.ContactName LIKE '%' + @SearchText + '%'
		  OR I.ContactPhone LIKE '%' + @SearchText + '%'
		  OR I.CenterID LIKE '%' + @SearchText + '%')
 END
 ELSE
 BEGIN
	  SELECT I.InstitutionID,
	   InstitutionName,
	   I.[Description],
	   ContactName,
	   ContactPhone,
	   DefaultCohortID,
	   CenterID,
	   TimeZone,
	   TZ.Description TZD,
	   IP,
	   FacilityID,
	   ProgramID,
	   [Status],
	   P.ProgramofStudyName
  FROM dbo.NurInstitution I
	  INNER JOIN NurAdminInstitution NAI  ON NAI.InstitutionId = I.InstitutionId
	  INNER JOIN TimeZones AS TZ  ON TZ.TimeZoneID = I.TimeZone
	  INNER JOIN ProgramofStudy P ON P.ProgramofStudyId = I.ProgramofStudyId      
	  WHERE NAI.AdminId = @UserId
		  AND (I.InstitutionName LIKE '%' + @SearchText + '%'
		  OR I.[Description] LIKE '%' + @SearchText + '%'
		  OR I.ContactName LIKE '%' + @SearchText + '%'
		  OR I.ContactPhone LIKE '%' + @SearchText + '%'
		  OR I.CenterID LIKE '%' + @SearchText + '%')
 END
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished creating PROCEDURE uspSearchInstitutions'
GO 
