IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteGroup]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteGroup]
@GroupDeleteUser int,
@GroupId int
AS

SET NOCOUNT ON

BEGIN
	UPDATE NurGroup
	SET GroupDeleteUser = @GroupDeleteUser,
	GroupDeleteDate=getdate() WHERE GroupID = @GroupID
END
GO
