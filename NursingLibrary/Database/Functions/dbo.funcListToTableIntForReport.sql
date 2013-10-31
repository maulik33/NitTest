IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[funcListToTableIntForReport]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[funcListToTableIntForReport]
GO

/****** Object:  UserDefinedFunction [dbo].[funcListToTableIntForReport]    Script Date: 03/28/2013 18:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[funcListToTableIntForReport](@list as varchar(max), @delim as varchar(10))
RETURNS @listTable table(
  Value INT
  )
AS
/*============================================================================================================          
--      Purpose: Create a table variable using the specified character delimited string            
--      Created On: 03/28/2013          
--      Created By: Atul Gupta 
--      Description : Only used in "Multi Campus report card page currently. As part of fix for "NURSING-3415" issue. 
        Execution Sample: "Select * from [dbo].[funcListToTableIntForReport]('655|333|423|553|559|565|560', '|')"
******************************************************************************          
* This software is the confidential and proprietary information of          
* Kaplan,Inc. ("Confidential Information").  You shall not          
* disclose such Confidential Information and shall use it only in          
* accordance with the terms of the license agreement you entered into          
* with Kaplan, Inc.          
*          
* KAPLAN, INC. MAKES NO REPRESENTATIONS OR WARRANTIES ABOUT THE           
* SUITABILITY OF THE SOFTWARE, EITHER EXPRESS OR IMPLIED, INCLUDING BUT           
* NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR          
* A PARTICULAR  PURPOSE, OR NON-INFRINGEMENT. KAPLAN, INC. SHALL           
* NOT BE LIABLE FOR ANY DAMAGES SUFFERED BY LICENSEE AS A RESULT OF           
* USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.          
*****************************************************************************/            
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
