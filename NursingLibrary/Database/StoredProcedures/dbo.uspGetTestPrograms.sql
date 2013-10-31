
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestPrograms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestPrograms]
GO
PRINT 'Creating PROCEDURE uspGetTestPrograms'
GO 

CREATE PROCEDURE [dbo].[uspGetTestPrograms]        
(          
 @TestId INT,
 @Type VARCHAR(10),
 @ProgramofStudyId INT
)        
 AS        
BEGIN 
  
/*============================================================================================================    
//Purpose:	Returns all the programs and "IsTestAssignedToProgram" as 1 for assigned program.
//Modified: Sprint 46: PN RN Unification. Changes done for NURSING-3970 (Bulk Modify Programs)
//			to filter Program List by Program Of Study.
//Created:	03/07/2013, 06/14/2013
//Author:	Shodhan, Atul
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
 WITH AssignedPrograms(ProgramID, ProgramName, Description,IsTestAssignedToProgram)        
 AS      
 (      
  SELECT P.ProgramID,P.ProgramName,p.Description,1      
  FROM	 NurProgram P       
		LEFT JOIN NurProgramProduct PP 
		ON P.ProgramId = PP.ProgramId      
		WHERE PP.ProductID = @TestId AND [Type] =  @Type       
 )     
     
 SELECT P.ProgramID, P.ProgramName,P.[Description], 
		ISNULL(AP.IsTestAssignedToProgram,0) AS IsTestAssignedToProgram, 
		PS.ProgramofStudyName 
 FROM	AssignedPrograms AP 
		RIGHT JOIN NurProgram P 
		ON P.ProgramId = AP.ProgramId
		INNER JOIN ProgramofStudy PS 
		ON P.ProgramOfStudyId = PS.ProgramofStudyId
 WHERE  P.DeletedDate IS NULL
 AND	((@ProgramofStudyId != 0 AND P.ProgramOfStudyId = @ProgramofStudyId) OR @ProgramofStudyId = 0)
     
END 
 

  
GO
PRINT 'Finished creating PROCEDURE uspGetTestPrograms'
GO 



