IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteAdmin]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteAdmin]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteAdmin]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[[USPDeleteAdmin]]    Script Date: 05/17/2011  ******/
CREATE PROCEDURE [dbo].[uspDeleteAdmin]
	@UserId int
AS
BEGIN
	UPDATE NurAdmin
	SET AdminDeleteData = getdate()
	WHERE UserID = @UserId
END
GO
