
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohorts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetCohorts]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohorts]    Script Date: 10/10/2011 15:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[uspGetCohorts]    Script Date: 04/22/2011 14:46:33 ******/
CREATE PROCEDURE [dbo].[uspGetCohorts]
(
	@CohortId int,
	@InstitutionIds varchar(max)
)
AS

SET NOCOUNT ON

BEGIN    
	 SELECT C.CohortId,    
	  C.CohortName,    
	  C.CohortDescription,    
	  C.CohortStatus,    
	  C.CohortStartDate,    
	  C.CohortEndDate,    
	  C.InstitutionId,  
	  I.ProgramOfStudyId   
	 FROM NurCohort C INNER JOIN  
		  NurInstitution I ON C.InstitutionID = I.InstitutionID   
	 WHERE    
	 (@CohortId = 0    
	 OR C.CohortID = @CohortId)    
	 AND (@InstitutionIds = ''    
	 OR C.InstitutionId IN    
	  (SELECT value    
	  FROM dbo.funcListToTableInt(@InstitutionIds,'|')))    
END 

SET NOCOUNT OFF
GO

