IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteProgram]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteProgram]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspDeleteProgram]
(
	@ProgramId int,
	@UserId int
)
AS
	UPDATE dbo.NurProgram
	SET	DeletedDate = GETDATE()
	WHERE ProgramId = @ProgramId
GO
