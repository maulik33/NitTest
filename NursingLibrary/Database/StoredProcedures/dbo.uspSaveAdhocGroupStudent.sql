IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroupStudent]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAdhocGroupStudent]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdhocGroupStudent]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAdhocGroupStudent]
	@AdhocGroupID INT ,
	@StudentID INT
AS
BEGIN
 INSERT INTO [StudentAdhocGroup]
 (
	 AdhocGroupID,
	 StudentID
 )
 VALUES
 (
	 @AdhocGroupID,
	 @StudentID
 )
END
GO
