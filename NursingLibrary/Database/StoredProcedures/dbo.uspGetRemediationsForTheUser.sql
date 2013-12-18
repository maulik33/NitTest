IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationsForTheUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediationsForTheUser]
GO

/****** Object:  StoredProcedure [dbo].[uspGetRemediationsForTheUser]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[uspGetRemediationsForTheUser]
 @studentId int
AS
BEGIN
 SET NOCOUNT ON;
    SELECT
	RemReviewId,
	Name,
	StudentId,
	NoOfRemediations,
	DATEADD(hour, TZ.[Hour], CreatedDate) as CreatedDate

	FROM RemediationReview AS RR
			Inner Join NurStudentInfo AS NS ON RR.StudentId = NS.UserId
			Inner Join NurInstitution AS I ON I.InstitutionId = NS.InstitutionId
			Inner Join TimeZones TZ ON I.TimeZone = TZ.TimeZoneID
	WHERE StudentId=@studentId
	AND isnull(RR.status,0)=0

SET NOCOUNT Off;
END
GO
