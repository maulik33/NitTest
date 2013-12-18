IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudentForEmail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchStudentForEmail]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchStudentForEmail]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSearchStudentForEmail]
@criteria VARCHAR(MAX)
AS
BEGIN
	
	SELECT FirstName + ' ' + lastName UserName, UserId
	FROM  NurStudentInfo
	WHERE (LEN(LTRIM(RTRIM(@criteria))) <> 0)
	AND ((FirstName like '%' + @criteria + '%')
	OR (UserName like '%' + @criteria + '%')
	OR (Email like '%' + @criteria + '%')
	OR (LastName like '%' + @criteria + '%'))
END
GO
