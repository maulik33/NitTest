IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAuditTrailsByStudentID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAuditTrailsByStudentID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAuditTrailsByStudentID]
 @StudentId INT
AS
BEGIN
 
 SET NOCOUNT ON;
 /*============================================================================================================  
//Purpose: Get AuditTrails for Student 
//Created: 05/14/13  
//Author: Maulik Shah
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
 SELECT AuditTrailId,
        StudentId,
		StudentUserName,
		FromInstitution,
		FromCohort,
		FromGroup,
		ToInstitution,
		ToCohort,
		ToGroup,
		DateMoved,
		AdminUserId,
		AdminUserName
 FROM   NurAuditTrail
 WHERE StudentId = @StudentId
 ORDER BY  DateMoved desc
SET NOCOUNT OFF 
END
GO
PRINT 'Finished creating PROCEDURE uspGetAuditTrailsByStudentID'
GO 

