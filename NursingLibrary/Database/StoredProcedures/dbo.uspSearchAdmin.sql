IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAdmin]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchAdmin]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchAdmin]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSearchAdmin]
@criteria VARCHAR(MAX)
AS
BEGIN
	
	SELECT FirstName + ' ' + lastName UserName, UserId
	FROM  NurAdmin
	WHERE (LEN(LTRIM(RTRIM(@criteria))) <> 0)
	AND ((FirstName like '%' + @criteria + '%')
	OR (UserName like '%' + @criteria + '%')
	OR (Email like '%' + @criteria + '%')
	OR (LastName like '%' + @criteria + '%'))

END
GO
