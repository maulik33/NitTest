SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromInstitutions]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetResultsFromInstitutions]
GO
PRINT 'Creating PROCEDURE uspGetResultsFromInstitutions'
GO
CREATE PROCEDURE [dbo].[uspGetResultsFromInstitutions]
@InstitutionIDs VARCHAR(MAX),
@TestTypeIds VARCHAR(MAX),
@TestIDs VARCHAR(MAX),
@Charttype INT,
@FromDate VARCHAR(10),
@ToDate VARCHAR(10)
AS
BEGIN
SET NOCOUNT ON     
 /*============================================================================================================    
 -- Purpose: Wired up to use [dbo].[funcListToTableIntForReport] for resolving 8000 character bug issue    
 -- modified: 04/9/2013    
 -- Author: Liju    
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
IF(@Charttype = 1)
	BEGIN
		SELECT ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total,
		dbo.UserTests.InsitutionID
		FROM dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		WHERE ((@TestTypeIds='0') OR ProductID IN ( select value from  dbo.funcListToTableIntForReport(@TestTypeIds,'|')))
		AND TestStatus = 1
		AND dbo.UserTests.TestID IN (select value from  dbo.funcListToTableIntForReport(@TestIds,'|'))
		AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableIntForReport(@InstitutionIDs,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.UserTests.TestID, dbo.UserTests.InsitutionID, dbo.UserTests.ProductID
	END
ELSE IF(@Charttype = 2)
	BEGIN
		SELECT  ISNULL(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END),0)  AS N_Correct,
                ISNULL(SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END),0)  AS N_InCorrect,
                ISNULL(SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END),0)  AS N_NAnswered,
                ISNULL(SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END),0)  AS N_CI,
                ISNULL(SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END),0)  AS N_II,
                ISNULL(SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END),0)  AS N_IC
        FROM    dbo.UserQuestions
        INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
        WHERE ((@TestTypeIds='0') OR ProductID IN ( select value from  dbo.funcListToTableIntForReport(@TestTypeIds,'|')))
		AND TestStatus = 1
		AND dbo.UserTests.TestID IN (select value from  dbo.funcListToTableIntForReport(@TestIds,'|'))
		AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableIntForReport(@InstitutionIDs,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.UserTests.TestID,dbo.UserTests.InsitutionID, dbo.UserTests.ProductID
	END
SET NOCOUNT OFF     
END
GO
PRINT 'Finished creating PROCEDURE uspGetResultsFromInstitutions'
GO
