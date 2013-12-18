IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseStudies]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCaseStudies]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCaseStudies]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCaseStudies]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT CaseID, CaseName, CaseOrder FROM  NurCase
END
GO
