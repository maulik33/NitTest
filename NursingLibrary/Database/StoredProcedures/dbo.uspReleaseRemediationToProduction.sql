IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseRemediationToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseRemediationToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseRemediationToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspReleaseRemediationToProduction]
 @RemediationId as int,
 @Explanation as varchar(5000),
 @TopicTitle as varchar(500)
AS
Begin
if exists(select RemediationID from Remediation where RemediationID = @RemediationId)
 Begin
   UPDATE Remediation
   SET
   Remediation.Explanation = @Explanation,
   Remediation.TopicTitle = @TopicTitle
   Where RemediationID = @RemediationId
 End
Else
   Begin
    SET IDENTITY_INSERT Remediation ON
    Insert Into Remediation (RemediationID, Explanation, TopicTitle)
    Values(@RemediationId,@Explanation,@TopicTitle)
    SET IDENTITY_INSERT Remediation OFF
   End
End
GO
