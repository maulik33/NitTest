IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspChangePassword]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspChangePassword]
GO

/****** Object:  StoredProcedure [dbo].[uspChangePassword]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspChangePassword]
	@userId int, @password varchar(50)
AS
BEGIN
	UPDATE NurStudentInfo
	SET UserPass = @password
	WHERE UserID = @userId
END
GO
