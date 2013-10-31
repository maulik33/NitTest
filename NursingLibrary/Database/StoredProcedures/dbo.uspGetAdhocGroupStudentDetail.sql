IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdhocGroupStudentDetail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdhocGroupStudentDetail]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdhocGroupStudentDetail]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspGetAdhocGroupStudentDetail]
  @AdhocGroupId as int
AS
Begin
 SELECT
  SAG.StudentId,
  NSA.CohortId,
  NSA.GroupId
 FROM nusstudentassign NSA inner join StudentAdhocGroup SAG
      on NSA.StudentID = SAG.StudentID
 where AdhocGroupID = @AdhocGroupId
End
GO
