IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdhocGroupTests]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAdhocGroupTests]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdhocGroupTests]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetAdhocGroupTests]
  @AdhocGroupId as int
AS
Begin
 SELECT
  AdhocGroupTestDetailID,
  T.TestID,
  AdhocGroupID,
  StartDate,
  EndDate,
  CreatedBy,
  CreatedDate,
  TestName
 FROM AdhocGroupTestDetail AG inner join
      Tests T on AG.TestID = T.TestId
 where AdhocGroupID = @AdhocGroupId
End
GO
