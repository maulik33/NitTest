IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspActiveTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspActiveTests]
GO

/****** Object:  StoredProcedure [dbo].[uspActiveTests]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspActiveTests]
@ProductId int,
@TestSubGroup int,
@TestID INT
AS
BEGIN
	select * from Tests
	where ProductID=@ProductId and ActiveTest=1 and TestSubGroup=@TestSubGroup and (TestId = @TestID or @TestID = -1 )
END
GO
