SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USPGetResultsByInstitutionCohortQuestions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsByInstitutionCohortQuestions]
GO
PRINT 'Creating PROCEDURE uspGetResultsByInstitutionCohortQuestions'
GO 

CREATE PROCEDURE [dbo].[uspGetResultsByInstitutionCohortQuestions]    
@ProductId INT,    
@TestId INT,    
@CohortIds VARCHAR(MAX)    
AS    
BEGIN  
SET NOCOUNT ON   
/*============================================================================================================    
//Purpose: Wired up to use [dbo].[funcListToTableIntForReport] for resolving 8000 character bug issue    
//Modified: 04/9/2013    
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
    DECLARE @IsCustomFRTest as bit      
   DECLARE @TempTableName as NVarchar(20),@tempResultQuery as NVarchar(200)      
   SET @IsCustomFRTest = 0      
         
   SELECT  @IsCustomFRTest = 1       
   FROM tests   
   WHERE ReleaseStatus ='F' and (productid = 3 or productid = 6) and  testid =@TestId          
   IF(@IsCustomFRTest = 0)      
    BEGIN      
    SELECT  dbo.Questions.QuestionID,dbo.Remediation.TopicTitle, LD.LevelOfDifficulty,  
    CASE WHEN dbo.Questions.Q_Norming is null or dbo.Questions.Q_Norming=-1 Then -100.0      
      ELSE Cast(dbo.Questions.Q_Norming as numeric(10,1))      
    END AS Q_Norming INTO #result      
    FROM   dbo.Questions      
    INNER JOIN TestQuestions ON dbo.Questions.QuestionID = dbo.TestQuestions.QuestionID      
    LEFT JOIN LevelOfDifficulty LD ON Questions.LevelOfDifficultyID = LD.LevelOfDifficultyID     
    INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID      
    WHERE dbo.TestQuestions.TestID = @TestId      
    ORDER BY dbo.Questions.QuestionID      
    SET @TempTableName = '#result'        
    END      
    ELSE       
    BEGIN      
          SELECT distinct dbo.Questions.QuestionID,dbo.Remediation.TopicTitle, LD.LevelOfDifficulty,  
    CASE WHEN dbo.Questions.Q_Norming is null or dbo.Questions.Q_Norming=-1 Then -100.0      
    ELSE Cast(dbo.Questions.Q_Norming as numeric(10,1))      
    END AS Q_Norming INTO #result1      
    FROM   dbo.Questions      
    INNER JOIN UserQuestions UQ ON dbo.Questions.QID = UQ.QID      
    LEFT JOIN LevelOfDifficulty LD       
         ON Questions.LevelOfDifficultyID = LD.LevelOfDifficultyID    
    INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID      
    INNER JOIN UserTests UT ON UQ.UserTestId = UT.UserTestID      
    WHERE UT.TestID = @TestId        
    ORDER BY dbo.Questions.QuestionID      
    SET @TempTableName = '#result1'      
    END      
 -- Fetch Cohort Names      
  CREATE TABLE #Cohorts (ID int identity(1,1),CohortId INT,CohortName Varchar(100),InstitutionName Varchar(100))      
      
  Insert into #Cohorts (CohortId,CohortName,InstitutionName)    
  SELECT CohortId,CohortName,(InstitutionName + ' - ' + PS.ProgramofStudyName)    
  FROM NurCohort INNER JOIN 
       NurInstitution AS I ON NurCohort.InstitutionId = I.InstitutionId  
       INNER JOIN ProgramofStudy AS PS On PS.ProgramofStudyId = I.ProgramofStudyId
  WHERE  CohortId IN (select value from  dbo.funcListToTableIntForReport(@CohortIds,'|'))      
  ORDER BY CohortName      
      
 -- Add the cohort Names as columns to result table and update Percentage      
  DECLARE @count int      
  SELECT @count = max(id) from #Cohorts      
  DECLARE @index int      
  SET @index =1      
   
  DECLARE @colName varchar(500)      
  DECLARE @CohortId INT      
     DECLARE @string varchar(8);  
  WHILE(@index<=@count)      
  BEGIN      
    
  --SET @string = CAST(CohortId AS varchar(8));  
  PRINT (@string)  
    SELECT @colName =InstitutionName+'/'+ CohortName +'/'+CAST(CohortId AS varchar(8)), @CohortId = CohortId      
    FROM #Cohorts      
    WHERE id = @index      
         
    exec('alter table [' +@TempTableName+ '] add [' + @colName + '] numeric(10,1) ')      
         
    -- Fetch Percentage for Cohort        
    SELECT Cast( 100.0 * SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) / SUM(1) as numeric(10,1)) AS Percentage,      
    dbo.Questions.QuestionID INTO #CohortPercentage      
    FROM   dbo.UserQuestions      
    INNER JOIN  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID      
    INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID      
    INNER JOIN  dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID      
    INNER JOIN  dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID      
    INNER JOIN  dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID      
    WHERE TestStatus = 1      
    AND dbo.Tests.ProductID= @ProductId      
    AND dbo.Tests.TestID=@TestId      
    AND dbo.NurCohort.CohortID= @CohortId      
    GROUP BY dbo.Questions.QuestionID, dbo.UserTests.CohortID,dbo.Tests.ProductID,dbo.Tests.TestID      
    , dbo.Remediation.TopicTitle,Q_Norming      
    ORDER BY dbo.Questions.QuestionID      
      
    -- update Percentage into main Table      
    exec('UPDATE ['+@TempTableName+ ']      
    SET ['+ @colName + '] = p.Percentage      
    FROM ['+@TempTableName+ '] main      
    INNER JOIN #CohortPercentage p ON p.QuestionID = main.QuestionID')      
     
    exec('UPDATE ['+@TempTableName+ ']      
    SET ['+ @colName + '] = ''-100''      
  FROM ['+@TempTableName+ '] WHERE ['+ @colName + '] IS NULL')      
         
    set @index = @index + 1      
         
    DROP TABLE #CohortPercentage      
  END      
    set @tempResultQuery = 'SELECT * FROM ' + @TempTableName + ' ORDER BY TopicTitle'      
          
   EXECUTE sp_executesql @tempResultQuery      
   SET NOCOUNT OFF   
END   
GO
PRINT 'Finished creating PROCEDURE uspGetResultsByInstitutionCohortQuestions'
GO 


