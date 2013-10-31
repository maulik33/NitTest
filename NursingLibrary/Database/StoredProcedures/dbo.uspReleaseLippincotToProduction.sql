IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReleaseLippincotToProduction]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspReleaseLippincotToProduction]
GO

/****** Object:  StoredProcedure [dbo].[uspReleaseLippincotToProduction]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[uspReleaseLippincotToProduction]
@LippincottId as int,
@RemediationId as int,
@LippincottTitle as varchar(800),
@LippincottExplanation ntext,
@LippincottTitle2 as varchar(800),
@LippincottExplanation2 ntext
As
Begin
 Delete Lippincot where LippincottID=@LippincottId
 SET IDENTITY_INSERT Lippincot ON
 Insert Into Lippincot
 (LippincottID, RemediationID, LippincottTitle, LippincottExplanation, LippincottTitle2,LippincottExplanation2)
 Values(@LippincottId,@RemediationId,@LippincottTitle,@LippincottExplanation,@LippincottTitle2,@LippincottExplanation2)
 SET IDENTITY_INSERT Lippincot OFF
End
GO
