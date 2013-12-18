IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetEmails]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetEmails]
GO

/****** Object:  StoredProcedure [dbo].[uspGetEmails]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetEmails]
@EmailId INT = 0
AS
BEGIN

SET NOCOUNT ON

	SELECT [EmailId]
      ,[Title]
      ,[Body]
      ,[EmailType]
	FROM [Email]
	WHERE (@EmailId = 0 OR (@EmailId <> 0 AND emailId = @EmailId))
		
SET NOCOUNT OFF
	
END
GO
