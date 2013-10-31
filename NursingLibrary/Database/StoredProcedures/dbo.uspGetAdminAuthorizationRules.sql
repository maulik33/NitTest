IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminAuthorizationRules]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdminAuthorizationRules]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdminAuthorizationRules]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[uspGetAdminAuthorizationRules]
AS
SELECT
	SecurityLevel,
	AdminType,
	I_Add,
	I_Edit,
	I_Delete,
	C_Add,
	C_Edit,
	C_AccessDatesEdit,
	C_Delete,
	C_AssisgnStudents,
	C_EditTestDates,
	C_AssignProgram,
	G_Add,
	G_Edit,
	G_Delete,
	G_EditTestDates,
	G_AssignStudents,
	S_Add,
	S_Edit,
	S_Delete,
	S_AssignToCohort,
	S_AssignToGroup,
	S_EditTestDates,
	A_Add,
	A_Edit,
	A_Delete,
	P_Add,
	P_Edit,
	P_Delete,
	P_AssignTests,
	Cms,
	R_InstitutionResults,
	R_CohortResults,
	R_GroupResults,
	R_StudentResults,
	R_KaplanReport
FROM dbo.NurAdminSecurity
GO
