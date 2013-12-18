
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationByTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetRemediationByTest]
GO
PRINT 'Creating PROCEDURE uspGetRemediationByTest'
GO

CREATE PROCEDURE [dbo].[uspGetRemediationByTest]   
      @userID        INT,                             
      @productID     INT,            
      @institutionID INT,
      @cohortIDs    VARCHAR(MAX)
AS  
  BEGIN  
	 SET NOCOUNT ON
/*============================================================================================================
--  Purpose  : Wired up to use [dbo].[funcListToTableIntForReport] for resolving 8000 character bug issue 
--           : Fix to Track Tests along with Student instead of Cohort [Nursing-4523]
--  Modified : 04/9/2013,10/04/2013
--	Author   :Liju Mathews, Maulik Shah
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

     SELECT nc.CohortName,t.testname,      
             CONVERT(CHAR(8), Dateadd(ss, SUM(CASE ut.productid WHEN 1 THEN      
             uq.timespendforremedation ELSE uq.timespendforexplanation END), 0),      
             108)      
             AS      
             remedation ,  
    t.ProductId     
      FROM   userquestions AS uq      
             JOIN dbo.usertests ut ON ut.usertestid = uq.usertestid      
             INNER JOIN dbo.tests t ON ut.testid = t.testid      
             INNER JOIN dbo.nurcohort nc ON ut.cohortid = nc.cohortid      
             INNER JOIN products p ON t.productid = p.productid      
             INNER JOIN dbo.nurstudentinfo si ON si.userid = ut.userid      
             INNER JOIN dbo.nusstudentassign sa ON si.userid = sa.studentid         
     
      WHERE  nc.institutionid = @InstitutionId      
             AND ( ( @userID <> 0      
                     AND ut.userid = @userID )      
                    OR @userID = 0 )      
             AND ( ( @productID <> 0      
                     AND ut.productid = @productID )      
                    OR @productID = 0 ) 
             GROUP  BY nc.CohortName,t.testname,t.ProductId  
     SET NOCOUNT OFF   
  END   
GO
PRINT 'Finished creating PROCEDURE uspGetRemediationByTest'
GO 
