SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetFRRemediations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetFRRemediations]
GO

CREATE PROCEDURE [dbo].[uspGetFRRemediations]    
    @CategoryIds VARCHAR(MAX) ,    
    @TopicIds VARCHAR(MAX)   ,
    @ProgramofStudyId int          
AS    
BEGIN 
SET NOCOUNT ON            
/*============================================================================================================          
 --      Purpose: GET FRRemediations based on the programofstudyId 
 --      Modified: 09/25/2013 - NURSING_4679        
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
    DECLARE  @TypeId int
    IF (@ProgramofStudyId = 1)
      SET @TypeId = 18
     ELSE
      SET @TypeId = 21
      select Rem.RID,newid() as scramble from    
         (SELECT DISTINCT CAST(Q.RemediationID as int) AS RID    
   FROM dbo.LookupMappings AS LM    
     INNER JOIN dbo.LookupMappings AS LM17  ON LM17.MappingId = LM.LookupId   
     INNER JOIN Questions Q on  LM.MappedTo = Q.QID  
   WHERE      LM.TypeId = @TypeId    
        AND LM17.MappedTo IN (select value AS SelectedId from dbo.funcListToTableInt(@TopicIds, '|'))    
        AND LM17.LookupId IN (select value AS SelectedId from dbo.funcListToTableInt(@CategoryIds, '|'))) as Rem  
   ORDER BY scramble      
   SET NOCOUNT OFF                    
END 
GO
PRINT 'Finished creating PROCEDURE uspGetFRRemediations'
GO    
  
GO


