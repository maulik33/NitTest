
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetMultiCampusReportCardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetMultiCampusReportCardDetails]
GO
PRINT 'Creating PROCEDURE uspGetMultiCampusReportCardDetails'
GO 
CREATE PROCEDURE [dbo].[uspGetMultiCampusReportCardDetails]            
@StudentIds varchar(max),            
@TestIds varchar(max),                  
@InstitutionIds varchar(max),            
@TestTypeId varchar(100)            
AS            
BEGIN            
            
SET NOCOUNT ON;            
/*============================================================================================================          
--      Purpose: Retrive Multi Campus report detail            
--      Modified On: 02/05/2013          
--      Created By:Shodhan Kini 
--      Description : Used in "Multi Campus report card page.As part of "Nursing-2859" story. 
        Execution sample "exec uspGetMultiCampusReportCardDetails '71085|27403|65052|65112|68092|63855|65607|50291|60518|41582','394|376|63|36|33|1000054|1000036|170|169|1|90|606|74|448|5|1000033|57|550|3','117','1|3|4|6'        
        
        Modified by: Atul Gupta
        Modified Date: 03/29/2013
        Description: Wired up to use [dbo].[funcListToTableIntForReport] for resolving NURSING-3415 issue
        
        Modified by: Liju Mathews
        Modified Date: 06/14/2013
        Description: NURSING-3519 - Add testStyle to Multicampus Report card

		 Modified by: Karthik CS
        Modified Date: 07/04/2013
        Description: NURSING-3616 - Add Program of Study Filter to Reports for Multi-campus
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
SELECT                
  SI.UserID, SI.FirstName, SI.LastName,NI.InstitutionName,NI.InstitutionId,                 
  C.CohortID, C.CohortName,                
  P.ProductId TestTypeId, P.ProductName TestType,                
  T.TestId, T.TestName,                
  UT.TestStarted TestTaken,
 PS.ProgramofStudyName,              
  case                
   when p.productid = 1 then                
    ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForRemedation),0),108),'')                
   when p.productid IN (3, 4) then                
    ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForExplanation),0),108),'')                
   end AS remediationTime,                
  ISNULL(G.GroupName,'') GroupName,                
  ISNULL(((dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)*1.0)*100) / dbo.UFNReturnTotalPercentByUserIDTestID(UT.UserTestId),0) AS Correct,                
  CASE                
   WHEN dbo.UFNReturnPercentileRankByTestIDNumberCorrect(T.TestId,dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)) <> 0 THEN                
    CAST(dbo.UFNReturnPercentileRankByTestIDNumberCorrect(T.TestId,dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)) AS VARCHAR(10))                
   ELSE                
    CASE                
     WHEN dbo.UFNCheckPercentileRankExistForTest(T.TestId) = 0 THEN                
      'n/a'                
     ELSE                
      '0'                
    END                
  END [Rank],                
  UT.UserTestId,    
  CASE WHEN (P.Productid != 4) THEN ''   
  ELSE  
        CASE  WHEN T.TestId in (Select testId from tests where Productid=4 and TestSubGroup=3) THEN  
      CASE WHEN UT.TutorMode=1 THEN 'Tutor Mode'  
           WHEN UT.TutorMode=0 THEN 'Timed Test'   
       END  
  ELSE ''  
  END  
  END AS TestStyle,          
  dbo.ReturnGetTestTimeUsed(UT.UserTestId) as TimeUsed,                
  dbo.UFNReturnAnsweredQuestionCount(UT.UserTestId) as QuestionCount              
 FROM  dbo.UserTests UT INNER JOIN Tests T ON T.TestId = UT.TestId AND UT.TestStatus = 1                
  INNER JOIN dbo.UserQuestions AS UQ ON UT.UserTestID = UQ.UserTestID       
  INNER JOIN nurInstitution ni ON UT.InsitutionId =  NI.InstitutionId  
  INNER JOIN dbo.ProgramofStudy PS on ni.ProgramofStudyid=PS.programofstudyid            
  INNER JOIN Products P ON T.ProductId = P.ProductId                
  AND P.ProductId IN(select value from  dbo.funcListToTableIntForReport(@TestTypeId,'|'))                
  INNER JOIN dbo.NurCohort C ON UT.CohortID = C.CohortID                
  INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID                
  INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID                   
  LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND UT.CohortID = G.CohortId                
 WHERE (UT.InsitutionId IN ( select value from  dbo.funcListToTableIntForReport(@InstitutionIds,'|')))               
  AND ((SI.UserID IN ( select value from  dbo.funcListToTableIntForReport(@StudentIds,'|')))                
  OR @StudentIds = '0')           
  AND ((@TestIds <> '0,' AND UT.TestId IN ( select value from  dbo.funcListToTableIntForReport(@TestIds,'|')))                
  OR @TestIds = '0,' )                       
 GROUP BY UT.UserTestId,SI.UserID, SI.FirstName, SI.LastName,PS.ProgramofStudyName,NI.InstitutionName,NI.InstitutionId,                 
  C.CohortID, C.CohortName,                
  P.ProductId, P.ProductName,                
  T.TestId, T.TestName, UT.TutorMode,               
  UT.TestStarted,G.GroupName
         
SET NOCOUNT OFF            
END
GO
PRINT 'Finished creating PROCEDURE uspGetMultiCampusReportCardDetails'
GO 


