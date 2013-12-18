IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProbability]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetProbability]
GO

/****** Object:  StoredProcedure [dbo].[uspGetProbability]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetProbability]
	-- Add the parameters for the stored procedure here
	@testId int, @correct int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Probability
	FROM Norming
	WHERE TestID = @testId AND NumberCorrect = @correct
END
GO
