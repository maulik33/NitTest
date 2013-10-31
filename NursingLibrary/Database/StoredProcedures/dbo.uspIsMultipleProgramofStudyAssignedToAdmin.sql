
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspIsMultipleProgramofStudyAssignedToAdmin]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspIsMultipleProgramofStudyAssignedToAdmin]
GO
PRINT 'Creating PROCEDURE uspIsMultipleProgramofStudyAssignedToAdmin'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[uspIsMultipleProgramofStudyAssignedToAdmin]  
(  
 @AdminId INT,
 @IsMultiplePSAssigned BIT OUTPUT
)  
AS  
BEGIN                   
 SET NOCOUNT ON;    
/*============================================================================================================                  
 -- Purpose:  Check program of study 
 -- Modified For: Sprint 46: Changes done for NURSING-3613   
 --     Check multiple program of study type assigned to Admin
 -- Modified On: 05/15/2013  
 -- Modified By: Shodhan Kini  
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
 Declare @programofStudyCount as int   
  SELECT   @programofStudyCount = COUNT(DISTINCT I.ProgramOfStudyId)  FROM dbo.NurAdminInstitution AS AI 
    INNER JOIN dbo.NurInstitution AS I ON AI.InstitutionID = I.InstitutionID
   WHERE AI.AdminId = @AdminId AND Active = 1
    
   SET @IsMultiplePSAssigned = 0
   IF(@programofStudyCount > 1)
   BEGIN
    SET @IsMultiplePSAssigned = 1
   END
 SET NOCOUNT OFF            
END
GO
PRINT 'Finished creating PROCEDURE uspIsMultipleProgramofStudyAssignedToAdmin'
GO 