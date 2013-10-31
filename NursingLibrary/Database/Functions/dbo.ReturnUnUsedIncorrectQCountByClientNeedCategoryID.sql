SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]
GO

PRINT 'Creating Function ReturnUnUsedIncorrectQCountByClientNeedCategoryID'
GO 

CREATE function [dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]
(@ClientNeedCategoryID int,@UserID int, @ProgramofStudyId int)
RETURNS INT
AS
BEGIN 
/*============================================================================================================                
 -- Purpose: Retrieves UnUsedIncorrectQCountByClientNeedCategoryID(Qbank) based on ProgramofStudyId
 -- Modified On:	06/07/2013
 -- Modified By:	Liju Mathews
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
DECLARE @Result int

SELECT  @Result=count(*)
FROM  dbo.Tests 
INNER JOIN dbo.TestQuestions ON dbo.Tests.TestID = dbo.TestQuestions.TestID 
INNER JOIN dbo.Questions ON dbo.TestQuestions.QID = dbo.Questions.QID
WHERE dbo.Questions.ClientNeedsCategoryID=@ClientNeedCategoryID
AND dbo.Questions.ClientNeedsCategoryID > 0
AND (dbo.Tests.ProductID = 4) AND (dbo.Tests.TestSubGroup = 3) 
AND (dbo.Tests.ProgramofStudyId = @ProgramofStudyId)
AND (dbo.TestQuestions.QID NOT IN
(SELECT   dbo.UserQuestions.QID
FROM  dbo.UserQuestions 
INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID 
INNER JOIN dbo.Tests AS Tests_1 ON dbo.UserTests.TestID = Tests_1.TestID
WHERE (dbo.UserQuestions.Correct=1) AND  dbo.UserTests.UserID=@UserID
AND (Tests_1.ProductID = 4) AND (Tests_1.TestSubGroup = 3)
AND (Tests_1.ProgramofStudyId = @ProgramofStudyId)))
GROUP BY dbo.Questions.ClientNeedsCategoryID

RETURN @Result
END 
GO
PRINT 'Finished creating Function ReturnUnUsedIncorrectQCountByClientNeedCategoryID'
GO 




