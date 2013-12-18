
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UspSaveBulkmodifiedPrograms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UspSaveBulkmodifiedPrograms]
GO
PRINT 'Creating PROCEDURE UspSaveBulkmodifiedPrograms'
GO 

CREATE PROCEDURE [dbo].[UspSaveBulkmodifiedPrograms] 
(
 @ProgramIds VARCHAR(max),  
 @TestId     INT,  
 @Type       INT
)  
AS  
  BEGIN  
	/*============================================================================================================    
	//Purpose: Save all the programs for given test and type. 
	//created: 03/07/2013    
	//Author: Shodhan    
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
      CREATE TABLE #Programs  
        (  
           Id        INT IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,  
           ProgramId INT  
        )  
  
      DECLARE @rowCount  AS INT,  
              @count     AS INT,  
              @ProgramId AS INT  
  
      SET @count = 1  
  
      INSERT INTO #Programs  
      SELECT [value] AS ProgramId  
      FROM   dbo.Funclisttotableint(@ProgramIds, '|')  
  
      SELECT @rowCount = Count(id)  
      FROM   #programs  
  
      DELETE FROM nurprogramproduct  
      WHERE  productid = @TestId  
             AND type = @Type  
             AND programid NOT IN(SELECT programid  
                                  FROM   #programs)  
			 AND programid NOT IN(SELECT programId FROM nurprogram P WHERE P.DeletedDate IS NOT NULL)
  
      WHILE( @rowCount >= @count )  
        BEGIN  
            SELECT @ProgramId = ProgramId  
            FROM   #Programs  
            WHERE  id = @count  
  
            IF NOT EXISTS (SELECT 1  
                           FROM   dbo.nurprogramproduct  
                           WHERE  programid = @ProgramId  
                                  AND type = @Type  
                                  AND productid = @TestId)  
              BEGIN  
                  INSERT INTO nurprogramproduct  
                              (programid,  
                               productid,  
                               type)  
                  VALUES      ( @ProgramId,  
                                @TestId,  
                                @Type )  
              END  
  
            SET @count = @count + 1  
        END  
  
      DROP TABLE #Programs  
  END    

SET NOCOUNT OFF   
GO
PRINT 'Finished creating PROCEDURE UspSaveBulkmodifiedPrograms'
GO 

