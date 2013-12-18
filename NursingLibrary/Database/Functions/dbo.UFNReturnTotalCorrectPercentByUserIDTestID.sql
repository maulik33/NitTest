IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]
GO

/****** Object:  UserDefinedFunction [dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]
(@UserTestID int )
RETURNS INT
As
begin
	DECLARE @Result int
	
	SELECT @Result = ISNULL(sum(CASE WHEN correct=1 THEN 1 ELSE 0 END),0)
	FROM dbo.UserQuestions
	Where UserTestID = @UserTestID
	group by UserTestID

	RETURN @Result
end
GO
