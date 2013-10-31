IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseStudyResultSubCategoryScore]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCaseStudyResultSubCategoryScore]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCaseStudyResultSubCategoryScore]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCaseStudyResultSubCategoryScore]	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.CaseSubcategory WHERE ID = IDENT_CURRENT('dbo.CaseSubCategory')
END
GO
