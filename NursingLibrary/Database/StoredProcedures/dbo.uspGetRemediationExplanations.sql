IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationExplanations]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediationExplanations]
GO

/****** Object:  StoredProcedure [dbo].[uspGetRemediationExplanations]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetRemediationExplanations] @RemReviewId AS INT  
AS  
    BEGIN  
        SELECT DISTINCT    RRQ.RemReviewId ,  
						   RRQ.RemReviewQuestionId,  
						   R.Explanation,  
						   RRQ.RemediationNumber,  
						   RR.Name,  
						   RRQ.RemediatedTime,  
						   R.TopicTitle  
        FROM    dbo.RemediationReviewQuestions RRQ  
				INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId  
				INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId  
        WHERE RRQ.RemReviewId = @RemReviewId 
        order by RRQ.RemediationNumber  
    END 

GO

