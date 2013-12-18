IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveLippinCott]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveLippinCott]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveLippinCott]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSaveLippinCott]
@LippincottId as int output,
@RemediationId INT,
@LippincottTitle varchar(800),
@LippincottExplanation as ntext,
@LippincottTitle2 as varchar(800),
@LippincottExplanation2 as ntext
AS
BEGIN

	If(@LippincottId =0)
	 Begin
		 INSERT INTO dbo.Lippincot(RemediationID,LippincottTitle,LippincottExplanation,LippincottTitle2,LippincottExplanation2,ReleaseStatus)
		 values(@RemediationID,@LippincottTitle,@LippincottExplanation,@LippincottTitle2,@LippincottExplanation2,'E')
		 SET @LippincottId = CONVERT(int, SCOPE_IDENTITY())
	 End
	Else
	 Begin
		 UPDATE  Lippincot  SET RemediationID=@RemediationID,LippincottTitle=@LippincottTitle,
		      LippincottExplanation=@LippincottExplanation,LippincottTitle2=@LippincottTitle2,LippincottExplanation2=@LippincottExplanation2,ReleaseStatus='E'
		 Where  LippincottID= @LippincottId
	 End
END
GO
