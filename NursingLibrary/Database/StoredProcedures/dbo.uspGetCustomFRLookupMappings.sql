


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCustomFRLookupMappings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCustomFRLookupMappings]
GO

PRINT 'Creating PROCEDURE uspGetCustomFRLookupMappings'
GO

CREATE PROCEDURE [dbo].[uspGetCustomFRLookupMappings]      
 @QuestionId int,  
 @ProgramofStudyId int  
AS 
BEGIN
SET NOCOUNT ON            
/*============================================================================================================          
 -- Purpose: -- Get all Remediation & Test Mappings for this Category - Topic Combination for the Question
 -- Modified: 10/09/2013 - NURSING_4694(Adding Questions and Remediations to the Customized Focused Reviews)     
 -- Author:Liju        
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
       
 DECLARE @TypeId1 int,@TypeId2 int, @TypeId3 int, @TypeId4 int  
 If (@ProgramofStudyId = 1)  
 BEGIN  
   SET @TypeId1 =18  
   SET @TypeId2 =19  
   SET @TypeId3 =17  
   SET @TypeId4 =10  
  END  
  ELSE  
   BEGIN  
   SET @TypeId1 =21  
   SET @TypeId2 =22  
   SET @TypeId3 =20  
   SET @TypeId4 =11  
   END  
 SELECT MappingId,LookupId,TypeId,MappedTo
 FROM dbo.LookupMappings      
 WHERE TypeId IN (@TypeId1,@TypeId2) AND LookupId IN (SELECT LookupId 
	 FROM dbo.LookupMappings 
	 WHERE MappedTo = @QuestionId AND TypeId IN (@TypeId1,@TypeId2))      
 SELECT LM.MappingId,      
  LM.LookupId,      
  LM.MappedTo,      
  LC.TypeId AS CategoryTypeId,      
  LC.OriginalId AS CategoryOriginalId,      
  LT.DisplayText AS TopicText      
 FROM dbo.LookupMappings AS LM      
 INNER JOIN dbo.[Lookup] AS LC  -- Lookup Category      
 ON LC.Id = LM.LookupId      
 INNER JOIN dbo.[Lookup] AS LT  -- Lookup Topic      
 ON LT.Id = LM.MappedTo      
 WHERE LM.TypeId = @TypeId3 AND MappingId IN (      
  SELECT LookupId      
  FROM dbo.LookupMappings      
  WHERE TypeId IN (@TypeId1,@TypeId2)      
  AND MappedTo = @QuestionId)  
  
 DECLARE @IsRemediationMapped bit, @IsTestMapped bit      
      
 SET @IsRemediationMapped = 0      
 IF EXISTS(SELECT 1 FROM dbo.TestQuestions AS TQ WHERE TQ.QId = @QuestionId)      
 BEGIN      
  SET @IsRemediationMapped = 1      
 END      
      
 SET @IsTestMapped = 0      
 IF @IsRemediationMapped = 1      
  AND EXISTS(SELECT 1      
   FROM dbo.TestQuestions AS TQ      
   INNER JOIN dbo.Tests AS T      
   ON T.TestId = TQ.TestId      
   INNER JOIN dbo.[Lookup] AS L      
   ON L.OriginalId = T.TestId      
   WHERE TQ.QId = @QuestionId      
   AND L.TypeId = @TypeId4)      
 BEGIN      
  SET @IsTestMapped = 1      
 END      
      
 SELECT @IsRemediationMapped AS [IsRemediationMapped], @IsTestMapped AS [IsTestMapped] 
SET NOCOUNT OFF             
END
GO
PRINT 'Finished creating PROCEDURE uspGetCustomFRLookupMappings'
GO 

