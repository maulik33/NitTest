IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincottById]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincottById]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincottById]
@LippincottId int
AS
BEGIN
	select * from Lippincot where LippincottID = @LippincottId
END
GO
