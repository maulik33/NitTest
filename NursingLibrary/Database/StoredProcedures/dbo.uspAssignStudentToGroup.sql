IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignStudentToGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspAssignStudentToGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignStudentToGroup]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspAssignStudentToGroup]    Script Date: 05/17/2011  ******/


CREATE PROC [dbo].[uspAssignStudentToGroup]
(
	@GroupId int ,
	@AssignStudentList VARCHAR(4000) ,
	@UnassignedStudentList VARCHAR(4000)
)
AS
BEGIN
	IF LEN(@AssignStudentList) > 0
		BEGIN
			UPDATE  NusStudentAssign SET GroupID = @GroupId
			WHERE  StudentID IN (SELECT value
			FROM dbo.funcListToTableInt(@AssignStudentList,'|'))
		END

	IF LEN(@UnassignedStudentList) > 0
		BEGIN
			UPDATE  NusStudentAssign SET GroupID=0 WHERE  StudentID IN (SELECT value
			FROM dbo.funcListToTableInt(@UnassignedStudentList,'|'))
		END
END
GO
