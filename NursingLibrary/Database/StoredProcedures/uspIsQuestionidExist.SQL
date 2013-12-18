
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspIsQuestionidExist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspIsQuestionidExist]
GO
PRINT 'Creating PROCEDURE uspIsQuestionidExist'
GO

CREATE PROCEDURE [dbo].[uspIsQuestionidExist] 
@QuestionId VARCHAR(50),
@IsExit     BIT OUT
AS
/*============================================================================================================      
//Purpose: Check question id exist or not   
           when time expires for an IT Test       
//Created: Feb 3 2012      
//Author:Shodhan      
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
  SET nocount ON

  BEGIN
      DECLARE @QIDCount INT

      SET @QIDCount = 0

      SELECT @QIDCount = COUNT(questionid)
      FROM   questions
      WHERE  questionid = @QuestionId

      PRINT @QIDCount

      IF @QIDCount > 0
        BEGIN
            SET @IsExit = 1
        END
      ELSE
        BEGIN
            SET @IsExit = 0
        END

      PRINT @IsExit

      RETURN @IsExit
  END

  SET nocount OFF  
  GO
PRINT 'Finished creating PROCEDURE uspIsQuestionidExist'
GO