
/****** Object:  StoredProcedure [dbo].[uspSaveCustomTest]    Script Date: 04/04/2013 17:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveCustomTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveCustomTest]
GO
PRINT 'Creating PROCEDURE uspSaveCustomTest'
GO
CREATE PROCEDURE [dbo].[uspSaveCustomTest]  
 @TestId as int OUTPUT,  
 @Name varchar(50),  
 @ProductId int,  
 @DefaultGroup int,
 @SecondPerQuestion int,
 @ProgramofStudyId int
AS  
 BEGIN            
   SET NOCOUNT ON         
/*============================================================================================================  
//Purpose: To include the newly added column ''SecondPerQuestion'' 
//       : To Associate Test with RN/PN added ProgramofStudyId [04/04/13]
//Modified: June 06/12,04/04/13  
//Author: Liju
//LastModifiedBy : Shodhan  
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
	IF @DefaultGroup < 1 SET @DefaultGroup = NULL 
	 If(@TestId = 0)  
	  Begin  
		INSERT INTO Tests (TestName, ProductID, DefaultGroup, ReleaseStatus, ActiveTest, TestSubGroup ,SecondPerQuestion,ProgramofStudyId)  
		   VALUES (@Name, @ProductId, @DefaultGroup, 'E', 1, 1 , @SecondPerQuestion,@ProgramofStudyId)  
		   set @TestId = CONVERT(int, SCOPE_IDENTITY())  
	    End  
		Else  
		 Begin  
		   UPDATE Tests SET ProductID = @ProductId, TestName = @Name, DefaultGroup = @DefaultGroup, ReleaseStatus = 'E' ,SecondPerQuestion = @SecondPerQuestion
		   WHERE TestID = @TestId  
		 End  
 SET NOCOUNT OFF 
END 
GO
PRINT 'Finished creating PROCEDURE uspSaveCustomTest'
GO


