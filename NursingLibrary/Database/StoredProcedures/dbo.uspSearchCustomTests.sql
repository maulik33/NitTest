IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchCustomTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchCustomTests]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchCustomTests]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspSearchCustomTests]
(
 @ProgramOfStudyId INT,
 @TestName VARCHAR(50)
)
AS
/*============================================================================================================        
 --		 Modified For:	Sprint 43: PN RN Unification. Changes done for Nursing-2985 to filter test list 
 --						for Program of Study in addition to Test Name.
 --      Modified:		05/08/2013
 --      Modified By:	Atul
 --		 Sample Usage:	Exec uspSearchCustomTests @ProgramOfStudyId=1, @TestName=''
 **************************************************************************************************************        
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
	 SELECT TestID,TestName,ProductID,DefaultGroup 
	 FROM	dbo.Tests  
	 WHERE	ProgramofStudyId = @ProgramOfStudyId 
			AND	TestName LIKE '%'+@TestName+'%' 
			AND	(ActiveTest=1)
			AND	ISNULL(ReleaseStatus, 'R') != 'F'
END
GO
