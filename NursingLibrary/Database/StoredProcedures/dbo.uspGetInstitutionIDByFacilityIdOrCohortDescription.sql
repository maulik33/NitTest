IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionIDByFacilityIdOrCohortDescription]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionIDByFacilityIdOrCohortDescription]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIDByFacilityIdOrCohortDescription]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetInstitutionIDByFacilityIdOrCohortDescription]
@FacilityId int,
@ClassCode varchar(1000)
AS
/*====================================================================================================      
 --     Purpose: Retrieve InstitutionId using FacilityId (if mapped to single institution) 
 --				 Or Cohort Description (Class Code) (if FacilityId is mapped to multiple institutions)
 --				 Changes done for NURSING-3985 (Integration from Kaptest)
 --     Created: 08/27/2013      
 --     Author:	 Atul
 *****************************************************************************************************      
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
 *******************************************************************************************************/      
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @institutionMappedtoFacilityIdCount INT
	
	SET @institutionMappedtoFacilityIdCount = (SELECT COUNT(InstitutionID) FROM NurInstitution WHERE FacilityID = @FacilityId)
	
	IF (@institutionMappedtoFacilityIdCount = 1)
		BEGIN
			SELECT InstitutionID FROM NurInstitution WHERE FacilityID = @FacilityId
		END
	ELSE IF (@institutionMappedtoFacilityIdCount > 1)
		BEGIN
			SELECT InstitutionID from NurCohort WHERE CohortDescription = @ClassCode
		END

	SET NOCOUNT OFF;
END
GO

