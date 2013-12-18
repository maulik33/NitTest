SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateTestStatusOnExpiry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspUpdateTestStatusOnExpiry]
GO
/****** Object:  StoredProcedure [dbo].[uspUpdateTestStatusOnExpiry]    Script Date: 12/21/2011 17:13:54 ******/
PRINT 'Creating PROCEDURE uspUpdateTestStatusOnExpiry'
GO

CREATE PROCEDURE [dbo].[uspUpdateTestStatusOnExpiry]  
(  
 @userTestId int,   
 @testStatus int,  
 @timeRemaining int,  
 @QuestionId int  
)  
AS  
/*============================================================================================================      
//Purpose: For Updating the Timeremaining to 0 and Teststatus to complete   
           when time expires for an IT Test       
//modified: Dec 29 2011      
//Author:Shodhan      
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
SET NOCOUNT ON      
 DECLARE @ErrorMsg varchar(255)  
 Declare @TotalTimeRemaining  as int  
BEGIN    
	    
     IF EXISTS( SELECT ID FROM UserQuestions WHERE UserTestID = @userTestId AND QID = @QuestionId AND Correct = 2)
     BEGIN
         SELECT @TotalTimeRemaining = TimeRemaining    
		 FROM Usertests where UserTestId = @userTestId 
     
		 UPDATE UserQuestions  
		 SET TimeSpendForQuestion = @TotalTimeRemaining  
		 Where QID = @QuestionId AND  UserTestID = @userTestId  
     END 
  
	 UPDATE UserTests  
	 SET TestStatus = @testStatus, TimeRemaining =@timeRemaining,TestComplited = GetDate()  
	 WHERE UserTestID = @userTestId  
END       
IF @@ERROR <> 0       
 BEGIN      
   SET @ErrorMsg = 'Error while updating the TestStatus for an IT Test(Time Expiry)'      
  GOTO ERROR      
 END       
SET NOCOUNT OFF      
 RETURN @@ERROR      
ERROR:      
   RAISERROR(60100, 18, 1, @ErrorMsg)      
    RETURN -1
GO
PRINT 'Finished creating PROCEDURE uspUpdateTestStatusOnExpiry'
GO






