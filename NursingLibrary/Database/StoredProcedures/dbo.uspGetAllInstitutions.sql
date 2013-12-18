
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllInstitutions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetAllInstitutions]
GO
PRINT 'Creating PROCEDURE uspGetAllInstitutions'
GO
/****** Object:  StoredProcedure [dbo].[uspGetAllInstitutions]    Script Date: 12/20/2011 13:49:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[uspGetAllInstitutions]      
AS      
BEGIN                 
SET NOCOUNT ON 
/*============================================================================================================    
--  Purpose: Nursing-4016 to retrieve all institutions 
--  Modified: 17/06/2013   
--  Author:Karthik  
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
SELECT P.ProgramofStudyName,I.institutionName,I.institutionid 
FROM dbo.NurInstitution I inner join ProgramofStudy P  
ON P.ProgramofStudyId=I.ProgramofStudyId   
end  

GO
PRINT 'Finished creating PROCEDURE uspGetAllInstitutions'
GO 

