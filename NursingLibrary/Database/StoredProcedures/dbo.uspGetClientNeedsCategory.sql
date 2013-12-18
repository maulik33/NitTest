IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedsCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetClientNeedsCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetClientNeedsCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetClientNeedsCategory]
AS
SELECT ClientNeedsID AS [Id], ClientNeeds AS [Description], ProgramofStudyId 
FROM ClientNeeds
GO
