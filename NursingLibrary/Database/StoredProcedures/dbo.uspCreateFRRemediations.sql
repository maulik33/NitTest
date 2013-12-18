SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateFRRemediations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateFRRemediations]
GO

CREATE PROCEDURE [dbo].[uspCreateFRRemediations]    
    @RemReviewId INT OUTPUT ,    
    @Name NVARCHAR(500) ,    
    @StudentId INT ,    
    @RemediatedTime INT ,    
    @CreateDate DATETIME ,    
    @NoOfRemediations INT ,    
    @SystemIds VARCHAR(MAX) ,    
    @TopicIds VARCHAR(MAX)  ,
    @ProgramofStudyId int    
AS 
BEGIN  
SET NOCOUNT ON            
/*============================================================================================================          
 --  Purpose: Create FRRemediation
 --  Modified: 09/25/2013 - NURSING_4679(Search by Remediations by Topic for PN)       
 --  Author:Liju        
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
   DECLARE @RemCount AS INT ,    
        @count AS INT ,    
        @RId AS INT ,              
        @Id AS INT,    
        @NameCount INT  
    DECLARE @PrevName AS NVARCHAR(500) 
    DECLARE @IsMultiSystem AS BIT 
    SET @IsMultiSystem = 0  
    BEGIN    
        IF ( @RemReviewId = 0 )    
            BEGIN  
                CREATE TABLE #RemediationTbl    
                 (    
                      RemediationId INT IDENTITY(1, 1)    
                                        NOT NULL ,    
                      RID INT,  
                      Scramble Varchar(50)    
                  )   
                SET  ROWCOUNT @NoOfRemediations  
                INSERT  INTO #RemediationTbl   
                    EXEC uspGetFRRemediations @SystemIds,@TopicIds,@ProgramofStudyId
				SET  ROWCOUNT 0  
               
        IF(@Name = 'Multi-Category')  
		BEGIN  
		 SET @IsMultiSystem = 1  
		END   
      
   IF(@IsMultiSystem = 1)  
    BEGIN  
     SELECT @NameCount = count(ID)   
     FROM CustomTest  
     WHERE IsMultiCategory = @IsMultiSystem AND IsRemediation = 1 AND UserId = @StudentId  
    END  
   ELSE  
    BEGIN  
     SELECT @NameCount = count(ID)   
     FROM CustomTest  
     WHERE IsMultiCategory = @IsMultiSystem AND IsRemediation = 1 AND UserId = @StudentId AND Category = @SystemIds   
    END 
		   SET @NameCount = @NameCount + 1  
		   SET @Name = @Name + '.' + CAST(@NameCount as varchar(200))               
		   INSERT INTO CustomTest  
		   (  
			Category,  
			Topic,  
			UserId,  
			IsMultiCategory,  
			IsRemediation   
			)   
			VALUES  
		   (  
			@SystemIds,  
			@TopicIds,  
			@StudentId,  
			@IsMultiSystem,  
			1  
		   )  
            SELECT  @RemCount = COUNT(RemediationId)    
            FROM    #RemediationTbl    
            INSERT  INTO dbo.RemediationReview    
                    ( Name ,    
                      StudentId ,    
                      NoOfRemediations ,    
                      CreatedDate    
                    )    
            VALUES  ( @Name ,    
                      @StudentId ,    
                      @RemCount ,    
                      @CreateDate    
                    )    
    
            SET @RemReviewId = SCOPE_IDENTITY()    
            SET @count = 1    
            WHILE ( @RemCount >= @count )    
                BEGIN    
                    SELECT  @RId = RID    
                    FROM    #RemediationTbl    
                    WHERE   RemediationId = @count    
  
                    INSERT  INTO dbo.RemediationReviewQuestions    
                            ( RemReviewId ,    
                              RemediationId ,     
                              RemediationNumber,  
                              SystemId    
                            )    
                    VALUES  ( @RemReviewId ,    
                              @RId ,           
                              @count ,  
                              0   
                            )    
                    SET @count = @count + 1    
                END    
    
            END    
    ELSE    
            BEGIN    
                UPDATE  RemediationReview    
                SET     RemediatedTime = @RemediatedTime    
                WHERE   RemReviewId = @RemReviewId    
            END    
    END  
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished Creating PROCEDURE uspCreateFRRemediations'
GO 




