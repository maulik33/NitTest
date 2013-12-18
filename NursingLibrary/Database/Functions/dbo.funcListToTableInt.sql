IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[funcListToTableInt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[funcListToTableInt]
GO

/****** Object:  UserDefinedFunction [dbo].[funcListToTableInt]    Script Date: 10/10/2011 15:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[funcListToTableInt](@list as varchar(8000), @delim as varchar(10))
RETURNS @listTable table(
  Value INT
  )
AS
BEGIN
    --Declare helper to identify the position of the delim
    DECLARE @DelimPosition INT
    
    --Prime the loop, with an initial check for the delim
    SET @DelimPosition = CHARINDEX(@delim, @list)

    --Loop through, until we no longer find the delimiter
    WHILE @DelimPosition > 0
    BEGIN
        --Add the item to the table
        INSERT INTO @listTable(Value)
            VALUES(CAST(RTRIM(LEFT(@list, @DelimPosition - 1)) AS INT))
    
        --Remove the entry from the List
        SET @list = right(@list, len(@list) - @DelimPosition)

        --Perform position comparison
        SET @DelimPosition = CHARINDEX(@delim, @list)
    END

    --If we still have an entry, add it to the list
    IF len(@list) > 0
        insert into @listTable(Value)
        values(CAST(RTRIM(@list) AS INT))

  RETURN
END
GO
