IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetModule]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetModule]
GO

/****** Object:  StoredProcedure [dbo].[uspGetModule]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetModule]
AS

BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 SELECT ModuleID,ModuleName
 FROM  NurModule

 SET NOCOUNT OFF
END
GO
