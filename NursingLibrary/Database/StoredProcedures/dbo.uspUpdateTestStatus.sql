IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateTestStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateTestStatus]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateTestStatus]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateTestStatus]
	@userTestId int, @testStatus int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE UserTests
	SET TestStatus = @testStatus, TestComplited = GetDate()
	WHERE UserTestID = @userTestId
END
GO
