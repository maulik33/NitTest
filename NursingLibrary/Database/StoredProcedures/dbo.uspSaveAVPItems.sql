IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAVPItems]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAVPItems]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAVPItems]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAVPItems]
@TestID INT OUTPUT,
@TestName varchar(50),
@Url as nVarchar(200),
@PopHeight as int,
@PopWidth as int,
@ProgramOfStudyId as int
AS
BEGIN
/*============================================================================================================                
 -- Purpose: To save AVP items
 -- Modified to also save program of study information Nursing-4014
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

	If(@TestID =0)
	 Begin
		 INSERT INTO Tests(ProductID,TestName,ActiveTest,TestSubGroup,Url,PopHeight,PopWidth,ReleaseStatus, ProgramofStudyId)
		 values(4,@TestName,1,10,@Url,@PopHeight,@PopWidth,'E', @ProgramOfStudyId)
		 set @TestID = Scope_Identity()
	 End
	Else
	--note that program of study id can NOT be updated on edit as per business rules
	 Begin
		 UPDATE  Tests  SET TestName=@TestName,Url=@Url,PopHeight=@PopHeight,PopWidth=@PopWidth,ReleaseStatus='E'
		 Where TestID = @TestID
	 End
END
GO
