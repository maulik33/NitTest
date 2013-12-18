
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAllTests]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAllTests]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAllTests]
	@userId int, @productId int, @sType int, @testSubGroup int, @timeOffset int
AS 
BEGIN  
SET NOCOUNT ON; 
IF (@sType = 0)   
BEGIN   
 IF (@productId = 4 OR @productId = 5)   
 BEGIN  
    SELECT DISTINCT V.*  
 FROM  
   (SELECT Cohort_V.Type,  
     Cohort_V.TestName,  
     Cohort_V.StartDate,  
     Cohort_V.EndDate,  
     Cohort_V.TestID,  
     Cohort_V.TestNumber,  
     Cohort_V.TestSubGroup,  
     Cohort_V.ProgramID,  
     Cohort_V.UserID,  
     dbo.NurProductDatesStudent.StartDate AS Student_StartDate,  
     dbo.NurProductDatesStudent.EndDate AS Student_EndDate,  
     Cohort_V.CohortID,  
     Cohort_V.GroupID,  
     dbo.NurProductDatesGroup.StartDate AS Group_StartDate,  
     dbo.NurProductDatesGroup.EndDate AS Group_EndDate,  
     Cohort_V.ProductID,  
     COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,  
     COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All  
    FROM dbo.NurProductDatesGroup  
    RIGHT OUTER JOIN  
   (SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID,  
         dbo.NurProgramProduct.Type,  
         dbo.NurProgramProduct.ProductID,  
         dbo.Tests.TestName,  
         dbo.NurProductDatesCohort.StartDate,  
         dbo.NurProductDatesCohort.EndDate,  
         dbo.Tests.TestID,  
         dbo.Tests.TestNumber,  
         dbo.Tests.ProductID AS TestProductID,  
         dbo.Tests.TestSubGroup,  
         dbo.NusStudentAssign.CohortID,  
         dbo.NusStudentAssign.GroupID,  
         dbo.NurCohortPrograms.ProgramID  
    FROM dbo.NurCohort  
    INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
    INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID  
    INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID  
    INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID  
    INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID  
    INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID  
    AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID  
    AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type  
    WHERE (dbo.NurStudentInfo.UserID = @userId)  
   AND (dbo.NurProgramProduct.Type = 0)  
   AND (dbo.NurCohortPrograms.Active = 1)  
   AND (dbo.NurCohort.CohortStatus = 1)  
   AND dbo.Tests.TestSubGroup = @testSubGroup  
   AND (dbo.Tests.ProductID= @productId)  
   AND (dbo.Tests.ActiveTest = 1)  
    ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID  
    AND dbo.NurProductDatesGroup.[Type] = Cohort_V.[Type]  
    AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID  
    AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID  
    LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID  
    AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID  
    AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID  
    AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V  
 WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate()))  
   AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))   
 END   
ELSE   
 BEGIN  
    SELECT DISTINCT V.*  
   FROM  
  (SELECT Cohort_V.Type,  
    Cohort_V.TestName,  
    Cohort_V.StartDate,  
    Cohort_V.EndDate,  
    Cohort_V.TestID,  
    Cohort_V.TestNumber,  
    Cohort_V.TestSubGroup,  
    Cohort_V.ProgramID,  
    Cohort_V.UserID,  
    dbo.NurProductDatesStudent.StartDate AS Student_StartDate,  
    dbo.NurProductDatesStudent.EndDate AS Student_EndDate,  
    Cohort_V.CohortID,  
    Cohort_V.GroupID,  
    dbo.NurProductDatesGroup.StartDate AS Group_StartDate,  
    dbo.NurProductDatesGroup.EndDate AS Group_EndDate,  
    Cohort_V.ProductID,  
    COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,  
    COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All  
   FROM dbo.NurProductDatesGroup  
   RIGHT OUTER JOIN  
     (SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID,  
           dbo.NurProgramProduct.Type,  
           dbo.NurProgramProduct.ProductID,  
           dbo.Tests.TestName,  
           dbo.NurProductDatesCohort.StartDate,  
           dbo.NurProductDatesCohort.EndDate,  
           dbo.Tests.TestID,  
           dbo.Tests.TestNumber,  
           dbo.Tests.ProductID AS TestProductID,  
           dbo.Tests.TestSubGroup,  
           dbo.NusStudentAssign.CohortID,  
           dbo.NusStudentAssign.GroupID,  
           dbo.NurCohortPrograms.ProgramID  
   FROM dbo.NurCohort  
   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID  
   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID  
   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID  
   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID  
   INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID  
   AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID  
   AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type  
   WHERE (dbo.NurStudentInfo.UserID = @userId)  
     AND (dbo.NurProgramProduct.Type = 0)  
     AND (dbo.NurCohortPrograms.Active = 1)  
     AND (dbo.NurCohort.CohortStatus = 1)  
     AND dbo.Tests.TestSubGroup = @testSubGroup  
     AND (dbo.Tests.ProductID= @productId)  
     AND (dbo.Tests.ActiveTest = 1)  
   ORDER BY dbo.Tests.TestName ASC) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID  
   AND dbo.NurProductDatesGroup.[Type] = Cohort_V.[Type]  
   AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID  
   AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID  
   LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID  
   AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID  
   AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID  
   AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate()))  
   AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))   
 END   
END   
ELSE   
  BEGIN   
   IF (@productId = 4 OR @productId = 5)  
    BEGIN  
    SELECT DISTINCT V.*  
    FROM  
   (SELECT Cohort_V.Type,  
     Cohort_V.TestName,  
     Cohort_V.StartDate,  
     Cohort_V.EndDate,  
     Cohort_V.TestID,  
     Cohort_V.TestNumber,  
     Cohort_V.TestSubGroup,  
     Cohort_V.ProgramID,  
     Cohort_V.UserID,  
     dbo.NurProductDatesStudent.StartDate AS Student_StartDate,  
     dbo.NurProductDatesStudent.EndDate AS Student_EndDate,  
     Cohort_V.CohortID,  
     Cohort_V.GroupID,  
     dbo.NurProductDatesGroup.StartDate AS Group_StartDate,  
     dbo.NurProductDatesGroup.EndDate AS Group_EndDate,  
     Cohort_V.ProductID,  
     COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,  
     COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All  
    FROM dbo.NurProductDatesGroup  
    RIGHT OUTER JOIN  
      (SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID,  
            dbo.NurProgramProduct.Type,  
            dbo.NurProgramProduct.ProductID,  
            dbo.Tests.TestName,  
            dbo.NurProductDatesCohort.StartDate,  
            dbo.NurProductDatesCohort.EndDate,  
            dbo.Tests.TestID,  
            dbo.Tests.TestNumber,  
            dbo.Tests.TestSubGroup,  
            dbo.NusStudentAssign.CohortID,  
            dbo.NusStudentAssign.GroupID,  
            dbo.NurCohortPrograms.ProgramID  
    FROM dbo.NurCohort  
    INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
    INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID  
    INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID  
    INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID  
    INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID  
    INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID  
    AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID  
    AND dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID  
    AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type  
    WHERE (dbo.NurStudentInfo.UserID = @userId)  
      AND (dbo.NurProgramProduct.Type = 1)  
      AND (dbo.NurCohortPrograms.Active = 1)  
      AND (dbo.NurCohort.CohortStatus = 1)  
      AND dbo.Tests.TestSubGroup = @testSubGroup  
      AND (dbo.NurProgramProduct.ProductID = @productId)  
      AND (dbo.Tests.ActiveTest = 1)  
    ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID  
    AND dbo.NurProductDatesGroup.[Type] = Cohort_V.[Type]  
    AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID  
    AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID  
    LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID  
    AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID  
    AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID  
    AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate()))  
    AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))   
    END   
   ELSE   
   BEGIN  
   SELECT DISTINCT V.*  
   FROM  
  (SELECT Cohort_V.Type,  
    Cohort_V.TestName,  
    Cohort_V.StartDate,  
    Cohort_V.EndDate,  
    Cohort_V.TestID,  
    Cohort_V.TestNumber,  
    Cohort_V.TestSubGroup,  
    Cohort_V.ProgramID,  
    Cohort_V.UserID,  
    dbo.NurProductDatesStudent.StartDate AS Student_StartDate,  
    dbo.NurProductDatesStudent.EndDate AS Student_EndDate,  
    Cohort_V.CohortID,  
    Cohort_V.GroupID,  
    dbo.NurProductDatesGroup.StartDate AS Group_StartDate,  
    dbo.NurProductDatesGroup.EndDate AS Group_EndDate,  
    Cohort_V.ProductID,  
    COALESCE(dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,  
    COALESCE(dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All  
   FROM dbo.NurProductDatesGroup  
   RIGHT OUTER JOIN  
     (SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID,  
           dbo.NurProgramProduct.Type,  
           dbo.NurProgramProduct.ProductID,  
           dbo.Tests.TestName,  
           dbo.NurProductDatesCohort.StartDate,  
           dbo.NurProductDatesCohort.EndDate,  
           dbo.Tests.TestID,  
           dbo.Tests.TestNumber,  
           dbo.Tests.TestSubGroup,  
           dbo.NusStudentAssign.CohortID,  
           dbo.NusStudentAssign.GroupID,  
           dbo.NurCohortPrograms.ProgramID  
   FROM dbo.NurCohort  
   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  
   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID  
   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID  
   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID  
   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID  
   INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID  
   AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID  
   AND dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID  
   AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type  
   WHERE (dbo.NurStudentInfo.UserID = @userId)  
     AND (dbo.NurProgramProduct.Type = CONVERT(varchar,@sType))  
     AND (dbo.NurCohortPrograms.Active = 1)  
     AND (dbo.NurCohort.CohortStatus = 1)  
     AND dbo.Tests.TestSubGroup = @testSubGroup  
     AND (dbo.NurProgramProduct.ProductID = @productId)  
     AND (dbo.Tests.ActiveTest = 1)  
     AND (dbo.Tests.DefaultGroup= CONVERT(char(1),@sType))  
     AND (ISNULL(dbo.Tests.ReleaseStatus,'R') != 'F')  
   ORDER BY dbo.Tests.TestName ASC) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID  
   AND dbo.NurProductDatesGroup.[Type] = Cohort_V.[Type]  
   AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID  
   AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID  
   LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID  
   AND dbo.NurProductDatesStudent.Type=@sType  
   AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID  
   AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID  
   AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate()))  
   AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))   
   END  
  END   
  END  
GO

