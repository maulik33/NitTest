IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteNormingById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteNormingById]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteNormingById]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspDeleteNormingById]
@Id as int
AS
BEGIN
	Delete from Norming
	where id=@Id
END
GO
