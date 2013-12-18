IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestCategories]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestCategories]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestCategories]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestCategories]
 @TestId int,
 @TestIds Varchar(4000)
AS
  BEGIN
    Select id,TestID,CategoryID,Student,[Admin] from TestCategory
    where (TestID = @TestId  or @TestId =0) and
    ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')
  End
GO
