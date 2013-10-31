IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAVPItems]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchAVPItems]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchAVPItems]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSearchAVPItems]
@TestName varchar(50),
@ProgramOfStudyId int
AS
BEGIN
/*============================================================================================================                
 -- Purpose: To retrieve AVP Items by TestName and program of study
 -- Modified to also search by program of study Nursing-4014
 -- Modified: 06/19/2013              
 -- Author:Glenn (original author unknown)     
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

	select * from Tests
	where ProductID=4
	and ActiveTest=1
	and TestSubGroup=10
	and ProgramOfStudyId = @ProgramOfStudyId
	and (TestName Like '%'+@TestName+'%' or @TestName = '')
END
GO
