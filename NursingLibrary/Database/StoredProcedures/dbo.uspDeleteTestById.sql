IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTestById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteTestById]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteTestById]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspDeleteTestById]
@TestID varchar(50)
AS
BEGIN
	delete from Tests where TestId = @TestID
END

/****** Object:  StoredProcedure [dbo].[uspGetAVPItemsByID]    Script Date: 04/18/2011 15:25:31 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetAVPItemsByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAVPItemsByID]
GO
