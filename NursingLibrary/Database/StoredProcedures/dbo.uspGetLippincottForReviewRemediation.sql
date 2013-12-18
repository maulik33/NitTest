IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottForReviewRemediation]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincottForReviewRemediation]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincottForReviewRemediation]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincottForReviewRemediation]
    @RemReviewQuestionId AS INT
AS
    BEGIN
        SELECT  L.LippincottTitle ,
                L.LippincottExplanation ,
                L.LippincottTitle2 ,
                L.LippincottExplanation2 ,
                L.LippincottID
        FROM    Questions Q
                INNER JOIN Lippincot L ON Q.RemediationID = L.RemediationID
                INNER JOIN dbo.RemediationReviewQuestions RRQ ON Q.RemediationID = RRQ.RemediationId
        WHERE   RRQ.RemReviewQuestionId = @RemReviewQuestionId

    END
GO
