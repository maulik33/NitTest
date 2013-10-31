IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminByInstitution]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdminByInstitution]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdminByInstitution]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAdminByInstitution]
@InstitutionIds VARCHAR(MAX),
@SearchText VARCHAR(100)
AS
BEGIN
 SELECT NA.FirstName + ' ' + NA.lastName UserName, NA.UserId
 ,NA.UserName ,NA.Email,NA.LastName,NA.FirstName
 FROM dbo.NurAdmin NA
 INNER JOIN dbo.NurAdminInstitution NAI ON NA.UserID = NAI.AdminID
 WHERE ((@InstitutionIds <> '' AND NAI.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))) OR @InstitutionIds = '')
 AND (NAI.Active = 1)
	AND ( LEN(@SearchText) = 0 OR (LEN(@SearchText) > 0
	AND (NA.UserName LIKE '%' + @SearchText + '%'
	OR NA.Email LIKE '%' + @SearchText + '%'
	OR NA.LastName LIKE '%' + @SearchText + '%'
	OR NA.FirstName LIKE '%' + @SearchText + '%')))
END
GO
