SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
IF EXISTS (SELECT * FROM   sys.objects WHERE  object_id = Object_id(N'[dbo].[uspGetGroupsByInstitutionIdsAndCohortIds]')AND TYPE IN ( N'P', N'PC' ))
  DROP PROCEDURE [dbo].[uspGetGroupsByInstitutionIdsAndCohortIds]

GO
PRINT 'Creating PROCEDURE uspGetGroupsByInstitutionIdsAndCohortIds'
GO
CREATE PROCEDURE [dbo].[uspGetGroupsByInstitutionIdsAndCohortIds]
@InstitutionIds VARCHAR(MAX),
@CohortIds      VARCHAR(MAX)
AS
  BEGIN
      SET nocount ON


/*============================================================================================================
--      Purpose: Retrive group by institution and cohort id.  
--      Created: 11/22/2011
--	    Author:Shodhan Kini
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

      SELECT DISTINCT dbo.nurgroup.groupid,
                      dbo.nurgroup.groupname
      FROM   dbo.nurgroup
             INNER JOIN dbo.nurcohort
               ON dbo.nurgroup.cohortid = dbo.nurcohort.cohortid
             INNER JOIN dbo.nurinstitution
               ON dbo.nurcohort.institutionid = dbo.nurinstitution.institutionid
      WHERE  ( dbo.nurcohort.cohortstatus = 1 )
             AND ( ( @CohortIds <> '0'
                     AND dbo.nurgroup.cohortid IN (SELECT VALUE
                                                   FROM
                   dbo.Funclisttotableint(@CohortIds, '|')) )
                    OR @CohortIds = '0' )
             AND ( @InstitutionIds = ''
                    OR dbo.nurcohort.institutionid IN (SELECT VALUE
                                                       FROM
                           dbo.Funclisttotableint(@InstitutionIds, '|')) )
      ORDER  BY groupname ASC

      SET nocount OFF
  END

GO

PRINT 'Finished creating PROCEDURE uspGetGroupsByInstitutionIdsAndCohortIds'
GO 
  