IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteRemediationReview]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteRemediationReview]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteRemediationReview]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteRemediationReview]
 @RemediationReviewId int
AS
BEGIN
 UPDATE RemediationReview
		SET status = 1
 WHERE RemReviewId = @RemediationReviewId
END
GO
