SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAdmins]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchAdmins]
GO
PRINT 'Creating PROCEDURE uspSearchAdmins'
GO
CREATE PROCEDURE [dbo].[uspSearchAdmins]
	@InstitutionIds varchar(max),
	@SecurityLevel Varchar(20),
	@SearchString Varchar(400),
	@ProgramofStudyId int = 1
AS
BEGIN           
SET NOCOUNT ON                  
/*============================================================================================================                
 -- Purpose: To retrieve the Institutions for an admin based on the program of study
 -- Modified Changes done for Nursing-3608 
 -- Modified Changes done for Nursing 4032 (only the institutions which are active has to be retrieved
 -- Modified: Liju 05/21/2013,06/24/2013           
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
	IF @InstitutionIds = ''  
  BEGIN  
    SELECT A.UserId,  
        A.FirstName,  
        A.LastName,  
        A.UserName,  
        A.UserPass ,  
        I.InstitutionName, 
        P.ProgramofStudyName,
        S.AdminType  
    FROM dbo.NurAdmin A  
    LEFT OUTER JOIN dbo.NurAdminInstitution AI  
    ON A.UserID = AI.AdminID  
    LEFT OUTER JOIN dbo.NurInstitution I  
    ON AI.InstitutionID = I.InstitutionID 
    LEFT OUTER JOIN dbo.ProgramofStudy P  
    ON I.ProgramOfStudyId = P.ProgramofStudyId 
    INNER JOIN dbo.NurAdminSecurity S  
    ON A.SecurityLevel = S.SecurityLevel  
    WHERE AdminDeleteData is null  
    AND Active=1 
    AND (@SecurityLevel = '' OR A.SecurityLevel IN (SELECT value FROM dbo.funcListToTableInt(@SecurityLevel,'|')))  
    AND ( @ProgramofStudyId = 0 OR I.ProgramofStudyId = @ProgramofStudyId OR A.UserID not IN (select AdminID from NurAdminInstitution))      
    AND (@SearchString = '' OR( UserName like +'%'+ @SearchString + '%' OR FirstName like + '%'+ @SearchString + '%' OR LastName like + '%'+ @SearchString + '%' ))    
  END  
 ELSE  
  BEGIN  
    SELECT A.UserId,  
        A.FirstName,  
        A.LastName,  
        A.UserName,  
        A.UserPass ,  
        I.InstitutionName, 
        P.ProgramofStudyName,  
        S.AdminType  
    FROM dbo.NurAdmin A  
    LEFT OUTER JOIN dbo.NurAdminInstitution AI  
    ON A.UserID = AI.AdminID  
    LEFT OUTER JOIN dbo.NurInstitution I  
    ON AI.InstitutionID = I.InstitutionID  
    LEFT OUTER JOIN dbo.ProgramofStudy P  
    ON I.ProgramOfStudyId = P.ProgramofStudyId 
    INNER JOIN dbo.NurAdminSecurity S  
    ON A.SecurityLevel = S.SecurityLevel  
    WHERE Active=1       
    AND (@SecurityLevel = '' OR A.SecurityLevel IN  (SELECT value  FROM dbo.funcListToTableInt(@SecurityLevel,'|')))  
    AND (@InstitutionIds = '' OR AI.InstitutionID IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))  
    AND (@ProgramofStudyId = 0 OR I.ProgramofStudyId = @ProgramofStudyId)    
    AND (@SearchString = '' OR( UserName like +'%'+ @SearchString + '%' OR FirstName like + '%'+ @SearchString + '%' OR LastName like + '%'+ @SearchString + '%' ))  
  END   
SET NOCOUNT OFF                      
END 
GO
PRINT 'Finished creating PROCEDURE uspSearchAdmins'
GO 
