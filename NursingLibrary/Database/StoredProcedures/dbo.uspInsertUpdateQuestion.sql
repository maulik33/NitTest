IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertUpdateQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspInsertUpdateQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspInsertUpdateQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspInsertUpdateQuestion]
@QId as int,
@QuestionId as Varchar(50),
@RemediationId as int,
@TopicTitle as Varchar(500)
AS
BEGIN
Declare @Id as int
    set @Id = @QId
	If(@QId = 0)
	 Begin
	  INSERT INTO dbo.Questions(QuestionId,RemediationId,TopicTitleId ) Values(@QuestionId,@RemediationId,@TopicTitle)
	  set @Id = Scope_identity()
	 End
	Else
	 Begin
		UPDATE  Questions  SET QuestionId=@QuestionId, TopicTitleId=@TopicTitle, RemediationId = @RemediationId
		WHERE QID=@QId
	 End
	 select QID,QuestionId,RemediationId,TopicTitleId from Questions where QID = @Id
END
GO
