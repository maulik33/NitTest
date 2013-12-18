
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsForStudentReportCardByModule]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetResultsForStudentReportCardByModule]
GO
  PRINT 'Creating PROCEDURE uspGetResultsForStudentReportCardByModule'
 GO
/****** Object:  StoredProcedure [dbo].[uspGetResultsForStudentReportCardByModule]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetResultsForStudentReportCardByModule]  
@InstitutionIds VARCHAR(MAX),  
@CohortId INT,  
@ModuleIds VARCHAR(MAX),  
@StudentIds VARCHAR(MAX),  
@CaseID int  
AS  
BEGIN  
 SET NOCOUNT ON;  
 SELECT t.*,(CaseModuleScore.Correct*100.0/CaseModuleScore.Total) As Correct  FROM  
 (SELECT nsi.FirstName,nsi.LastName,nsi.EnrollmentID,ModuleID,ModuleName  
 FROM NurStudentInfo nsi  
 CROSS JOIN (select * from NurModule WHERE ModuleID IN ( select value from  dbo.funcListToTableInt(@ModuleIds,'|') )) t1  
 WHERE (nsi.InstitutionID = 0 OR (nsi.InstitutionID <> 0 AND nsi.InstitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))))  
 AND (nsi.UserID = 0 OR (nsi.UserID <> 0 AND nsi.UserID IN ( select value from  dbo.funcListToTableInt(@StudentIds,'|'))))) t  
 LEFT JOIN CaseModuleScore ON CaseModuleScore.StudentID = t.EnrollmentID AND casemodulescore.ModuleId = t.ModuleId AND CaseModuleScore.CaseID = @CaseID  
 WHERE CaseModuleScore.ModuleID IN ( select value from  dbo.funcListToTableInt(@ModuleIds,'|') )  
 SET NOCOUNT OFF   
END  
GO
PRINT 'Finished creating PROCEDURE uspGetResultsForStudentReportCardByModule'
GO
