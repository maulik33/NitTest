/****** Object:  StoredProcedure [dbo].[uspGetTestsForStudentReportCard]    Script Date: 03/13/2012 02:36:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForStudentReportCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForStudentReportCard]
GO
PRINT 'Creating PROCEDURE uspGetTestsForStudentReportCard'
GO
/****** Object:  StoredProcedure [dbo].[uspGetTestsForStudentReportCard]    Script Date: 03/13/2012 02:36:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetTestsForStudentReportCard]
	@ProductIds VARCHAR(MAX),        
	@StudentIds VARCHAR(MAX)
AS
BEGIN
	
	SET NOCOUNT ON
	/*============================================================================================================
	--      Purpose: Retrieves tests for given product ids, cohort ids 
	--				and student ids. Used by Student Report Card.
	--      Modified: 07/04/2012
	--	    Author: Shodhan Kini
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
	Where (@ProductIds <> '0' AND Tests.ProductID IN (select value from  dbo.funcListToTableInt(@ProductIds,'|')))
	AND (@StudentIds <> '0' AND dbo.UserTests.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))
	
	SET NOCOUNT OFF
END
GO
PRINT 'Finished creating PROCEDURE uspGetTestsForStudentReportCard'
GO 


