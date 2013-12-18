IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPrograms]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetPrograms]
GO

/****** Object:  StoredProcedure [dbo].[uspGetPrograms]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetPrograms]
AS
BEGIN
	SELECT * FROM NurProgram WHERE DeletedDate is null
	ORDER BY ProgramName asc
END
GO
