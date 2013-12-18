
/****** Object:  StoredProcedure [dbo].[uspGetProgramofStudies]    Script Date: 04/04/2013 16:50:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProgramofStudies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetProgramofStudies]
GO

/****** Object:  StoredProcedure [dbo].[uspGetProgramofStudies]    Script Date: 04/04/2013 16:50:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[uspGetProgramofStudies]
AS
BEGIN            
   SET NOCOUNT ON         
/*============================================================================================================  
//Purpose: Get Program of Studies 
//Modified: 04/25/13  
//Author: Maulik Shah
//LastModifiedBy : Shodhan  
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

select ProgramofStudyId,ProgramofStudyName from ProgramofStudy

 SET NOCOUNT OFF 
END 
GO



