IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertSubCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertSubCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspInsertSubCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertSubCategory]
	@ModuleStudentID int,
	@SubcategoryID int,
	@CategoryName varchar(50),
	@Correct int,
	@Total int,
	@CategoryID int
AS
BEGIN
	
	SET NOCOUNT ON;   	
	Insert into dbo.CaseSubCategory(ModuleStudentID,SubcategoryID,CategoryName,Correct,Total,CategoryID)
	values(@ModuleStudentID,@SubcategoryID,@CategoryName,@Correct,@Total,@CategoryID)
END
GO
