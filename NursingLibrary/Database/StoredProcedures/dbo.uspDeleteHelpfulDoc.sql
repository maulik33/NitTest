IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteHelpfulDoc]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteHelpfulDoc]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteHelpfulDoc]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteHelpfulDoc]
	@Id INT,
	@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
SET NOCOUNT ON;

	UPDATE HelpfulDocuments
		SET [Status] = 0,
		DeletedDate = GETDATE(),
		DeletedBy = @UserId
	WHERE Id=@Id

SET NOCOUNT OFF

END
GO
