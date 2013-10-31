IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateCustomEmailToCohort]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspCreateCustomEmailToCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspCreateCustomEmailToCohort]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspCreateCustomEmailToCohort]
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent BIT,
@CohortIds VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

	SET @EmailMissionId = SCOPE_IDENTITY()
	
	INSERT INTO EmailCohort
           ([EmailMissionID]
           ,[CohortId])
	SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@CohortIds,'|')
	
END
GO
