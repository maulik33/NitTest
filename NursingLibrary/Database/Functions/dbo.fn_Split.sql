
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[fn_Split]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Split]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_Split](@text varchar(8000), @delimiter varchar(20))
RETURNS @Strings TABLE
(    
  QID int,
  UTID int 
)
AS
BEGIN
 
DECLARE @index int, @seedIdx int, @floatIdx int, @floatIdx2 int
DECLARE @qid int, @utid int, @aid int
DECLARE @temp varchar(2000)
SET @index = -1
SET @seedIdx = 1 
 
WHILE (LEN(@text) > 0) 
  BEGIN  
    SET @index = CHARINDEX(@delimiter , @text)  
    IF (@index = 0) AND (LEN(@text) > 0)  
      BEGIN   
        INSERT INTO @Strings (QID) VALUES (@text)
          BREAK  
      END  
    IF (@index > 1)  
      BEGIN
		WHILE (LEN(@temp) > 0)
		BEGIN
		SET @temp = SUBSTRING(@text, @seedIdx, @index - 1)
		SET @floatIdx = CHARINDEX(';', @temp)
				
		IF(@floatIdx > 1)
		BEGIN
			SET @qid	= SUBSTRING(@temp, 1, @floatIdx - 1)
			SET @floatIdx2 = @floatIdx
			SET @floatIdx = CHARINDEX(';', @temp, @floatIdx + 1)
			SET @utid	= SUBSTRING(@temp, @floatIdx2 + 1, 1)
--			SET @floatIdx2 = @floatIdx
--			SET @floatIdx = CHARINDEX(';', @temp, @floatIdx + 1)
--			SET @aid	= SUBSTRING(@temp, @floatIdx2 + 1, 1)
		END

        INSERT INTO @Strings (QID, UTID) VALUES (@qid, @utid)   

		SET @seedIdx = @index
		SET @temp = RIGHT(@temp, (LEN(@temp) - @floatIdx))
END
        SET @text = RIGHT(@text, (LEN(@text) - @index))  
      END  
    ELSE 
      SET @text = RIGHT(@text, (LEN(@text) - @index)) 
    END
  RETURN
END
GO
