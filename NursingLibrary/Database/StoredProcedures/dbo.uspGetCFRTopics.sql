SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCFRTopics]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCFRTopics]
GO

CREATE PROC [dbo].[uspGetCFRTopics]          
 @CategoryIds varchar(2000),          
 @IsTest bit ,         
 @ProgramofStudyId int=1        
AS 
BEGIN
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: Get all the topics based on the programofstudyId 
 --      Modified: 09/25/2013 - NURSING_4515(Search by Questions by Topic for PN)     
 --      Author:Liju        
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
IF (@ProgramofStudyId = 1)        
BEGIN        
 SELECT L.Id, L.DisplayText          
 FROM dbo.[Lookup] AS L          
 WHERE L.TypeId = 15  
 AND L.Id IN (SELECT LM.MappedTo          
			  FROM dbo.LookupMappings AS LM          
			  INNER JOIN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|')) AS C          
			  ON C.SelectedId = LM.LookupId          
			  INNER JOIN dbo.LookupMappings AS LMQ          
			  ON LMQ.LookupId = LM.MappingId AND ((@IsTest = 1 AND LMQ.TypeId = 19) OR (@IsTest = 0 AND LMQ.TypeId = 18))          
			  WHERE LM.TypeId = 17)          
END        
ELSE        
BEGIN        
 SELECT L.Id, L.DisplayText          
 FROM dbo.[Lookup] AS L          
 WHERE L.TypeId = 15          
 AND L.Id IN (SELECT LM.MappedTo          
  FROM dbo.LookupMappings AS LM          
  INNER JOIN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|')) AS C          
  ON C.SelectedId = LM.LookupId          
  INNER JOIN dbo.LookupMappings AS LMQ          
  ON LMQ.LookupId = LM.MappingId AND ((@IsTest = 1 AND LMQ.TypeId = 22) OR (@IsTest = 0 AND LMQ.TypeId = 21))          
  WHERE LM.TypeId = 20)           
END       
SET NOCOUNT OFF             
END
GO
PRINT 'Finished creating PROCEDURE uspGetCFRTopics'
GO 



