    
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDetailsForEnglishNursingTracking]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetDetailsForEnglishNursingTracking]
GO

PRINT 'Creating PROCEDURE uspGetDetailsForEnglishNursingTracking'
GO    
CREATE PROCEDURE [dbo].[uspGetDetailsForEnglishNursingTracking]             
@InstitutionId VARCHAR(MAX),         
@CohortIds VARCHAR(MAX),            
@StudentIds VARCHAR(MAX),           
@TestIds VARCHAR(MAX),        
@QIds VARCHAR(MAX)         
AS            
BEGIN            
             
SET NOCOUNT ON          
/*============================================================================================================        
 --      Purpose: Retrieves details for EnglishNursingTracking report.
 --          Sprint 55 : NURSING-4893(Institution name is not getting appended with RN/PN in Institution list box, report and exported report.)         
 --      Modified: 04/02/2012,10/25/2013       
 --     Author:Mohan 
        Modified By : Shodhan      
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

Create TABLE #temp  
(  
    InstitutionName varchar(255),  
    CohortName varchar(255),  
    LastName varchar(255),  
    FirstName varchar(255),  
    TestName varchar(255),  
    QuestionID varchar(255),  
    UserTestID varchar(255),  
    TestStarted datetime,  
    InsitutionID varchar(255),  
    AltTabClicked varchar(255),  
    AltTabClickedDate datetime,  
    UserAction varchar(255),  
    Correct varchar(255),  
    Correc varchar(255),  
    TestID varchar(255),  
    UserID varchar(255),
    ProgramofStudyName  varchar(255)
)  

INSERT INTO #temp           
Select             
  NI.InstitutionName,            
  NC.CohortName,            
  NS.LastName,            
  NS.FirstName,            
  TS.TestName,            
  QU.QuestionID,  
  UT.UserTestID,  
  DATEADD(hour, TZ.Hour, UT.TestStarted) As TestStarted,  
  UT.InsitutionID,  
  UQ.AltTabClicked,                  
  CASE UQ.AltTabClicked WHEN '1' THEN DATEADD(hour, TZ.Hour, UQ.AltTabClickedDate) else DATEADD(hour, TZ.Hour, UT.TestStarted) END AS AltTabClickedDate,            
  CASE UQ.AltTabClicked WHEN '1' THEN 'Remediated' else 'Test' END As  UserAction,              
  CASE WHEN UQ.AltTabClicked = 1 THEN 'N/A' WHEN UQ.Correct = '0' THEN 'N' WHEN UQ.Correct = '1' THEN 'Y' WHEN UQ.Correct = '2' THEN 'N' END AS Correct,  
  Correct AS Correc,  
  TS.TestID,  
  NS.UserID,
  PS.ProgramofStudyName 
From UserTests UT            
  Inner Join Tests TS    on UT.TestID  = TS.TestID            
  Inner Join NurInstitution NI on UT.InsitutionID = NI.InstitutionID 
  INNER JOIN dbo.ProgramofStudy PS on NI.ProgramofStudyid = PS.programofstudyid           
  Inner Join NurCohort NC   on UT.CohortID     = NC.CohortID            
  Inner Join NurStudentInfo NS on UT.UserID  = NS.UserID            
  Inner Join UserQuestions UQ  on UT.UserTestID   = UQ.UserTestID            
  Inner Join Questions QU      on UQ.QID   = QU.QID            
  INNER JOIN TimeZones TZ ON TZ.TimeZoneID = NI.TimeZone          
Where (@InstitutionId <> '0' AND UT.InsitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionId,'|')))        
  AND (@CohortIds <> '0' AND UT.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))      
  AND (@StudentIds <> '0' AND UT.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))       
  AND (@TestIds <> '0' AND UT.TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|')))       
  AND (@QIds <> '0' AND QU.QID IN (select value from  dbo.funcListToTableInt(@QIds,'|')))                   
  AND ISNULL(UT.IsCustomizedFRTest,0)= 0   
  AND UT.TestStatus = 1   
   
INSERT INTO #temp  
Select                   
  InstitutionName = T.InstitutionName,              
  CohortName,              
  LastName,              
  FirstName,              
  TestName,              
  QuestionID,    
  UserTestID,  
  TestStarted,  
  InsitutionID,  
  AltTabClicked,                  
  CASE AltTabClicked WHEN '1' THEN TestStarted END AS AltTabClickedDate,            
  CASE UserAction WHEN 'Remediated' THEN 'Test' else 'Test' END As  UserAction,    
  CASE WHEN Correc = '0' THEN 'N' WHEN Correc = '1' THEN 'Y' WHEN Correc = '2' THEN 'N' END AS Correct,   
  Correc,  
  TestID,  
  UserID,
  ProgramofStudyName  
From #temp T                                  
Where UserAction = 'Remediated'   
     
    
Select   
  T1.InstitutionName,              
  T1.CohortName,              
  T1.LastName,              
  T1.FirstName,              
  T1.TestName,              
  T1.QuestionID,      
  T1.AltTabClickedDate,  
  T1.UserAction,  
  T1.Correct,
  T1.ProgramofStudyName  
From #temp T1   
Where T1.UserID in (Select distinct UserID from #temp where UserAction='Remediated' And Correct = 'N/A' And TestID = T1.TestID)  
And T1.TestID in (Select distinct TestID from #temp where UserAction='Remediated' And Correct = 'N/A' And TestID = T1.TestID)  
And T1.QuestionID in (Select distinct QuestionID from #temp where UserAction='Remediated' And UserID = T1.UserID And TestID = T1.TestID)  
order by T1.TestName, T1.AltTabClickedDate Asc, T1.UserAction Desc  
   
END   

GO

PRINT 'Finished creating PROCEDURE uspGetDetailsForEnglishNursingTracking'
GO 

