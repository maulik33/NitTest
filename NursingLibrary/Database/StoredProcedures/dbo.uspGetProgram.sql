IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspGetProgram]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspGetProgram]
(
	@ProgramId int
)
AS
/*============================================================================================================                
 -- Purpose:		Return details for a Program
 -- Modified For:	Sprint 43: Changes done for Nursing-3728 to return ProgramOfStudyId and ProgramOfStudyName
 --					along with other Program details
 -- Modified On:	05/15/2013
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
	SELECT P.ProgramID,
		P.ProgramName,
		P.[Description],
		P.ProgramOfStudyId,
		PS.ProgramofStudyName
	FROM dbo.NurProgram P
	INNER JOIN ProgramofStudy PS 
	ON P.ProgramofStudyId = PS.ProgramofStudyId
	WHERE P.ProgramId = @ProgramId
	AND P.DeletedDate IS NULL
GO
