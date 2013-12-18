IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateIntegratedusers]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspUpdateIntegratedusers]
GO

/****** Object:  StoredProcedure [dbo].[uspUpdateIntegratedusers]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUpdateIntegratedusers]
 @StudentId int,
 @CohortId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 UPDATE NusStudentAssign
 SET CohortID=@CohortId,Access=1
 WHERE StudentID= @StudentId
END
GO
