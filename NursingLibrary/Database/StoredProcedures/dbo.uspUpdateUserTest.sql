IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateUserTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateUserTest]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateUserTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateUserTest](@UserTestID int, @SuspendQuestionNumber int,
	@SuspendQID int, @SuspendType char(2), @TimeRemaining varchar(50))
AS

UPDATE UserTests
SET SuspendQuestionNumber=@SuspendQuestionNumber, SuspendQID=@SuspendQID,
	SuspendType=@SuspendType, TimeRemaining=@TimeRemaining
WHERE UserTestID = @UserTestID
GO
