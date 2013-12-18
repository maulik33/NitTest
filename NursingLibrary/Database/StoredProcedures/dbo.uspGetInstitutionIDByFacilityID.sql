IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionIDByFacilityID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionIDByFacilityID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIDByFacilityID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetInstitutionIDByFacilityID]
@FID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select InstitutionID
	FROM NurInstitution
	WHERE FacilityID=@FID

END
GO
