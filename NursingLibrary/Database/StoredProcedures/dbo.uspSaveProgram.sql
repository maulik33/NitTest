IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveProgram]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspSaveProgram]
(
	@ProgramId int OUTPUT,
	@ProgramName varchar(200),
	@Description varchar(200),
	@UserId int,
	@ProgramOfStudyId int
)
AS
/*============================================================================================================                
 -- Purpose:		Create/Update a Program
 -- Modified For:	Sprint 43: Changes done for Nursing-3728 to save ProgramOfStudyId
 --					while creating/updating a Program
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

	IF ISNULL(@ProgramId, 0) = 0
	BEGIN
		INSERT INTO dbo.NurProgram (ProgramName, [Description], CreateUser, CreateDate, ProgramOfStudyId)
		VALUES (@ProgramName, @Description, @UserId, GETDATE(), @ProgramOfStudyId)
		
		SET @ProgramId = CONVERT(int, SCOPE_IDENTITY())
	END
	ELSE
		UPDATE dbo.NurProgram
		SET	ProgramName = @ProgramName,
			[Description] = @Description,
			UpdateUser = @UserId,
			UpdateDate = GETDATE()
		WHERE ProgramId = @ProgramId
GO
