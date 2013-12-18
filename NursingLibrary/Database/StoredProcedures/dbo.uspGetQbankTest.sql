SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQbankTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetQbankTest]
GO

PRINT 'Creating PROCEDURE uspGetQbankTest'
GO

CREATE PROCEDURE [dbo].[uspGetQbankTest]
@UserId int,
@ProductId int,
@TestSubGroupId int,
@TimeOffSet int
AS
BEGIN   
 SET NOCOUNT ON
/*============================================================================================================                
 -- Purpose: Retrieves the QbankTest which is assigned to the student
 -- Created On:	06/07/2013
 -- Created By:	Liju Mathews
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
DECLARE @tempTestTbl TABLE(ID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,TestID int, GroupID int, CohortID int, ProgramofStudyId int,ValidDate int)   
DECLARE @groupId int, @testId int, @cohortId int,@time int, @rcount int,@result int,@I int,@count int;   
INSERT INTO @tempTestTbl (CohortID,GroupID,TestID ,ProgramofStudyId,ValidDate)    
   SELECT dbo.NusStudentAssign.CohortID,dbo.NusStudentAssign.GroupID,dbo.Tests.TestID,dbo.Tests.ProgramofStudyId,0   
   FROM  dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID    
   INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID    
   INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID    
   INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID    
   INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID    
   INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID    
   INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID    
     WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)    
   AND (dbo.NurStudentInfo.UserID = @UserId) AND (dbo.NurCohortPrograms.Active = 1)    
   AND dbo.Tests.ProductID = @ProductId AND dbo.Tests.TestSubGroup = @TestSubGroupId AND dbo.NurInstitution.Status = '1'    
 
 SET @I =1;
 Select @count = count(*) from @tempTestTbl
 WHILE (@I <= @count) 
 BEGIN  
 SET @result = 0   
 SELECT @testId = TestID,@cohortId =CohortID,@groupId =GroupID FROM @temptestTbl where ID =@I
 
	 EXECUTE @rcount = uspTestsExistsByTestIDCohortIDHour @testId, @cohortId, @TimeOffSet    
	 IF @RCount > 0    
	 BEGIN    
	   SET @result = 1
	 END    
   
	 EXECUTE @rcount = uspTestsExistsByTestIDGroupIDHour @testId, @groupId, @TimeOffSet    
	 IF @RCount > 0    
	 BEGIN    
	   SET @result = 1   
	 END     
     
     EXECUTE @rcount = uspTestsExistsByTestIDUserIDHour @testId, @userId, @TimeOffSet    
     IF @RCount > 0    
	 BEGIN    
	   SET @result = 1  
	 END 
	  
     IF (@result > 0)  
	  BEGIN  
		UPDATE @tempTestTbl SET ValidDate =1 WHERE ID =@I
	  END  
SET @I =@I+1;
END  
SELECT * from @tempTestTbl  where ValidDate=1  
SET NOCOUNT OFF    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetQbankTest'
GO 


