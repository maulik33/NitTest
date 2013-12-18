IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseStudyResultModuleScore]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCaseStudyResultModuleScore]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCaseStudyResultModuleScore]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCaseStudyResultModuleScore]	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.CaseModuleScore WHERE ModuleStudentID = IDENT_CURRENT('dbo.CaseModuleScore')
END
GO
