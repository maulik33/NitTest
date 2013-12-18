    
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentByUserNameEmailId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentByUserNameEmailId]
GO

PRINT 'Creating PROCEDURE uspGetStudentByUserNameEmailId'
GO 

CREATE PROCEDURE [dbo].[uspGetStudentByUserNameEmailId]    
	 @UserName nvarchar(80),  
	 @Email  nvarchar(80)  
AS      
BEGIN   
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: Retrieves student info for given email id and  username.        
 --      Modified: 05/21/2012        
 --     Author:Shodhan       
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
 
 SELECT    
  NSI.UserID,    
  NSI.FirstName,    
  NSI.LastName ,    
  NSI.UserName ,    
  NSI.UserPass ,    
  NSI.Telephone ,    
  NSI.Email ,    
  NSI.UserType  ,    
  NSI.InstitutionID      
 FROM  NurStudentInfo NSI  
       INNER JOIN  NusStudentAssign NSA ON NSI.UserID = NSA.StudentId  
    INNER JOIN  NurCohort NC ON NSA.CohortID = NC.CohortID  
       INNER JOIN  NurInstitution NI ON NC.InstitutionID = NI.InstitutionID  
 Where  UserName = @UserName AND NSI.Email = @Email 
		AND CohortEndDate > getdate()  
		AND CohortStartDate < getdate()
        AND NC.CohortStatus = 1 AND NI.Status = 1 
		AND (NSI.UserExpireDate IS NULL OR NSI.UserExpireDate > getdate())
		AND (NSI.UserStartDate IS NULL OR NSI.UserStartDate < getdate())    
		AND (NSI.UserDeleteData IS NULL) 
END 

GO

PRINT 'Finished creating PROCEDURE uspGetStudentByUserNameEmailId'
GO 

