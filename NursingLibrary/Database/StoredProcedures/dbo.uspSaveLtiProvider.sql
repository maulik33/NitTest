IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveLtiProvider]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSaveLtiProvider]
GO
/****** Object:  StoredProcedure [dbo].[uspSaveLtiProvider]    Script Date: 09/13/2013 14:20:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSaveLtiProvider]
@Id int,
@Name varchar(50),
@Title varchar(100),
@Url varchar(100),
@Description varchar(100),
@ConsumerKey varchar(100),
@ConsumerSecret varchar(100),
@CustomParameters varchar(2000),
@Active bit,
@newLtiProviderId int OUTPUT

AS

/*============================================================================================================                
 -- Purpose:  save Lti Provider
 -- Modified By: Glenn 09/13/2013
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

SET NOCOUNT ON

BEGIN
	IF @ID = 0
		BEGIN
			INSERT INTO LTIProviders
			(
			name, title, url, [description], consumerKey, consumerSecret, customParameters, active
			)
			VALUES
			(
			@Name, @Title, @Url, @Description, @ConsumerKey, @ConsumerSecret, @CustomParameters, @active
			)
			
			SELECT @newLtiProviderId = SCOPE_IDENTITY()
		END
ELSE
		BEGIN
			UPDATE LTIProviders
			SET name = @Name,
			title = @Title,
            url = @Url,
			[description] = @Description,
			consumerKey = @COnsumerKey,
			consumerSecret = @ConsumerSecret,
			customParameters = @CustomParameters,
			active = @active,
			lastModifiedOn = getutcdate()
			 WHERE id = @id
			
			SELECT @newLtiProviderId = @id
		END
END
GO


print 'uspSaveLtiProvider created successfully'
GO

