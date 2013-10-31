IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateTestCompleted]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateTestCompleted]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateTestCompleted]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateTestCompleted]
	
	
	@UserTestID int,
	@SuspendQuestionNumber int,
	@SuspendQID int,
	@SuspendType char(2),
	@TimeRemaining varchar(50)

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE UserTests SET

SuspendQuestionNumber=@SuspendQuestionNumber, SuspendQID=@SuspendQID, SuspendType=@SuspendType, TimeRemaining=@TimeRemaining, TestStatus=1, TestComplited=getdate() WHERE UserTestID=@UserTestID

	SET NOCOUNT OFF
END
GO
