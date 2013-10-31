IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortIdByCohortDesc]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCohortIdByCohortDesc]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortIdByCohortDesc]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCohortIdByCohortDesc]
@ClassCode varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CohortID
	FROM NurCohort
	WHERE CohortDescription= @ClassCode
END
GO
