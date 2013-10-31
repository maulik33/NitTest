SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignInstitutionsToAdmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignInstitutionsToAdmin]
GO

PRINT 'Creating PROCEDURE uspAssignInstitutionsToAdmin'
GO 
 
CREATE PROCEDURE [dbo].[uspAssignInstitutionsToAdmin]    
(    
@AdminId INT,    
@InstitutionId INT,    
@Active INT,    
@AssignedInstitutionIds VARCHAR(1000),   
@ProgramofStudyId INT  
)    
AS   
BEGIN 
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: To retrieve the assignment of institutions only which is active
 --      Modified: 06/24/2013 /06/26/2013  -NURSING:4128     
 --     Author:Liju       
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
 BEGIN TRANSACTION    
	DECLARE @SecurityLevel int
   SELECT @SecurityLevel = securityLevel FROM nuradmin WHERE userid=@AdminId
 
  IF (@SecurityLevel = 2) or (@SecurityLevel = 3)
   BEGIN
   UPDATE NurAdminInstitution      
   SET Active = 0      
   FROM NurAdminInstitution NAI    
   INNER JOIN NurInstitution NI on NAI.InstitutionID = NI.InstitutionID    
   WHERE AdminID = @AdminId      
   AND NAI.InstitutionID NOT IN (SELECT value FROM dbo.funcListToTableInt(@AssignedInstitutionIds,'|'))     
  END
 ELSE
  BEGIN
  UPDATE NurAdminInstitution      
  SET Active = 0      
  FROM NurAdminInstitution NAI    
  INNER JOIN NurInstitution NI on NAI.InstitutionID = NI.InstitutionID    
  WHERE AdminID = @AdminId      
  AND NAI.InstitutionID NOT IN (SELECT value FROM dbo.funcListToTableInt(@AssignedInstitutionIds,'|'))     
  AND (@ProgramofStudyId = 0 OR NI.ProgramOfStudyId = @ProgramofStudyId )  
  END     
	    
	 IF EXISTS (    
	   SELECT 1 FROM NurAdminInstitution    
	   WHERE AdminID = @AdminId    
	   AND InstitutionID = @InstitutionId    
		 )    
	  BEGIN    
	   UPDATE  NurAdminInstitution    
	   SET Active = @Active       
	   WHERE AdminID = @AdminId    
	   AND InstitutionID = @InstitutionId    
	      
	  END    
	 ELSE    
	  BEGIN    
	   INSERT INTO NurAdminInstitution    
	   (    
		AdminID,    
		InstitutionID,    
		Active    
	   )    
	   VALUES    
	   (    
	   @AdminId,    
	   @InstitutionId,    
	   @Active    
	   )    
	  END    
	  IF @@ERROR > 0    
	   ROLLBACK TRANSACTION    
	  ELSE    
	   COMMIT TRANSACTION  
END    
GO
PRINT 'Finished creating PROCEDURE uspAssignInstitutionsToAdmin'
GO 



