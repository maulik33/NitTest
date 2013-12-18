SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAssignedStudentforCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAssignedStudentforCohort]
GO
/****** Object:  StoredProcedure [dbo].[uspGetAssignedStudentforCohort]    Script Date: 11/11/2011 18:12:53 ******/
PRINT 'Creating PROCEDURE uspGetAssignedStudentforCohort'
GO
CREATE PROCEDURE [dbo].[uspGetAssignedStudentforCohort]      
(     
 @CohortId INT,      
 @TestId INT,     
 @StartDate DATETIME,        
 @EndDate DATETIME      
)    
AS     
/*============================================================================================================    
//Purpose: When the Tests are assigned to a Cohort it checks if any student/group in that    
//          Cohort has taken/assigned this test     
//Created: 29 12 2011    
//Author :Shodhan Kini   
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
SET NOCOUNT ON    
DECLARE @ErrorMsg varchar(255)    
BEGIN     
SELECT     
	 'GROUPNAME - ' as FirstName,    
      GroupName as LastName,    
      TestId,    
      TestName    
	FROM  NurProductDatesGroup NPG     
	  INNER JOIN NurGroup NG ON NG.GroupID = NPG.GroupID    
	  INNER JOIN Tests T ON T.TestId = NPG.ProductId    
	WHERE NPG.ProductID = @TestId     
	  AND NPG.CohortID = @CohortId     
	  AND NPG.EndDate >= @StartDate    
	  AND T.ACTIVETEST = 1    
	  AND NPG.TYPE = 0    
	  AND T.ProductId = 1  
UNION    
	SELECT FirstName,    
	 LastName,    
	 TestId,    
	 TestName    
	FROM NusStudentAssign NSA    
	 INNER JOIN NurProductdatesStudent NPS ON NSA.StudentID = NPS.StudentID    
	 INNER JOIN NurStudentInfo NSI ON NSI.UserID = NSA.StudentId    
	 INNER JOIN Tests T ON T.TestId = NPS.ProductId    
	WHERE NSA.DeletedDate Is Null    
	 AND NPS.ProductID = @TestId     
	 AND NPS.CohortID = @CohortId     
	 AND NPS.GroupID NOT IN (Select DISTINCT GroupID from NurProductDatesGroup where CohortID =@CohortID and ProductID=@TestID and StartDate IS NOT NULL)    
	 AND NPS.EndDate >= @StartDate    
	 AND T.ACTIVETEST = 1    
	 AND NPS.TYPE = 0    
	 AND T.ProductId = 1  
UNION    
	SELECT     
	 FirstName,    
	 LastName,    
	 T.TestId,    
	 T.TestName    
	FROM UserTests UT     
	  INNER JOIN NusStudentAssign NSA ON UT.UserId = NSA.StudentId    
	  INNER JOIN NurStudentInfo NSI ON NSA.StudentId = NSI.UserId    
	  INNER JOIN NurCohort NC ON NC.CohortId = NSA.CohortId    
	  INNER JOIN Tests T ON T.TestId = UT.TestId     
	WHERE NSA.DeletedDate Is Null    
	   AND NSA.CohortId = @CohortId    
	   AND NSA.GroupId NOT IN(Select DISTINCT GroupID from NurProductDatesGroup where CohortID =@CohortID and ProductID=@TestID and StartDate IS NOT NULL)    
	   AND T.ProductID = 1    
	   AND UT.TestId = @TestId    
	   AND (UT.TestStarted BETWEEN NC.CohortStartDate and NC.CohortEndDate)  
	   AND T.ProductId = 1    
END     
IF @@ERROR <> 0     
 BEGIN    
   SET @ErrorMsg = 'Error while checking if students/group taken up the test other than Cohort Level assignment'    
  GOTO ERROR    
 END     
SET NOCOUNT OFF    
 RETURN @@ERROR    
ERROR:    
   RAISERROR(60100, 18, 1, @ErrorMsg)    
    RETURN -1
GO
PRINT 'Finished creating PROCEDURE uspGetAssignedStudentforCohort'
GO








