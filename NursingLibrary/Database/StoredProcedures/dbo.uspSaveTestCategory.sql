IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveTestCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveTestCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveTestCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveTestCategory]
 @TestId int,
 @CategoryId as int,
 @Student as int,
 @Admin as int
AS
BEGIN
Declare @count as int
select @count = count(*) from dbo.TestCategory
where TestID=@TestId and CategoryID=@CategoryId

 If (@count>0)
  Begin
   UPDATE TestCategory SET Student=@Student,[Admin] =@Admin
   WHERE TestID=@TestId And CategoryID=@CategoryId
  End
 Else
  Begin
   INSERT INTO TestCategory (TestID,CategoryID,Student,[Admin])
   values(@TestId,@CategoryId,@Student,@Admin)
  End
END
GO
