SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckQbankExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckQbankExists]
GO

PRINT 'Creating PROCEDURE uspCheckQbankExists'
GO

CREATE PROCEDURE [dbo].[uspCheckQbankExists]
(
@userId varchar(80),
@productId int,
@testSubGroup int,
@timeOffset int,
@result int OUTPUT  
)
AS    
BEGIN        
 /*============================================================================        
 --     Created: 06/10/2013   
 --     Author: Liju Mathews 
 --     Purpose: NURSING-3601 - For checking if the QbankTest Exists for a student        
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
DECLARE @tempTestTbl TABLE(TestID int, GroupID int, CohortID int) 
DECLARE @groupId int, @testId int, @cohortId int, @rcount int; 
INSERT INTO @tempTestTbl (CohortID,GroupID,TestID )  
   SELECT dbo.NusStudentAssign.CohortID,dbo.NusStudentAssign.GroupID,dbo.Tests.TestID 
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
    
  WHILE (Select Count(*) From @tempTestTbl) > 0
  BEGIN
	SELECT Top 1 @testId = TestID,@cohortId =CohortID,@groupId =GroupID FROM @temptestTbl
	
	EXECUTE @rcount = uspTestsExistsByTestIDCohortIDHour @testId, @cohortId, @timeOffset  
	IF @RCount > 0  
	BEGIN  
	  SET @result = 1  
	RETURN  
	END  
   
    EXECUTE @rcount = uspTestsExistsByTestIDGroupIDHour @testId, @groupId, @timeOffset  
    IF @RCount > 0  
	BEGIN  
	  SET @result = 1  
	RETURN  
	END   
   
    EXECUTE @rcount = uspTestsExistsByTestIDUserIDHour @testId, @userId, @timeOffset  
    IF @RCount > 0  
	BEGIN  
	  SET @result = 1  
	RETURN  
	END  
	
    DELETE FROM @tempTestTbl WHERE TestID = @testId
  END
  
	SET @result = 0  
    RETURN  
SET NOCOUNT OFF    
END 
GO
PRINT 'Finished creating PROCEDURE uspCheckQbankExists'
GO 


