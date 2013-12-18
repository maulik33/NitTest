SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutions]
GO
PRINT 'Creating PROCEDURE uspGetInstitutions'
GO
CREATE PROCEDURE [dbo].[uspGetInstitutions]
(
 @InstitutionId int,
 @InstitutionIds varchar(max),
 @UserId int,
 @ProgramofStudyId int = 0
)
AS
BEGIN           
SET NOCOUNT ON                  
/*============================================================================================================                
 -- Purpose: To retrieve the Institutions based on the InstitutionIds and UserId
 -- Modified to return proctortrackenabled status.  Changes done for nursing-4484
 -- Modified to return ProgramofStudyName. Changes done for Nursing-3583.
 -- Modified to return the Institutions based on ProgramofStudyId
 -- Set default value for @ProgramofStudyId to '0' to return both PN and RN Institutions
 -- Modified to return only active institutions when searching for institutions with a particular admin
 -- Modified: 10/10/2011, 05/06/2013 ,05/14/2013,05/24/2013, 07/01/2013, 08/30/2013                
 -- Author:Kamal,Glenn,Maulik,Liju,Maulik          
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
   SELECT   
     P.ProgramofStudyName,  
	 InstitutionID,    
     InstitutionName,    
     [Description],    
     ContactName,    
     ContactPhone,    
     DefaultCohortID,    
     CenterID,    
     TimeZone,    
     IP,    
     FacilityID,    
     ProgramID,    
     0 AS Active,    
     I.[Status],    
     AddressID,    
     I.Annotation,    
     I.ContractualStartDate,    
     I.Email,  
  I.PayLinkEnabled,
  I.ProctorTrackEnabled  
  FROM dbo.NurInstitution I
  INNER JOIN ProgramofStudy P ON P.ProgramofStudyId = I.ProgramofStudyId      
  WHERE 0    
   IN (SELECT SecurityLevel -- Unfortunately 0 is assigned to Super Admin which is very risky.    
   FROM dbo.NurAdmin    
   WHERE UserId = @UserId) 
   AND ( @ProgramofStudyId = 0 
   OR I.ProgramofStudyId = @ProgramofStudyId)    
   AND (@InstitutionId = 0    
   OR I.InstitutionId = @InstitutionId)    
   AND (@InstitutionIds = ''    
   OR I.InstitutionId IN    
   (SELECT value  FROM dbo.funcListToTableInt(@InstitutionIds,'|'))) 
 END    
 ELSE    
 BEGIN    
  SELECT   
      P.ProgramofStudyName,  
	 I.InstitutionID,    
     InstitutionName,    
     [Description],    
     ContactName,    
     ContactPhone,    
     DefaultCohortID,    
     CenterID,    
     TimeZone,    
     IP,    
     FacilityID,    
     ProgramID,    
     NAI.Active,    
     I.[Status],    
     AddressID,    
     I.Annotation,    
     I.ContractualStartDate,    
     I.Email,  
  I.PayLinkEnabled,
  I.ProctorTrackEnabled    
  FROM dbo.NurInstitution I    
   INNER JOIN NurAdminInstitution NAI    
   ON NAI.InstitutionId = I.InstitutionId  
   INNER JOIN ProgramofStudy P ON P.ProgramofStudyId = I.ProgramofStudyId     
  WHERE NAI.AdminId = @UserId
  AND NAI.Active = 1    
   AND ( @ProgramofStudyId = 0 
   OR I.ProgramofStudyId = @ProgramofStudyId) 
   AND (@InstitutionId = 0    
   OR I.InstitutionId = @InstitutionId)    
   AND (@InstitutionIds = ''    
   OR I.InstitutionId IN    
      (SELECT value  FROM dbo.funcListToTableInt(@InstitutionIds,'|')))    
 END
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetInstitutions'
GO 


