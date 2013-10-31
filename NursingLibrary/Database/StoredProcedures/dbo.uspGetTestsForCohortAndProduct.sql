IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForCohortAndProduct]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestsForCohortAndProduct]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForCohortAndProduct]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestsForCohortAndProduct]
@ProductIds VARCHAR(MAX),
@InstitutionIds VARCHAR(MAX) = '',
@CohortIds VARCHAR(MAX),
@StudentIds VARCHAR(MAX),
@ProgramOfStudyId INT
AS
BEGIN
	SET NOCOUNT ON
	/*============================================================================================================                
	 -- Purpose:		Filter Tests By ProductIds, InstitutionIds and CohortIds
	 -- Modified For:	Sprint 47 Change made for Nursing-3615 to filter tests by optional ProgramOfStudyId
	 --					along with existing input parameters
	 -- Modified On:	06/27/2013
	 -- Modified By:	Atul
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
	SELECT DISTINCT Tests.TestName, UserTests.TestID
	From UserTests
	Join Tests on UserTests.TestID = Tests.TestID
	Where ((@ProductIds <> '0' AND Tests.ProductID IN (select value from  dbo.funcListToTableInt(@ProductIds,'|')))
		 OR @ProductIds = '0')
	AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
		 OR @CohortIds = '0')
	AND ( (@StudentIds <> '0' AND dbo.UserTests.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))
		 OR @StudentIds = '0')
	AND ( (@InstitutionIds <> '' AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))
		 OR @InstitutionIds = '')
	AND	( (@ProgramOfStudyId <> 0 AND TESTS.ProgramofStudyId = @ProgramOfStudyId) 
		OR @ProgramOfStudyId = 0)
	SET NOCOUNT OFF
END
GO

