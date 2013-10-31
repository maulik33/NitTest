IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchGroups]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchGroups]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchGroups]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[uspSearchGroups]
(      
 @SearchString varchar(max),      
 @InstitutionIds varchar(8000),    
 @CohortIds varchar(8000)      
)      
AS      
SET NOCOUNT ON;
/*============================================================================================================                
 -- Purpose:		Returns Group list for the specified search criteria
 -- Modified For:	Sprint 44: PN RN Unification. Changes done for NURSING-3825
 --					to return Program Of Study Name for the institution.
 -- Modified On:	05/24/2013
 --	Modified By:	Atul Gupta
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
      
BEGIN      
	  SELECT      
	  C.InstitutionID,      
	  G.GroupName,      
	  C.CohortName,      
	  G.GroupID,      
	  C.CohortStatus,      
	  C.CohortID,      
	  I.InstitutionName,      
	  G.GroupDeleteDate,      
	  I.Status,
	  PS.ProgramofStudyName      
	  FROM dbo.NurGroup G      
	  LEFT OUTER JOIN dbo.NurCohort C      
	  ON G.CohortID = C.CohortID      
	  LEFT OUTER JOIN dbo.NurInstitution I      
	  ON C.InstitutionID = I.InstitutionID
	  INNER JOIN dbo.ProgramofStudy PS
	  ON I.ProgramOfStudyId = PS.ProgramofStudyId      
	  WHERE  (I.Status = 1      
	  OR I.Status IS NULL)      
	  AND (C.CohortStatus = 1      
	  OR C.CohortStatus IS NULL)      
	  AND (G.GroupDeleteDate IS NULL)      
	  AND (@InstitutionIds = ''      
	  OR C.InstitutionId IN      
	  (SELECT value      
	  FROM dbo.funcListToTableInt(@InstitutionIds,'|')))      
	  AND (@SearchString = ''      
		OR( GroupName like +'%'+ @SearchString + '%'      
		OR CohortName like +'%'+ @SearchString +'%'      
		OR InstitutionName like +'%' + @SearchString + '%'))      
	  AND (@CohortIds = ''    
	  OR C.CohortID IN    
	  (SELECT value FROM dbo.funcListToTableInt(@CohortIds,'|')))    
 END 
 GO
