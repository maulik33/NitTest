
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestByInstitutionResults]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestByInstitutionResults]
GO
PRINT 'Creating PROCEDURE uspGetTestByInstitutionResults'
GO
CREATE PROCEDURE [dbo].[uspGetTestByInstitutionResults]
@InstitutionIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@ProductId INT,
@TestId INT
AS
BEGIN
SET NOCOUNT ON
/*============================================================================================================          
--  Purpose  : Wired up to use [dbo].[funcListToTableIntForReport] for resolving 8000 character bug issue 
--	Author   :Liju Mathews
--  Modified : 04/9/2013
 
    Modified by: Karthik CS
    Modified Date: 07/04/2013
    Description: NURSING-3616 - Add Program of Study Filter to Reports for Multi-campus

	Modified by: Atul Gupta
    Modified Date: 08/12/2013
    Description: NURSING-3501 - Tests by Institution to include tests (display "N/A") without Norming data

******************************************************************************          
* This software is the confidential and proprietary information of          
* Kaplan,Inc. ("Confidential Information").  You shall not          
* disclose such Confidential Information and shall use it only in          
* accordance with the terms of the license agreement you entered into          
* with Kaplan, Inc.  
* KAPLAN, INC. MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE           
* SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT           
* NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR          
* A PARTICULAR  PURPOSE, OR NON-INFRINGEMENT. KAPLAN, INC. SHALL           
* NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF           
* USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.          
*****************************************************************************/            	
DECLARE @Test_WithOut_Norming_Data bit
SET @Test_WithOut_Norming_Data = ISNULL((SELECT TOP 1 1 FROM Norm WHERE TestID = @TestId), 0 )

SELECT   
      Cast((100.0*SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) )/ SUM(1) as Decimal(4,1)) AS Percantage,    
   dbo.NurCohort.CohortName,    
   dbo.NurCohort.CohortID,    
   dbo.NurCohort.InstitutionID,    
   dbo.NurInstitution.InstitutionName, 
   dbo.ProgramofStudy.ProgramofStudyName,
    
   COUNT( DISTINCT dbo.UserTests.UserID) AS NStudents,  
   dbo.Norm.Norm AS Normed  
      FROM   dbo.UserQuestions    
      INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID    
      INNER JOIN dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID    
      INNER JOIN dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID    
      INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID = dbo.NurInstitution.InstitutionID  
      INNER JOIN dbo.ProgramofStudy on dbo.NurInstitution.ProgramofStudyid=dbo.ProgramofStudy.ProgramofStudyId
	  LEFT JOIN dbo.Norm ON dbo.Norm.TestID = dbo.Tests.TestID   
WHERE TestStatus = 1    
   AND (@ProductId = 0 OR (@ProductId <> 0 AND dbo.Tests.ProductID = @ProductId))    
   AND (@TestId = 0 OR (@TestId <> 0 AND dbo.Tests.TestID = @TestId))  
   AND (@Test_WithOut_Norming_Data = 0 OR ((dbo.Norm.TestID = @TestId) AND (dbo.Norm.ChartType = 'overall')))   
   AND (dbo.NurCohort.InstitutionID IN (select value from  dbo.funcListToTableIntForReport(@InstitutionIds,'|')))    
   AND (dbo.NurCohort.CohortID IN (select value from  dbo.funcListToTableIntForReport(@CohortIds,'|')))    
GROUP BY    
   dbo.NurCohort.InstitutionID,    
   dbo.NurInstitution.InstitutionName,    
   dbo.NurCohort.CohortName,    
   dbo.NurCohort.CohortID,    
   dbo.Norm.Norm ,
   dbo.ProgramofStudy.ProgramofStudyName 
   
SET NOCOUNT OFF            
END
GO
PRINT 'Finished creating PROCEDURE uspGetTestByInstitutionResults'
GO 

