IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateReviewRemediationTotalTime]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateReviewRemediationTotalTime]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateReviewRemediationTotalTime]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateReviewRemediationTotalTime]
 @RemReviewId int
AS
BEGIN
 SET NOCOUNT ON;
   Declare @TotalTime as int

  select  @TotalTime = sum(RemediatedTime)
  from dbo.RemediationReviewQuestions
  where RemReviewId = @RemReviewId
	
  UPDATE RemediationReview
		 SET RemediatedTime=@TotalTime
  WHERE RemReviewId=@RemReviewId
  SET NOCOUNT OFF
END
GO
