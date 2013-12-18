IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfInstitution]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfInstitution]
GO

/****** Object:  StoredProcedure [dbo].[uspGetListOfInstitution]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetListOfInstitution]
@institutionIds VARCHAR(400)
AS
BEGIN
  SELECT
  InstitutionID,
  InstitutionName,
  Description,
  Status,
  ContactName,
  ContactPhone,
  DefaultCohortID,
  CenterID,
  TimeZone,
  IP,UpdateDate,
  UpdateUser,
  CreateUser,
  DeleteUser,
  DeleteDate,
  FacilityID,
  ProgramID
  FROM NurInstitution WHERE Status=1
  AND (@institutionIds = '' OR @institutionIds = '0' OR InstitutionID IN (SELECT VALUE  FROM  dbo.funcListToTableInt(@institutionIds,',')))
  --OR (@institutionIds = '0' OR InstitutionID IN (SELECT VALUE  FROM  dbo.funcListToTableInt(@institutionIds,',')))
  ORDER BY InstitutionName

END
GO
