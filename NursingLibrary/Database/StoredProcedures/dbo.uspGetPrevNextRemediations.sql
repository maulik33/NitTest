IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPrevNextRemediations]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetPrevNextRemediations]
GO

/****** Object:  StoredProcedure [dbo].[uspGetPrevNextRemediations]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetPrevNextRemediations]
    @RemReviewId INT ,
    @RemediationNumber INT ,
    @Action CHAR(1)
AS
    BEGIN
        SET NOCOUNT ON ;
        IF ( @Action = 'N' )
            BEGIN
                SELECT TOP 1
                        RRQ.RemReviewQuestionId ,
                        RRQ.RemediationNumber ,
                        R.Explanation ,
                        RRQ.RemediatedTime ,
                        RR.Name
                FROM    dbo.RemediationReviewQuestions RRQ
                        INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId
                        INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId
                WHERE   RRQ.RemReviewId = @RemReviewId
                        AND RemediationNumber > @RemediationNumber
                ORDER BY RemediationNumber
            END
        ELSE
            IF ( @Action = 'P' )
                BEGIN
                    SELECT TOP 1
                            RRQ.RemReviewQuestionId ,
                            RRQ.RemediationNumber ,
                            R.Explanation ,
                            RRQ.RemediatedTime ,
                            RR.Name
                    FROM    dbo.RemediationReviewQuestions RRQ
                            INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId
                            INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId
                    WHERE   RRQ.RemReviewId = @RemReviewId
                            AND RemediationNumber < @RemediationNumber
                    ORDER BY RemediationNumber DESC
                END

        SET NOCOUNT OFF ;
    END
GO
