
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUser]
GO

PRINT 'Creating PROCEDURE uspGetUser'
GO

CREATE PROCEDURE [dbo].[uspGetUser]      
@UserId VARCHAR(100),
@InstitutionId INT
AS   
SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose:		Retrieve User details based on Kaplan userid
 -- Modified By:	Shodhan 09/12/2013, Atul 09/27/2013
 -- Modified For:	Added support to pull out student record using Kaplan userid and Institutionid
 --					as part of changes done for Nursing-3985 (Integration from Kaptest) in Sprint 53
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
  
  IF (@InstitutionId <= 0)
	BEGIN
		SELECT UserID FROM NurStudentInfo WHERE KaplanUserID = @UserId     
	END
  ELSE
	BEGIN
		SELECT UserID FROM NurStudentInfo WHERE KaplanUserID = @UserId AND InstitutionID = @InstitutionId
	END	
  SET NOCOUNT OFF
GO
 PRINT 'Finished creating PROCEDURE uspGetUser' 
GO
