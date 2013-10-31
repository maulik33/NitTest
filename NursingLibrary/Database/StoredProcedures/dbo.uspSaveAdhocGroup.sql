IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroup]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveAdhocGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdhocGroup]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveAdhocGroup]
 @AdhocGroupID INT OUTPUT ,
 @AdhocGroupName VARCHAR(50),
 @IsADAGroup BIT,
 @ADA BIT,
 @CreatedBy INT ,
 @CreatedDate DATETIME
AS

BEGIN
 INSERT INTO [AdhocGroup]
 (
	 AdhocGroupName,
	 IsAdaGroup,
	 ADA,
	 CreatedDate,
	 CreatedBy
 )
 VALUES
 (
	 @AdhocGroupName,
	 @IsADAGroup,
	 @ADA,
	 @CreatedDate,
	 @CreatedBy
 )

 SELECT @AdhocGroupID = SCOPE_IDENTITY()
END
GO
