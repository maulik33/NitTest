SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetFinishedTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetFinishedTests]
GO

CREATE PROCEDURE [dbo].[uspGetFinishedTests]  
 @userId int, @productId int, @timeOffset int  
AS    
BEGIN  
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: To make the percent correct field to 1 decimal place        
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
  
 IF (@productId != 0)  
  SELECT CAST(QInfo.PercentCorrect as varchar(5))+'%'  as PercentCorrect, QInfo.QuestionCount,QInfo.UserTestID,  
   UserTestsInfo.TestName, UserTestsInfo.TestStarted, UserTestsInfo.TestID, UserTestsInfo.TestStatus, UserTestsInfo.ProductName,  
   UserTestsInfo.QuizOrQBank  
  FROM (SELECT (CAST((ISNULL(QCC.QuestionCorrectCount,0)*100.0) / QC.QuestionCount AS decimal(4,1))) AS PercentCorrect,  
    QC.QuestionCount,QC.UserTestID  
     FROM (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCount  
     FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  
     WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.ProductID = @productId AND  
      dbo.UserTests.TestStatus = 1  
     GROUP BY dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN  
    (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCorrectCount  
     FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  
     WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.ProductID = @productId AND dbo.UserTests.TestStatus = 1  
     AND dbo.UserQuestions.Correct = 1  
     GROUP BY dbo.UserQuestions .UserTestID)QCC ON QC.UserTestID = QCC.UserTestID) QInfo INNER JOIN  
     (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,  
      DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.UserTests.TestID, dbo.UserTests.TestStatus,  
      dbo.Products.ProductName,dbo.UserTests.QuizOrQBank  
      FROM dbo.Tests INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID  
      INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID  
      AND (dbo.Tests.ProductID = @productId)  
      WHERE (UserID = @userId) and TestStatus=1 )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID  
  
 ELSE  
  SELECT CAST(QInfo.PercentCorrect as varchar(5)) + '%' as PercentCorrect, QInfo.QuestionCount,QInfo.UserTestID,  
   UserTestsInfo.TestName, UserTestsInfo.TestStarted, UserTestsInfo.TestID, UserTestsInfo.TestStatus, UserTestsInfo.ProductName,  
   UserTestsInfo.QuizOrQBank  
  FROM (SELECT (CAST((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount AS decimal(4,1))) AS PercentCorrect,  
    QC.QuestionCount,QC.UserTestID  
     FROM (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCount  
     FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  
     WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.TestStatus = 1  
     GROUP BY dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN  
    (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCorrectCount  
     FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  
     WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.TestStatus = 1 AND dbo.UserQuestions.Correct = 1  
     GROUP BY dbo.UserQuestions .UserTestID)QCC ON QC.UserTestID=QCC.UserTestID) QInfo INNER JOIN  
     (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,  
      DATEADD(hour, @timeOffset, TestStarted) as TestStarted,dbo.UserTests.TestID, dbo.UserTests.TestStatus,  
      dbo.Products.ProductName,dbo.UserTests.QuizOrQBank  
      FROM dbo.Tests INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID  
      INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID  
      WHERE (UserID = @userId) and TestStatus=1 )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID  
END    
GO
PRINT 'Finished creating PROCEDURE uspGetFinishedTests'
GO 




