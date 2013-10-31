IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveRemediation]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveRemediation]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveRemediation]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveRemediation]
@RemediationId as int OUTPUT,
@Explanation as Varchar(5000),
@TopicTitle as Varchar(500),
@ReleaseStatus as char(1)
AS
BEGIN
 If(@RemediationID = 0)
  Begin
   INSERT INTO Remediation(Explanation, TopicTitle, ReleaseStatus) Values(@Explanation,@TopicTitle,@ReleaseStatus)
   SET @RemediationID = CONVERT(int, SCOPE_IDENTITY())
  End
 Else
  Begin
  UPDATE  Remediation  SET Explanation=@Explanation, TopicTitle=@TopicTitle, ReleaseStatus = @ReleaseStatus WHERE RemediationID=@RemediationID
  End
END
GO
