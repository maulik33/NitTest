IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfRem]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetListOfRem]
GO

/****** Object:  StoredProcedure [dbo].[uspGetListOfRem]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetListOfRem]

@RemediationID as int,
@Text as varchar(max)

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

SELECT * FROM Remediation WHERE
(@RemediationID = 0 OR RemediationID=@RemediationID)
AND ((@Text = '' OR @Text is null) OR (Explanation like '%' + @Text + '%' OR TopicTitle like '%' + @Text + '%'))

SET NOCOUNT OFF;
END
GO
