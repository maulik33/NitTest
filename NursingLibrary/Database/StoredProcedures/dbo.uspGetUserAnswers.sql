IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserAnswers]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUserAnswers]
GO

/****** Object:  StoredProcedure [dbo].[uspGetUserAnswers]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspGetUserAnswers]
	@UserTestID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT * FROM dbo.UserAnswers WHERE   UserTestID= @UserTestID
END
GO
