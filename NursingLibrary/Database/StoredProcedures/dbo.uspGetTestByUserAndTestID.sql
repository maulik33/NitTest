IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestByUserAndTestID]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTestByUserAndTestID]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestByUserAndTestID]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTestByUserAndTestID]
	
	@userId int,@testId int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT   UserTestID,TestID,TestNumber,TestStarted,TestName,Override,SuspendType FROM  UserTests WHERE TestID = @testId and UserID= @userId
	SET NOCOUNT OFF
END
GO
