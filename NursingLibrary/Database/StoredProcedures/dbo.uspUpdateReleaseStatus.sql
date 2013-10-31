IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateReleaseStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateReleaseStatus]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateReleaseStatus]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateReleaseStatus]
@Ids as Nvarchar(2000),
@ReleaseStatus as char(1),
@ReleaseChoice as nVarchar(50)
AS
BEGIN
   If(@ReleaseChoice = 'Questions')
    Begin
        UPDATE dbo.Questions
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        WHERE QID IN (SELECT * FROM dbo.funcListToTableInt(@Ids,'|'))
    End
   Else If(@ReleaseChoice='Remediation')
       Begin
        UPDATE dbo.Remediation
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  RemediationID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
   Else If(@ReleaseChoice='Lippincot')
       Begin
        Update dbo.Lippincot
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  LippincottID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
    Else If(@ReleaseChoice='Tests')
       Begin
        Update dbo.Tests
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  TestID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
END
GO
