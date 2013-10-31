IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCustomFRTestQuestionCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCustomFRTestQuestionCount]
GO
/****** Object:  StoredProcedure [dbo].[uspGetCustomFRTestQuestionCount]    Script Date: 10/20/2011 17:19:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCustomFRTestQuestionCount]
	@UserTestId int,
	@TotalCount int Output
AS
BEGIN
	SELECT @TotalCount =COUNT(QID)FROM UserQuestions
	WHERE UserTestId =@UserTestId
END
GO
  


