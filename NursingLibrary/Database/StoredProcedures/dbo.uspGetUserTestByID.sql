IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserTestByID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetUserTestByID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetUserTestByID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetUserTestByID]
	
	@UserTestID int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Select TestID,TestStarted,SuspendType,UserTestID,TestName,TestNumber,SuspendQID,TimedTest,TimeRemaining,TutorMode FROM UserTests WHERE UserTestID=@UserTestID

	SET NOCOUNT OFF
END
GO
