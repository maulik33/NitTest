IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateTestsReleaseStatusById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateTestsReleaseStatusById]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateTestsReleaseStatusById]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateTestsReleaseStatusById]
@TestId as int,
@ReleaseStatus as char(1)
AS
BEGIN
	Update Tests Set ReleaseStatus=@ReleaseStatus
	WHERE TestID = @TestId
END
GO
