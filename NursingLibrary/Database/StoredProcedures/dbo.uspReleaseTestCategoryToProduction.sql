IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseTestCategoryToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseTestCategoryToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseTestCategoryToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspReleaseTestCategoryToProduction]
@Id as INT,
@TestId as INT,
@CategoryId as Varchar(50),
@Student as INT,
@Admin as INT
AS
Begin
 SET IDENTITY_INSERT TestCategory ON
   Insert Into TestCategory (id, TestID, CategoryID, Student, [Admin])
   values(@Id,@TestId,@CategoryId,@Student,@Admin)
 SET IDENTITY_INSERT TestCategory OFF
End
GO
