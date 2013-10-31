IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottAssignedInQuestion]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetLippincottAssignedInQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspGetLippincottAssignedInQuestion]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetLippincottAssignedInQuestion]
@qId as int
AS
BEGIN
 SELECT     Questions.QID, Lippincot.LippincottTitle, Lippincot.LippincottExplanation, Lippincot.LippincottTitle2,
 Lippincot.LippincottExplanation2, Lippincot.LippincottID
    FROM Lippincot INNER JOIN
    QuestionLippincott ON Lippincot.LippincottID = QuestionLippincott.LippincottID INNER JOIN
    Questions ON QuestionLippincott.QID = Questions.QID
    WHERE(Questions.QID = @qId )
    union all
    SELECT     Questions.QID, Lippincot.LippincottTitle, Lippincot.LippincottExplanation, Lippincot.LippincottTitle2,
    Lippincot.LippincottExplanation2, Lippincot.LippincottID
    FROM         Questions INNER JOIN
    Lippincot ON Questions.RemediationID = Lippincot.RemediationID
    WHERE(Questions.QID =  @qId )
END
GO
