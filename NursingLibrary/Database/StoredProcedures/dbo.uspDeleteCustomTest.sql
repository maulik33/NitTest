IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteCustomTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteCustomTest]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteCustomTest]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteCustomTest]
 @TestId int
AS
BEGIN
     UPDATE Tests SET ActiveTest = 0
      WHERE TestID = @TestId
END
GO
