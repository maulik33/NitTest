IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckExistCaseModuleStudent]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCheckExistCaseModuleStudent]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckExistCaseModuleStudent]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCheckExistCaseModuleStudent]
	
	@CID int,
	@MID int,
	@SID varchar(50)

	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 SELECT * FROM CaseModuleScore  WHERE  StudentID =@SID AND ModuleID = @MID AND CaseID = @CID

	SET NOCOUNT OFF
END
GO
