IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnGetTestTimeUsed]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[ReturnGetTestTimeUsed]
GO
PRINT 'Creating Function ReturnGetTestTimeUsed'
GO 
CREATE function [dbo].[ReturnGetTestTimeUsed]      
 (@userTestId INT)    
RETURNS Varchar(50)      
AS      
BEGIN

/*============================================================================================================
--      Purpose: Returns TimeSpend for Question of format hh:mm:ss
--      Created: 12/20/2011
--	    Author:  Kannan
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
DECLARE @Result int    
DECLARE @Value Varchar(50)  
 Select @Result=  SUM(TimeSpendForQuestion) from UserQuestions where usertestId =@userTestId    
  
 RETURN (SELECT REPLACE(( right('0' + rtrim(convert(char(2), @Result/ (60 * 60))), 2) + ':' +    
 right('0' + rtrim(convert(char(2), (@Result / 60) % 60)), 2) + ':' +     
 right('0' + rtrim(convert(char(2), @Result % 60)),2)), '*','0' ))  
      
END      

GO
PRINT 'Finished creating Function ReturnGetTestTimeUsed'
GO 
