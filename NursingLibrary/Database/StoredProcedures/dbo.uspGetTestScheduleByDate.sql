
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestScheduleByDate]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestScheduleByDate]
GO
PRINT 'Creating PROCEDURE uspGetTestScheduleByDate'
GO
/****** Object:  StoredProcedure [dbo].[uspGetTestScheduleByDate]    Script Date: 10/10/2011 15:49:07 ******/

CREATE PROCEDURE [dbo].[uspGetTestScheduleByDate]        
    @InstitutionIds VARCHAR(8000) ,        
    @CohortIds NVARCHAR(2000) ,        
    @GroupIds NVARCHAR(2000) ,    
    @ProductIds NVARCHAR(100),    
    @StartDate DATETIME ,        
    @EndDate DATETIME        
AS        
    BEGIN   
 SET nocount ON


/*============================================================================================================
--      Purpose: Retrieve student test schedule details  
--      Modified: 11/22/2011
--	    Author:Shodhan Kini
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
        SELECT  V.InstitutionName ,        
                V.CohortName ,        
                V.TestName ,        
                V.ProductName ,        
                V.StartDate_All AS StartDate ,        
                COUNT(V.UserID) AS Students        
        FROM    ( SELECT    cohort_V.InstitutionName ,        
                            cohort_V.CohortName ,        
                            cohort_V.ProductName ,        
                            Cohort_V.Type ,        
                            Cohort_V.TestName ,        
                            Cohort_V.StartDate ,        
                            Cohort_V.EndDate ,        
                            Cohort_V.TestID ,        
                            Cohort_V.ProgramID ,        
                            Cohort_V.UserID ,        
                            dbo.NurProductDatesStudent.StartDate AS Student_StartDate ,        
                            dbo.NurProductDatesStudent.EndDate AS Student_EndDate ,        
                            Cohort_V.CohortID ,        
                            Cohort_V.GroupID ,        
                            dbo.NurProductDatesGroup.StartDate AS Group_StartDate ,        
                            dbo.NurProductDatesGroup.EndDate AS Group_EndDate ,        
                            Cohort_V.ProductID ,        
                            COALESCE(dbo.NurProductDatesStudent.StartDate,        
                                     dbo.NurProductDatesGroup.StartDate,        
                                     Cohort_V.StartDate) AS StartDate_All ,        
                            COALESCE(dbo.NurProductDatesStudent.EndDate,        
                                     dbo.NurProductDatesGroup.EndDate,        
                                     Cohort_V.EndDate) AS EndDate_All        
                  FROM      dbo.NurProductDatesGroup        
                            RIGHT OUTER JOIN ( SELECT   NurInstitution.InstitutionName ,        
                                                        NurCohort.CohortName ,        
                                                        Products.ProductName ,        
                                                        dbo.NurStudentInfo.UserID ,        
                                                        dbo.NurProgramProduct.Type ,        
                                                        dbo.NurProgramProduct.ProductID ,        
                                                        dbo.Tests.TestName ,        
                                                        dbo.NurProductDatesCohort.StartDate ,        
                                                        dbo.NurProductDatesCohort.EndDate ,        
                                                        dbo.Tests.TestID ,        
                                                        dbo.Tests.TestNumber ,        
                                                        dbo.Tests.TestSubGroup ,        
                                                        dbo.Tests.ProductID AS TestProductID ,        
                                                        dbo.NusStudentAssign.CohortID ,        
                                                        dbo.NusStudentAssign.GroupID ,        
                                                        dbo.NurCohortPrograms.ProgramID        
                                               FROM     dbo.NurInstitution        
                                                        INNER JOIN dbo.NurCohort ON NurInstitution.InstitutionID = NurCohort.InstitutionID        
                                                        INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID        
                                                        INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID        
                                                        INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID        
       INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID        
                                                        INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID        
                                                        INNER JOIN Products ON Tests.ProductId = Products.ProductId        
                                                        INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID        
                                                              AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID        
                                                              AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type        
                                               WHERE    (@InstitutionIds = '' OR NurCohort.Institutionid IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))      
                                                        AND ( dbo.NurProgramProduct.Type = 0 )        
                                                        AND ( dbo.NurCohortPrograms.Active = 1 )        
                                                        AND ( @CohortIds = ''        
                                                              OR NusStudentAssign.CohortID IN (        
                                                              SELECT        
                                                              *        
                                                              FROM        
                                                              dbo.funcListToTableInt(@CohortIds,        
                                                              '|') )        
                                                            )        
                                                        AND ( @GroupIds = ''        
                                                              OR NusStudentAssign.GroupID IN (        
                                                              SELECT        
                                                              *        
                                                              FROM        
                                                              dbo.funcListToTableInt(@GroupIds,        
                                                              '|') )        
                                                            )     
													   AND (@ProductIds =''    
														OR Tests.ProductID IN (    
														 SELECT        
                                                              *        
                                                              FROM        
                                                              dbo.funcListToTableInt(@ProductIds,    
															  '|'))      
															)     
                                                        AND ( dbo.Tests.ActiveTest = 1 )        
                                             ) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID        
                                                              AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID        
    AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID        
                                                              AND NurProductDatesGroup.Type = 0        
                            LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID        
                                                              AND dbo.NurProductDatesStudent.Type = 0        
                                                              AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID        
                                                              AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID        
                                                              AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID        
                ) AS V        
        WHERE   ( @StartDate IS NULL        
                  OR V.StartDate_All > CONVERT(DATETIME, @StartDate, 101)        
                )        
AND ( @EndDate IS NULL        
                      OR DATEADD(D, 0, DATEDIFF(D, 0, V.EndDate_All)) <= CONVERT(DATETIME, @EndDate, 101)        
                    )        
        GROUP BY V.InstitutionName ,        
                V.CohortName ,        
                V.TestName ,        
                V.ProductName ,        
                V.StartDate_All        
        UNION        
        SELECT  V.InstitutionName ,        
                V.CohortName ,        
                V.ProductName AS TestName ,        
                V.ProductName ,        
                V.StartDate_All AS StartDate ,        
                COUNT(V.UserID) AS Students        
        FROM    ( SELECT    cohort_V.InstitutionName ,        
                            cohort_V.CohortName ,        
                            cohort_V.ProductName ,        
                            Cohort_V.Type ,        
                            Cohort_V.TestName ,        
                            Cohort_V.StartDate ,        
                            Cohort_V.EndDate ,        
                            Cohort_V.TestID ,        
                            Cohort_V.ProgramID ,        
                            Cohort_V.UserID ,        
                            dbo.NurProductDatesStudent.StartDate AS Student_StartDate ,        
                            dbo.NurProductDatesStudent.EndDate AS Student_EndDate ,        
                            Cohort_V.CohortID ,        
                            Cohort_V.GroupID ,        
                            dbo.NurProductDatesGroup.StartDate AS Group_StartDate ,        
                            dbo.NurProductDatesGroup.EndDate AS Group_EndDate ,        
                            Cohort_V.ProductID ,        
                            COALESCE(dbo.NurProductDatesStudent.StartDate,        
                                     dbo.NurProductDatesGroup.StartDate,        
                                     Cohort_V.StartDate) AS StartDate_All ,        
                            COALESCE(dbo.NurProductDatesStudent.EndDate,        
                                     dbo.NurProductDatesGroup.EndDate,        
                                     Cohort_V.EndDate) AS EndDate_All        
                  FROM      dbo.NurProductDatesGroup        
                            RIGHT OUTER JOIN ( SELECT   NurInstitution.InstitutionName ,        
                                                        NurCohort.CohortName ,        
                                                        Products.ProductName ,        
                                                        dbo.NurStudentInfo.UserID ,        
                                                        dbo.NurProgramProduct.Type ,        
                                                        dbo.NurProgramProduct.ProductID ,        
                       '' AS TestName ,        
                                                        dbo.NurProductDatesCohort.StartDate ,        
                                                        dbo.NurProductDatesCohort.EndDate ,        
                                         dbo.Tests.TestID ,        
                                                        dbo.Tests.TestNumber ,        
                                                        dbo.Tests.TestSubGroup ,        
                                                        dbo.Tests.ProductID AS TestProductID ,        
                                                        dbo.NusStudentAssign.CohortID ,        
                                                        dbo.NusStudentAssign.GroupID ,        
                                                        dbo.NurCohortPrograms.ProgramID        
                                               FROM     dbo.NurInstitution        
            INNER JOIN dbo.NurCohort ON NurInstitution.InstitutionID = NurCohort.InstitutionID        
                                                        INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID        
                                                        INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID        
                                                        INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID        
                                                        INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID        
                                                        INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID        
                                                        INNER JOIN Products ON NurProgramProduct.ProductId = Products.ProductId        
                                                        INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID        
                                                              AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID        
                                                              AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID        
                                                              AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type        
                                               WHERE    (@InstitutionIds = '' OR NurCohort.Institutionid IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))      
                                                        AND ( dbo.NurProgramProduct.Type = 1 )        
                                                        AND ( dbo.NurCohortPrograms.Active = 1 )        
                                                        AND ( @CohortIds = ''        
                                                              OR NusStudentAssign.CohortID IN (        
                                                              SELECT        
                                                              *        
                                                              FROM        
                                                              dbo.funcListToTableInt(@CohortIds,        
                                                              '|') )        
                                                            )        
                                                        AND ( @GroupIds = ''        
                                                              OR NusStudentAssign.GroupID IN (        
                                                              SELECT        
                                                              *        
                                                              FROM        
                                                              dbo.funcListToTableInt(@GroupIds,        
                                                              '|') )        
                                                            )      
                                                        AND (CHARINDEX('3', @ProductIds) > 0)    
                                                        AND ( dbo.Tests.ActiveTest = 1 )        
                                             ) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID        
                                                              AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID        
                                                              AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID        
                                                              AND NurProductDatesGroup.Type = 1        
    LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID        
                                                              AND dbo.NurProductDatesStudent.Type = 1        
                                                              AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID        
                                                              AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID        
                                                              AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID        
                ) AS V        
        WHERE   ( @StartDate IS NULL        
                  OR V.StartDate_All > CONVERT(DATETIME, @StartDate, 101)        
                )        
                AND ( @EndDate IS NULL        
                      OR DATEADD(D, 0, DATEDIFF(D, 0, V.EndDate_All)) <= CONVERT(DATETIME, @EndDate, 101)        
                    )        
        GROUP BY V.InstitutionName ,        
                V.CohortName ,        
                V.TestName ,        
                V.ProductName ,        
                V.StartDate_All  
      SET nocount OFF      
    END 
GO
PRINT 'Finished creating PROCEDURE uspGetTestScheduleByDate'
GO 
