IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediations]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediations]
GO

/****** Object:  StoredProcedure [dbo].[uspGetRemediations]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetRemediations]
@RemediationId as int,
@ReleaseStatus as char(1)
AS
BEGIN
  SELECT RemediationID,Explanation,TopicTitle,ReleaseStatus
  FROM Remediation
  WHERE (RemediationID=@RemediationId or @RemediationId =0)
  and ( ReleaseStatus = @ReleaseStatus or @ReleaseStatus ='')
END
GO
