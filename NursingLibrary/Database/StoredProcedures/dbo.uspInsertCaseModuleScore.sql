IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertCaseModuleScore]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertCaseModuleScore]
GO

/****** Object:  StoredProcedure [dbo].[uspInsertCaseModuleScore]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspInsertCaseModuleScore]
@CaseID int,
@MID int,
@StudentID varchar(50),
@Correct int,
@Total int,
@Id INT OUTPUT
AS
Begin
	INSERT INTO dbo.CaseModuleScore (CaseID,ModuleID,StudentID,Correct,Total)
	Values (@CaseID,@MID,@StudentID,@Correct,@Total)
	set @Id = Scope_Identity()
	select @@Identity
End
GO
