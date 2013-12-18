IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTimeZones]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetTimeZones]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTimeZones]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetTimeZones]
AS
BEGIN
	select * from dbo.TimeZones
	order by OrderNumber
END
GO
