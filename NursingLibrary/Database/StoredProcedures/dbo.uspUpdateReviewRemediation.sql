IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateReviewRemediation]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateReviewRemediation]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateReviewRemediation]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateReviewRemediation]
 @RemReviewQuestionId int,
 @RemTime int
AS
BEGIN
 SET NOCOUNT ON;
  UPDATE RemediationReviewQuestions
		 SET RemediatedTime=@RemTime
  WHERE RemReviewQuestionId=@RemReviewQuestionId
END
GO
