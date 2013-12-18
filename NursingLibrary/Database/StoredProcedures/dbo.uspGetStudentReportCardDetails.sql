
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentReportCardDetails]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetStudentReportCardDetails]
GO
PRINT 'Creating PROCEDURE uspGetStudentReportCardDetails'
GO
/****** Object:  StoredProcedure [dbo].[uspGetStudentReportCardDetails]    Script Date: 12/20/2011 13:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStudentReportCardDetails]      
@StudentIds varchar(max),      
@TestIds varchar(max),            
@InstitutionId int,      
@TestTypeId varchar(100)      
AS      
BEGIN      
      
SET NOCOUNT ON;      
/*============================================================================================================    
--  Purpose: NURSING -3519 To include the Tutor Mode/Timed Test for the Qbank test in StudentReportCard
--  Purpose: NURSING-3628 (Fix Timezone Issue with Student Report Card)
--  Modified: 09/04/2013,13/06/13,28/06/13   
--  Author:Liju ,Karthik
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

 DECLARE @Hour INT
  SELECT @Hour = TimeZones.Hour  
  FROM TimeZones INNER JOIN NurInstitution ON NurInstitution.TimeZone = TimeZones.TimeZoneID   
  WHERE NurInstitution.InstitutionID=@InstitutionId

               
SELECT            
  SI.UserID, SI.FirstName, SI.LastName,            
  C.CohortID, C.CohortName,            
  P.ProductId TestTypeId, P.ProductName TestType,            
  T.TestId, T.TestName,
  DATEADD(hour, @Hour, UT.TestStarted) TestTaken,

  CASE            
   WHEN p.productid = 1 THEN            
    ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForRemedation),0),108),'')            
   WHEN p.productid IN (3, 4) THEN            
    ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForExplanation),0),108),'')            
   END AS remediationTime,            
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
  END as TestStyle,  
  dbo.ReturnGetTestTimeUsed(UT.UserTestId) as TimeUsed,            
  dbo.UFNReturnAnsweredQuestionCount(UT.UserTestId) as QuestionCount          
 FROM  dbo.UserTests UT INNER JOIN Tests T ON T.TestId = UT.TestId AND UT.TestStatus = 1            
  INNER JOIN dbo.UserQuestions AS UQ ON UT.UserTestID = UQ.UserTestID            
  INNER JOIN Products P ON T.ProductId = P.ProductId            
  AND P.ProductId IN(select value from  dbo.funcListToTableIntForReport(@TestTypeId,'|'))            
  INNER JOIN dbo.NurCohort C ON UT.CohortID = C.CohortID            
  INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID            
  INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID               
  LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND UT.CohortID = G.CohortId            
 WHERE UT.InsitutionId = @InstitutionId            
  AND ((SI.UserID IN ( select value from  dbo.funcListToTableIntForReport(@StudentIds,'|')))            
  OR @StudentIds = '0')            
  AND ((@TestIds <> '0,' AND UT.TestId IN ( select value from  dbo.funcListToTableIntForReport(@TestIds,'|')))            
  OR @TestIds = '0,' )                   
 GROUP BY UT.UserTestId,SI.UserID, SI.FirstName, SI.LastName,            
  C.CohortID, C.CohortName,            
  P.ProductId, P.ProductName,            
  T.TestId, T.TestName,UT.TutorMode,           
  UT.TestStarted,G.GroupName   
          
SET NOCOUNT OFF          
END      
GO
PRINT 'Finished creating PROCEDURE uspGetStudentReportCardDetails'
GO 



