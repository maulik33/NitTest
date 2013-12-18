IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationTimeForNCLEX]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediationTimeForNCLEX]
GO

/****** Object:  StoredProcedure [dbo].[uspGetRemediationTimeForNCLEX]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetRemediationTimeForNCLEX]
@UserTestID INT,
@TypeOfFileID CHAR(2)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT  dbo.ClientNeeds.ClientNeeds, dbo.Questions.ClientNeedsCategoryID, dbo.ClientNeedCategory.ClientNeedCategory,
            dbo.NursingProcess.NursingProcess,  dbo.Questions.QID, dbo.Questions.QuestionID,dbo.Questions.QuestionType,dbo.Questions.RemediationID,
            dbo.Remediation.TopicTitle,dbo.Demographic.Demographic,
            dbo.UserQuestions.QuestionNumber,TypeOfFileID, dbo.UserQuestions.TimeSpendForQuestion, dbo.UserQuestions.Correct,
            dbo.UserQuestions.TimeSpendForExplanation,
            dbo.Tests.TestName, dbo.UserQuestions.TimeSpendForRemedation

            FROM  dbo.UserQuestions INNER JOIN
            dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  INNER JOIN
            dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID  INNER JOIN

            dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
            dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID LEFT OUTER JOIN
            dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID LEFT OUTER JOIN
            dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID LEFT OUTER JOIN
            dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
            LEFT OUTER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID

            WHERE     (dbo.UserQuestions.UserTestID = @UserTestID) AND TypeOfFileID=@TypeOfFileID

	
	SET NOCOUNT OFF
END
GO
