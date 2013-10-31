SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetAssignedStudentforGroup]    Script Date: 11/15/2011 14:07:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAssignedStudentforGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAssignedStudentforGroup]
GO
PRINT 'Creating PROCEDURE uspGetAssignedStudentforGroup'
GO
CREATE PROCEDURE [dbo].[uspGetAssignedStudentforGroup]  
(
	@GroupId INT,   
	@CohortId INT,  
	@TestId INT, 
	@StartDate DATETIME,    
	@EndDate DATETIME
)
AS 
/*============================================================================================================
// Purpose: When the Tests are assigned to a group it checks if any student in that
//          group has taken/individually assigned this test 
// Created: Nov 11 2011
   Author:Liju Mathews
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
	NSI.UserID,
	FirstName,
	LastName,
	TestId,
	TestName
FROM NUSSTUDENTASSIGN NSA   
	INNER JOIN NurProductDatesStudent NPD ON NSA.StudentID = NPD.StudentID 
	INNER JOIN NurStudentInfo NSI ON NSA.StudentID = NSI.UserID
	INNER JOIN Tests T ON NPD.ProductID = T.TestId  
WHERE NSA.DeletedDate Is Null
	AND NPD.ProductID = @TestId  
	AND NPD.GroupID = @GroupId  
	AND NPD.CohortID = @CohortId 
	AND NPD.EndDate >= @StartDate
	AND T.ProductID = 1  
	AND T.ACTIVETEST = 1
	AND TYPE = 0
UNION
SELECT 
	 NSI.UserID,
	 FirstName,
	 LastName,
	 T.TestId,
	 T.TestName
FROM UserTests UT 
	 INNER JOIN NusStudentAssign NSA ON UT.UserId = NSA.StudentId
	 INNER JOIN NurStudentInfo NSI ON NSA.StudentId = NSI.UserId
	 INNER JOIN NurCohort NC ON NC.CohortId = NSA.CohortId
	 INNER JOIN Tests T ON T.TestId = UT.TestId 
WHERE NSA.GroupId = @GroupId
	 AND NSA.DeletedDate Is Null
	 AND T.ProductID = 1
	 AND UT.TestId = @TestId
	 AND (UT.TestStarted BETWEEN NC.CohortStartDate and NC.CohortEndDate)
	
END
IF @@ERROR <> 0 
	BEGIN
	  SET @ErrorMsg = 'Error while checking if students taken up the test other than Group Level assignment'
		GOTO ERROR
	END	
SET NOCOUNT OFF
	RETURN @@ERROR
ERROR:
	  RAISERROR(60100, 18, 1, @ErrorMsg)
    RETURN -1
GO
PRINT 'Finished creating PROCEDURE uspGetAssignedStudentforGroup'
GO


