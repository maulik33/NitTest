IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckProbabilityExist]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCheckProbabilityExist]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckProbabilityExist]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCheckProbabilityExist]
 @UserTestId int
AS
BEGIN
 DECLARE @TestId INT

 SELECT @TestId = TestId
 FROM UserTests
 WHERE UserTestID=@UserTestID

    SELECT Probability
    FROM Norming
    WHERE TestID = @testId

END
GO
