
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsByCohortQuestions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetResultsByCohortQuestions]
GO
PRINT 'Creating PROCEDURE uspGetResultsByCohortQuestions'
GO
Create PROCEDURE [dbo].[uspGetResultsByCohortQuestions]    
 @ProductId INT,     
 @TestId INT,     
 @CohortIds VARCHAR(MAX)     
AS     
BEGIN    
 SET NOCOUNT ON     
 /*============================================================================================================    
 //Purpose: Wired up to use [dbo].[funcListToTableIntForReport] for resolving 8000 character bug issue    
 //modified: 04/9/2013    
 //Author: Liju    
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
 DECLARE @IsCustomFRTest AS bit DECLARE @TempTableName AS NVarchar(20), @tempResultQuery AS NVarchar(200),@dbCohortId AS VARCHAR(MAX)     
 SET @dbCohortId = @CohortIds    
 SET @IsCustomFRTest = 0    
 SELECT @IsCustomFRTest = 1    
 FROM tests    
 WHERE ReleaseStatus ='F'    
   AND (productid = 3    
     OR productid = 6)    
   AND testid =@TestId     
    
 IF(@IsCustomFRTest = 0)     
  BEGIN    
     SELECT dbo.Questions.QuestionID,    
      dbo.Remediation.TopicTitle,    
      LD.LevelOfDifficulty ,    
      CASE    
       WHEN dbo.Questions.Q_Norming IS NULL    
         OR dbo.Questions.Q_Norming=-1 THEN -100.0    
       ELSE Cast(dbo.Questions.Q_Norming AS numeric(10,1))    
      END AS Q_Norming INTO #RESULT    
     FROM dbo.Questions    
     INNER JOIN TestQuestions ON dbo.Questions.QuestionID = dbo.TestQuestions.QuestionID    
     LEFT JOIN LevelOfDifficulty LD ON Questions.LevelOfDifficultyID = LD.LevelOfDifficultyID    
     INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID     
     WHERE dbo.TestQuestions.TestID = @TestId    
   SET @TempTableName = '#result'     
  END    
 ELSE     
  BEGIN    
   SELECT DISTINCT dbo.Questions.QuestionID,    
       dbo.Remediation.TopicTitle,    
       LD.LevelOfDifficulty ,    
       CASE    
        WHEN dbo.Questions.Q_Norming IS NULL    
          OR dbo.Questions.Q_Norming=-1 THEN -100.0    
        ELSE Cast(dbo.Questions.Q_Norming AS numeric(10,1))    
       END AS Q_Norming INTO #result1    
   FROM dbo.Questions    
   INNER JOIN UserQuestions UQ ON dbo.Questions.QID = UQ.QID    
   LEFT JOIN LevelOfDifficulty LD ON Questions.LevelOfDifficultyID = LD.LevelOfDifficultyID    
   INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID    
   INNER JOIN UserTests UT ON UQ.UserTestId = UT.UserTestID    
   WHERE UT.TestID = @TestId    
   SET @TempTableName = '#result1'     
  END -- Fetch Cohort Names    
    
  CREATE TABLE #Cohorts (ID int identity(1,1),CohortId INT,CohortName Varchar(100))    
    
  INSERT INTO #Cohorts (CohortId,CohortName)    
  SELECT CohortId,    
      CohortName    
  FROM NurCohort    
  WHERE CohortId IN    
   (SELECT value    
    FROM dbo.funcListToTableIntForReport(@dbCohortId,'|'))    
    ORDER BY CohortName -- Add the cohort Names as columns to result table and update Percentage    
    
  DECLARE @count int    
  SELECT @count = max(id)    
  FROM #Cohorts   
  DECLARE @index int,@CohortId INT,@studentCount INT    
  DECLARE @colName varchar(50)   
    
  SET @index =1   
  
WHILE(@index<=@count)  
 BEGIN    
  SELECT @colName = CohortName,    
        @CohortId = CohortId    
  FROM #Cohorts    
  WHERE id = @index   
   set @StudentCount = 0
   select @StudentCount = CAST(ISNULL(count(testID),0) AS INT)   
   from dbo.UserTests    
   WHERE TestStatus = 1    
      AND ProductID= @ProductId    
      AND TestID= @TestId    
      AND CohortID=@CohortId    
   GROUP BY TestID   

   SET @colName = @colName +'(N='+ CAST(ISnull(@studentCount,0) as varchar)+')'  
   exec('alter table [' +@TempTableName+ '] add [' + @colName + '] numeric(10,1) ') -- Fetch Percentage for Cohort    
      
  
    SELECT Cast(100.0 * SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) / SUM(1) AS numeric(10,1)) AS Percentage,    
     dbo.Questions.QuestionID INTO #CohortPercentage    
    FROM dbo.UserQuestions    
    INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID    
    INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID    
    INNER JOIN dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID    
    INNER JOIN dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID    
    INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID WHERE TestStatus = 1    
    AND dbo.Tests.ProductID= @ProductId    
    AND dbo.Tests.TestID=@TestId    
    AND dbo.NurCohort.CohortID= @CohortId    
    GROUP BY dbo.Questions.QuestionID,    
     dbo.UserTests.CohortID,    
     dbo.Tests.ProductID,    
     dbo.Tests.TestID ,    
     dbo.Remediation.TopicTitle,    
     Q_Norming    
      -- update Percentage into main Table    
  
   exec('UPDATE ['+@TempTableName+ ']    
     SET ['+ @colName + '] = p.Percentage    
     FROM ['+@TempTableName+ '] main    
     INNER JOIN #CohortPercentage p ON p.QuestionID = main.QuestionID')   
  
     exec('UPDATE ['+@TempTableName+ ']    
                       SET ['+ @colName + '] = ''-100''    
                       FROM ['+@TempTableName+ '] WHERE ['+ @colName + '] IS NULL')    
  SET @index = @index + 1    
  DROP TABLE #CohortPercentage END    
  SET @tempResultQuery = 'SELECT * FROM ' + @TempTableName    
  
  EXECUTE  sp_executesql @tempResultQuery    
  print @tempResultQuery  
  SET NOCOUNT OFF     
END
GO
PRINT 'Finished creating PROCEDURE uspGetResultsByCohortQuestions'
GO

