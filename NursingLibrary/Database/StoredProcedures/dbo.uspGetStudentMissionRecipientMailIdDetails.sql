
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentMissionRecipientMailIdDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudentMissionRecipientMailIdDetails]
GO
PRINT 'Creating PROCEDURE uspGetStudentMissionRecipientMailIdDetails'
GO
CREATE PROCEDURE [dbo].[uspGetStudentMissionRecipientMailIdDetails]           
   @UserIds VARCHAR(MAX),    
   @GroupIds VARCHAR(MAX),    
   @InstitutionIds VARCHAR(MAX),    
   @CohortIds VARCHAR(MAX)    
AS  
/*============================================================================================================      
//Purpose: Retrieve all mission recepient for validation purpose   
                  
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
SET NOCOUNT ON     
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
 IF  @UserIds != ''  
  Begin  
	  SELECT SI.Email, 4 AS SelectionLevel, SI.FirstName + ' ' + SI.LastName AS Name,SI.UserId as UserId,SI.UserName as UserName    
	  FROM  NurStudentInfo SI    
	  WHERE SI.UserId in(select value from dbo.funcListToTableInt(@UserIds,'|')) AND SI.UserDeleteData IS NULL      
  END     
  ELSE IF   @GroupIds != ''  
  BEGIN  
 -- get group level data      
	  SELECT SI.Email, 3 AS SelectionLevel, C.GroupName AS Name,SI.UserId as UserId,SI.UserName as UserName  
	  FROM  NurGroup C    
	   JOIN NusStudentAssign AS SA ON SA.GroupID = C.GroupID      
	   JOIN NurStudentInfo SI ON SA.StudentID = SI.UserID      
	  WHERE SA.GroupID in(select value from dbo.funcListToTableInt(@GroupIds,'|'))     
	  AND SI.UserDeleteData IS NULL AND SA.DeletedDate IS NULL      
  END      
  ELSE IF   @CohortIds != ''  
  BEGIN   
    -- get cohort level data      
	  SELECT SI.Email, 2 AS SelectionLevel, C.CohortName AS Name, SI.UserId, SI.UserName as UserName  
	  FROM NurCohort C    
	   JOIN NusStudentAssign SA  ON SA.CohortID = C.CohortID     
	   JOIN NurStudentInfo SI ON SA.StudentID = SI.UserID       
	  WHERE SA.cohortid in(select value from dbo.funcListToTableInt(@CohortIds,'|')) AND SI.UserDeleteData IS NULL   
 END  
 ELSE IF   @InstitutionIds != ''  
  BEGIN   
 -- get institution level data      
	  SELECT SI.Email, 1 AS SelectionLevel, C.InstitutionName AS Name, SI.UserId as UserId,SI.UserName as UserName    
	  FROM NurInstitution C      
	   JOIN NurStudentInfo SI ON C.InstitutionID = SI.InstitutionID      
	  WHERE SI.InstitutionID in(select value from dbo.funcListToTableInt(@InstitutionIds,'|')) AND SI.UserDeleteData IS NULL        
 END    
END 
SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE uspGetStudentMissionRecipientMailIdDetails'
GO

