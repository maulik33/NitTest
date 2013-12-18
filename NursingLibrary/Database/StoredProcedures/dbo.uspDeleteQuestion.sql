IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspDeleteQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteQuestion]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteQuestion]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspDeleteQuestion]
@QuestionId INT
AS
	BEGIN
	SET NOCOUNT ON
		UPDATE Questions
		SET ACTIVE=0
		WHERE QID=@QuestionId
	SET NOCOUNT OFF
	END
GO
