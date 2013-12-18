  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnAnsweredQuestionCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNReturnAnsweredQuestionCount]
GO

PRINT 'Creating Function UFNReturnAnsweredQuestionCount'
GO 
CREATE function [dbo].[UFNReturnAnsweredQuestionCount]    
(@UserTestID int)  
RETURNS INT  
AS    
BEGIN 

/*============================================================================================================
--      Purpose: Returns count of answered questions  
--      Created: 12/20/2011
--	    Author:Shodhan Kini
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
 DECLARE @Result INT   
   
 SELECT @Result = count(1)
 FROM userquestions   
 WHERE UserTestId = @UserTestID and Correct != 2  
  
 RETURN @Result
  
END  
GO
PRINT 'Finished creating Function UFNReturnAnsweredQuestionCount'
GO 
