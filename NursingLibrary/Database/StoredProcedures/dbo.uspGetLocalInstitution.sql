IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLocalInstitution]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLocalInstitution]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLocalInstitution]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLocalInstitution]
@UserId INT
AS
BEGIN
	SELECT dbo.NurInstitution.InstitutionId,dbo.NurInstitution.InstitutionName
	FROM   dbo.NurInstitution
	INNER JOIN dbo.NurAdminInstitution ON dbo.NurInstitution.InstitutionID = dbo.NurAdminInstitution.InstitutionID
	WHERE (dbo.NurAdminInstitution.AdminID = @UserId) AND (dbo.NurAdminInstitution.Active = 1)
END
GO
