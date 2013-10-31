IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroupTestDate]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAdhocGroupTestDate]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdhocGroupTestDate]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAdhocGroupTestDate]
(
@TestID INT,
@AdhocGroupID INT,
@StartDate DATETIME,
@EndDate DATETIME,
@CreatedBy INT,
@CreatedDate DATETIME
)
 AS

 BEGIN
  INSERT INTO AdhocGroupTestDetail
  (
   TestID,
   AdhocGroupID,
   StartDate,
   EndDate,
   CreatedBy,
   CreatedDate
  )
  VALUES
  (
   @TestID,
   @AdhocGroupID,
   @StartDate,
   @EndDate,
   @CreatedBy,
   @CreatedDate
  )
 END
GO
