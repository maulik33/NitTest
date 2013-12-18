IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionDetailsById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionDetailsById]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionDetailsById]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetInstitutionDetailsById]
@InstitutionID VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	/*============================================================================================================        
	--  Purpose: Get Institution Details
	--	Modified For: Sprint 47. Returning ProgramOfStudyId as part of changes done for Nursing-3615
	--	Modified Date: 07/10/2013        
	--  Modified By: Atul      
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
	
	SELECT InstitutionID, InstitutionName, ProgramOfStudyId
	FROM NurInstitution
	WHERE Status=1
	AND ((@InstitutionID <> '0' AND InstitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionID,',')))
		OR @InstitutionID = '0')
	ORDER BY InstitutionName
	
	SET NOCOUNT OFF
END
GO
