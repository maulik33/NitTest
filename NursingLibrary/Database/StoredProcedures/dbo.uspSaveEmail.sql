IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveEmail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveEmail]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveEmail]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveEmail]
@emailId INT,
@title VARCHAR(MAX),
@body VARCHAR(MAX)
AS
BEGIN
	IF (@emailId = -1)
	BEGIN
		INSERT INTO [dbo].[Email]
           ([Title]
           ,[Body]
           ,[EmailType])
     VALUES
           (@title
           ,@body
           ,-99)
	END
	ELSE
	BEGIN
		UPDATE Email
		SET
			Title = @title,
			Body = @body
		WHERE EmailID = @emailId
	END
END
GO
