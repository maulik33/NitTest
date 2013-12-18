IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudentForTest]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspSearchStudentForTest]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchStudentForTest]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspSearchStudentForTest]
	@InstitutionId INT,
	@CohortId INT,
	@GroupId INT,
	@SearchString VARCHAR(400)
AS
   BEGIN
    SELECT
		 S.UserID,
		 S.FirstName,
		 S.LastName,
		 S.UserName,
		 S.InstitutionID,
		 A.CohortID,
		 A.GroupID,
		 I.InstitutionName,
		 C.CohortName,
		 G.GroupName
    FROM NurStudentInfo S
    LEFT OUTER JOIN NusStudentAssign A  ON S.UserID=A.StudentID
    LEFT JOIN NurInstitution I  ON S.InstitutionID=I.InstitutionID
    LEFT OUTER JOIN NurCohort C ON A.CohortID=C.CohortID
    LEFT OUTER JOIN NurGroup G  ON A.GroupID=G.GroupID
    WHERE UserType='S' AND UserDeleteData IS NULL
        AND (A.DeletedDate IS NULL)
		AND (@InstitutionId = 0 OR I.InstitutionID = @InstitutionId)
		AND (@CohortId= 0 OR C.CohortID = @CohortId )
		AND (@GroupId = 0 OR G.GroupID = @GroupId  )
		AND (LEN(LTRIM(RTRIM(@SearchString))) = 0
		OR (FirstName like +'%'+ @SearchString + '%') OR (LastName like +'%'+ @SearchString +'%')
		OR (UserName like +'%' + @SearchString + '%') OR (UserID like +'%'+ @SearchString +'%')
		OR (InstitutionName like +'%' + @SearchString + '%') OR (CohortName like +'%'+ @SearchString +'%'))
   END
GO
