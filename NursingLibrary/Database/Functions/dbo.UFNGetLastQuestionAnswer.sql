SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNGetLastQuestionAnswer]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
    DROP FUNCTION [dbo].[UFNGetLastQuestionAnswer]
GO

PRINT 'Creating Function UFNGetLastQuestionAnswer'
GO 
CREATE function [dbo].[UFNGetLastQuestionAnswer]    
 (@QID INT, @userTestId INT )   
RETURNS INT   
AS    
BEGIN   
/*============================================================================================================
--      Purpose: To check if the last question was answered or not
--      Created: 12/21/2011
--	    Author:Liju Mathews
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

 SELECT @Result= Count(QID)
  FROM UserQuestions 
  WHERE usertestId =@userTestId and QID =@QID 
  
 RETURN @Result  
    
END
GO
PRINT 'Finished creating Function UFNGetLastQuestionAnswer'
GO





