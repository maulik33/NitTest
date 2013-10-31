IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestName]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestName]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestName]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TestName
	FROM Tests
	WHERE TestID = @testId
END
GO
