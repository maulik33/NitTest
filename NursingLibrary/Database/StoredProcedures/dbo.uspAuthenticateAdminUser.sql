IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAuthenticateAdminUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAuthenticateAdminUser]
GO
PRINT 'Creating PROCEDURE uspAuthenticateAdminUser'
GO
/****** Object:  StoredProcedure [dbo].[uspAuthenticateAdminUser]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAuthenticateAdminUser]    Script Date: 06/01/2011  ******/


CREATE PROCEDURE [dbo].[uspAuthenticateAdminUser]
 @UserName varchar(50),
 @UserPassword Varchar(50)
AS
 BEGIN
	  SET NOCOUNT ON
	/*============================================================================
	--     Purpose: Fix for NURSING-1220 (Removed admins can still log into RN)
	--     Modified: 03/06/2012
	--     Author:Atul Gupta
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
	   SELECT A.UserId,
		   A.FirstName,
		   A.LastName,
		   A.UserName,
		   A.UserPass ,
		   A.SecurityLevel,
		   A.Email,
		   A.UploadAccess
	   FROM dbo.NurAdmin A
	   WHERE  UserName = @UserName
			AND UserPass = @UserPassword
			AND AdminDeleteData IS NULL
  SET NOCOUNT OFF
 END
GO
PRINT 'Finished creating PROCEDURE uspAuthenticateAdminUser'
GO 
