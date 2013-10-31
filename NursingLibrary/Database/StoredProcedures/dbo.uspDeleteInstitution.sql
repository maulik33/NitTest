IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteInstitution]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteInstitution]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteInstitution]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteInstitution]
@InstitutionId as int ,
@DeleteUserId as int
AS
BEGIN
	SET NOCOUNT ON
	UPDATE NurInstitution
		SET DeleteUser=@DeleteUserId,
		DeleteDate=getdate(),
		Status=0
	WHERE InstitutionID=@InstitutionId
	SET NOCOUNT OFF
END
GO
