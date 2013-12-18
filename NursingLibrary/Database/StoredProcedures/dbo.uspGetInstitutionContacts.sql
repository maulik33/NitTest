IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionContacts]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[uspGetInstitutionContacts]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionContacts]    Script Date: 10/10/2011 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetInstitutionContacts]
 @InstitutionId as int ,
 @ContactId as int
 AS
 BEGIN
  select
    ContactId,
    InstitutionId,
    ContactType,
    Name,
    PhoneNumber,
    Email,
    [Status],
    SortOrder
  from dbo.InstitutionContacts
  where (@InstitutionId = 0 or InstitutionId = @InstitutionId)
         and (@ContactId=0 or ContactId = @ContactId)
 END
GO
