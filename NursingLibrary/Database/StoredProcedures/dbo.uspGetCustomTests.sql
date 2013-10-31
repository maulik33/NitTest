
/****** Object:  StoredProcedure [dbo].[uspGetCustomTests]    Script Date: 04/04/2013 17:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCustomTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCustomTests]
GO
PRINT 'Creating PROCEDURE uspGetCustomTests'
GO
CREATE PROCEDURE [dbo].[uspGetCustomTests]  
(  
 @TestId int,  
 @ProductId int,  
 @TestName varchar(50)  
) 
AS  
 BEGIN            
   SET NOCOUNT ON         
/*============================================================================================================  
//Purpose: To include the newly added column ''SecondPerQuestion'' 
//       : Returning ProgramofStudyId to display Test association with RN/PN [04/04/13]
//Modified: June 06/12,04/04/13 
//Author: Liju  
//LastModifiedBY:Shodhan
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
 
   SELECT T.TestID ,T.TestName AS [Name],T.ProductId , T.DefaultGroup , T.SecondPerQuestion,P.ProgramofStudyName,T.ProgramofStudyId    
   FROM  Tests T 
    JOIN ProgramofStudy P ON P.ProgramofStudyId = T.ProgramofStudyId
   WHERE (T.TestID =@TestId OR @TestId=0) AND (T.ProductID= @ProductId OR @ProductId =0)  
   AND (T.TestName=@TestName OR @TestName='') AND (T.ActiveTest=1)  
 SET NOCOUNT OFF 
END 
GO
PRINT 'Finished creating PROCEDURE uspGetCustomTests'
GO



