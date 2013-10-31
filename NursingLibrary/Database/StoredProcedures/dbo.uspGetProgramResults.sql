SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

/****** Object:  StoredProcedure [dbo].[uspGetProgramResults]    Script Date: 05/28/2012 17:13:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProgramResults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetProgramResults]
GO
  
CREATE PROCEDURE [dbo].[uspGetProgramResults]    
 -- Add the parameters for the stored procedure here    
 @userTestId int, @chartType int  
AS   
BEGIN  

SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: To make the Total round of to 1 decimal point     
 --      Modified: 05/28/2012        
 --     Author:Liju     
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
    
 IF (@chartType = 1)    
 BEGIN    
 SELECT  ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total ,   
      -1 AS N_Correct,    
      -1 AS N_InCorrect,    
      -1 AS N_NAnswered,    
      -1 AS N_CI,    
      -1 AS N_II,    
     -1 AS N_IC    
  FROM UserQuestions    
  WHERE UserTestId = @userTestId    
 END    
 ELSE IF (@chartType = 2)    
 BEGIN    
  SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct,    
      SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END) AS N_InCorrect,    
      SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END) AS N_NAnswered,    
      SUM(CASE WHEN AnswerChanges = 'CI' THEN 1 ELSE 0 END) AS N_CI,    
      SUM(CASE WHEN AnswerChanges = 'II' THEN 1 ELSE 0 END) AS N_II,    
      SUM(CASE WHEN AnswerChanges = 'IC' THEN 1 ELSE 0 END) AS N_IC, 
      ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total 
  FROM UserQuestions    
  WHERE UserTestId = @userTestId    
 END    
END    
GO
PRINT 'Finished creating PROCEDURE uspGetProgramResults'
GO 




