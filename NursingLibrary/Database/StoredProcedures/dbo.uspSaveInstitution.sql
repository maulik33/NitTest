SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveInstitution]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveInstitution]
GO

CREATE PROCEDURE [dbo].[uspSaveInstitution]
	 @ProgramOfStudyId as int, 
	 @InstitutionID as int OUTPUT,    
	 @InstitutionName as nVarchar(80),    
	 @Description as nVarchar(80),    
	 @ContactName as nVarchar(50),    
	 @ContactPhone as varchar(50),    
	 @TimeZone as int,    
	 @IP as varchar(250),    
	 @FacilityID int,    
	 @CenterID as nVarchar(50),    
	 @ProgramID int,    
	 @CreateOrUpdatedUser as int,    
	 @DeleteUser as int,    
	 @Status as int,    
	 @AddressID as int,    
	 @Annotation as varchar(1000),    
	 @ContractualStartDate smalldatetime,    
	 @Email as nVarchar(100),  
	 @PayLinkEnabled as Bit,
	 @ProctorTrackSecurityEnabled as int    
AS    
BEGIN 
SET NOCOUNT ON                  
/*============================================================================================================                
 -- Purpose: Save the Institution
 -- Modified to save proctor track enabled status Changes done for Nursing-4484.
 -- Modified: 10/10/2011, 05/06/2013, 8/30/2013                
 -- Author:Kamal,Maulik            
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
  If(@InstitutionID = 0)    
   Begin    
    INSERT INTO NurInstitution (InstitutionName,Description,ContactName,ContactPhone,TimeZone,IP,Status,CreateDate,CreateUser,FacilityID,CenterID,ProgramID,DeleteUser,AddressID,Annotation,ContractualStartDate,Email,PayLinkEnabled,ProgramOfStudyId,ProctorTrackEnabled)    
    VALUES (@InstitutionName,@Description,@ContactName,@ContactPhone,@TimeZone,@IP,@Status,getdate(),@CreateOrUpdatedUser,@FacilityID,@CenterID,@ProgramID,@DeleteUser,@AddressID,@Annotation,@ContractualStartDate,@Email,@PayLinkEnabled,@ProgramOfStudyId,@ProctorTrackSecurityEnabled) 
    SET @InstitutionID = CONVERT(int, SCOPE_IDENTITY())    
   End    
  Else    
   Begin    
     UPDATE NurInstitution SET InstitutionName=@InstitutionName,Description=@Description,ContactName=@ContactName,    
     ContactPhone=@ContactPhone,TimeZone=@TimeZone,IP=@IP,Status=@Status,CenterID=@CenterID,    
     UpdateDate=getdate(),UpdateUser=@CreateOrUpdatedUser,FacilityID=@FacilityID,ProgramID=@ProgramID,DeleteUser=@DeleteUser,AddressID=@AddressID,Annotation =@Annotation,ContractualStartDate = @ContractualStartDate,    
     Email = @Email,PayLinkEnabled=@PayLinkEnabled,ProctorTrackEnabled = @ProctorTrackSecurityEnabled  
     WHERE InstitutionID=@InstitutionID    
   End 
END
SET NOCOUNT OFF                    
GO
PRINT 'Finished creating PROCEDURE uspSaveInstitution'
GO 
