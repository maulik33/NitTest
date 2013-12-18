SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAvailableCFRQuestions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAvailableCFRQuestions]
GO
  
CREATE PROC [dbo].[uspGetAvailableCFRQuestions]      
 @UserId int,    
 @CategoryIds varchar(2000),      
 @TopicIds varchar(max),      
 @ReUseMode int,
 @ProgramofStudyId int      
AS      
BEGIN  
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: Get all available CFRQuestions based on the programofstudyId 
 --      Modified: 09/25/2013 - NURSING_4515(Search by Questions by Topic for PN)  
 --      Modified:10/09/2013 NURSING-4694 (Adding Questions and Remediations to the Customized Focused Reviews)
 --      Author:Liju,Liju  
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
DECLARE @TypeId int
IF (@ProgramofStudyId = 1)
  SET @TypeId = 19
ELSE
  SET @TypeId = 22 
IF (@ReUseMode = 1)      
  BEGIN      
     -- Return all unused Questions  
;With CTE AS(  
    SELECT DISTINCT LM.MappedTo AS QID     
    FROM dbo.LookupMappings AS LM      
    INNER JOIN dbo.LookupMappings AS LM17 ON LM17.MappingId = LM.LookupId      
    WHERE (LM.TypeId = @TypeId)      
      AND LM17.MappedTo IN (select value AS SelectedId from dbo.funcListToTableInt(@TopicIds, '|'))      
      AND LM17.LookupId IN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|'))      
      AND LM.MappedTo NOT IN      
        (  
      SELECT UserQuestions.QID      
      FROM  UserQuestions   
         INNER JOIN UserTests ON UserQuestions.UserTestID = UserTests.UserTestID      
      WHERE UserTests.UserID = @userId     
         AND UserTests.IsCustomizedFRTest = 1   
        ) )
    SELECT QID ,newId() as Scramble FROM CTE ORDER BY Scramble              
  END    
 ELSE IF (@ReUseMode = 2)    
 BEGIN    
    --return all unused and incorrect questions  
;With CTE AS(    
    SELECT DISTINCT LM.MappedTo AS QID    
    FROM dbo.LookupMappings AS LM      
    INNER JOIN dbo.LookupMappings AS LM17 ON LM17.MappingId = LM.LookupId      
    WHERE (LM.TypeId = @TypeId)      
      AND LM17.MappedTo IN (select value AS SelectedId from dbo.funcListToTableInt(@TopicIds, '|'))      
      AND LM17.LookupId IN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|'))      
      AND LM.MappedTo NOT IN      
      (  
       SELECT UQ.QID      
       FROM  UserQuestions UQ    
        INNER JOIN UserTests UT ON UQ.UserTestID = UT.UserTestID      
       WHERE Correct = 1 AND UT.UserID = @userId      
          AND UT.IsCustomizedFRTest = 1    
      )  )
    SELECT QID ,newId() as Scramble FROM CTE ORDER BY Scramble   
   END    
   ELSE IF (@ReUseMode = 3)      
 BEGIN      
  -- return all questions 
 ;With CTE AS(         
     SELECT DISTINCT LM.MappedTo AS QID    
     FROM dbo.LookupMappings AS LM      
     INNER JOIN dbo.LookupMappings AS LM17 ON LM17.MappingId = LM.LookupId      
     WHERE LM.TypeId = @TypeId  -- 19 is for FR Tests    
   AND LM17.MappedTo IN (select value AS SelectedId from dbo.funcListToTableInt(@TopicIds, '|'))      
   AND LM17.LookupId IN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|'))   
     )
    SELECT QID ,newId() as Scramble FROM CTE ORDER BY Scramble      
 END    
 ELSE IF (@ReUseMode = 4)      
   BEGIN    
    -- Get all Incorrect 
;With CTE AS(        
    SELECT DISTINCT LM.MappedTo AS QID     
    FROM dbo.LookupMappings AS LM      
    INNER JOIN dbo.LookupMappings AS LM17 ON LM17.MappingId = LM.LookupId      
    WHERE (LM.TypeId = @TypeId)      
    AND LM17.MappedTo IN (select value AS SelectedId from dbo.funcListToTableInt(@TopicIds, '|'))      
    AND LM17.LookupId IN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|'))      
    AND LM.MappedTo IN      
      (      
       SELECT  UQ.QID      
       FROM  UserQuestions UQ     
       INNER JOIN UserTests UT ON UQ.UserTestID = UT.UserTestID      
       WHERE (Correct = 0 OR Correct = 2)     
          AND UT.UserID = @userId     
          AND   UT.IsCustomizedFRTest = 1    
            
       EXCEPT
       SELECT  UQ.QID      
       FROM  UserQuestions UQ    
       INNER JOIN UserTests UT ON UQ.UserTestID = UT.UserTestID      
       WHERE (Correct = 1)     
         AND UT.UserID = @userId      
         AND UT.IsCustomizedFRTest = 1    
      )    
     )
    SELECT QID ,newId() as Scramble FROM CTE ORDER BY Scramble        
    END    
SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetAvailableCFRQuestions'
GO 


