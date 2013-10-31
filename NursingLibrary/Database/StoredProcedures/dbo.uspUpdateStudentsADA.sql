IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateStudentsADA]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateStudentsADA]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateStudentsADA]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateStudentsADA]
  @Students as varchar(2000) ,
  @Ada bit
 AS
 BEGIN
  Update  dbo.NurStudentInfo
  set ADA = @Ada
  where UserID in
        (select *
         from dbo.funcListToTableInt(@Students,'|'))
 END
GO
