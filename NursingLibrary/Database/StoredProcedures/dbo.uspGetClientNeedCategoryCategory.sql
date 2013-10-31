IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedCategoryCategory]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetClientNeedCategoryCategory]
GO

/****** Object:  StoredProcedure [dbo].[uspGetClientNeedCategoryCategory]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetClientNeedCategoryCategory]

AS
/*============================================================================================================  
--	Modification Purpose:	Sprint 43: PN RN Unification Fix for NURSING-3758.
--							Returning ProgramofStudyId based on associated parent category.
--	Modified:				05/14/2013
--	Last Modified By:		Atul
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
SELECT ClientNeedCategoryID AS [Id]
	, ClientNeedCategory AS [Description]
	, ClientNeedID AS [ParentId]
	, C.ProgramofStudyId AS [ProgramofStudyId]
FROM ClientNeedCategory
INNER JOIN ClientNeeds C
ON ClientNeedID = C.ClientNeedsID
GO
