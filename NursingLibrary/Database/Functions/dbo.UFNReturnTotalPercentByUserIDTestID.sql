IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnTotalPercentByUserIDTestID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNReturnTotalPercentByUserIDTestID]
GO

/****** Object:  UserDefinedFunction [dbo].[UFNReturnTotalPercentByUserIDTestID]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[UFNReturnTotalPercentByUserIDTestID]
(@UserTestID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT @Result = ISNULL(count(correct),0)
FROM dbo.UserQuestions
Where UserTestID = @UserTestID
group by UserTestID

RETURN @Result
END
GO
