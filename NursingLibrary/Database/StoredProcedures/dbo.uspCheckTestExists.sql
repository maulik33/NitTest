SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckTestExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckTestExists]
GO


PRINT 'Creating PROCEDURE uspCheckTestExists'
GO

CREATE PROCEDURE [dbo].[uspCheckTestExists]    
 @userId varchar(80), @productId int, @testSubGroup int, @sType int, @timeOffset int, @result int OUTPUT    
AS    
BEGIN       
 SET NOCOUNT ON    
/*============================================================================================================                    
 -- Purpose: Check whether the test is assigned to a student    
 -- NURSING 3601 - As we have more than 1 qbank test the harded coded value 74 is removed
 -- NURSING-4875( Post-Production: NCLEX Prep tab is not working when the Qbank test is not scheduled.)    
 -- modified On: 06/10/2013,10/24/2013    
 -- modified By: Liju Mathews,Shodhan     
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
     
DECLARE @groupId int, @testId int, @cohortId int, @rcount int,@pointer int,@maxId INT;    
     CREATE TABLE #AssignedTests
	 (
	  Id INT IDENTITY(1,1),
	  CohortId INT, 
	  GroupId INT,
	  TestId INT
	 ) 
 SET @pointer = 1
 
  IF (@sType = 0)    
  BEGIN    
    IF (@testSubGroup != -1 AND @productId = 4)
     INSERT INTO #AssignedTests(CohortId,GroupId,TestId)     
     SELECT NusStudentAssign.CohortID, NusStudentAssign.GroupID, dbo.Tests.TestID    
     FROM  dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID    
	   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
	   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
	   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID    
	   INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID    
	   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID    
	   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID    
     WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)    
   AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)    
   AND dbo.Tests.ProductID = @productId AND dbo.Tests.TestSubGroup = @testSubGroup AND dbo.NurInstitution.Status = '1'    
   ELSE    
   INSERT INTO #AssignedTests(CohortId,GroupId,TestId) 
     SELECT NusStudentAssign.CohortID,NusStudentAssign.GroupID, Tests.TestID    
     FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID    
	   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
	   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
	   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID    
	   INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID    
	   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID    
	   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID    
     WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)    
   AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)    
   AND dbo.Tests.ProductID = @productId  AND dbo.NurInstitution.Status = '1'    
  END    
  ELSE    
  BEGIN    
    IF (@testSubGroup != -1 AND @productId = 4)
    INSERT INTO #AssignedTests(CohortId,GroupId,TestId)    
     SELECT NusStudentAssign.CohortID, NusStudentAssign.GroupID, Tests.TestID    
     FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID    
	   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
	   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
	   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID    
	   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID    
	   INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID    
	   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID    
     WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)    
   AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)    
   AND dbo.NurProgramProduct.ProductID = @productId AND dbo.Tests.TestSubGroup = @testSubGroup AND dbo.NurInstitution.Status = '1'    
   ELSE   
   INSERT INTO #AssignedTests(CohortId,GroupId,TestId) 
     SELECT NusStudentAssign.CohortID,NusStudentAssign.GroupID, Tests.TestID    
     FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID    
	   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
	   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
	   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID    
	   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID    
	   INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID    
	   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID    
     WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)    
   AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)    
   AND dbo.NurProgramProduct.ProductID = @productId AND dbo.NurInstitution.Status = '1'    
  END    
     
 
 IF EXISTS(SELECT Id FROM #AssignedTests)
	 BEGIN
	  SELECT @testId = TestId,@cohortId=CohortId,@groupId = GroupId FROM #AssignedTests WHERE Id = @pointer
		IF (@testId NOT IN (Select TestId from tests where ProductID=4 and TestSubGroup = 3) and @testSubGroup = 3 )    
		  BEGIN  
		    SELECT @maxId=COUNT(Id) FROM  #AssignedTests 
			     WHILE(@maxId >= @pointer and @result = 0)
			      BEGIN
					  EXECUTE @rcount = uspTestsExistsByTestIDCohortIDHour @testId, @cohortId, @timeOffset    
					  IF @RCount > 0    
					  BEGIN    
						SET @result = 1    
						RETURN    
					  END    
					     
					  EXECUTE @rcount = uspTestsExistsByTestIDGroupIDHour @testId, @groupId, @timeOffset    
					  IF @rcount > 0    
					  BEGIN    
						SET @result = 1    
						RETURN    
					  END    
					     
					  EXECUTE @rcount = uspTestsExistsByTestIDUserIDHour @testId, @userId, @timeOffset    
					  IF @rcount > 0    
					  BEGIN    
						SET @result = 1    
						RETURN    
					  END
					 SET @pointer = @pointer+1
				  END 			
		  END    
		  ELSE
			  BEGIN  
				SET @result = 1    
				RETURN    
			  END    
	  END   
  SET @result = 0    
  RETURN    
SET NOCOUNT OFF        
END 
GO
PRINT 'Finished creating PROCEDURE uspCheckTestExists'
GO 


