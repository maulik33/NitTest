IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSpecialtyAreaCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetSpecialtyAreaCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetSpecialtyAreaCategory]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetSpecialtyAreaCategory]
AS
SELECT SpecialtyAreaID AS [Id], SpecialtyArea AS [Description], ProgramofStudyId 
FROM SpecialtyArea
GO
