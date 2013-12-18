--Table Changes
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[CountryID] [smallint] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED
(
	[CountryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CountryState]    Script Date: 07/27/2011 15:04:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CountryState](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[CountryID] [smallint] NOT NULL,
	[StateName] [varchar](100) NOT NULL,
	[StateStatus] [int] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED
(
	[StateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[InstitutionContacts] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstitutionContacts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InstitutionContacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[InstitutionID] [int] NOT NULL,
	[ContactType] [smallint] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [smalldatetime] NULL,
	[SortOrder] [smallint] NULL,
 CONSTRAINT [PK_InstitutionContacts] PRIMARY KEY CLUSTERED
(
	[ContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'InstitutionContacts' AND COLUMN_NAME = 'PhoneNumber')
BEGIN
   ALTER TABLE dbo.InstitutionContacts
   ALTER COLUMN [PhoneNumber] [varchar](50) NOT NULL
END

GO


/****** Object:  Table [dbo].[StudentAdhocGroup]    Script Date: 08/19/2011 19:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentAdhocGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentAdhocGroup](
	[AdhocGroupID] [int] NOT NULL,
	[StudentID] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Address]    Script Date: 08/23/2011  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[Address3] [varchar](50) NULL,
	[CountryID] [smallint] NULL,
	[StateName] [varchar](100) NULL,
	[Zip] [varchar](100) NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED
(
	[AddressID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
End
GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[AdhocGroupTestDetail]    Script Date: 08/19/2011 19:52:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdhocGroupTestDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdhocGroupTestDetail](
	[AdhocGroupTestDetailID] [int] IDENTITY(1,1) NOT NULL,
	[TestID] [int] NOT NULL,
	[AdhocGroupID] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_AdhocGroupTestDetail] PRIMARY KEY CLUSTERED
(
	[AdhocGroupTestDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[AdhocGroup]    Script Date: 08/19/2011 19:51:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdhocGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdhocGroup](
	[AdhocGroupID] [int] IDENTITY(1,1) NOT NULL,
	[AdhocGroupName] [varchar](50) NULL,
	[IsAdaGroup] [bit] NULL,
	[ADA] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](80) NULL,
 CONSTRAINT [PK_AdhocGroup] PRIMARY KEY CLUSTERED
(
	[AdhocGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_State_Country]    Script Date: 07/27/2011 15:04:48 ******/


IF NOT EXISTS(SELECT * FROM SYS.FOREIGN_KEYS
	WHERE object_id = OBJECT_ID('dbo.FK_State_Country')
	AND parent_object_id = OBJECT_ID('dbo.CountryState'))
BEGIN
	ALTER TABLE [dbo].[CountryState]  WITH NOCHECK ADD  CONSTRAINT [FK_State_Country] FOREIGN KEY([CountryID])
	REFERENCES [dbo].[Country] ([CountryID])
	ALTER TABLE [dbo].[CountryState] CHECK CONSTRAINT [FK_State_Country]
	
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurInstitution' AND COLUMN_NAME = 'AddressId')
BEGIN
   ALTER TABLE NurInstitution
   ADD AddressId int
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurStudentInfo' AND COLUMN_NAME = 'AddressID')
BEGIN
   ALTER TABLE NurStudentInfo
   ADD AddressID int
END

GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurProductDatesStudent' AND COLUMN_NAME = 'IsAdhocGroup')
BEGIN
   ALTER TABLE NurProductDatesStudent
   DROP COLUMN IsAdhocGroup
END

GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurProductDatesStudent' AND COLUMN_NAME = 'AdhocGroupId')
BEGIN
   ALTER TABLE NurProductDatesStudent
   DROP COLUMN AdhocGroupId
END

GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurStudentInfo' AND COLUMN_NAME = 'EmergencyPhone')
BEGIN
   ALTER TABLE NurStudentInfo
   ADD EmergencyPhone Varchar(50)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurStudentInfo' AND COLUMN_NAME = 'ContactPerson')
BEGIN
   ALTER TABLE NurStudentInfo
   ADD ContactPerson Varchar(80)
END

GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserQuestionsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserQuestionsHistory](
	[ID] [int] NULL,
	[UserTestID] [int] NULL,
	[QID] [int] NULL,
	[QuestionNumber] [int] NULL,
	[Correct] [int] NULL,
	[TimeSpendForQuestion] [int] NULL,
	[TimeSpendForRemedation] [int] NULL,
	[TimeSpendForExplanation] [int] NULL,
	[AnswerTrack] [varchar](50) NULL,
	[IncorrecCorrect] [char](2) NULL,
	[FirstChoice] [char](1) NULL,
	[SecondChoice] [char](1) NULL,
	[AnswerChanges] [char](2) NULL,
	[OrderedIndexes] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTestsHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserTestsHistory](
	[UserTestID] [int] NULL,
	[UserID] [int] NULL,
	[TestID] [int] NULL,
	[TestNumber] [int] NULL,
	[InsitutionID] [int] NULL,
	[CohortID] [int] NULL,
	[ProgramID] [int] NULL,
	[TestStarted] [datetime] NULL,
	[TestComplited] [datetime] NULL,
	[TestStarted_R] [datetime] NULL,
	[TestComplited_R] [datetime] NULL,
	[TestStatus] [int] NULL,
	[TimedTest] [int] NULL,
	[TutorMode] [int] NULL,
	[ReusedMode] [int] NULL,
	[NumberOfQuestions] [int] NULL,
	[QuizOrQBank] [char](1) NULL,
	[TimeRemaining] [varchar](50) NULL,
	[SuspendQuestionNumber] [int] NULL,
	[SuspendQID] [int] NULL,
	[SuspendType] [char](2) NULL,
	[ProductID] [int] NULL,
	[TestName] [varchar](500) NULL,
	[Override] [bit] NULL CONSTRAINT [DF__UserTests__Overr__4171D534]  DEFAULT ((0)),
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL
) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HelpfulDocuments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HelpfulDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](1000) NULL,
	[GUID] [nvarchar](100) NULL,
	[FileName] [nvarchar](100) NULL,
	[Type] [nvarchar](100) NULL,
	[Size] [float] NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy][int] NULL,
 CONSTRAINT [PK_HelpfulDocuments] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactType](
	[ContactLevel] [smallint] NULL,
	[ContactType] [varchar](25) NULL,
	[Status] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemediationReview]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RemediationReview](
	[RemReviewId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[StudentId] [int] NULL,
	[NoOfRemediations] [int] NULL,
	[Status] [bit] NULL,
	[RemediatedTime] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_RemediationReview] PRIMARY KEY CLUSTERED
(
	[RemReviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemediationReviewQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RemediationReviewQuestions](
	[RemReviewQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[RemReviewId] [int] NOT NULL,
	[RemediationId] [int] NOT NULL,
	[RemediationNumber] [int] NULL,
	[SystemId] [int] NOT NULL,
	[RemediatedTime] [int] NULL,
 CONSTRAINT [PK_RemediationReviewQuestions] PRIMARY KEY CLUSTERED
(
	[RemReviewQuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurAdmin' AND COLUMN_NAME = 'UploadAccess')
BEGIN
   ALTER TABLE NurAdmin
   ADD UploadAccess bit
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Country' AND COLUMN_NAME = 'CountryCode')
BEGIN
   ALTER TABLE Country
   ADD CountryCode varchar(10)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'CountryState' AND COLUMN_NAME = 'StateCode')
BEGIN
   ALTER TABLE CountryState
   ADD StateCode varchar(10)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurInstitution' AND COLUMN_NAME = 'Annotation')
BEGIN
   ALTER TABLE NurInstitution
   ADD Annotation varchar(1000)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurInstitution' AND COLUMN_NAME = 'Email')
BEGIN
   ALTER TABLE NurInstitution
   ADD Email nvarchar(100)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'NurInstitution' AND COLUMN_NAME = 'ContractualStartDate')
BEGIN
   ALTER TABLE NurInstitution
   ADD ContractualStartDate smalldatetime
END

GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Questions' AND COLUMN_NAME = 'AlternateStem')
BEGIN
   ALTER TABLE Questions
   ADD AlternateStem varchar(5000)
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'AnswerChoices' AND COLUMN_NAME = 'AlternateAText')
BEGIN
   ALTER TABLE AnswerChoices
   ADD AlternateAText varchar(3000)
END

GO



-- Data Changes

IF NOT EXISTS (SELECT 1 FROM [ContactType] WHERE ContactLevel =1 )
BEGIN
INSERT [dbo].[ContactType] ([ContactLevel], [ContactType], [Status]) VALUES (1, N'Administrative', 1)
INSERT [dbo].[ContactType] ([ContactLevel], [ContactType], [Status]) VALUES (2, N'Academic', 1)
INSERT [dbo].[ContactType] ([ContactLevel], [ContactType], [Status]) VALUES (3, N'Finance', 1)
INSERT [dbo].[ContactType] ([ContactLevel], [ContactType], [Status]) VALUES (4, N'Technical', 1)
END
GO

--/****** Object:  Table [dbo].[Country]    Script Date: 07/27/2011 15:21:36 ******/
IF NOT EXISTS(SELECT 1 FROM [dbo].[Country] WHERE CountryID = 235)
BEGIN
SET IDENTITY_INSERT [dbo].[Country] ON
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (1, N'Afghanistan', 1, N'AF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (2, N'Aland Islands', 1, N'AX')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (3, N'Albania', 1, N'AL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (4, N'Algeria', 1, N'DZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (5, N'American Samoa', 1, N'AS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (6, N'Andorra', 1, N'AD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (7, N'Angola', 1, N'AO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (8, N'Anguilla', 1, N'AI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (9, N'Antarctica', 1, N'AQ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (10, N'Antigua And Barbuda', 1, N'AG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (11, N'Argentina', 1, N'AR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (12, N'Armenia', 1, N'AM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (13, N'Aruba', 1, N'AW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (14, N'Australia', 1, N'AU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (15, N'Austria', 1, N'AT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (16, N'Azerbaijan', 1, N'AZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (17, N'Bahamas', 1, N'BS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (18, N'Bahrain', 1, N'BH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (19, N'Bangladesh', 1, N'BD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (20, N'Barbados', 1, N'BB')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (21, N'Belarus', 1, N'BY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (22, N'Belgium', 1, N'BE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (23, N'Belize', 1, N'BZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (24, N'Benin', 1, N'BJ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (25, N'Bermuda', 1, N'BM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (26, N'Bhutan', 1, N'BT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (27, N'Bolivia Plurinational State Of', 1, N'BO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (28, N'Bonaire Sint Eustatius And Saba', 1, N'BQ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (29, N'Bosnia And Herzegovina', 1, N'BA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (30, N'Botswana', 1, N'BW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (31, N'Bouvet Island', 1, N'BV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (32, N'Brazil', 1, N'BR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (33, N'British Indian Ocean Territory', 1, N'IO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (34, N'Brunei Darussalam', 1, N'BN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (35, N'Bulgaria', 1, N'BG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (36, N'Burkina Faso', 1, N'BF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (37, N'Burundi', 1, N'BI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (38, N'Cambodia', 1, N'KH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (39, N'Cameroon', 1, N'CM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (40, N'Canada', 1, N'CA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (41, N'Cape Verde', 1, N'CV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (42, N'Cayman Islands', 1, N'KY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (43, N'Central African Republic', 1, N'CF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (44, N'Chad', 1, N'TD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (45, N'Chile', 1, N'CL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (46, N'China', 1, N'CN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (47, N'Christmas Island', 1, N'CX')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (48, N'Cocos (Keeling) Islands', 1, N'CC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (49, N'Colombia', 1, N'CO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (50, N'Comoros', 1, N'KM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (51, N'Congo', 1, N'CG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (52, N'Congo The Democratic Republic Of The', 1, N'CD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (53, N'Cook Islands', 1, N'CK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (54, N'Costa Rica', 1, N'CR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (55, N'Cote D''Ivoire', 1, N'CI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (56, N'Croatia', 1, N'HR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (57, N'Cuba', 1, N'CU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (58, N'Cura?ao', 1, N'CW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (59, N'Cyprus', 1, N'CY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (60, N'Czech Republic', 1, N'CZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (61, N'Denmark', 1, N'DK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (62, N'Djibouti', 1, N'DJ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (63, N'Dominica', 1, N'DM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (64, N'Dominican Republic', 1, N'DO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (65, N'Ecuador', 1, N'EC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (66, N'Egypt', 1, N'EG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (67, N'El Salvador', 1, N'SV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (68, N'Equatorial Guinea', 1, N'GQ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (69, N'Eritrea', 1, N'ER')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (70, N'Estonia', 1, N'EE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (71, N'Ethiopia', 1, N'ET')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (72, N'Falkland Islands (Malvinas)', 1, N'FK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (73, N'Faroe Islands', 1, N'FO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (74, N'Fiji', 1, N'FJ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (75, N'Finland', 1, N'FI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (76, N'France', 1, N'FR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (77, N'French Guiana', 1, N'GF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (78, N'French Polynesia', 1, N'PF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (79, N'French Southern Territories', 1, N'TF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (80, N'Gabon', 1, N'GA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (81, N'Gambia', 1, N'GM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (82, N'Georgia', 1, N'GE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (83, N'Germany', 1, N'DE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (84, N'Ghana', 1, N'GH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (85, N'Gibraltar', 1, N'GI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (86, N'Greece', 1, N'GR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (87, N'Greenland', 1, N'GL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (88, N'Grenada', 1, N'GD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (89, N'Guadeloupe', 1, N'GP')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (90, N'Guam', 1, N'GU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (91, N'Guatemala', 1, N'GT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (92, N'Guernsey', 1, N'GG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (93, N'Guinea', 1, N'GN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (94, N'Guinea-Bissau', 1, N'GW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (95, N'Guyana', 1, N'GY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (96, N'Haiti', 1, N'HT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (97, N'Heard Island And Mcdonald Islands', 1, N'HM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (98, N'Holy See (Vatican City State)', 1, N'VA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (99, N'Honduras', 1, N'HN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (100, N'Hong Kong', 1, N'HK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (101, N'Hungary', 1, N'HU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (102, N'Iceland', 1, N'IS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (103, N'India', 1, N'IN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (104, N'Indonesia', 1, N'ID')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (105, N'Iran Islamic Republic Of', 1, N'IR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (106, N'Iraq', 1, N'IQ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (107, N'Ireland', 1, N'IE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (108, N'Isle Of Man', 1, N'IM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (109, N'Israel', 1, N'IL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (110, N'Italy', 1, N'IT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (111, N'Jamaica', 1, N'JM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (112, N'Japan', 1, N'JP')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (113, N'Jersey', 1, N'JE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (114, N'Jordan', 1, N'JO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (115, N'Kazakhstan', 1, N'KZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (116, N'Kenya', 1, N'KE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (117, N'Kiribati', 1, N'KI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (118, N'Korea Democratic People''S Republic Of', 1, N'KP')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (119, N'Korea Republic Of', 1, N'KR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (120, N'Kuwait', 1, N'KW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (121, N'Kyrgyzstan', 1, N'KG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (122, N'Lao People''S Democratic Republic', 1, N'LA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (123, N'Latvia', 1, N'LV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (124, N'Lebanon', 1, N'LB')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (125, N'Lesotho', 1, N'LS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (126, N'Liberia', 1, N'LR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (127, N'Libyan Arab Jamahiriya', 1, N'LY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (128, N'Liechtenstein', 1, N'LI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (129, N'Lithuania', 1, N'LT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (130, N'Luxembourg', 1, N'LU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (131, N'Macao', 1, N'MO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (132, N'Macedonia The Former Yugoslav Republic Of', 1, N'MK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (133, N'Madagascar', 1, N'MG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (134, N'Malawi', 1, N'MW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (135, N'Malaysia', 1, N'MY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (136, N'Maldives', 1, N'MV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (137, N'Mali', 1, N'ML')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (138, N'Malta', 1, N'MT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (139, N'Marshall Islands', 1, N'MH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (140, N'Martinique', 1, N'MQ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (141, N'Mauritania', 1, N'MR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (142, N'Mauritius', 1, N'MU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (143, N'Mayotte', 1, N'YT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (144, N'Mexico', 1, N'MX')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (145, N'Micronesia Federated States Of', 1, N'FM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (146, N'Moldova Republic Of', 1, N'MD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (147, N'Monaco', 1, N'MC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (148, N'Mongolia', 1, N'MN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (149, N'Montenegro', 1, N'ME')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (150, N'Montserrat', 1, N'MS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (151, N'Morocco', 1, N'MA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (152, N'Mozambique', 1, N'MZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (153, N'Myanmar', 1, N'MM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (154, N'Namibia', 1, N'NA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (155, N'Nauru', 1, N'NR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (156, N'Nepal', 1, N'NP')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (157, N'Netherlands', 1, N'NL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (158, N'New Caledonia', 1, N'NC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (159, N'New Zealand', 1, N'NZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (160, N'Nicaragua', 1, N'NI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (161, N'Niger', 1, N'NE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (162, N'Nigeria', 1, N'NG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (163, N'Niue', 1, N'NU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (164, N'Norfolk Island', 1, N'NF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (165, N'Northern Mariana Islands', 1, N'MP')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (166, N'Norway', 1, N'NO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (167, N'Oman', 1, N'OM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (168, N'Pakistan', 1, N'PK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (169, N'Palau', 1, N'PW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (170, N'Palestinian Territory Occupied', 1, N'PS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (171, N'Panama', 1, N'PA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (172, N'Papua New Guinea', 1, N'PG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (173, N'Paraguay', 1, N'PY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (174, N'Peru', 1, N'PE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (175, N'Philippines', 1, N'PH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (176, N'Pitcairn', 1, N'PN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (177, N'Poland', 1, N'PL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (178, N'Portugal', 1, N'PT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (179, N'Puerto Rico', 1, N'PR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (180, N'Qatar', 1, N'QA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (181, N'Reunion', 1, N'RE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (182, N'Romania', 1, N'RO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (183, N'Russian Federation', 1, N'RU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (184, N'Rwanda', 1, N'RW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (185, N'Saint Barthelemy', 1, N'BL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (186, N'Saint Helena Ascension And Tristan Da Cunha', 1, N'SH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (187, N'Saint Kitts And Nevis', 1, N'KN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (188, N'Saint Lucia', 1, N'LC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (189, N'Saint Martin (French Part)', 1, N'MF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (190, N'Saint Pierre And Miquelon', 1, N'PM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (191, N'Saint Vincent And The Grenadines', 1, N'VC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (192, N'Samoa', 1, N'WS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (193, N'San Marino', 1, N'SM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (194, N'Sao Tome And Principe', 1, N'ST')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (195, N'Saudi Arabia', 1, N'SA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (196, N'Senegal', 1, N'SN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (197, N'Serbia', 1, N'RS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (198, N'Seychelles', 1, N'SC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (199, N'Sierra Leone', 1, N'SL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (200, N'Singapore', 1, N'SG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (201, N'Sint Maarten (Dutch Part)', 1, N'SX')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (202, N'Slovakia', 1, N'SK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (203, N'Slovenia', 1, N'SI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (204, N'Solomon Islands', 1, N'SB')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (205, N'Somalia', 1, N'SO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (206, N'South Africa', 1, N'ZA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (207, N'South Georgia And The South Sandwich Islands', 1, N'GS')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (208, N'Spain', 1, N'ES')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (209, N'Sri Lanka', 1, N'LK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (210, N'Sudan', 1, N'SD')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (211, N'Suriname', 1, N'SR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (212, N'Svalbard And Jan Mayen', 1, N'SJ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (213, N'Swaziland', 1, N'SZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (214, N'Sweden', 1, N'SE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (215, N'Switzerland', 1, N'CH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (216, N'Syrian Arab Republic', 1, N'SY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (217, N'Taiwan Province Of China', 1, N'TW')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (218, N'Tajikistan', 1, N'TJ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (219, N'Tanzania United Republic Of', 1, N'TZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (220, N'Thailand', 1, N'TH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (221, N'Timor-Leste', 1, N'TL')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (222, N'Togo', 1, N'TG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (223, N'Tokelau', 1, N'TK')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (224, N'Tonga', 1, N'TO')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (225, N'Trinidad And Tobago', 1, N'TT')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (226, N'Tunisia', 1, N'TN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (227, N'Turkey', 1, N'TR')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (228, N'Turkmenistan', 1, N'TM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (229, N'Turks And Caicos Islands', 1, N'TC')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (230, N'Tuvalu', 1, N'TV')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (231, N'Uganda', 1, N'UG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (232, N'Ukraine', 1, N'UA')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (233, N'United Arab Emirates', 1, N'AE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (234, N'United Kingdom', 1, N'GB')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (235, N'United States', 1, N'US')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (236, N'United States Minor Outlying Islands', 1, N'UM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (237, N'Uruguay', 1, N'UY')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (238, N'Uzbekistan', 1, N'UZ')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (239, N'Vanuatu', 1, N'VU')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (240, N'Venezuela Bolivarian Republic Of', 1, N'VE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (241, N'Viet Nam', 1, N'VN')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (242, N'Virgin Islands British', 1, N'VG')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (243, N'Virgin Islands U.S.', 1, N'VI')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (244, N'Wallis And Futuna', 1, N'WF')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (245, N'Western Sahara', 1, N'EH')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (246, N'Yemen', 1, N'YE')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (247, N'Zambia', 1, N'ZM')
INSERT [dbo].[Country] ([CountryID], [CountryName], [Status], [CountryCode]) VALUES (248, N'Zimbabwe', 1, N'ZW')
SET IDENTITY_INSERT [dbo].[Country] OFF
END
GO
--/****** Object:  Table [dbo].[CountryState]    Script Date: 07/27/2011 15:21:36 ******/
-- INSERT STATEMENT FOR UNITED STATES
IF NOT EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE CountryID = 235)
BEGIN
SET IDENTITY_INSERT [dbo].[CountryState] ON
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (1, 235, N'Alabama', 1, N'AL')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (2, 235, N'Alaska', 1, N'AK')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (4, 235, N'Arizona', 1, N'AZ')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (5, 235, N'Arkansas', 1, N'AR')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (6, 235, N'California', 1, N'CA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (7, 235, N'Colorado', 1, N'CO')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (8, 235, N'Connecticut', 1, N'CT')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (9, 235, N'Delaware', 1, N'DE')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (10, 235, N'District of Columbia', 1, N'DC')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (12, 235, N'Florida', 1, N'FL')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (13, 235, N'Georgia', 1, N'GA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (15, 235, N'Hawaii', 1, N'HI')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (16, 235, N'Idaho', 1, N'ID')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (17, 235, N'Illinois', 1, N'IL')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (18, 235, N'Indiana', 1, N'IN')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (19, 235, N'Iowa', 1, N'IA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (20, 235, N'Kansas', 1, N'KS')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (21, 235, N'Kentucky', 1, N'KY')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (22, 235, N'Louisiana', 1, N'LA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (23, 235, N'Maine', 1, N'ME')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (25, 235, N'Maryland', 1, N'MD')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (26, 235, N'Massachusetts', 1, N'MA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (27, 235, N'Michigan', 1, N'MI')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (28, 235, N'Minnesota', 1, N'MN')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (29, 235, N'Mississippi', 1, N'MS')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (30, 235, N'Missouri', 1, N'MO')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (31, 235, N'Montana', 1, N'MT')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (32, 235, N'Nebraska', 1, N'NE')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (33, 235, N'Nevada', 1, N'NV')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (34, 235, N'New Hampshire', 1, N'NH')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (35, 235, N'New Jersey', 1, N'NJ')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (36, 235, N'New Mexico', 1, N'NM')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (37, 235, N'New York', 1, N'NY')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (38, 235, N'North Carolina', 1, N'NC')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (39, 235, N'North Dakota', 1, N'ND')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (41, 235, N'Ohio', 1, N'OH')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (42, 235, N'Oklahoma', 1, N'OK')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (43, 235, N'Oregon', 1, N'OR')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (45, 235, N'Pennsylvania', 1, N'PA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (47, 235, N'Rhode Island', 1, N'RI')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (48, 235, N'South Carolina', 1, N'SC')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (49, 235, N'South Dakota', 1, N'SD')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (50, 235, N'Tennessee', 1, N'TN')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (51, 235, N'Texas', 1, N'TX')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (52, 235, N'Utah', 1, N'UT')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (53, 235, N'Vermont', 1, N'VT')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (55, 235, N'Virginia', 1, N'VA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (56, 235, N'Washington', 1, N'WA')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (57, 235, N'West Virginia', 1, N'WV')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (58, 235, N'Wisconsin', 1, N'WI')
INSERT [dbo].[CountryState] ([StateID], [CountryID], [StateName], [StateStatus], [StateCode]) VALUES (59, 235, N'Wyoming', 1, N'WY')
SET IDENTITY_INSERT [dbo].[CountryState] OFF
END

GO

IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 3)
BEGIN
Delete from CountryState
	where StateID = 3
END
GO

IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 24)
BEGIN
Delete from CountryState
	where StateID = 24
END
GO

IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 40)
BEGIN
Delete from CountryState
	where StateID = 40
END
GO


IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 44)
BEGIN
Delete from CountryState
	where StateID = 44
END
GO


IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 46)
BEGIN
Delete from CountryState
	where StateID = 46
END
GO

IF EXISTS(SELECT 1 FROM [dbo].[CountryState] WHERE StateID = 54)
BEGIN
Delete from CountryState
	where StateID = 54
END
GO



UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with fluid volume deficit.  Which laboratory finding does the nurse expect to see?</P>' WHERE QID = 35
UPDATE Questions SET AlternateStem = '<P>A nurse measures the central venous pressure.  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 36
UPDATE Questions SET AlternateStem = '<P>Which of the following signs and symptoms is an early indication of fluid volume excess?</P>' WHERE QID = 37
UPDATE Questions SET AlternateStem = '<P>Which of the following readings of the central venous pressure (CVP) indicates fluid overload?</P>' WHERE QID = 38
UPDATE Questions SET AlternateStem = '<P>An older man is admitted to the hospital for persistent vomiting and abdominal pain.  A nurse cares for the patient.  The nurse inserts a nasogastric tube and connects it to suction.  An intravenous infusion of 1,000 ml of D5 with 20 mEq of potassium chloride is started, to infuse at 100 ml per hour.  What does the nurse understand is the reason for adding potassium chloride to the infusion?</P>' WHERE QID = 40
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with a fractured right hip.  A nurse cares for the patient. The patient''s lab values are: Hgb 15, Hct 46%, sodium 140 mEq/L, potassium 5.6 mEq/L, and chloride 100 mEq/L. What observation is the nurse <B>MOST</B> concerned about?</P>' WHERE QID = 41
UPDATE Questions SET AlternateStem = '<P>An adult male patient has a history of diabetes insipidus. A nurse cares for the patient.  The nurse identifies an imbalance that is <B>MOST</B> likely to develop if diabetes insipidus recurs.   Which imbalance does the nurse identify?</P>' WHERE QID = 42
UPDATE Questions SET AlternateStem = '<P>Muscle cramps, weakness, and bradycardia are signs of an electrolyte imbalance.  Which electrolyte imbalance are they signs of?</P>' WHERE QID = 43
UPDATE Questions SET AlternateStem = '<P>Nasogastric drainage, vomiting, diarrhea, and the use of diuretics can cause an electrolyte imbalance.  Which electrolyte imbalance can they cause?</P>' WHERE QID = 44
UPDATE Questions SET AlternateStem = '<P>A patient is receiving a blood transfusion.  A nurse cares for the patient.  Which observation is the nurse <B>MOST</B> concerned about?</P>' WHERE QID = 46
UPDATE Questions SET AlternateStem = '<P>A patient receives a transfusion of one unit of packed red blood cells.  The nurse prepares to give the patient another unit of packed red blood cells. Which action is <B>MOST</B> appropriate for the nurse to do <B>FIRST</B>?</P>' WHERE QID = 47
UPDATE Questions SET AlternateStem = '<P>A nurse inserts a central venous pressure line in a patient. Following the insertion, the patient complains of dyspnea, shortness of breath, and chest pain. What does the nurse understand is <B>MOST</B> likely the cause of these symptoms?</P>' WHERE QID = 48
UPDATE Questions SET AlternateStem = '<P>A student nurse begins an IV on an elderly patient.  A nurse observes the student nurse.  The nurse intervenes because of something the student nurse does.  What does the student nurse do?</P>' WHERE QID = 49
UPDATE Questions SET AlternateStem = '<P>The nurse notices that an intravenous infusion is not running. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 50
UPDATE Questions SET AlternateStem = '<P>A patient is receiving a blood transfusion. A nurse cares for the patient.  What does the nurse observe if fluid overload occurs during the transfusion?</P>' WHERE QID = 51
UPDATE Questions SET AlternateStem = 'A nurse administers 1,000 ml of D5W, 40 mEq of KCl at 100 ml/hour to a patient.  The nurse uses an administration set that delivers 60 drops/ml.  How many drops/minute does the nurse adjust the flow rate to deliver?' WHERE QID = 52
UPDATE Questions SET AlternateStem = '<P>A patient is receiving a blood transfusion.  A nurse monitors the patient. The nurse intervenes because of something the nurse observes.  What does the nurse observe?</P>' WHERE QID = 53
UPDATE Questions SET AlternateStem = '<P>A nurse delivers 3,000 ml of D5W in 24 hours to a patient.  The nurse uses an administration set that delivers 15 drops/ml.  How many drops/minute does the nurse adjust the flow rate to deliver?</P>' WHERE QID = 54
UPDATE Questions SET AlternateStem = '<P>When patients have any type of reaction to a blood transfusion, what does the nurse do <B>FIRST</B>?</P>' WHERE QID = 55
UPDATE Questions SET AlternateStem = '<P>Which group of symptoms indicates a hemolytic transfusion reaction?</P>' WHERE QID = 56
UPDATE Questions SET AlternateStem = '<P>A patient has a burn injury.  A nurse assesses the patient.  The burn area is blistered and painful.  Which classification <B>BEST</B> describes the burned area?</P>' WHERE QID = 57
UPDATE Questions SET AlternateStem = '<P>A patient has a major burn injury.  A nurse gives medication to the patient. Which route of delivery is <B>BEST</B> for the absorption of medication by a patient with a major burn injury?</P>' WHERE QID = 58
UPDATE Questions SET AlternateStem = '<P>A patient has a major burn injury.  A nurse gives medication to the patient. Which route of delivery is <B>BEST</B> for the absorption of medication by a patient with a major burn injury?</P>' WHERE QID = 59
UPDATE Questions SET AlternateStem = '<P>A patient is in the shock phrase after a full thickness burn injury.  A nurse cares for the patient.  Which finding does the nurse expect to see during this phase?>/P>' WHERE QID = 60
UPDATE Questions SET AlternateStem = '<P>A patient has a burn injury from an apartment fire.  A nurse assesses the patient.  Which observation is the nurse <B>MOST</B> concerned about?</P>' WHERE QID = 61
UPDATE Questions SET AlternateStem = '<P>A patient has a full thickness burn injury of the legs.  A nurse teaches the patient about appropriate diet.  The nurse determines the teaching is successful because of the menu that the patient chooses.  Which menu does the patient choose?</P>' WHERE QID = 62
UPDATE Questions SET AlternateStem = '<P>The home care nurse cares for a client diagnosed with a fractured humerus due to a fall in the home. Which of the following observations, if made by the nurse, requires an immediate intervention?<P>' WHERE QID = 66
UPDATE Questions SET AlternateStem = '<P>An elderly client is admitted to the hospital for abdominal surgery. The admitting orders for the client include the following:  activity as desired, standard bowel prep, and an intravenous infusion of 5% dextrose in water.  The nurse should start the IV at 6 pm on the evening before surgery, and the IV should infuse at 75 cc per hour. What is the <B>PRIMARY</B> purpose of giving IV fluids to the client before surgery? </P>' WHERE QID = 67
UPDATE Questions SET AlternateStem = '<P>The nurse instructs a patient about how to successfully establish a regular exercise program. The nurse determines further teaching is needed if the patient makes which of the following statements?<P>' WHERE QID = 68
UPDATE Questions SET AlternateStem = '<P>A middle-aged adult comes to the clinic.  He complains of having difficulty sleeping and being constantly tired.  The clinic nurse interviews the client.  The nurse learns that the client works as a security guard and that he frequently works nights.  Which of the following is the <B>BEST</B> response for the nurse to say <B>FIRST</B>?  </P>' WHERE QID = 69
UPDATE Questions SET AlternateStem = '<P>The nurse prepares four patients for surgery. The nurse is <B>MOST</B> concerned about the psychological adjustment of which of the following patients?<P>' WHERE QID = 70
UPDATE Questions SET AlternateStem = '<P>What is psoriasis?</P>' WHERE QID = 71
UPDATE Questions SET AlternateStem = '<P>To promote evening rest and sleep for patients who are immobilized and in bed, it is <B>MOST</B> important for the nurse to provide which of the following?<P>' WHERE QID = 72
UPDATE Questions SET AlternateStem = '<P>At discharge, the nurse tells a patient how to follow a low-calorie diet to lose weight.  What is the ideal rate for a person to lose weight?</P>' WHERE QID = 73
UPDATE Questions SET AlternateStem = '<P>A patient with acute pain has a physician''s order for meperidine (Demerol) 50 mg IM every 3-4 hrs prn for pain. The patient asks the nurse for the medication at bedtime. Prior to administering the pain medication, the nurse should take which of the following actions?<P>' WHERE QID = 74
UPDATE Questions SET AlternateStem = '<P>A nurse watches a patient sign his name on a document of informed consent for a procedure.  What question is <B>MOST</B> important for the nurse to assess for?</P>' WHERE QID = 75
UPDATE Questions SET AlternateStem = '<P>The nurse helps a patient to cough and deep breathe after surgery. It is desirable for the patient to assume which of the following positions?<P>' WHERE QID = 76
UPDATE Questions SET AlternateStem = '<P>What is the <B>PRIMARY</B> reason for elderly adults to be constipated?</P>' WHERE QID = 77
UPDATE Questions SET AlternateStem = '<P>Which of the following actions is essential for the nurse to take after administration of a preoperative medication to a patient?<P>' WHERE QID = 78
UPDATE Questions SET AlternateStem = '<P>Before surgery, a patient will receive a liver scan.  Which statement <B>BEST</B> describes a liver scan?</P>' WHERE QID = 79
UPDATE Questions SET AlternateStem = '<P>The nurse understands which of the following behaviors is helpful to facilitate a patient to have bowel elimination?<P>' WHERE QID = 80
UPDATE Questions SET AlternateStem = '<P>A patient experiences episodes of acute pain.  Which of the following physiological changes occurs during acute pain?</P>' WHERE QID = 81
UPDATE Questions SET AlternateStem = '<P>Several days postoperatively, a patient complains of pain, tenderness, and redness of the right calf. Which of the following are critical signs and symptoms the nurse should assess for <B>NEXT</B>?<P>' WHERE QID = 82
UPDATE Questions SET AlternateStem = '<P>A patient is at risk to develop a pressure ulcer.  Which of the following does the nurse identify is a risk factor for the patient?</P>' WHERE QID = 83
UPDATE Questions SET AlternateStem = '<P>A client comes to the emergency room after puncturing a foot with a dirty, rusty nail. The client states the last Td immunization was 6 years ago.  Which of the following actions should the nurse take <B>FIRST</B>?<P>' WHERE QID = 84
UPDATE Questions SET AlternateStem = '<P>The pattern of urinary elimination changes as a person gets older.  Which of the following changes is associated with aging?</P>' WHERE QID = 85
UPDATE Questions SET AlternateStem = '<P>The nurse cares for a postoperative patient with a nasogastric tube. Which of the following observations by the nurse is the <B>MOST</B> reliable indication the nasogastric tube is correctly positioned?<P>' WHERE QID = 86
UPDATE Questions SET AlternateStem = '<P>A patient begins intermittent heparin therapy.  A nurse cares for the patient.  Which laboratory test does the nurse recognize is used to monitor the effectiveness of heparin?</P>' WHERE QID = 87
UPDATE Questions SET AlternateStem = '<P>The nurse identifies which of the following findings is characteristic of chronic pain?<P>' WHERE QID = 88
UPDATE Questions SET AlternateStem = '<P>A patient with ovarian cancer experiences severe pain. A nurse provides care for the patient.  Which principle regarding pain medication does the nurse remember?</P>' WHERE QID = 89
UPDATE Questions SET AlternateStem = '<P>A nurse cares for a patient with an abdominal wound. The nurse notices purulent drainage from the wound. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 90
UPDATE Questions SET AlternateStem = '<P>The home care nurse visits an elderly client living alone on a limited income. The client''s diet consists primarily of carbohydrates. Based on an understanding of the nutritional needs of the elderly, which of these interpretations of the client''s diet by the nurse is most justified?<P>' WHERE QID = 91
UPDATE Questions SET AlternateStem = '<P>The nurse performs discharge teaching for a patient receiving sodium warfarin (Coumadin). The nurse determines further teaching is required if the patient makes which of the following statements?<P>' WHERE QID = 92
UPDATE Questions SET AlternateStem = '<P>A nurse gives pain medication to a patient.  After the nurse gives the medication, what is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 93
UPDATE Questions SET AlternateStem = '<P>The nurse knows an important fact about Coumadin is it<P>' WHERE QID = 94
UPDATE Questions SET AlternateStem = '<P>On the morning before surgery, a patient signs an operative consent form. Soon afterward, the patient tells the nurse that the patient does not want the surgery. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 95
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with acute gout.  The patient has taken ibuprofen every day as prescribed by the doctor for four weeks. The patient tells the nurse that she has a ringing sound in her ears. What is the <B>BEST</B> interpretation of this information by the nurse?</P>' WHERE QID = 309
UPDATE Questions SET AlternateStem = '<P>A patient receives intermittent infusion of ceftriaxone (Rocephin) IV. A nurse cares for the patient.  Before the nurse gives the patient the next dose, the doctor orders heparin 10 units IV. The nurse understands the reason why heparin is ordered.  Why is heparin ordered?</P>' WHERE QID = 312
UPDATE Questions SET AlternateStem = '<P>The nurse teaches a group of pregnant women about Lamaze techniques. During class, the nurse hears one woman tell another woman, "Taking medication during pregnancy is very risky; you should stop taking the medication that your physician prescribed." Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 315
UPDATE Questions SET AlternateStem = '<P>A home care nurse visits a client.  The nurse learns that the physician has prescribed methotrexate for the client. The nurse knows that methotrexate is used to treat certain conditions.  Which conditions are treated with methotrexate?</P>' WHERE QID = 318
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with atrial fibrillation.  The client is receiving lisinopril (Zestril). A nurse teaches the client about diet.  Which statement by the client indicates that more teaching is needed?</P>' WHERE QID = 321
UPDATE Questions SET AlternateStem = '<P>A nurse understands that phentermine (Adipex-P) is prescribed for certain clients.  Which client is this medication <B>MOST</B> often prescribed for?</P>' WHERE QID = 324
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with bipolar disorder.  The client is receiving lithium (Lithobid) 300 mg tid. A nurse teaches the client.  The nurse determines that teaching is effective because of something the client says.  What does the client say?</P>' WHERE QID = 327
UPDATE Questions SET AlternateStem = '<P>A client complains of neuralgia.  The physician orders gabapentin (Neurontin) for the client. The client tells the nurse that she has difficulty swallowing capsules. Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 333
UPDATE Questions SET AlternateStem = '<P>A patient is newly diagnosed with glaucoma.  The patient has a prescription for dorzolamide (Trusopt).  A nurse teaches the patient about the medication.  The nurse knows the patient needs more education because of something the patient says.  What does the patient say?</P>' WHERE QID = 336
UPDATE Questions SET AlternateStem = '<P>An elderly client has taken sustained-release nitroglycerin (Nitrocot) for several years.  The client heard a TV ad about sildenafil citrate (Viagra).  He asks the nurse if he can take both medications on the same day.  Which response by the nurse is <B>MOST</B> appropriate?</P>' WHERE QID = 339
UPDATE Questions SET AlternateStem = '<P>The nurse understands the similarities between Schedule I drugs and Schedule V drugs.  Which statement represents a similarity between Schedule I drugs and Schedule V drugs?</P>' WHERE QID = 342
UPDATE Questions SET AlternateStem = '<P>A patient has taken minocycline (Minocin) for acne for over a year. A nurse performs an assessment of the patient.  The nurse looks for signs of a Candida superinfection.  What part of the patient’s body does the nurse examine for signs of a Candida superinfection?</P>' WHERE QID = 345
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with type 1 diabetes. A home care nurse cares for the client. The client receives insulin therapy in a four-dose protocol. The client injects insulin lispro (Humalog) SC at 11:45 a.m. The nurse knows that the peak action of insulin lispro (Humalog) occurs at a certain time.  At which time does the peak action of this medication occur?</P>' WHERE QID = 348
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with pulmonary edema. A nurse gives the initial dose of furosemide (Lasix) to the patient.  Which piece of equipment is <B>MOST</B> important for the nurse to give the patient easy access to?</P>' WHERE QID = 351
UPDATE Questions SET AlternateStem = '<P>An elderly patient is taking finasteride (Proscar).  A nurse cares for the patient.  The nurse teaches the patient about possible side effects. What is/are the <B>MOST</B> likely side effect(s) of finasteride (Proscar)?</P>' WHERE QID = 356
UPDATE Questions SET AlternateStem = '<P>Which medication is used for the treatment of Parkinson''s disease?</P>' WHERE QID = 359
UPDATE Questions SET AlternateStem = '<P>A young adult calls the clinic and talks to a nurse.  The young adult’s sexual partner is diagnosed with trichomoniasis and is taking metronidazole (Flagyl) for treatment. What does the nurse expect the doctor to recommend for the young adult?</P>' WHERE QID = 362
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with rheumatoid arthritis.  The doctor orders prednisone (Cordrol). A nurse teaches the patient about this drug.  What is the <B>MOST</B> important point the nurse includes in the teaching?</P>' WHERE QID = 365
UPDATE Questions SET AlternateStem = '<P>A patient receives probenecid (Benemid). A nurse teaches the patient about this drug.  Which statement is <B>MOST</B> important for the nurse to include in the teaching?</P>' WHERE QID = 369
UPDATE Questions SET AlternateStem = '<P>The nurse teaches a pregnant woman about taking carbonyl iron (Feosol). Which statement is <B>MOST</B> important for the nurse to include in the teaching?</P>' WHERE QID = 372
UPDATE Questions SET AlternateStem = '<P>A patient is taking the contraceptive norgestrel (Ovrette). A nurse assesses the patient.  Which statement by the patient is <B>MOST</B> important for the nurse to follow up on?</P>' WHERE QID = 374
UPDATE Questions SET AlternateStem = '<P>The nurse observes another nurse give a parenteral injection. The nurse pulls the patient''s skin to one side and holds the skin, inserts the needle at a 90-degree angle, injects the medication, withdraws the needle, and releases the skin. Which technique does the nurse use to give the medication?</P>' WHERE QID = 377
UPDATE Questions SET AlternateStem = '<P>For patients who are diagnosed with hypothyroidism, which medication is prescribed?</P>' WHERE QID = 382
UPDATE Questions SET AlternateStem = '<P>A patient is a mother of two school-age children.  The patient tells the nurse that her husband has recently become unemployed.  The patient complains of feeling depressed. Which statement about the husband’s unemployment does the nurse understand is true?</P>' WHERE QID = 388
UPDATE Questions SET AlternateStem = '<P>A patient has disorientation due to dementia.  A nurse cares for the patient.  Which group of symptoms does the nurse anticipate when providing care for this patient?</P>' WHERE QID = 389
UPDATE Questions SET AlternateStem = '<P>A nurse cares for clients in the mental health clinic. A client is diagnosed with obsessive-compulsive disorder.  The client tells the nurse that he is afraid that he will contract AIDS. The client reports that he spends much of the day washing his hands and spraying disinfectant in the room. Which of the following statements does the nurse understand his handwashing behavior represents?</P>' WHERE QID = 390
UPDATE Questions SET AlternateStem = '<P>A client took an overdose of diazepam (Valium).  The client is brought to the emergency room by family members.  The family reports that the client has become increasingly depressed and withdrawn during the last month.  Which question is <B>MOST</B> important for the nurse to ask of the client during the initial interview?</P>' WHERE QID = 391
UPDATE Questions SET AlternateStem = '<P>A client is admitted to the hospital with a diagnosis of paranoid schizophrenia.  The client’s spouse states that the client has not slept in three nights.  Which nursing goal is <B>MOST</B> important in the care of this client?</P>' WHERE QID = 393
UPDATE Questions SET AlternateStem = '<P>The nurse cares for clients in the pediatric clinic. The nurse understands that according to Erikson''s stages of psychosocial development, a child develops trust and important early attachments.  During which years does a child develop this trust and important early attachments?</P>' WHERE QID = 394
UPDATE Questions SET AlternateStem = '<P>A client comes to a local clinic with complaints of dizziness and a heart that is beating too fast.  The client''s physical exam is normal. The client reports that his company recently lost a large sum of money, and the client feels responsible for the loss. The client tells the nurse that he is very anxious. Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 395
UPDATE Questions SET AlternateStem = '<P>A nurse cares for patients on the medical/surgical unit. The nurse admits a patient for possible appendicitis. During the admission interview, the patient states, "Most days I drink about one pint of vodka." The nurse understands the <B>MOST</B> likely time for the patient to develop delirium from alcohol withdrawal.  When is the <B>MOST</B> likely time for the patient to develop alcohol withdrawal delirium?</P>' WHERE QID = 396
UPDATE Questions SET AlternateStem = '<P>A middle-aged client is admitted to the hospital with a diagnosis of terminal lung cancer. The client''s spouse reports to the nurse that the client did <B>NOT</B> want to come to the hospital and "refuses to slow down."  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 398
UPDATE Questions SET AlternateStem = '<P>A nurse cares for patients in an inpatient psychiatric unit.  The nurse leads an adolescent social/support group to discuss the difficulties of growing up in today''s society. The nurse understands the therapeutic benefit of a social/support group.  Which concept is the therapeutic benefit of a social/support group based on?</P>' WHERE QID = 400
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with a phobic disorder.  The client joins a group meeting that is led by a psychiatric nurse. During the first meeting, the client makes the following statements: "I know my feeling of being terrified of closed spaces is dumb. It does <B>NOT</B> make any sense. I just can <B>NOT</B> seem to do anything about it. Right now I get nervous and scared just thinking about it." Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 401
UPDATE Questions SET AlternateStem = '<P>A nurse teaches a client about phenelzine sulfate (Nardil). The nurse realizes the more teaching is necessary because of something the client says.  What does the client say?</P>' WHERE QID = 402
UPDATE Questions SET AlternateStem = '<P>A patient has a diagnosis of antisocial personality disorder.  The patient has an appointment with a nurse, but he does <B>NOT</B> show up.  The nurse contacts the patient to remind him of the appointment, and the patient states, "I would rather meet between 12 and 1." Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 403
UPDATE Questions SET AlternateStem = '<P>During the second session of individual therapy, a patient sits quietly with arms folded and eyes looking down. Which approach by the nurse is <B>BEST</B>?</P>' WHERE QID = 404
UPDATE Questions SET AlternateStem = '<P>Which statement about anorexia nervosa is true?</P>' WHERE QID = 405
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with substance abuse.  Which principle is nursing care for this patient based on?</P>' WHERE QID = 406
UPDATE Questions SET AlternateStem = '<P>A mother delivers a newborn baby with a cleft palate. The mother already has two children.  The parents visit the baby in the newborn nursery. Which statement by the nurse to the parents is <B>BEST</B>?</P>' WHERE QID = 408
UPDATE Questions SET AlternateStem = '<P>A nurse cares for patients in the pediatric clinic. The mother of a young child asks the nurse why her child is involved in play therapy. Which statement by the nurse is <B>BEST</B>?</P>' WHERE QID = 410
UPDATE Questions SET AlternateStem = '<P>A nurse volunteers in a homeless shelter. The nurse notices that another volunteer develops overly close relationships with the older women in the shelter. During a conversation, the volunteer tells the nurse that several years before the volunteer’s mother died, she refused to let her mother come live with her. The nurse understands that the volunteer is using a defense mechanism in her relationships with the older women.  Which defense mechanism does the volunteer use?</P>' WHERE QID = 411
UPDATE Questions SET AlternateStem = '<P>A nurse cares for clients in the mental health clinic. A client with depression joins an ongoing therapy group. What is the goal of group therapy for the client?</P>' WHERE QID = 412
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with inoperable cancer.  After chemotherapy, the client has difficulty walking. The nurse tries to help the client to the bathroom, but the client says, "Leave me alone. You treat me like a child." Which interpretation of the client''s behavior by the nurse is <B>MOST</B> justifiable?</P>' WHERE QID = 413
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with dementia.  The patient tells a story about something that the nurse knows is <B>NOT</B> true. The nurse overhears the patient tell the story.  Which action by the nurse is <B>BEST</B>?</P>' WHERE QID = 414
UPDATE Questions SET AlternateStem = '<P>A patient is admitted after an accidental overdose. One of the nursing assistants is critical of the patient.  The nursing assistant says, "The family is worried about the patient, but the patient does <B>NOT</B> care how anybody feels." Which response by the nurse to the nursing assistant is <B>BEST</B>?</P>' WHERE QID = 415
UPDATE Questions SET AlternateStem = '<P>One morning at a group therapy session, several patients begin to criticize another patient for his passive behavior. The nurse leader says that the patient is a very sensitive person who has problems, and the other patients should stop criticizing him. What is the <B>MOST</B> likely effect of this statement on the patient?</P>' WHERE QID = 416
UPDATE Questions SET AlternateStem = '<P>The nurse understands that according to Maslow''s hierarchy of needs, some needs are <B>MOST</B> basic to the health maintenance plan for any patient.  Which needs are <B>MOST</B> basic to any patient’s health maintenance plan?</P>' WHERE QID = 417
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with bipolar disorder.  During the period of elation, which approach does the nurse plan to use often?</P>' WHERE QID = 418
UPDATE Questions SET AlternateStem = '<P>A nurse asks a patient the date and day of the week.  The patient responds incorrectly.  How does the nurse <B>BEST</B> describe the patient''s mental state?</P>' WHERE QID = 419
UPDATE Questions SET AlternateStem = '<P>A nurse prepares to lead a group session for patients who are dependent on alcohol. The nurse understands why an alcoholic person drinks.  Why does an alcoholic person drink?</P>' WHERE QID = 420
UPDATE Questions SET AlternateStem = '<P>An adolescent is admitted to a psychiatric hospital. The adolescent reports that he hit his brother during an argument the previous weekend. Later that evening after the argument, the patient''s arm became paralyzed. How does the nurse anticipate the patient will react to the paralysis of his arm?</P>' WHERE QID = 421
UPDATE Questions SET AlternateStem = '<P>A client is told by the physician that the client''s cancer can <B>NOT</B> be operated on. The nurse enters the room a short time later and finds the patient crying. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 422
UPDATE Questions SET AlternateStem = '<P>A patient has a history of substance abuse. A nurse plans care for the patient.  Which approach is <B>MOST</B> important for the nurse to take in planning care for this patient?</P>' WHERE QID = 423
UPDATE Questions SET AlternateStem = '<P>A nurse intervenes with a violent patient.  Which action does the nurse take?</P>' WHERE QID = 424
UPDATE Questions SET AlternateStem = '<P>A 29-year-old woman is told by the doctor that she can <B>NOT</B> have children.  After the woman is told this information, she forms a close attachment to her niece and nephew. The nurse understands that this behavior is a defense mechanism.  Which defense mechanism is the woman’s behavior an example of?</P>' WHERE QID = 425
UPDATE Questions SET AlternateStem = '<P>A client is prescribed escitalopram (Lexapro) 10 mg daily. A nurse teaches the family about the side effects of this medication.  Which side effect does the nurse teach the family to watch for?</P>' WHERE QID = 426
UPDATE Questions SET AlternateStem = '<P>A woman is admitted to the hospital for a possible mastectomy. On the night before the surgery, the woman’s husband seems tense and paces up and down the hall. Which comment by the nurse to the husband is BEST?</P>' WHERE QID = 427
UPDATE Questions SET AlternateStem = '<P>A client had a hysterectomy six months ago.  The client suddenly develops an intense fear of elevators. When the client approaches an elevator, she becomes panicky and can <B>NOT</B> get on the elevator. The nurse understands the cause of the client’s fear.  What is the cause of the client’s fear of elevators?</P>' WHERE QID = 429
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with depression.  A nurse cares for the patient.  The nurse encourages the patient to join an activity. Which approach by the nurse is <B>BEST</B>?</P>' WHERE QID = 430
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with a peptic ulcer.  A nurse cares for the client.  Which nursing measure is indicated by the client’s condition?</P>' WHERE QID = 432
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with antisocial personality disorder. A nurse cares for the patient. The patient says something that BEST indicates to the nurse that the patient is getting better.  What does the patient say?</P>' WHERE QID = 434
UPDATE Questions SET AlternateStem = '<P>A patient with depression attempts suicide. A nurse cares for the patient. The nurse understands the <B>MOST</B> likely reason why a patient attempts suicide.  Why does a patient <B>MOST</B> likely attempt suicide?</P>' WHERE QID = 435
UPDATE Questions SET AlternateStem = '<P>A client is diagnosed with a myocardial infarction (MI). A home care nurse makes an initial visit to the client’s home.  The client''s spouse states that she has difficulty coping with the client''s "obsessive-compulsive" tendencies. The client says something to the nurse that is consistent with obsessive-compulsive disorder.  What does the client say?</P>' WHERE QID = 436
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with a terminal illness. A nurse cares for the patient.  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 437
UPDATE Questions SET AlternateStem = '<P>A nurse provides care to an alcoholic patient.  The alcoholic patient says something to the nurse that indicates the patient has an accurate understanding of the problem.  What does the patient say?</P>' WHERE QID = 439
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with paranoid schizophrenia.  The patient tells the nurse, "I have a feeling of numbness in my legs. They feel like they do <B>NOT</B> belong to me.  I think someone on TV is controlling my walking." Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 440
UPDATE Questions SET AlternateStem = '<P>A client is in the hypertension clinic.  The client expresses worry to the nurse that his wife has been unemployed for more than six months.  The client is afraid that soon they will <B>NOT</B> be able to pay the rent. Which response by the nurse is <B>MOST</B> appropriate?</P>' WHERE QID = 442
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with bipolar depression.  The patient is hospitalized during the elation phase of the illness. The patient says to the nurse, "I just bought myself a home computer and a large screen TV for the family." Which interpretation of the patient’s behavior by the nurse is <B>MOST</B> accurate?</P>' WHERE QID = 443
UPDATE Questions SET AlternateStem = '<P>A patient has a below-the-knee amputation.  The patient seems angry and demanding following the surgery.  Which interpretation of the patient’s behavior by the nurse is <B>MOST</B> justifiable?</P>' WHERE QID = 444
UPDATE Questions SET AlternateStem = '<P>A client has a diagnosis of schizophrenia. A nurse admits the client to the psychiatric unit. Which actions by the nurse <B>BEST</B> meet the client''s needs?</P>' WHERE QID = 445
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with depression.  A nurse interacts with the patient. Which of the following thoughts does the nurse expect the patient to express?</P>' WHERE QID = 446
UPDATE Questions SET AlternateStem = '<P>A young man is brought to the emergency department by a friend. The patient is upset and screams, "I can''t stop seeing things. Help me, I''m going crazy." His friend reports to the nurse that the patient took LSD earlier in the day.  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 447
UPDATE Questions SET AlternateStem = '<P>A psychiatric patient develops a strong attachment to another patient who repeatedly insults him. The nurse observes the attachment behavior and understands that it is an example of something.  What is the attachment behavior an example of?</P>' WHERE QID = 448
UPDATE Questions SET AlternateStem = '<P>The doctor prescribes lithium carbonate (Eskalith) for a patient. The nurse understands that certain types of medications are contraindicated for this patient.  Which type of medications is contraindicated?</P>' WHERE QID = 449
UPDATE Questions SET AlternateStem = '<P>A patient with a terminal illness dies quietly in her sleep. What does the nurse do?</P>' WHERE QID = 450
UPDATE Questions SET AlternateStem = '<P>A graduate nursing student fails an examination.  The student accuses the psychiatric nurse instructor of being a bad teacher and causing the student to fail. The nurse instructor identifies the student’s behavior as an example of something.  What is the student’s behavior an example of?</P>' WHERE QID = 452
UPDATE Questions SET AlternateStem = '<P>One morning a nurse finds a patient crying.  The nurse approaches the patient. The patient says, "What do you want? Go away, you can''t help me. I hate you and I hate myself." Which response by the nurse is <B>BEST</B>?</P>' WHERE QID = 453
UPDATE Questions SET AlternateStem = '<P>What do the ABCs of CPR stand for?</P>' WHERE QID = 509
UPDATE Questions SET AlternateStem = '<P>A nurse observes a person suddenly collapse on the street.  The nurse finds the person unresponsive.  What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 510
UPDATE Questions SET AlternateStem = '<P>During cardiopulmonary resuscitation (CPR) of an adult, the nurse assesses the pulse.  Which artery does the nurse check?</P>' WHERE QID = 511
UPDATE Questions SET AlternateStem = '<P>A nurse performs CPR on an adult.  What is the correct ratio of compressions to breaths on an adult?</P>' WHERE QID = 512
UPDATE Questions SET AlternateStem = '<P>The nurse administers cardiopulmonary resuscitation.  Which action is <B>MOST</B> important for the nurse to take?</P>' WHERE QID = 513
UPDATE Questions SET AlternateStem = '<P>A nurse discovers an unconscious person in the street. The nurse notes that the person is <B>NOT</B> breathing. What does the nurse do?</P>' WHERE QID = 514
UPDATE Questions SET AlternateStem = '<P>A patient is admitted to the cardiac unit with a diagnosis of acute myocardial infarction.  Several hours later, a nurse starts an intravenous lidocaine drip for the patient.  What does the nurse understand is the purpose of this medication?</P>' WHERE QID = 515
UPDATE Questions SET AlternateStem = '<P>An older man is admitted to the hospital with a diagnosis of heart failure (HF). A nurse assesses the patient.  Which of the following set of findings is <B>MOST</B> characteristic of heart failure?</P>' WHERE QID = 516
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with heart failure.  Digoxin (Lanoxin) is prescribed for the patient.  The nurse teaches the patient about this medication.  What does the nurse say is the purpose of this medication?</P>' WHERE QID = 517
UPDATE Questions SET AlternateStem = '<P>A doctor prescribes hydrochlorothiazide (HydroDIURIL) 50 mg once a day for a patient. What is the <B>BEST</B> time for the nurse to give the medication to the patient?</P>' WHERE QID = 518
UPDATE Questions SET AlternateStem = '<P>A client comes to the cardiac clinic.  The client complains of anorexia, nausea, and blurred vision. The nurse understands that these symptoms of something that the client may be experiencing.  What do they indicate the client is possibly experiencing?</P>' WHERE QID = 519
UPDATE Questions SET AlternateStem = '<P>A client with heart failure (HF) is discharged from the hospital.  A week later, the client comes to the cardiac clinic for a follow-up visit. A nurse assesses the client.  Which statement by the client indicates to the nurse that the client’s condition is improved?</P>' WHERE QID = 520
UPDATE Questions SET AlternateStem = '<P>A patient is receiving digoxin (Lanoxin) and hydrochlorothiazide (HCTZ).  What does the nurse understand is a major side effect of hydrochlorothiazide (HCTZ)?</P>' WHERE QID = 521
UPDATE Questions SET AlternateStem = '<P>What type of edema is related to cardiac failure?</P>' WHERE QID = 522
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with angina.  A nurse teaches the patient about care at home. The nurse determines that teaching is effective because of something the patient says.  What does the patient say?</P>' WHERE QID = 523
UPDATE Questions SET AlternateStem = '<P>A patient has angina.  A nurse teaches the patient about side effects of nitroglycerin.  What are common side effects of nitroglycerin?</P>' WHERE QID = 524
UPDATE Questions SET AlternateStem = '<P>A patient has angina.  A nurse provides discharge teaching for the patient.  What does the nurse teach is <B>MOST</B> important for the patient to report?</P>' WHERE QID = 525
UPDATE Questions SET AlternateStem = '<P>A patient is admitted to the cardiac unit with a diagnosis of acute myocardial infarction.  A nurse attaches a cardiac monitor to the patient.  What does the nurse understand is the reason for the cardiac monitor?</P>' WHERE QID = 526
UPDATE Questions SET AlternateStem = '<P>A patient has a myocardial infarction.  One week later, the patient complains of fatigue to the nurse. The nurse notes that the patient is slightly short of breath and the patient’s pulse rate is 110 bpm. Which action by the nurse is <B>BEST</B>?</P>' WHERE QID = 527
UPDATE Questions SET AlternateStem = '<P>A patient had a myocardial infarction.  A nurse prepares a nursing care plan for the patient.  Which goal does the nurse include in the care plan?</P>' WHERE QID = 528
UPDATE Questions SET AlternateStem = '<P>A client has a myocardial infarction.  The nurse promotes rest for the client after the myocardial infarction.  What is the <B>PRIMARY</B> purpose of promoting rest?</P>' WHERE QID = 529
UPDATE Questions SET AlternateStem = '<P>A patient had a myocardial infarction.  A nurse plans for the patient’s discharge.  What information from the patient’s health history is <B>MOST</B> important to consider for the discharge plan?</P>' WHERE QID = 530
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with a myocardial infarction. The nurse teaches the patient about diet.  The nurse determines that teaching is effective because of the menu that the patient chooses.  Which menu does the patient choose?</P>' WHERE QID = 531
UPDATE Questions SET AlternateStem = '<P>A patient had a cardiac catheterization. After the procedure, the nurse discovers that the patient is bleeding from the cut-down site. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 532
UPDATE Questions SET AlternateStem = '<P>A patient is admitted to the coronary care unit with a diagnosis of acute myocardial infarction. The patient''s skin is clammy, and the patient’s blood pressure is 85/50.  The patient appears restless and anxious. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 533
UPDATE Questions SET AlternateStem = '<P>A patient had an acute myocardial infarction.  Which laboratory test results does the nurse expect to be elevated in the patient?</P>' WHERE QID = 534
UPDATE Questions SET AlternateStem = '<P>A nurse teaches a client in the outpatient clinic about the purpose of a stress test. Which statement by the nurse is <B>BEST</B>?</P>' WHERE QID = 535
UPDATE Questions SET AlternateStem = '<P>What causes the pain that is associated with angina?</P>' WHERE QID = 536
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with angina.  A nurse cares for the patient.  The nurse understands the reason nitroglycerin is used in the treatment of angina pectoris.  Why is nitroglycerin used in the treatment of angina pectoris?</P>' WHERE QID = 537
UPDATE Questions SET AlternateStem = '<P>A patient had a cardiac catheterization.  Which nursing measure is <B>MOST</B> important immediately after a cardiac catheterization?' WHERE QID = 538
UPDATE Questions SET AlternateStem = '<P>Propranolol (Inderal) is ordered for a patient on the medical unit. A nurse cares for the patient.  What information in the patient''s history causes the nurse to intervene? </P>' WHERE QID = 539
UPDATE Questions SET AlternateStem = '<P>A patient receives methyldopa (Aldomet).  A nurse cares for the patient.  The nurse teaches the patient about side effects of Aldomet.  What is a common side effect of Aldomet?</P>' WHERE QID = 541
UPDATE Questions SET AlternateStem = '<P>What is the cause of essential hypertension?</P>' WHERE QID = 542
UPDATE Questions SET AlternateStem = '<P>A patient is receiving an antihypertensive medication.  A nurse performs discharge teaching for the patient.  The nurse determines that more teaching is necessary because of something the patient says.  What does the patient say?</P>' WHERE QID = 543
UPDATE Questions SET AlternateStem = '<P>The nurse performs blood pressure screenings at the local grocery store. Which blood pressure reading indicates to the nurse that a client has stage 1 hypertension?</P>' WHERE QID = 544
UPDATE Questions SET AlternateStem = '<P>A patient has hypertension.  A nurse teaches the patient about hypertension.  The nurse knows that teaching is successful because of something the patient says.  What does the patient say? </P>' WHERE QID = 545
UPDATE Questions SET AlternateStem = '<P>A client takes verapamil (Calan) in the sustained-release form.  The client complains of a headache. What does the nurse tell the client?</P>' WHERE QID = 546
UPDATE Questions SET AlternateStem = '<P>A patient receives treatment for hypertension.  A nurse monitors the patient.  Which blood pressure reading indicates to the nurse that the treatment for hypertension is successful?</P>' WHERE QID = 547
UPDATE Questions SET AlternateStem = '<P>A patient had a femoral-to-popliteal bypass graft. A nurse cares for the patient immediately after the procedure. Which observation is the nurse <B>MOST</B> concerned about?</P>' WHERE QID = 548
UPDATE Questions SET AlternateStem = '<P>A nurse administers dopamine via IV drip.  What is the <B>MOST</B> important factor for the nurse to consider?</P>' WHERE QID = 549
UPDATE Questions SET AlternateStem = '<P>To maintain adequate circulation, what is the <B>MOST</B> important factor?</P>' WHERE QID = 550
UPDATE Questions SET AlternateStem = '<P>A patient on bedrest is at risk for thrombophlebitis.  A nurse cares for the patient.  Which nursing measure is <B>MOST</B> effective for preventing thrombophlebitis in the patient?</P>' WHERE QID = 551
UPDATE Questions SET AlternateStem = '<P>A patient receives a continuous intravenous infusion of heparin.  What other medication is <B>MOST</B> important for the nurse to have available?</P>' WHERE QID = 552
UPDATE Questions SET AlternateStem = '<P>A patient is started on sodium warfarin (Coumadin). The nurse teaches the patient to have bloodwork done regularly.  What does the bloodwork measure?</P>' WHERE QID = 553
UPDATE Questions SET AlternateStem = ' <P>What medication is the antagonist of Coumadin?</P>' WHERE QID = 554
UPDATE Questions SET AlternateStem = '<P>A patient arrives in the emergency room.  The patient complains of severe pain in the left leg.  The pain is <B>NOT</B> relieved by rest or medication. A nurse examines the patient.  What is the nurse <B>MOST</B> likely to find?</P>' WHERE QID = 555
UPDATE Questions SET AlternateStem = '<P>What is the purpose of a coronary artery bypass graft (CABG)?</P>' WHERE QID = 556
UPDATE Questions SET AlternateStem = '<P>A patient had an aortofemoral bypass. A nurse cares for the patient 12 hours after surgery. What position is <B>MOST</B> important for the nurse to place the patient in?</P>' WHERE QID = 557
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with peripheral vascular disease.  A nurse plans discharge teaching for the patient.  What is <B>MOST</B> important for the nurse to address in the discharge teaching?</P>' WHERE QID = 558
UPDATE Questions SET AlternateStem = '<P>What is intermittent claudication? </P>' WHERE QID = 559
UPDATE Questions SET AlternateStem = '<P>A nurse works in the student health service of a college.  The nurse plans a series of brief presentations to students on how they can reduce health risks.  One of the topics is toxic shock syndrome (TSS).  For this presentation, which audience is <B>MOST</B> important for the nurse to target?</P>' WHERE QID = 560
UPDATE Questions SET AlternateStem = '<P>A patient is diagnosed with a deep vein thrombosis in the left leg.  The patient is treated for iron deficiency anemia and is started on heparin.  A nurse cares for the patient.  Which observation is the nurse <B>MOST</B> concerned about?</P>' WHERE QID = 561
UPDATE Questions SET AlternateStem = '<P>A patient is scheduled to receive a pacemaker.  A cardiac nurse teaches the patient about the cardiac conduction cycle and how the cycle usually flows.  What does the nurse identify as the natural pacemaker of the heart?</P>' WHERE QID = 563
UPDATE Questions SET AlternateStem = '<P>An office nurse prepares a client for a resting electrocardiogram (EKG).  The nurse teaches the client about the procedure.  Which statement by the client indicates that the teaching is successful?</P>' WHERE QID = 564
UPDATE Questions SET AlternateStem = '<P>New graduate nurses will care for cardiac patients on the medical surgical unit.  A nurse educator teaches an orientation class for the new graduates.  The educator reminds the graduates about the QRS complex of an electrocardiogram (EKG).  What does the QRS complex represent?</P>' WHERE QID = 565
UPDATE Questions SET AlternateStem = '<P>A patient has a new permanent pacemaker that is implanted in the area below the left clavicle.  A home care nurse visits the patient.  The patient talks about the pacemaker.  Which statement by the patient is <B>MOST</B> important for the nurse to respond to?</P>' WHERE QID = 566
UPDATE Questions SET AlternateStem = '<P>There is an increase in the number of patients with cardiac conditions at a hospital.  As a result, a medical surgical unit is changed into a cardiac unit.  The nurse manager  reviews with staff the differences between defibrillation and cardioversion.  Which of the following are characteristics that the two procedures have in common? </P>' WHERE QID = 567
UPDATE Questions SET AlternateStem = '<P>A nurse assesses an elderly client in the outpatient clinic. What does the nurse expect the client to say?</P>' WHERE QID = 750
UPDATE Questions SET AlternateStem = '<P>The nurse identifies which of the following diets BEST meet the needs of a person with multiple wounds?<P>' WHERE QID = 751
UPDATE Questions SET AlternateStem = '<P>Spinal anesthesia requires important considerations.  Which statement describes an important consideration for the nurse?</P>' WHERE QID = 752
UPDATE Questions SET AlternateStem = '<P>On the first postoperative day, a patient develops a fever. The nurse auscultates crackles bilaterally in the lower lobes. The nurse understands which of the following complications of surgery is probably developing?<P>' WHERE QID = 753
UPDATE Questions SET AlternateStem = '<P>Ten hours after surgery, the nurse becomes concerned because the patient has <B>NOT</B> voided.  Which of the following actions by the nurse is <B>MOST</B> appropriate?</P>' WHERE QID = 754
UPDATE Questions SET AlternateStem = '<P>A man is six-feet tall.  What is the appropriate amount of calories for this man to consume on a daily basis?</P>' WHERE QID = 755
UPDATE Questions SET AlternateStem = '<P>The nurse understands the purpose of a drain in a wound is to<P>' WHERE QID = 756
UPDATE Questions SET AlternateStem = '<P>The nurse teaches correct body mechanics to a nurse’s aide.  Which of the following suggestions by the nurse is <B>MOST</B> appropriate?</P>' WHERE QID = 757
UPDATE Questions SET AlternateStem = '<P>The nurse observes a staff member prepare to leave the room of a patient on droplet precautions. The nurse should intervene if which of the following is observed?<P>' WHERE QID = 758
UPDATE Questions SET AlternateStem = '<P>A female patient had a left modified radical mastectomy. The patient is transferred from the recovery room to the surgical unit.  The nurse notices that the Hemovac drain is half filled with blood. What does the nurse do <B>FIRST</B>?</P>' WHERE QID = 759
UPDATE Questions SET AlternateStem = '<P>The nurse counsels a patient about how to maintain an adequate intake of protein. The nurse determines that further teaching is required if the patient chooses which of the following foods?<P>' WHERE QID = 760
UPDATE Questions SET AlternateStem = '<P>A 4-year-old child is about to have surgery.  A nurse plans care for the child.  Which of the following fears is <B>MOST</B> important for the nurse to consider?</P>' WHERE QID = 761
UPDATE Questions SET AlternateStem = '<P>In which of the following situations should the nurse consider withholding morphine until further assessment is completed?<P>' WHERE QID = 762
UPDATE Questions SET AlternateStem = '<P>In which of the following situations should the nurse consider withholding morphine until further assessment is completed?<P>' WHERE QID = 763
UPDATE Questions SET AlternateStem = '<P>Why is serum albumin used to identify malnutrition?</P>' WHERE QID = 764
UPDATE Questions SET AlternateStem = '<P>A 5-year-old is scheduled for a tonsillectomy and adenoidectomy. The child is given midazolam (Versed) preoperatively. What does the nurse understand is the purpose for giving this medication?</P>' WHERE QID = 765
UPDATE Questions SET AlternateStem = '<P>The nurse observes a staff member enter the patient''s room wearing a protective respiratory device. The nurse determines care is appropriate if the staff member is caring for which of the following patients?<P>' WHERE QID = 766
UPDATE Questions SET AlternateStem = '<P>The nurse knows that aspirin, if given in high, prolonged dosages, may precipitate which of the following physiological changes?<P>' WHERE QID = 767
UPDATE Questions SET AlternateStem = '<P>A patient is given wet-to-dry dressing changes.  After the nurse removes the first dry dressing, the patient yells at the nurse, "Ouch.  That really hurts. Are you sure you are doing it right?" Which of the following statements is the <B>BEST<B/> response by the nurse?</P>' WHERE QID = 768
UPDATE Questions SET AlternateStem = '<P>The nurse identifies that a 5-foot 6-inch tall woman''s diet is appropriate if the patient consumes how many calories?<P>' WHERE QID = 769
UPDATE Questions SET AlternateStem = '<P>A 53-year-old man is admitted to the hospital for hematuria. He has no previous history of illness.  He is married and has three children in high school. Which task of middle adulthood is <B>MOST</B> likely to be disturbed by a physical disability?</P>' WHERE QID = 770
UPDATE Questions SET AlternateStem = '<P>Some common foods cause eczema and should be removed from a person’s diet.  Which of these foods does the nurse understand to be the <B>MOST</B> likely cause of eczema?</P>' WHERE QID = 771
UPDATE Questions SET AlternateStem = '<P>A patient returns from abdominal surgery with an order for morphine sulfate IV q 3-4 hours prn for pain. During the first 24 hours after surgery, which of the following actions by the nurse is <B>BEST</B>?<P>' WHERE QID = 772
UPDATE Questions SET AlternateStem = '<P>A patient is treated for a wound infection.  A nurse provides routine care.  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 773
UPDATE Questions SET AlternateStem = '<P>The nurse explains to the patient the <B>MOST</B> vitamin C can be found in which of the following juices?<P>' WHERE QID = 774
UPDATE Questions SET AlternateStem = '<P>A nurse observes a staff member.  The nurse recognizes that the staff member uses standard precautions appropriately.  What does the nurse observe?</P>' WHERE QID = 775
UPDATE Questions SET AlternateStem = '<P>The nurse notes that an elderly patient has a reddened area on the coccyx. Which of the following actions should the nurse take <B>FIRST</B>?<P>' WHERE QID = 776
UPDATE Questions SET AlternateStem = '<P>The nurse identifies which of the following lab findings reflects the signs and symptoms of infection?<P>' WHERE QID = 777
UPDATE Questions SET AlternateStem = '<P>A nurse cares for a client in pain.  What is <B>MOST</B> important for the nurse to do?</P>' WHERE QID = 778
UPDATE Questions SET AlternateStem = '<P>The doctor orders cefaclor (Ceclor) 10 mg/kg q 12 h for a child who weighs 44 lb. The medication comes in an oral suspension that contains 250 mg/5 ml. How many ml does the nurse give for each dose?</P>' WHERE QID = 5710
UPDATE Questions SET AlternateStem = '<P>A young child diagnosed with autism is admitted to the pediatric unit with a tracheotomy after swallowing a small toy. The unlicensed assistive personnel reports to the nurse that the child does not maintain eye contact. Which of the following responses by the nurse is <B>BEST<B>?</P>' WHERE QID = 6211
UPDATE Questions SET AlternateStem = '<P>A 5 1/2-year-old child comes to the clinic for a routine exam. The parent reports that the child likes to jump and climb, questions everything, and is often observed interacting with an "imaginary" best friend. The nurse should advise the parent to take which of these actions?</P>' WHERE QID = 6212
UPDATE Questions SET AlternateStem = '<P>A four-week-old infant is brought to a health care provider by the parent because of vomiting and abdominal distention. The infant is diagnosed as having pyloric stenosis and is admitted to the hospital. The nurse should expect the infant''s emesis to have which of these qualities?</P>' WHERE QID = 6214
UPDATE Questions SET AlternateStem = '<P>An 18-month-old child drinks some drain cleaner and is brought to the emergency department. Which of the following pieces of equipment is <B>MOST</B> essential for the nurse to have on hand?</P>' WHERE QID = 6246
UPDATE Questions SET AlternateStem = '<P>The home care monitors a pediatric client diagnosed with a chronic seizure disorder. The nurse should intervene if which of the following is observed?</P>' WHERE QID = 6248
UPDATE Questions SET AlternateStem = '<P>The nurse observes a five-year-old child playing with several other children about the same age. The nurse identifies which of these play activities as the one in which the child is <B>MOST</B> likely to engage?</P>' WHERE QID = 6249
UPDATE Questions SET AlternateStem = '<P>The nurse performs a home care visit for a child diagnosed with cystic fibrosis. The nurse should intervene if which of the following is observed?</P>' WHERE QID = 6250
UPDATE Questions SET AlternateStem = '<P>The nurse observes a child walk up and down steps. The nurse notes the child has a steady gait and can use short sentences. The nurse estimates the child''s age to be which of the following?</P>' WHERE QID = 6251
UPDATE Questions SET AlternateStem = '<P>To prevent parent-child disturbances, the nurse should do which of the following?</P>' WHERE QID = 6252
UPDATE Questions SET AlternateStem = '<P>The school nurse assesses children enrolled in the kindergarten class. The nurse is <B>MOST</B> concerned if which of the following is observed?</P>' WHERE QID = 6253
UPDATE Questions SET AlternateStem = '<P>Which of the following measures should the nurse recognize as <B>MOST</B> important to promote maximum mobility in infants?</P>' WHERE QID = 6255
UPDATE Questions SET AlternateStem = '<P>Which of the following medications should the nurse have available for the treatment of acetaminophen overdose?</P>' WHERE QID = 6256
UPDATE Questions SET AlternateStem = '<P>A child is admitted with lead poisoning. Which of the following symptoms does the nurse expect to see?</P>' WHERE QID = 6257
UPDATE Questions SET AlternateStem = '<P>The nurse knows DTaP vaccine protects against which of the following diseases?</P>' WHERE QID = 6258
UPDATE Questions SET AlternateStem = '<P>A 6-month-old baby has a cyanotic congenital heart defect. The nurse knows that a cyanotic congenital heart defect is associated with which of the following symptoms?</P>' WHERE QID = 6259
UPDATE Questions SET AlternateStem = '<P>Which of the following guidelines is appropriate for the nurse to give a mother concerning the developmental age of her 7-year-old child?</P>' WHERE QID = 6260
UPDATE Questions SET AlternateStem = '<P>The nurse cares for a newborn diagnosed with developmental dysplasia of the hip (DDH). The nurse expects which of the following methods of treatment to be used for the newborn?</P>' WHERE QID = 6261
UPDATE Questions SET AlternateStem = '<P>A brace is ordered for an adolescent to correct a scoliosis deformity. Which of the following statements, if made by the parent to the nurse, indicates teaching is successful?</P>' WHERE QID = 6262
UPDATE Questions SET AlternateStem = '<P>The nurse performs assessments in the well-baby clinic. The nurse identifies which of the following is a warning sign of cerebral palsy (CP)?</P>' WHERE QID = 6263
UPDATE Questions SET AlternateStem = '<P>A woman delivers a healthy 8-lb, 2-oz infant. She mentions to the nurse that her baby''s "soft spot" seems very large. Which of the following statements, if made by the nurse, is <B>MOST</B> appropriate?</P>' WHERE QID = 6264
UPDATE Questions SET AlternateStem = '<P>A 1-week-old child is diagnosed with hemophilia A. Neither the mother nor the father has the disease. Which of the following statements by the nurse describes the hemophilia trait?</P>' WHERE QID = 6265
UPDATE Questions SET AlternateStem = '<P>A 7-year-old child is admitted to the hospital with a diagnosis of idiopathic hypopituitarism. Which of the following clinical manifestations is the nurse <B>MOST</B> likely to observe?</P>' WHERE QID = 6266
UPDATE Questions SET AlternateStem = '<P>A child with attention deficit hyperactive disorder (ADHD) is taking methylphenidate (Ritalin). The nurse knows that Ritalin is prescribed for this child for which of the following effects?</P>' WHERE QID = 6267
UPDATE Questions SET AlternateStem = '<P>When planning care for an infant diagnosed with a myelomeningocele, it is MOST important The nurse identifies which of the following principles of nursing care is <B>MOST</B> important to apply when caring for an infant with a myelomeningocele?</P>' WHERE QID = 6279
UPDATE Questions SET AlternateStem = '<P>The nurse cares for children in the well-child clinic. A parent brings a 6-month-old baby to the clinic for a check-up. The parent reports the baby had a check-up at 2 months of age and received the first DTaP. Which of the following actions by the nurse is <B>MOST</B> appropriate?</P>' WHERE QID = 6289
UPDATE Questions SET AlternateStem = '<P>The nurse knows MMR is a vaccine for which of the following diseases?</P>' WHERE QID = 6293
UPDATE Questions SET AlternateStem = '<P>Which of the following is the <B>BEST</B> way for the nurse to maintain an adequate fluid intake for a toddler with nausea, vomiting, and diarrhea?</P>' WHERE QID = 6316
UPDATE Questions SET AlternateStem = '<P>The home care nurse visits a 3-year-old child diagnosed at birth with phenylketonuria. The nurse assesses the child''s intake for the previous week. The nurse is <B>MOST</B> concerned if the child''s parent makes which of the following statements?</P>' WHERE QID = 6324
UPDATE Questions SET AlternateStem = '<P>The nurse instructs a 10-year-old client about how to collect a 24-hour urine specimen at home using a clean, empty jar. The nurse should recommend that the client use which of the following jars?</P>' WHERE QID = 6327
UPDATE Questions SET AlternateStem = '<P>The clinic nurse teaches a parent how to care for a child with impetigo. The nurse knows that the greatest danger associated with an impetigo infection is the risk of which of the following problems?</P>' WHERE QID = 6332


UPDATE AnswerChoices SET AlternateAText = 'A 13-year-old girl scheduled to have a wart removed from her nose.' WHERE QID = 70 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A 26-year-old man scheduled for the Whipple procedure due to cancer of the pancreas.' WHERE QID = 70 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A 42-year-old woman scheduled to have a benign cyst removed from the left breast.' WHERE QID = 70 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'An 80-year-old man scheduled for a colostomy due to severe diverticular disease.' WHERE QID = 70 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Privacy.' WHERE QID = 72 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Back rubs.' WHERE QID = 72 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Back rubs.' WHERE QID = 72 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Daytime activities.' WHERE QID = 72 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Side-lying.' WHERE QID = 76 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Prone.' WHERE QID = 76 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Supine with one pillow.' WHERE QID = 76 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'High Fowler''s.' WHERE QID = 76 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Ensure the operative permit is signed.' WHERE QID = 78 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Discuss the patient''s feelings about surgery.' WHERE QID = 78 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Raise the side rails of the bed.' WHERE QID = 78 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Tell the patient what to expect in the operating room.' WHERE QID = 78 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in skin moisture' WHERE QID = 83 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Walking with an assistive device' WHERE QID = 83 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Anemia' WHERE QID = 83 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Alzheimer''s disease' WHERE QID = 83 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient should increase the intake of protein.' WHERE QID = 91 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient should reduce the intake of fat.' WHERE QID = 91 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient should increase the caloric intake.' WHERE QID = 91 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient should decrease the fluid intake.' WHERE QID = 91 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'I should look for yellow-tinged complexion.' WHERE QID = 92 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'I will wear a Medic-Alert bracelet.' WHERE QID = 92 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'I should tell the physician if I have black stools.' WHERE QID = 92 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'I should consult the physician before taking any medication.' WHERE QID = 92 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Do not disturb the patient.' WHERE QID = 93 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Keep the environment cool and quiet for the patient.' WHERE QID = 93 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Provide activities that distract the patient for short periods of time.' WHERE QID = 93 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Find out if the medication is working for the patient.' WHERE QID = 93 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Tell the physician about the patient''s decision.' WHERE QID = 95 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Tell the patient that he has caused a delay in the operating room schedule.' WHERE QID = 95 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the patient to discuss his reasons for canceling the surgery.' WHERE QID = 95 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Ask the patient''s family to encourage the patient to have the surgery.' WHERE QID = 95 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Insert a catheter into the patient’s bladder.' WHERE QID = 754 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the patient to take sips of water.' WHERE QID = 754 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Inform the physician immediately about the patient.' WHERE QID = 754 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Palpate the patient’s bladder for distention.' WHERE QID = 754 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Contact the physician about the patient.' WHERE QID = 759 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Increase the rate of the IV fluids for the patient.' WHERE QID = 759 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Look at the recovery room record for the patient.' WHERE QID = 759 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Measure the patient''s output.' WHERE QID = 759 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Peanut butter on whole wheat bread.' WHERE QID = 760 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Rice and red beans.' WHERE QID = 760 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Orange juice and white toast.' WHERE QID = 760 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Spaghetti and meat sauce.' WHERE QID = 760 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Fear of losing independence' WHERE QID = 761 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Fear of losing control' WHERE QID = 761 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Fear of separation' WHERE QID = 761 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Fear of mutilation' WHERE QID = 761 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Patient complains of acute pain from deep partial thickness burn affecting the lower extremities.' WHERE QID = 763 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Patient''s blood pressure is 140/90, pulse 90, and respirations 28.' WHERE QID = 763 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Patient''s level of consciousness fluctuates from alert to lethargic.' WHERE QID = 763 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Patient exhibits restlessness, anxiety, and cold, clammy skin.' WHERE QID = 763 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Eggs are commonly eaten in the American diet, so the nurse can assume that albumin from eggs is consistently found in the American diet.' WHERE QID = 764 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Albumin is the first result on a protein electrophoresis, and the result is often included on the hospital chart.' WHERE QID = 764 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Serum albumin is easy to measure and can indicate a protein deficiency that is not detected on a physical examination.' WHERE QID = 764 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Serum albumin has a short half-life, so it is an easy protein to measure.' WHERE QID = 764 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"I know it hurts and I am really sorry to have to do it, but sometimes things have to hurt before they get better."' WHERE QID = 768 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I am peeling away the dead tissue. It hurts more the first time. Next time it will not hurt as much, I promise."' WHERE QID = 768 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Yes, I am doing it right. The dead tissue is supposed to stick to the dry dressing, but if I wet it a little bit, it won''t hurt so much."' WHERE QID = 768 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"This type of dressing cleans the wound so that it can heal. I''ll bring you some pain medication."' WHERE QID = 768 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Offer pain medication every 4 hours.' WHERE QID = 772 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Administer pain medication every 3 hours.' WHERE QID = 772 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Offer pain medication every 3 hours.' WHERE QID = 772 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Administer pain medication every 4 hours.' WHERE QID = 772 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The staff member wears gloves when she takes the blood pressure of a patient who is diagnosed with AIDS.' WHERE QID = 775 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The staff member wears a gown and gloves when she irrigates an abdominal wound.' WHERE QID = 775 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The staff member places contaminated linens in a leak-proof bag.' WHERE QID = 775 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The staff member removes her gloves after she bathes a patient and puts on a clean pair of gloves to bathe another patient.' WHERE QID = 775 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Continue assessment of the area.' WHERE QID = 776 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Reposition the patient every 1-2 hours.' WHERE QID = 776 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Massage the reddened area four times per day.' WHERE QID = 776 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Place the patient in a semireclining position.' WHERE QID = 776 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The symptoms of gout include tinnitus; the patient should increase the dose of ibuprofen.' WHERE QID = 309 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Gout causes problems with joints, not ears; the patient should consult an audiologist.' WHERE QID = 309 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient shows signs of toxicity from the medication; the patient should discontinue the medication until she contacts the physician.' WHERE QID = 309 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Unpleasant ear sounds are a complication of gout; the sounds will decrease with time.' WHERE QID = 309 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Cancer and diabetes' WHERE QID = 318 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Diabetes, arthritis, and GI bleeding' WHERE QID = 318 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Cancer, arthritis, and psoriasis' WHERE QID = 318 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Diabetes and alopecia' WHERE QID = 318 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I will limit the amount of canned soup, lunch meats, and cheese in my diet.”' WHERE QID = 321 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“I will decrease the amount of oranges, bananas, and apricots in my diet.”' WHERE QID = 321 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I will switch to a salt substitute instead of iodized salt in my diet.”' WHERE QID = 321 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I will NOT include as much broccoli, potatoes, and leafy green vegetables in my diet.”' WHERE QID = 321 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A client who is obese.' WHERE QID = 324 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A client who is dying.' WHERE QID = 324 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A client who has anorexia.' WHERE QID = 324 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A client who abuses alcohol.' WHERE QID = 324 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I will have a bitter taste in my mouth a short time after I put in the eye drops.”' WHERE QID = 336 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Although I have a reaction to sulfa drugs, I do <B>NOT</B> need to worry about eye drops.”' WHERE QID = 336 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I will carry the eye drops with me when I go on long day trips, so I can put them in at the correct time.”' WHERE QID = 336 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I will expect to use the eye drops for a long time to prevent permanent damage to my eyes.”' WHERE QID = 336 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“If you take sildenafil citrate (Viagra) on an empty stomach, it will decrease any side effects of nitroglycerin.”' WHERE QID = 339 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“If you take sildenafil citrate (Viagra) and any nitrate together, the resulting hypotension could be fatal.”' WHERE QID = 339 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Take sildenafil citrate (Viagra) two hours after you take the sustained-release nitroglycerin (Nitrocot).”' WHERE QID = 339 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“It does <B>NOT</B> matter when you take sildenafil citrate (Viagra) and sustained release nitroglycerin (Nitrocot).”' WHERE QID = 339 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Both Schedule I drugs and Schedule V drugs can be abused.' WHERE QID = 342 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Both Schedule I drugs and Schedule V drugs do <B>NOT</B> have a currently accepted medical use.' WHERE QID = 342 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Severe psychological or physical dependence can occur with both Schedule I drugs and Schedule V drugs.' WHERE QID = 342 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The prescriber can authorize limited refills of both Schedule I drugs and Schedule V drugs.' WHERE QID = 342 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A bed' WHERE QID = 351 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A chair' WHERE QID = 351 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A toilet' WHERE QID = 351 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A wheelchair' WHERE QID = 351 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypertension' WHERE QID = 356 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Decreased libido and impotence' WHERE QID = 356 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Depression' WHERE QID = 356 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Arthritic joints' WHERE QID = 356 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Watch for symptoms of overdose in the partner, such as a metallic taste in the mouth.' WHERE QID = 362 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Take the pulse of the partner and have the partner stop taking the medication if the pulse is less than 60 bpm.' WHERE QID = 362 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Take the blood pressure of the partner and have the partner stop taking the medication if the blood pressure is greater than 140/90 mm Hg.' WHERE QID = 362 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Prescribe metronidazole (Flagyl) for the young adult.' WHERE QID = 362 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The doctor will increase the dose of predisone (Cordrol) as quickly as needed so that the patient will get complete relief of symptoms.' WHERE QID = 365 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The dose of prednisone (Cordrol) must be increased and decreased gradually.' WHERE QID = 365 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'During the time that the patient takes the drug, the patient must eat foods that are high in protein, calcium, and vitamin D.' WHERE QID = 365 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient must take the medication on an empty stomach.' WHERE QID = 365 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Take the medication on an empty stomach to increase absorption.”' WHERE QID = 369 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Take aspirin for minor pain; do <B>NOT</B> take acetaminophen.”' WHERE QID = 369 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“You do <B>NOT</B> have to watch your diet: beer, wine, organ meats, gravy, and legumes are okay.”' WHERE QID = 369 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Drink at least eight glasses of water each day.”' WHERE QID = 369 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“This pill makes me nauseous sometimes.”' WHERE QID = 374 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“I take the pill at the same time every day.”' WHERE QID = 374 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“My contact lenses have started to bother me.”' WHERE QID = 374 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“My ankles and feet are swollen.”' WHERE QID = 374 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Intradermal injection' WHERE QID = 377 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Subcutaneous injection' WHERE QID = 377 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Intramuscular injection' WHERE QID = 377 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Z-track injection' WHERE QID = 377 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Stay close to the victim''s side with knees apart.' WHERE QID = 513 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Compress the victim’s chest at a rate of 100 compressions per minute.' WHERE QID = 513 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Give the victim 3 shocks before the nurse begins CPR.' WHERE QID = 513 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = ' Give the victim a rescue breath that lasts 30 seconds.' WHERE QID = 513 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Lift the back of the person''s neck.' WHERE QID = 514 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Use thumbs to move the person''s lower jaw backward.' WHERE QID = 514 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Turn the person''s head to one side.' WHERE QID = 514 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Tilt the person''s head back and lift the chin.' WHERE QID = 514 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Pulse 110, respirations 24, blood pressure 100/60' WHERE QID = 516 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Pulse 100, respirations 16, blood pressure 140/90' WHERE QID = 516 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Pulse 150, respirations 20, blood pressure 120/80' WHERE QID = 516 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Pulse 100, respirations 12, blood pressure 100/70' WHERE QID = 516 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Dilate the coronary arteries' WHERE QID = 517 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Increase the strength of the heart''s contractions' WHERE QID = 517 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Prevent premature ventricular contractions' WHERE QID = 517 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Increase the rate of myocardial contractions' WHERE QID = 517 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'At 6 a.m' WHERE QID = 518 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'At breakfast time' WHERE QID = 518 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'At dinner time' WHERE QID = 518 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'At bedtime' WHERE QID = 518 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Cardiac tamponade' WHERE QID = 519 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 519 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Myocardial infarction' WHERE QID = 519 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Digitalis toxicity' WHERE QID = 519 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"My clothes are too tight."' WHERE QID = 520 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I sleep with only one pillow."' WHERE QID = 520 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"I am really worried about going back to work."' WHERE QID = 520 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"I empty my bladder less often than I used to."' WHERE QID = 520 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 521 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hyperkalemia' WHERE QID = 521 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hyponatremia' WHERE QID = 521 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Oliguria' WHERE QID = 521 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Nonpitting' WHERE QID = 522 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Dependent' WHERE QID = 522 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Painful' WHERE QID = 522 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Severe' WHERE QID = 522 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"If I have chest pain, I will contact my doctor immediately."' WHERE QID = 523 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"If I have chest pain, I will stop my activity and take a nitroglycerin tablet right away."' WHERE QID = 523 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"If chest pain does NOT decrease in 30 minutes, I will take another nitroglycerin tablet."' WHERE QID = 523 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"If I have chest pain, I will rest for 30 minutes and after 30 minutes, I will take a nitroglycerin tablet."' WHERE QID = 523 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Palpitations, hypertension, and tachycardia' WHERE QID = 524 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Flushing, bradycardia, and muscle weakness' WHERE QID = 524 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Dizziness, headache, and hypotension' WHERE QID = 524 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Flushing, vertigo, and seizures' WHERE QID = 524 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Pain after sexual activity' WHERE QID = 525 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A headache after the patient takes nitroglycerin' WHERE QID = 525 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A change in the character of the pain' WHERE QID = 525 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Pain after the patient eats a large meal' WHERE QID = 525 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'To monitor the patient''s condition closely without having to wake up the patient' WHERE QID = 526 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'To prevent another, more serious heart attack from happening' WHERE QID = 526 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'To confirm the diagnosis of acute myocardial infarction' WHERE QID = 526 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'To note any changes in the heart rhythm that are life-threatening' WHERE QID = 526 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Continue to monitor the patient.' WHERE QID = 527 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the patient to rest more.' WHERE QID = 527 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Check the patient for any edema or weight gain.' WHERE QID = 527 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Administer high-flow oxygen to the patient.' WHERE QID = 527 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient will return to the level of pre-illness activity.' WHERE QID = 528 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient will achieve the optimum level of health.' WHERE QID = 528 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient will be free from pain and dysrhythmias.' WHERE QID = 528 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient will get rid of all stress from his lifestyle.' WHERE QID = 528 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Facilitate accurate cardiac monitoring' WHERE QID = 529 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Promote a restful atmosphere' WHERE QID = 529 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Decrease the workload on the heart' WHERE QID = 529 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Allow regeneration of the myocardium' WHERE QID = 529 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Ham and cheese sandwich, milk, apple' WHERE QID = 531 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Sliced turkey, green beans, pear' WHERE QID = 531 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Broiled fish, creamed spinach, custard' WHERE QID = 531 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Broiled chicken, green beans, ice cream' WHERE QID = 531 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Take the patient''s vital signs.' WHERE QID = 532 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Contact the doctor.' WHERE QID = 532 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Apply pressure to the site.' WHERE QID = 532 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Reinforce the dressing over the site' WHERE QID = 532 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Take a 12-lead EKG of the patient.' WHERE QID = 533 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Give analgesia to the patient, as ordered.' WHERE QID = 533 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Give anticoagulants to the patient, as ordered.' WHERE QID = 533 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Give the patient a brief orientation to the unit.' WHERE QID = 533 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Serum CK, troponin T and I, and myoglobin' WHERE QID = 534 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'BUN, serum creatinine, and protein-bound iodine' WHERE QID = 534 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Serum AST (SGOT), RBC, and platelets' WHERE QID = 534 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Serum LDH, thyroxine, and endorphin' WHERE QID = 534 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"The stress test assesses your overall physical fitness."' WHERE QID = 535 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"The stress test determines how much stress your heart can handle."' WHERE QID = 535 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"The stress test determines if your peripheral circulation is adequate."' WHERE QID = 535 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"The stress test helps the doctor to evaluate your cardiac output."' WHERE QID = 535 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Insufficient oxygen in the heart muscles' WHERE QID = 536 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Inflammation of the pericardium' WHERE QID = 536 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Ineffective contractions of the heart muscles' WHERE QID = 536 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Severe dysrhythmias' WHERE QID = 536 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Monitor the patient''s temperature.</P>' WHERE QID = 538 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Observe the patient for dysrhythmias.' WHERE QID = 538 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Check the patient’s extremities for pulses.' WHERE QID = 538 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the patient to cough and breathe deeply.' WHERE QID = 538 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"I will take the medication at the same time every day."' WHERE QID = 543 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I will stop taking the medication when my blood pressure goes down."' WHERE QID = 543 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"The physician will check my blood pressure and may need to change the medication."' WHERE QID = 543 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"When I first start taking the medication, I may feel sleepy."' WHERE QID = 543 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '160/110' WHERE QID = 544 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '142/88' WHERE QID = 544 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '130/88' WHERE QID = 544 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '126/80' WHERE QID = 544 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = ' "I can tell when my blood pressure is high."' WHERE QID = 545 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = ' "Hypertension is caused by eating too much salt."' WHERE QID = 545 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = ' "I can stop taking the medication when my blood pressure goes down."' WHERE QID = 545 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = ' "I know that I have to see my doctor regularly."' WHERE QID = 545 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“A headache is <B>NOT</B> related to the medication; you should report the headache to your doctor.”' WHERE QID = 546 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“This medication often causes headaches.”' WHERE QID = 546 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Stop the medication until the headache is gone.”' WHERE QID = 546 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Go immediately to the emergency room.”' WHERE QID = 546 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '120/78' WHERE QID = 547 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '180/90' WHERE QID = 547 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '190/100' WHERE QID = 547 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '170/110' WHERE QID = 547 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Clammy skin' WHERE QID = 548 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Poor skin turgor' WHERE QID = 548 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Shortness of breath' WHERE QID = 548 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Engorged neck veins' WHERE QID = 548 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s urinary output' WHERE QID = 549 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s pre-shock blood pressure' WHERE QID = 549 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s weight in kilograms' WHERE QID = 549 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s mental status' WHERE QID = 549 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Blood volume' WHERE QID = 550 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'White blood cell count' WHERE QID = 550 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Aerobic exercise' WHERE QID = 550 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Effective respiration' WHERE QID = 550 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Raise the foot of the bed with the knee gatch and pillow.' WHERE QID = 551 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Apply Ace bandages from the patient’s ankle to thigh.' WHERE QID = 551 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Teach the patient to flex and point his toes every two hours.' WHERE QID = 551 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Massage the patient''s legs, except for the calf area, several times a day.' WHERE QID = 551 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Protamine sulfate' WHERE QID = 554 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Calcium' WHERE QID = 554 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Imferon' WHERE QID = 554 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Vitamin K' WHERE QID = 554 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Cold, mottled leg' WHERE QID = 555 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Strong popliteal pulse' WHERE QID = 555 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Edematous leg' WHERE QID = 555 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hot, reddened leg' WHERE QID = 555 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Sitting up in straight-back chair' WHERE QID = 557 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'High Fowler''s with knee gatched' WHERE QID = 557 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Full supine' WHERE QID = 557 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Flat prone' WHERE QID = 557 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = ' The patient drinks socially.' WHERE QID = 558 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = ' The patient walks two miles a day.' WHERE QID = 558 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = ' The patient takes vitamins every day.' WHERE QID = 558 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = ' The patient is a heavy smoker.' WHERE QID = 558 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'It is due to venous insufficiency.' WHERE QID = 559 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'It is pain that is caused by the cold.' WHERE QID = 559 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'It is pain that is caused by walking.' WHERE QID = 559 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'It is experienced only by the elderly.' WHERE QID = 559 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Atrioventricular (AV) node' WHERE QID = 563 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Purkinje fibers' WHERE QID = 563 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Bundle of His fibers' WHERE QID = 563 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Sinoatrial (SA) node' WHERE QID = 563 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"Encourage the child to play more often with other children."' WHERE QID = 6212 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Tell the child that the playmate is not real."' WHERE QID = 6212 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Allow the child to engage in imaginary play."' WHERE QID = 6212 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Never leave the child alone."' WHERE QID = 6212 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"6 months."' WHERE QID = 6251 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"12 months."' WHERE QID = 6251 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"14 months."' WHERE QID = 6251 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"24 months."' WHERE QID = 6251 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A child throws and catches a ball.' WHERE QID = 6253 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A child is able to neatly tie shoelaces.' WHERE QID = 6253 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A child eats with fingers.' WHERE QID = 6253 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A child walks down stairs by placing both feet on one step.' WHERE QID = 6253 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Encouraging daily exercise.' WHERE QID = 6255 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Using a playpen whenever possible.' WHERE QID = 6255 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Providing a safe play area.' WHERE QID = 6255 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Teaching noncompetitive activities.' WHERE QID = 6255 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Vitamin K.' WHERE QID = 6256 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Mucomyst.' WHERE QID = 6256 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Aspirin.' WHERE QID = 6256 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Narcan' WHERE QID = 6256 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Anemia, hearing impairment, and distractibility.' WHERE QID = 6257 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Tinnitus, confusion, hyperthermia.' WHERE QID = 6257 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Polycythemia, hypoactivity, impaired liver functioning.' WHERE QID = 6257 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Shortness of breath, dependent edema, bounding pulse.' WHERE QID = 6257 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The child''s periods of shyness should be tolerated.' WHERE QID = 6260 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Nightmares are not characteristic of this age and the reasons for their occurrence should be investigated.' WHERE QID = 6260 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The child''s participation in groups or clubs should be encouraged.' WHERE QID = 6260 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Severe punishment may be necessary for acts of independence.' WHERE QID = 6260 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Pavlik harness.' WHERE QID = 6261 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Double diapering.' WHERE QID = 6261 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Placing a small pillow between the legs.' WHERE QID = 6261 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Bracing the affected leg.' WHERE QID = 6261 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A bed board may replace the brace at night.' WHERE QID = 6262 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'My child''s diet should be low in calories.' WHERE QID = 6262 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Daily tub baths are preferred to showers.' WHERE QID = 6262 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The brace should be worn 23 hours a day.' WHERE QID = 6262 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The infant has poor head control after 3 months.' WHERE QID = 6263 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The infant sits with support by 8 months.' WHERE QID = 6263 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The infant uses arms and legs to crawl across the room.' WHERE QID = 6263 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The infant smiles at the mother by 3 months.' WHERE QID = 6263 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"Both of the baby''s fontanels should close within 1 month."' WHERE QID = 6264 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"The baby could be brain damaged if the soft spot is injured."' WHERE QID = 6264 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"The baby''s posterior fontanel should close after 1 year."' WHERE QID = 6264 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"The baby''s anterior soft spot" will remain for approximately 1 1/2 years.""' WHERE QID = 6264 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Abnormal body proportions.' WHERE QID = 6266 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Early sexual maturation.' WHERE QID = 6266 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Delicate features.' WHERE QID = 6266 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Coarse, dry skin.' WHERE QID = 6266 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Measles, mumps, rubeola.' WHERE QID = 6293 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Measles, mumps, roseola.' WHERE QID = 6293 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Measles, mumps, rubella.' WHERE QID = 6293 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Measles, mumps, chickenpox.' WHERE QID = 6293 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Find out what the previous reading was of the central venous pressure.' WHERE QID = 36 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Place the manometer at the level of the right atrium.' WHERE QID = 36 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Position the patient in an upright position for the reading.' WHERE QID = 36 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Teach the patient to hold her breath during the reading.' WHERE QID = 36 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Specific gravity 1.020' WHERE QID = 35 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Specific gravity 1.034' WHERE QID = 35 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Potassium 5.8 mEq/L' WHERE QID = 35 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Potassium 4.8 mEq/L' WHERE QID = 35 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '0 cm' WHERE QID = 38 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '3 cm' WHERE QID = 38 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '8 cm' WHERE QID = 38 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '15 cm' WHERE QID = 38 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Cyanosis' WHERE QID = 37 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Diarrhea' WHERE QID = 37 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Edema' WHERE QID = 37 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Shock' WHERE QID = 37 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypernatremia' WHERE QID = 43 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hyperkalemia' WHERE QID = 43 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hyponatremia' WHERE QID = 43 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 43 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypernatremia' WHERE QID = 44 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hyperkalemia' WHERE QID = 44 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hyponatremia' WHERE QID = 44 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 44 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient’s blood pressure is 130/80.' WHERE QID = 46 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient complains of shortness of breath.' WHERE QID = 46 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient complains of pruritus' WHERE QID = 46 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient has hematuria.' WHERE QID = 46 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Fluid overload' WHERE QID = 48 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Pneumothorax' WHERE QID = 48 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 48 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Pneumonia' WHERE QID = 48 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Uses a 24-gauge catheter to start the IV' WHERE QID = 49 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Marks the time on the IV bag with a permanent marker' WHERE QID = 49 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Inserts the catheter at a 10 degree angle' WHERE QID = 49 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Sets the flow rate at 100 ml per hour' WHERE QID = 49 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Reposition the patient''s arm.' WHERE QID = 50 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Check the site of the IV.' WHERE QID = 50 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Raise the solution.' WHERE QID = 50 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Flush the tubing.' WHERE QID = 50 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Decreased pulse rate, increased BP, and decreased respirations' WHERE QID = 51 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Increased pulse rate, increased BP, and increased respirations' WHERE QID = 51 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Increased pulse rate, increased BP, and decreased respirations' WHERE QID = 51 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Decreased pulse rate, decreased BP, and increased respirations' WHERE QID = 51 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'I should choose an exercise that suits my lifestyle.' WHERE QID = 68 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'I should incorporate exercise into my daily routine.' WHERE QID = 68 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'I should make a commitment to exercise regularly.' WHERE QID = 68 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'I should start by running 5 miles every day.' WHERE QID = 68 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Engage in sedentary activity.' WHERE QID = 80 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Increase dietary bulk.' WHERE QID = 80 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Decrease fluid intake.' WHERE QID = 80 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Use oral laxatives.' WHERE QID = 80 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Nausea and abdominal distention.' WHERE QID = 82 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Back pain and hematuria.' WHERE QID = 82 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Chest pain and shortness of breath.' WHERE QID = 82 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Similar findings in the right arm.' WHERE QID = 82 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Administer tetanus toxoid.' WHERE QID = 84 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Determine how many Td immunizations the client has received.' WHERE QID = 84 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Administer tetanus immune globulin (TIG).' WHERE QID = 84 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Monitor for lockjaw.' WHERE QID = 84 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Absence of respiratory distress.' WHERE QID = 86 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Absence of respiratory distress.' WHERE QID = 86 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The marking on the tube designating the correct length remains visible just outside the nares.' WHERE QID = 86 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The tube is securely taped.' WHERE QID = 86 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'has a prolonged action.' WHERE QID = 94 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'is never given for prolonged periods of time.' WHERE QID = 94 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'must be given several times a day to be effective.' WHERE QID = 94 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'can only be given parenterally.' WHERE QID = 94 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Heart failure.' WHERE QID = 753 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Thrombophlebitis.' WHERE QID = 753 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Pulmonary embolism.' WHERE QID = 753 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Atelectasis.' WHERE QID = 753 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'keep the tissues close together so that healing can occur.' WHERE QID = 756 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'prevent infection by providing a means for bacteria to escape.' WHERE QID = 756 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'evaluate the effectiveness of hemostasis.' WHERE QID = 756 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'create a space that will facilitate reconstructive surgery at a later date.' WHERE QID = 756 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The staff member removes the gloves by pulling off inside out.' WHERE QID = 758 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The staff member holds onto the outer surface of the facemask while pulling mask away from face.' WHERE QID = 758 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The staff member unties the gown and removes it without touching the outside of the gown.' WHERE QID = 758 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The nurse performs hand hygiene for 15 seconds.' WHERE QID = 758 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Patient complains of acute pain from deep partial thickness burn affecting the lower extremities.' WHERE QID = 762 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Patient''s blood pressure is 140/90, pulse 90, and respirations 28.' WHERE QID = 762 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Patient''s level of consciousness fluctuates from alert to lethargic.' WHERE QID = 762 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Patient exhibits restlessness, anxiety, and cold, clammy skin.' WHERE QID = 762 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Urinary frequency.' WHERE QID = 767 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hypoventilation.' WHERE QID = 767 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'GI bleeding.' WHERE QID = 767 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hemoconcentration.' WHERE QID = 767 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '2,500 calories per day.' WHERE QID = 769 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '1,900 calories per day.' WHERE QID = 769 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '1,500 calories per day.' WHERE QID = 769 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '1,300 calories per day.' WHERE QID = 769 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Canned apple juice.' WHERE QID = 774 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Canned tomato juice.' WHERE QID = 774 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Frozen grapefruit juice.' WHERE QID = 774 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Fresh orange juice.' WHERE QID = 774 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'To establish a route for the nurse to give medications quickly' WHERE QID = 67 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'To avoid the nurse having to insert the IV on the morning of surgery' WHERE QID = 67 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'To decrease the client''s desire to have fluids by mouth before surgery' WHERE QID = 67 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'To make sure the client is sufficiently hydrated during surgery' WHERE QID = 67 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Tell me about your sleep patterns since you began working as a security guard.”' WHERE QID = 69 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“You probably sleep during your night shift when you can.”' WHERE QID = 69 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“It is normal for people of your age to have difficulty sleeping.”' WHERE QID = 69 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Working the night shift disrupts normal sleep patterns.”' WHERE QID = 69 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'a chronic autoimmune reaction' WHERE QID = 71 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'an acute infectious disease' WHERE QID = 71 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'a viral disease' WHERE QID = 71 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'a cystic disease that is self-limiting' WHERE QID = 71 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'One-half pound per day' WHERE QID = 73 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'One-half pound per week' WHERE QID = 73 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '1 pound per week' WHERE QID = 73 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '1 pound per day' WHERE QID = 73 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'They eat a small amount of food with less bulk.' WHERE QID = 77 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'They are less active and have less muscle tone.' WHERE QID = 77 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'There are neurological changes in their gastrointestinal tract.' WHERE QID = 77 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'There is less sensation in their gastrointestinal tract.' WHERE QID = 77 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Partial thromboplastin time' WHERE QID = 87 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Prothrombin time' WHERE QID = 87 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Bleeding time' WHERE QID = 87 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Protein electrophoresis' WHERE QID = 87 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I seem to get fewer upper respiratory infections than I did before.”' WHERE QID = 750 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“I think that I am a little taller than I was before.”' WHERE QID = 750 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I do not enjoy eating anymore.”' WHERE QID = 750 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I sleep with fewer blankets now.”' WHERE QID = 750 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Partial paralysis is a serious but frequent complication.' WHERE QID = 752 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The nurse must protect the patient from injury since the patient’s sensation is impaired.' WHERE QID = 752 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient should try to walk as soon as possible.' WHERE QID = 752 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The nurse can prevent spinal headache in the patient by restricting the patient’s intake of oral and intravenous fluids.' WHERE QID = 752 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '3,000 calories per day' WHERE QID = 755 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '2,300 calories per day' WHERE QID = 755 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '2,000 calories per day' WHERE QID = 755 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '1,800 calories per day' WHERE QID = 755 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Bend at the waist when you lift objects.' WHERE QID = 757 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Lift objects with your arms extended.' WHERE QID = 757 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Bend your knees when you lift objects.' WHERE QID = 757 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Lean forward when you lift objects.' WHERE QID = 757 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'To decrease the gag reflex' WHERE QID = 765 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'To sedate the patient and reduce anxiety' WHERE QID = 765 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'To promote healing of the wound' WHERE QID = 765 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'To promote vasoconstriction of the mucous membranes' WHERE QID = 765 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Helping his children to grow into adulthood' WHERE QID = 770 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Coping with a role transition' WHERE QID = 770 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Renewing earlier relationships' WHERE QID = 770 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Developing adult leisure time activities' WHERE QID = 770 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Fish, nuts, chocolate' WHERE QID = 771 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Strawberries, tomato, apples' WHERE QID = 771 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Milk, wheat, egg whites' WHERE QID = 771 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Soybeans, orange juice, egg yolks' WHERE QID = 771 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Check and record the patient''s temperature.' WHERE QID = 773 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Send samples of wound drainage for culture.' WHERE QID = 773 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Assess the perfusion in the area of the wound.' WHERE QID = 773 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Evaluate the results of the blood culture.' WHERE QID = 773 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Teach the patient about pain.' WHERE QID = 778 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Establish a trusting relationship with the patient.' WHERE QID = 778 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Find out if relaxation techniques decrease the pain.' WHERE QID = 778 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Give the patient pain medication.' WHERE QID = 778 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"If we can help the patient to realize how much the family worries, perhaps the patient will get better."' WHERE QID = 415 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Sometimes it is difficult to see how anxious a patient really is."' WHERE QID = 415 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Perhaps the patient''s family has caused the patient pain."' WHERE QID = 415 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Being critical of the patient is <B>NOT</B> going to help the patient improve."' WHERE QID = 415 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Photophobia' WHERE QID = 426 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Dizziness' WHERE QID = 426 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Epistaxis' WHERE QID = 426 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hypertensive crisis' WHERE QID = 426 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Identify stress factors in the client’s environment.' WHERE QID = 432 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Avoid giving the client choices to make.' WHERE QID = 432 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the client to become angry.' WHERE QID = 432 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Avoid discussing the client''s symptoms.' WHERE QID = 432 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient looks for attention from others to make up for the loss of her leg.' WHERE QID = 444 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient places the blame for her difficulties on others.' WHERE QID = 444 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has difficulty accepting her new body image.' WHERE QID = 444 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient feels alienated by the hospital staff.' WHERE QID = 444 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Diuretics' WHERE QID = 449 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Monoamine oxidase inhibitors' WHERE QID = 449 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Tricyclic antidepressants' WHERE QID = 449 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Antibiotics' WHERE QID = 449 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient is strapped to a table and is irradiated by a cobalt scanner.' WHERE QID = 79 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient stands in front of a large machine that takes x-ray pictures of the liver.' WHERE QID = 79 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient lies still while a scanning probe is passed back and forth over the body.' WHERE QID = 79 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s skin is rubbed with oil and ultrasound pictures are taken.' WHERE QID = 79 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Asepsis' WHERE QID = 6279 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Exercise' WHERE QID = 6279 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hygiene' WHERE QID = 6279 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Rest' WHERE QID = 6279 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Black in appearance.' WHERE QID = 6214 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Diminish after feedings.' WHERE QID = 6214 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Be projectile.' WHERE QID = 6214 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Be accompanied by diarrhea.' WHERE QID = 6214 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Playing independently, but with the same toy as another child.' WHERE QID = 6249 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Playing with a toy telephone and imitating the doctor.' WHERE QID = 6249 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Playing doctor with another child.' WHERE QID = 6249 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Playing with a doctor doll.' WHERE QID = 6249 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The child eats a high-protein, high-calorie diet.' WHERE QID = 6250 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The child has two to three stools per day.' WHERE QID = 6250 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The child swallows the pancreatic enzyme capsules whole.' WHERE QID = 6250 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The child takes the pancreatic enzymes one hour after eating.' WHERE QID = 6250 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Report potential abuse to the appropriate authorities.' WHERE QID = 6252 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Discuss with the parents any problems or fears about childrearing that they may have.' WHERE QID = 6252 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Tell the parents that if their attitudes don''t change, action will have to be taken by the medical staff.' WHERE QID = 6252 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Suggest to the parents that they have the child stay with a relative until they feel confident in their roles.' WHERE QID = 6252 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Keep the patient NPO and give hypotonic solutions intravenously.' WHERE QID = 6316 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Force fluids and give hypertonic solutions intravenously.' WHERE QID = 6316 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Provide Jell-O and Popsicles to increase fluid intake.' WHERE QID = 6316 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Offer oral rehydration solutions (ORS) to rehydrate patient.' WHERE QID = 6316 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Infecting a potentially large group of people.' WHERE QID = 6332 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Developing glomerulonephritis secondary to streptococcus infection.' WHERE QID = 6332 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Acquiring a superinfection.' WHERE QID = 6332 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Progressive tissue necrosis and gangrene.' WHERE QID = 6332 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'It is an X-linked recessive trait found primarily in females.' WHERE QID = 6265 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'It is an X-linked dominant trait found primarily in females.' WHERE QID = 6265 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'I is an X-linked recessive trait found primarily in males.' WHERE QID = 6265 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'It is an X-linked dominant trait found primarily in males.' WHERE QID = 6265 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A patient diagnosed with varicella.' WHERE QID = 766 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A patient diagnosed with mumps.' WHERE QID = 766 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A patient diagnosed with vancomycin-resistant Enterococcus (VRE).' WHERE QID = 766 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A patient diagnosed with pneumonia.' WHERE QID = 766 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"You are correct that taking any medication any time during pregnancy is dangerous."' WHERE QID = 315 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Depending on the drug and the trimester of pregnancy, some medications are safe for the fetus."' WHERE QID = 315 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Taking medication will not affect the fetus because the amniotic fluid provides a protective barrier."' WHERE QID = 315 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Taking oral medications is acceptable because oral medications are metabolized in the mother''s stomach, not in her uterus."' WHERE QID = 315 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Take gabapentin (Neurontin) with an antacid to help you swallow the capsule.”' WHERE QID = 333 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Crush the capsule and mix the medication with applesauce or juice.”' WHERE QID = 333 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Chew the capsule so that the medication will take effect faster.”' WHERE QID = 333 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Open the capsule and sprinkle the medication in applesauce or juice.”' WHERE QID = 333 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s ears' WHERE QID = 345 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s eyes' WHERE QID = 345 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s hairline' WHERE QID = 345 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s tongue' WHERE QID = 345 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '12 noon' WHERE QID = 348 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '12:15 p.m. to 1:15 p.m.' WHERE QID = 348 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '5:45 p.m. to 7:45 p.m.' WHERE QID = 348 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '7:45 p.m. to 11:45 p.m.' WHERE QID = 348 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Trihexyphenidyl (Artane)' WHERE QID = 359 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Meclizine (Antivert)' WHERE QID = 359 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Fexofenadine (Allegra)' WHERE QID = 359 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Donepezil (Aricept)' WHERE QID = 359 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Vasopressin (Pitressin)' WHERE QID = 382 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Potassium iodide (SSKI)' WHERE QID = 382 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Propylthiouracil (PTU)' WHERE QID = 382 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Levothyroxine sodium (Synthroid)' WHERE QID = 382 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The husband''s unemployment is a significant potential stressor for the patient.' WHERE QID = 388 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The husband''s unemployment is unrelated to the patient’s depression.' WHERE QID = 388 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Unemployment is mainly a factor in development crises.' WHERE QID = 388 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient is using her husband''s unemployment to avoid her own problems.' WHERE QID = 388 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Why did you do this to yourself?”' WHERE QID = 391 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Can you tell me what is bothering you?”' WHERE QID = 391 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Exactly what medication and how much did you take, and when did you take the medication?”' WHERE QID = 391 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Did you seriously think of killing yourself?”' WHERE QID = 391 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Increase a sense of responsibility in the client' WHERE QID = 393 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Increase independence in the client' WHERE QID = 393 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Promote trust in the client' WHERE QID = 393 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Promote rest in the client' WHERE QID = 393 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“When did you first notice that you were feeling anxious?”' WHERE QID = 395 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Have you shared this information with a loved one?”' WHERE QID = 395 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Are you worried about having to visit the doctor?”' WHERE QID = 395 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Do you want to discuss your concerns with me?”' WHERE QID = 395 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '6-12 hours after the patient’s last drink' WHERE QID = 396 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '12-18 hours after the patient’s last drink' WHERE QID = 396 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '48-72 hours after the patients’ last drink' WHERE QID = 396 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '4 days after the patient’s last drink' WHERE QID = 396 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Promote rest and relaxation in the client.' WHERE QID = 398 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the client to participate in planning her care.' WHERE QID = 398 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the client to accept help from others.' WHERE QID = 398 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Set limits on too many activities of the client.' WHERE QID = 398 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"Perhaps we can make that change for your next appointment."' WHERE QID = 403 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Is there something you are having trouble discussing?"' WHERE QID = 403 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"I will have to discuss any changes in the time of your appointment with the health-care team first."' WHERE QID = 403 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Are you having difficulty with the time of your appointment that you agreed to?"' WHERE QID = 403 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Use small talk to keep the conversation going.' WHERE QID = 404 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Ask the patient why she is having difficulty talking.' WHERE QID = 404 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Ask direct, concrete questions that require simple answers.' WHERE QID = 404 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Use broad openings and leads to encourage discussion.' WHERE QID = 404 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Adolescent males are <B>MOST</B> affected by anorexia nervosa.' WHERE QID = 405 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '5-20% of anorexia patients die.' WHERE QID = 405 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Anorexia patients see themselves as very thin.' WHERE QID = 405 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Anorexia patients are self-indulgent.' WHERE QID = 405 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient has difficulty making decisions.' WHERE QID = 406 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient expects too much of herself.' WHERE QID = 406 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient tries to please others at all cost.' WHERE QID = 406 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient can <B>NOT</B> tolerate anxiety.' WHERE QID = 406 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Sit in the rocking chair and hold your baby.”' WHERE QID = 408 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“We feed the infants every four hours.”' WHERE QID = 408 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I''ll hold your baby while you look at her.”' WHERE QID = 408 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Watch the nurse give your baby a bath.”' WHERE QID = 408 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Young children have difficulty verbalizing their emotions.”' WHERE QID = 410 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Children hesitate to confide in anyone but their parents.”' WHERE QID = 410 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Play is an enjoyable form of therapy for children.”' WHERE QID = 410 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“Play therapy is helpful in preventing regression in children.”' WHERE QID = 410 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Correct the information in the patient’s story.' WHERE QID = 414 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Do <B>NOT</B> interfere; allow the patient to continue the story.' WHERE QID = 414 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Refer the patient for reminiscence therapy.' WHERE QID = 414 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Orient the patient to person, place, and time.' WHERE QID = 414 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient will be less isolated from the group.' WHERE QID = 416 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient will have increased insight into the group’s behavior.' WHERE QID = 416 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient will be more isolated from the group.' WHERE QID = 416 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient will participate more in the group.' WHERE QID = 416 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Love and belonging' WHERE QID = 417 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Esteem and recognition' WHERE QID = 417 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Safety and security' WHERE QID = 417 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Self-actualization' WHERE QID = 417 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The client is afraid of falling.' WHERE QID = 413 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The client is in a regressive phase.' WHERE QID = 413 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The client wants to maintain her independence.' WHERE QID = 413 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The client is angry at the nurse because the nurse tries to interfere.' WHERE QID = 413 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Point out how the patient’s behavior affects others.' WHERE QID = 418 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Try to distract and redirect the patient.' WHERE QID = 418 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Encourage the patient to express himself.' WHERE QID = 418 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Provide opportunities for the patient to socialize.' WHERE QID = 418 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Euphoric' WHERE QID = 419 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Ambivalent' WHERE QID = 419 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Incoherent' WHERE QID = 419 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Disoriented' WHERE QID = 419 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The alcoholic person enjoys the feeling of being intoxicated.' WHERE QID = 420 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The alcoholic person uses alcohol to escape from problems.' WHERE QID = 420 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The alcoholic person has a greater tolerance for alcohol than most people.' WHERE QID = 420 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The alcoholic person performs tasks more efficiently when drinking.' WHERE QID = 420 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient appears calm about the paralysis of his arm.' WHERE QID = 421 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient expresses anxiety about permanent damage in his arm.' WHERE QID = 421 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient’s arm improves with passive arm exercises.' WHERE QID = 421 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient recognizes that the symptoms of paralysis are <B>NOT</B> real.' WHERE QID = 421 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Tell the client that this is a sad time.' WHERE QID = 422 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Quietly leave the room.' WHERE QID = 422 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Call the chaplain or other spiritual leader at the hospital.' WHERE QID = 422 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Talk about what the client can do in the time the client has left.' WHERE QID = 422 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A setting that is structured but permissive' WHERE QID = 423 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'An environment that increases the opportunity for the patient to test reality' WHERE QID = 423 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A setting that is structured and <B>NOT</B> permissive' WHERE QID = 423 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'An environment that reduces stimuli and redirects the patient’s behavior' WHERE QID = 423 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Tell the patient that he has no control over his behavior.' WHERE QID = 424 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Point out to the patient that he is making others anxious.' WHERE QID = 424 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Identify yourself as a nurse to the patient and remain calm.' WHERE QID = 424 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Touch the patient gently to reassure the patient.' WHERE QID = 424 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Sublimation' WHERE QID = 425 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Projection' WHERE QID = 425 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Undoing' WHERE QID = 425 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Rationalization' WHERE QID = 425 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“We will do everything that we can to help your wife.”' WHERE QID = 427 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“This is an upsetting experience for you and your wife.”' WHERE QID = 427 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“You will feel relieved once the surgery is over.”' WHERE QID = 427 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I think it will help you if we discussed your wife''s surgery.”' WHERE QID = 427 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A projection of the client’s anxiety onto a neutral object' WHERE QID = 429 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A postoperative phenomenon that is common in females' WHERE QID = 429 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'An attempt to undo the client’s traumatic hospital experience' WHERE QID = 429 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A conversion reaction by the client to emotional stress' WHERE QID = 429 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Offer several choices of activities that are appealing to the patient.' WHERE QID = 430 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Tell the patient that the doctor ordered the activity for the patient.' WHERE QID = 430 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Describe the activity in detail to the patient.' WHERE QID = 430 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Invite the patient to join the activity.' WHERE QID = 430 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I get into trouble because I do NOT think before I act.”' WHERE QID = 434 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“My parents have difficulty accepting my independence.”' WHERE QID = 434 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I''ve spent very little time actually enjoying life.”' WHERE QID = 434 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“It''s sad that others do <B>NOT</B> recognize my potential.”' WHERE QID = 434 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient is suspicious and mistrustful of others.' WHERE QID = 435 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient consciously wishes to manipulate others.' WHERE QID = 435 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient feels overwhelmed and helpless.' WHERE QID = 435 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient wants to get attention from others.' WHERE QID = 435 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"I have difficulty making decisions and adjusting to change."' WHERE QID = 436 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I am sure that I am being followed by someone from work."' WHERE QID = 436 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"All of my life I''ve had problems with <B>NOT</B> taking care of my appearance."' WHERE QID = 436 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"I spend too much money, which upsets my wife."' WHERE QID = 436 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Let the patient know that the patient is <B>NOT</B> alone.' WHERE QID = 437 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Encourage hope in the patient.' WHERE QID = 437 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Be helpful to the patient at all times' WHERE QID = 437 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Discourage the patient from denying the situation.' WHERE QID = 437 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“When I learn to stop after one drink, I can cope with my problems.”' WHERE QID = 439 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“When my family problems and problems at work go away, I will <B>NOT</B> need alcohol anymore.”' WHERE QID = 439 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I can <B>NOT</B> cope with my problems without drinking.”' WHERE QID = 439 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“In my business, most people work hard and drink too much.”' WHERE QID = 439 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"That must be an unpleasant experience for you. Have you had these feelings before?"' WHERE QID = 440 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I know you are frightened now, but soon the medication will decrease your symptoms."' WHERE QID = 440 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Part of your sickness is an imaginary world. In reality, television does <B>NOT</B> control people."' WHERE QID = 440 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Tell me more about these feelings."' WHERE QID = 440 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"These things always seem worse than they really are."' WHERE QID = 442 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"It''s important for your blood pressure that you <B>NOT</B> worry too much about that."' WHERE QID = 442 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"You''re worried that you will <B>NOT</B> be able to pay the rent?"' WHERE QID = 442 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"I will refer you to a social worker."' WHERE QID = 442 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Give the client a brief orientation to the psychiatric unit and stay with the client for a while.' WHERE QID = 445 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Offer the client a description of activities on the psychiatric unit and introduce the client to other patients.' WHERE QID = 445 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Introduce the client to another client and ask the other client to give a short tour of the psychiatric unit.' WHERE QID = 445 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Sit with the client in a quiet room and wait for the hallucinations to stop.' WHERE QID = 445 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I''m embarrassed that everyone has to take care of me.”' WHERE QID = 446 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Once my depression is over, I''ll be able to continue with my life.”' WHERE QID = 446 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I like being taken care of from time to time.”' WHERE QID = 446 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I''m glad that I came in for help.”' WHERE QID = 446 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Reaction formation' WHERE QID = 448 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Undoing' WHERE QID = 448 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Displacement' WHERE QID = 448 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Introjection' WHERE QID = 448 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Provide a private place for family members.' WHERE QID = 450 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Explain to the family that the patient is now in heaven or in some final resting place.' WHERE QID = 450 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Notify the family members individually.' WHERE QID = 450 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Keep the family away from seeing the patient.' WHERE QID = 450 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Conversion' WHERE QID = 452 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Acting out' WHERE QID = 452 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Compensation' WHERE QID = 452 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Projection' WHERE QID = 452 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"Why is it that you do <B>NOT</B> like me and yourself?"' WHERE QID = 453 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I''ll come back later when you are in a better mood."' WHERE QID = 453 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"It''s difficult for me to communicate with you when you talk this way."' WHERE QID = 453 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"You seem to be in pain; I''ll stay with you for a while."' WHERE QID = 453 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'To introduce the client to other clients' WHERE QID = 412 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'To communicate acceptance to the client by others' WHERE QID = 412 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'To encourage the client to make decisions' WHERE QID = 412 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'To increase the client’s sense of responsibility' WHERE QID = 412 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Check for normal breathing.' WHERE QID = 510 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Call the emergency response number.' WHERE QID = 510 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Deliver two rescue breaths.' WHERE QID = 510 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Begin chest compressions.' WHERE QID = 510 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '30 compressions to 2 breaths' WHERE QID = 512 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '30 compressions to 1 breath' WHERE QID = 512 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '15 compressions to 1 breath' WHERE QID = 512 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '15 compressions to 2 breaths' WHERE QID = 512 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient takes vitamin supplements every day.' WHERE QID = 530 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a history of pneumonia.' WHERE QID = 530 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient plays golf once a week.' WHERE QID = 530 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a high-stress job.' WHERE QID = 530 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'High-salt diet' WHERE QID = 542 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Kidney disease' WHERE QID = 542 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Obesity' WHERE QID = 542 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Unknown cause' WHERE QID = 542 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Digitalis' WHERE QID = 552 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Vitamin K' WHERE QID = 552 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Magnesium sulfate' WHERE QID = 552 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Protamine sulfate' WHERE QID = 552 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The location where both procedures are done and the use of sedation for both procedures' WHERE QID = 567 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The intended action of both procedures and the paddle placement for both procedures' WHERE QID = 567 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The timing of shock delivery and the voltage used for both procedures' WHERE QID = 567 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The indications for both procedures and the informed consent that is required for both procedures' WHERE QID = 567 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Airways, breathing, compressions' WHERE QID = 509 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Airway, back blows, compressions' WHERE QID = 509 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Airway, breathing, circulation' WHERE QID = 509 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Airway, breathing, carotid pulse' WHERE QID = 509 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'femoral artery' WHERE QID = 511 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'radial artery' WHERE QID = 511 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'carotid artery' WHERE QID = 511 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'brachial artery' WHERE QID = 511 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Bronchospasm' WHERE QID = 541 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Loss of potassium' WHERE QID = 541 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Loss of libido' WHERE QID = 541 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Tachycardia' WHERE QID = 541 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Males who abuse drugs' WHERE QID = 560 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Males who are sexually active' WHERE QID = 560 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'All females' WHERE QID = 560 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Females who are sexually active' WHERE QID = 560 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"I will be asleep during the first part of the test."' WHERE QID = 564 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I will look at flickering lights at one point in the test."' WHERE QID = 564 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"I will lie as still as I can during the test.  The results will be better if I lie still."' WHERE QID = 564 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"I will feel shocks during the test, but the shocks will be very small and will feel like a tingle."' WHERE QID = 564 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"It will be good to use my electric blanket again when I sleep."' WHERE QID = 566 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"I will move my left arm several times a day."' WHERE QID = 566 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"My spouse signed up for a CPR class."' WHERE QID = 566 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"I know my wound is healing because I see drainage from the site of the incision."' WHERE QID = 566 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Does the patient understand the procedure that he is giving consent for?' WHERE QID = 75 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Does the patient have any questions about the procedure?' WHERE QID = 75 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Does the patient give consent for the procedure by his own free will?' WHERE QID = 75 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Is the patient able to write his name?' WHERE QID = 75 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Pedal edema.' WHERE QID = 6259 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Clubbing of the fingers.' WHERE QID = 6259 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Obligate nose breathing.' WHERE QID = 6259 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Warm, dry skin.' WHERE QID = 6259 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Prothrombin time' WHERE QID = 553 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Clotting time' WHERE QID = 553 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Partial thromboplastin time' WHERE QID = 553 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Platelet count' WHERE QID = 553 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'A drive that the client should deny' WHERE QID = 390 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'A dissociative response to trauma' WHERE QID = 390 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'A hidden wish to become ill and disabled' WHERE QID = 390 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'A symbolic expression of conflict and guilt' WHERE QID = 390 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"If a nurse stays with you in a closed space, it may help you to overcome your fear."' WHERE QID = 401 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Even though you know that your fears do <B>NOT</B> make sense, it does <B>NOT</B> help you feel better."' WHERE QID = 401 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"If you participate in several of our activities on the psychiatric ward, you may feel better."' WHERE QID = 401 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Some incident frightened you when you were a child and may have caused this fear."' WHERE QID = 401 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Give the patient reflective feedback.' WHERE QID = 447 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Stay with the patient and try to calm the patient down by talking quietly to the patient.' WHERE QID = 447 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Set limits on the patient''s behavior.' WHERE QID = 447 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Place the patient in a room that is well lit and close to the nurse''s station.' WHERE QID = 447 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'High-protein, low-fat, high-iron diet.' WHERE QID = 751 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'High-vitamin C, high-protein, high-carbohydrate diet.' WHERE QID = 751 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'High-vitamin A, high-calcium, high-fat diet.' WHERE QID = 751 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'High-vitamin B, high-protein, low-carbohydrate diet.' WHERE QID = 751 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The child is frightened due to the hospitalization.' WHERE QID = 6211 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'I should perform a neurological assessment.' WHERE QID = 6211 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Thank you for bringing that observation to my attention.' WHERE QID = 6211 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The inability to maintain eye contact is a characteristic of autism.' WHERE QID = 6211 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Intubation tray.' WHERE QID = 6246 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'EKG machine.' WHERE QID = 6246 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Dialysis machine.' WHERE QID = 6246 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Gastric lavage tube.' WHERE QID = 6246 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Central nervous system depressant.' WHERE QID = 6267 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Antianxiety' WHERE QID = 6267 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Sedative' WHERE QID = 6267 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Central nervous system stimulant.' WHERE QID = 6267 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I will <B>NOT</B> drink beverages that contain caffeine.”' WHERE QID = 327 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“I will <B>NOT</B> increase my level of exercise.”' WHERE QID = 327 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I will <B>NOT</B> sit in a hot tub.”' WHERE QID = 327 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“I will eat a moderate amount of sodium.”' WHERE QID = 327 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient passes a black stool.' WHERE QID = 561 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient is pale.' WHERE QID = 561 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a nosebleed.' WHERE QID = 561 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient is confused.' WHERE QID = 561 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in blood pressure' WHERE QID = 81 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in skin temperature' WHERE QID = 81 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in heart rate' WHERE QID = 81 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in respiration' WHERE QID = 81 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Weight loss or gain, fatigue.' WHERE QID = 88 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Obesity, restlessness, and thirst.' WHERE QID = 88 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Anxiety, insomnia, and memory loss.' WHERE QID = 88 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Quick response to analgesics.' WHERE QID = 88 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Contact the physician.' WHERE QID = 90 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Place the patient on contact precautions.' WHERE QID = 90 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Irrigate the wound.' WHERE QID = 90 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Ask the patient to identify how severe the pain is on a scale of 0-10.' WHERE QID = 90 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Recheck the patient’s blood type and cross-match with another nurse.' WHERE QID = 55 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Slow down the transfusion.' WHERE QID = 55 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Stop the transfusion.' WHERE QID = 55 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Contact the doctor.' WHERE QID = 55 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypotension, sudden fever, and flushed skin' WHERE QID = 56 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Kidney pain, hematuria, and cyanosis' WHERE QID = 56 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Urticaria, wheezing, and flushed skin' WHERE QID = 56 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hives, itching, and anaphylaxis' WHERE QID = 56 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The blood infuses in three hours.' WHERE QID = 53 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The blood is started with normal saline.' WHERE QID = 53 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The blood is started 15 min after it arrives from the blood bank.' WHERE QID = 53 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The blood infuses at 10 ml/min for the first 15 min.' WHERE QID = 53 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Judgment alterations, memory deficit, and irritability' WHERE QID = 389 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Anorexia and weight loss, fatigue, and hopelessness' WHERE QID = 389 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Confusion, delirium, and hallucinations' WHERE QID = 389 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Impaired motor skills, lack of coordination, and mood changes' WHERE QID = 389 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The group’s ability to evaluate their behavior' WHERE QID = 400 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The stage of the group’s interaction' WHERE QID = 400 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The leader’s skill in promoting progress in the group' WHERE QID = 400 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The sense of belonging that the group members feel' WHERE QID = 400 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Diphtheria, polio, typhoid fever.' WHERE QID = 6258 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Diphtheria, pertussis, typhoid fever.' WHERE QID = 6258 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Diphtheria, pertussis, tetanus.' WHERE QID = 6258 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Diphtheria, polio, tetanus.' WHERE QID = 6258 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Repeat first DTaP, starting the schedule again.' WHERE QID = 6289 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Give second DTaP.' WHERE QID = 6289 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Give MMR.' WHERE QID = 6289 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Give two DPT vaccinations today.' WHERE QID = 6289 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Substitution' WHERE QID = 411 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Undoing' WHERE QID = 411 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Compensation' WHERE QID = 411 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Denial' WHERE QID = 411 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient wants to impress the nurse with his generosity toward his family.' WHERE QID = 443 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient is insecure about his self-worth and needs to have electronic devices to manipulate.' WHERE QID = 443 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has completely lost contact with reality and his thought patterns are disturbed.' WHERE QID = 443 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a mood disturbance and his judgment is poor at this time.' WHERE QID = 443 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“Take with antacids or milk to decrease the GI symptoms.”' WHERE QID = 372 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“Sit upright for 15 to 30 minutes after you take the drug.”' WHERE QID = 372 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“Your stool may turn the color of clay.”' WHERE QID = 372 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“If you are taking other iron pills, finish the bottle of pills before you start this prescription.”' WHERE QID = 372 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Prevents attacks that are caused by aggravation' WHERE QID = 537 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Decreases preload.' WHERE QID = 537 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Produces dilation of the coronary artery' WHERE QID = 537 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Corrects dysrhythmias that are caused by drugs' WHERE QID = 537 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a history of myocardial infarction.' WHERE QID = 539 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a history of asthma since childhood.' WHERE QID = 539 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a history of infective endocarditis.' WHERE QID = 539 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient has a history of hypertension for five years.' WHERE QID = 539 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Birth-18 months' WHERE QID = 394 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '18 months-3 years' WHERE QID = 394 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '3-6 years' WHERE QID = 394 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '6-12 years' WHERE QID = 394 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Serum creatinine level of 2.4 mg/dL.' WHERE QID = 777 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'AST (SGOT) 15 u/L.' WHERE QID = 777 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'White blood cell count of 16,000/mm' WHERE QID = 777 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'White blood cell count of 4,000/mm' WHERE QID = 777 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Intramuscular' WHERE QID = 58 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Oral' WHERE QID = 58 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Intravenous' WHERE QID = 58 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Topical' WHERE QID = 58 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Intramuscular' WHERE QID = 59 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Oral' WHERE QID = 59 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Intravenous' WHERE QID = 59 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Topical' WHERE QID = 59 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Increased blood pressure' WHERE QID = 60 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Decreased urine output' WHERE QID = 60 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 60 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Decreased pulse' WHERE QID = 60 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient has singed nasal hair.' WHERE QID = 61 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s blood pressure is 106/62.' WHERE QID = 61 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has blisters on the hands.' WHERE QID = 61 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient''s capillary refill time is < 3 s.' WHERE QID = 61 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Meat and orange juice' WHERE QID = 62 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Whole grain bread and an apple' WHERE QID = 62 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Green vegetables and milk' WHERE QID = 62 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Peanut butter and a banana' WHERE QID = 62 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Cut out the vessel.' WHERE QID = 556 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Insert a graft.' WHERE QID = 556 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Repair the artery.' WHERE QID = 556 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Remove the clot.' WHERE QID = 556 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Third degree' WHERE QID = 57 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Full thickness' WHERE QID = 57 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Deep partial thickness' WHERE QID = 57 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Superficial partial thickness' WHERE QID = 57 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '44 lb divided by 2.2 = 20 kg' WHERE QID = 5710 AND AIndex = 'A'

IF NOT EXISTS(Select 1 from AnswerChoices WHERE QID =  5710 AND AIndex = 'B')
BEGIN
INSERT INTO AnswerChoices Values(5710,'B','10 mg multiplied by 20 kg = 200 mg',0,0,1,0,'mg','10 mg multiplied by 20 kg = 200 mg')
END
ELSE
BEGIN
UPDATE AnswerChoices SET AlternateAText = '10 mg multiplied by 20 kg = 200 mg' WHERE QID = 5710 AND AIndex = 'B'
END

UPDATE AnswerChoices SET AlternateAText = '8-ounce jar.' WHERE QID = 6327 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '16-ounce jar.' WHERE QID = 6327 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '48-ounce jar.' WHERE QID = 6327 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '128-ounce jar.' WHERE QID = 6327 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '“I can <B>NOT</B> wait to eat a hot dog with sauerkraut.”' WHERE QID = 402 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '“I will start taking FiberCon when I get home.”' WHERE QID = 402 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '“I will play doubles tennis with my neighbors.”' WHERE QID = 402 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '“When I get home, I will take my car out for a road trip.”' WHERE QID = 402 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Potassium chloride replaces the potassium that is lost in the gastric fluid.' WHERE QID = 40 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Potassium chloride replaces decreased dietary potassium due to NPO status.' WHERE QID = 40 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Potassium chloride prevents the loss of sodium in the urine.' WHERE QID = 40 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Potassium chloride prevents the loss of potassium in the urine.' WHERE QID = 40 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Hypernatremia' WHERE QID = 42 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Hyponatremia' WHERE QID = 42 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Hyperkalemia' WHERE QID = 42 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Hypokalemia' WHERE QID = 42 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Keep the IV open.' WHERE QID = 312 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Counteract the effects of protamine sulfate.' WHERE QID = 312 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Make the patient more comfortable.' WHERE QID = 312 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Keep the patient from developing deep venous blood clots.' WHERE QID = 312 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The patient gains 4 lbs in 1 day.' WHERE QID = 41 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The patient has an increase in nausea.' WHERE QID = 41 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The patient has an increase in muscle irritability.' WHERE QID = 41 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The patient has an episode of fibrillation.' WHERE QID = 41 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = '"Assume the pain is psychological."' WHERE QID = 74 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = '"Check to see if the patient has a history of addiction."' WHERE QID = 74 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = '"Try several other pain relief measures."' WHERE QID = 74 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = '"Assess location, character, and intensity of pain."' WHERE QID = 74 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Use caution to prevent the patient’s addition to narcotics.' WHERE QID = 89 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Recognize that cancer pain is often psychological in origin.' WHERE QID = 89 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Give pain medication only if the patient experiences severe pain.' WHERE QID = 89 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Give pain medication before the pain becomes severe.' WHERE QID = 89 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The bathroom is equipped with grab bars.' WHERE QID = 66 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Throw rugs have been removed' WHERE QID = 66 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The client ambulates wearing socks.' WHERE QID = 66 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The stairs are well lighted.' WHERE QID = 66 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Atrial depolarization' WHERE QID = 565 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Ventricular depolarization' WHERE QID = 565 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Ventricular repolarization' WHERE QID = 565 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Central venous pressure' WHERE QID = 565 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in frequency' WHERE QID = 85 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Incontinence' WHERE QID = 85 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Decrease in sphincter reflexes' WHERE QID = 85 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Formation of bladder stones' WHERE QID = 85 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Check the patient’s blood type and cross-match with another nurse.' WHERE QID = 47 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Leave the blood at the patient''s bedside until the doctor checks it.' WHERE QID = 47 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Flush the IV tubing with normal saline and hang the next unit of blood.' WHERE QID = 47 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Run first 50 mL of blood rapidly to check for any reaction in the patient.' WHERE QID = 47 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The parent takes the child''s temperature using an oral electronic thermometer.' WHERE QID = 6248 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'The parent encourages the child to play with boats during bath time.' WHERE QID = 6248 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'The child wears a helmet when riding a bicycle.' WHERE QID = 6248 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'The child eats peanut butter and jelly sandwiches.' WHERE QID = 6248 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'Decrease the cardiac workload' WHERE QID = 515 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'Decrease the myocardial irritability' WHERE QID = 515 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'Increase the strength of the heart''s contractions' WHERE QID = 515 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'Increase myocardial automaticity' WHERE QID = 515 AND AIndex = 'D'
UPDATE AnswerChoices SET AlternateAText = 'The home care nurse visits a 3-year-old child diagnosed at birth with phenylketonuria. The nurse assesses the child''s intake for the previous week. The nurse is MOST concerned if the child''s parent makes which of the following statements?' WHERE QID = 6324 AND AIndex = 'A'
UPDATE AnswerChoices SET AlternateAText = 'My child eats low-protein pasta for dinner.' WHERE QID = 6324 AND AIndex = 'B'
UPDATE AnswerChoices SET AlternateAText = 'My child really likes potato chips.' WHERE QID = 6324 AND AIndex = 'C'
UPDATE AnswerChoices SET AlternateAText = 'My child''s favorite lunch is a peanut butter and jelly sandwich.' WHERE QID = 6324 AND AIndex = 'D'



-- Stored Procedure / Function Changes
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetStudentMissionRecipients' AND TYPE = 'P')
	DROP PROCEDURE [dbo].[uspGetStudentMissionRecipients]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get email mission details
-- =============================================
CREATE PROCEDURE [dbo].[uspGetStudentMissionRecipients]
	-- Add the parameters for the stored procedure here
	@emailMissionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		--SelectionLevel Definition
        --Institution = 1,
        --Cohort = 2,
        --Group = 3,
        --StudentUser = 4,
        --AdminUser = 5

	-- get student level data
	SELECT C.Email, 4 AS SelectionLevel, C.FirstName + ' ' + C.LastName AS Name
	FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurStudentInfo C ON B.PersonID = C.UserID
	WHERE A.EmailMissionId = @emailMissionId AND C.UserDeleteData IS NULL

	UNION ALL

	-- get group level data
	SELECT F.Email, 3 AS SelectionLevel, C.GroupName AS Name
	FROM EmailMission A
		JOIN EmailGroup B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.GroupId = B.GroupId
		JOIN NurGroup C ON B.GroupID = C.GroupID
		JOIN NurStudentInfo F ON SA.StudentID = F.UserID
	WHERE A.EmailMissionId = @emailMissionId
	AND F.UserDeleteData IS NULL AND SA.DeletedDate IS NULL

	UNION ALL

	-- get institution level data
	SELECT D.Email, 1 AS SelectionLevel, C.InstitutionName AS Name
	FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
		JOIN NurStudentInfo D ON C.InstitutionID = D.InstitutionID
	WHERE A.EmailMissionId = @emailMissionId AND D.UserDeleteData IS NULL

	UNION ALL

	-- get cohort level data
	SELECT E.Email, 2 AS SelectionLevel, C.CohortName AS Name
	FROM EmailMission A
		JOIN EmailCohort B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.CohortId = B.CohortId
		JOIN NurStudentInfo E ON SA.StudentID = E.UserID
		JOIN NurCohort C ON B.CohortID = C.CohortID
	WHERE A.EmailMissionId = @emailMissionId AND E.UserDeleteData IS NULL
	
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetEmailMissionDetails' AND TYPE = 'P')
	DROP PROCEDURE [dbo].[uspGetEmailMissionDetails]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get email mission details (student lists)
-- =============================================
CREATE PROCEDURE [dbo].[uspGetEmailMissionDetails]
	-- Add the parameters for the stored procedure here
	@emailMissionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- get student level data
	SELECT C.username, C.userpass, C.email
	FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurStudentInfo C ON B.PersonID = C.UserID
	WHERE A.EmailMissionId = @emailMissionId AND C.UserDeleteData IS NULL

	UNION ALL

	-- get group level data
	SELECT F.username, F.userpass, F.email
	FROM EmailMission A
		JOIN EmailGroup B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.GroupId = B.GroupId
		JOIN NurStudentInfo F ON SA.StudentID = F.UserID
	WHERE A.EmailMissionId = @emailMissionId
	AND F.UserDeleteData IS NULL AND SA.DeletedDate IS NULL

	UNION ALL

	-- get institution level data
	SELECT D.username, D.userpass, D.email
	FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
		JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
		JOIN NurStudentInfo D ON C.InstitutionID = D.InstitutionID
	WHERE A.EmailMissionId = @emailMissionId AND D.UserDeleteData IS NULL

	UNION ALL

	-- get cohort level data
	SELECT E.username, E.userpass, E.email
	FROM EmailMission A
		JOIN EmailCohort B ON A.EmailMissionId = B.EmailMissionId
		JOIN NusStudentAssign AS SA ON SA.CohortId = B.CohortId
		JOIN NurStudentInfo E ON SA.StudentID = E.UserID
	WHERE A.EmailMissionId = @emailMissionId AND E.UserDeleteData IS NULL
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateEmailStatus' AND TYPE = 'P')
	DROP PROCEDURE [dbo].[uspUpdateEmailStatus]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/30/2010
-- Description:	Procedure used to update email sending status
-- =============================================
CREATE PROCEDURE [dbo].[uspUpdateEmailStatus]
	-- Add the parameters for the stored procedure here
	@emailMissionId int, @emailStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE EmailMission SET State = @emailStatus WHERE EmailMissionID = @emailMissionId
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetQueuedMissions' AND TYPE = 'P')
	DROP PROCEDURE [dbo].[uspGetQueuedMissions]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/27/2010
-- Description:	Procedure used to get queued email missions.
-- =============================================
CREATE PROCEDURE [dbo].[uspGetQueuedMissions]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT M.EmailMissionID, M.EmailID, M.ToAdminOrStudent, A.Email AS CreatorEmail
	FROM EmailMission AS M
	INNER JOIN NurAdmin AS A
	ON A.UserId = M.AdminId
	WHERE State = 1 AND GetDate() > SendTime
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetCustomEmailDefinition' AND TYPE = 'P')
	DROP PROCEDURE [dbo].[uspGetCustomEmailDefinition]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cale Teeter
-- Create date: 08/30/2010
-- Description:	Procedure used to get custom email definition
-- =============================================
CREATE PROCEDURE [dbo].[uspGetCustomEmailDefinition]
	-- Add the parameters for the stored procedure here
	@emailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Title, Body FROM Email WHERE EmailId = @emailId
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveSessionId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveSessionId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSaveSessionId]
	@userId int,
	@sessionid varchar(50)
AS
BEGIN
-- =============================================
-- Author:		Cale Teeter
-- Create date: 05/13/2009
-- Description:	Procedure used to save the current asp.net session id for the user.
-- =============================================

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE NurStudentInfo
	SET SessionId = @sessionId
	WHERE UserId = @userId
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetAllProducts' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAllProducts]
GO

CREATE PROCEDURE [dbo].[uspGetAllProducts]
@ProductId int
AS
BEGIN
   -- SET NOCOUNT ON added to prevent extra result sets from
   SET NOCOUNT ON;

   SELECT ProductID,ProductName, ProductType,
		  TakeTestMultipleTimes AS MultiUseTest, TestType, Scramble,
          Remediation, Bundle
   FROM Products
   Where ( @ProductID = 0 OR ProductID = @ProductId)
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestByUserAndTestID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestByUserAndTestID]
GO

CREATE PROCEDURE [dbo].[uspGetTestByUserAndTestID]
	
	@userId int,@testId int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT   UserTestID,TestID,TestNumber,TestStarted,TestName,Override,SuspendType FROM  UserTests WHERE TestID = @testId and UserID= @userId
	SET NOCOUNT OFF
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetProductByTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetProductByTest]
GO

CREATE PROCEDURE [dbo].[uspGetProductByTest]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ProductID, TestID,TestName,TestNumber
	From Tests
	WHERE ProductID IS NOT NULL
    SET NOCOUNT Off;
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestQuestionCount' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestQuestionCount]
GO
CREATE PROCEDURE [dbo].[uspGetTestQuestionCount]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Declare @Number int
	SELECT @Number = COUNT(*)
	FROM TestQuestions, Questions
	WHERE TestQuestions.QID = dbo.Questions.QID AND TestID = @testId
		AND (Questions.Active = 1 OR Questions.Active IS NULL)

Return  @Number

SET NOCOUNT Off;
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspInsertTestQuestion' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertTestQuestion]
GO
CREATE PROCEDURE [dbo].[uspInsertTestQuestion]
	@QID int, @UserTestID int, @QuestionNumber int
AS

INSERT INTO UserQuestions
	(QID, UserTestID, QuestionNumber, Correct, TimeSpendForQuestion,
		TimeSpendForRemedation, TimeSpendForExplanation, AnswerTrack)
VALUES
	(@QID, @UserTestID, @QuestionNumber, 2, 0, 0, 0, '')

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestionCountByFileTypeId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestQuestionCountByFileTypeId]
GO
CREATE PROC [dbo].[uspGetTestQuestionCountByFileTypeId]
(
	@TestId int,
	@TypeOfFileId varchar(500),
	@TotalCount int OUTPUT
)
AS
	SET NOCOUNT ON;

	SELECT @TotalCount = COUNT(*)
	FROM dbo.TestQuestions AS T
	INNER JOIN dbo.Questions AS Q
	ON T.QID = Q.QID
	WHERE TypeOfFileID = @TypeOfFileId
	AND	TestID = @TestId
	AND (Q.Active = 1
	OR Q.Active IS NULL)
	
	SET NOCOUNT OFF
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCreateTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCreateTest]
GO

CREATE PROCEDURE [dbo].[uspCreateTest]
	@userId int, @productId int, @programId int, @timedTest int,@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @userTestId int, @testNumber int, @firstQuestionId int, @QuestionID int, @QID int, @QuestionNumber int

    Declare @numberOfQuestions int, @cohortId int,@institutionId int, @scramble int, @TimeRemaining int
    exec [dbo].[uspGetTestQuestionCountByFileTypeId] @TestId = @testId, @TypeOfFileId = '03', @TotalCount = @numberOfQuestions OUTPUT

	SET @TimeRemaining = 72 * @numberOfQuestions

   SELECT  @institutionId = InstitutionID,@cohortId=CohortID
	FROM NurStudentInfo LEFT JOIN dbo.NusStudentAssign
		ON NurStudentInfo.UserID = NusStudentAssign.StudentID
	WHERE UserID = @userId

    SELECT  @scramble = Scramble FROM Products WHERE ProductID = @productId

	-- Create temp table for iteration of questions
	CREATE TABLE #questionTbl (QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, QID int, QuestionNumber int)

	-- Check for existing test
	SET @testNumber = (SELECT COUNT(TestNumber) FROM dbo.UserTests WHERE UserID = @userId AND TestID = @testId)
	IF(@testNumber IS NULL)
		SET @testNumber = 1
	ELSE
		SET @testNumber = @testNumber + 1
	END

	BEGIN TRANSACTION
		-- Create the new test instance
		INSERT INTO UserTests
			(UserId, TestId, TestNumber, CohortId, InsitutionID, ProductId, ProgramId, TestStarted, TestStatus,
				QuizOrQbank, TimedTest, TutorMode, ReusedMode, NumberOfQuestions, TestName, SuspendQuestionNumber,
				TimeRemaining, SuspendType, SuspendQID)
		VALUES
			(@userId, @testId, @testNumber, @cohortId, @institutionId, @productId, @programId, GetDate(), 0,
				'Q', @timedTest, 0, 0, @numberOfQuestions, NULL, 0, @TimeRemaining, '03', 0)

		IF @@ERROR <> 0
			BEGIN
				-- Rollback transaction
				ROLLBACK

				-- Raise error and return
				RAISERROR('Error in inserting TestInstance into UserTests', 16, 1)
				RETURN
			END

		-- Get the user instance id
		SET @userTestId = SCOPE_IDENTITY()

		INSERT INTO #questionTbl
			EXEC uspGetQuestionsToCreateTest @testId, @scramble

		IF @@ERROR <> 0
			BEGIN
				-- Rollback transaction
				ROLLBACK

				-- Raise error and return
				RAISERROR('Error in inserting TestQuestions into temp table', 16, 1)
				RETURN
			END

		DECLARE QS CURSOR FOR
		SELECT QuestionID, QID, QuestionNumber
		FROM #questionTbl

		OPEN QS
		FETCH QS INTO @QuestionID, @QID, @QuestionNumber

		SET @firstQuestionId = @QID

		WHILE @@FETCH_STATUS = 0
			BEGIN
				IF @scramble = 1
					SET @QuestionNumber = @QuestionID

				-- insert the question
				EXEC uspInsertTestQuestion @QID, @userTestId, @QuestionNumber

				FETCH QS INTO @QuestionID, @QID, @QuestionNumber
			END

		IF @@ERROR <> 0
			BEGIN
				-- Rollback transaction
				ROLLBACK

				-- Raise error and return
				RAISERROR('Error in inserting TestQuestions into UserTestQuestions', 16, 1)
				RETURN
			END

		DROP TABLE #questionTbl

		CLOSE QS
		DEALLOCATE QS

	COMMIT TRANSACTION

--	Return First Question FileType : Can be refactored later.
	SELECT UserTestID, @numberOfQuestions AS NumberOfQuestions,
		@TimeRemaining AS TimeRemaining,
		CAST(Q.TypeOfFileID AS int) AS TypeOfFileID
	FROM UserQuestions UQ INNER JOIN Questions Q
	ON Q.QId = UQ.QId
	WHERE UQ.UserTestId = @userTestId
	AND UQ.QuestionNumber = 1

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestQuestions' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestQuestions]
GO

CREATE PROCEDURE [dbo].[uspGetTestQuestions]
		@userTestId int, @questionId int, @typeOfFileId varchar(500), @inCorrectOnly bit
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 1 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType ,B.Active
	FROM UserQuestions AS A
		Inner JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND ((@inCorrectOnly = 0 AND A.QuestionNumber = @questionId - 1)
	OR (@inCorrectOnly = 1 AND A.QuestionNumber IN (SELECT MAX(UQ.QuestionNumber)
		FROM UserQuestions AS UQ
		INNER JOIN Questions AS Q ON UQ.QID = Q.QID
		WHERE UQ.Correct <> 1
		AND UQ.UserTestId = @userTestId
		AND UQ.QuestionNumber < @questionId
		AND (Q.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (Q.Active IS NULL OR Q.Active = 1))))

	UNION ALL

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 0 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType,B.Active
	FROM UserQuestions AS A JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId AND A.QID  = @questionId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND (@inCorrectOnly = 0 OR (@inCorrectOnly = 1 AND A.Correct <> 1))

	UNION ALL

	SELECT @userTestId AS UserTestID,A.QID, CAST(B.TypeOfFileID AS int) AS TypeOfFileID, 2 AS PointerType, TimeSpendForRemedation, TimeSpendForExplanation,
		B.Stem, B.ALternateStem, B.ListeningFileUrl, B.Explanation, CAST(B.RemediationID AS int) AS RemediationID, B.TopicTitleId, B.ItemTitle,A.QuestionNumber,A.Correct,CAST(B.QuestionType AS int) AS QuestionType,B.Active
	FROM UserQuestions AS A JOIN Questions AS B ON A.QID = B.QID
	WHERE UserTestId = @userTestId
	AND (B.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
	AND (B.Active IS NULL OR B.Active = 1)
	AND ((@inCorrectOnly = 0 AND A.QuestionNumber = @questionId + 1)
	OR (@inCorrectOnly = 1 AND A.QuestionNumber IN (SELECT MIN(UQ.QuestionNumber)
		FROM UserQuestions AS UQ
		INNER JOIN Questions AS Q ON UQ.QID = Q.QID
		WHERE UQ.Correct <> 1
		AND UQ.UserTestId = @userTestId
		AND UQ.QuestionNumber > @questionId
		AND (Q.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (Q.Active IS NULL OR Q.Active = 1))))

	SET NOCOUNT OFF;
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetQuestionByTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionByTest]
GO

CREATE PROCEDURE [dbo].[uspGetQuestionByTest]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT testquestions.QID, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID, CAST(dbo.Questions.QuestionType AS int) AS QuestionType, 0 AS PointerType, Questions.Stem, Questions.ListeningFileUrl, Questions.Explanation, CAST(Questions.RemediationID AS int) AS RemediationID, Questions.TopicTitleId, Questions.ItemTitle,testquestions.QuestionNumber,Questions.Active
	FROM testquestions, Questions
	WHERE testquestions.QID = Questions.QID AND testquestions.testID = @testId
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetQuestionsByUserTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionsByUserTest]
GO

CREATE PROCEDURE [dbo].[uspGetQuestionsByUserTest]
	@UserTestID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT UserTestID,QID,QuestionNumber,TimeSpendForRemedation, TimeSpendForExplanation,TimeSpendForQuestion ,AnswerTrack,OrderedIndexes,Correct
FROM dbo.UserQuestions WHERE   UserTestID= @UserTestID
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetExhibitByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetExhibitByID]
GO

CREATE PROCEDURE [dbo].[uspGetExhibitByID]
	@QuestionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT  QID,ExhibitTab1,ExhibitTab2,ExhibitTab3,ExhibitTitle1,ExhibitTitle2,ExhibitTitle3 FROM dbo.Questions
       WHERE QID=@QuestionID
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetPrevNextItemInTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetPrevNextItemInTest]
GO

CREATE PROCEDURE [dbo].[uspGetPrevNextItemInTest]
	@TestID int, @questionId int, @typeOfFileId varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,2 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
	FROM  dbo.TestQuestions INNER JOIN dbo.Questions
	ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QuestionNumber > @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)
	UNION ALL
	SELECT  dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,1 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
    FROM dbo.TestQuestions INNER JOIN dbo.Questions
    ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QuestionNumber < @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)
	UNION ALL
	SELECT  dbo.TestQuestions.QuestionNumber,dbo.Questions.Stem,0 AS PointerType,dbo.Questions.Explanation,CAST(Questions.RemediationID AS int) AS RemediationID,
		dbo.Questions.QuestionType, CAST(Questions.TypeOfFileID AS int) AS TypeOfFileID,dbo.Questions.TopicTitleID, Questions.ListeningFileUrl,
		dbo.Questions.QID,dbo.Questions.ItemTitle, dbo.Questions.Active
	FROM dbo.TestQuestions INNER JOIN dbo.Questions
	ON dbo.Questions.QID = dbo.TestQuestions.QID
	WHERE dbo.TestQuestions.TestID = @TestID
		AND dbo.TestQuestions.QID = @questionId
		AND (dbo.Questions.TypeOfFileID = @typeOfFileId OR @typeOfFileId = '00')
		AND (dbo.Questions.Active IS NULL OR dbo.Questions.Active = 1)

	SET NOCOUNT OFF;
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateTestCompleted' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateTestCompleted]
GO

CREATE PROCEDURE [dbo].[uspUpdateTestCompleted]
	
	
	@UserTestID int,
	@SuspendQuestionNumber int,
	@SuspendQID int,
	@SuspendType char(2),
	@TimeRemaining varchar(50)

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE UserTests SET

SuspendQuestionNumber=@SuspendQuestionNumber, SuspendQID=@SuspendQID, SuspendType=@SuspendType, TimeRemaining=@TimeRemaining, TestStatus=1, TestComplited=getdate() WHERE UserTestID=@UserTestID

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspIsTest74Question' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspIsTest74Question]
GO

CREATE PROCEDURE [dbo].[uspIsTest74Question]
	@QID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT  TestID, QID FROM  dbo.TestQuestions WHERE  TestID = 74 AND QID = @QID
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUserTestByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUserTestByID]
GO
CREATE PROCEDURE [dbo].[uspGetUserTestByID]
	
	@UserTestID int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Select TestID,TestStarted,SuspendType,UserTestID,TestName,TestNumber,SuspendQID,TimedTest,TimeRemaining,TutorMode FROM UserTests WHERE UserTestID=@UserTestID

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestsByUserIDProductID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestsByUserIDProductID]
GO


CREATE PROCEDURE [dbo].[uspGetTestsByUserIDProductID]

 @ProductID int,
    @UserID int,
 @Hour int


AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;



IF (@ProductID != 0)

 Select  CONVERT(nvarchar,QInfo.PercentCorrect)  as PercentCorrect , QInfo.QuestionCount,QInfo.UserTestID, UserTestsInfo.TestName,UserTestsInfo.TestStarted,UserTestsInfo.TestID,UserTestsInfo.TestStatus,UserTestsInfo.ProductName,
 UserTestsInfo.QuizOrQBank,UserTestsInfo.TestSubGroup FROM
(SELECT ((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount) AS PercentCorrect, QC.QuestionCount,QC.UserTestID FROM (select dbo.UserQuestions.UserTestID,count(*) AS QuestionCount FROm dbo.UserQuestions
 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID AND dbo.UserTests.ProductID=@ProductID  group by dbo.UserQuestions .UserTestID) QC
 LEFT OUTER JOIN   (SELECT  dbo.UserQuestions.UserTestID,count(*) AS QuestionCorrectCount FROm dbo.UserQuestions  INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID AND dbo.UserTests.ProductID=@ProductID
 AND dbo.UserQuestions.Correct=1  group by dbo.UserQuestions .UserTestID)QCC  ON QC.UserTestID=QCC.UserTestID ) QInfo INNER JOIN (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,DATEADD(hour, @Hour, TestStarted)
 as TestStarted, dbo.UserTests.TestID, dbo.UserTests.TestStatus,  dbo.Products.ProductName,dbo.UserTests.QuizOrQBank , dbo.Tests.TestSubGroup
 FROM  dbo.Tests INNER JOIN
 dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
 dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
 AND (dbo.Tests.ProductID = @ProductID) WHERE (UserID = @UserID)  )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID  ORDER BY UserTestsInfo.ProductName,UserTestsInfo.TestName,UserTestsInfo.TestStarted desc



ELSE

 Select CONVERT(nvarchar,QInfo.PercentCorrect) as PercentCorrect,
  QInfo.QuestionCount,QInfo.UserTestID, UserTestsInfo.TestName,UserTestsInfo.TestStarted,UserTestsInfo.TestID,UserTestsInfo.TestStatus,UserTestsInfo.ProductName,UserTestsInfo.QuizOrQBank,UserTestsInfo.TestSubGroup FROM
	(SELECT ((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount) AS PercentCorrect, QC.QuestionCount,QC.UserTestID FROM (select dbo.UserQuestions.UserTestID,count(*) AS QuestionCount FROm dbo.UserQuestions
	 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID   group by dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN
	(SELECT  dbo.UserQuestions.UserTestID,count(*) AS QuestionCorrectCount FROm dbo.UserQuestions  INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID where dbo.UserTests.UserID=@UserID   AND dbo.UserQuestions.Correct=1
	group by dbo.UserQuestions .UserTestID)QCC  ON QC.UserTestID=QCC.UserTestID ) QInfo INNER JOIN (SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,DATEADD(hour, @Hour, TestStarted) as TestStarted,dbo.UserTests.TestID,
	dbo.UserTests.TestStatus,  dbo.Products.ProductName,dbo.UserTests.QuizOrQBank,dbo.Tests.TestSubGroup

 FROM  dbo.Tests INNER JOIN
 dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
 dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
  WHERE (UserID = @UserID) )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID ORDER BY UserTestsInfo.ProductName,UserTestsInfo.TestName,UserTestsInfo.TestStarted desc



 SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetPerformanceOverviewByProductIDChartType' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetPerformanceOverviewByProductIDChartType]
GO

CREATE PROCEDURE [dbo].[uspGetPerformanceOverviewByProductIDChartType]

 @ProductID int,
    @UserID int,
 @ChartType int

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;


IF (@ChartType = 1)


 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS Total,
	SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS N_Correct,
    SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC,0 AS UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3

ELSE

IF (@ChartType = 2)

 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS Total,
 SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS N_Correct,
 SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC,0 AS UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID AND dbo.Tests.TestSubGroup=3

ELSE

IF (@ChartType = 3)

 SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS Total,
		SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100 / SUM(1) AS N_Correct,
        SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
 SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
 SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
 SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
 SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC
    ,dbo.UserTests.UserTestID

 FROM    dbo.UserQuestions
  INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
  INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID AND  dbo.Tests.ProductID=@ProductID AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.UserTests.UserTestID


 SET NOCOUNT OFF
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetPerformanceDetailsByProductIDChartType' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetPerformanceDetailsByProductIDChartType]
GO


CREATE PROCEDURE [dbo].[uspGetPerformanceDetailsByProductIDChartType]
	
	@ProductID int,
    @UserID int,
	@ChartType int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON


IF (@ChartType = 1)
	
	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText
	 FROM  dbo.UserQuestions INNER JOIN
	 dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
	 dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
	 INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
	 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
	 WHERE   UserID =@UserID
	 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
	 GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty

ELSE

IF (@ChartType = 2)

	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText
	FROM  dbo.UserQuestions INNER JOIN
	dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
	dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
	INNER JOIN
	dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
	INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
	WHERE   UserID =@UserID
	AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
	GROUP BY dbo.NursingProcess.NursingProcess
ELSE

IF (@ChartType = 3)

	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText
    FROM  dbo.UserQuestions INNER JOIN
    dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
    dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
    INNER JOIN
    dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
    INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
    WHERE   UserID =@UserID
    AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
    GROUP BY dbo.ClinicalConcept.ClinicalConcept
ELSE
IF (@ChartType = 4)
SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText
 FROM  dbo.UserQuestions INNER JOIN
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
  dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
 INNER JOIN
 dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID
 ORDER BY dbo.ClientNeeds.ClientNeedsID
ELSE
IF (@ChartType = 5)
SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText
 FROM  dbo.UserQuestions INNER JOIN
  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
  dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
 INNER JOIN
 dbo.UserTests ON dbo.UserQuestions.UserTestID=dbo.UserTests.UserTestID
 INNER JOIN dbo.Tests ON dbo.Tests.TestID=dbo.UserTests.TestID
 WHERE   UserID =@UserID
 AND  dbo.Tests.ProductID=@ProductID  AND dbo.Tests.TestSubGroup=3
 GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID
 ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID
	SET NOCOUNT OFF
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetNumberOfCategory' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetNumberOfCategory]
GO

CREATE PROCEDURE [dbo].[uspGetNumberOfCategory]
AS
BEGIN
	
	SELECT count(*) as Number FROM dbo.ClientNeeds
	
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspChangePassword' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspChangePassword]
GO


CREATE PROCEDURE [dbo].[uspChangePassword]
	@userId int, @password varchar(50)
AS
BEGIN
	UPDATE NurStudentInfo
	SET UserPass = @password
	WHERE UserID = @userId
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCheckPercentileRankExists' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCheckPercentileRankExists]
GO


CREATE PROCEDURE [dbo].[uspCheckPercentileRankExists]
	-- Add the parameters for the stored procedure here
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT PercentileRank
	FROM Norming
	WHERE TestID = @testId
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCreateUserAnswers' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCreateUserAnswers]
GO


CREATE PROCEDURE [dbo].[uspCreateUserAnswers](@QID int, @UserTestID int, @AnswerID int,
	@AText varchar(2000), @Correct int, @AnswerConnectID int, @AType int,
	@AIndex char(1))
AS
INSERT INTO UserAnswers
	(QID, UserTestID, AnswerID, AText, Correct, AnswerConnectID, AType, AIndex)
VALUES
	(@QID, @UserTestID, @AnswerID, @AText, @Correct, @AnswerConnectID, @AType, @AIndex)




GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetClientNeedsSummary' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetClientNeedsSummary]
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsSummary]
			
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT dbo.ClientNeeds.ClientNeedsID,dbo.ClientNeeds.ClientNeeds, CNC.ClientNeedCategoryCount  FROM dbo.ClientNeeds INNER JOIN  (SELECT dbo.ClientNeedCategory.ClientNeedID,COUNT(*) AS ClientNeedCategoryCount FROM dbo.ClientNeedCategory GROUP BY ClientNeedID )CNC
ON dbo.ClientNeeds.ClientNeedsID= CNC.ClientNeedID
order by dbo.ClientNeeds.ClientNeedsID

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetClientNeedsCategoryInfo' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetClientNeedsCategoryInfo]
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsCategoryInfo]
	  @UserID int
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT dbo.ClientNeeds.ClientNeedsID,dbo.ClientNeedCategory.ClientNeedCategoryID,
dbo.ClientNeedCategory.ClientNeedCategory,dbo.ReturnAllQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID ) AS TotQCount,
dbo.ReturnUnUsedIncorrectQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID) AS UnUsedIncorrectQCount,
dbo.ReturnUnUsedQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID) AS UnUsedQCount,
dbo.ReturnIncorrectQCountByClientNeedCategoryID(dbo.ClientNeedCategory.ClientNeedCategoryID,@UserID) AS InCorrectQCount
FROM dbo.ClientNeeds INNER JOIN  dbo.ClientNeedCategory
ON dbo.ClientNeeds.ClientNeedsID = dbo.ClientNeedCategory.ClientNeedID

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetAllTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAllTests]
GO


CREATE PROCEDURE [dbo].[uspGetAllTests]
	@userId int, @productId int, @sType int, @testSubGroup int, @timeOffset int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@sType = 0)
	BEGIN
		IF (@productId = 4 OR @productId = 5)
		BEGIN
			SELECT DISTINCT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT  dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber,dbo.Tests.ProductID as TestProductID, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID,
						dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
						(dbo.Tests.ProductID= @productId) and (dbo.Tests.ActiveTest = 1)
					ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V
			ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate())) AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))
		END
		ELSE
		BEGIN
			SELECT DISTINCT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
				(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
					dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
					dbo.Tests.TestNumber,dbo.Tests.ProductID as TestProductID, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID,
					dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
				FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
					INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
					INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
					INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
					INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
					INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
						dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
						dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
				WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
					(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
					(dbo.Tests.ProductID= @productId) and (dbo.Tests.ActiveTest = 1)
				ORDER BY dbo.Tests.TestName ASC) AS Cohort_V
			ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
			dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
			dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
			Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
			Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
		WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate())) AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))
		END
	END
	ELSE
	BEGIN
		IF (@productId = 4 OR @productId = 5)
		BEGIN
			SELECT distinct V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID,
						dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 1) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
						(dbo.NurProgramProduct.ProductID = @productId) and (dbo.Tests.ActiveTest = 1)
					ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate())) AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))
		END
		ELSE
		BEGIN
			SELECT DISTINCT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE(dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE(dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID,
						dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 1) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
						(dbo.NurProgramProduct.ProductID = @productId) AND (dbo.Tests.ActiveTest = 1) AND (dbo.Tests.DefaultGroup='1')
					ORDER BY dbo.Tests.TestName ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND dbo.NurProductDatesStudent.Type=1 AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE (StartDate_All < DATEADD(hour, @timeOffset, GetDate())) AND (EndDate_All > DATEADD(hour, @timeOffset, GetDate()))
		END
	END
END



GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetAvpItems' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAvpItems]
GO

CREATE PROCEDURE [dbo].[uspGetAvpItems]
	-- Add the parameters for the stored procedure here
	@userId int, @productId int, @testSubGroup int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Hour int
	SET @Hour = 0

	SELECT * FROM (
	SELECT DISTINCT
		dbo.NusStudentAssign.StudentID As UserID, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID,
		dbo.NurCohortPrograms.Active, dbo.NurProgramProduct.ProductID,
		dbo.Tests.TestID,dbo.Tests.PopWidth,dbo.Tests.PopHeight,dbo.Tests.TestName,dbo.Tests.Url,dbo.Tests.DefaultGroup,
		COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, dbo.NurProductDatesCohort.StartDate) AS StartDate_All,
		COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, dbo.NurProductDatesCohort.EndDate) AS EndDate_All
	FROM   dbo.NurCohort INNER JOIN
	  dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
	  INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
	  INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
	  INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
	  INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID

		LEFT OUTER JOIN dbo.NurProductDatesCohort ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID
			AND  dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
			AND (
			(dbo.NurProductDatesCohort.StartDate < DATEADD(hour, @Hour, GETDATE()))
			AND (dbo.NurProductDatesCohort.EndDate > DATEADD(hour, @Hour, GETDATE()))
			)
		LEFT OUTER JOIN dbo.NurProductDatesGroup ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesGroup.CohortID
			AND dbo.NusStudentAssign.GroupID = dbo.NurProductDatesGroup.GroupID			
			AND dbo.Tests.TestID = dbo.NurProductDatesGroup.ProductID
			AND (
			dbo.NurProductDatesGroup.StartDate < DATEADD(hour, @Hour, GETDATE())
			AND dbo.NurProductDatesGroup.EndDate > DATEADD(hour, @Hour, GETDATE())
			)
		LEFT OUTER JOIN dbo.NurProductDatesStudent ON dbo.NusStudentAssign.CohortID = dbo.NurProductDatesStudent.CohortID
			AND dbo.NusStudentAssign.GroupID = dbo.NurProductDatesStudent.GroupID
			AND dbo.NusStudentAssign.StudentID = dbo.NurProductDatesStudent.StudentID
			AND dbo.Tests.TestID = dbo.NurProductDatesStudent.ProductID
			AND (
			dbo.NurProductDatesStudent.EndDate > DATEADD(hour, @Hour, GETDATE())
			AND dbo.NurProductDatesStudent.StartDate < DATEADD(hour, @Hour, GETDATE())
			)
	WHERE
		dbo.NurProgram.DeletedDate IS NULL AND dbo.NurCohort.CohortStatus = 1 AND dbo.NusStudentAssign.StudentID = @UserID AND dbo.NurCohortPrograms.Active = 1
		AND dbo.Tests.ProductID = @ProductID AND dbo.Tests.TestSubGroup = @TestSubGroup AND dbo.Tests.ActiveTest = 1
	) A WHERE StartDate_All IS NOT NULL AND EndDate_All IS NOT NULL

END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetCaseStudies' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCaseStudies]
GO

CREATE PROCEDURE [dbo].[uspGetCaseStudies]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT CaseID, CaseName, CaseOrder FROM  NurCase
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetFinishedTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetFinishedTests]
GO


CREATE PROCEDURE [dbo].[uspGetFinishedTests]
	@userId int, @productId int, @timeOffset int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@productId != 0)
		SELECT CAST(QInfo.PercentCorrect as varchar(5))+'%'  as PercentCorrect, QInfo.QuestionCount,QInfo.UserTestID,
			UserTestsInfo.TestName, UserTestsInfo.TestStarted, UserTestsInfo.TestID, UserTestsInfo.TestStatus, UserTestsInfo.ProductName,
			UserTestsInfo.QuizOrQBank
		FROM (SELECT (CAST((ISNULL(QCC.QuestionCorrectCount,0)*100.0) / QC.QuestionCount AS numeric(5,0))) AS PercentCorrect,
				QC.QuestionCount,QC.UserTestID
			  FROM (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCount
					FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
					WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.ProductID = @productId AND
						dbo.UserTests.TestStatus = 1
					GROUP BY dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN
				(SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCorrectCount
				 FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
				 WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.ProductID = @productId AND dbo.UserTests.TestStatus = 1
					AND dbo.UserQuestions.Correct = 1
				 GROUP BY dbo.UserQuestions .UserTestID)QCC ON QC.UserTestID = QCC.UserTestID) QInfo INNER JOIN
					(SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,
						DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.UserTests.TestID, dbo.UserTests.TestStatus,
						dbo.Products.ProductName,dbo.UserTests.QuizOrQBank
					 FROM dbo.Tests INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
						INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
						AND (dbo.Tests.ProductID = @productId)
					 WHERE (UserID = @userId) and TestStatus=1 )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID

	ELSE
		SELECT CAST(QInfo.PercentCorrect as varchar(5)) + '%' as PercentCorrect, QInfo.QuestionCount,QInfo.UserTestID,
			UserTestsInfo.TestName, UserTestsInfo.TestStarted, UserTestsInfo.TestID, UserTestsInfo.TestStatus, UserTestsInfo.ProductName,
			UserTestsInfo.QuizOrQBank
		FROM (SELECT (CAST((ISNULL(QCC.QuestionCorrectCount,0)*100.0)/ QC.QuestionCount AS numeric(5,0))) AS PercentCorrect,
				QC.QuestionCount,QC.UserTestID
			  FROM (SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCount
					FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
					WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.TestStatus = 1
					GROUP BY dbo.UserQuestions .UserTestID) QC LEFT OUTER JOIN
				(SELECT dbo.UserQuestions.UserTestID, COUNT(*) AS QuestionCorrectCount
				 FROM dbo.UserQuestions INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
				 WHERE dbo.UserTests.UserID = @userId AND dbo.UserTests.TestStatus = 1 AND dbo.UserQuestions.Correct = 1
				 GROUP BY dbo.UserQuestions .UserTestID)QCC ON QC.UserTestID=QCC.UserTestID) QInfo INNER JOIN
					(SELECT dbo.UserTests.UserTestID, dbo.UserTests.UserID, dbo.Tests.ProductID, dbo.Tests.TestName,
						DATEADD(hour, @timeOffset, TestStarted) as TestStarted,dbo.UserTests.TestID, dbo.UserTests.TestStatus,
						dbo.Products.ProductName,dbo.UserTests.QuizOrQBank
					 FROM dbo.Tests INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
						INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
					 WHERE (UserID = @userId) and TestStatus=1 )UserTestsInfo ON UserTestsInfo.UserTestID=QInfo.UserTestID
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetFutureTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetFutureTests]
GO

CREATE PROCEDURE [dbo].[uspGetFutureTests]
	@userId int, @productId int, @testSubGroup int, @sType int, @timeOffset int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@sType = 0)
	BEGIN
		IF (@productId = 4 OR @productId = 5)
		BEGIN
			SELECT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber,dbo.Tests.ProductID AS TestProductID, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID,
						dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND (dbo.Tests.ProductID = @productId)
					ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
				WHERE V.StartDate_All > DATEADD(hour, @timeOffset, GetDate())
		END
		ELSE
		BEGIN
			SELECT V.* FROM
				(SELECT  Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE(dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE(dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber,dbo.Tests.ProductID AS TestProductID, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID,
						dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND dbo.Tests.TestSubGroup = @testSubGroup AND (dbo.Tests.ProductID = @productId)
					ORDER BY dbo.Tests.TestName ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND dbo.NurProductDatesStudent.Type=0 AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE V.StartDate_All > DATEADD(hour, @timeOffset, GetDate())
		END
	END
	ELSE
	BEGIN
		IF (@productId = 4 OR @productId = 5)
		BEGIN
			SELECT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE(dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE(dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID,
						dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 1) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND (dbo.NurProgramProduct.ProductID = @productId)
					ORDER BY dbo.Tests.TestNumber ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE V.StartDate_All > DATEADD(hour, @timeOffset, GetDate())
		END
		ELSE
		BEGIN
			SELECT V.* FROM
				(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
					Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
					dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
					dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
					COALESCE(dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
					COALESCE(dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
				FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
					(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
						dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
						dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.NusStudentAssign.CohortID, dbo.NusStudentAssign.GroupID,
						dbo.NurCohortPrograms.ProgramID
					FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
						INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
						INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
						INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
						INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID	
						INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
							dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.Tests.ProductID = dbo.NurProductDatesCohort.ProductID AND
							dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
					WHERE (dbo.NurStudentInfo.UserID = @UserID) AND (dbo.NurProgramProduct.Type = 1) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND (dbo.NurProgramProduct.ProductID = @productId)
					ORDER BY dbo.Tests.TestName ASC) AS Cohort_V
				ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND dbo.NurProductDatesStudent.Type=1 AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V
			WHERE V.StartDate_All > DATEADD(hour, @timeOffset, GetDate())
		END
	END
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetHotSpotAnswerByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetHotSpotAnswerByID]
GO

create PROCEDURE [dbo].[uspGetHotSpotAnswerByID]
	@QuestionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   SELECT AnswerChoices.*,Questions.Stimulus FROM AnswerChoices,Questions WHERE Questions.QID=AnswerChoices.QID AND Questions.QID = @QuestionID
END


GO



IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetPercentileRank' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetPercentileRank]
GO

CREATE PROCEDURE [dbo].[uspGetPercentileRank]
	-- Add the parameters for the stored procedure here
	@testId int, @correct int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT PercentileRank
	FROM Norming
	WHERE TestID = @testId AND NumberCorrect = @correct
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetProbability' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetProbability]
GO

CREATE PROCEDURE [dbo].[uspGetProbability]
	-- Add the parameters for the stored procedure here
	@testId int, @correct int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Probability
	FROM Norming
	WHERE TestID = @testId AND NumberCorrect = @correct
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetProgramResults' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetProgramResults]
GO

CREATE PROCEDURE [dbo].[uspGetProgramResults]
	-- Add the parameters for the stored procedure here
	@userTestId int, @chartType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@chartType = 1)
	BEGIN
		SELECT CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5, 0)) AS Total,
               -1 AS N_Correct,
			   -1 AS N_InCorrect,
			   -1 AS N_NAnswered,
			   -1 AS N_CI,
			   -1 AS N_II,
			  -1 AS N_IC
		FROM UserQuestions
		WHERE UserTestId = @userTestId
	END
	ELSE IF (@chartType = 2)
	BEGIN
		SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS N_Correct,
			   SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END) AS N_InCorrect,
			   SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END) AS N_NAnswered,
			   SUM(CASE WHEN AnswerChanges = 'CI' THEN 1 ELSE 0 END) AS N_CI,
			   SUM(CASE WHEN AnswerChanges = 'II' THEN 1 ELSE 0 END) AS N_II,
			   SUM(CASE WHEN AnswerChanges = 'IC' THEN 1 ELSE 0 END) AS N_IC,
				CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5, 0)) AS Total
		FROM UserQuestions
		WHERE UserTestId = @userTestId
	END
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestName' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestName]
GO

CREATE PROCEDURE [dbo].[uspGetTestName]
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TestName
	FROM Tests
	WHERE TestID = @testId
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestsByProductUser' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestsByProductUser]
GO
CREATE PROCEDURE [dbo].[uspGetTestsByProductUser]
	@productId int, @userId int, @hour int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@productId != 0 )
		BEGIN
			SELECT UserTests.UserTestID, Tests.TestName + CAST(' (' as varchar) + CAST(DATEADD(hour, @hour, dbo.UserTests.TestStarted) as varchar) + CAST(')' as varchar) as TestName
			FROM Tests INNER JOIN Products ON Tests.ProductID = Products.ProductID
				INNER JOIN UserTests ON Tests.TestID = UserTests.TestID
			WHERE TestStatus = 1 AND UserID = @userId AND Tests.ProductID = @productId
			ORDER BY TestName
		END
	ELSE
		BEGIN
			SELECT UserTests.UserTestID, Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour,dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
			FROM dbo.Tests INNER JOIN Products ON Tests.ProductID = Products.ProductID
				INNER JOIN UserTests ON Tests.TestID = UserTests.TestID
			WHERE TestStatus = 1 AND UserID = @userId
			ORDER BY TestName
		END
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUntakenTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUntakenTests]
GO

CREATE PROCEDURE [dbo].[uspGetUntakenTests]
	@userId int, @productId int, @testSubGroup int, @timeOffset int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@productId = 4 OR @productId = 5)
	BEGIN
		SELECT distinct V.* FROM
			(SELECT  Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
				Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
				dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
				dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
				COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
				COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
			FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
				(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
					dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
					dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.Tests.ProductID AS TestProductID,dbo.NusStudentAssign.CohortID,
					dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
				FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
					INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
					INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
					INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
					INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
					INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
						dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
						dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
				WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
						(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
						(dbo.Tests.ProductID  = @productId) AND (dbo.Tests.ActiveTest = 1)
				ORDER BY dbo.Tests.TestNumber ASC ) AS Cohort_V
			ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V  WHERE  V.TestID NOT IN
					(SELECT TestID FROM dbo.UserTests WHERE (UserID = @userId) AND (Override = 0)) AND V.StartDate_All < DATEADD(hour, @timeOffset, GetDate()) AND V.EndDate_All > DATEADD(hour, @timeOffset, GetDate())
	END
	ELSE
	BEGIN
		SELECT distinct V.* FROM
			(SELECT Cohort_V.Type, Cohort_V.TestName, Cohort_V.StartDate, Cohort_V.EndDate, Cohort_V.TestID, Cohort_V.TestNumber,
				Cohort_V.TestSubGroup, Cohort_V.ProgramID, Cohort_V.UserID, dbo.NurProductDatesStudent.StartDate AS Student_StartDate,
				dbo.NurProductDatesStudent.EndDate AS Student_EndDate, Cohort_V.CohortID, Cohort_V.GroupID,
				dbo.NurProductDatesGroup.StartDate AS Group_StartDate, dbo.NurProductDatesGroup.EndDate AS Group_EndDate, Cohort_V.ProductID,
				COALESCE (dbo.NurProductDatesStudent.StartDate, dbo.NurProductDatesGroup.StartDate, Cohort_V.StartDate) AS StartDate_All,
				COALESCE (dbo.NurProductDatesStudent.EndDate, dbo.NurProductDatesGroup.EndDate, Cohort_V.EndDate) AS EndDate_All
			FROM dbo.NurProductDatesGroup RIGHT OUTER JOIN
				(SELECT TOP 100 PERCENT dbo.NurStudentInfo.UserID, dbo.NurProgramProduct.Type, dbo.NurProgramProduct.ProductID,
					dbo.Tests.TestName, dbo.NurProductDatesCohort.StartDate, dbo.NurProductDatesCohort.EndDate, dbo.Tests.TestID,
					dbo.Tests.TestNumber, dbo.Tests.TestSubGroup, dbo.Tests.ProductID AS TestProductID,dbo.NusStudentAssign.CohortID,
					dbo.NusStudentAssign.GroupID, dbo.NurCohortPrograms.ProgramID
				FROM dbo.NurCohort INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
					INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
					INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
					INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
					INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
					INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID AND
						dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID AND
						dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID AND
						dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
				WHERE (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurProgramProduct.Type = 0) AND (dbo.NurCohortPrograms.Active = 1) AND
					(dbo.NurCohort.CohortStatus = 1) AND  dbo.Tests.TestSubGroup = @testSubGroup AND
					(dbo.Tests.ProductID  = @productId) AND (dbo.Tests.ActiveTest = 1)
				ORDER BY dbo.Tests.TestName ASC) AS Cohort_V
			ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID AND
				dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID LEFT OUTER JOIN
				dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID AND
				Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID AND
				Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID) AS V WHERE V.TestID NOT IN
					(SELECT TestID FROM dbo.UserTests WHERE (UserID = @userId) AND (Override = 0)) AND V.StartDate_All < DATEADD(hour, @timeOffset, GetDate()) AND V.EndDate_All > DATEADD(hour, @timeOffset, GetDate())
	END
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUserAnswerByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUserAnswerByID]
GO

create PROCEDURE [dbo].[uspGetUserAnswerByID]
	@QuestionID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT * FROM AnswerChoices WHERE QID=@QuestionID
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUserAnswers' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUserAnswers]
GO

Create PROCEDURE [dbo].[uspGetUserAnswers]
	@UserTestID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT * FROM dbo.UserAnswers WHERE   UserTestID= @UserTestID
END




GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUserInfo' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUserInfo]
GO

CREATE PROCEDURE [dbo].[uspGetUserInfo]
	@userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT UserId, FirstName,LastName, UserName, UserPass, NurStudentInfo.Email,TimeZones.Hour AS TimeOffset, dbo.NurStudentInfo.InstitutionID,
		dbo.NusStudentAssign.CohortID,dbo.NusStudentAssign.GroupID,
		COALESCE (dbo.NurCohortPrograms.ProgramID, 0) AS ProgramID, Integreted, ADA, UserStartDate, UserExpireDate,
		EnrollmentId, KaplanUserId, NurInstitution.Ip
	FROM dbo.NusStudentAssign
		INNER JOIN dbo.NurCohortPrograms ON dbo.NusStudentAssign.CohortID = dbo.NurCohortPrograms.CohortID
		INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
		INNER JOIN dbo.NurInstitution ON  dbo.NurStudentInfo.InstitutionID = dbo.NurInstitution.InstitutionID
		INNER JOIN TimeZones ON dbo.NurInstitution.TimeZone = TimeZones.TimeZoneID
		INNER JOIN dbo.NurCohort ON NusStudentAssign.CohortID = dbo.NurCohort.CohortID
	WHERE (dbo.NurStudentInfo.UserStartDate is null or dbo.NurStudentInfo.UserStartDate<getdate())AND
		(dbo.NurStudentInfo.UserExpireDate is null or dbo.NurStudentInfo.UserExpireDate>getdate()) AND
		(dbo.NurCohort.CohortEndDate>getdate())AND (dbo.NurCohort.CohortStartDate<getdate()) AND
		((dbo.NurCohortPrograms.Active = 1) OR (dbo.NurCohortPrograms.Active IS NULL))AND
			(dbo.NurStudentInfo.UserId=@userId)
			AND (dbo.NurStudentInfo.UserDeleteData is null)
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetUserTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetUserTests]
GO


CREATE PROCEDURE [dbo].[uspGetUserTests]
	@userId int, @productId int, @testSubGroup int, @timeOffset int, @testStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF (@productId = 4 OR @productId = 5)
	BEGIN
		SELECT DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.Tests.TestID, SuspendType, UserTestID,
			dbo.Tests.TestName as TN, dbo.Tests.TestNumber
         FROM dbo.Tests INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
			INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
         WHERE UserID = @userId AND TestStatus = @testStatus AND dbo.Tests.ProductID = @productId
			AND dbo.Tests.TestSubGroup = @testSubGroup
         ORDER BY dbo.Tests.TestNumber ASC
	END
	ELSE
	BEGIN
		SELECT DATEADD(hour, @timeOffset, TestStarted) as TestStarted, dbo.Tests.TestID, SuspendType, UserTestID,
			dbo.Tests.TestName as TN, dbo.Tests.TestNumber
         FROM  dbo.Tests INNER JOIN dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID
			INNER JOIN dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
         WHERE UserID = @userId AND TestStatus = @testStatus AND dbo.Tests.ProductID = @productId
			AND dbo.Tests.TestSubGroup = @testSubGroup
         ORDER BY dbo.Tests.TestName ASC
	END
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateQuestionExplanation' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateQuestionExplanation]
GO

CREATE PROCEDURE [dbo].[uspUpdateQuestionExplanation]
	@questionId int, @userTestId int, @timer int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE UserQuestions
	SET TimeSpendForExplanation = @timer
	WHERE QID = @questionId and UserTestID = @userTestId
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateQuestionRemediation' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateQuestionRemediation]
GO


CREATE PROCEDURE [dbo].[uspUpdateQuestionRemediation]
	@questionId int, @userTestId int, @timer int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE UserQuestions
	SET TimeSpendForRemedation = @timer
	WHERE QID = @questionId and UserTestID = @userTestId
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateTestStatus' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateTestStatus]
GO


CREATE PROCEDURE [dbo].[uspUpdateTestStatus]
	@userTestId int, @testStatus int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE UserTests
	SET TestStatus = @testStatus, TestComplited = GetDate()
	WHERE UserTestID = @userTestId
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateUserQuestions' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateUserQuestions]
GO

CREATE PROCEDURE [dbo].[uspUpdateUserQuestions] (@QID int, @UserTestID int, @Correct int,
	@TimeSpendForQuestion int, @TimeSpendForRemediation int, @TimeSpendForExplanation int,
	@AnswerTrack varchar(50), @AnswerChanges char(2), @OrderedIndexes varchar(50))
AS

-- Update questions table with metadata
UPDATE UserQuestions SET Correct = @Correct, TimeSpendForQuestion = @TimeSpendForQuestion,
	AnswerTrack=@AnswerTrack,AnswerChanges=@AnswerChanges,OrderedIndexes=@OrderedIndexes
	WHERE UserTestID = @UserTestID AND QID = @QID

-- Delete previous answers
DELETE UserAnswers WHERE UserTestID = @UserTestID and QID = @QID


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspUpdateUserTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateUserTest]
GO

CREATE PROCEDURE [dbo].[uspUpdateUserTest](@UserTestID int, @SuspendQuestionNumber int,
	@SuspendQID int, @SuspendType char(2), @TimeRemaining varchar(50))
AS

UPDATE UserTests
SET SuspendQuestionNumber=@SuspendQuestionNumber, SuspendQID=@SuspendQID,
	SuspendType=@SuspendType, TimeRemaining=@TimeRemaining
WHERE UserTestID = @UserTestID



GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspTestsExistsByTestIDCohortIDHour' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDCohortIDHour]
GO

CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDCohortIDHour]
	
	@TestID int,
    @CohortID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @ID int;
SELECT @ID = dbo.Tests.TestID
FROM  dbo.Tests INNER JOIN dbo.NurProductDatesCohort ON
dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
WHERE (dbo.Tests.TestID = @TestID) AND (dbo.NurProductDatesCohort.CohortID =@CohortID)
 AND (dbo.NurProductDatesCohort.StartDate < DATEADD(hour, @Hour, GETDATE()))
 AND (dbo.NurProductDatesCohort.EndDate > DATEADD(hour, @Hour, GETDATE()))

RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspTestsExistsByTestIDGroupIDHour' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDGroupIDHour]
GO

CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDGroupIDHour]
	
	@TestID int,
    @GroupID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DECLARE @ID int;
 SELECT  @ID = dbo.Tests.TestID FROM
 dbo.Tests INNER JOIN  dbo.NurProductDatesGroup ON dbo.Tests.TestID =
 dbo.NurProductDatesGroup.ProductID WHERE   (dbo.Tests.TestID = @TestID )
 AND (dbo.NurProductDatesGroup.GroupID = @GroupID) AND
 (dbo.NurProductDatesGroup.StartDate < DATEADD(hour, @Hour, GETDATE()))
 AND (dbo.NurProductDatesGroup.EndDate > DATEADD(hour, @Hour , GETDATE()))


RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspTestsExistsByTestIDUserIDHour' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspTestsExistsByTestIDUserIDHour]
GO

CREATE PROCEDURE [dbo].[uspTestsExistsByTestIDUserIDHour]
	
	@TestID int,
    @UserID int,
	@Hour int
	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
DECLARE @ID int;
SELECT  @ID = dbo.Tests.TestID FROM
dbo.Tests INNER JOIN   dbo.NurProductDatesStudent ON
dbo.Tests.TestID = dbo.NurProductDatesStudent.ProductID WHERE
(dbo.Tests.TestID =  @TestID) AND (dbo.NurProductDatesStudent.EndDate > DATEADD(hour,  @Hour , GETDATE()))
AND  (dbo.NurProductDatesStudent.StartDate < DATEADD(hour, @Hour, GETDATE()))
AND (dbo.NurProductDatesStudent.StudentID = @UserID )



RETURN @@ROWCOUNT

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCheckTestExists' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCheckTestExists]
GO

CREATE PROCEDURE [dbo].[uspCheckTestExists]
 @userId varchar(80), @productId int, @testSubGroup int, @sType int, @timeOffset int, @result int OUTPUT
AS
BEGIN
	 -- SET NOCOUNT ON added to prevent extra result sets from
	 -- interfering with SELECT statements.
	 SET NOCOUNT ON;
	
	 DECLARE @groupId int, @testId int, @cohortId int, @rcount int;
	
	 IF (@sType = 0)
	 BEGIN
		  IF (@testSubGroup != -1 AND @productId = 4)
		   SELECT TOP 1 PERCENT @cohortId = dbo.NusStudentAssign.CohortID, @groupId = dbo.NusStudentAssign.GroupID,
			@testId = dbo.Tests.TestID
		   FROM  dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID
			INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
			INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
			INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
			INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
			INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
			INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
		   WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)
			AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)
			AND dbo.Tests.ProductID = @productId AND dbo.Tests.TestSubGroup = @testSubGroup AND dbo.NurInstitution.Status = '1'
	  ELSE
		   SELECT TOP 1 PERCENT @cohortId = dbo.NusStudentAssign.CohortID, @groupId = dbo.NusStudentAssign.GroupID,
			@testId = dbo.Tests.TestID
		   FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID
			INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
			INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
			INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
			INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
			INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
			INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
		   WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)
			AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)
			AND dbo.Tests.ProductID = @productId  AND dbo.NurInstitution.Status = '1'
	 END
	 ELSE
	 BEGIN
		  IF (@testSubGroup != -1 AND @productId = 4)
		   SELECT TOP 1 PERCENT  @cohortId = dbo.NusStudentAssign.CohortID, @groupId = dbo.NusStudentAssign.GroupID,
			@testId = dbo.Tests.TestID
		   FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID
			INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
			INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
			INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
			INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
			INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
			INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID
		   WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)
			AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)
			AND dbo.NurProgramProduct.ProductID = @productId AND dbo.Tests.TestSubGroup = @testSubGroup AND dbo.NurInstitution.Status = '1'
	  ELSE
		   SELECT TOP 1 PERCENT @cohortId = dbo.NusStudentAssign.CohortID, @groupId = dbo.NusStudentAssign.GroupID,
			@testId = dbo.Tests.TestID
		   FROM dbo.NurInstitution INNER JOIN dbo.NurCohort ON dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID
			INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
			INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
			INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
			INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
			INNER JOIN dbo.NurProgram ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgram.ProgramID
			INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.ProductID
		   WHERE dbo.NurProgram.DeletedDate IS NULL AND (dbo.NurCohort.CohortStatus = 1)
			AND (dbo.NurStudentInfo.UserID = @userId) AND (dbo.NurCohortPrograms.Active = 1)
			AND dbo.NurProgramProduct.ProductID = @productId AND dbo.NurInstitution.Status = '1'
	 END
	
	 IF (@testId != 74)
	 BEGIN
		  SET @result = 1
		  RETURN
	 END
	
	 EXECUTE @rcount = uspTestsExistsByTestIDCohortIDHour @testId, @cohortId, @timeOffset
	 IF @RCount > 0
	 BEGIN
		  SET @result = 1
		  RETURN
	 END
	
	 EXECUTE @rcount = uspTestsExistsByTestIDGroupIDHour @testId, @groupId, @timeOffset
	 IF @rcount > 0
	 BEGIN
		  SET @result = 1
		  RETURN
	 END
	
	 EXECUTE @rcount = uspTestsExistsByTestIDUserIDHour @testId, @userId, @timeOffset
	 IF @rcount > 0
	 BEGIN
		  SET @result = 1
		  RETURN
	 END
	
	 SET @result = 0
	 RETURN
END

GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspLoginUser' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspLoginUser]
GO

Create PROCEDURE [dbo].[uspLoginUser]
	-- Add the parameters for the stored procedure here
	@username varchar(80), @password varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Declare @UserID int,@FirstName nvarchar(80),@LastName nvarchar(80),@KaplanUserID varchar(50), @InstitutionID int,@CohortID int,@ProgramID int, @Ada bit, @TimeOffset int,@EnrollmentID varchar(50),@Ip varchar(250),@GroupID int
	SELECT @UserID =UserID, @FirstName = FirstName,@LastName= LastName,@KaplanUserID = KaplanUserID, @InstitutionID = NurStudentInfo.InstitutionID,@CohortID = NusStudentAssign.CohortID,
		@ProgramID = COALESCE (dbo.NurCohortPrograms.ProgramID, 0) , @Ada = Ada, @TimeOffset = TimeZones.Hour , @EnrollmentID = EnrollmentID,
		@Ip =NurInstitution.Ip,@GroupID = NusStudentAssign.GroupID
	FROM dbo.NusStudentAssign
		INNER JOIN dbo.NurCohortPrograms ON dbo.NusStudentAssign.CohortID = dbo.NurCohortPrograms.CohortID
		INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
		INNER JOIN dbo.NurInstitution ON  dbo.NurStudentInfo.InstitutionID = dbo.NurInstitution.InstitutionID
		INNER JOIN TimeZones ON dbo.NurInstitution.TimeZone = TimeZones.TimeZoneID
		INNER JOIN dbo.NurCohort ON NusStudentAssign.CohortID = dbo.NurCohort.CohortID
	WHERE (dbo.NurStudentInfo.UserStartDate is null or dbo.NurStudentInfo.UserStartDate<getdate())AND
		(dbo.NurStudentInfo.UserExpireDate is null or dbo.NurStudentInfo.UserExpireDate>getdate()) AND
		(dbo.NurCohort.CohortEndDate>getdate())AND (dbo.NurCohort.CohortStartDate<getdate()) AND
			((dbo.NurCohortPrograms.Active = 1) OR (dbo.NurCohortPrograms.Active IS NULL))AND
			(dbo.NurStudentInfo.UserPass=@password AND dbo.NurStudentInfo.UserName=@username )
			AND (dbo.NurStudentInfo.UserDeleteData is null)

Declare @TestExistsIntegrated bit,@TestExistsFocussed bit,@TestExistsNclex bit,@TestExistsQbank bit,
@TestExistsQbankSample bit,@TestExistsTimedQbank bit,@TestExistsDiagnostic bit,@TestExistsReadiness bit,
@TestExistsDiagnosticResult bit,@TestExistsReadinessResult bit

EXECUTE [dbo].[uspCheckTestExists] @UserID,1,1,0,@TimeOffset,@TestExistsIntegrated OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,3,1,1,@TimeOffset,@TestExistsFocussed OUTPUT
IF @TestExistsFocussed = 0 EXECUTE [dbo].[uspCheckTestExists] @UserID,3,1,0,@TimeOffset,@TestExistsFocussed OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,-1,0,@TimeOffset,@TestExistsNclex OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,3,0,@TimeOffset,@TestExistsQbank OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,2,0,@TimeOffset,@TestExistsQbankSample OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,1,0,@TimeOffset,@TestExistsTimedQbank OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,4,0,@TimeOffset,@TestExistsDiagnostic OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,5,0,@TimeOffset,@TestExistsReadiness OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,6,0,@TimeOffset,@TestExistsDiagnosticResult OUTPUT
EXECUTE [dbo].[uspCheckTestExists] @UserID,4,7,0,@TimeOffset,@TestExistsReadinessResult OUTPUT

SELECT @UserID AS UserID,@FirstName AS FirstName,@LastName AS LastName,@username AS UserName, @KaplanUserID AS KaplanUserID,@InstitutionID AS InstitutionID
,@CohortID AS CohortID,@ProgramID AS ProgramID, @Ada AS Ada, @TimeOffset AS TimeOffset,@EnrollmentID AS EnrollmentID,@Ip AS Ip
,@TestExistsIntegrated AS TestExistsIntegrated,@TestExistsFocussed AS TestExistsFocussed,@TestExistsNclex AS TestExistsNclex
,@TestExistsQbank AS TestExistsQbank,@TestExistsQbankSample AS TestExistsQbankSample,@TestExistsTimedQbank AS TestExistsTimedQbank
,@TestExistsDiagnostic AS TestExistsDiagnostic,@TestExistsReadiness AS TestExistsReadiness
,@TestExistsDiagnosticResult AS TestExistsDiagnosticResult ,@TestExistsReadinessResult AS TestExistsReadinessResult,@GroupID AS GroupID


END

GO


GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetListOfItemsForTestForUserI' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetListOfItemsForTestForUserI]
GO
CREATE PROCEDURE [dbo].[uspGetListOfItemsForTestForUserI]
@UserTestID int, @TypeOfFileID varchar(500)

AS

SELECT dbo.Questions.QID, dbo.Questions.QuestionID, dbo.Questions.QuestionType,
	dbo.Questions.RemediationID, dbo.Remediation.TopicTitle, dbo.UserQuestions.QuestionNumber, dbo.Questions.TypeOfFileID,
	dbo.UserQuestions.TimeSpendForQuestion,dbo.UserQuestions.TimeSpendForRemedation, dbo.UserQuestions.Correct,
	dbo.Questions.LevelOfDifficultyID AS LevelOfDifficulty, dbo.Questions.NursingProcessID AS NursingProcess,
	dbo.Questions.ClinicalConceptsID AS ClinicalConcept, dbo.Questions.DemographicID AS Demographic,
	dbo.Questions.CriticalThinkingID AS CriticalThinking, dbo.Questions.SpecialtyAreaID AS SpecialtyArea,
	dbo.Questions.SystemID AS Systems, dbo.Questions.CognitiveLevelID AS CognitiveLevel,
	dbo.Questions.ClientNeedsID AS ClientNeeds, dbo.Questions.ClientNeedsCategoryID AS ClientNeedCategory
FROM  dbo.UserQuestions INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID
			LEFT OUTER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
WHERE  (dbo.UserQuestions.UserTestID = @UserTestID) AND TypeOfFileID=@TypeOfFileID
ORDER BY dbo.UserQuestions.QuestionNumber


GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCheckProbabilityExists' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCheckProbabilityExists]
GO


CREATE PROCEDURE [dbo].[uspCheckProbabilityExists]
	-- Add the parameters for the stored procedure here
	@testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT Probability FROM Norming	WHERE TestID = @testId
END

GO
/****** Object:  StoredProcedure [dbo].[uspGetAllCategories]    Script Date: 02/15/2011 18:08:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAllCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAllCategories]
GO

CREATE PROCEDURE [dbo].[uspGetAllCategories]

AS

SELECT CategoryID, TableName, OrderNumber FROM Category
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspCheckExistCaseModuleStudent' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCheckExistCaseModuleStudent]
GO
CREATE PROCEDURE [dbo].[uspCheckExistCaseModuleStudent]
	
	@CID int,
	@MID int,
	@SID varchar(50)

	
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 SELECT * FROM CaseModuleScore  WHERE  StudentID =@SID AND ModuleID = @MID AND CaseID = @CID

	SET NOCOUNT OFF
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetCaseStudyResultModuleScore' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCaseStudyResultModuleScore]
GO

CREATE PROCEDURE [dbo].[uspGetCaseStudyResultModuleScore]	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.CaseModuleScore WHERE ModuleStudentID = IDENT_CURRENT('dbo.CaseModuleScore')
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetCaseStudyResultSubCategoryScore' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCaseStudyResultSubCategoryScore]
GO

CREATE PROCEDURE [dbo].[uspGetCaseStudyResultSubCategoryScore]	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.CaseSubcategory WHERE ID = IDENT_CURRENT('dbo.CaseSubCategory')
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspInsertSubCategory' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertSubCategory]
GO

CREATE PROCEDURE [dbo].[uspInsertSubCategory]
	@ModuleStudentID int,
	@SubcategoryID int,
	@CategoryName varchar(50),
	@Correct int,
	@Total int,
	@CategoryID int
AS
BEGIN
	
	SET NOCOUNT ON;   	
	Insert into dbo.CaseSubCategory(ModuleStudentID,SubcategoryID,CategoryName,Correct,Total,CategoryID)
	values(@ModuleStudentID,@SubcategoryID,@CategoryName,@Correct,@Total,@CategoryID)
END


GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetProgramResultsByNorm' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetProgramResultsByNorm]
GO

CREATE PROCEDURE [dbo].[uspGetProgramResultsByNorm]
	-- Add the parameters for the stored procedure here
	@UserTestID int, @testId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   CREATE TABLE #NormResults (
Correct_N int,
Total int,
ItemText varchar(500),
Norm real,
ChartType nvarchar(50),
UserTestID int

 )
    --LevelOfDifficulty
    -- create temp table for Norming information
	SELECT Norm.Norm, LevelOfDifficulty.LevelOfDifficultyID AS ID,Norm.ChartType INTO #lodNorm1
	FROM Norm INNER JOIN LevelOfDifficulty ON
		Norm.ChartID = LevelOfDifficulty.LevelOfDifficultyID
	WHERE Norm.TestID = @testId AND Norm.ChartType = 'LevelOfDifficulty'

  INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
	SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		LevelOfDifficulty.LevelOfDifficulty as ItemText, B.Norm , ISNULL(B.ChartType,'LevelOfDifficulty') AS ChartType,UserQuestions.UserTestID
	FROM UserQuestions INNER JOIN Questions ON UserQuestions.QID = Questions.QID
		LEFT OUTER JOIN LevelOfDifficulty ON Questions.LevelOfDifficultyID = LevelOfDifficulty.LevelOfDifficultyID
		LEFT OUTER JOIN #lodNorm1 B ON B.ID = Questions.LevelOfDifficultyID
		WHERE (UserQuestions.UserTestID = @UserTestID)
		GROUP BY LevelOfDifficulty.LevelOfDifficulty, LevelOfDifficulty.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID
		ORDER BY LevelOfDifficulty.OrderNumber

	DROP TABLE #lodNorm1
--NursingProcess
	SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID AS ID ,ChartType INTO #lodNorm2 FROM dbo.Norm INNER JOIN
                dbo.NursingProcess ON
                dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID
                WHERE (dbo.Norm.TestID =  @testId )  and (ChartType='NursingProcess')

             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
 			SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText, B.Norm , ISNULL(B.ChartType,'NursingProcess') AS ChartType ,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
                LEFT OUTER JOIN #lodNorm2 B ON B.ID = dbo.Questions.NursingProcessID
               WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.NursingProcess.NursingProcess,dbo.NursingProcess.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID
                ORDER BY dbo.NursingProcess.OrderNumber

       DROP TABLE #lodNorm2
  --ClinicalConcept
    SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID AS ID,ChartType  INTO #lodNorm3
                FROM dbo.Norm INNER JOIN
                dbo.ClinicalConcept ON
                dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID
                WHERE (dbo.Norm.TestID = @testId) and (ChartType= 'ClinicalConcept')

       INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
	    SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText, B.Norm ,ISNULL(B.ChartType,'ClinicalConcept') AS ChartType,UserQuestions.UserTestID
                 FROM  dbo.UserQuestions INNER JOIN
				 dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                 dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
                 LEFT OUTER JOIN #lodNorm3 B ON B.ID = dbo.Questions.ClinicalConceptsID
                WHERE (UserQuestions.UserTestID = @UserTestID)
                 GROUP BY dbo.ClinicalConcept.ClinicalConcept,dbo.ClinicalConcept.OrderNumber, B.Norm ,B.ChartType,UserQuestions.UserTestID
                 ORDER BY dbo.ClinicalConcept.OrderNumber

      DROP TABLE #lodNorm3

--ClientNeeds
      	SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID AS ID,ChartType INTO #lodNorm4
                FROM dbo.Norm INNER JOIN
                dbo.ClientNeeds ON  dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID
                WHERE (dbo.Norm.TestID = @testId)  and (ChartType='ClientNeeds' )

          INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText, B.Norm ,ISNULL(B.ChartType,'ClientNeeds') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
                 LEFT OUTER JOIN #lodNorm4 B ON B.ID = dbo.Questions.ClientNeedsID
               WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,B.Norm ,B.ChartType,UserQuestions.UserTestID
                ORDER BY dbo.ClientNeeds.ClientNeedsID

DROP TABLE #lodNorm4

--ClientNeedCategory

			SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID AS ID,ChartType INTO #lodNorm5
                FROM dbo.Norm INNER JOIN
                dbo.ClientNeedCategory ON
                dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID
                WHERE (dbo.Norm.TestID =@testId)  and (ChartType= 'ClientNeedCategory')

             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText, B.Norm ,ISNULL(B.ChartType,'ClientNeedCategory') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
                 LEFT OUTER JOIN #lodNorm5 B ON B.ID = dbo.Questions.ClientNeedsCategoryID
               WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,B.Norm,B.ChartType,UserQuestions.UserTestID
                ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID

DROP TABLE #lodNorm5
--Demographic
    	 SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID AS ID ,ChartType INTO #lodNorm6
                FROM dbo.Norm INNER JOIN
                dbo.Demographic ON
                dbo.Norm.ChartID = dbo.Demographic.DemographicID
              WHERE Norm.TestID = @testId AND Norm.ChartType = 'Demographic'

             INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			 SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Demographic.Demographic as ItemText, B.Norm ,ISNULL(B.ChartType,'Demographic') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
	             LEFT OUTER JOIN #lodNorm6 B ON B.ID = dbo.Questions.DemographicID
                WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.Demographic.Demographic,dbo.Demographic.OrderNumber, B.Norm,B.ChartType,UserQuestions.UserTestID
                ORDER BY dbo.Demographic.OrderNumber

DROP TABLE #lodNorm6
--CognitiveLevel
	SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID AS ID ,ChartType INTO #lodNorm7
                FROM dbo.Norm INNER JOIN
                dbo.CognitiveLevel ON
                dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'CognitiveLevel'

            INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText, B.Norm ,ISNULL(B.ChartType,'CognitiveLevel') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
                LEFT OUTER JOIN #lodNorm7 B ON B.ID = dbo.Questions.CognitiveLevelID
               WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,B.Norm,B.ChartType,UserQuestions.UserTestID
                ORDER BY dbo.CognitiveLevel.CognitiveLevelID
DROP TABLE #lodNorm7

--CriticalThinking

	 SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID AS ID ,ChartType INTO #lodNorm8
                FROM dbo.Norm INNER JOIN
                dbo.CriticalThinking ON
                dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'CriticalThinking'

              INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			 SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText, B.Norm ,ISNULL(B.ChartType,'CriticalThinking') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
                LEFT OUTER JOIN #lodNorm8 B ON B.ID = dbo.Questions.CriticalThinkingID
             WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.CriticalThinking.CriticalThinking, B.Norm,B.ChartType,UserQuestions.UserTestID

DROP TABLE #lodNorm8

--SpecialtyArea

	SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID AS ID ,ChartType INTO #lodNorm9
                FROM dbo.Norm INNER JOIN
                dbo.SpecialtyArea ON
                dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID
               WHERE Norm.TestID = @testId AND Norm.ChartType = 'SpecialtyArea'

               INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
			 SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText, B.Norm ,ISNULL(B.ChartType,'SpecialtyArea') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
                 LEFT OUTER JOIN #lodNorm9 B ON B.ID = dbo.Questions.SpecialtyAreaID
              WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.SpecialtyArea.SpecialtyArea, B.Norm  ,B.ChartType,UserQuestions.UserTestID
DROP TABLE #lodNorm9
--System

 SELECT dbo.Norm.Norm, dbo.Systems.SystemID  AS ID ,ChartType INTO #lodNorm10
                FROM dbo.Norm INNER JOIN
                dbo.Systems ON
                dbo.Norm.ChartID = dbo.Systems.SystemID
                WHERE Norm.TestID = @testId AND Norm.ChartType = 'System'

            INSERT  INTO #NormResults (Correct_N,Total,ItemText, Norm,ChartType, UserTestID )
            SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
				dbo.Systems.System as ItemText,
				B.Norm ,ISNULL(B.ChartType,'System') AS ChartType,UserQuestions.UserTestID
                FROM  dbo.UserQuestions INNER JOIN
                dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
                dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
                LEFT OUTER JOIN #lodNorm10 B ON B.ID = dbo.Questions.SystemID
              WHERE (UserQuestions.UserTestID = @UserTestID)
                GROUP BY dbo.Systems.System, B.Norm,B.ChartType,UserQuestions.UserTestID

DROP TABLE #lodNorm10
       Select * from #NormResults
       Drop TABLE  #NormResults

END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestsByUserProductSubGroup' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestsByUserProductSubGroup]
GO


CREATE PROCEDURE [dbo].[uspGetTestsByUserProductSubGroup]
(
	@userId int,
	@productId int,
	@testSubGroup int,
	@timeOffset int
)
AS

BEGIN
	SET NOCOUNT ON;

    SELECT UserTestID,
		DATEADD(hour, @timeOffset, TestStarted) AS TestStarted
	FROM dbo.UserTests
		INNER JOIN dbo.Tests
		ON dbo.Tests.TestID = dbo.UserTests.TestID
    WHERE UserID = @userId
		AND dbo.Tests.ProductID = @productId
		AND dbo.Tests.TestSubGroup = @testSubGroup
	ORDER BY TestStarted ASC

	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetStudentTestCharacteristics' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetStudentTestCharacteristics]
GO



CREATE PROCEDURE [dbo].[uspGetStudentTestCharacteristics]
	-- Add the parameters for the stored procedure here
	@testId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	 SELECT Category.TableName FROM  TestCategory INNER JOIN
	 Category ON TestCategory.CategoryID = Category.CategoryID
	 WHERE Student=1 AND TestID=@TestID ORDER BY Category.OrderNumber

	SET NOCOUNT OFF
END
GO

/****** Object:  StoredProcedure [dbo].[uspGetDetailsForCohortByTest]    Script Date: 03/03/2011 12:09:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDetailsForCohortByTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetDetailsForCohortByTest]
GO
/****** Object:  StoredProcedure [dbo].[uspGetDetailsForCohortByTest]    Script Date: 03/03/2011 12:09:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetDetailsForCohortByTest]
@TestIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@GroupIds VARCHAR(MAX),
@InstitutionId int,
@ProductIds VARCHAR(MAX)
AS
BEGIN

	 DECLARE @TestXml xml,@CohortXml xml,@GroupXml xml,@ProductXml xml
	 DECLARE @TestName as VARCHAR(MAX),@CohortName as VARCHAR(MAX),@GroupName as VARCHAR(MAX),@ProductName as VARCHAR(MAX)
		
   	 SET @TestName = '<R><N>' + REPLACE(@TestIds,'|','</N><N>') + '</N></R>';
	 SET @CohortName = '<R><N>' + REPLACE(@CohortIds,'|','</N><N>') + '</N></R>';
	 SET @GroupName = '<R><N>' + REPLACE(@GroupIds,'|','</N><N>') + '</N></R>';
     SET @ProductName = '<R><N>' + REPLACE(@ProductIds,'|','</N><N>') + '</N></R>';
	 SET @TestXml = (select cast(@TestName as xml))
	 SET @CohortXml = (select cast(@CohortName as xml))
	 SET @GroupXml = (select cast(@GroupName as xml))
	 SET @ProductXml = (select cast(@ProductName as xml))
		

      SELECT Cast (100.0*SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)/ SUM(1) as decimal(4,1)) AS Percantage,
      dbo.Tests.TestID,dbo.Tests.ProductID,dbo.NurCohort.InstitutionID,dbo.Tests.TestName,
      COUNT( DISTINCT dbo.UserTests.UserID) as NStudents,
      dbo.NurCohort.CohortName CohortName,
      dbo.NurCohort.CohortId,
      ISNULL(N.Norm,0) NormedPercCorrect
      FROM   dbo.UserQuestions
      INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
      INNER JOIN dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID
      INNER JOIN dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID
      INNER JOIN Products P ON Tests.ProductId = P.ProductId
      INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
      INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
      LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
      LEFT JOIN dbo.Norm N ON dbo.Tests.TestId = N.TestId AND N.ChartType = 'OverAll' AND N.ChartId = 0
      WHERE  dbo.NurCohort.InstitutionID = @InstitutionId AND TestStatus = 1
            AND ((@TestIds <> '0' AND dbo.UserTests.TestId IN ( select c.value('(.)[1]','char(5)') [col] from @TestXml.nodes('//N') as tab(c)))
				OR @TestIds = '0')
            AND ((@CohortIds <> '0' AND dbo.NurCohort.CohortID IN ( select c.value('(.)[1]','char(5)') [col] from @CohortXml.nodes('//N') as tab(c)))
            OR (@CohortIds = '0'))
            AND ((@GroupIds <> '' AND G.GroupID IN ( select c.value('(.)[1]','char(5)') [col] from @GroupXml.nodes('//N') as tab(c)))
            OR (@GroupIds = ''))
			AND ((@ProductIds <> '0' AND dbo.Tests.ProductID IN ( select c.value('(.)[1]','char(5)') [col] from @ProductXml.nodes('//N') as tab(c)))
            OR (@ProductIds = '0'))
        GROUP BY dbo.NurCohort.InstitutionID,dbo.Tests.ProductID,dbo.Tests.TestID,dbo.Tests.TestName
                  ,dbo.NurCohort.CohortName ,dbo.NurCohort.CohortId,ISNULL(N.Norm,0)

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]
GO

CREATE function [dbo].[UFNReturnTotalCorrectPercentByUserIDTestID]
(@UserTestID int )
RETURNS INT
As
begin
	DECLARE @Result int
	
	SELECT @Result = ISNULL(sum(CASE WHEN correct=1 THEN 1 ELSE 0 END),0)
	FROM dbo.UserQuestions
	Where UserTestID = @UserTestID
	group by UserTestID

	RETURN @Result
end


GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnTotalPercentByUserIDTestID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UFNReturnTotalPercentByUserIDTestID]
GO

CREATE function [dbo].[UFNReturnTotalPercentByUserIDTestID]
(@UserTestID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT @Result = ISNULL(count(correct),0)
FROM dbo.UserQuestions
Where UserTestID = @UserTestID
group by UserTestID

RETURN @Result
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]
GO

Create function [dbo].[UFNReturnPercentileRankByTestIDNumberCorrect]
(@TestID int,
@NumberCorrect float  )
Returns float
AS
BEGIN
	DECLARE @Result float

	select @Result = ISNULL(PercentileRank,0) from dbo.Norming
	where TestID=@TestID AND NumberCorrect=@NumberCorrect

	RETURN @Result
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UFNCheckPercentileRankExistForTest]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[UFNCheckPercentileRankExistForTest]
GO

create function [dbo].[UFNCheckPercentileRankExistForTest]
(@TestID int)
RETURNS INT
AS
BEGIN

DECLARE @Result int

	SELECT @Result = ISNULL(PercentileRank,0) FROM Norming WHERE TestID=@TestID

	RETURN @Result

END

GO

/****** Object:  StoredProcedure [dbo].[uspGetAdminMissionRecipients]    Script Date: 03/04/2011 15:12:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminMissionRecipients]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAdminMissionRecipients]
GO

CREATE PROCEDURE [dbo].[uspGetAdminMissionRecipients]
 -- Add the parameters for the stored procedure here
 @emailMissionId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

		--SelectionLevel Definition
        --Institution = 1,
        --AdminUser = 5

 -- get Admin level data
 SELECT C.Email, 5 AS SelectionLevel, CONVERT(varchar(300), C.FirstName + ' ' + C.LastName) COLLATE database_default AS Name
 FROM EmailMission A JOIN EmailPerson B ON A.EmailMissionId = B.EmailMissionId
  JOIN NurAdmin C ON B.PersonID = C.UserID
 WHERE A.EmailMissionId = @emailMissionId AND C.AdminDeleteData IS NULL

 UNION ALL

 -- get institution level data
 SELECT F.Email, 1 AS SelectionLevel, C.InstitutionName AS Name
 FROM EmailMission A JOIN EmailInstitution B ON A.EmailMissionId = B.EmailMissionId
  JOIN NurAdminInstitution D ON D.InstitutionID = B.InstitutionID
  JOIN NurInstitution C ON B.InstitutionID = C.InstitutionID
  JOIN NurAdmin F ON D.AdminID = F.UserID
 WHERE A.EmailMissionId = @emailMissionId

END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedsCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetClientNeedsCategory]
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsCategory]
AS
SELECT ClientNeedsID AS [Id], ClientNeeds AS [Description] FROM ClientNeeds

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNursingProcessCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNursingProcessCategory]
GO

CREATE PROCEDURE [dbo].[uspGetNursingProcessCategory]
AS

SELECT NursingProcessID AS [Id], NursingProcess AS [Description] FROM NursingProcess

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCriticalThinkingCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCriticalThinkingCategory]
GO

CREATE PROCEDURE [dbo].[uspGetCriticalThinkingCategory]
AS

SELECT CriticalThinkingID AS [Id], CriticalThinking AS [Description] FROM CriticalThinking

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClinicalConceptCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetClinicalConceptCategory]
GO

CREATE PROCEDURE [dbo].[uspGetClinicalConceptCategory]
AS
SELECT ClinicalConceptID AS [Id], ClinicalConcept AS [Description] FROM ClinicalConcept

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDemographicCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetDemographicCategory]
GO

CREATE PROCEDURE [dbo].[uspGetDemographicCategory]
AS
SELECT DemographicID AS [Id], Demographic AS [Description] FROM Demographic

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCognitiveLevelCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCognitiveLevelCategory]
GO

CREATE PROCEDURE [dbo].[uspGetCognitiveLevelCategory]
AS
SELECT CognitiveLevelID AS [Id], CognitiveLevel AS [Description] FROM CognitiveLevel

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSpecialtyAreaCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetSpecialtyAreaCategory]
GO

CREATE PROCEDURE [dbo].[uspGetSpecialtyAreaCategory]
AS
SELECT SpecialtyAreaID AS [Id], SpecialtyArea AS [Description] FROM SpecialtyArea

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSystemsCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetSystemsCategory]
GO

CREATE PROCEDURE [dbo].[uspGetSystemsCategory]
AS
SELECT SystemID AS [Id], [System] AS [Description] FROM Systems

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLevelOfDifficultyCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetLevelOfDifficultyCategory]
GO

CREATE PROCEDURE [dbo].[uspGetLevelOfDifficultyCategory]
AS
SELECT LevelOfDifficultyID AS [Id], LevelOfDifficulty AS [Description] FROM LevelOfDifficulty
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetClientNeedCategoryCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetClientNeedCategoryCategory]
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedCategoryCategory]
AS
SELECT ClientNeedCategoryID AS [Id]
	, ClientNeedCategory AS [Description]
	, ClientNeedID AS [ParentId]
FROM ClientNeedCategory
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateQBankTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateQBankTest]
GO

CREATE PROCEDURE [dbo].[uspCreateQBankTest]
	@userId int, @productId int, @programId int, @timedTest int,
	@tutorMode int, @reuseMode int, @numberOfQuestions int, @testId int,
	@correct int, @options varchar(500)
AS

	-- Gokul - 3/17/2011
	-- This SP is created as a patch for 4/5/2011 release. Functionality has to be merged with uspCreateTest later

	DECLARE @cohortId int, @institutionId int, @scramble int, @timeRemaining int

	SELECT  @institutionId = InstitutionID, @cohortId=CohortID,
		@scramble = 1, @timeRemaining = @numberOfQuestions * 72
	FROM NurStudentInfo LEFT JOIN dbo.NusStudentAssign
		ON NurStudentInfo.UserID = NusStudentAssign.StudentID
	WHERE UserID = @userId

	DECLARE @testNumber int, @userTestId int, @QuestionID int, @QID int, @QuestionNumber int

	-- Create temp table for iteration of questions
	CREATE TABLE #questionTbl (QuestionID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, QID int, QuestionNumber int)

-- Check for existing test
	SET @testNumber = (SELECT MAX(TestNumber) FROM dbo.UserTests WHERE UserID = @userId AND TestID = @testId)
	IF(@testNumber IS NULL)
		SET @testNumber = 1
	ELSE
		SET @testNumber = @testNumber + 1

	BEGIN TRANSACTION
	-- Create the new test instance
	INSERT INTO UserTests
		(UserId, TestId, TestNumber, CohortId, InsitutionID, ProductId, ProgramId, TestStarted, TestStatus,
			QuizOrQbank, TimedTest, TutorMode, ReusedMode, NumberOfQuestions, TestName, SuspendQuestionNumber,
			TimeRemaining, SuspendType, SuspendQID)
	VALUES
		(@userId, @testId, @testNumber, @cohortId, @institutionId, @productId, @programId, GetDate(), 0,
			'B', @timedTest, @tutorMode, @reuseMode, @numberOfQuestions, '', 0, @timeRemaining,
			'01', 0)

	IF @@ERROR <> 0
		BEGIN
			-- Rollback transaction
			ROLLBACK

			-- Raise error and return
			RAISERROR('Error in inserting TestInstance into UserTests', 16, 1)
			RETURN
		END

	-- Get the user instance id
	SET @userTestId = SCOPE_IDENTITY()

	INSERT INTO #questionTbl
		EXEC uspGetTestQuestionsQbank @numberOfQuestions, @testId, @userId, @institutionId,
			@programId, @cohortId, @correct, @reuseMode, @options

	IF @@ERROR <> 0
		BEGIN
			-- Rollback transaction
			ROLLBACK

			-- Raise error and return
			RAISERROR('Error in inserting TestQuestions into temp table', 16, 1)
			RETURN
		END

	DECLARE QS CURSOR FOR
	SELECT QuestionID, QID, QuestionNumber
	FROM #questionTbl

	OPEN QS
	FETCH QS INTO @QuestionID, @QID, @QuestionNumber

	WHILE @@FETCH_STATUS = 0
		BEGIN
			-- check for proper ordering
			IF @scramble = 1
				SET @QuestionNumber = @QuestionID

			-- insert the question
			EXEC uspInsertTestQuestion @QID, @userTestId, @QuestionNumber

			FETCH QS INTO @QuestionID, @QID, @QuestionNumber
		END

	IF @@ERROR <> 0
		BEGIN
			-- Rollback transaction
			ROLLBACK

			-- Raise error and return
			RAISERROR('Error in inserting TestQuestions into UserTestQuestions', 16, 1)
			RETURN
		END

	DROP TABLE #questionTbl

	CLOSE QS
	DEALLOCATE QS

	COMMIT TRANSACTION

	SELECT @userTestId AS UserTestID, @TimeRemaining AS TimeRemaining

GO


/****** Object:  StoredProcedure [dbo].[ReturnStudentSummaryByQuestionHeader]    Script Date: 03/17/2011 19:10:18 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnStudentSummaryByQuestionHeader]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[ReturnStudentSummaryByQuestionHeader]

GO

/****** Object:  StoredProcedure [dbo].[ReturnStudentSummaryByQuestion]    Script Date: 03/17/2011 19:10:18 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnStudentSummaryByQuestion]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[ReturnStudentSummaryByQuestion]

GO


/****** Object:  StoredProcedure [dbo].[uspGetTestsByProdCohortUserIds]    Script Date: 03/17/2011 15:58:29 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByProdCohortUserIds]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspGetTestsByProdCohortUserIds]

GO

CREATE PROCEDURE [dbo].[uspGetTestsByProdCohortUserIds]
@tProductIds nVarchar(2000),
@tCohortIds nVarchar(2000),
@tStudentIds nVarchar(2000),
@groupIds VARCHAR(MAX),
@institutionId INT
AS
BEGIN

 SELECT DISTINCT Tests.TestName, UserTests.TestID
	From UserTests
	Join Tests on UserTests.TestID = Tests.TestID AND UserTests.TestStatus = 1
	INNER JOIN Products P ON Tests.ProductId = P.ProductId  AND P.ProductId = @tProductIds
	LEFT JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
	LEFT JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
	LEFT JOIN dbo.NurCohort C ON SA.CohortID = C.CohortID
	LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
	Where ((@tProductIds <> '' AND Tests.ProductID in (select * from dbo.funcListToTableInt(@tProductIds,'|'))) or (@tProductIds=''))
	and ((@tCohortIds <> '' AND UserTests.CohortID in (select * from dbo.funcListToTableInt(@tCohortIds,'|'))) or (@tCohortIds=''))
	and ((@tStudentIds <> '' AND UserTests.UserId in (select * from dbo.funcListToTableInt(@tStudentIds,'|'))) or (@tStudentIds = ''))
	AND ((@GroupIds <> '' AND G.GroupId IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|'))) OR @GroupIds = '')
	AND UserTests.InsitutionID = @institutionId

END


GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationExplainationByID]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspGetRemediationExplainationByID]

GO

CREATE PROCEDURE dbo.uspGetRemediationExplainationByID
@RemediationId INT
AS

BEGIN
	SET NOCOUNT ON;
		SELECT TOP 1 ISNULL(explanation,'')
		FROM Remediation
		WHERE RemediationID = @RemediationId
	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnSummaryPerformanceByQuestionReport]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspReturnSummaryPerformanceByQuestionReport]
GO

CREATE PROCEDURE [dbo].[uspReturnSummaryPerformanceByQuestionReport]
    @CohortID varchar(500),
    @ProductID int,
    @TestID int

AS
	SELECT Q.QuestionID,
		Q.QuestionType,
		Answer = CASE
			WHEN Q.QuestionType IN ('05') THEN '1'
			WHEN AC.AIndex = 'A' THEN '1'
			WHEN AC.AIndex = 'B' THEN '2'
			WHEN AC.AIndex = 'C' THEN '3'
			WHEN AC.AIndex = 'D' THEN '4'
			WHEN AC.AIndex = 'E' THEN '5'
			WHEN AC.AIndex = 'F' THEN '6'
			ELSE '7'
		END,
		SUM(CASE WHEN UA.AIndex = 'A' THEN 1 ELSE 0 END) AS Total1,
		SUM(CASE WHEN UA.AIndex = 'B' THEN 1 ELSE 0 END) AS Total2,
		SUM(CASE WHEN UA.AIndex = 'C' THEN 1 ELSE 0 END) AS Total3,
		SUM(CASE WHEN UA.AIndex = 'D' THEN 1 ELSE 0 END) AS Total4,
		SUM(CASE WHEN UA.AIndex = 'E' THEN 1 ELSE 0 END) AS Total5,
		SUM(CASE WHEN UA.AIndex = 'F' THEN 1 ELSE 0 END) AS Total6,
		COUNT(*) AS TotalN,
		SUM(CASE WHEN UQ.Correct = 1 THEN 1 ELSE 0 END)  AS Total#Correct,
		SUM(CASE WHEN UQ.Correct = 1 THEN 0 ELSE 1 END)  AS Total#Wrong,
		CAST(SUM(CASE WHEN UQ.Correct = 1 THEN 1 ELSE 0 END)* 100.0 / COUNT(*) AS decimal(8, 1)) AS CorrectPercent,
		COUNT (DISTINCT UT.UserID) AS StudentNumber
	FROM dbo.Tests AS T
		INNER JOIN dbo.UserTests AS UT ON T.TestID = UT.TestID
		INNER JOIN dbo.UserQuestions AS UQ ON UT.UserTestID = UQ.UserTestID
		INNER JOIN dbo.Questions AS Q ON UQ.QID = Q.QID
		LEFT JOIN dbo.AnswerChoices AS AC ON Q.QID = AC.QID AND Q.QuestionType NOT IN ('05')
		LEFT JOIN dbo.UserAnswers AS UA  ON UQ.QID = UA.QID AND UQ.UserTestID = UA.UserTestID
	WHERE ((@ProductID <> 0 AND T.ProductID = @ProductID) OR @ProductID = 0)
		AND (UT.CohortID IN (SELECT value FROM dbo.funcListToTableInt(@CohortID,'|')))
		AND T.TestID = @TestID
	GROUP BY UQ.QID, UT.TestID, T.TestName, Q.QuestionID, AC.AIndex, AC.Correct, Q.QuestionType

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestForTheProduct]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspReturnTestForTheProduct]

GO

CREATE PROCEDURE [dbo].[uspReturnTestForTheProduct]
(
	   @ProductID int
)
AS
	SET NOCOUNT ON
	SELECT 	*
	FROM  Tests
	WHERE
		((@ProductID <> 0 AND ProductID= @ProductID) OR @ProductID = 0) and ActiveTest=1
		ORDER BY TestName
	RETURN

	GO

	/****** Object:  StoredProcedure [dbo].[uspReturnTestsRemediationForStudent]    Script Date: 03/23/2011 19:57:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestsRemediationForStudent]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspReturnTestsRemediationForStudent]

GO

CREATE procedure [dbo].[uspReturnTestsRemediationForStudent]
@userID int,
@productID int,
@groupID int,
@cohortID int,
@institutionID int
as
 Begin
select T.TestName, CONVERT(CHAR(8),DATEADD(ss,sum(UQ.TimeSpendForRemedation),0),108) as Remedation
from UserQuestions AS UQ join dbo.UserTests UT  on UT.UserTestID = UQ.UserTestID
join dbo.Tests T on UT.TestID = T.TestID
inner join dbo.NurCohort ON UT.CohortID = dbo.NurCohort.CohortID
INNER JOIN Products P ON T.ProductId = P.ProductId
INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID
INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
INNER JOIN dbo.NurCohort C ON SA.CohortID = C.CohortID
LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
where
	NurCohort.InstitutionID = @InstitutionId and
	((@userID <> 0 AND UT.UserId=@userID ) OR @userID = 0) And
	((@productID <> 0 AND UT.productID=@productID ) OR @productID = 0) And
	((@CohortID <> 0 AND UT.CohortID=@CohortID ) OR @CohortID = 0) And
	((@groupID <> 0 AND G.GroupID=@groupID ) OR @groupID = 0)
	Group by T.TestName
End
GO
/****** Object:  StoredProcedure [dbo].[uspReturnTestsExplanationForStudent]    Script Date: 03/23/2011 19:57:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestsExplanationForStudent]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspReturnTestsExplanationForStudent]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspReturnTestsExplanationForStudent]
@userID int,
@productID int,
@groupID int,
@cohortID int,
@institutionID int
as
 Begin
select T.TestName, CONVERT(CHAR(8),DATEADD(ss,sum(UQ.TimeSpendForExplanation),0),108) as Remedation
from UserQuestions AS UQ
join dbo.UserTests UT  on UT.UserTestID = UQ.UserTestID
join dbo.Tests T on UT.TestID = T.TestID
inner join dbo.NurCohort ON UT.CohortID = dbo.NurCohort.CohortID
INNER JOIN Products P ON T.ProductId = P.ProductId
INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID
INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
INNER JOIN dbo.NurCohort C ON SA.CohortID = C.CohortID
Left JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId  AND SA.CohortID = G.CohortId
where
	NurCohort.InstitutionID = @InstitutionId and
	((@userID <> 0 AND UT.UserId=@userID ) OR @userID = 0) And
	((@productID <> 0 AND UT.productID=@productID ) OR @productID = 0) And
	((@CohortID <> 0 AND UT.CohortID=@CohortID ) OR @CohortID = 0) And
	((@groupID <> 0 AND G.GroupID=@groupID ) OR @groupID = 0)
	Group by T.TestName
End

GO

/****** Object:  StoredProcedure [dbo].[uspReturnTestsRemediationForCohort]    Script Date: 03/23/2011 19:57:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestsRemediationForCohort]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspReturnTestsRemediationForCohort]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspReturnTestsRemediationForCohort]
@userID int,
@productID int,
@groupID int,
@cohortID int,
@institutionID int
as
Begin
select T.TestName, CONVERT(CHAR(8),DATEADD(ss,sum(UQ.TimeSpendForRemedation),0),108) as Remedation
from UserQuestions AS UQ join dbo.UserTests UT  on UT.UserTestID = UQ.UserTestID
join dbo.Tests T on UT.TestID = T.TestID
inner join dbo.NurCohort ON UT.CohortID = dbo.NurCohort.CohortID
INNER JOIN Products P ON T.ProductId = P.ProductId
INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID
INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
INNER JOIN dbo.NurCohort C ON SA.CohortID = C.CohortID
left JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
where
	NurCohort.InstitutionID = @InstitutionId and
	((@userID <> 0 AND UT.UserId=@userID ) OR @userID = 0) And
	((@productID <> 0 AND UT.productID=@productID ) OR @productID = 0) And
	((@CohortID <> 0 AND UT.CohortID=@CohortID ) OR @CohortID = 0) And
	((@groupID <> 0 AND G.GroupID=@groupID ) OR @groupID = 0)
	Group by T.TestName
End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSessionId]') AND type in (N'P', N'PC'))

DROP PROCEDURE [dbo].[uspGetSessionId]

GO


CREATE PROCEDURE [dbo].[uspGetSessionId]
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT SessionID
	FROM NurStudentInfo
	WHERE userId = @userId

END

Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchPrograms]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspSearchPrograms]
GO

CREATE PROC [dbo].[uspSearchPrograms]
(
	@SearchText varchar(200)
)
AS
	SELECT ProgramID,
		ProgramName,
		[Description]
	FROM dbo.NurProgram
	WHERE DeletedDate IS NULL
	AND (@SearchText = ''
	OR ProgramName LIKE '%' + @SearchText + '%')

GO

/****** Object:  StoredProcedure [dbo].[uspInsertUpdateNurInstitution]    Script Date: 04/12/2011 19:48:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertUpdateNurInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspInsertUpdateNurInstitution]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNewNurPrograms]    Script Date: 04/12/2011 19:48:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNewNurPrograms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNewNurPrograms]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNurInstitutionsTimeZoneByInstIDSearchText]    Script Date: 04/12/2011 19:48:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNurInstitutionsTimeZoneByInstIDSearchText]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNurInstitutionsTimeZoneByInstIDSearchText]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTimeZones]    Script Date: 04/12/2011 19:48:25 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetTimeZones' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTimeZones]
GO
CREATE PROCEDURE [dbo].[uspGetTimeZones]
AS
BEGIN
	select * from dbo.TimeZones
	order by OrderNumber
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetPrograms' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetPrograms]
GO
CREATE PROCEDURE [dbo].[uspGetPrograms]
AS
BEGIN
	SELECT * FROM NurProgram WHERE DeletedDate is null
	ORDER BY ProgramName asc
END
GO
/****** Object:  StoredProcedure [dbo].[uspGetNewNurProgramById]    Script Date: 04/12/2011 19:48:23 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetNewNurProgramById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetNewNurProgramById]
GO

CREATE PROCEDURE [dbo].[uspGetNewNurProgramById]
@ProgramID as int
AS
BEGIN
  SELECT * FROM NurProgram WHERE DeletedDate is null and ProgramID=@ProgramID
  ORDER BY ProgramName asc
END

GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveInstitution' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveInstitution]

GO

CREATE PROCEDURE [dbo].[uspSaveInstitution]
	@InstitutionID as int OUTPUT,
	@InstitutionName as nVarchar(80),
	@Description as nVarchar(80),
	@ContactName as nVarchar(50),
	@ContactPhone as varchar(50),
	@TimeZone as int,
	@IP as varchar(250),
	@FacilityID int,
	@CenterID as nVarchar(50),
	@ProgramID int,
	@CreateOrUpdatedUser as int,
	@DeleteUser as int,
	@Status as int,
	@AddressID as int,
	@Annotation as varchar(1000),
	@ContractualStartDate smalldatetime,
	@Email as nVarchar(100)
AS
BEGIN
	 If(@InstitutionID = 0)
	  Begin
		  INSERT INTO NurInstitution (InstitutionName,Description,ContactName,ContactPhone,TimeZone,IP,Status,CreateDate,CreateUser,FacilityID,CenterID,ProgramID,DeleteUser,AddressID,Annotation,ContractualStartDate,Email)
		  VALUES (@InstitutionName,@Description,@ContactName,@ContactPhone,@TimeZone,@IP,@Status,getdate(),@CreateOrUpdatedUser,@FacilityID,@CenterID,@ProgramID,@DeleteUser,@AddressID,@Annotation,@ContractualStartDate,@Email)
		  SET @InstitutionID = CONVERT(int, SCOPE_IDENTITY())
	  End
	 Else
	  Begin
		   UPDATE NurInstitution SET InstitutionName=@InstitutionName,Description=@Description,ContactName=@ContactName,
		   ContactPhone=@ContactPhone,TimeZone=@TimeZone,IP=@IP,Status=@Status,CenterID=@CenterID,
		   UpdateDate=getdate(),UpdateUser=@CreateOrUpdatedUser,FacilityID=@FacilityID,ProgramID=@ProgramID,DeleteUser=@DeleteUser,AddressID=@AddressID,Annotation =@Annotation,ContractualStartDate = @ContractualStartDate,
		   Email = @Email
		   WHERE InstitutionID=@InstitutionID
	  End
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminAuthorizationRules]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetAdminAuthorizationRules]
	
GO

CREATE PROC [dbo].[uspGetAdminAuthorizationRules]
AS
SELECT
	SecurityLevel,
	AdminType,
	I_Add,
	I_Edit,
	I_Delete,
	C_Add,
	C_Edit,
	C_AccessDatesEdit,
	C_Delete,
	C_AssisgnStudents,
	C_EditTestDates,
	C_AssignProgram,
	G_Add,
	G_Edit,
	G_Delete,
	G_EditTestDates,
	G_AssignStudents,
	S_Add,
	S_Edit,
	S_Delete,
	S_AssignToCohort,
	S_AssignToGroup,
	S_EditTestDates,
	A_Add,
	A_Edit,
	A_Delete,
	P_Add,
	P_Edit,
	P_Delete,
	P_AssignTests,
	Cms,
	R_InstitutionResults,
	R_CohortResults,
	R_GroupResults,
	R_StudentResults,
	R_KaplanReport
FROM dbo.NurAdminSecurity

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveProgram]
GO

CREATE PROC [dbo].[uspSaveProgram]
(
	@ProgramId int OUTPUT,
	@ProgramName varchar(200),
	@Description varchar(200),
	@UserId int
)
AS
	IF ISNULL(@ProgramId, 0) = 0
	BEGIN
		INSERT INTO dbo.NurProgram (ProgramName, [Description], CreateUser, CreateDate)
		VALUES (@ProgramName, @Description, @UserId, GETDATE())
		
		SET @ProgramId = CONVERT(int, SCOPE_IDENTITY())
	END
	ELSE
		UPDATE dbo.NurProgram
		SET	ProgramName = @ProgramName,
			[Description] = @Description,
			UpdateUser = @UserId,
			UpdateDate = GETDATE()
		WHERE ProgramId = @ProgramId
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteProgram]
GO

CREATE PROC [dbo].[uspDeleteProgram]
(
	@ProgramId int,
	@UserId int
)
AS
	UPDATE dbo.NurProgram
	SET	DeletedDate = GETDATE()
	WHERE ProgramId = @ProgramId

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetProgram]
GO

CREATE PROC [dbo].[uspGetProgram]
(
	@ProgramId int
)
AS
	SELECT ProgramID,
		ProgramName,
		[Description]
	FROM dbo.NurProgram
	WHERE ProgramId = @ProgramId
	AND DeletedDate IS NULL

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfQuestions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetListOfQuestions]
GO
Create PROCEDURE [dbo].[uspGetListOfQuestions]

@ProductID as int,
@TestID as int,
@ClientNeedID as int,
@ClientNeedsCategoryID as int,
@ClinicalConceptID as int,
@CognitiveLevelID as int,
@CriticalThinkingID as int,
@DemographicID as int,
@LevelOfDifficultyID as int,
@NursingProcessID as int,
@RemediationID as int,
@SpecialtyAreaID as int,
@SystemID as int,
@QuestionID as varchar(100),
@TypeOfFileID as varchar(10),
@QuestionType as char(2),
@Text as varchar(max),
@ReleaseStatus as bit,
@Active as bit

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;


Select
T.ProductID,T.TestID,
CN.ClientNeeds,
Q.QuestionID,Q.QID as QID,Q.QID AS QN,Q.Stem,Q.ReleaseStatus,Q.TypeOfFileID ,
CNC.ClientNeedCategory ,
R.TopicTitle ,
NP.NursingProcess,
S.System
FROM dbo.Questions Q
LEFT OUTER JOIN dbo.TestQuestions TQ ON TQ.QID = Q.QID and (@Productid>0 OR @TestID >0)
LEFT OUTER JOIN dbo.Tests T ON TQ.TestID=T.TestID and (@Productid>0 OR  @TestID >0)
LEFT OUTER JOIN dbo.ClientNeeds CN ON Q.ClientNeedsID = CN.ClientNeedsID
LEFT OUTER JOIN dbo.ClientNeedCategory CNC ON Q.ClientNeedsCategoryID = CNC.ClientNeedCategoryID
LEFT OUTER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID
LEFT OUTER JOIN dbo.NursingProcess NP ON Q.NursingProcessID = NP.NursingProcessID
LEFT OUTER JOIN dbo.Systems S ON Q.SystemID=S.SystemID
where
(@ProductID = 0 OR T.ProductID=@ProductID)
AND (@TestID =0 OR T.TestID=@TestID)
AND (@ClientNeedID = 0 OR Q.ClientNeedsID=@ClientNeedID)
AND (@ClientNeedsCategoryID = 0  OR Q.ClientNeedsCategoryID=@ClientNeedsCategoryID)
AND (@ClinicalConceptID  = 0 OR Q.ClinicalConceptsID=@ClinicalConceptID)
AND (@CognitiveLevelID = 0 OR Q.CognitiveLevelID=@CognitiveLevelID)
AND (@CriticalThinkingID = 0 OR Q.CriticalThinkingID=@CriticalThinkingID)
AND (@DemographicID = 0 OR Q.DemographicID=@DemographicID)
AND (@LevelOfDifficultyID = 0 OR Q.LevelOfDifficultyID=@LevelOfDifficultyID)
AND (@NursingProcessID = 0 OR Q.NursingProcessID=@NursingProcessID)
AND (@RemediationID = 0 OR Q.RemediationID=@RemediationID)
AND (@SpecialtyAreaID = 0 OR Q.SpecialtyAreaID=@SpecialtyAreaID)
AND (@SystemID = 0 OR Q.SystemID=@SystemID)
AND ((@QuestionID = '' OR @QuestionID is null) OR Q.QuestionID like '%' + @QuestionID + '%')
AND ((@TypeOfFileID = '' OR @TypeOfFileID is null) OR Q.TypeOfFileID=@TypeOfFileID)
AND ((@QuestionType ='0' OR @QuestionType is null) OR Q.QuestionType= @QuestionType)
AND ((@Text = '' OR @Text is null) OR (Q.Stem like '%' + @Text + '%' OR Q.Stimulus like '%' + @Text + '%' OR Q.Explanation like '%' + @Text + '%' OR Q.ItemTitle like '%' + @Text + '%'))
AND (@ReleaseStatus = 0 OR Q.ReleaseStatus IS NOT NULL)
AND (@Active = 1 OR Q.Active = 0)
AND (@Active = 0 OR (Q.Active IS NULL OR Q.Active = 1))

ORDER BY T.TestID

SET NOCOUNT OFF;
END

Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfRem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetListOfRem]
GO

CREATE PROCEDURE [dbo].[uspGetListOfRem]

@RemediationID as int,
@Text as varchar(max)

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

SELECT * FROM Remediation WHERE
(@RemediationID = 0 OR RemediationID=@RemediationID)
AND ((@Text = '' OR @Text is null) OR (Explanation like '%' + @Text + '%' OR TopicTitle like '%' + @Text + '%'))

SET NOCOUNT OFF;
END
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionDetailsById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetInstitutionDetailsById]
GO
CREATE PROCEDURE dbo.uspGetInstitutionDetailsById
@InstitutionID VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT InstitutionID, InstitutionName
	FROM NurInstitution
	WHERE Status=1
	AND ((@InstitutionID <> '0' AND InstitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionID,',')))
		OR @InstitutionID = '0')
	ORDER BY InstitutionName
	
	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForCohortAndProduct]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetTestsForCohortAndProduct]
GO

CREATE PROCEDURE dbo.uspGetTestsForCohortAndProduct
@ProductIds VARCHAR(MAX),
@InstitutionIds VARCHAR(MAX) = '',
@CohortIds VARCHAR(MAX),
@StudentIds VARCHAR(MAX)
AS
BEGIN
	
	SET NOCOUNT ON
	
	SELECT DISTINCT Tests.TestName, UserTests.TestID
	From UserTests
	Join Tests on UserTests.TestID = Tests.TestID
	Where ((@ProductIds <> '0' AND Tests.ProductID IN (select value from  dbo.funcListToTableInt(@ProductIds,'|')))
		 OR @ProductIds = '0')
	AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
		 OR @CohortIds = '0')
	AND ( (@StudentIds <> '0' AND dbo.UserTests.UserID IN (select value from  dbo.funcListToTableInt(@StudentIds,'|')))
		 OR @StudentIds = '0')
	AND ( (@InstitutionIds <> '' AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))
		 OR @InstitutionIds = '')
	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortsByInstitutionId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetCohortsByInstitutionId]
GO
CREATE PROCEDURE dbo.uspGetCohortsByInstitutionId
@InstitutionID INT
AS
BEGIN
	
SET NOCOUNT ON
	
	SELECT  dbo.NurCohort.CohortId, dbo.NurCohort.CohortName
    FROM    dbo.NurCohort
    INNER JOIN dbo.NurInstitution ON dbo.NurCohort.InstitutionID = dbo.NurInstitution.InstitutionID
    WHERE CohortDeleteDate is null and CohortStatus=1
		AND dbo.NurCohort.InstitutionID=@InstitutionID
    ORDER BY CohortName ASC
			
SET NOCOUNT OFF

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByQuestion]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspReturnStudentSummaryByQuestion]
GO
CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByQuestion]
	  @InstitutionId INT,
      @ProductID int,
      @CohortIds varchar(MAX),
      @TestID int
AS
BEGIN
SELECT UserTests.UserID,Questions.QuestionID,
          AIndex = CASE UserAnswers.AIndex
                        WHEN 'A' THEN '1'
                        WHEN 'B' THEN '2'
                        WHEN 'C' THEN '3'
                        WHEN 'D' THEN '4'
                        WHEN 'E' THEN '5'
                        WHEN 'F' THEN '6'
                        END,
    UserAnswers.Correct
    FROM  dbo.Tests INNER JOIN
                dbo.UserTests     ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
                dbo.UserQuestions ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN
                dbo.Questions     ON dbo.UserQuestions.QID = dbo.Questions.QID INNER JOIN
                dbo.UserAnswers   ON dbo.UserQuestions.QID = dbo.UserAnswers.QID
                AND dbo.UserQuestions.UserTestID = dbo.UserAnswers.UserTestID
                INNER JOIN dbo.NurCohort ON UserTests.CohortID = dbo.NurCohort.CohortID
    AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
    OR @CohortIds = '0')
    AND UserTests.TestID= @TestID
    AND UserTests.ProductID = @ProductID
    AND dbo.NurCohort.InstitutionID = @InstitutionId
    order by UserTests.UserID

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByQuestionHeader]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspReturnStudentSummaryByQuestionHeader]
GO

CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByQuestionHeader]
@ProductId int,
@CohortIds varchar(MAX),
@TestId int
AS
BEGIN
SELECT Questions.QID,Questions.QuestionID
				FROM  dbo.Tests INNER JOIN
                dbo.UserTests     ON dbo.Tests.TestID = dbo.UserTests.TestID INNER JOIN
                dbo.UserQuestions ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN
                dbo.Questions     ON dbo.UserQuestions.QID = dbo.Questions.QID INNER JOIN
                dbo.UserAnswers   ON dbo.UserQuestions.QID = dbo.UserAnswers.QID
                AND dbo.UserQuestions.UserTestID = dbo.UserAnswers.UserTestID
                WHERE dbo.Tests.ProductID=@ProductID
                AND dbo.UserTests.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
				AND dbo.Tests.TestID = @TestID
Group by Questions.QID, Questions.QuestionID
order by QuestionId

END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDataForStudentSummaryByQuestionReport]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetDataForStudentSummaryByQuestionReport]
GO

Create PROC dbo.uspGetDataForStudentSummaryByQuestionReport
@InstitutionId INT,
@ProductID int,
@CohortIds varchar(4000),
@TestID int
AS
BEGIN

 SET NOCOUNT ON

 -- Create result table
 Create table #result (ID int identity(1,1),Student VARCHAR(10),StudentID VARCHAR(10))

 -- Fetch Header column names
 Create table #Headers (ID int identity(1,1),QID INT, ColName Varchar(50))

 Insert into #Headers (QID,ColName)
 select Questions.QID,Questions.QuestionID
 from UserTests
	 INNER JOIN UserAnswers ON UserTests.UserTestID = UserAnswers.UserTestID
	 INNER JOIN Questions ON Questions.QID = UserAnswers.QID
	 INNER JOIN dbo.NurCohort ON UserTests.CohortID = dbo.NurCohort.CohortID
	 where UserTests.UserTestID=UserAnswers.UserTestID
	 AND UserAnswers.QID=Questions.QID
	 AND Questions.QuestionType!='00'
	 AND Questions.QuestionType!='05'
	 AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
	 OR @CohortIds = '0')
	 AND UserTests.TestID= @TestID
	 AND UserTests.ProductID = @ProductID
	 AND dbo.NurCohort.InstitutionID = @InstitutionId
	 AND RTRIM(LTRIM(Questions.QuestionID)) <> ''
	 Group by Questions.QID, Questions.QuestionID
	 order by Questions.QuestionID

 -- Add the columns to result table
 declare @count int
 select @count = max(id) from #Headers
 declare @index int
 set @index =1

 declare @colName varchar(50)

while(@index<=@count)
	 begin
	   select @colName = ColName
	   from #Headers
	   where id = @index
	   exec('alter table #result add ' + @colName + ' varchar(50) ')
	   set @index = @index + 1
	 end
 -- Add score column to result table
 alter table #result add Score int

 -- Fetch Student Data
 SELECT ROW_NUMBER() OVER (order by UserTests.UserID, UserAnswers.QID  ) Id, UserTests.UserID,Questions.QuestionID,
          AIndex = CASE UserAnswers.AIndex
                        WHEN 'A' THEN '1'
                        WHEN 'B' THEN '2'
                        WHEN 'C' THEN '3'
                        WHEN 'D' THEN '4'
                        ELSE '5'
                        END,
    UserAnswers.Correct INTO #StudentData
    from UserTests
   INNER JOIN UserAnswers ON UserTests.UserTestID = UserAnswers.UserTestID
   INNER JOIN Questions ON Questions.QID = UserAnswers.QID
   INNER JOIN dbo.NurCohort ON UserTests.CohortID = dbo.NurCohort.CohortID
    where UserTests.UserTestID=UserAnswers.UserTestID
    AND UserAnswers.QID=Questions.QID
    AND ( (@CohortIds <> '0' AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
   OR @CohortIds = '0')
    AND UserTests.TestID= @TestID
       AND UserTests.ProductID = @ProductID
       AND dbo.NurCohort.InstitutionID = @InstitutionId
    order by UserTests.UserID, UserAnswers.QID

 -- populate result table
 DECLARE @StudentID VARCHAR(10)
 SET @StudentID = ''
 DECLARE @QuestionID VARCHAR(10)
 SET @QuestionID = ''
 DECLARE @Score INT
 SET @Score =0
 DECLARE @CountStudent INT
 SET @CountStudent = 0

 -- variable to hold table values
 DECLARE @UserId VARCHAR(10)
 SET @UserId = ''
 DECLARE @StudentQuestionID VARCHAR(10)
 DECLARE @Correct INT
 DECLARE @AIndex INT

 -- variables to hold new row values
 declare @NewRowQuestionID VARCHAR(10)
 declare @NewRowStudentID VARCHAR(10)
 DECLARE @NewRowScore INT

 DECLARE @StudentDataCount INT
 SELECT @StudentDataCount = COUNT(Id) FROM #StudentData
 set @index =1
 DECLARE @NewrowId INT
 Declare @lastStudentId as VARCHAR(10)
 DECLARE @sql VARCHAR(MAX)
 SET @sql = ''

 IF(@StudentDataCount <> 0)
 BEGIN
  while(@index <= @StudentDataCount)
  BEGIN
   SELECT @UserId= cast(userid AS VARCHAR(10)), @StudentQuestionID = cast(QuestionID AS VARCHAR(10)),
   @Correct = Correct,@AIndex = AIndex
   FROM #StudentData
   WHERE id = @index

   PRINT  '@UserId = ' + CAST(@UserId AS VARCHAR(10)) + ' @StudentID = ' + CAST(@StudentID AS VARCHAR(10))

   if (@UserId = @StudentID)
   BEGIN
		if (@StudentQuestionID <> @QuestionID)
		 BEGIN
			 IF(CHARINDEX(@StudentQuestionID,@sql) = 0)
				 BEGIN
				  set @lastStudentId = @StudentID
				  SET @sql = @sql + ' ' +  @StudentQuestionID + ' = ' + CAST(@AIndex AS VARCHAR(10)) + ','
				 END
			 SET @QuestionID = @StudentQuestionID
			 set @Score = @Score + @Correct
			 PRINT 'score = '
			 PRINT @Score
			 PRINT 'Correct'
			 PRINT @Correct
		 END
	 END
   ELSE
   BEGIN
	  IF(@StudentID = '')
		BEGIN
		 SET @CountStudent = @CountStudent + 1
		 SET @sql = @sql +  ' Student = ''001''' + ','
		END
		ELSE
			BEGIN
			     SET @sql = @sql + ' StudentID = ' + CAST(@lastStudentId AS VARCHAR(10)) + ','
				 SET @sql = @sql + ' score = ' + CAST(@Score AS VARCHAR(10)) + ','
				 SET @Score = 0
				 -- insert dummy row in table
				 INSERT INTO #result(Score) VALUES(0)
				 SET @NewrowId = SCOPE_IDENTITY()
				 SET @sql = SUBSTRING(@sql,1,len(@sql)-1)
				
				 SET @sql = 'update #result SET ' + @sql + ' WHERE id = ' + CAST(@NewrowId AS VARCHAR(10))
				 EXEC(@sql)
				 PRINT @sql
				 SET @sql = ''
				 SET @CountStudent = @CountStudent + 1
				 IF(@CountStudent < 10)
				 BEGIN
				  SET @sql = @sql + ' Student = ''00' + CAST(@CountStudent AS VARCHAR(10)) + ''','
				 END
				 ELSE
				 BEGIN
				  SET @sql = @sql + ' Student = ''0' + CAST(@CountStudent AS VARCHAR(10))  + ''','
				 END
			END

		 IF(CHARINDEX(@StudentQuestionID,@sql) = 0)
			BEGIN
			 SET @sql = @sql + ' ' + @StudentQuestionID + ' = ' + CAST(@AIndex AS VARCHAR(10))  + ','
			END
		SET @QuestionID = @StudentQuestionID
		set @Score = @Score + @Correct
		SET @StudentID = @UserId
	END
    set @index = @index + 1
  END
  SET @sql = @sql + ' StudentID = ' + CAST(@lastStudentId AS VARCHAR(10)) + ','
  SET @sql = @sql + ' score = ' + CAST(@Score AS VARCHAR(10)) + ','
  -- insert dummy row in table
  INSERT INTO #result(Score) VALUES(0)
  SET @NewrowId = SCOPE_IDENTITY()
  SET @sql = SUBSTRING(@sql,1,len(@sql)-1)
  SET @sql = 'update #result SET ' + @sql + ' WHERE id = ' + CAST(@NewrowId AS VARCHAR(10))
  EXEC(@sql)

 END

 alter table #result
 drop column id

 alter table #result
 drop column Student

 SELECT * FROM #result
 ORDER BY StudentID

 SET NOCOUNT OFF
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroupByInstitutionIdAndCohortId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetGroupByInstitutionIdAndCohortId]
GO

CREATE PROCEDURE dbo.uspGetGroupByInstitutionIdAndCohortId
@InstitutionID INT,
@CohortIds VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT DISTINCT dbo.NurGroup.GroupID, dbo.NurGroup.GroupName
    FROM dbo.NurGroup
    INNER JOIN dbo.NurCohort ON dbo.NurGroup.CohortID = dbo.NurCohort.CohortID
    INNER JOIN dbo.NurInstitution ON dbo.NurCohort.InstitutionID = dbo.NurInstitution.InstitutionID
    WHERE     (dbo.NurCohort.CohortStatus = 1)
    AND dbo.NurCohort.InstitutionID=@InstitutionID
    AND ( (@CohortIds <> '0' AND dbo.NurGroup.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
		 OR @CohortIds = '0')
	AND dbo.NurCohort.InstitutionID= @InstitutionID
	ORDER BY GroupName asc
		
	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentsInInstitutionByCohortAndGroups]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetStudentsInInstitutionByCohortAndGroups]
GO

CREATE PROCEDURE dbo.uspGetStudentsInInstitutionByCohortAndGroups
@InstitutionIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@GroupIds VARCHAR(MAX),
@SearchText VARCHAR(100)
AS
BEGIN
	
	SET NOCOUNT ON
	
	SELECT dbo.NurStudentInfo.UserID,dbo.NurStudentInfo.LastName+','+dbo.NurStudentInfo.FirstName as NAME
			,NurStudentInfo.UserName ,NurStudentInfo.Email,NurStudentInfo.LastName,NurStudentInfo.FirstName
            FROM   dbo.NurInstitution
            INNER JOIN dbo.NurStudentInfo
            INNER JOIN dbo.NusStudentAssign ON dbo.NurStudentInfo.UserID = dbo.NusStudentAssign.StudentID
            LEFT OUTER JOIN dbo.NurCohort ON dbo.NusStudentAssign.CohortID = dbo.NurCohort.CohortID
            ON dbo.NurInstitution.InstitutionID = dbo.NurStudentInfo.InstitutionID
            AND dbo.NurInstitution.InstitutionID = dbo.NurCohort.InstitutionID
            LEFT OUTER JOIN dbo.NurGroup ON dbo.NusStudentAssign.GroupID = dbo.NurGroup.GroupID AND dbo.NurCohort.CohortID = dbo.NurGroup.CohortID
            LEFT OUTER JOIN dbo.NurProgram
            INNER JOIN dbo.NurCohortPrograms ON dbo.NurProgram.ProgramID = dbo.NurCohortPrograms.ProgramID ON
            dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
            WHERE     (dbo.NusStudentAssign.DeletedDate IS NULL)
            AND (dbo.NurCohort.CohortStatus = 1 OR dbo.NurCohort.CohortStatus IS NULL)
            AND (dbo.NurCohortPrograms.Active IS NULL OR dbo.NurCohortPrograms.Active = 1)
            AND (dbo.NurProgram.DeletedDate IS NULL) AND (dbo.NurInstitution.Status = 1)
            AND ( (@InstitutionIds <> '' AND dbo.NurInstitution.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))
			OR @InstitutionIds = '')
            AND ( (@CohortIds <> '' AND dbo.NusStudentAssign.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
			OR @CohortIds = '')
			AND ( (@GroupIds <> '' AND dbo.NusStudentAssign.GroupID IN (select value from  dbo.funcListToTableInt(@GroupIds,'|')))
			OR @GroupIds = '')
			AND ( LEN(@SearchText) = 0 OR (LEN(@SearchText) > 0
				AND (NurStudentInfo.UserName LIKE '%' + @SearchText + '%'
					OR NurStudentInfo.Email LIKE '%' + @SearchText + '%'
					OR NurStudentInfo.LastName LIKE '%' + @SearchText + '%'
					OR NurStudentInfo.FirstName LIKE '%' + @SearchText + '%')))
            ORDER BY Name ASC

    SET NOCOUNT OFF

END
GO

/****** Object:  StoredProcedure [dbo].[uspInsertUpdateNorming]    Script Date: 04/18/2011 15:25:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertUpdateNorming]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspInsertUpdateNorming]
GO
/****** Object:  StoredProcedure [dbo].[uspGetNormingByTestId]    Script Date: 04/18/2011 15:25:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNormingByTestId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNormingByTestId]
GO

/****** Object:  StoredProcedure [dbo].[uspGetNormingByTestId]    Script Date: 04/18/2011 15:25:33 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetNormings' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetNormings]
GO

CREATE PROCEDURE [dbo].[uspGetNormings]
@TestId as int,
@TestIds as Varchar(4000)
AS
BEGIN
 Select NumberCorrect,Correct,PercentileRank,Probability,TestID,id
 From Norming
 Where (TestID=@TestId or  @TestId=0)
 and ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')
 Order By NumberCorrect
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveNorming' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveNorming]
GO

CREATE PROCEDURE [dbo].[uspSaveNorming]
@TestID as int,
@NumberCorrect float,
@Correct float,
@PercentileRank float,
@Id as int,
@Probability as int
AS
BEGIN
	If(@Id = 0)
	 Begin
		INSERT INTO Norming(TestID,NumberCorrect,Correct,PercentileRank,Probability)
		 values(@TestID,@NumberCorrect,@Correct,@PercentileRank,@Probability)
	 End
	Else
	 Begin
		 Update Norming set NumberCorrect=@NumberCorrect,Correct=@Correct,PercentileRank=@PercentileRank,Probability=@Probability
		 where TestID=@TestID and id=@Id
	 End
END

GO

/****** Object:  StoredProcedure [dbo].[uspDeleteNormingById]    Script Date: 04/18/2011 15:25:30 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteNormingById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteNormingById]
GO

CREATE PROCEDURE [dbo].[uspDeleteNormingById]
@Id as int
AS
BEGIN
	Delete from Norming
	where id=@Id
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetAVPItemsByTestName]    Script Date: 04/18/2011 15:25:32 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetAVPItemsByTestName' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAVPItemsByTestName]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSearchAVPItems' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSearchAVPItems]
GO

CREATE PROCEDURE [dbo].[uspSearchAVPItems]
@TestName varchar(50)
AS
BEGIN
	select * from Tests where ProductID=4 and ActiveTest=1 and TestSubGroup=10 and (TestName Like '%'+@TestName+'%' or @TestName = '')
END

GO
/****** Object:  StoredProcedure [dbo].[uspDeleteTestById]    Script Date: 04/18/2011 15:25:31 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteTestById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteTestById]
GO

Create PROCEDURE [dbo].[uspDeleteTestById]
@TestID varchar(50)
AS
BEGIN
	delete from Tests where TestId = @TestID
END

/****** Object:  StoredProcedure [dbo].[uspGetAVPItemsByID]    Script Date: 04/18/2011 15:25:31 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetAVPItemsByID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAVPItemsByID]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPActiveTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspActiveTests]
GO

CREATE PROCEDURE [dbo].[uspActiveTests]
@ProductId int,
@TestSubGroup int,
@TestID INT
AS
BEGIN
	select * from Tests
	where ProductID=@ProductId and ActiveTest=1 and TestSubGroup=@TestSubGroup and (TestId = @TestID or @TestID = -1 )
END

GO
/****** Object:  StoredProcedure [dbo].[uspInsertUpdateAVPItems]    Script Date: 04/18/2011 15:25:33 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertUpdateAVPItems' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertUpdateAVPItems]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveAVPItems' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveAVPItems]
GO

CREATE PROCEDURE [dbo].[uspSaveAVPItems]
@TestID INT OUTPUT,
@TestName varchar(50),
@Url as nVarchar(200),
@PopHeight as int,
@PopWidth as int
AS
BEGIN
	If(@TestID =0)
	 Begin
		 INSERT INTO Tests(ProductID,TestName,ActiveTest,TestSubGroup,Url,PopHeight,PopWidth,ReleaseStatus)
		 values(4,@TestName,1,10,@Url,@PopHeight,@PopWidth,'E')
		 set @TestID = Scope_Identity()
	 End
	Else
	 Begin
		 UPDATE  Tests  SET TestName=@TestName,Url=@Url,PopHeight=@PopHeight,PopWidth=@PopWidth,ReleaseStatus='E'
		 Where TestID = @TestID
	 End
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPUpdateTestsReleaseStatusById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateTestsReleaseStatusById]

GO

CREATE PROCEDURE [dbo].[uspUpdateTestsReleaseStatusById]
@TestId as int,
@ReleaseStatus as char(1)
AS
BEGIN
	Update Tests Set ReleaseStatus=@ReleaseStatus
	WHERE TestID = @TestId
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetNormByTestIdChartType' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetNormByTestIdChartType]

GO

CREATE PROCEDURE [dbo].[uspGetNormByTestIdChartType]
@TestId as int,
@ChartType as nVarchar(50)
AS
BEGIN
	select * from Norm where TestID = @TestId and ChartType = @ChartType
END
GO

/****** Object:  StoredProcedure [dbo].[uspInsertUpdateNorm]    Script Date: 04/19/2011 19:36:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspInsertUpdateNorm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspInsertUpdateNorm]
GO
/****** Object:  StoredProcedure [dbo].[uspInsertUpdateNorm]    Script Date: 04/19/2011 19:36:49 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveNorm' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveNorm]
GO

CREATE PROCEDURE [dbo].[uspSaveNorm]
@NormId as Int,
@ChartType as nVarchar(50),
@ChartId as int,
@Norm as real,
@TestId as int
AS
BEGIN
Declare @Id as int
set @Id = @NormId
	If(@NormId = 0)
	 Begin
	   Insert into Norm(ChartType,ChartID,Norm,TestID)
	   values(@ChartType,@ChartId,@Norm,@TestId)
     End
    Else
     Begin
       Update Norm
       Set ChartType = @ChartType,ChartID=@ChartId,Norm=@Norm,TestID=@TestId
       where ID = @NormId
     End
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetClientNeedsCategories' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetClientNeedsCategories]
GO

CREATE PROCEDURE [dbo].[uspGetClientNeedsCategories]
(
	@ClientNeedId int
)
	
AS
	BEGIN
	SET NOCOUNT ON
		SELECT 	
			ClientNeedID,
			ClientNeedCategoryID,
			ClientNeedCategory
		FROM  ClientNeedCategory
		WHERE (@ClientNeedId = 0 OR ClientNeedID = @ClientNeedId)

	SET NOCOUNT OFF
	END

GO

/****** Object:  StoredProcedure [dbo].[uspGetClientNeeds]    Script Date: 04/19/2011 19:36:43 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetClientNeeds' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetClientNeeds]
GO


/****** Object:  StoredProcedure [dbo].[uspGetClinicalConcept]    Script Date: 04/19/2011 19:36:44 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetClinicalConcept' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetClinicalConcept]
GO



/****** Object:  StoredProcedure [dbo].[uspGetCognitiveLevel]    Script Date: 04/19/2011 19:36:44 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetCognitiveLevel' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCognitiveLevel]
GO


/****** Object:  StoredProcedure [dbo].[uspGetCriticalThinking]    Script Date: 04/19/2011 19:36:45 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetCriticalThinking' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCriticalThinking]
GO


/****** Object:  StoredProcedure [dbo].[uspGetDemographic]    Script Date: 04/19/2011 19:36:45 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetDemographic' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetDemographic]
GO

CREATE PROCEDURE [dbo].[uspGetDemographic]
AS
BEGIN
	select DemographicID,Demographic from Demographic
END
GO
/****** Object:  StoredProcedure [dbo].[uspGetLevelOfDifficulty]    Script Date: 04/19/2011 19:36:46 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLevelOfDifficulty' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLevelOfDifficulty]
GO


/****** Object:  StoredProcedure [dbo].[uspGetSpecialtyArea]    Script Date: 04/19/2011 19:36:47 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetSpecialtyArea' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetSpecialtyArea]
GO


/****** Object:  StoredProcedure [dbo].[uspGetSystems]    Script Date: 04/19/2011 19:36:48 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetSystems' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetSystems]
GO
CREATE PROCEDURE [dbo].[uspGetSystems]
AS
BEGIN
	select SystemID,System from Systems
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetNursingProcess]    Script Date: 04/19/2011 19:36:47 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetNursingProcess' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetNursingProcess]
GO


-- Report SPs
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheProgram]
GO

CREATE PROCEDURE dbo.uspGetResultsFromTheProgram
@UserTestID INT,
@Charttype INT
AS
BEGIN
	IF(@Charttype = 1)
	BEGIN
		
		SELECT  ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total
		FROM    dbo.UserQuestions
		WHERE   UserTestID = @UserTestID
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
         WHERE   UserTestID = @UserTestID
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]
GO

CREATE PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByUserTestID]
	@UserTestID int,
    @UserType varchar(5)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @TestId INT
	
	SELECT @TestId = TestId
	FROM UserTests
	WHERE UserTestID=@UserTestID

IF (@UserType = 'S' )
BEGIN
	 SELECT Category.TableName FROM  TestCategory INNER JOIN
	 Category ON TestCategory.CategoryID = Category.CategoryID
	 WHERE Student=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END
ELSE
BEGIN
	SELECT Category.TableName FROM  TestCategory INNER JOIN
	Category ON TestCategory.CategoryID = Category.CategoryID
	WHERE [Admin]=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END

SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestCharacteristicsForUserAdminByTestID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByTestID]
GO

CREATE PROCEDURE [dbo].[uspReturnTestCharacteristicsForUserAdminByTestID]
	@TestID int,
    @UserType varchar(5)
AS
BEGIN
	SET NOCOUNT ON
	
IF (@UserType = 'S' )
BEGIN
	 SELECT Category.TableName FROM  TestCategory INNER JOIN
	 Category ON TestCategory.CategoryID = Category.CategoryID
	 WHERE Student=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END
ELSE
BEGIN
	SELECT Category.TableName FROM  TestCategory INNER JOIN
	Category ON TestCategory.CategoryID = Category.CategoryID
	WHERE [Admin]=1 AND TestID=@TestID ORDER BY Category.OrderNumber
END

SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnProbabilityByTestAndNumberCorrect]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnProbabilityByTestAndNumberCorrect]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnProbability]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnProbability]
GO

CREATE PROCEDURE dbo.uspReturnProbability
@UserTestId INT,
@NumberCorrect FLOAT
AS
BEGIN
	DECLARE @TestId INT
	
	SELECT @TestId = TestId
	FROM UserTests
	WHERE UserTestID=@UserTestID
	
	SELECT Probability
	FROM Norming
	WHERE TestID= @TestId AND NumberCorrect= @NumberCorrect
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnPercentileRankByTestAndNumberCorrect]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnPercentileRankByTestAndNumberCorrect]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnPercentileRank]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnPercentileRank]
GO

CREATE PROCEDURE dbo.uspReturnPercentileRank
@UserTestId INT,
@NumberCorrect FLOAT
AS
BEGIN
	
	DECLARE @TestId INT
	
	SELECT @TestId = TestId
	FROM UserTests
	WHERE UserTestID=@UserTestID
	
	SELECT PercentileRank
	FROM Norming
	WHERE TestID= @TestId
	AND NumberCorrect= @NumberCorrect
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckPercentileRankExistForTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckPercentileRankExistForTest]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsByCohortIDAndProductIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsByCohortIDAndProductIDs]
GO

CREATE PROCEDURE dbo.uspGetTestsByCohortIDAndProductIDs
	@ProductIds nVarchar(2000),
	@CohortIds nVarchar(2000)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT DISTINCT Tests.TestName,
	       UserTests.TestID
	FROM   UserTests
	       JOIN Tests
	            ON  UserTests.TestID = Tests.TestID
	WHERE  (
	           (
	               @ProductIds <> ''
	               AND Tests.ProductID IN (SELECT *
	                                       FROM   dbo.funcListToTableInt(@ProductIds, '|'))
	           )
	           OR (@ProductIds = '')
	       )
	       AND (
	               (
	                   @CohortIds <> ''
	                   AND UserTests.CohortID IN (SELECT *
	                                              FROM   dbo.funcListToTableInt(@CohortIds, '|'))
	               )
	               OR (@CohortIds = '')
	           )
	ORDER BY
	       Tests.TestName ASC
	SET NOCOUNT OFF;

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheProgramForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheProgramForChart]
GO

CREATE PROCEDURE dbo.uspGetResultsFromTheProgramForChart
@UserTestID INT,
@ChartType VARCHAR(20)
AS
BEGIN
	DECLARE @TestId INT
	
	SELECT @TestId = TestID
	FROM UserTests
	WHERE UserTestID = @UserTestId
	
	IF (@ChartType = 'LevelOfDifficulty')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText, B.Norm
        FROM  dbo.UserQuestions INNER JOIN
        dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
        dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
        LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.LevelOfDifficulty.LevelOfDifficultyID FROM dbo.Norm INNER JOIN
        dbo.LevelOfDifficulty ON  dbo.Norm.ChartID = dbo.LevelOfDifficulty.LevelOfDifficultyID
			WHERE (dbo.Norm.TestID = @TestId ) and (ChartType= @ChartType)) B ON B.LevelOfDifficultyID = dbo.Questions.LevelOfDifficultyID
        WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)
        GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty,dbo.LevelOfDifficulty.OrderNumber, B.Norm
        ORDER BY dbo.LevelOfDifficulty.OrderNumber

	END
	ELSE IF (@ChartType = 'NursingProcess')
	BEGIN
		SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID
		FROM dbo.Norm INNER JOIN
		dbo.NursingProcess ON
		dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID
		WHERE (dbo.Norm.TestID =  @TestId )  and (ChartType = @ChartType )) B ON B.NursingProcessID = dbo.Questions.NursingProcessID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestId )
		GROUP BY dbo.NursingProcess.NursingProcess,dbo.NursingProcess.OrderNumber, B.Norm
		ORDER BY dbo.NursingProcess.OrderNumber
	END
	ELSE IF (@ChartType = 'ClinicalConcept')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID
		FROM dbo.Norm INNER JOIN
		dbo.ClinicalConcept ON
		dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID
		WHERE (dbo.Norm.TestID =  @TestId ) and (ChartType = @ChartType)) B ON B.ClinicalConceptID = dbo.Questions.ClinicalConceptsID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)
		GROUP BY dbo.ClinicalConcept.ClinicalConcept,dbo.ClinicalConcept.OrderNumber, B.Norm
		ORDER BY dbo.ClinicalConcept.OrderNumber
	END
	ELSE IF (@ChartType = 'ClientNeeds')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeeds.ClientNeeds as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID
		FROM dbo.Norm INNER JOIN
		dbo.ClientNeeds ON
		dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID
		WHERE (dbo.Norm.TestID = @TestId )  and (ChartType = @ChartType) ) B ON B.ClientNeedsID = dbo.Questions.ClientNeedsID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)
		GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,B.Norm
		ORDER BY dbo.ClientNeeds.ClientNeedsID
	END
	ELSE IF (@ChartType = 'ClientNeedCategory')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.ClientNeedCategory.ClientNeedCategory as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID
		FROM dbo.Norm INNER JOIN
		dbo.ClientNeedCategory ON
		dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID
		WHERE (dbo.Norm.TestID =  @TestId)  and (ChartType=@ChartType)) B ON B.ClientNeedCategoryID = dbo.Questions.ClientNeedsCategoryID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)
		GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,B.Norm
		ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID
	END
	ELSE IF (@ChartType = 'Demographic')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Demographic.Demographic as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID
		FROM dbo.Norm INNER JOIN
		dbo.Demographic ON
		dbo.Norm.ChartID = dbo.Demographic.DemographicID
		WHERE (dbo.Norm.TestID =  @TestId)  and (ChartType=@ChartType)) B ON B.DemographicID = dbo.Questions.DemographicID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)
		GROUP BY dbo.Demographic.Demographic,dbo.Demographic.OrderNumber, B.Norm
		ORDER BY dbo.Demographic.OrderNumber
	END
	ELSE IF (@ChartType = 'CognitiveLevel')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID
		FROM dbo.Norm INNER JOIN
		dbo.CognitiveLevel ON
		dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID
		WHERE (dbo.Norm.TestID = @TestId)  and (ChartType= @ChartType)) B ON B.CognitiveLevelID = dbo.Questions.CognitiveLevelID
		WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)
		GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,B.Norm
		ORDER BY dbo.CognitiveLevel.CognitiveLevelID
	END
	ELSE IF (@ChartType = 'CriticalThinking')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID
		FROM dbo.Norm INNER JOIN  dbo.CriticalThinking ON dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID
		WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.CriticalThinkingID = dbo.Questions.CriticalThinkingID
		WHERE     (dbo.UserQuestions.UserTestID =  @UserTestID)
		GROUP BY dbo.CriticalThinking.CriticalThinking, B.Norm
	END
	ELSE IF (@ChartType = 'SpecialtyArea')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID
		FROM dbo.Norm INNER JOIN dbo.SpecialtyArea ON dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID
		WHERE (dbo.Norm.TestID = @TestId)  and (ChartType= @ChartType)) B ON B.SpecialtyAreaID = dbo.Questions.SpecialtyAreaID
		WHERE     (dbo.UserQuestions.UserTestID =@UserTestID)
		GROUP BY dbo.SpecialtyArea.SpecialtyArea, B.Norm
	END
	ELSE IF (@ChartType = 'Systems')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Systems.System as ItemText, B.Norm
		FROM  dbo.UserQuestions INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Systems.SystemID
		FROM dbo.Norm INNER JOIN dbo.Systems ON dbo.Norm.ChartID = dbo.Systems.SystemID
		WHERE (dbo.Norm.TestID = @TestId)  and (ChartType=@ChartType)) B ON B.SystemID = dbo.Questions.SystemID
		WHERE     (dbo.UserQuestions.UserTestID = @UserTestID)
		GROUP BY dbo.Systems.System, B.Norm
	END
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestAssignment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestAssignment]
GO
CREATE PROCEDURE dbo.uspGetTestAssignment
@UserTestId INT
AS
BEGIN
	
	DECLARE @TestId INT
	
	SELECT @TestId = TestID
	FROM UserTests
	WHERE UserTestID = @UserTestId

	SELECT Category.TableName, Category.OrderNumber, TestCategory.[Admin]
    FROM   TestCategory
	INNER JOIN Category ON TestCategory.CategoryID = Category.CategoryID
    WHERE     TestCategory.TestID = @TestId

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationTimeByTestId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetRemediationTimeByTestId]
GO

CREATE PROCEDURE dbo.uspGetRemediationTimeByTestId
@UserTestID INT,
@TypeOfFileID CHAR(2)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT TOP 100 PERCENT dbo.Questions.ClientNeedsCategoryID, dbo.Questions.QID, dbo.Questions.QuestionID, dbo.Questions.QuestionType,
    dbo.Questions.RemediationID, dbo.Remediation.TopicTitle, dbo.UserQuestions.QuestionNumber, dbo.Questions.TypeOfFileID,
    dbo.LevelOfDifficulty.LevelOfDifficulty, dbo.NursingProcess.NursingProcess, dbo.ClinicalConcept.ClinicalConcept, dbo.Questions.SpecialtyAreaID,
    dbo.Demographic.Demographic, dbo.NursingProcess.NursingProcessID, dbo.UserQuestions.TimeSpendForQuestion, dbo.UserQuestions.TimeSpendForExplanation,
    dbo.UserQuestions.Correct,
    dbo.Tests.TestName, dbo.UserQuestions.TimeSpendForRemedation, dbo.ClientNeeds.ClientNeeds, dbo.Questions.ClientNeedsID,
    dbo.Questions.ClinicalConceptsID, dbo.Questions.LevelOfDifficultyID, dbo.Questions.CriticalThinkingID, dbo.CriticalThinking.CriticalThinking,
    dbo.Questions.CognitiveLevelID, dbo.CognitiveLevel.CognitiveLevel, dbo.SpecialtyArea.SpecialtyArea, dbo.Questions.SystemID, dbo.Systems.[System],
    dbo.ClientNeedCategory.ClientNeedCategory
    FROM         dbo.UserQuestions INNER JOIN
    dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN
    dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN
    dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
    dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID LEFT OUTER JOIN
    dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID LEFT OUTER JOIN
    dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID LEFT OUTER JOIN
    dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID LEFT OUTER JOIN
    dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID LEFT OUTER JOIN
    dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID LEFT OUTER JOIN
    dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID LEFT OUTER JOIN
    dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID LEFT OUTER JOIN
    dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID LEFT OUTER JOIN
    dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID LEFT OUTER JOIN
    dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
    WHERE     (dbo.UserQuestions.UserTestID = @UserTestID) AND (dbo.Questions.TypeOfFileID = @TypeOfFileID)
    ORDER BY dbo.UserQuestions.QuestionNumber
	
	SET NOCOUNT OFF
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentReportCardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudentReportCardDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetStudentReportCardDetails]
@StudentIds varchar(max),
@TestIds varchar(max),
@CohortIds varchar(max),
@GroupIds varchar(max),
@InstitutionId int,
@TestTypeId varchar(100)
AS
BEGIN

SET NOCOUNT ON;

	SELECT
		SI.UserID, SI.FirstName, SI.LastName,
		C.CohortID, C.CohortName,
		P.ProductId TestTypeId, P.ProductName TestType,
		T.TestId, T.TestName,
		UT.TestStarted TestTaken,
		case
			when p.productid = 1 then
				ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForRemedation),0),108),'')
			when p.productid IN (3, 4) then
				ISNULL(CONVERT(CHAR(8),DATEADD(ss,sum(TimeSpendForExplanation),0),108),'')
			end AS remediationTime,
		ISNULL(G.GroupName,'') GroupName,
		ISNULL(((dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)*1.0)*100) / dbo.UFNReturnTotalPercentByUserIDTestID(UT.UserTestId),0) AS Correct,
		CASE
			WHEN dbo.UFNReturnPercentileRankByTestIDNumberCorrect(T.TestId,dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)) <> 0 THEN
				CAST(dbo.UFNReturnPercentileRankByTestIDNumberCorrect(T.TestId,dbo.UFNReturnTotalCorrectPercentByUserIDTestID(UT.UserTestId)) AS VARCHAR(10))
			ELSE
				CASE
					WHEN dbo.UFNCheckPercentileRankExistForTest(T.TestId) = 0 THEN
						'n/a'
					ELSE
						'0'
				END
		END [Rank],
		UT.UserTestId
	FROM  dbo.UserTests UT INNER JOIN Tests T ON T.TestId = UT.TestId AND UT.TestStatus = 1
		INNER JOIN dbo.UserQuestions AS UQ ON UT.UserTestID = UQ.UserTestID
		INNER JOIN Products P ON T.ProductId = P.ProductId
		AND P.ProductId IN(select value from  dbo.funcListToTableInt(@TestTypeId,'|'))
		INNER JOIN dbo.NurCohort C ON UT.CohortID = C.CohortID
		INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UT.UserID
		INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
		INNER JOIN dbo.NurCohort C2 ON SA.CohortID = C2.CohortID
		LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
	WHERE UT.InsitutionId = @InstitutionId
		AND ((SI.UserID IN ( select value from  dbo.funcListToTableInt(@StudentIds,'|')))
		OR @StudentIds = '0')
		AND ((@TestIds <> '0,' AND UT.TestId IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		OR @TestIds = '0,' )
		AND ((@CohortIds <> '0' AND C.CohortId IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))) OR @CohortIds = '0')
		AND ((@GroupIds <> '' AND G.GroupId IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|'))) OR @GroupIds = '')
	GROUP BY UT.UserTestId,SI.UserID, SI.FirstName, SI.LastName,
		C.CohortID, C.CohortName,
		P.ProductId, P.ProductName,
		T.TestId, T.TestName,
		UT.TestStarted
		,G.GroupName

SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTestsByProductAndUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnTestsByProductAndUser]
GO

CREATE PROCEDURE [dbo].[uspReturnTestsByProductAndUser]
	@ProductID int,
    @UserID int
AS

BEGIN
	SET NOCOUNT ON

	DECLARE @Hour INT

	SELECT @Hour = TimeZones.Hour
	 FROM NurStudentInfo INNER JOIN NurInstitution ON NurStudentInfo.InstitutionID = NurInstitution.InstitutionID
	 INNER JOIN TimeZones ON NurInstitution.TimeZone = TimeZones.TimeZoneID
	 WHERE NurStudentInfo.UserID = @UserID

IF (@ProductID != 0 )
BEGIN
	SELECT dbo.UserTests.UserTestID, dbo.Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour, dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
	FROM  dbo.Tests INNER JOIN
	dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID INNER JOIN
	dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
	WHERE  TestStatus=1  AND UserID=@UserID AND dbo.Tests.ProductID=@ProductID
END
ELSE
BEGIN
	SELECT dbo.UserTests.UserTestID, dbo.Tests.TestName +CAST(' (' as varchar) +CAST(DATEADD(hour, @hour,dbo.UserTests.TestStarted) as varchar) +CAST(')' as varchar) as TestName
	FROM  dbo.Tests INNER JOIN
	dbo.Products ON dbo.Tests.ProductID = dbo.Products.ProductID INNER JOIN
	dbo.UserTests ON dbo.Tests.TestID = dbo.UserTests.TestID
	WHERE  TestStatus=1  AND UserID=@UserID
END

SET NOCOUNT OFF

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortsByStudentID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetCohortsByStudentID
GO

CREATE PROCEDURE dbo.uspGetCohortsByStudentID
@StudentId INT
AS
BEGIN
	SELECT dbo.NurCohort.CohortID, dbo.NurCohort.CohortName
    FROM dbo.NurCohort
    INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
    INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
    WHERE  (dbo.NurStudentInfo.UserID = @StudentId)
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestByInstitutionResults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetTestByInstitutionResults
GO

CREATE PROCEDURE dbo.uspGetTestByInstitutionResults
@InstitutionIds VARCHAR(MAX),
@CohortIds VARCHAR(MAX),
@ProductId INT,
@TestId INT
AS
BEGIN
	
	SELECT Cast((100.0*SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) )/ SUM(1) as Decimal(4,1)) AS Percantage,
		dbo.NurCohort.CohortName,
		dbo.NurCohort.CohortID,
		dbo.NurCohort.InstitutionID,
		dbo.NurInstitution.InstitutionName,
		COUNT( DISTINCT dbo.UserTests.UserID) AS NStudents
     FROM   dbo.UserQuestions
     INNER JOIN dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
     INNER JOIN dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID
     INNER JOIN dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID
     INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID = dbo.NurInstitution.InstitutionID
     WHERE TestStatus = 1
	 AND (@ProductId = 0 OR (@ProductId <> 0 AND dbo.Tests.ProductID = @ProductId))
	 AND (@TestId = 0 OR (@TestId <> 0 AND dbo.Tests.TestID = @TestId))
	 AND (dbo.NurCohort.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|')))
     AND (dbo.NurCohort.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|')))
	 GROUP BY
	 dbo.NurCohort.InstitutionID,
	 dbo.NurInstitution.InstitutionName,
	 dbo.NurCohort.CohortName,
	 dbo.NurCohort.CohortID
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfStudents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetListOfStudents
GO

CREATE PROCEDURE [dbo].[uspGetListOfStudents]
 @CohortId int,
 @InstitutionId int,
 @CaseId int,
 @sText VARCHAR(MAX)

AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 Select Distinct S.UserID,S.LastName,S.FirstName,S.UserName,S.EnrollmentID
   from CaseModuleScore M
 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
 INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
 INNER JOIN NurCohort C ON A.CohortID=C.CohortID
 WHERE UserType='S' AND UserDeleteData is null
 AND (A.DeletedDate IS NULL) AND I.InstitutionID = @InstitutionId
  AND C.CohortID = @CohortId AND M.CaseID=@CaseId AND
 (FirstName like '%'+ @sText + '%' OR LastName like '%' + @sText + '%' OR  UserName like '%' + @sText + '%')

 SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfStudentsByCohortID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetListOfStudentsByCohortID
GO

CREATE PROCEDURE [dbo].[uspGetListOfStudentsByCohortID]
@CohortID int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

	SELECT  dbo.NurStudentInfo.UserID, dbo.NurStudentInfo.FirstName, dbo.NurStudentInfo.LastName
	, dbo.NurStudentInfo.LastName+','+dbo.NurStudentInfo.FirstName as Name, dbo.NurStudentInfo.UserName
	FROM   dbo.NurStudentInfo
	INNER JOIN	dbo.NusStudentAssign ON dbo.NurStudentInfo.UserID = dbo.NusStudentAssign.StudentID
	WHERE (dbo.NusStudentAssign.DeletedDate IS NULL)
	AND (dbo.NusStudentAssign.CohortID IS NULL OR dbo.NusStudentAssign.CohortID=0 Or dbo.NusStudentAssign.CohortID=@CohortID)
	
 SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetModule
GO

CREATE PROCEDURE [dbo].[uspGetModule]
AS

BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 SELECT ModuleID,ModuleName
 FROM  NurModule

 SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseByCohortResult]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetCaseByCohortResult
GO

CREATE PROCEDURE [dbo].[uspGetCaseByCohortResult]
	@InstitutionIds VARCHAR(MAX),
	@CaseID int,
	@ModuleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Select CAST(SUM(M.Correct)*100.0/SUM(M.Total) AS numeric(10,1)) AS Percantage,
	count(S.EnrollmentID) As NStudents,
	C.CohortID, C.CohortName ,C.InstitutionId
	from CaseModuleScore M
	INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	INNER JOIN NurCohort C ON A.CohortID=C.CohortID
	WHERE UserType='S' AND UserDeleteData is null
	AND (A.DeletedDate IS NULL)
    AND M.CaseID=@CaseID AND M.ModuleID=@ModuleID
    AND S.InstitutionID IN (SELECT value
		FROM dbo.funcListToTableInt(@InstitutionIds,'|'))
	Group BY C.CohortID,C.CohortName,C.InstitutionId
	
	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationTimeForNCLEX]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetRemediationTimeForNCLEX
GO

CREATE PROCEDURE dbo.uspGetRemediationTimeForNCLEX
@UserTestID INT,
@TypeOfFileID CHAR(2)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT  dbo.ClientNeeds.ClientNeeds, dbo.Questions.ClientNeedsCategoryID, dbo.ClientNeedCategory.ClientNeedCategory,
            dbo.NursingProcess.NursingProcess,  dbo.Questions.QID, dbo.Questions.QuestionID,dbo.Questions.QuestionType,dbo.Questions.RemediationID,
            dbo.Remediation.TopicTitle,dbo.Demographic.Demographic,
            dbo.UserQuestions.QuestionNumber,TypeOfFileID, dbo.UserQuestions.TimeSpendForQuestion, dbo.UserQuestions.Correct,
            dbo.UserQuestions.TimeSpendForExplanation,
            dbo.Tests.TestName, dbo.UserQuestions.TimeSpendForRemedation

            FROM  dbo.UserQuestions INNER JOIN
            dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID  INNER JOIN
            dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID  INNER JOIN

            dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
            dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID LEFT OUTER JOIN
            dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID LEFT OUTER JOIN
            dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID LEFT OUTER JOIN
            dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
            LEFT OUTER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID

            WHERE     (dbo.UserQuestions.UserTestID = @UserTestID) AND TypeOfFileID=@TypeOfFileID

	
	SET NOCOUNT OFF
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsForStudentReportCardByModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetResultsForStudentReportCardByModule
GO

CREATE PROCEDURE dbo.uspGetResultsForStudentReportCardByModule
@InstitutionIds INT,
@CohortId INT,
@ModuleIds VARCHAR(MAX),
@StudentIds VARCHAR(MAX),
@CaseID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT t.*,(CaseModuleScore.Correct*100.0/CaseModuleScore.Total) As Correct  FROM
	(SELECT nsi.FirstName,nsi.LastName,nsi.EnrollmentID,ModuleID,ModuleName
	FROM NurStudentInfo nsi
	CROSS JOIN (select * from NurModule WHERE ModuleID IN ( select value from  dbo.funcListToTableInt(@ModuleIds,'|') )) t1
	WHERE (nsi.InstitutionID = 0 OR (nsi.InstitutionID <> 0 AND nsi.InstitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))))
	AND (nsi.UserID = 0 OR (nsi.UserID <> 0 AND nsi.UserID IN ( select value from  dbo.funcListToTableInt(@StudentIds,'|'))))) t
	LEFT JOIN CaseModuleScore ON CaseModuleScore.StudentID = t.EnrollmentID AND CaseModuleScore.CaseID = @CaseID
	WHERE CaseModuleScore.ModuleID IN ( select value from  dbo.funcListToTableInt(@ModuleIds,'|') )
	
	SET NOCOUNT OFF
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortResultbyModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetCohortResultbyModule
GO

CREATE PROCEDURE dbo.uspGetCohortResultbyModule
 @CohortIds VARCHAR(MAX),
 @CaseID int,
 @MID VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	 Select ISNULL(SUM(M.Correct),0) AS Correct, ISNULL(SUM(M.Total),0) As Total
	 from CaseModuleScore M
	 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	 WHERE UserType='S' AND UserDeleteData is NULL
	 AND A.CohortID IN (SELECT VALUE FROM dbo.funcListToTableInt(@CohortIds,'|'))
	 AND (A.DeletedDate IS NULL)
	 AND M.CaseID=@CaseID
	 AND M.ModuleID IN (SELECT VALUE FROM dbo.funcListToTableInt(@MID,'|'))

	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseSubCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetCaseSubCategories
GO

CREATE PROCEDURE [dbo].[uspGetCaseSubCategories]
AS
BEGIN
 SET NOCOUNT ON;

 select distinct CategoryName from caseSubCategory

 SET NOCOUNT OFF

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCaseSubCategoryResultbyCohortModule]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetCaseSubCategoryResultbyCohortModule
GO

CREATE PROCEDURE [dbo].[uspGetCaseSubCategoryResultbyCohortModule]
	@CohortIds VARCHAR(MAX),
	@CaseID int,
	@ModuleIds varchar(max),
	@CategoryName varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	
	Select CS.SubcategoryID, SUM(CS.Correct) AS Correct, SUM(CS.Total) As Total
	from CaseModuleScore M
	INNER JOIN CaseSubCategory CS ON M.ModuleStudentID = CS.ModuleStudentID
	INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
	INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
	INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
	WHERE A.CohortID IN (SELECT VALUE FROM dbo.funcListToTableInt(@CohortIds,'|'))
	AND UserType='S'
	AND UserDeleteData is null
	AND (A.DeletedDate IS NULL)
    AND M.CaseID=@CaseID
    AND M.ModuleID  IN (SELECT VALUE FROM dbo.funcListToTableInt(@ModuleIds,'|'))
    AND CS.CategoryName = @CategoryName
	Group BY CS.SubcategoryID

	SET NOCOUNT OFF
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionResultsForInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetQuestionResultsForInstitution
GO

CREATE PROCEDURE dbo.uspGetQuestionResultsForInstitution
@InstitutionId INT,
@TestTypes VARCHAR(100),
@TestIds VARCHAR(MAX),
@ChartType INT
AS
BEGIN
		if (@ChartType = 1)
        BEGIN
        	 SELECT  Cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0/ SUM(1) as decimal(4,1)) AS Total
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.InsitutionID= @InstitutionId
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID, dbo.UserTests.InsitutionID, dbo.UserTests.ProductID
        END
        else if (@ChartType = 2)
        BEGIN
        	SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS N_Correct,
             SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
             SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
             SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
             SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
             SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.InsitutionID= @InstitutionId
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID,dbo.UserTests.InsitutionID, dbo.UserTests.ProductID
        END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionResultsForCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetQuestionResultsForCohort
GO

CREATE PROCEDURE dbo.uspGetQuestionResultsForCohort
@CohortIds VARCHAR(MAX),
@TestTypes VARCHAR(100),
@TestIds VARCHAR(MAX),
@ChartType INT,
@GroupIds VARCHAR(MAX)
AS
BEGIN
		if (@ChartType = 1)
        BEGIN
        	 SELECT  Cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0/ SUM(1) as decimal(4,1)) AS Total
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
			 INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
			 INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
			 LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
			 AND ((@GroupIds <> '' AND G.GroupID IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|')))
             OR (@GroupIds = ''))
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID, dbo.UserTests.CohortID, dbo.UserTests.ProductID
        END
        else if (@ChartType = 2)
        BEGIN
        	 SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END)  AS N_Correct,
             SUM(CASE WHEN correct = 0 THEN 1 ELSE 0 END)  AS N_InCorrect,
             SUM(CASE WHEN correct = 2 THEN 1 ELSE 0 END)  AS N_NAnswered,
             SUM(CASE WHEN AnswerChanges ='CI' THEN 1 ELSE 0 END)  AS N_CI,
             SUM(CASE WHEN AnswerChanges ='II' THEN 1 ELSE 0 END)  AS N_II,
             SUM(CASE WHEN AnswerChanges ='IC' THEN 1 ELSE 0 END)  AS N_IC
             FROM    dbo.UserQuestions
             INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
			 INNER JOIN dbo.NurStudentInfo SI ON SI.UserID = UserTests.UserID
			 INNER JOIN dbo.NusStudentAssign SA ON SI.UserID = SA.StudentID
			 LEFT JOIN dbo.NurGroup G ON SA.GroupId = G.GroupId AND SA.CohortID = G.CohortId
             WHERE  (dbo.UserTests.ProductID IN (select value from  dbo.funcListToTableInt(@TestTypes,'|')))
             AND TestStatus = 1
			 AND dbo.UserTests.CohortID IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
			 AND ((@GroupIds <> '' AND G.GroupID IN ( select value from  dbo.funcListToTableInt(@GroupIds,'|')))
             OR (@GroupIds = ''))
             AND TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
             GROUP BY dbo.UserTests.TestID,dbo.UserTests.CohortID, dbo.UserTests.ProductID
        END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheCohortForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetResultsFromTheCohortForChart
GO

CREATE PROCEDURE dbo.uspGetResultsFromTheCohortForChart
@CohortIds VARCHAR(MAX),
@TestTypeIds VARCHAR(MAX),
@TestIds VARCHAR(MAX),
@ChartType VARCHAR(50),
@FromDate varchar(10),
@ToDate VARCHAR(10)
AS
BEGIN
	
	IF (@ChartType = 'LevelOfDifficulty')
	BEGIN
		SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,
		SUM(1) AS Total, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText,
		dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID
		LEFT OUTER JOIN dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.Category = 15
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.LevelOfDifficulty.LevelOfDifficultyID
		FROM dbo.Norm
		INNER JOIN dbo.LevelOfDifficulty ON dbo.Norm.ChartID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		WHERE dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		and (ChartType=@ChartType) ) B ON B.LevelOfDifficultyID = dbo.Questions.LevelOfDifficultyID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty,  dbo.CategoryPercentage.Percentage,dbo.LevelOfDifficulty.OrderNumber, B.Norm	
	END
	ELSE IF (@ChartType = 'NursingProcess')
	BEGIN
		SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,
		SUM(1) AS Total, dbo.NursingProcess.NursingProcess as ItemText,
		dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID
		AND dbo.CategoryPercentage.Category = 16
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID
		FROM dbo.Norm
		INNER JOIN dbo.NursingProcess ON dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		and (ChartType=@ChartType)) B
		ON B.NursingProcessID = dbo.Questions.NursingProcessID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.NursingProcess.NursingProcess, dbo.CategoryPercentage.Percentage,dbo.NursingProcess.OrderNumber, B.Norm
	END
	ELSE IF (@ChartType = 'ClinicalConcept')
	BEGIN
		SELECT SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N,
		SUM(1) AS Total, dbo.ClinicalConcept.ClinicalConcept as ItemText,
		dbo.CategoryPercentage.Percentage, B.Norm
		FROM         dbo.ClinicalConcept INNER JOIN
		dbo.UserQuestions INNER JOIN
		dbo.UserTests ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID INNER JOIN
		dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID ON
		dbo.ClinicalConcept.ClinicalConceptID = dbo.Questions.ClinicalConceptsID LEFT OUTER JOIN
		dbo.CategoryPercentage ON dbo.UserTests.TestID = dbo.CategoryPercentage.TestID AND
		dbo.ClinicalConcept.ClinicalConceptID = dbo.CategoryPercentage.CategoryFields AND dbo.CategoryPercentage.Category = 4
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID
		FROM dbo.Norm INNER JOIN
		dbo.ClinicalConcept ON
		dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')) )
		and (ChartType=@ChartType)) B ON B.ClinicalConceptID = dbo.Questions.ClinicalConceptsID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClinicalConcept.ClinicalConcept, dbo.CategoryPercentage.Percentage,dbo.ClinicalConcept.OrderNumber, B.Norm
	END
	ELSE IF (@ChartType = 'ClientNeeds')
	BEGIN
		SELECT  SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		dbo.ClientNeeds.ClientNeeds as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeeds.ClientNeedsID
		AND dbo.CategoryPercentage.Category = 1
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID
		FROM dbo.Norm
		INNER JOIN dbo.ClientNeeds ON dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		and (ChartType=@ChartType)) B ON B.ClientNeedsID = dbo.Questions.ClientNeedsID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID,dbo.CategoryPercentage.Percentage, B.Norm
	END
	ELSE IF (@ChartType = 'ClientNeedCategory')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		dbo.ClientNeedCategory.ClientNeedCategory as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID
		AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeedCategory.ClientNeedCategoryID
		AND dbo.CategoryPercentage.Category = 18
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID
		FROM dbo.Norm
		INNER JOIN  dbo.ClientNeedCategory ON dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		and (ChartType= @ChartType)) B ON B.ClientNeedCategoryID = dbo.Questions.ClientNeedsCategoryID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,
		dbo.CategoryPercentage.Percentage, B.Norm
	END
	ELSE IF (@ChartType = 'Demographic')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		dbo.Demographic.Demographic as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID
		AND dbo.CategoryPercentage.CategoryFields = dbo.Demographic.DemographicID
		AND dbo.CategoryPercentage.Category = 6
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID
		FROM dbo.Norm
		INNER JOIN dbo.Demographic ON dbo.Norm.ChartID = dbo.Demographic.DemographicID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		and (ChartType=@ChartType)) B ON B.DemographicID = dbo.Questions.DemographicID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.Demographic.Demographic, dbo.CategoryPercentage.Percentage,dbo.Demographic.OrderNumber, B.Norm
	END
	ELSE IF (@ChartType = 'CognitiveLevel')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CognitiveLevel.CognitiveLevel as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CognitiveLevel.CognitiveLevelID
		AND dbo.CategoryPercentage.Category = 17
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID
		FROM dbo.Norm INNER JOIN dbo.CognitiveLevel ON dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))
		and (ChartType= @ChartType)) B ON B.CognitiveLevelID = dbo.Questions.CognitiveLevelID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID,dbo.CategoryPercentage.Percentage, B.Norm
	END
	ELSE IF (@ChartType = 'CriticalThinking')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.CriticalThinking.CriticalThinking as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CriticalThinking.CriticalThinkingID
		AND dbo.CategoryPercentage.Category = 5	
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID
		FROM dbo.Norm INNER JOIN
		dbo.CriticalThinking ON
		dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.CriticalThinkingID = dbo.Questions.CriticalThinkingID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.CriticalThinking.CriticalThinking, dbo.CategoryPercentage.Percentage, B.Norm                	
	END
	ELSE IF (@ChartType = 'SpecialtyArea')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.SpecialtyArea.SpecialtyArea as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.SpecialtyArea.SpecialtyAreaID
		AND dbo.CategoryPercentage.Category = 11
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID
		FROM dbo.Norm INNER JOIN
		dbo.SpecialtyArea ON
		dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.SpecialtyAreaID = dbo.Questions.SpecialtyAreaID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.SpecialtyArea.SpecialtyArea, dbo.CategoryPercentage.Percentage, B.Norm
	END
	ELSE IF (@ChartType = 'Systems')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total, dbo.Systems.System as ItemText,  dbo.CategoryPercentage.Percentage, B.Norm
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.Systems.SystemID
		AND dbo.CategoryPercentage.Category = 12
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Systems.SystemID
		FROM dbo.Norm INNER JOIN
		dbo.Systems ON
		dbo.Norm.ChartID = dbo.Systems.SystemID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|')))  and (ChartType=@ChartType)) B ON B.SystemID = dbo.Questions.SystemID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.Systems.System, dbo.CategoryPercentage.Percentage, B.Norm
	END
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNormForTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetNormForTest
GO

CREATE PROCEDURE dbo.uspGetNormForTest
@TestId INT
AS
BEGIN
	
	SELECT top 1 Norm.Norm
	FROM Norm
	WHERE (TestID = @TestId)
	AND (ChartType = 'overall')
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestRank]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetTestRank
GO

CREATE PROCEDURE dbo.uspGetTestRank
@TestId INT
AS
BEGIN
	SELECT   ISNULL(AVG(PercentileRank),0) AS Rank
	FROM         dbo.Norming
	WHERE     TestID = @TestId
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromInstitutionForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.uspGetResultsFromInstitutionForChart
GO

CREATE PROCEDURE dbo.uspGetResultsFromInstitutionForChart
@InstitutionIds VARCHAR(MAX),
@TestTypeIds VARCHAR(MAX),
@TestIds VARCHAR(MAX),
@ChartType VARCHAR(50),
@FromDate varchar(10),
@ToDate VARCHAR(10)
AS
BEGIN
	
	IF (@ChartType = 'LevelOfDifficulty')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.LevelOfDifficulty.LevelOfDifficulty as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.LevelOfDifficulty ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.Category = 15
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.LevelOfDifficulty.LevelOfDifficultyID
		FROM dbo.Norm INNER JOIN
		dbo.LevelOfDifficulty ON
		dbo.Norm.ChartID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  ) and (ChartType= @ChartType)) B ON B.LevelOfDifficultyID = dbo.Questions.LevelOfDifficultyID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.LevelOfDifficulty.LevelOfDifficulty,  dbo.CategoryPercentage.Percentage
		,dbo.LevelOfDifficulty.OrderNumber, B.Norm,dbo.NurInstitution.InstitutionName
		 ORDER BY dbo.LevelOfDifficulty.OrderNumber,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'NursingProcess')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		dbo.NursingProcess.NursingProcess as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.Category = 16
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.NursingProcess.NursingProcessID
		FROM dbo.Norm INNER JOIN
		dbo.NursingProcess ON
		dbo.Norm.ChartID = dbo.NursingProcess.NursingProcessID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.NursingProcessID = dbo.Questions.NursingProcessID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.NursingProcess.NursingProcess, dbo.CategoryPercentage.Percentage
		,dbo.NursingProcess.OrderNumber, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.NursingProcess.OrderNumber,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'ClinicalConcept')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.ClinicalConcept.ClinicalConcept as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM         dbo.ClinicalConcept INNER JOIN
		dbo.UserQuestions INNER JOIN
		dbo.UserTests ON dbo.UserTests.UserTestID = dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID ON
		dbo.ClinicalConcept.ClinicalConceptID = dbo.Questions.ClinicalConceptsID LEFT OUTER JOIN
		dbo.CategoryPercentage ON dbo.UserTests.TestID = dbo.CategoryPercentage.TestID AND
		dbo.ClinicalConcept.ClinicalConceptID = dbo.CategoryPercentage.CategoryFields AND dbo.CategoryPercentage.Category = 4
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClinicalConcept.ClinicalConceptID
		FROM dbo.Norm INNER JOIN
		dbo.ClinicalConcept ON
		dbo.Norm.ChartID = dbo.ClinicalConcept.ClinicalConceptID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  ) and (ChartType= @ChartType)) B ON B.ClinicalConceptID = dbo.Questions.ClinicalConceptsID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClinicalConcept.ClinicalConcept, dbo.CategoryPercentage.Percentage
		,dbo.ClinicalConcept.OrderNumber, B.Norm,dbo.NurInstitution.InstitutionName
		 ORDER BY dbo.ClinicalConcept.OrderNumber,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'ClientNeeds')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.ClientNeeds.ClientNeeds as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm, dbo.ClientNeeds.ClientNeedsID,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeeds ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeeds.ClientNeedsID AND dbo.CategoryPercentage.Category = 1
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeeds.ClientNeedsID
		FROM dbo.Norm INNER JOIN
		dbo.ClientNeeds ON
		dbo.Norm.ChartID = dbo.ClientNeeds.ClientNeedsID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType) ) B ON B.ClientNeedsID = dbo.Questions.ClientNeedsID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClientNeeds.ClientNeeds,dbo.ClientNeeds.ClientNeedsID
		,dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.ClientNeeds.ClientNeeds,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'ClientNeedCategory')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.ClientNeedCategory.ClientNeedCategory as ItemText,dbo.ClientNeedCategory.ClientNeedCategoryID
		,dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.ClientNeedCategory.ClientNeedCategoryID AND dbo.CategoryPercentage.Category = 18
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.ClientNeedCategory.ClientNeedCategoryID
		FROM dbo.Norm INNER JOIN
		dbo.ClientNeedCategory ON
		dbo.Norm.ChartID = dbo.ClientNeedCategory.ClientNeedCategoryID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.ClientNeedCategoryID = dbo.Questions.ClientNeedsCategoryID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.ClientNeedCategory.ClientNeedCategoryID,
		dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		 ORDER BY dbo.ClientNeedCategory.ClientNeedCategory,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'Demographic')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.Demographic.Demographic as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.Demographic.DemographicID AND dbo.CategoryPercentage.Category = 6
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Demographic.DemographicID
		FROM dbo.Norm INNER JOIN
		dbo.Demographic ON
		dbo.Norm.ChartID = dbo.Demographic.DemographicID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.DemographicID = dbo.Questions.DemographicID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.Demographic.Demographic, dbo.CategoryPercentage.Percentage
		,dbo.Demographic.OrderNumber, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.Demographic.OrderNumber,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'CognitiveLevel')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total,
		dbo.CognitiveLevel.CognitiveLevel as ItemText,dbo.CognitiveLevel.CognitiveLevelID,
		dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CognitiveLevel.CognitiveLevelID AND dbo.CategoryPercentage.Category = 17
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CognitiveLevel.CognitiveLevelID
		FROM dbo.Norm INNER JOIN
		dbo.CognitiveLevel ON
		dbo.Norm.ChartID = dbo.CognitiveLevel.CognitiveLevelID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.CognitiveLevelID = dbo.Questions.CognitiveLevelID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.CognitiveLevel.CognitiveLevel,dbo.CognitiveLevel.CognitiveLevelID
		,dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.CognitiveLevel.CognitiveLevel,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'CriticalThinking')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.CriticalThinking.CriticalThinking as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.CriticalThinking.CriticalThinkingID AND dbo.CategoryPercentage.Category = 5
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.CriticalThinking.CriticalThinkingID
		FROM dbo.Norm INNER JOIN
		dbo.CriticalThinking ON
		dbo.Norm.ChartID = dbo.CriticalThinking.CriticalThinkingID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.CriticalThinkingID = dbo.Questions.CriticalThinkingID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.CriticalThinking.CriticalThinking, dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName                 	
		ORDER BY dbo.CriticalThinking.CriticalThinking,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'SpecialtyArea')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.SpecialtyArea.SpecialtyArea as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.SpecialtyArea.SpecialtyAreaID AND dbo.CategoryPercentage.Category = 11
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.SpecialtyArea.SpecialtyAreaID
		FROM dbo.Norm INNER JOIN
		dbo.SpecialtyArea ON
		dbo.Norm.ChartID = dbo.SpecialtyArea.SpecialtyAreaID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.SpecialtyAreaID = dbo.Questions.SpecialtyAreaID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.SpecialtyArea.SpecialtyArea, dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.SpecialtyArea.SpecialtyArea,dbo.NurInstitution.InstitutionName
	END
	ELSE IF (@ChartType = 'Systems')
	BEGIN
		SELECT    SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) AS Correct_N, SUM(1) AS Total
		, dbo.Systems.System as ItemText,  dbo.CategoryPercentage.Percentage
		, B.Norm,dbo.NurInstitution.InstitutionName
		FROM  dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		INNER JOIN dbo.NurInstitution ON dbo.UserTests.InsitutionID=dbo.NurInstitution.InstitutionID
		INNER JOIN dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN
		dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
		LEFT OUTER JOIN  dbo.CategoryPercentage ON dbo.CategoryPercentage.TestID = dbo.UserTests.TestID AND dbo.CategoryPercentage.CategoryFields = dbo.Systems.SystemID AND dbo.CategoryPercentage.Category = 12
		LEFT OUTER JOIN (SELECT dbo.Norm.Norm, dbo.Systems.SystemID
		FROM dbo.Norm INNER JOIN
		dbo.Systems ON
		dbo.Norm.ChartID = dbo.Systems.SystemID
		WHERE (dbo.Norm.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))  )  and (ChartType= @ChartType)) B ON B.SystemID = dbo.Questions.SystemID
		WHERE   ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|'))
		AND TestStatus = 1
		AND InsitutionID IN ( select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))
		AND dbo.UserTests.TestID IN ( select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0
		AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0
		AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.Systems.System, dbo.CategoryPercentage.Percentage, B.Norm,dbo.NurInstitution.InstitutionName
		ORDER BY dbo.Systems.System,dbo.NurInstitution.InstitutionName
	END
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromInstitutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetResultsFromInstitutions
GO

CREATE PROCEDURE dbo.uspGetResultsFromInstitutions
@InstitutionIDs VARCHAR(MAX),
@TestTypeIds VARCHAR(MAX),
@TestIDs VARCHAR(MAX),
@Charttype INT,
@FromDate VARCHAR(10),
@ToDate VARCHAR(10)
AS
BEGIN
	IF(@Charttype = 1)
	BEGIN
		SELECT ISNULL(CAST(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / SUM(1) AS numeric(5,1)),0) AS Total,
		dbo.UserTests.InsitutionID
		FROM dbo.UserQuestions
		INNER JOIN dbo.UserTests ON dbo.UserTests.UserTestID=dbo.UserQuestions.UserTestID
		WHERE ((@TestTypeIds='0') OR ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|')))
		AND TestStatus = 1
		AND dbo.UserTests.TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIDs,'|'))
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
        WHERE ((@TestTypeIds='0') OR ProductID IN ( select value from  dbo.funcListToTableInt(@TestTypeIds,'|')))
		AND TestStatus = 1
		AND dbo.UserTests.TestID IN (select value from  dbo.funcListToTableInt(@TestIds,'|'))
		AND dbo.UserTests.InsitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIDs,'|'))
		AND (RTRIM(LTRIM(@FromDate)) = '' OR (ISDATE(@FromDate) > 0 AND (dbo.UserTests.TestComplited >= CONVERT(DATETIME,@FromDate))))
		AND (RTRIM(LTRIM(@ToDate)) = '' OR (ISDATE(@ToDate) > 0 AND (dbo.UserTests.TestStarted <= CONVERT(DATETIME,@ToDate))))
		GROUP BY dbo.UserTests.TestID,dbo.UserTests.InsitutionID, dbo.UserTests.ProductID
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnStudentSummaryByAnswerChoice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPReturnStudentSummaryByAnswerChoice
GO

CREATE PROCEDURE [dbo].[uspReturnStudentSummaryByAnswerChoice]
	@ProductId int,
	@CohortIds varchar(MAX),
	@TestId int
AS
BEGIN
	Select UserTests.UserID,Questions.QuestionID, UserAnswers.Correct
	from UserTests, UserAnswers, Questions
	where UserTests.UserTestID=UserAnswers.UserTestID
	AND UserAnswers.QID=Questions.QID
	AND dbo.UserTests.CohortID IN ( select value from  dbo.funcListToTableInt(@CohortIds,'|'))
	AND UserTests.TestID= @TestId
	AND UserTests.ProductID=@ProductId	
	order by UserId,Questions.QId
END
GO	
	
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsByCohortQuestions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetResultsByCohortQuestions
GO

CREATE PROCEDURE dbo.uspGetResultsByCohortQuestions
@ProductId INT,
@TestId INT,
@CohortIds VARCHAR(MAX)
AS
BEGIN

	SELECT  dbo.Questions.QuestionID,dbo.Remediation.TopicTitle
	,Case WHEN dbo.Questions.Q_Norming is null or dbo.Questions.Q_Norming=-1 Then -100.0
	ELSE Cast(dbo.Questions.Q_Norming as numeric(10,1))
	END AS Q_Norming INTO #result
	FROM   dbo.Questions
	INNER JOIN TestQuestions ON dbo.Questions.QuestionID = dbo.TestQuestions.QuestionID
	INNER JOIN dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
	WHERE dbo.TestQuestions.TestID = @TestId
	ORDER BY dbo.Questions.QuestionID

	-- Fetch Cohort Names
	Create table #Cohorts (ID int identity(1,1),CohortId INT,CohortName Varchar(100))

	Insert into #Cohorts (CohortId,CohortName)
	select CohortId,CohortName
	FROM NurCohort
	WHERE CohortId IN (select value from  dbo.funcListToTableInt(@CohortIds,'|'))
	order by CohortName

	-- Add the cohort Names as columns to result table and update Percentage
	declare @count int
	select @count = max(id) from #Cohorts
	declare @index int
	set @index =1

	declare @colName varchar(50)
	DECLARE @CohortId INT
	
	while(@index<=@count)
	BEGIN
			select @colName = CohortName, @CohortId = CohortId
			from #Cohorts
			where id = @index
			
			exec('alter table #result add [' + @colName + '] numeric(10,1) ')
			
			-- Fetch Percentage for Cohort		
			SELECT Cast( 100.0 * SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) / SUM(1) as numeric(10,1)) AS Percentage,
			dbo.Questions.QuestionID INTO #CohortPercentage
			FROM   dbo.UserQuestions
			INNER JOIN  dbo.Questions ON dbo.UserQuestions.QID = dbo.Questions.QID
			INNER JOIN  dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID
			INNER JOIN  dbo.Tests ON dbo.UserTests.TestID = dbo.Tests.TestID
			INNER JOIN  dbo.NurCohort ON dbo.UserTests.CohortID = dbo.NurCohort.CohortID
			INNER JOIN  dbo.Remediation ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
			WHERE TestStatus = 1
			AND dbo.Tests.ProductID= @ProductId
			AND dbo.Tests.TestID=@TestId
			AND dbo.NurCohort.CohortID= @CohortId
			GROUP BY dbo.Questions.QuestionID, dbo.UserTests.CohortID,dbo.Tests.ProductID,dbo.Tests.TestID
			, dbo.Remediation.TopicTitle,Q_Norming
			ORDER BY dbo.Questions.QuestionID

			-- update Percentage into main Table
			exec('UPDATE #result
			SET ['+ @colName + '] = p.Percentage
			FROM #result main
			INNER JOIN #CohortPercentage p ON p.QuestionID = main.QuestionID')
			
			exec('UPDATE #result
			SET ['+ @colName + '] = ''-100''
			 FROM #result WHERE ['+ @colName + '] IS NULL')
			
			set @index = @index + 1
			
			DROP TABLE #CohortPercentage
	END

	SELECT * FROM #result
	ORDER BY TopicTitle
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentNumberByCohortTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].USPGetStudentNumberByCohortTest
GO

CREATE PROCEDURE dbo.uspGetStudentNumberByCohortTest
@ProductId INT,
@TestId INT,
@CohortId INT
AS
BEGIN
	select ISNULL(count(testID),0) AS Number
	from dbo.UserTests
	WHERE TestStatus = 1
	AND ProductID= @ProductId
	AND TestID= @TestId
	AND CohortID=@CohortId
	GROUP BY TestID
END
GO

-- Report SPs

/****** Object:  StoredProcedure [dbo].[uspGetCohorts]    Script Date: 04/22/2011 14:46:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohorts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCohorts]
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
		C.InstitutionId
	FROM dbo.NurCohort C
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

/****** Object:  StoredProcedure [dbo].[uspSearchCohorts]    Script Date: 04/22/2011 15:26:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchCohorts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchCohorts]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchCohorts]    Script Date: 04/22/2011 15:26:31 ******/

CREATE PROCEDURE [dbo].[uspSearchCohorts]
(
 @InstitutionIds VARCHAR(8000),
 @SearchString VARCHAR(1000),
 @TestId INT,
 @State VARCHAR(1000),
 @Type VARCHAR(1000),
 @DateFrom VARCHAR(15),
 @DateTo VARCHAR(15),
 @CohortStatus INT
)
AS

SET NOCOUNT ON

DECLARE @stateValue VARCHAR(2)
SET @stateValue = '-3'

BEGIN
 IF (@DateTo != ''  OR @DateFrom != '')
  BEGIN
	 SELECT
	 C.CohortID,
	 C.CohortDescription,
	 C.CohortName,
	 C.CohortStartDate,
	 C.CohortEndDate,
	 I.InstitutionName,
	 A.StateName,
	 I.ContractualStartDate,
	 I.Annotation,
	 count(Distinct S.StudentID) As Students
	  FROM   dbo.NurCohort C
	  LEFT OUTER JOIN dbo.NurInstitution I
	  ON C.InstitutionID = I.InstitutionID
	  LEFT OUTER JOIN dbo.Address A
	  ON A.AddressID = I.AddressId
	  LEFT OUTER JOIN dbo.CountryState CS
	  ON CS.StateName = A.StateName
	  LEFT JOIN dbo.NusStudentAssign S
	  ON C.CohortID = S.CohortID
	  LEFT OUTER JOIN dbo.NurCohortPrograms CP
	  ON C.CohortID = CP.CohortID
	  LEFT JOIN dbo.NurProgramProduct PP
	  ON CP.ProgramID = PP.ProgramID
	  LEFT JOIN dbo.NurProductDatesCohort D
	  ON D.CohortID = C.CohortID
	 WHERE C.CohortStatus = @CohortStatus AND S.DeletedDate IS NULL
	 AND (@TestId = 0 OR D.ProductID =  @TestId)
	 AND (@InstitutionIds = '' OR I.InstitutionId IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
	 AND (@SearchString = '' OR(C.CohortName like +'%'+ @SearchString +'%'))
	 AND ((@State = '' OR (CS.StateID IN (SELECT value FROM dbo.funcListToTableInt(@State,'|')) AND @State <> @stateValue)) or (@State = @stateValue AND A.CountryId != 235))
	 AND (@Type = '' OR C.CohortDescription LIKE '%' + @Type +'%')
	 AND (@DateFrom = '' OR D.StartDate  > CAST(CONVERT(VARCHAR(10), @DateFrom, 111) AS DATETIME))
	 AND (@DateTo = '' OR D.EndDate < CAST(CONVERT(VARCHAR(10), @DateTo, 111) AS DATETIME))
	 GROUP BY C.CohortID,C.CohortDescription, C.CohortName, C.CohortStartDate,
	 C.CohortEndDate, I.InstitutionName,I.ContractualStartDate,A.StateName,I.Annotation
  END
ELSE
  BEGIN
	SELECT
		C.CohortID,
		C.CohortDescription,
		C.CohortName,
		C.CohortStartDate,
		C.CohortEndDate,
		I.InstitutionName,
		A.StateName,
		I.ContractualStartDate,
		I.Annotation,
		COUNT(Distinct S.StudentID) As Students
	FROM dbo.NurCohort C
		LEFT OUTER JOIN dbo.NurInstitution I
		ON C.InstitutionID = I.InstitutionID
		LEFT OUTER JOIN dbo.Address A
		ON A.AddressID = I.AddressId
		LEFT OUTER JOIN dbo.CountryState CS
		ON CS.StateName = A.StateName
		LEFT OUTER JOIN dbo.NusStudentAssign S
		ON S.CohortID = C.CohortID
		LEFT OUTER JOIN dbo.NurProductDatesCohort D
		ON D.CohortID = C.CohortID AND D.ProductID =  @TestId AND D.[Type] = 0
	WHERE C.CohortStatus = @CohortStatus AND S.DeletedDate IS NULL
		AND (@InstitutionIds = '' OR I.InstitutionId IN (SELECT value FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
		AND (@SearchString = '' OR( C.CohortName like +'%'+ @SearchString +'%'))
		AND((@State = '' OR (CS.StateID IN (SELECT value FROM dbo.funcListToTableInt(@State,'|')) AND @State <> @stateValue)) or (@State = @stateValue AND A.CountryId != 235))
		AND (@Type = '' OR C.CohortDescription LIKE '%' + @Type +'%')
	GROUP BY C.CohortID,C.CohortDescription, C.CohortName, C.CohortStartDate,
		C.CohortEndDate, I.InstitutionName,I.ContractualStartDate,A.StateName,I.Annotation
  END
END
SET NOCOUNT OFF

GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutions]    Script Date: 04/22/2011 23:02:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetInstitutions]
GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutions]    Script Date: 04/22/2011 23:02:53 ******/
CREATE PROCEDURE [dbo].[uspGetInstitutions]
(
 @InstitutionId int,
 @InstitutionIds varchar(max),
 @UserId int
)
AS

SET NOCOUNT ON

 -- Query is same in both Then and Else blocks except that
 --  check SecurityLevel = 0 for user accessing Then block
 --  do a inner join to NurAdminInstitution table in Else block
 IF NOT EXISTS(SELECT 1
  FROM dbo.NurAdminInstitution
  WHERE AdminId = @UserId)
 BEGIN
   SELECT   InstitutionID,
		   InstitutionName,
		   [Description],
		   ContactName,
		   ContactPhone,
		   DefaultCohortID,
		   CenterID,
		   TimeZone,
		   IP,
		   FacilityID,
		   ProgramID,
		   0 AS Active,
		   I.[Status],
		   AddressID,
		   I.Annotation,
		   I.ContractualStartDate,
		   I.Email
  FROM dbo.NurInstitution I
  WHERE 0
   IN (SELECT SecurityLevel -- Unfortunately 0 is assigned to Super Admin which is very risky.
   FROM dbo.NurAdmin
   WHERE UserId = @UserId)
	  AND (@InstitutionId = 0
	  OR I.InstitutionId = @InstitutionId)
	  AND (@InstitutionIds = ''
	  OR I.InstitutionId IN
	  (SELECT value  FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
 END
 ELSE
 BEGIN
  SELECT   I.InstitutionID,
		   InstitutionName,
		   [Description],
		   ContactName,
		   ContactPhone,
		   DefaultCohortID,
		   CenterID,
		   TimeZone,
		   IP,
		   FacilityID,
		   ProgramID,
		   NAI.Active,
		   I.[Status],
		   AddressID,
		   I.Annotation,
		   I.ContractualStartDate,
		   I.Email
  FROM dbo.NurInstitution I
	  INNER JOIN NurAdminInstitution NAI
	  ON NAI.InstitutionId = I.InstitutionId
  WHERE NAI.AdminId = @UserId
	  AND (@InstitutionId = 0
	  OR I.InstitutionId = @InstitutionId)
	  AND (@InstitutionIds = ''
	  OR I.InstitutionId IN
      (SELECT value  FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
 END

SET NOCOUNT OFF

GO
--
/****** Object:  StoredProcedure [dbo].[uspSearchInstitutions]    Script Date: 04/22/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchInstitutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchInstitutions]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchInstitutions]    Script Date: 04/22/2011 23:12:59 ******/

CREATE PROCEDURE [dbo].[uspSearchInstitutions]
(
 @SearchText varchar(200),
 @UserId int
)
AS

SET NOCOUNT ON

 -- Query is same in both Then and Else blocks except that
 --  check SecurityLevel = 0 for user accessing Then block
 --  do a inner join to NurAdminInstitution table in Else block
 IF NOT EXISTS(SELECT 1
  FROM dbo.NurAdminInstitution
  WHERE AdminId = @UserId)
 BEGIN
	  SELECT InstitutionID,
	   InstitutionName,
	   I.[Description],
	   ContactName,
	   ContactPhone,
	   DefaultCohortID,
	   CenterID,
	   TimeZone,
	   TZ.Description TZD,
	   IP,
	   FacilityID,
	   ProgramID,
	   [Status]
  FROM dbo.NurInstitution I
	  INNER JOIN TimeZones AS TZ ON TZ.TimeZoneID = I.TimeZone
	  WHERE 0 IN (SELECT SecurityLevel -- Unfortunately 0 is assigned to Super Admin which is very risky.
	   FROM dbo.NurAdmin
	   WHERE UserId = @UserId)
		  AND (I.InstitutionName LIKE '%' + @SearchText + '%'
		  OR I.[Description] LIKE '%' + @SearchText + '%'
		  OR I.ContactName LIKE '%' + @SearchText + '%'
		  OR I.ContactPhone LIKE '%' + @SearchText + '%'
		  OR I.CenterID LIKE '%' + @SearchText + '%')
 END
 ELSE
 BEGIN
	  SELECT I.InstitutionID,
	   InstitutionName,
	   I.[Description],
	   ContactName,
	   ContactPhone,
	   DefaultCohortID,
	   CenterID,
	   TimeZone,
	   TZ.Description TZD,
	   IP,
	   FacilityID,
	   ProgramID,
	   [Status]
  FROM dbo.NurInstitution I
	  INNER JOIN NurAdminInstitution NAI  ON NAI.InstitutionId = I.InstitutionId
	  INNER JOIN TimeZones AS TZ  ON TZ.TimeZoneID = I.TimeZone
	  WHERE NAI.AdminId = @UserId
		  AND (I.InstitutionName LIKE '%' + @SearchText + '%'
		  OR I.[Description] LIKE '%' + @SearchText + '%'
		  OR I.ContactName LIKE '%' + @SearchText + '%'
		  OR I.ContactPhone LIKE '%' + @SearchText + '%'
		  OR I.CenterID LIKE '%' + @SearchText + '%')
 END

SET NOCOUNT OFF


GO

/****** Object:  StoredProcedure [dbo].[uspSaveGroup]    Script Date: 04/25/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveGroup]
GO
/****** Object:  StoredProcedure [dbo].[uspSaveGroup]    Script Date: 04/25/2011 16:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSaveGroup]
@GroupName nvarchar(100),
@CohortID int,
@GroupUserID int,
@GroupID int,
@newGroupID int OUTPUT

AS

SET NOCOUNT ON

BEGIN
	IF @GroupID = 0
		BEGIN
			INSERT INTO NurGroup
			(
			GroupName,
			CohortID,
			GroupInsertDate,
			GroupInsertUser
			)
			VALUES
			(
			@GroupName,
			@CohortID,
			getdate(),
			@GroupUserID
			)
			
			SELECT @newGroupID = SCOPE_IDENTITY()
		END
ELSE
		BEGIN
			UPDATE NurGroup
			SET GroupName = @GroupName,
            GroupUpdateDate = getdate(),
			GroupUpdateUser = @GroupUserID,
			CohortID = @CohortID
            WHERE GroupID = @GroupID
			
			SELECT @newGroupID = @GroupID
		END
END
Go


/****** Object:  StoredProcedure [dbo].[uspSearchGroups]    Script Date: 04/25/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchGroups]
GO
/****** Object:  StoredProcedure [dbo].[uspSearchGroups]    Script Date: 04/25/2011 16:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [dbo].[uspSearchGroups]
(
 @SearchString varchar(max),
 @InstitutionIds varchar(2000)
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
		C.InstitutionID,
		G.GroupName,
		C.CohortName,
		G.GroupID,
		C.CohortStatus,
		C.CohortID,
		I.InstitutionName,
		G.GroupDeleteDate,
		I.Status
		FROM dbo.NurGroup G
		LEFT OUTER JOIN dbo.NurCohort C
		ON G.CohortID = C.CohortID
		LEFT OUTER JOIN dbo.NurInstitution I
		ON C.InstitutionID = I.InstitutionID
		WHERE  (I.Status = 1
		OR I.Status IS NULL)
		AND (C.CohortStatus = 1
		OR C.CohortStatus IS NULL)
		AND (G.GroupDeleteDate IS NULL)
		AND (@InstitutionIds = ''
		OR C.InstitutionId IN
		(SELECT value
		FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
		AND (@SearchString = ''
				OR( GroupName like +'%'+ @SearchString + '%'
				OR CohortName like +'%'+ @SearchString +'%'
				OR InstitutionName like +'%' + @SearchString + '%'))
	END
		
Go
/****** Object:  StoredProcedure [dbo].[uspGetGroup]    Script Date: 04/25/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspGetGroup]    Script Date: 04/25/2011 16:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[uspGetGroup]
(
 @GroupId int ,
 @CohortIds varchar(max)
)
AS
SET NOCOUNT ON;
BEGIN

	SELECT	G.GroupID,
			G.GroupName,
			G.CohortID,
			G.GroupUpdateUser,
			G.GroupDeleteUser,
			G.GroupInsertUser,
			G.GroupUpdateDate,
			G.GroupInsertDate,
			G.GroupDeleteDate			
	FROM dbo.NurGroup G			
	WHERE GroupDeleteDate IS NULL
	AND (@GroupId = 0
	OR	G.GroupID = @GroupId)
	AND ( @CohortIds = ''
	OR G.CohortID IN
		(SELECT value
		 FROM dbo.funcListToTableInt(@CohortIds,'|')))
END

--END
Go
/****** Object:  StoredProcedure [dbo].[uspDeleteGroup]    Script Date: 04/25/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteGroup]    Script Date: 04/25/2011 16:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteGroup]
@GroupDeleteUser int,
@GroupId int
AS

SET NOCOUNT ON

BEGIN
	UPDATE NurGroup
	SET GroupDeleteUser = @GroupDeleteUser,
	GroupDeleteDate=getdate() WHERE GroupID = @GroupID
END
Go

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteLippinCottByLippinCottId' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteLippinCottByLippinCottId]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteLippinCott]
GO


CREATE PROCEDURE [dbo].[uspDeleteLippinCott]
@LippincottId int
AS
BEGIN
	Delete from QuestionLippincott where LippincottID = @LippincottId
    Delete from dbo.Lippincot where LippincottID = @LippincottId
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetQuestionsAndLippincottsByLippincottID' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionsAndLippincottsByLippincottID]
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetQuestionsAndLippincotts' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionsAndLippincotts]
GO


CREATE PROCEDURE [dbo].[uspGetQuestionsAndLippincotts]
@LippincottId int
AS
BEGIN
	SELECT Lippincot.LippincottID, Questions.QuestionID, Questions.QID
    FROM Lippincot INNER JOIN QuestionLippincott ON Lippincot.LippincottID = QuestionLippincott.LippincottID
    INNER JOIN Questions ON QuestionLippincott.QID = Questions.QID
    WHERE (Lippincot.LippincottID =@LippincottId)
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertQuestionLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertQuestionLippinCott]
GO


CREATE PROCEDURE [dbo].[uspInsertQuestionLippinCott]
@QID as int,
@LippincottID as int
AS
BEGIN
if Not Exists(select QID from  QuestionLippincott where QID= @QID and LippincottID=@LippincottID)
 Begin
 Insert into dbo.QuestionLippincott(QID,LippincottID) values(@QID,@LippincottID)
 End
End
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetQuestionLippincottByIds' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionLippincottByIds]
GO


CREATE PROCEDURE [dbo].[uspGetQuestionLippincottByIds]
@QID int,
@LippincottId int,
@LippincottIds Varchar(4000)
AS
BEGIN
 select QID,LippincottID from QuestionLippincott
 where (QID=@QID or @QID =0)  and (LippincottID = @LippincottId or @LippincottId=0)
 and   ( (LippincottID in (select * from dbo.funcListToTableInt(@LippincottIds,'|'))) or @LippincottIds ='')
END

GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteQuestionLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteQuestionLippinCott]
GO


CREATE PROCEDURE [dbo].[uspDeleteQuestionLippinCott]
@LippincottId int,
@QID int
AS
BEGIN
 Delete from QuestionLippincott where LippincottID = @LippincottId  and  QID=@QID
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertUpdateRemediation' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertUpdateRemediation]
GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveRemediation' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveRemediation]
GO

CREATE PROCEDURE [dbo].[uspSaveRemediation]
@RemediationId as int OUTPUT,
@Explanation as Varchar(5000),
@TopicTitle as Varchar(500),
@ReleaseStatus as char(1)
AS
BEGIN
 If(@RemediationID = 0)
  Begin
   INSERT INTO Remediation(Explanation, TopicTitle, ReleaseStatus) Values(@Explanation,@TopicTitle,@ReleaseStatus)
   SET @RemediationID = CONVERT(int, SCOPE_IDENTITY())
  End
 Else
  Begin
  UPDATE  Remediation  SET Explanation=@Explanation, TopicTitle=@TopicTitle, ReleaseStatus = @ReleaseStatus WHERE RemediationID=@RemediationID
  End
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincottRemediationByTitle' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincottRemediationByTitle]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSearchLippincotts' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSearchLippincotts]
GO

CREATE PROCEDURE [dbo].[uspSearchLippincotts]
@LippinCottTitle varchar(800)
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle
 FROM dbo.Lippincot  LEFT OUTER JOIN dbo.Remediation ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 where (LippincottTitle like '%'+@LippinCottTitle+'%')
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincottRemediationById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincottRemediationById]
GO


CREATE PROCEDURE [dbo].[uspGetLippincottRemediationById]
@LippinCottID INT
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle ,
 LippincottExplanation,LippincottTitle2,LippincottExplanation2 FROM dbo.Lippincot  LEFT OUTER JOIN dbo.Remediation ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 where (Lippincot.LippincottID = @LippinCottID or @LippinCottID = -1 )
END
GO


--This Sp will be changed once question page is ready
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetQuestionsByQuestionId' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestionsByQuestionId]
GO


CREATE PROCEDURE [dbo].[uspGetQuestionsByQuestionId]
@QuestionID as varchar(50)
AS
BEGIN
  SELECT QID,QuestionID,RemediationID,Remediation,TopicTitleID FROM dbo.Questions WHERE QuestionID=@QuestionID
END
GO


--This Sp will be changed once question page is ready
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertUpdateQuestion' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertUpdateQuestion]
GO

CREATE PROCEDURE [dbo].[uspInsertUpdateQuestion]
@QId as int,
@QuestionId as Varchar(50),
@RemediationId as int,
@TopicTitle as Varchar(500)
AS
BEGIN
Declare @Id as int
    set @Id = @QId
	If(@QId = 0)
	 Begin
	  INSERT INTO dbo.Questions(QuestionId,RemediationId,TopicTitleId ) Values(@QuestionId,@RemediationId,@TopicTitle)
	  set @Id = Scope_identity()
	 End
	Else
	 Begin
		UPDATE  Questions  SET QuestionId=@QuestionId, TopicTitleId=@TopicTitle, RemediationId = @RemediationId
		WHERE QID=@QId
	 End
	 select QID,QuestionId,RemediationId,TopicTitleId from Questions where QID = @Id
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertUpdateLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertUpdateLippinCott]
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveLippinCott]
GO

CREATE PROCEDURE [dbo].[uspSaveLippinCott]
@LippincottId as int output,
@RemediationId INT,
@LippincottTitle varchar(800),
@LippincottExplanation as ntext,
@LippincottTitle2 as varchar(800),
@LippincottExplanation2 as ntext
AS
BEGIN

	If(@LippincottId =0)
	 Begin
		 INSERT INTO dbo.Lippincot(RemediationID,LippincottTitle,LippincottExplanation,LippincottTitle2,LippincottExplanation2,ReleaseStatus)
		 values(@RemediationID,@LippincottTitle,@LippincottExplanation,@LippincottTitle2,@LippincottExplanation2,'E')
		 SET @LippincottId = CONVERT(int, SCOPE_IDENTITY())
	 End
	Else
	 Begin
		 UPDATE  Lippincot  SET RemediationID=@RemediationID,LippincottTitle=@LippincottTitle,
		      LippincottExplanation=@LippincottExplanation,LippincottTitle2=@LippincottTitle2,LippincottExplanation2=@LippincottExplanation2,ReleaseStatus='E'
		 Where  LippincottID= @LippincottId
	 End
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincottById' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincottById]
GO

CREATE PROCEDURE [dbo].[uspGetLippincottById]
@LippincottId int
AS
BEGIN
	select * from Lippincot where LippincottID = @LippincottId
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincottByRemediationId' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincottByRemediationId]
GO


CREATE PROCEDURE [dbo].[uspGetLippincottByRemediationId]
@RemediationId INT
AS
BEGIN
	SELECT LippincottID, RemediationID, LippincottTitle,LippincottExplanation,LippincottTitle2,LippincottExplanation2,ReleaseStatus
	FROM dbo.Lippincot
	where (Lippincot.RemediationID = @RemediationId)
END
GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetQuestions' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetQuestions]
GO

CREATE PROCEDURE [dbo].[uspGetQuestions]
@QuestionId as varchar(50),
@QID as int,
@RemediationId as varchar(5000) ,
@ForEdit bit ,
@ReleaseStatus char(1)
AS
BEGIN
  IF @ForEdit = 0
   BEGIN
    SELECT
    '' AS RE,
    Q.Remediation,
    Q.QuestionType,
    Q.QID,
    Q.QuestionID,
    Q.Stimulus,
    Q.Stem,
    Q.ListeningFileUrl,
    Q.Explanation,
    Q.ProductLineID,
    Q.PointBiserialsID,
    Q.Statisctics,
    Q.CreatorID,
    Q.DateCreated,
    Q.EditorID,
    Q.DateEdited,
    Q.EditorID_2,
    Q.DateEdited_2,
    Q.Source_SBD,
    Q.Feedback,
    Q.WhoOwns,
    Q.ItemTitle,
    Q.TypeOfFileID,
    Q.QuestionType,
    Q.active,
    Q.Q_Norming,
    Q.RemediationID,
    Q.ClinicalConceptsID,
    Q.CriticalThinkingID,
    Q.SystemID,
    Q.SpecialtyAreaID,
    Q.CognitiveLevelID,
    Q.DemographicID,
    Q.LevelOfDifficultyID,
    Q.NursingProcessID,
    Q.ClientNeedsID,
    Q.ClientNeedsCategoryID,
    Q.ExhibitTab1,
    Q.ExhibitTab2,
    Q.ExhibitTab3,
    Q.ListeningFileUrl,
    Q.ExhibitTitle1,
    Q.ExhibitTitle2,
    Q.ExhibitTitle3,
    Q.TopicTitleID,
    Q.XMLQID,
    Q.IntegratedConceptsID,
    Q.ReadingCategoryID,
    Q.ReadingID,
    Q.WritingCategoryID,
    Q.WritingID,
    Q.MathCategoryID,
    Q.MathID,
    Q.WhereUsed,
    Q.Deleted,
    Q.QuestionNumber,
    Q.TestNumber,
    Q.ReleaseStatus,
	Q.AlternateStem
    FROM dbo.Questions Q
    WHERE
   (Q.QuestionID=@QuestionId or @QuestionId ='')
    AND (Q.QID = @QID or @QID = 0)
    AND (
    Q.RemediationID=@RemediationId
    OR @RemediationId=''
     ) AND (ReleaseStatus = @ReleaseStatus or @ReleaseStatus='')

  END
 ELSE
  BEGIN
   SELECT R.Explanation AS RE,
    Q.Remediation,
    Q.QuestionType,
    Q.QID,
    Q.QuestionID,
    Q.Stimulus,
    Q.Stem,
    Q.ListeningFileUrl,
    Q.Explanation,
    Q.ProductLineID,
    Q.PointBiserialsID,
    Q.Statisctics,
    Q.CreatorID,
    Q.DateCreated,
    Q.EditorID,
    Q.DateEdited,
    Q.EditorID_2,
    Q.DateEdited_2,
    Q.Source_SBD,
    Q.Feedback,
    Q.WhoOwns,
    Q.ItemTitle,
    Q.TypeOfFileID,
    Q.QuestionType,
    Q.active,
    Q.Q_Norming,
    Q.RemediationID,
    Q.ClinicalConceptsID,
    Q.CriticalThinkingID,
    Q.SystemID,
    Q.SpecialtyAreaID,
    Q.CognitiveLevelID,
    Q.DemographicID,
    Q.LevelOfDifficultyID,
    Q.NursingProcessID,
    Q.ClientNeedsID,
    Q.ClientNeedsCategoryID,
    Q.ExhibitTab1,
    Q.ExhibitTab2,
    Q.ExhibitTab3,
    Q.ListeningFileUrl,
    Q.ExhibitTitle1,
    Q.ExhibitTitle2,
    Q.ExhibitTitle3,
    Q.TopicTitleID,
    Q.XMLQID,
    Q.IntegratedConceptsID,
    Q.ReadingCategoryID,
    Q.ReadingID,
    Q.WritingCategoryID,
    Q.WritingID,
    Q.MathCategoryID,
    Q.MathID,
    Q.WhereUsed,
    Q.Deleted,
    Q.QuestionNumber,
    Q.TestNumber,
    Q.ReleaseStatus,
	Q.AlternateStem
   FROM
   dbo.Questions Q
   LEFT OUTER JOIN dbo.Remediation  R
   ON Q.RemediationID = R.RemediationID
   WHERE QID = @QID
  END
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetRemediations' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetRemediations]
GO

CREATE PROCEDURE [dbo].[uspGetRemediations]
@RemediationId as int,
@ReleaseStatus as char(1)
AS
BEGIN
  SELECT RemediationID,Explanation,TopicTitle,ReleaseStatus
  FROM Remediation
  WHERE (RemediationID=@RemediationId or @RemediationId =0)
  and ( ReleaseStatus = @ReleaseStatus or @ReleaseStatus ='')
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteRemediation' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteRemediation]
GO

CREATE PROCEDURE [dbo].[uspDeleteRemediation]
@RemediationId as int
AS
BEGIN
 DELETE FROM Remediation WHERE RemediationID=@RemediationId
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincottAssignedInQuestion' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincottAssignedInQuestion]
GO

CREATE PROCEDURE [dbo].[uspGetLippincottAssignedInQuestion]
@qId as int
AS
BEGIN
 SELECT     Questions.QID, Lippincot.LippincottTitle, Lippincot.LippincottExplanation, Lippincot.LippincottTitle2,
 Lippincot.LippincottExplanation2, Lippincot.LippincottID
    FROM Lippincot INNER JOIN
    QuestionLippincott ON Lippincot.LippincottID = QuestionLippincott.LippincottID INNER JOIN
    Questions ON QuestionLippincott.QID = Questions.QID
    WHERE(Questions.QID = @qId )
    union all
    SELECT     Questions.QID, Lippincot.LippincottTitle, Lippincot.LippincottExplanation, Lippincot.LippincottTitle2,
    Lippincot.LippincottExplanation2, Lippincot.LippincottID
    FROM         Questions INNER JOIN
    Lippincot ON Questions.RemediationID = Lippincot.RemediationID
    WHERE(Questions.QID =  @qId )
END

GO


/****** Object:  StoredProcedure [dbo].[uspSearchStudent]    Script Date: 04/25/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchStudent]
GO

CREATE PROCEDURE [dbo].[uspSearchStudent]
@InstitutionId INT,
@CohortId INT,
@GroupId INT,
@SearchString VARCHAR(400),
@AssignStudent BIT
AS
BEGIN
	IF @AssignStudent = 0
			BEGIN
				SELECT
					S.UserID,
					S.FirstName,
					S.LastName,
					S.UserName,
					S.UserPass,
					S.Telephone,
					S.Email,
					S.UserType,
					S.InstitutionID,
					S.CountryCode,
					S.UserCreateDate,
					S.UserExpireDate,
					S.UserStartDate,
					S.UserUpdateDate,
					S.UserDeleteData,
					S.Integreted,
					S.KaplanUserID,
					S.EnrollmentID,
					S.ADA,
					S.SessionID ,
					A.CohortID,
					A.GroupID,
					I.InstitutionName,
					C.CohortName,
					G.GroupName
				FROM NurStudentInfo S
				LEFT OUTER JOIN NusStudentAssign A
				ON S.UserID=A.StudentID
				LEFT JOIN NurInstitution I
				ON S.InstitutionID=I.InstitutionID
				LEFT OUTER JOIN NurCohort C
				ON A.CohortID=C.CohortID
				LEFT OUTER JOIN NurGroup G
				ON A.GroupID=G.GroupID
				WHERE UserType='S' AND UserDeleteData IS NULL
				AND (A.DeletedDate IS NULL)
				AND (@InstitutionId = 0 OR I.InstitutionID = @InstitutionId)
				AND (@CohortId= 0 OR C.CohortID = @CohortId )
				AND (@GroupId = 0 OR G.GroupID = @GroupId  )
				AND (LEN(LTRIM(RTRIM(@SearchString))) = 0
				OR( FirstName like +'%'+ @SearchString + '%' OR LastName like +'%'+ @SearchString +'%'
				OR UserName like +'%' + @SearchString + '%'))
			END
	ELSE
			BEGIN
				SELECT
					S.UserID,
					S.FirstName,
					S.LastName,
					S.UserName,
					S.UserPass,
					S.Telephone,
					S.Email,
					S.UserType,
					S.InstitutionID,
					S.CountryCode,
					S.UserCreateDate,
					S.UserExpireDate,
					S.UserStartDate,
					S.UserUpdateDate,
					S.UserDeleteData,
					S.Integreted,
					S.KaplanUserID,
					S.EnrollmentID,
					S.ADA,
					S.SessionID ,
					A.CohortID,
					A.GroupID,
					I.InstitutionName,
					C.CohortName,
					G.GroupName
					from NurStudentInfo S
				LEFT OUTER JOIN NusStudentAssign A
				ON S.UserID=A.StudentID
				LEFT JOIN NurInstitution I
				ON S.InstitutionID=I.InstitutionID
				LEFT OUTER JOIN NurCohort C
				ON A.CohortID=C.CohortID
				LEFT OUTER JOIN NurGroup G
				ON A.GroupID=G.GroupID
				WHERE UserType='S' AND UserDeleteData IS NULL
				AND (A.DeletedDate IS NULL)
				AND S.Integreted=1
				AND (S.InstitutionID=0
				OR A.CohortID =0)
				AND (FirstName like +'%'+ @SearchString + '%'
				OR LastName like +'%'+ @SearchString +'%'
				OR UserName like +'%' + @SearchString + '%')

			END
END
GO

/****** Object:  StoredProcedure [dbo].USPGetStudents    Script Date: 05/08/2011 23:12:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudents]
GO
/****** Object:  StoredProcedure [dbo].[uspGetStudents]    Script Date: 05/08/2011  ******/
CREATE PROCEDURE [dbo].[uspGetStudents]
@studentId int
AS

BEGIN
SET NOCOUNT ON;
	SELECT
		S.UserID,
		S.FirstName,
		S.LastName ,
		S.UserName ,
		S.UserPass ,
		S.Telephone ,
		S.Email ,
		S.UserType  ,
		S.InstitutionID ,
		S.CountryCode ,
		S.UserCreateDate ,
		S.UserExpireDate ,
		S.UserStartDate  ,
		S.UserUpdateDate ,
		S.UserDeleteData ,
		S.Integreted  ,
		S.KaplanUserID ,
		S.EnrollmentID ,
		S.ADA ,
		S.SessionID ,
		S.AddressID,
		S.EmergencyPhone,
		S.ContactPerson,
		S.Telephone,
		A.CohortID,
		A.GroupID,
		A.Access,
		A.DeletedDate,
		A.DeletedAdmin
	FROM  dbo.NurStudentInfo S
	LEFT JOIN dbo.NusStudentAssign  A
	ON S.UserID = A.StudentID
	WHERE UserID=@studentId
SET NOCOUNT OFF
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDatesForCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetDatesForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetDatesForCohort]    Script Date: 05/08/2011  ******/
CREATE PROCEDURE [dbo].[uspGetDatesForCohort]
	@cohortId int
AS
SET NOCOUNT ON
BEGIN
	SELECT  CohortStartDate ,
			CohortEndDate
	FROM    dbo.NurCohort
	WHERE CohortID = @cohortId
END
GO

/****** Object:  StoredProcedure [dbo].[uspSaveUser]    Script Date: 05/08/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveUser]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveUser]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspSaveUser]
	@userId int,
    @username varchar(255),
    @userpass varchar(255),
    @email varchar(255),
    @institutionId int,
	@integrated varchar(255),
    @kaplanUserId varchar(255),
    @enrollmentId varchar(255),
	@expireDate datetime,
    @startDate datetime,
    @firstname varchar(255),
    @lastname varchar(255),
    @ada bit,
	@cohortId int,
    @groupId int ,
    @addressId int,
    @emergencyPhone varchar(255),
    @contactPerson varchar(255),
    @telephone varchar(255),
    @NewUserId int OUT
AS

SET NOCOUNT ON;
BEGIN TRANSACTION
DECLARE @studentAssignCount INT
DECLARE @userCount INT
SELECT @userCount = 0
-- Insert or update user record
IF (@userId = 0)
	BEGIN
		SELECT @userCount = UserID
		FROM dbo.NurStudentInfo
		WHERE  UserName= @userName
		AND UserPass = @userPass
		-- INSERT new student base record
		IF @userCount = 0
			BEGIN
				INSERT INTO NurStudentInfo
					(UserName,
					 FirstName,
					 LastName,
					 UserPass,
					 Email,
					 InstitutionID,
					 Integreted,
					 UserCreateDate,
					 UserType,
					 UserStartDate,
					 UserExpireDate,
					 KaplanUserID,
					 EnrollmentID,
					 ADA,
					 AddressID,
					 EmergencyPhone,
					 ContactPerson,
					 Telephone)
				VALUES
					(@username,
					 @firstname,
					 @lastname,
					 @userpass,
					 @email,
					 @institutionId,
					 @integrated,
					 GetDate(),
					 'S',
					 @startDate,
					 @expireDate,
					 @kaplanUserId,
					 @enrollmentId,
					 @ada,
					 @addressId,
					 @emergencyPhone,
					 @contactPerson,
					 @telephone)
				SELECT @NewUserId = SCOPE_IDENTITY()
				INSERT INTO NusStudentAssign
				(
				 StudentID,
				 CohortID,
				 GroupID,
				 Access
				 )
				VALUES
				(
				 @NewUserId,
				 @cohortId,
				 @groupId,
				 1
				)				
			END
		ELSE
			BEGIN
				SELECT @NewUserId = -1
			END		
		IF @@ERROR > 0
			ROLLBACK TRANSACTION
		ELSE
			COMMIT TRANSACTION
		RETURN @NewUserId
	END
ELSE
BEGIN
	-- UPDATE existing student base record
	UPDATE NurStudentInfo
	SET UserPass = @userpass,
		UserName = @username,
		Email = @email,
		InstitutionId = @institutionId,
		EnrollmentId = @enrollmentId,
		ADA = @ada,
		Integreted = @integrated,
		FirstName = @firstname,
		LastName = @lastname,
		UserUpdateDate = GetDate(),
		UserStartDate = @startDate,
		KaplanUserID = @kaplanUserId,
		UserExpireDate = @expireDate,
		AddressID=@addressId,
		EmergencyPhone=@emergencyPhone,
		ContactPerson=@contactPerson,
		Telephone=@telephone
	WHERE UserID = @userId
SELECT @NewUserId = @userId
	-- INSERT / UPDATE student assignment record
SET @studentAssignCount = (
			SELECT COUNT(*) FROM NusStudentAssign
			WHERE StudentID = @userId
						  )
	IF (@studentAssignCount IS NOT NULL AND @studentAssignCount > 0)
		BEGIN
			UPDATE NusStudentAssign
			SET CohortID = @cohortId,
				GroupID = @groupId,
				Access = 1
			WHERE StudentID = @userId;
		END
	ELSE
		BEGIN
			INSERT INTO NusStudentAssign
						(
						StudentID,
						CohortID,
						GroupID,
						Access
						)
			VALUES
						(
						 SCOPE_IDENTITY(),
						 @cohortId,
						 @groupId,
						 1
						)
		END
		IF @@ERROR > 0
			ROLLBACK TRANSACTION
		ELSE
			COMMIT TRANSACTION
		RETURN @NewUserId
		RETURN @NewUserId
	END

GO

/****** Object:  StoredProcedure [dbo].[uspDeleteUser]    Script Date: 05/08/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteUser]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveUser]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspDeleteUser]
	@userId int
AS

-- Prevent row count message
SET NOCOUNT ON;

BEGIN TRANSACTION
	-- Update student info table
	UPDATE NurStudentInfo
		SET UserDeleteData = GetDate()
	WHERE UserID = @userId;

IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR('Error in updating UserDeleteData in NurStudentInfo', 16, 1)
		RETURN
	END

	-- Update assignment table
	UPDATE NusStudentAssign
		SET DeletedDate = GetDate()
	WHERE StudentID = @userId;

IF @@ERROR <> 0
	BEGIN
		ROLLBACK
		RAISERROR('Error in updating DeletedDate in NurStudentInfo', 16, 1)
		RETURN
	END

COMMIT TRANSACTION
GO

/****** Object:  StoredProcedure [dbo].[uspCheckUserExists]    Script Date: 05/08/2011
NEED to combine logic in USPSaveUser
******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckUserExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckUserExists]
GO

/****** Object:  StoredProcedure [dbo].[uspCheckUserExists]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspCheckUserExists]
	@userName varchar(160),
	@userPass varchar(100),
	@userId int
AS
	-- Prevent row count message
SET NOCOUNT ON
BEGIN
IF @userId = 0
	BEGIN
		SELECT FirstName
		FROM dbo.NurStudentInfo
		WHERE  UserName= @userName
		AND UserPass = @userPass
	END
ELSE
	BEGIN
		SELECT FirstName
		FROM dbo.NurStudentInfo
		WHERE  UserName= @userName
		AND UserPass = @userPass
		AND NOT  UserID = @userId
	END
END
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentsForCohort]    Script Date: 05/08/2011 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentsForCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudentsForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetStudentsForCohort]    Script Date: 05/08/2011  ******
This query is going to change
**/

CREATE PROCEDURE dbo.uspGetStudentsForCohort
(
@InstitutionId INT,
@CohortId INT,
@GroupId INT
)
 AS
BEGIN
	IF @InstitutionId != -1
		BEGIN
			SELECT  SI.UserID,
			SI.FirstName,
			SI.LastName,
			SI.LastName+','+SI.FirstName as [Name],
			SI.UserName,
			SA.CohortID,
			SA.GroupID,
			SA.DeletedDate,
			C.CohortName,
			C.CohortStatus,
			CP.ProgramID,
			CP.Active,
			P.ProgramName,
			P.DeletedDate AS P_DeletedDate,
			SI.InstitutionID,
			I.InstitutionName,
			I.Status,
			G.GroupName ,
			0 as IsAssigned
			FROM   dbo.NurInstitution I
			INNER JOIN dbo.NurStudentInfo SI
			INNER JOIN dbo.NusStudentAssign SA
			ON SI.UserID = SA.StudentID
			 LEFT OUTER JOIN dbo.NurCohort C
			ON SA.CohortID = C.CohortID
			ON I.InstitutionID = SI.InstitutionID
			AND I.InstitutionID = C.InstitutionID
			LEFT OUTER JOIN dbo.NurGroup G
			ON SA.GroupID = G.GroupID
			AND C.CohortID = G.CohortID
			LEFT OUTER JOIN dbo.NurProgram P
			INNER JOIN dbo.NurCohortPrograms CP
			ON P.ProgramID = CP.ProgramID
			ON C.CohortID = CP.CohortID
			WHERE     (SA.DeletedDate IS NULL)
			AND (C.CohortStatus = 1
			OR C.CohortStatus IS NULL)
			AND (CP.Active IS NULL
			OR CP.Active = 1)
			AND (P.DeletedDate IS NULL)
			AND (I.Status = 1)
			AND (@InstitutionId = 0
				OR I.InstitutionID = @InstitutionId)		
			AND (@CohortId = 0
				OR(SA.CohortID IS NULL
				   OR SA.CohortID=0
				   OR SA.CohortID = @CohortId))
			AND (@GroupId = 0
				OR(SA.GroupId = @GroupId))
		END
	ELSE
		BEGIN
			SELECT
			SI.UserPass ,
			SI.LastName+','+SI.FirstName as [Name],
			SI.Telephone ,
			SI.Email ,
			SI.UserType  ,
			SI.InstitutionID ,
			SI.CountryCode ,
			SI.UserCreateDate ,
			SI.UserExpireDate ,
			SI.UserStartDate  ,
			SI.UserUpdateDate ,
			SI.UserDeleteData ,
			SI.Integreted  ,
			SI.KaplanUserID ,
			SI.EnrollmentID ,
			SI.ADA ,
			SI.SessionID ,
			SA.CohortID,
			SA.GroupID,
			SA.Access,
			SA.DeletedAdmin,
			SI.UserID,			
			SI.FirstName,
			SI.LastName,
			SI.UserName,
			SA.CohortID,
			SA.GroupID,
			SA.DeletedDate,
			C.CohortName,
			C.CohortStatus,
			CP.ProgramID,
			CP.Active,
			P.ProgramName,
			P.DeletedDate AS P_DeletedDate,
			SI.InstitutionID,
			I.InstitutionName,
			I.Status,
			'' AS GroupName,
			(
			 SELECT count(*) FROM dbo.NusStudentAssign
			 WHERE StudentID = SI.UserID
			 AND  GroupID = @GroupId
			 ) as IsAssigned			
			FROM dbo.NurInstitution I
			INNER JOIN dbo.NurStudentInfo SI
			INNER JOIN dbo.NusStudentAssign SA
			ON SI.UserID = SA.StudentID
			INNER JOIN dbo.NurCohort C
			ON SA.CohortID = C.CohortID
			ON I.InstitutionID = SI.InstitutionID
			AND  I.InstitutionID = C.InstitutionID
			LEFT OUTER JOIN dbo.NurProgram P
			INNER JOIN dbo.NurCohortPrograms CP
			ON P.ProgramID = CP.ProgramID
			ON  C.CohortID = CP.CohortID
			WHERE (SA.DeletedDate IS NULL)
			AND (C.CohortStatus = 1)
			AND (CP.Active IS NULL OR CP.Active = 1)
			AND (P.DeletedDate IS NULL)
			AND (I.Status = 1)
			AND C.CohortID = @CohortId
			AND (SA.GroupID IS NULL
				OR SA.GroupID = 0
				OR SA.GroupID = @GroupId)
		END

END
GO

/****** Object:  StoredProcedure [dbo].[uspGetTests]    Script Date: 05/08/2011 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTests]
GO
-- TODO This SP is going to chnage to remove default test id
/****** Object:  StoredProcedure [dbo].[uspGetTests]    Script Date: 05/08/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTests]
(
 @ProductId INT,
 @InstitutionIds VARCHAR(1000),
 @QuestionId INT,
 @ForCMS BIT
 )
AS
SET NOCOUNT ON
IF @ForCMS = 0
	BEGIN
		IF @InstitutionIds = ''
		 BEGIN
		   SELECT
				'' AS ProductName ,
				TestID ,
				TestName AS [Name] ,
				0 AS TestNumber
		   FROM  Tests
		   WHERE (ProductID = @ProductId or @ProductId = 0 )
		   AND ActiveTest=1
		 END
		ELSE
		 BEGIN
		   SELECT
		   DISTINCT T.TestID,
		   '' AS ProductName ,
		   CASE WHEN PP.Type = 1
				THEN PR.ProductName
			ELSE T.TestName 			
		   END AS [Name],
		   0 AS TestNumber
		   FROM  dbo.NurCohort C
		   LEFT JOIN dbo.NurInstitution I
		   ON C.InstitutionID = I.InstitutionID
		   LEFT JOIN dbo.NurCohortPrograms CP
		   ON C.CohortID = CP.CohortID
		   LEFT JOIN dbo.NurProgram P
		   ON CP.ProgramID = P.ProgramID
		   INNER JOIN dbo.NurProgramProduct PP
		   ON CP.ProgramID = PP.ProgramID
		   INNER JOIN dbo.Tests  T
		   ON PP.ProductID = T.TestID
		   LEFT OUTER JOIN dbo.Products PR
		   ON PP.ProductID = PR.ProductID
		   WHERE T.ActiveTest=1
		   AND (@ProductId = 0 OR T.ProductID = @ProductId)
		   AND (@InstitutionIds = ''
		   OR I.InstitutionId IN
		   (SELECT value
		   FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
		 END
	END
ELSE
	BEGIN
			SELECT
				P.ProductName, 				
				T.TestID ,
				T.TestName as [Name],
				(SELECT QuestionNumber FROM TestQuestions
				 WHERE QID = @QuestionId AND TestID = T.TestID ) AS  TestNumber
			FROM  dbo.Products P
			INNER JOIN dbo.Tests T
			ON P.ProductID = T.ProductID
	END

GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortProgram]    Script Date: 05/08/2011 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspGetCohortProgram]
(
	@CohortProgramId int,
	@ProgramId int,
	@CohortId int	
)
AS
BEGIN
	SELECT
		CohortProgramID ,
		CohortID ,
		ProgramID ,
		Active
	FROM dbo.NurCohortPrograms
	WHERE Active = 1
	AND ( @CohortId = 0 OR CohortID = @CohortId)
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetCohortProgram]    Script Date: 05/08/2011 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchCohortProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspSearchCohortProgram]
(
	@CohortId int,
	@SearchText varchar(200)
)
AS
BEGIN
	SELECT
		NP.ProgramID,
		NP.ProgramName,
		NP.Description,
		NP.DeletedDate,
		NCP.CohortID,
		NCP.Active
	FROM    dbo.NurCohortPrograms NCP
	INNER JOIN dbo.NurProgram NP
	ON NCP.ProgramID = NP.ProgramID
	WHERE  NP.DeletedDate IS NULL
	AND    NCP.Active=1
	AND NCP.CohortID= @CohortId
	AND (@SearchText = '' OR NP.ProgramName LIKE '%' + @SearchText + '%')
END
GO

/****** Object:  StoredProcedure [dbo].[uspAssignCohortProgram]    Script Date: 05/08/2011 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignCohortProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignCohortProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignCohortProgram]    Script Date: 05/08/2011  ******/

CREATE PROC [dbo].[uspAssignCohortProgram]
(
	@CohortId int ,
	@ProgramId int,
	@Active int
)
AS
BEGIN

BEGIN TRAN
	UPDATE  NurCohortPrograms
	SET Active = 0
    WHERE CohortID = @CohortId
	IF EXISTS
	(
	SELECT ProgramID
	FROM dbo.NurCohortPrograms
	WHERE ProgramID = @ProgramId
	AND CohortID =  @CohortId
	)
		BEGIN
			UPDATE  NurCohortPrograms
			SET Active = @Active
			WHERE CohortID = @CohortId
			AND ProgramID = @ProgramId
		END
	ELSE
		BEGIN
			INSERT INTO NurCohortPrograms
			(
				CohortID,
				ProgramID,
				Active
			)
			VALUES
			(
				@CohortID ,
				@ProgramId,
				@Active
			)
		END
	
	IF @@ERROR != 0
		BEGIN
			ROLLBACK TRAN
		END
	ELSE
		BEGIN
			COMMIT TRAN
		END
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetResultsFromTheCohortsForChart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetResultsFromTheCohortsForChart]
GO


CREATE PROCEDURE dbo.uspGetResultsFromTheCohortsForChart
@InstitutionId int,
@SubCategoryId INT,
@ChartType int,
@ProductIds nVarchar(1000),
@Tests nVarchar(1000),
@CohortIds nVarchar(1000)
AS
BEGIN

 IF (@ChartType = 1)--ClientNeeds
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClientNeeds.ClientNeedsID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.ClientNeeds
        ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
        WHERE dbo.ClientNeeds.ClientNeedsID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClientNeeds.ClientNeedsID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))

 END
 ELSE IF (@ChartType = 2)--'NursingProcess'
 BEGIN

  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.NursingProcess.NursingProcessID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.NursingProcess ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
        WHERE dbo.NursingProcess.NursingProcessID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.NursingProcess.NursingProcessID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END
 ELSE IF (@ChartType = 3)--'CriticalThinking'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.CriticalThinking.CriticalThinkingID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.CriticalThinking ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
        WHERE dbo.CriticalThinking.CriticalThinkingID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.CriticalThinking.CriticalThinkingID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))

 END
 ELSE IF (@ChartType = 4)--'ClinicalConcept'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClinicalConcept.ClinicalConceptID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.ClinicalConcept ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
        WHERE dbo.ClinicalConcept.ClinicalConceptID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClinicalConcept.ClinicalConceptID ) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))

 END
 ELSE IF (@ChartType = 5)--'Demographic'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.Demographic.DemographicID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.Demographic ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
        WHERE dbo.Demographic.DemographicID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.Demographic.DemographicID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END

 ELSE IF (@ChartType = 6)--'CognitiveLevel'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.CognitiveLevel.CognitiveLevelID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.CognitiveLevel ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
        WHERE dbo.CognitiveLevel.CognitiveLevelID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.CognitiveLevel.CognitiveLevelID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))

 END
 ELSE IF (@ChartType = 7)--'SpecialtyArea'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.SpecialtyArea.SpecialtyAreaID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.SpecialtyArea ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
        WHERE dbo.SpecialtyArea.SpecialtyAreaID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.SpecialtyArea.SpecialtyAreaID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END
 ELSE IF (@ChartType = 8)--'Systems'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.Systems.SystemID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
        WHERE dbo.Systems.SystemID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.Systems.SystemID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END
 ELSE IF (@ChartType = 9)--'LevelOfDifficulty'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.LevelOfDifficulty.LevelOfDifficultyID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID  INNER JOIN dbo.LevelOfDifficulty
        ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
        WHERE TestStatus = 1 AND dbo.LevelOfDifficulty.LevelOfDifficultyID =@SubCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.LevelOfDifficulty.LevelOfDifficultyID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END
 ELSE IF (@ChartType = 10)--'ClientNeedCategory'
 BEGIN
  SELECT   dbo.NurCohort.CohortName, V.N_Correct FROM
        dbo.NurCohort LEFT OUTER JOIN
       (SELECT  cast(SUM(CASE WHEN correct = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) as decimal(4,1)) AS N_Correct,
        dbo.NurCohort.CohortID, dbo.NurCohort.CohortName ,dbo.ClientNeedCategory.ClientNeedCategoryID as Id
        FROM  dbo.UserQuestions INNER JOIN dbo.UserTests
        ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN dbo.Tests
        ON dbo.UserTests.TestID = dbo.Tests.TestID INNER JOIN dbo.Questions
        ON dbo.UserQuestions.QID = dbo.Questions.QID LEFT JOIN dbo.NusStudentAssign
        ON dbo.UserTests.UserID = dbo.NusStudentAssign.StudentID   INNER JOIN dbo.NurCohort
        ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
        INNER JOIN dbo.ClientNeedCategory ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
        WHERE dbo.ClientNeedCategory.ClientNeedCategoryID=@subCategoryId
        AND ( 1=0  OR dbo.Tests.ProductID in( select * from dbo.funcListToTableInt(@ProductIds,'|')))
        AND ( 1=0  OR dbo.Tests.TestID in (select * from dbo.funcListToTableInt(@Tests,'|')))
        GROUP BY dbo.NurCohort.CohortID, dbo.NurCohort.CohortName, dbo.ClientNeedCategory.ClientNeedCategoryID) AS V
        ON dbo.NurCohort.CohortID = V.CohortID
        WHERE NurCohort.InstitutionID=@InstitutionId AND
        ( 1=0  OR dbo.NurCohort.CohortID in (select * from dbo.funcListToTableInt(@CohortIds,'|')))
 END
END

GO

/****** Object:  StoredProcedure [dbo].[uspAssignInstitutionsToAdmin]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignInstitutionsToAdmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignInstitutionsToAdmin]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignInstitutionsToAdmin]    Script Date: 05/17/2011  ******/


CREATE PROCEDURE dbo.uspAssignInstitutionsToAdmin
(
@AdminId INT,
@InstitutionId INT,
@Active INT,
@AssignedInstitutionIds VARCHAR(1000)
)
 AS
BEGIN
	BEGIN TRANSACTION
	UPDATE NurAdminInstitution
	SET Active = 0
	WHERE AdminID = @AdminId
	AND InstitutionID NOT IN (SELECT value
		FROM dbo.funcListToTableInt(@AssignedInstitutionIds,'|'))

	IF EXISTS (
			SELECT 1 FROM NurAdminInstitution
			WHERE AdminID = @AdminId
			AND InstitutionID = @InstitutionId
			  )
		BEGIN
			UPDATE  NurAdminInstitution
			SET Active = @Active			
			WHERE AdminID = @AdminId
			AND InstitutionID = @InstitutionId
		
		END
	ELSE
		BEGIN
			INSERT INTO NurAdminInstitution
			(
				AdminID,
				InstitutionID,
				Active
			)
			VALUES
			(
			@AdminId,
			@InstitutionId,
			@Active
			)
		END
		IF @@ERROR > 0
			ROLLBACK TRANSACTION
		ELSE
			COMMIT TRANSACTION

END

GO

/****** Object:  StoredProcedure [dbo].[uspGetAdmins]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdmins]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAdmins]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAdmins]    Script Date: 05/17/2011  ******/

Create PROCEDURE [dbo].[uspGetAdmins]
 @UserId int,
 @SearchString Varchar(400)
AS
BEGIN
	 SET NOCOUNT ON
	 SELECT UserName,UserPass,Email,FirstName,LastName,SecurityLevel,UploadAccess
	 FROM NurAdmin
	 WHERE (@UserId = 0 OR UserID  = @UserId)
	 AND (@SearchString = ''
	 OR( UserName like +'%'+ @SearchString + '%' ))
END
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteAdmin]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteAdmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteAdmin]
GO

/****** Object:  StoredProcedure [dbo].[[USPDeleteAdmin]]    Script Date: 05/17/2011  ******/
CREATE PROCEDURE [dbo].[uspDeleteAdmin]
	@UserId int
AS
BEGIN
	UPDATE NurAdmin
	SET AdminDeleteData = getdate()
	WHERE UserID = @UserId
END
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdmin]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAdmin]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAdmin]    Script Date: 05/17/2011  ******/

Create PROCEDURE [dbo].[uspSaveAdmin]
  @UserId INT,
  @UserName VARCHAR(50),
  @UserPass VARCHAR(50),
  @Email VARCHAR(50),
  @FirstName VARCHAR(50),
  @LastName VARCHAR(50),
  @SecurityLevel INT,
  @AdminModifyUser  INT,
  @InstitutionId INT,
  @UploadAccess Bit,
  @NewAdminId  INT OUT
AS
 -- Prevent row count message
SET NOCOUNT ON;
DECLARE @Id INT
SELECT @Id = 0
BEGIN
 IF @UserId = 0
  BEGIN
   SELECT @Id = UserID FROM dbo.NurAdmin
   WHERE  UserName = @UserName
   AND @UserPass = @UserPass
   IF @Id = 0
    BEGIN
     INSERT INTO NurAdmin
      (
       UserName,
       UserPass,
       Email,
       FirstName,
       LastName,
       SecurityLevel,
       AdminCreateUser,
       AdminCreateData,
       UploadAccess
      )
     VALUES
      (
       @UserName,
       @UserPass,
       @Email,
       @FirstName,
       @LastName,
       @SecurityLevel,
       @AdminModifyUser,
       GETDATE(),
       @UploadAccess
      )
     SELECT @NewAdminId = SCOPE_IDENTITY()
     IF @InstitutionId > 0
     BEGIN
      INSERT INTO NurAdminInstitution
      (
       AdminID,
       InstitutionID,
       Active
      )
      VALUES
      (
       @NewAdminId ,
       @InstitutionId,
       1
      )
     END
     RETURN  @NewAdminId
    END

   ELSE
    BEGIN
     SELECT @NewAdminId = -1 -- Id already exist
     RETURN @NewAdminId
    END
  END
 ELSE
  SELECT @Id = UserID FROM dbo.NurAdmin
  WHERE  UserName = @UserName
  AND @UserPass = @UserPass
  AND UserName != @UserName
  BEGIN
   IF @Id = 0
    BEGIN
     UPDATE NurAdmin
     SET UserName = @UserName,
     UserPass = @UserPass,
     Email = @Email,
     FirstName = @FirstName,
     LastName = @LastName,
     SecurityLevel = @SecurityLevel,
     AdminUpdateData = GETDATE(),
     AdminUpdateUser = @AdminModifyUser,
     UploadAccess = @UploadAccess
     WHERE UserID = @UserId

     SELECT @NewAdminId = @UserId
     RETURN @NewAdminId
    END
   ELSE
    BEGIN
    SELECT @NewAdminId = -1
    RETURN @NewAdminId
    END
  END
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForStudent]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForStudent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForStudent]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForStudent]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTestsForStudent]
(
@ProgramId INT,
@StudentId INT,
@CohortId INT,
@GroupId INT,
@SearchString varchar(1000)
)
AS
BEGIN
	SELECT
	PP.ProgramProductID,
	PP.ProgramID,
	(
		SELECT StartDate from NurProductDatesStudent
		WHERE StudentID = @StudentId
		AND CohortID = @CohortId
		AND GroupID = @GroupId
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS StartDate ,
	(
		SELECT EndDate from NurProductDatesStudent
		WHERE StudentID = @StudentId
		AND CohortID = @CohortId
		AND GroupID = @GroupId
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS EndDate ,
	PP.ProductID,
	PP.[Type] AS TestType,
	T.ProductID ,
	PP.OrderNo,
	CASE WHEN PP.Type = 1 THEN P.ProductName ELSE T.TestName END AS TestName
	FROM  dbo.NurProgramProduct PP
	INNER JOIN   dbo.Tests T
	ON PP.ProductID = T.TestID
	LEFT OUTER JOIN  dbo.Products P
	ON PP.ProductID = P.ProductID
	WHERE ProgramID= @programId and T.ActiveTest=1
	AND (@SearchString = ''
		OR( T.TestName like +'%'+ @SearchString +'%'))
END
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForCohort]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForCohort]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTestsForCohort]
(
@ProgramId INT,
@CohortId INT,
@SearchString varchar(1000)
)
AS
BEGIN
	SELECT
	PP.ProgramProductID,
	PP.ProgramID,
	(
		SELECT StartDate from NurProductDatesCohort WHERE CohortID = @CohortId
		-- AND ProductID=" + TestID
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS StartDate ,
	(
		SELECT EndDate from NurProductDatesCohort WHERE CohortID = @CohortId
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS EndDate ,
	PP.ProductID,
	PP.Type AS AssignType,
	T.ProductID AS TestType,
	PP.OrderNo,
	CASE WHEN PP.Type = 1 THEN P.ProductName ELSE T.TestName END AS TestName
	FROM  dbo.NurProgramProduct PP
	INNER JOIN   dbo.Tests T
	ON PP.ProductID = T.TestID
	LEFT OUTER JOIN  dbo.Products P
	ON PP.ProductID = P.ProductID
	WHERE ProgramID= @programId and T.ActiveTest=1
	AND (@SearchString = ''
		OR( T.TestName like +'%'+ @SearchString +'%'))
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForGroup]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForGroup]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTestsForGroup]
(
@ProgramId INT,
@CohortId INT,
@GroupId INT,
@SearchString varchar(1000)
)
AS
BEGIN
	SELECT
	PP.ProgramProductID,
	PP.ProgramID,
	(
		SELECT StartDate from NurProductDatesGroup WHERE CohortID = @CohortId AND GroupID = @GroupId
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS StartDate ,
	(
		SELECT EndDate from NurProductDatesGroup WHERE CohortID = @CohortId AND GroupID = @GroupId
		AND ProductID = PP.ProductID
		AND Type = PP.Type
	) AS EndDate ,
	PP.ProductID,
	PP.Type AS TestType,
	T.ProductID ,
	PP.OrderNo,
	CASE WHEN PP.Type = 1 THEN P.ProductName ELSE T.TestName END AS TestName
	FROM  dbo.NurProgramProduct PP
	INNER JOIN   dbo.Tests T
	ON PP.ProductID = T.TestID
	LEFT OUTER JOIN  dbo.Products P
	ON PP.ProductID = P.ProductID
	WHERE ProgramID= @programId and T.ActiveTest=1
	AND (@SearchString = ''
		OR( T.TestName like +'%'+ @SearchString +'%'))
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForProgram]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForProgram]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspGetTestsForProgram]
(
@ProgramId INT,
@SearchString varchar(1000)
)
 AS
BEGIN

	SELECT	PP.ProgramProductID,
			PP.ProgramID,
			PP.ProductID,
			CASE WHEN [Type] = 1 THEN P.ProductName ELSE
			(
				SELECT  ProductName
				FROM    dbo.Tests TP
				INNER JOIN dbo.Products PR
				ON TP.ProductID = PR.ProductID WHERE TestID = PP.ProductID
			)
			END AS TestCategory,
			PP.[Type] AS TestType,
			CASE WHEN [Type] = 1 THEN P.ProductID ELSE T.ProductID END ,
			PP.OrderNo,
			CASE WHEN [Type] = 1 AND ISNULL(P.Bundle, 0) = 1 THEN P.ProductName
			ELSE T.TestName END AS TestName
	FROM  dbo.NurProgramProduct PP
	LEFT JOIN dbo.Tests T
	ON PP.ProductID = T.TestID
	AND T.ActiveTest = 1
	AND [Type] = 0
	LEFT JOIN dbo.Products P
	ON PP.ProductID = P.ProductID
	AND [Type] = 1
	WHERE ProgramID = @ProgramId
	AND (@SearchString = ''
		OR( T.TestName like +'%'+ @SearchString +'%'))

END

GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForProgram]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToStudent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignTestDateToStudent]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToStudent]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignTestDateToStudent]
(
@CohortId INT,
@GroupId INT,
@TestId INT,
@StudentId Int,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS (
	SELECT 1 FROM NurProductDatesStudent
	WHERE StudentID = @StudentId
	AND  GroupID = @GroupId
	AND CohortID = @CohortId
	AND ProductID = @TestID
	AND Type = @Type
)
	BEGIN
		UPDATE  NurProductDatesStudent
		SET StartDate = @StartDate,
		EndDate = @EndDate,
		UpdateDate = getdate()
		WHERE CohortID= @CohortId
		AND ProductID=@TestId
		AND Type=@Type
		AND GroupID=@GroupId
		AND StudentID=@StudentId
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesStudent
		(
			CohortID,
			ProductID,
			StartDate,
			EndDate,
			UpdateDate,
			Type,
			GroupID,
			StudentID
		)
		VALUES
		(
			@CohortID,
			@TestId,
			@StartDate,
			@EndDate,
			getdate(),
			@Type,
			@GroupId,
			@StudentId
		)
	END
END

GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToGroup]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignTestDateToGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToGroup]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignTestDateToGroup]
(
@GroupId INT,
@CohortId INT,
@TestId INT,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS
(
	SELECT 1
	FROM NurProductDatesGroup
	WHERE
	GroupID = @GroupId
	AND CohortID = @CohortId
	AND ProductID = @TestId
	AND Type = @Type
)
	BEGIN

		UPDATE  NurProductDatesGroup
			SET StartDate = @StartDate,
				EndDate=@EndDate,
				UpdateDate = getdate()
				WHERE CohortID = @CohortId
				AND ProductID = @TestId
				AND Type=@Type
				AND GroupID=@GroupId
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesGroup
		(
			CohortID,
			ProductID,
			StartDate,
			EndDate,
			UpdateDate,
			Type,
			GroupID
		)
		VALUES
		(
			 @CohortId,
			 @TestId,
			 @StartDate,
			 @EndDate,
			 getdate(),
			 @Type,
			 @GroupId
		)
	END
END

GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestsToProgram]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestsToProgram]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignTestsToProgram]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestsToProgram]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignTestsToProgram]
(
@ProgramId INT,
@TestId INT,
@Type INT
)
 AS
BEGIN

IF NOT EXISTS (
	SELECT 1 FROM dbo.NurProgramProduct
	WHERE ProgramID = @ProgramId
	AND Type = @Type
	AND ProductID = @TestId
	)
		BEGIN
			INSERT INTO NurProgramProduct
			(
				ProgramID,
				ProductID,
				Type
			)
			VALUES
			(
				@ProgramId ,
				@TestId,
				@Type
			)		
		END
END

GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToCohort]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignTestDateToCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignTestDateToCohort]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignTestDateToCohort]    Script Date: 05/17/2011  ******/
CREATE PROCEDURE dbo.uspAssignTestDateToCohort
(
@CohortId INT,
@ProductId INT,
@Type INT,
@StartDate DATETIME,
@EndDate DATETIME
)
 AS
BEGIN
IF EXISTS (
SELECT 1 FROM NurProductDatesCohort
WHERE CohortID = @CohortID  AND ProductID = @ProductId AND  Type = @Type
)
	BEGIN
		UPDATE  NurProductDatesCohort
		SET StartDate= @StartDate,
			EndDate= @EndDate,
			UpdateDate = GETDATE()
		WHERE CohortID=@CohortId
		AND ProductID=@ProductId
		AND Type=@Type
	
	END
ELSE
	BEGIN
		INSERT INTO NurProductDatesCohort
		(
		CohortID,
		ProductID,
		StartDate,
		EndDate,
		UpdateDate,
		Type
		)
		VALUES
		(
		@CohortID,
		@ProductID,
		@StartDate,
		@EndDate,
		GETDATE(),
		@Type
		)
	END
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspResultsFromTheCohortsBySubCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspResultsFromTheCohortsBySubCategory]
GO

CREATE PROCEDURE [dbo].[uspResultsFromTheCohortsBySubCategory]
 @CohortList varchar(500),
 @SubCategory int,
 @CaseList Varchar(50),
 @ModuleList Varchar(50),
 @InstitutionID int,
 @CategoryID int
AS
BEGIN
Select C.CohortName, cast(SUM(CS.Correct)*100.0/SUM(CS.Total) as decimal(4,1)) As N_Correct
from CaseModuleScore M
 INNER JOIN CaseSubCategory CS ON M.ModuleStudentID = CS.ModuleStudentID
 INNER JOIN NurStudentInfo S ON M.StudentID = S.EnrollmentID
 INNER JOIN NusStudentAssign A ON S.UserID=A.StudentID
 INNER JOIN NurInstitution I ON S.InstitutionID=I.InstitutionID
 INNER JOIN NurCohort C On C.CohortID = A.CohortID
 WHERE UserType='S' AND UserDeleteData is null
 AND (A.DeletedDate IS NULL) AND I.InstitutionID = @InstitutionID
    AND CS.CategoryID = @CategoryID
 AND CS.SubcategoryID = @SubCategory
 AND A.CohortID in (select * from dbo.funcListToTableInt(@CohortList,'|'))
 AND M.CaseID in  (select * from dbo.funcListToTableInt(@CaseList,'|'))
 AND M.ModuleID in (select * from dbo.funcListToTableInt(@ModuleList,'|'))
 Group BY C.CohortName, CS.SubcategoryID
END

Go

/****** Object:  StoredProcedure [dbo].[uspSearchAdmins]    Script Date: 05/18/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAdmins]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchAdmins]
GO

/****** Object:  StoredProcedure [dbo].[uspSearchAdmins]    Script Date: 05/18/2011  ******/
CREATE PROCEDURE [dbo].[uspSearchAdmins]
	@InstitutionIds varchar(max),
	@SecurityLevel Varchar(20),
	@SearchString Varchar(400)
AS
BEGIN
	IF @InstitutionIds = ''
		BEGIN
				SELECT A.UserId,
					   A.FirstName,
					   A.LastName,
					   A.UserName,
					   A.UserPass ,
					   I.InstitutionName,
					   S.AdminType
				FROM dbo.NurAdmin A
				LEFT OUTER JOIN	dbo.NurAdminInstitution AI
				ON A.UserID = AI.AdminID
				LEFT OUTER JOIN dbo.NurInstitution I
				ON AI.InstitutionID = I.InstitutionID
				INNER JOIN dbo.NurAdminSecurity S
				ON A.SecurityLevel = S.SecurityLevel
				WHERE AdminDeleteData is null
				AND (@SecurityLevel = ''
				OR	A.SecurityLevel IN
				(SELECT value
				FROM dbo.funcListToTableInt(@SecurityLevel,'|')))
				AND (@SearchString = ''
				OR( UserName like +'%'+ @SearchString + '%' OR FirstName like + '%'+ @SearchString + '%' OR LastName like + '%'+ @SearchString + '%' ))
		END
	ELSE
		BEGIN
				SELECT A.UserId,
					   A.FirstName,
					   A.LastName,
					   A.UserName,
					   A.UserPass ,
					   I.InstitutionName,
					   S.AdminType
				FROM dbo.NurAdmin A
				LEFT OUTER JOIN	dbo.NurAdminInstitution AI
				ON A.UserID = AI.AdminID
				LEFT OUTER JOIN dbo.NurInstitution I
				ON AI.InstitutionID = I.InstitutionID
				INNER JOIN dbo.NurAdminSecurity S
				ON A.SecurityLevel = S.SecurityLevel
				WHERE Active=1 				
				AND (@SecurityLevel = ''
				OR	A.SecurityLevel IN
				(SELECT value
				FROM dbo.funcListToTableInt(@SecurityLevel,'|')))
				AND (@InstitutionIds = ''
				OR	AI.InstitutionID IN
				(SELECT value
				FROM dbo.funcListToTableInt(@InstitutionIds,'|')))
				AND (@SearchString = ''
				OR( UserName like +'%'+ @SearchString + '%' OR FirstName like + '%'+ @SearchString + '%' OR LastName like + '%'+ @SearchString + '%' ))
				
		END	
END

GO

/****** Object:  StoredProcedure [dbo].[uspAssignStudentToGroup]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignStudentToGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignStudentToGroup]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignStudentToGroup]    Script Date: 05/17/2011  ******/


CREATE PROC [dbo].[uspAssignStudentToGroup]
(
	@GroupId int ,
	@AssignStudentList VARCHAR(4000) ,
	@UnassignedStudentList VARCHAR(4000)
)
AS
BEGIN
	IF LEN(@AssignStudentList) > 0
		BEGIN
			UPDATE  NusStudentAssign SET GroupID = @GroupId
			WHERE  StudentID IN (SELECT value
			FROM dbo.funcListToTableInt(@AssignStudentList,'|'))
		END

	IF LEN(@UnassignedStudentList) > 0
		BEGIN
			UPDATE  NusStudentAssign SET GroupID=0 WHERE  StudentID IN (SELECT value
			FROM dbo.funcListToTableInt(@UnassignedStudentList,'|'))
		END
END

GO



/****** Object:  StoredProcedure [dbo].[uspAssignStudents]    Script Date: 05/17/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignStudents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignStudents]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignStudents]    Script Date: 05/17/2011  ******/

CREATE PROCEDURE [dbo].[uspAssignStudents]
	@userId VARCHAR(max),
	@cohortId int,
	@groupId int,
	@institutionId int
AS

-- Prevent row count message
SET NOCOUNT ON;

BEGIN TRANSACTION
	-- Update student info table
	UPDATE NurStudentInfo
		SET InstitutionID = @institutionId
	WHERE UserID IN
		  (SELECT value
		  FROM dbo.funcListToTableInt(@userId,'|'))
	-- Update assignment table
	UPDATE NusStudentAssign
		SET CohortID = @cohortId,
		GroupID = @groupId,
		Access = 1
	WHERE StudentID IN
		  (SELECT value
		  FROM dbo.funcListToTableInt(@userId,'|'))

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK
		END
COMMIT TRANSACTION


GO



IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetCustomTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCustomTests]
GO

Create PROCEDURE [dbo].[uspGetCustomTests]
(
 @TestId int,
 @ProductId int,
 @TestName varchar(50)
)
AS
 BEGIN
   SELECT TestID ,TestName AS [Name],ProductId , DefaultGroup
   FROM  Tests
   WHERE (TestID =@TestId OR @TestId=0) AND (ProductID= @ProductId OR @ProductId =0)
   AND (TestName=@TestName OR @TestName='') AND (ActiveTest=1)
 END

 GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveCustomTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveCustomTest]

GO

CREATE PROCEDURE [dbo].[uspSaveCustomTest]
 @TestId as int OUTPUT,
 @Name varchar(50),
 @ProductId int,
 @DefaultGroup int
AS
BEGIN
    IF @DefaultGroup < 1 SET @DefaultGroup = NULL

 If(@TestId = 0)
  Begin
    INSERT INTO Tests (TestName, ProductID, DefaultGroup, ReleaseStatus, ActiveTest, TestSubGroup)
       VALUES (@Name, @ProductId, @DefaultGroup, 'E', 1, 1)
       set @TestId = CONVERT(int, SCOPE_IDENTITY())

     End
    Else
     Begin
       UPDATE Tests SET ProductID = @ProductId, TestName = @Name, DefaultGroup = @DefaultGroup, ReleaseStatus = 'E'
       WHERE TestID = @TestId
     End
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSearchCustomTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSearchCustomTests]
GO

CREATE PROC [dbo].[uspSearchCustomTests]
(
 @TestName varchar(50)
)
AS
Begin
	Select TestID,TestName,ProductID,DefaultGroup from dbo.Tests
	where TestName like '%'+@TestName+'%'  and (ActiveTest=1)
End

Go

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPCopyCustomTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspCopyCustomTest]

GO

CREATE PROCEDURE [dbo].[uspCopyCustomTest]
 @OriginalTestID int,
 @NewTestID int
AS

BEGIN
 INSERT INTO TestQuestions (TestID, QuestionID, QID, QuestionNumber, Q_Norming)
  SELECT @NewTestID, QuestionID, QID, QuestionNumber, Q_Norming FROM TestQuestions
  WHERE TestID=@OriginalTestID
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteCustomTest' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteCustomTest]
GO

CREATE PROCEDURE [dbo].[uspDeleteCustomTest]
 @TestId int
AS
BEGIN
     UPDATE Tests SET ActiveTest = 0
      WHERE TestID = @TestId
END

GO

/****** Object:  StoredProcedure [dbo].[uspGetAnswerChoices]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAnswerChoices]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAnswerChoices]
GO

/****** Object:  StoredProcedure [dbo].[uspGetAnswerChoices]    Script Date: 05/23/2011  ******/

CREATE PROCEDURE [dbo].[uspGetAnswerChoices]
(
 @QuestionId int,
 @ActionType int,
 @QIds Varchar(4000)
)
AS
 BEGIN
 SET NOCOUNT ON
 SELECT
  QID ,
  AnswerID ,
  AIndex ,
  AText ,
  Correct ,
  AnswerConnectID ,
  AType ,
  initialPos ,
  Unit ,
  AlternateAText
 FROM AnswerChoices
 WHERE (QID = @QuestionId or @QuestionId = 0)  AND (AType=@ActionType or @ActionType = 0) And ((QID in (select * from dbo.funcListToTableInt(@QIds,'|'))) or  @QIds='')
 SET NOCOUNT OFF
END

GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPSaveTestCategory' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveTestCategory]

GO

CREATE PROCEDURE [dbo].[uspSaveTestCategory]
 @TestId int,
 @CategoryId as int,
 @Student as int,
 @Admin as int
AS
BEGIN
Declare @count as int
select @count = count(*) from dbo.TestCategory
where TestID=@TestId and CategoryID=@CategoryId

 If (@count>0)
  Begin
   UPDATE TestCategory SET Student=@Student,[Admin] =@Admin
   WHERE TestID=@TestId And CategoryID=@CategoryId
  End
 Else
  Begin
   INSERT INTO TestCategory (TestID,CategoryID,Student,[Admin])
   values(@TestId,@CategoryId,@Student,@Admin)
  End
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetTestCategories' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestCategories]

GO

CREATE PROCEDURE [dbo].[uspGetTestCategories]
 @TestId int,
 @TestIds Varchar(4000)
AS
  BEGIN
    Select id,TestID,CategoryID,Student,[Admin] from TestCategory
    where (TestID = @TestId  or @TestId =0) and
    ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')
  End

Go

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetTestCategoriesForTestQuestion' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestCategoriesForTestQuestion]

GO

CREATE PROCEDURE dbo.uspGetTestCategoriesForTestQuestion
	@TestType int,
	@TestId int
AS
BEGIN

	 IF (@TestType = 1)--ClientNeeds
	 BEGIN
		  Select Distinct dbo.ClientNeeds.ClientNeeds as [Description], dbo.ClientNeeds.ClientNeedsID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.ClientNeeds
		  ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClientNeeds.ClientNeedsID
	 END
	 ELSE IF (@TestType = 2)--'NursingProcess'
	 BEGIN
		  Select Distinct dbo.NursingProcess.NursingProcess as [Description],dbo.NursingProcess.NursingProcessID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.NursingProcess
		  ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 3)--'CriticalThinking'
	 BEGIN
		  Select Distinct dbo.CriticalThinking.CriticalThinking as [Description],dbo.CriticalThinking.CriticalThinkingID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.CriticalThinking
		  ON dbo.Questions.CriticalThinkingID = dbo.CriticalThinking.CriticalThinkingID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.CriticalThinking.CriticalThinkingID
	 END
	 ELSE IF (@TestType = 4)--'ClinicalConcept'
	 BEGIN
		  Select Distinct dbo.ClinicalConcept.ClinicalConcept as [Description], ClinicalConcept.ClinicalConceptID as Id,ClinicalConcept.OrderNumber
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.ClinicalConcept
		  ON dbo.Questions.ClinicalConceptsID = dbo.ClinicalConcept.ClinicalConceptID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClinicalConcept.OrderNumber
	 END
	 ELSE IF (@TestType = 5)--'Demographic'
	 BEGIN
		  Select Distinct dbo.Demographic.Demographic as [Description],Demographic.DemographicID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.Demographic
		  ON dbo.Questions.DemographicID = dbo.Demographic.DemographicID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END

	 ELSE IF (@TestType = 6)--'CognitiveLevel'
	 BEGIN
		 Select Distinct dbo.CognitiveLevel.CognitiveLevel as [Description],dbo.CognitiveLevel.CognitiveLevelID  as Id
		 FROM dbo.TestQuestions INNER JOIN dbo.Questions
		 ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.CognitiveLevel
		 ON dbo.Questions.CognitiveLevelID = dbo.CognitiveLevel.CognitiveLevelID
		 WHERE  dbo.TestQuestions.TestID=@TestId
		 ORDER BY dbo.CognitiveLevel.CognitiveLevelID
	 END
	 ELSE IF (@TestType = 7)--'SpecialtyArea'
	 BEGIN
		 Select Distinct dbo.SpecialtyArea.SpecialtyArea as [Description],dbo.SpecialtyArea.SpecialtyAreaID as Id
		 FROM dbo.TestQuestions INNER JOIN dbo.Questions
		 ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.SpecialtyArea
		 ON dbo.Questions.SpecialtyAreaID = dbo.SpecialtyArea.SpecialtyAreaID
		 WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 8)--'Systems'
	 BEGIN
		  Select Distinct dbo.Systems.System as [Description],Systems.SystemID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID   LEFT OUTER JOIN dbo.Systems
		  ON dbo.Questions.SystemID = dbo.Systems.SystemID
		  WHERE  dbo.TestQuestions.TestID=@TestId
	 END
	 ELSE IF (@TestType = 9)--'LevelOfDifficulty'
	 BEGIN
		  Select Distinct dbo.LevelOfDifficulty.LevelOfDifficulty as [Description], dbo.LevelOfDifficulty.LevelOfDifficultyID as Id  ,LevelOfDifficulty.OrderNumber
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID LEFT OUTER JOIN dbo.LevelOfDifficulty
		  ON dbo.Questions.LevelOfDifficultyID = dbo.LevelOfDifficulty.LevelOfDifficultyID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.LevelOfDifficulty.OrderNumber
	 END
	 ELSE IF (@TestType = 10)--'ClientNeedCategory'
	 BEGIN
		  Select Distinct ClientNeedCategory.ClientNeedCategory as [Description],dbo.ClientNeedCategory.ClientNeedCategoryID as Id
		  FROM dbo.TestQuestions INNER JOIN dbo.Questions
		  ON dbo.TestQuestions.QID = dbo.Questions.QID  LEFT OUTER JOIN dbo.ClientNeedCategory
		  ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
		  WHERE  dbo.TestQuestions.TestID=@TestId
		  ORDER BY dbo.ClientNeedCategory.ClientNeedCategoryID
	 END
END

GO


/****** Object:  StoredProcedure [dbo].[uspSaveQuestion]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveQuestion]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspSaveQuestion]
	@QID INT ,
	@QuestionId VARCHAR(50),
	@QuestionType CHAR (2),
	@ClientNeedsId VARCHAR(500),
	@ClientNeedsCategoryId VARCHAR(500),
	@NursingProcessId VARCHAR(500),
	@LevelOfDifficultyId VARCHAR(500),
	@DemographicId VARCHAR(500),
	@CognitiveLevelId VARCHAR(500),
	@CriticalThinkingId VARCHAR(500),
	@IntegratedConceptsId VARCHAR(500),
	@ClinicalConceptsId VARCHAR(500),
	@Stimulus VARCHAR(500),
	@Stem VARCHAR(5000),
	@Explanation VARCHAR(5000),
	@RemediationId VARCHAR(5000),
	@SpecialtyAreaId VARCHAR(500),
	@SystemId VARCHAR(500),
	@ReadingCategoryId VARCHAR(500),
	@ReadingId VARCHAR(500),
	@WritingCategoryId VARCHAR(500),
	@WritingId VARCHAR(500),
	@MathCategoryId VARCHAR(500),
	@MathId VARCHAR(500),
	@ProductLineId VARCHAR(500),
	@TypeOfFileId VARCHAR(500),
	@Statisctics VARCHAR(500),
	@CreatorId VARCHAR(500),
	@DateCreated VARCHAR (50),
	@EditorId VARCHAR(500),
	@DateEdited VARCHAR (500),
	@EditorId_2 VARCHAR(500),
	@DateEdited_2 VARCHAR (500),
	@Source_SBD VARCHAR (500),
	@WhoOwns VARCHAR(500),
	@Feedback VARCHAR (500),
	@Active INT ,
	@PointBiserialsId VARCHAR(500),
	@ItemTitle VARCHAR (500),
	@ExhibitTab1 VARCHAR (5000),
	@ExhibitTab2 VARCHAR (5000),
	@ExhibitTab3 VARCHAR (5000),
	@Norming FLOAT(53), -- as per db
	@ExhibitTitle1 VARCHAR (1000),
	@ExhibitTitle2 VARCHAR (1000),
	@ExhibitTitle3 VARCHAR (1000),
	@ListeningFileUrl VARCHAR (1000),
	@AlternateStem VARCHAR(5000),
	@NewQuestionId INT OUT
AS
SET NOCOUNT ON
BEGIN
	SELECT  @NewQuestionId = 0
	DECLARE @QIDCount INT
	SET @QIDCount = 0
	SELECT  @NewQuestionId = 0
	  IF @QID = 0
	  BEGIN
		SELECT  @QIDCount = count(QuestionID) from Questions where QuestionID=@QuestionId
	  END
	  ELSE
	  BEGIN
		SELECT  @QIDCount = count(QuestionID) from Questions where QuestionID=@QuestionId and QID<>@QID
	  END
	 IF @QIDCount > 0
	 BEGIN
		Select @NewQuestionId = - 1
		RETURN @NewQuestionId
	 END
	IF @QID = 0
		BEGIN
			INSERT INTO Questions
			(
			QuestionID,
			QuestionType,
			ClientNeedsID,
			ClientNeedsCategoryID,
			NursingProcessID,
			LevelOfDifficultyID,
			DemographicID,
			CognitiveLevelID,
			CriticalThinkingID,
			IntegratedConceptsID,
			ClinicalConceptsID,
			Stimulus,
			Stem,
			Explanation,
			RemediationID,
			SpecialtyAreaID,
			SystemID,
			ReadingCategoryID,
			ReadingID,
			WritingCategoryID,
			WritingID,
			MathCategoryID,
			MathID,
			ProductLineID,
			TypeOfFileID,
			Statisctics,
			CreatorID,
			DateCreated,
			EditorID,
			DateEdited,
			EditorID_2,
			DateEdited_2,
			Source_SBD,
			WhoOwns,
			PointBiserialsID,
			Feedback,
			Active,
			ItemTitle,
			Q_Norming,
			ReleaseStatus,
			ExhibitTab1,
			ExhibitTab2,
			ExhibitTab3,
			ExhibitTitle1,
			ExhibitTitle2,
			ExhibitTitle3,
			ListeningFileUrl,
			AlternateStem
			)
			VALUES
			(
			@QuestionId,
			@QuestionType,
			@ClientNeedsId,
			@ClientNeedsCategoryId,
			@NursingProcessId,
			@LevelOfDifficultyId,
			@DemographicId,
			@CognitiveLevelId,
			@CriticalThinkingId,
			@IntegratedConceptsId,
			@ClinicalConceptsId,
			@Stimulus,
			@Stem,
			@Explanation,
			@RemediationId,
			@SpecialtyAreaId,
			@SystemId,
			@ReadingCategoryId,
			@ReadingId,
			@WritingCategoryId,
			@WritingId,
			@MathCategoryId,
			@MathId,
			@ProductLineId,
			@TypeOfFileId,
			@Statisctics,
			@CreatorId,
			@DateCreated,
			@EditorId,
			@DateEdited,
			@EditorID_2,
			@DateEdited_2,
			@Source_SBD,
			@WhoOwns,
			@PointBiserialsId,
			@Feedback,
			@active,
			@ItemTitle,
			@Norming,
			'E',
			@ExhibitTab1,
			@ExhibitTab2,
			@ExhibitTab3,
			@ExhibitTitle1,
			@ExhibitTitle2,
			@ExhibitTitle3,
			@ListeningFileUrl,
			@AlternateStem
			)
			SELECT @NewQuestionId = SCOPE_IDENTITY()
			RETURN  @NewQuestionId
		END	
	ELSE
		BEGIN
			UPDATE  Questions
			SET QuestionID=@QuestionId,
				QuestionType=@QuestionType,
				ClientNeedsID=@ClientNeedsId,
				ClientNeedsCategoryID=@ClientNeedsCategoryId,
				NursingProcessID=@NursingProcessId,
				LevelOfDifficultyID=@LevelOfDifficultyId,
				DemographicID=@DemographicId,
				CognitiveLevelID=@CognitiveLevelId,
				CriticalThinkingID=@CriticalThinkingId,
				IntegratedConceptsID=@IntegratedConceptsId,
				ClinicalConceptsID=@ClinicalConceptsId,
				Stimulus=@Stimulus,
				Stem=@Stem,
				Explanation=@Explanation,
				RemediationID=@RemediationId,
				SpecialtyAreaID=@SpecialtyAreaId,
				SystemID=@SystemId,
				ReadingCategoryID=@ReadingCategoryId,
				ReadingID=@ReadingId,
				WritingCategoryID=@WritingCategoryId,
				WritingID=@WritingId,
				MathCategoryID=@MathCategoryId,
				MathID=@MathId,
				ProductLineID=@ProductLineId,
				TypeOfFileID=@TypeOfFileId,
				Statisctics=@Statisctics,
				CreatorID=@CreatorId,
				DateCreated=@DateCreated,
				EditorID=@EditorId,
				DateEdited=@DateEdited,
				EditorID_2=@EditorID_2,
				DateEdited_2=@DateEdited_2,
				Source_SBD=@Source_SBD,
				WhoOwns=@WhoOwns,
				PointBiserialsID=@PointBiserialsId,
				Feedback=@Feedback,Active=@active,
				ItemTitle=@ItemTitle,Q_Norming=@Norming, ReleaseStatus='E',
				AlternateStem=@AlternateStem,
				ExhibitTitle1=@ExhibitTitle1,
				ExhibitTitle2=@ExhibitTitle2,
				ExhibitTitle3=@ExhibitTitle3,
				ListeningFileUrl=@ListeningFileUrl,
				ExhibitTab1=@ExhibitTab1,
				ExhibitTab2=@ExhibitTab2,
				ExhibitTab3=@ExhibitTab3
			WHERE QID=@QID
			SELECT @NewQuestionId = @QID
			RETURN  @NewQuestionId
		END
END
SET NOCOUNT OFF

GO

/****** Object:  StoredProcedure [dbo].[uspSaveQuestion]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAnswer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAnswer]
GO

/****** Object:  StoredProcedure [dbo].[uspSaveAnswer]    Script Date: 05/23/2011  ******/

CREATE PROCEDURE [dbo].[uspSaveAnswer]
@QID INT ,
@AIndex CHAR (1),
@AText VARCHAR (3000),
@Correct INT ,
@AnswerConnectId INT ,
@ActionType  INT ,
@InitialPosition INT ,
@Unit VARCHAR (50) ,
@AlternateAText VARCHAR (3000)

AS
 BEGIN
 SET NOCOUNT ON
  INSERT INTO AnswerChoices
  (
  QID,
  AIndex,
  AText,
  Correct,
  AnswerConnectID,
  AType,
  InitialPos,
  Unit,
  AlternateAtext
  )
  VALUES
  (
  @QID,
  @AIndex,
  @AText,
  @Correct,
  @AnswerConnectId,
  @ActionType,
  @InitialPosition,
  @Unit,
  @AlternateAtext
  )
 SET NOCOUNT OFF
 END

GO

/****** Object:  StoredProcedure [dbo].[uspDeleteAnswer]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteAnswer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteAnswer]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteAnswer]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspDeleteAnswer]
@QID INT
AS
BEGIN
	SET NOCOUNT ON
		DELETE FROM AnswerChoices WHERE QID=@QID
	SET NOCOUNT OFF
END
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteQuestion]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteQuestion]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspDeleteQuestion]
@QuestionId INT
AS
	BEGIN
	SET NOCOUNT ON
		UPDATE Questions
		SET ACTIVE=0
		WHERE QID=@QuestionId
	SET NOCOUNT OFF
	END
GO

/****** Object:  StoredProcedure [dbo].[uspAssignQuestion]    Script Date: 05/23/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspAssignQuestion]    Script Date: 05/23/2011  ******/
CREATE PROCEDURE [dbo].[uspAssignQuestion]
@TestId INT,
@QuestionId INT ,
@QuestionNumber INT ,
@Active INT
AS
	BEGIN
	SET NOCOUNT ON
		DECLARE @v_TestCount INT
		SELECT @v_TestCount = 0
		SELECT @v_TestCount = Count(TestId)
		FROM TestQuestions
		WHERE QID = @QuestionId
		AND TestID = @TestId		
			IF(@Active = 1)				
				BEGIN
					
					IF @v_TestCount = 0
						BEGIN					
							INSERT INTO TestQuestions
							(
								TestID,
								QID,
								QuestionNumber
							 )
							Values
							(
								@TestId,
								@QuestionId,
								@QuestionNumber
							)
						END
					ELSE
						BEGIN
							
							UPDATE  TestQuestions
							SET QuestionNumber = @QuestionNumber
							WHERE TestID = @TestId
							AND QID = @QuestionId
						END
					END
				ELSE
					BEGIN
						IF @v_TestCount > 0
						BEGIN	
								DELETE FROM  TestQuestions
								WHERE TestID = @TestId
								AND QID = @QuestionId
						END
					END					
	SET NOCOUNT OFF
	END
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForQuestion]    Script Date: 05/27/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestsForQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestsForQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspGetTestsForQuestion]    Script Date: 05/27/2011  ******/


CREATE PROCEDURE [dbo].[uspGetTestsForQuestion]
(
 @QuestionId INT
 )
AS
BEGIN
SET NOCOUNT ON
	SELECT
		P.ProductName,
		T.TestName,
		Q.QuestionID,
		Q.QID
	FROM  dbo.Tests T
	INNER JOIN dbo.TestQuestions Q
	ON T.TestID = Q.TestID
	INNER JOIN dbo.Products P
	ON T.ProductID = P.ProductID
	WHERE QID=@QuestionId
END

GO

/****** Object:  StoredProcedure [dbo].[uspReturnNextQuestion]    Script Date: 05/29/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnNextQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnNextQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnNextQuestion]    Script Date: 05/29/2011  ******/

CREATE PROCEDURE [dbo].[uspReturnNextQuestion]	
	@UserTestId int, 	
	@QuestionNumber int,
	@TypeOfFileId varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF @TypeOfFileId = '01' OR @TypeOfFileId ='02' OR @TypeOfFileId ='03' OR @TypeOfFileId ='04' OR @TypeOfFileId ='05'
		BEGIN

			SELECT TOP 1
				UQ.QuestionNumber,
				UQ.TimeSpendForRemedation,
				UQ.TimeSpendForExplanation,
				P.ProductName,
				T.TestName,
				U.TestID,
				Q.Stem,
				Q.ListeningFileUrl,
				Q.Explanation,
				Q.RemediationID,
				Q.QuestionType,
				Q.TypeOfFileID,
				Q.TopicTitleID,
				Q.QID,
				Q.QuestionID,
				Q.ItemTitle
			FROM  dbo.Products P
			INNER JOIN  dbo.Tests T
			ON P.ProductID = T.ProductID
			INNER JOIN dbo.UserTests U
			ON T.TestID = U.TestID
			INNER JOIN dbo.UserQuestions UQ
			ON UQ.UserTestID = U.UserTestID
			INNER JOIN dbo.Questions Q
			ON Q.QID = UQ.QID
			WHERE U.UserTestID = @UserTestId
			AND UQ.QuestionNumber>@QuestionNumber
			AND TypeOfFileID = @TypeOfFileId
			AND (Q.Active IS NULL OR Q.Active = 1)
		END

	ELSE
		BEGIN
			SELECT TOP 1
				UQ.QuestionNumber,
				UQ.TimeSpendForRemedation,
				UQ.TimeSpendForExplanation,
				P.ProductName,
				T.TestName,
				U.TestID,
				Q.Stem,
				Q.ListeningFileUrl,
				Q.Explanation,
				Q.RemediationID,
				Q.QuestionType,
				Q.TypeOfFileID,
				Q.TopicTitleID,
				Q.QID,
				Q.QuestionID,
				Q.ItemTitle
			FROM  dbo.Products P
			INNER JOIN  dbo.Tests T
			ON P.ProductID = T.ProductID
			INNER JOIN dbo.UserTests U
			ON T.TestID = U.TestID
			INNER JOIN dbo.UserQuestions UQ
			ON UQ.UserTestID = U.UserTestID
			INNER JOIN dbo.Questions Q
			ON Q.QID = UQ.QID
			WHERE U.UserTestID = @UserTestId
			AND UQ.QuestionNumber > @QuestionNumber 			
		END
		SET NOCOUNT OFF
	END

GO


/****** Object:  StoredProcedure [dbo].[uspReturnPreviousQuestion]    Script Date: 05/29/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnPreviousQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnPreviousQuestion]
GO

/****** Object:  StoredProcedure [dbo].[uspReturnPreviousQuestion]    Script Date: 05/29/2011  ******/

CREATE PROCEDURE [dbo].[uspReturnPreviousQuestion]	
	@UserTestId int, 	
	@QuestionNumber int,
	@TypeOfFileId varchar(500)
AS
BEGIN

	SELECT
		UQ.QuestionNumber,
		UQ.TimeSpendForRemedation,
		UQ.TimeSpendForExplanation,
		P.ProductName,
		T.TestName,
		UT.TestID,
		Q.Stem,
		Q.Explanation,
		Q.RemediationID,
		Q.QuestionType,
		Q.TypeOfFileID,
		Q.TopicTitleID,
		Q.QID,
		Q.QuestionID,
		Q.ItemTitle,
		Q.ListeningFileUrl
	FROM  dbo.Products P
	INNER JOIN dbo.Tests T
	ON P.ProductID = T.ProductID
	INNER JOIN dbo.UserTests UT
	ON T.TestID = UT.TestID
	INNER JOIN dbo.UserQuestions UQ
	ON UQ.UserTestID = UT.UserTestID
	INNER JOIN dbo.Questions Q
	ON Q.QID = UQ.QID
	WHERE UT.UserTestID=@UserTestID
	AND UQ.QuestionNumber < @QuestionNumber
	AND TypeOfFileID = @TypeOfFileId
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetEmails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetEmails]
GO

CREATE PROCEDURE dbo.uspGetEmails
@EmailId INT = 0
AS
BEGIN

SET NOCOUNT ON

	SELECT [EmailId]
      ,[Title]
      ,[Body]
      ,[EmailType]
	FROM [Email]
	WHERE (@EmailId = 0 OR (@EmailId <> 0 AND emailId = @EmailId))
		
SET NOCOUNT OFF
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLocalInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetLocalInstitution]
GO

CREATE PROCEDURE dbo.uspGetLocalInstitution
@UserId INT
AS
BEGIN
	SELECT dbo.NurInstitution.InstitutionId,dbo.NurInstitution.InstitutionName
	FROM   dbo.NurInstitution
	INNER JOIN dbo.NurAdminInstitution ON dbo.NurInstitution.InstitutionID = dbo.NurAdminInstitution.InstitutionID
	WHERE (dbo.NurAdminInstitution.AdminID = @UserId) AND (dbo.NurAdminInstitution.Active = 1)
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdminByInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAdminByInstitution]
GO

CREATE PROCEDURE dbo.uspGetAdminByInstitution
@InstitutionIds VARCHAR(MAX),
@SearchText VARCHAR(100)
AS
BEGIN
 SELECT NA.FirstName + ' ' + NA.lastName UserName, NA.UserId
 ,NA.UserName ,NA.Email,NA.LastName,NA.FirstName
 FROM dbo.NurAdmin NA
 INNER JOIN dbo.NurAdminInstitution NAI ON NA.UserID = NAI.AdminID
 WHERE ((@InstitutionIds <> '' AND NAI.InstitutionID IN (select value from  dbo.funcListToTableInt(@InstitutionIds,'|'))) OR @InstitutionIds = '')
 AND (NAI.Active = 1)
	AND ( LEN(@SearchText) = 0 OR (LEN(@SearchText) > 0
	AND (NA.UserName LIKE '%' + @SearchText + '%'
	OR NA.Email LIKE '%' + @SearchText + '%'
	OR NA.LastName LIKE '%' + @SearchText + '%'
	OR NA.FirstName LIKE '%' + @SearchText + '%')))
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudentForEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchStudentForEmail]
GO

CREATE PROCEDURE dbo.uspSearchStudentForEmail
@criteria VARCHAR(MAX)
AS
BEGIN
	
	SELECT FirstName + ' ' + lastName UserName, UserId
	FROM  NurStudentInfo
	WHERE (LEN(LTRIM(RTRIM(@criteria))) <> 0)
	AND ((FirstName like '%' + @criteria + '%')
	OR (UserName like '%' + @criteria + '%')
	OR (Email like '%' + @criteria + '%')
	OR (LastName like '%' + @criteria + '%'))
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchAdmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchAdmin]
GO

CREATE PROCEDURE dbo.uspSearchAdmin
@criteria VARCHAR(MAX)
AS
BEGIN
	
	SELECT FirstName + ' ' + lastName UserName, UserId
	FROM  NurAdmin
	WHERE (LEN(LTRIM(RTRIM(@criteria))) <> 0)
	AND ((FirstName like '%' + @criteria + '%')
	OR (UserName like '%' + @criteria + '%')
	OR (Email like '%' + @criteria + '%')
	OR (LastName like '%' + @criteria + '%'))

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateCustomEmailToPerson]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateCustomEmailToPerson]
GO

CREATE PROCEDURE dbo.uspCreateCustomEmailToPerson
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent int,
@PersonIds VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

	SET @EmailMissionId = SCOPE_IDENTITY()
	
	INSERT INTO [EmailPerson]
           ([EmailMissionID]
           ,[PersonID])
	SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@PersonIds,'|')
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateCustomEmailToGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateCustomEmailToGroup]
GO

CREATE PROCEDURE dbo.uspCreateCustomEmailToGroup
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent BIT,
@GroupIds VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

	SET @EmailMissionId = SCOPE_IDENTITY()
	
	INSERT INTO EmailGroup
           ([EmailMissionID]
           ,[GroupId])
	SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@GroupIds,'|')
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateCustomEmailToCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateCustomEmailToCohort]
GO

CREATE PROCEDURE dbo.uspCreateCustomEmailToCohort
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent BIT,
@CohortIds VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

	SET @EmailMissionId = SCOPE_IDENTITY()
	
	INSERT INTO EmailCohort
           ([EmailMissionID]
           ,[CohortId])
	SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@CohortIds,'|')
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateCustomEmailToInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateCustomEmailToInstitution]
GO

CREATE PROCEDURE dbo.uspCreateCustomEmailToInstitution
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent BIT,
@InstitutionIds VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

	SET @EmailMissionId = SCOPE_IDENTITY()
	
	INSERT INTO EmailInstitution
           ([EmailMissionID]
           ,[InstitutionId])
	SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@InstitutionIds,'|')
	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspReturnTitles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspReturnTitles]
GO

CREATE PROCEDURE [dbo].[uspReturnTitles]
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT distinct TopicTitle,RemediationId
	FROM Remediation
	
	RETURN
		
	SET NOCOUNT OFF
END
GO

SET NOCOUNT OFF

/****** Object:  StoredProcedure [dbo].[uspReturnPreviousQuestion]    Script Date: 06/01/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAuthenticateAdminUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAuthenticateAdminUser]
GO
/****** Object:  StoredProcedure [dbo].[uspAuthenticateAdminUser]    Script Date: 06/01/2011  ******/


CREATE PROCEDURE [dbo].[uspAuthenticateAdminUser]
 @UserName varchar(50),
 @UserPassword Varchar(50)
AS
 BEGIN
	  SET NOCOUNT ON
	   SELECT A.UserId,
		   A.FirstName,
		   A.LastName,
		   A.UserName,
		   A.UserPass ,
		   A.SecurityLevel,
		   A.Email,
		   A.UploadAccess
	   FROM dbo.NurAdmin A
	   WHERE  UserName = @UserName
		   AND UserPass = @UserPassword
  SET NOCOUNT OFF
 END
	
	
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetReleaseQuestions' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetReleaseQuestions]
GO

Create PROCEDURE [dbo].[uspGetReleaseQuestions]
AS
BEGIN
	SELECT	Questions.QID,
			Questions.QuestionID,
			Questions.ReleaseStatus,
			dbo.Remediation.TopicTitle,
			ClientNeeds.ClientNeeds,
			dbo.ClientNeedCategory.ClientNeedCategory,
			dbo.Systems.System,
			NursingProcess.NursingProcess
	FROM   dbo.Questions
	LEFT OUTER JOIN dbo.ClientNeedCategory
	ON dbo.Questions.ClientNeedsCategoryID = dbo.ClientNeedCategory.ClientNeedCategoryID
	LEFT OUTER JOIN dbo.Remediation
	ON dbo.Questions.RemediationID = dbo.Remediation.RemediationID
	LEFT OUTER JOIN dbo.NursingProcess
	ON dbo.Questions.NursingProcessID = dbo.NursingProcess.NursingProcessID
	LEFT OUTER JOIN dbo.ClientNeeds
	ON dbo.Questions.ClientNeedsID = dbo.ClientNeeds.ClientNeedsID
	LEFT OUTER JOIN dbo.Systems ON dbo.Questions.SystemID = dbo.Systems.SystemID
	WHERE Questions.ReleaseStatus != 'R'
	ORDER BY Questions.QuestionID
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetReleaseLippinCots' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetReleaseLippinCots]

GO

Create PROCEDURE [dbo].[uspGetReleaseLippinCots]
AS
BEGIN
 SELECT dbo.Lippincot.LippincottID, dbo.Lippincot.RemediationID, dbo.Remediation.TopicTitle, dbo.Lippincot.LippincottTitle, Lippincot.ReleaseStatus
 FROM dbo.Lippincot LEFT OUTER JOIN dbo.Remediation
 ON dbo.Lippincot.RemediationID = dbo.Remediation.RemediationID
 WHERE Lippincot.ReleaseStatus != 'R'
End

GO

 IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetReleaseTests' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetReleaseTests]

GO

CREATE PROCEDURE [dbo].[uspGetReleaseTests]
@ReleaseStatus AS CHAR(1)
AS
BEGIN
 SELECT
	TestID,
	ProductID,
	TestName,
	TestNumber,
	ActivationTime,
	TimeActivated,
	SecureTest_S,
	SecureTest_D,
	Scrambling_S,
	Scrambling_D,
	Remediation_S,
	Remediation_D,
	Explanation_D,
	Explanation_S,
	LevelOfDifficulty_S,
	LevelOfDifficulty_D,
	NursingProcess_S,
	NursingProcess_D,
	ClinicalConcepts_S,
	ClinicalConcepts_D,
	Demographics_S,
	Demographics_D,
	ClientNeeds_S,
	ClientNeeds_D,
	Blooms_S,
	Blooms_D,
	Topic_S,
	Topic_D,
	SpecialtyArea_S,
	SpecialtyArea_D,
	System_S,
	System_D,
	CriticalThinking_S,
	CriticalThinking_D,
	Reading_S,
	Reading_D,
	Math_S,
	Math_D,
	Writing_S,
	Writing_D,
	RemedationTime_S,
	RemedationTime_D,
	ExplanationTime_S,
	ExplanationTime_D,
	TimeStamp_S,
	TimeStamp_D,
	ActiveTest,
	TestSubGroup,
	Url,
	PopHeight,
	PopWidth,
	DefaultGroup ,
	ReleaseStatus
 FROM Tests
 WHERE ReleaseStatus  = @ReleaseStatus
 ORDER BY ProductID
END
GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPUpdateReleaseStatus' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspUpdateReleaseStatus]

GO

CREATE PROCEDURE [dbo].[uspUpdateReleaseStatus]
@Ids as Nvarchar(2000),
@ReleaseStatus as char(1),
@ReleaseChoice as nVarchar(50)
AS
BEGIN
   If(@ReleaseChoice = 'Questions')
    Begin
        UPDATE dbo.Questions
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        WHERE QID IN (SELECT * FROM dbo.funcListToTableInt(@Ids,'|'))
    End
   Else If(@ReleaseChoice='Remediation')
       Begin
        UPDATE dbo.Remediation
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  RemediationID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
   Else If(@ReleaseChoice='Lippincot')
       Begin
        Update dbo.Lippincot
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  LippincottID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
    Else If(@ReleaseChoice='Tests')
       Begin
        Update dbo.Tests
        SET ReleaseStatus =
            CASE
                WHEN @ReleaseStatus = 'A' AND ReleaseStatus = 'E' THEN 'A'
                WHEN @ReleaseStatus = 'R' AND ReleaseStatus = 'A' THEN 'R'
                WHEN @ReleaseStatus = 'E' THEN 'E'
                ELSE ReleaseStatus
            END
        where  TestID in (select * from dbo.funcListToTableInt(@Ids,'|'))
       End
END

Go

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseQuestionsToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseQuestionsToProduction]
GO

CREATE Proc [dbo].[uspReleaseQuestionsToProduction]
	@SourceNumber INT,
	@QID INT ,
	@XMLQID varchar(50),
	@QuestionId VARCHAR(50),
	@QuestionType CHAR (2),
	@ClientNeedsId VARCHAR(500),
	@ClientNeedsCategoryId VARCHAR(500),
	@NursingProcessId VARCHAR(500),
	@LevelOfDifficultyId VARCHAR(500),
	@DemographicId VARCHAR(500),
	@CognitiveLevelId VARCHAR(500),
	@CriticalThinkingId VARCHAR(500),
	@IntegratedConceptsId VARCHAR(500),
	@ClinicalConceptsId VARCHAR(500),
	@Stimulus VARCHAR(500),
	@Stem VARCHAR(5000),
	@Explanation VARCHAR(5000),
	@Remediation VARCHAR(5000),
	@RemediationId VARCHAR(5000),
	@TopicTitleId VARCHAR(500),
	@SpecialtyAreaId VARCHAR(500),
	@SystemId VARCHAR(500),
	@ReadingCategoryId VARCHAR(500),
	@ReadingId VARCHAR(500),
	@WritingCategoryId VARCHAR(500),
	@WritingId VARCHAR(500),
	@MathCategoryId VARCHAR(500),
	@MathId VARCHAR(500),
	@ProductLineId VARCHAR(500),
	@TypeOfFileId VARCHAR(500),
	@ItemTitle VARCHAR(500),
	@Statisctics VARCHAR(500),
	@CreatorId VARCHAR(500),
	@DateCreated VARCHAR (50),
	@EditorId VARCHAR(500),
	@DateEdited VARCHAR (500),
	@EditorId2 VARCHAR(500),
	@DateEdited2 VARCHAR (500),
	@SourceSBD VARCHAR (500),
	@WhoOwns VARCHAR(500),
	@WhereUsed VARCHAR(500),
	@PointBiserialsId VARCHAR(500),
	@Feedback VARCHAR (500),
	@Active INT ,
	@Deleted INT,
	@QuestionNumber nChar(10),
	@TestNumber nchar(10),
	@QNorming float,
	@ExhibitTab1 VARCHAR (5000),
	@ExhibitTab2 VARCHAR (5000),
	@ExhibitTab3 VARCHAR (5000),
	@ListeningFileUrl nVarchar(500),
	@AlternateStem VARCHAR (5000)
AS
Begin
SET IDENTITY_INSERT Questions ON
if not exists(select QID from Questions where QID = @QID)
Begin
 Insert Into Questions
	   (SourceNumber, XMLQID, QID, QuestionID, QuestionType, ClientNeedsID, ClientNeedsCategoryID, NursingProcessID, LevelOfDifficultyID,
	   DemographicID, CognitiveLevelID, CriticalThinkingID, IntegratedConceptsID, ClinicalConceptsID, Stimulus, Stem, Explanation, Remediation,
	   RemediationID, TopicTitleID, SpecialtyAreaID, SystemID, ReadingCategoryID, ReadingID, WritingCategoryID, WritingID, MathCategoryID, MathID,
	   ProductLineID, TypeOfFileID, ItemTitle, Statisctics, CreatorID, DateCreated, EditorID, DateEdited, EditorID_2, DateEdited_2, Source_SBD, WhoOwns,
	   WhereUsed, PointBiserialsID, Feedback, Active, Deleted, QuestionNumber, TestNumber, Q_Norming, ExhibitTab1, ExhibitTab2, ExhibitTab3,ListeningFileUrl,AlternateStem)
	   Values(@SourceNumber,@XMLQID,@QID,@QuestionId,@QuestionType,@ClientNeedsId,@ClientNeedsCategoryId,@NursingProcessId,@LevelOfDifficultyId,
	   @DemographicId,@CognitiveLevelId,@CriticalThinkingId,@IntegratedConceptsId,@ClinicalConceptsId,@Stimulus,@Stem,@Explanation,@Remediation,
	   @RemediationId,@TopicTitleId,@SpecialtyAreaId,@SystemId,@ReadingCategoryId,@ReadingId,@WritingCategoryId,@WritingId,@MathCategoryId,@MathId,
	   @ProductLineId,@TypeOfFileId,@ItemTitle,@Statisctics,@CreatorId,@DateCreated,@EditorId,@DateEdited,@EditorId2,@DateEdited2,@SourceSBD,@WhoOwns,
	   @WhereUsed,@PointBiserialsId,@Feedback,@Active,@Deleted,@QuestionNumber,@TestNumber,@QNorming,@ExhibitTab1,@ExhibitTab2,@ExhibitTab3,@ListeningFileUrl,@AlternateStem)
End
Else
Begin
UPDATE Questions
 SET
		 SourceNumber = @SourceNumber,
		 XMLQID = @XMLQID,
		 QuestionID = @QuestionId,
		 QuestionType = @QuestionType,
		 ClientNeedsID = @ClientNeedsId,
		 ClientNeedsCategoryID = @ClientNeedsCategoryId,
		 NursingProcessID = @NursingProcessId,
		 LevelOfDifficultyID = @LevelOfDifficultyId,
		 DemographicID = @DemographicId,
		 CognitiveLevelID = @CognitiveLevelId,
		 CriticalThinkingID = @CriticalThinkingId,
		 IntegratedConceptsID = @IntegratedConceptsId,
		 ClinicalConceptsID = @ClinicalConceptsId,
		 Stimulus = @Stimulus,
		 Stem = @Stem,
		 Explanation = @Explanation ,
		 Remediation = @Remediation,
		 RemediationID = @RemediationId,
		 TopicTitleID = @TopicTitleId,
		 SpecialtyAreaID = @SpecialtyAreaId,
		 SystemID = @SystemId,
		 ReadingCategoryID = @ReadingCategoryId,
		 ReadingID = @ReadingId,
		 WritingCategoryID = @WritingCategoryId,
		 WritingID = @WritingId,
		 ProductLineID = @ProductLineId,
		 MathCategoryID=@MathCategoryId,
		 MathID = @MathId,
		 TypeOfFileID = @TypeOfFileId,
		 ItemTitle = @ItemTitle,
		 Statisctics = @Statisctics,
		 CreatorID = @CreatorId,
		 DateCreated = @DateCreated,
		 EditorID = @EditorId,
		 DateEdited = @DateEdited,
		 EditorID_2 = @EditorId2,
		 DateEdited_2 = @DateEdited2,
		 Source_SBD = @SourceSBD,
		 WhoOwns = @WhoOwns,
		 WhereUsed = @WhereUsed,
		 PointBiserialsID = @PointBiserialsId,
		 Feedback = @Feedback,
		 Active = @Active,
		 Deleted = @Deleted,
		 QuestionNumber = @QuestionNumber,
		 TestNumber = @TestNumber,
		 Q_Norming = @QNorming,
		 ExhibitTab1 = @ExhibitTab1,
		 ExhibitTab2 = @ExhibitTab2,
		 ExhibitTab3 = @ExhibitTab3,
		 ListeningFileUrl = @ListeningFileUrl,
		 AlternateStem = @AlternateStem
 WHERE  QID = @QID
END

SET IDENTITY_INSERT Questions OFF
End

GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteAnswerChoices' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteAnswerChoices]
GO

CREATE PROCEDURE [dbo].[uspDeleteAnswerChoices]
 @QIds varchar(4000)
As
Begin
Delete AnswerChoices where QID in
(select * from dbo.funcListToTableInt(@QIds,'|'))
End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseAnswerChoiceToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseAnswerChoiceToProduction]
GO

CREATE PROC dbo.uspReleaseAnswerChoiceToProduction
	@QId as int,
	@AIndex as char(1),
	@AText as varchar(3000),
	@Correct as int,
	@AnswerConnectId as int,
	@AType as int,
	@InitialPos as int,
	@Unit as char(50),
	@AnswerId as int,
	@AlternateAText as varchar(3000)
AS
BEGIN
  INSERT INTO AnswerChoices
	(
		QID,
		AIndex,
		AText,
		Correct,
		AnswerConnectID,
		AType,
		initialPos,
		Unit,
		AlternateAText
	)
  VALUES
	(
		@QId,
		@AIndex,
		@AText,
		@Correct,
		@AnswerConnectId,
		@AType,
		@InitialPos,
		@Unit,
		@AlternateAText
	)
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseRemediationToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseRemediationToProduction]
GO

CREATE PROC dbo.uspReleaseRemediationToProduction
 @RemediationId as int,
 @Explanation as varchar(5000),
 @TopicTitle as varchar(500)
AS
Begin
if exists(select RemediationID from Remediation where RemediationID = @RemediationId)
 Begin
   UPDATE Remediation
   SET
   Remediation.Explanation = @Explanation,
   Remediation.TopicTitle = @TopicTitle
   Where RemediationID = @RemediationId
 End
Else
   Begin
    SET IDENTITY_INSERT Remediation ON
    Insert Into Remediation (RemediationID, Explanation, TopicTitle)
    Values(@RemediationId,@Explanation,@TopicTitle)
    SET IDENTITY_INSERT Remediation OFF
   End
End

GO



IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetLippincotts' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetLippincotts]
GO

CREATE PROCEDURE [dbo].[uspGetLippincotts]
@LippincottId int,
@ReleaseStatus char(1)
AS
BEGIN
 SELECT LippincottID, RemediationID, LippincottTitle, LippincottExplanation, LippincottTitle2, LippincottExplanation2
 from Lippincot
 where (LippincottID = @LippincottId or @LippincottId=0) and (ReleaseStatus = @ReleaseStatus or @ReleaseStatus='')
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseLippincotToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseLippincotToProduction]
GO

Create PROC dbo.uspReleaseLippincotToProduction
@LippincottId as int,
@RemediationId as int,
@LippincottTitle as varchar(800),
@LippincottExplanation ntext,
@LippincottTitle2 as varchar(800),
@LippincottExplanation2 ntext
As
Begin
 Delete Lippincot where LippincottID=@LippincottId
 SET IDENTITY_INSERT Lippincot ON
 Insert Into Lippincot
 (LippincottID, RemediationID, LippincottTitle, LippincottExplanation, LippincottTitle2,LippincottExplanation2)
 Values(@LippincottId,@RemediationId,@LippincottTitle,@LippincottExplanation,@LippincottTitle2,@LippincottExplanation2)
 SET IDENTITY_INSERT Lippincot OFF
End

GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteReleaseQuestionLippinCott' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteReleaseQuestionLippinCott]

GO

Create PROCEDURE [dbo].[uspDeleteReleaseQuestionLippinCott]
@LippincottIds Varchar(4000)
AS
BEGIN
 Delete from QuestionLippincott
  where (LippincottID in (select * from dbo.funcListToTableInt(@LippincottIds,'|')))
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseQuestionLippincotToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseQuestionLippincotToProduction]
GO

CREATE PROC dbo.uspReleaseQuestionLippincotToProduction
 @LippincottId as int,
 @QID as int
As
Begin
  Insert Into QuestionLippincott(QID, LippincottID)
  Values(@QID,@LippincottId)
End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseTestsToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseTestsToProduction]

GO

CREATE PROC dbo.uspReleaseTestsToProduction
 @TestId int,
 @ProductId int,
 @TestName varchar (50),
 @TestNumber int,
 @ActivationTime datetime,
 @TimeActivated int,
 @SecureTestS int,
 @SecureTestD int,
 @ScramblingS int,
 @ScramblingD int,
 @RemediationS int,
 @RemediationD int,
 @ExplanationD int,
 @ExplanationS int,
 @LevelOfDifficultyS int,
 @LevelOfDifficultyD int,
 @NursingProcessS int,
 @NursingProcessD int,
 @ClinicalConceptsS int,
 @ClinicalConceptsD int,
 @DemographicsS int,
 @DemographicsD int,
 @ClientNeedsS int,
 @ClientNeedsD int,
 @BloomsS int,
 @BloomsD int,
 @TopicS int,
 @TopicD int,
 @SpecialtyAreaS int,
 @SpecialtyAreaD int,
 @SystemS int,
 @SystemD int,
 @CriticalThinkingS int,
 @CriticalThinkingD int,
 @ReadingS int,
 @ReadingD int,
 @MathS int,
 @MathD int,
 @WritingS int,
 @WritingD int,
 @RemedationTimeS int,
 @RemedationTimeD int,
 @ExplanationTimeS int,
 @ExplanationTimeD int,
 @TimeStampS int,
 @TimeStampD int,
 @ActiveTest int,
 @TestSubGroup int,
 @Url nvarchar (200),
 @PopHeight int,
 @PopWidth int,
 @DefaultGroup char(1)
As
Begin
if exists(select TestID from Tests where TestID = @TestId)
 Begin
  UPDATE Tests
   SET
   ProductID = @ProductId,
   TestName = @TestName,
   TestNumber = @TestNumber,
   ActivationTime = @ActivationTime,
   TimeActivated = @TimeActivated,
   SecureTest_S = @SecureTestS,
   SecureTest_D = @SecureTestD,
   Scrambling_S = @ScramblingS,
   Scrambling_D = @ScramblingD,
   Remediation_S = @RemediationS,
   Remediation_D = @RemediationD,
   Explanation_D = @ExplanationD,
   Explanation_S = @ExplanationS,
   LevelOfDifficulty_S = @LevelOfDifficultyS,
   LevelOfDifficulty_D = @LevelOfDifficultyD,
   NursingProcess_S = @NursingProcessS,
   NursingProcess_D = @NursingProcessD,
   ClinicalConcepts_S = @ClinicalConceptsS,
   ClinicalConcepts_D = @ClinicalConceptsD,
   Demographics_S = @DemographicsS,
   Demographics_D = @DemographicsD,
   ClientNeeds_S = @ClientNeedsS,
   ClientNeeds_D = @ClientNeedsD,
   Blooms_S = @BloomsS,
   Blooms_D = @BloomsD,
   Topic_S = @TopicS,
   Topic_D = @TopicD,
   SpecialtyArea_S = @SpecialtyAreaS,
   SpecialtyArea_D = @SpecialtyAreaD,
   System_S = @SystemS,
   System_D = @SystemD,
   CriticalThinking_S = @CriticalThinkingS,
   CriticalThinking_D = @CriticalThinkingD,
   Reading_S = @ReadingS,
   Reading_D = @ReadingD,
   Math_S = @MathS,
   Math_D = @MathD,
   Writing_S = @WritingS,
   Writing_D = @WritingD,
   RemedationTime_S = @RemedationTimeS,
   RemedationTime_D = @RemedationTimeD,
   ExplanationTime_S = @ExplanationTimeS,
   ExplanationTime_D = @ExplanationTimeD,
   TimeStamp_S = @TimeStampS,
   TimeStamp_D = @TimeStampD,
   ActiveTest = @ActiveTest,
   TestSubGroup = @TestSubGroup,
   Url = @Url,
   PopHeight = @PopHeight,
   PopWidth = @PopWidth,
   DefaultGroup = @DefaultGroup
   Where TestID = @TestId
 End
 Else
 Begin
   SET IDENTITY_INSERT Tests ON
   Insert Into Tests
   (TestID, ProductID, TestName, TestNumber, ActivationTime, TimeActivated, SecureTest_S, SecureTest_D, Scrambling_S, Scrambling_D,
   Remediation_S, Remediation_D, Explanation_D, Explanation_S, LevelOfDifficulty_S, LevelOfDifficulty_D, NursingProcess_S, NursingProcess_D,
   ClinicalConcepts_S, ClinicalConcepts_D, Demographics_S, Demographics_D, ClientNeeds_S, ClientNeeds_D, Blooms_S, Blooms_D, Topic_S,Topic_D,
   SpecialtyArea_S, SpecialtyArea_D, System_S, System_D, CriticalThinking_S, CriticalThinking_D, Reading_S, Reading_D, Math_S, Math_D,
   Writing_S, Writing_D, RemedationTime_S, RemedationTime_D, ExplanationTime_S, ExplanationTime_D, TimeStamp_S, TimeStamp_D, ActiveTest,
   TestSubGroup, Url, PopHeight, PopWidth, DefaultGroup)
   Values
   (@TestId,@ProductId,@TestName,@TestNumber,@ActivationTime,@TimeActivated,@SecureTestS,@SecureTestD,@ScramblingS,@ScramblingD,
    @RemediationS,@RemediationD,@ExplanationD,@ExplanationS,@LevelOfDifficultyS,@LevelOfDifficultyD,@NursingProcessS,@NursingProcessD,
    @ClinicalConceptsS,@ClinicalConceptsD,@DemographicsS,@DemographicsD,@ClientNeedsS,@ClientNeedsD,@BloomsS,@BloomsD,@TopicS,@TopicD,
    @SpecialtyAreaS,@SpecialtyAreaD,@SystemS,@SystemD,@CriticalThinkingS,@CriticalThinkingD,@ReadingS,@ReadingD,@MathS,@MathD,
    @WritingS,@WritingD,@RemedationTimeS,@RemedationTimeD,@ExplanationTimeS,@ExplanationTimeD,@TimeStampS,@TimeStampD,@ActiveTest,
    @TestSubGroup,@Url,@PopHeight,@PopWidth,@DefaultGroup)
    SET IDENTITY_INSERT Tests OFF
 End
End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPGetListOfTestQuestions' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetListOfTestQuestions]

GO
CREATE PROCEDURE [dbo].[uspGetListOfTestQuestions]
@TestId int ,
@TestIds Varchar(4000)
AS
BEGIN
 select TestID,QuestionID,QID,QuestionNumber,id,Q_Norming
 from  dbo.TestQuestions
 where (TestID=@TestId or @TestId =0) and
  ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))or @TestIds='')

END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPDeleteTestChildTableRows' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspDeleteTestChildTableRows]

GO

CREATE PROCEDURE [dbo].[uspDeleteTestChildTableRows]
    @TestIds as Varchar(4000)
    As
    Begin
	  Delete from TestQuestions
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from TestCategory
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from Norming
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
	
	  Delete from Norm
	  where (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|')))
    End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseTestCategoryToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseTestCategoryToProduction]

GO

CREATE PROC dbo.uspReleaseTestCategoryToProduction
@Id as INT,
@TestId as INT,
@CategoryId as Varchar(50),
@Student as INT,
@Admin as INT
AS
Begin
 SET IDENTITY_INSERT TestCategory ON
   Insert Into TestCategory (id, TestID, CategoryID, Student, [Admin])
   values(@Id,@TestId,@CategoryId,@Student,@Admin)
 SET IDENTITY_INSERT TestCategory OFF
End

GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseTestQuestionsToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseTestQuestionsToProduction]

GO

create PROC dbo.uspReleaseTestQuestionsToProduction
@Id as INT,
@TestId as INT,
@QuestionId as Varchar(50),
@QID as INT,
@QuestionNumber as INT,
@QNorming as Float
AS
Begin
SET IDENTITY_INSERT TestQuestions ON
	Insert Into TestQuestions  (id, TestID, QuestionID, QID, QuestionNumber, Q_Norming)
	values(@Id,@TestId,@QuestionId,@QID,@QuestionNumber,@QNorming)
SET IDENTITY_INSERT TestQuestions OFF
End

GO
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseNormingToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseNormingToProduction]

GO

CREATE PROC dbo.uspReleaseNormingToProduction
@Id as INT,
@NumberCorrect as float,
@Correct as float,
@PercentileRank as float,
@Probability as float,
@TestId as INT
AS
Begin
 SET IDENTITY_INSERT Norming ON
   Insert Into Norming(id, NumberCorrect, Correct, PercentileRank, Probability, TestID)
   Values(@Id,@NumberCorrect,@Correct,@PercentileRank,@Probability,@TestId)
 SET IDENTITY_INSERT Norming OFF
End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPReleaseNormToProduction' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspReleaseNormToProduction]

GO

CREATE PROC dbo.uspReleaseNormToProduction
@Id as INT,
@ChartType as nVarchar(50),
@ChartId as int,
@Norm as real,
@TestId as INT
AS
Begin
 SET IDENTITY_INSERT Norm ON
   Insert Into Norm(ID, ChartType, ChartID, Norm, TestID)
   Values(@Id,@ChartType,@ChartId,@Norm,@TestId)
 SET IDENTITY_INSERT Norm OFF
End

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPInsertCaseModuleScore' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspInsertCaseModuleScore]

GO

Create PROCEDURE [dbo].[uspInsertCaseModuleScore]
@CaseID int,
@MID int,
@StudentID varchar(50),
@Correct int,
@Total int,
@Id INT OUTPUT
AS
Begin
	INSERT INTO dbo.CaseModuleScore (CaseID,ModuleID,StudentID,Correct,Total)
	Values (@CaseID,@MID,@StudentID,@Correct,@Total)
	set @Id = Scope_Identity()
	select @@Identity
End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetStudentListForSetOverride]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetStudentListForSetOverride]
GO

CREATE PROCEDURE [dbo].[uspGetStudentListForSetOverride]
(
	@InstitutionId int,
	@FirstName varchar(100),
	@LastName varchar(100),
	@UserName varchar(100),
	@TestName varchar(100),
	@ShowIncompleteOnly bit
)
AS
BEGIN
	SET NOCOUNT ON
	SELECT TOP 3001 U.UserTestID,
		S.FirstName,
		S.LastName,
		S.UserName,
		T.TestName,
		U.TestStatus,
		DATEADD(hour, TZ.[Hour], TestStarted) as TestStarted
	FROM dbo.NurStudentInfo S
		INNER JOIN dbo.UserTests U ON S.UserID = U.UserID
		INNER JOIN dbo.NurCohort ON U.CohortID = dbo.NurCohort.CohortID
		INNER JOIN dbo.Tests T ON U.TestID = T.TestID
		INNER JOIN dbo.NurInstitution AS I
		ON I.InstitutionId = S.InstitutionId
		INNER JOIN TimeZones TZ
		ON I.TimeZone = TZ.TimeZoneID
	WHERE
	(@InstitutionId = 0
			OR	S.InstitutionId = @InstitutionId)
	AND (@FirstName = ''
			OR	S.FirstName  like +'%'+ @FirstName + '%' )
	AND (@LastName = ''
			OR	S.LastName like +'%'+ @LastName + '%'  )
	AND (@UserName = ''
			OR	S.UserName like +'%'+ @UserName + '%'  )
	AND (@TestName = ''
			OR	T.TestName like +'%'+ @TestName + '%'  )
	AND (@ShowIncompleteOnly = 0
		OR (@ShowIncompleteOnly = 1 AND ISNULL(U.TestStatus, 0) != 1))
	SET NOCOUNT OFF
END
GO

/****** Object:  StoredProcedure [dbo].[uspDeleteTest]    Script Date: 06/01/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteTest]
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteTest]    Script Date: 06/01/2011  ******/

CREATE PROCEDURE [dbo].[uspDeleteTest]
	@TestId INT,
    @UserName VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	
	BEGIN TRANSACTION
	BEGIN TRY

        INSERT INTO UserQuestionsHistory
        (
            ID,
            UserTestID,
            QID,
            QuestionNumber,
            Correct,
            TimeSpendForQuestion,
            TimeSpendForRemedation,
            TimeSpendForExplanation,
            AnswerTrack,
            IncorrecCorrect,
            FirstChoice,
            SecondChoice,
            AnswerChanges,
            OrderedIndexes
         )
        SELECT
            ID,
            UserTestID,
            QID,
            QuestionNumber,
            Correct,
            TimeSpendForQuestion,
            TimeSpendForRemedation,
            TimeSpendForExplanation,
            AnswerTrack,
            IncorrecCorrect,
            FirstChoice,
            SecondChoice,
            AnswerChanges,
            OrderedIndexes
        FROM  UserQuestions
        WHERE UserTestID = @TestId

        UPDATE UserQuestionsHistory
        SET DeletedBy = @UserName,
            DeletedDate = GETDATE()
        WHERE UserTestID = @TestId


        INSERT INTO UserTestsHistory
        (
            UserTestID,
            UserID,
            TestID,
            TestNumber,
            InsitutionID,
            CohortID,
            ProgramID,
            TestStarted,
            TestComplited,
            TestStarted_R,
            TestComplited_R,
            TestStatus,
            TimedTest,
            TutorMode,
            ReusedMode,
            NumberOfQuestions,
            QuizOrQBank,
            TimeRemaining,
            SuspendQuestionNumber,
            SuspendQID,
            SuspendType,
            ProductID,
            TestName,
            Override
        )
        SELECT
            UserTestID,
            UserID,
            TestID,
            TestNumber,
            InsitutionID,
            CohortID,
            ProgramID,
            TestStarted,
            TestComplited,
            TestStarted_R,
            TestComplited_R,
            TestStatus,
            TimedTest,
            TutorMode,
            ReusedMode,
            NumberOfQuestions,
            QuizOrQBank,
            TimeRemaining,
            SuspendQuestionNumber,
            SuspendQID,
            SuspendType,
            ProductID,
            TestName,
            Override
        FROM  UserTests
        WHERE UserTestID = @TestId

        UPDATE UserTestsHistory
        SET DeletedBy = @UserName,
            DeletedDate = GETDATE()
        WHERE UserTestID = @TestId

		DELETE UserQuestions
		WHERE UserTestID = @TestId

		DELETE UserTests
		WHERE UserTestID = @TestId

		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH

	SET NOCOUNT OFF
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveEmail]
GO

CREATE PROCEDURE dbo.uspSaveEmail
@emailId INT,
@title VARCHAR(MAX),
@body VARCHAR(MAX)
AS
BEGIN
	IF (@emailId = -1)
	BEGIN
		INSERT INTO [dbo].[Email]
           ([Title]
           ,[Body]
           ,[EmailType])
     VALUES
           (@title
           ,@body
           ,-99)
	END
	ELSE
	BEGIN
		UPDATE Email
		SET
			Title = @title,
			Body = @body
		WHERE EmailID = @emailId
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveTestQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveTestQuestion]
GO

 CREATE PROCEDURE [dbo].[uspSaveTestQuestion]
	 @TestId as int,
	 @QuestionId as varchar(50),
	 @Qid as int,
	 @QuestionNumber as int
 AS
 Begin
       INSERT INTO TestQuestions (TestID, QuestionID, QID, QuestionNumber)
       VALUES (@TestId, @QuestionId, @Qid, @QuestionNumber)
 End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteTestQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteTestQuestion]
GO

CREATE PROCEDURE [dbo].[uspDeleteTestQuestion]
 @TestId int
As
Begin
  Delete TestQuestions where TestID = @TestId
End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionsInTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetQuestionsInTest]
GO

CREATE PROCEDURE dbo.uspGetQuestionsInTest
  @TestId as int
 AS
 Begin
  SELECT Questions.Stem,Questions.QuestionID,Questions.QID,TestQuestions.QuestionNumber
  FROM dbo.Tests INNER JOIN dbo.TestQuestions
  ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN dbo.Questions
  ON dbo.TestQuestions.QID = dbo.Questions.QID
  WHERE Tests.TestID =@TestId
  order by TypeOfFileID ASC,QuestionNumber ASC
End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetQuestionsToCreateTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetQuestionsToCreateTest]
GO

CREATE PROCEDURE dbo.uspGetQuestionsToCreateTest
	@testId int, @scramble int
AS

IF @scramble = 0 OR @testId = 63
	SELECT TestQuestions.QID, TestQuestions.QuestionNumber
	FROM TestQuestions,Questions
	WHERE TestQuestions.QID=Questions.QID AND TypeOfFileID='03'
		AND TestID = @testId ORDER BY TestQuestions.QuestionNumber
ELSE
	SELECT TestQuestions.QID, TestQuestions.QuestionNumber
	FROM TestQuestions,Questions
	WHERE TestQuestions.QID=Questions.QID AND TypeOfFileID='03'
		AND TestID = @testId ORDER BY NEWID()

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTestQuestionsQbank]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTestQuestionsQbank]
GO

CREATE PROCEDURE dbo.uspGetTestQuestionsQbank
 @rowLimit int, @testId int, @userId int, @institutionId int,
 @programId int, @cohortId int, @correct int, @filter int, @options varchar(500)
AS

IF (@filter = 3)
 BEGIN
	 -- return all questions
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
			ON TestQuestions.QID=Questions.QID
	  WHERE TestQuestions.TestID = @testId
			AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
  END
ELSE IF (@filter = 1)
  BEGIN
	  -- return all unused questions
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID  NOT IN
	   (SELECT distinct UserQuestions.QID
		FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
		WHERE UserTests.UserID = @userId
		AND UserTests.InsitutionID = @institutionId
		AND dbo.UserTests.TestID = @testId)
	    AND TestQuestions.TestID = @testId
	    AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END
ELSE IF (@filter = 4)
   BEGIN
	  -- return all incorrect questions
	  -- @filter = 4, @correct=0: Incorrect only
	
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID IN
	   (
		 SELECT  UserQuestions.QID
			FROM  UserQuestions INNER JOIN UserTests
				ON UserQuestions.UserTestID = UserTests.UserTestID
			WHERE (Correct = 0 OR Correct = 2) AND UserTests.UserID = @userId
				AND UserTests.InsitutionID = @institutionId
			  AND dbo.UserTests.TestID = @testId
	
		  Except
	
		SELECT  UserQuestions.QID
		FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
		WHERE (Correct = 1) AND UserTests.UserID = @userId
			  AND UserTests.InsitutionID = @institutionId
			  AND dbo.UserTests.TestID = @testId
		)
	   AND TestQuestions.TestID = @testId
	   AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END
ELSE
   BEGIN
	  -- return all unused and incorrect/correct questions
	  -- @filter = 2, @correct=0: Unused and Incorrect
	
	  SET ROWCOUNT @rowLimit
	  SELECT Questions.QID, TestQuestions.QuestionNumber
	  FROM  Questions INNER JOIN TestQuestions
	   ON TestQuestions.QID=Questions.QID
	  WHERE Questions.QID  NOT IN
	   (SELECT distinct UserQuestions.QID
	   FROM  UserQuestions INNER JOIN UserTests
		ON UserQuestions.UserTestID = UserTests.UserTestID
	   WHERE Correct = 1 AND UserTests.UserID = @userId
		AND UserTests.InsitutionID = @institutionId
		AND dbo.UserTests.TestID = @testId)
	   AND TestQuestions.TestID = @testId
	   AND (1=0 OR Questions.ClientNeedsCategoryID > 0 AND Questions.ClientNeedsCategoryID IN (SELECT * FROM funcListToTableInt(@options, ',')))
	  ORDER BY NEWID()
	  SET ROWCOUNT 0
   END

GO

/****** Object:  StoredProcedure [dbo].[uspDeleteProgramFromProduct]    Script Date: 06/02/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteProgramFromProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteProgramFromProduct]
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteProgramFromProduct]    Script Date: 06/02/2011  ******/

CREATE PROCEDURE [dbo].[uspDeleteProgramFromProduct]
	 @ProgramId int,	
	 @ProductId int,	
	 @Type int
AS

SET NOCOUNT ON
BEGIN
	DELETE FROM NurProgramProduct
	WHERE ProgramID = @ProgramId
	AND ProductID = @ProductId
	AND Type = @Type
END
SET NOCOUNT OFF

GO

/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIdForCohort]    Script Date: 06/02/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionIdForCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetInstitutionIdForCohort]
GO
/****** Object:  StoredProcedure [dbo].[uspGetInstitutionIdForCohort]    Script Date: 06/02/2011  ******/

CREATE PROC [dbo].[uspGetInstitutionIdForCohort]
(
 @cohortId int
)
AS
SET NOCOUNT ON;
	BEGIN
SELECT InstitutionID FROM NurCohort WHERE CohortID = @cohortId

END

GO

/****** Object:  StoredProcedure [dbo].[uspSaveCohort]    Script Date: 06/02/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveCohort]
GO
/****** Object:  StoredProcedure [dbo].[uspSaveCohort]    Script Date: 06/02/2011  ******/

CREATE PROCEDURE [dbo].[uspSaveCohort]
	 @CohortId int,
	 @CohortName varchar(160),
	 @CohortDescription varchar(1000),
	 @CohortStatus int,
	 @CohortStartDate datetime,
	 @CohortEndDate datetime,
	 @CohortCreateUser int,
	 @InstitutionId  int,
     @NewCohortId int OUT
AS

	-- Prevent row count message
SET NOCOUNT ON;
DECLARE @ProgramId INT
SELECT @ProgramId = 0
SELECT @ProgramId = programID FROM NurInstitution WHERE InstitutionID = @InstitutionID
BEGIN
	IF @CohortId = 0
		BEGIN
			INSERT INTO NurCohort
						(
						CohortName,
						CohortDescription,
						CohortStatus,
						CohortStartDate,
						CohortEndDate,
						CohortCreateDate,
						CohortCreateUser,
						InstitutionID)
			VALUES
						(
						 @CohortName,
						 @CohortDescription,
						 @CohortStatus,
						 @CohortStartDate,
						 @CohortEndDate,
						 GETDATE(),
						 @CohortCreateUser,
						 @InstitutionId
						)
			SELECT @NewCohortId = SCOPE_IDENTITY()
			END
	ELSE
		BEGIN
			UPDATE NurCohort
			SET
				CohortName	= @CohortName,
				CohortDescription= @CohortDescription,
				CohortStatus	= @CohortStatus,
				InstitutionID	= @InstitutionID,
				CohortEndDate	= @CohortEndDate,
				CohortStartDate	= @CohortStartDate,
				CohortUpdateDate	= GETDATE(),
				CohortUpdateUser	= @CohortCreateUser
			WHERE CohortID	= @CohortId
			
			SELECT @NewCohortId = @CohortId
		END
	IF @ProgramId != 0
		BEGIN
			DECLARE @maxCohortId INT
			SELECT @maxCohortId = 0
			SELECT @maxCohortId = MAX(CohortID) FROM NurCohort -- check logic with Gokul

			UPDATE  NurCohortPrograms
			SET Active=0
			WHERE CohortID  = @maxCohortId
			
			DECLARE @CohortProgram INT
			SELECT @CohortProgram = 0;
			
			SELECT @CohortProgram = COUNT(*)
			FROM dbo.NurCohortPrograms
			WHERE ProgramID = @ProgramId
			AND CohortID = @maxCohortId
			
			IF @CohortProgram > 0
				BEGIN
					UPDATE  NurCohortPrograms
					SET Active=1
					WHERE CohortID = @maxCohortId
					AND ProgramID = @ProgramId
				END
			ELSE
				BEGIN
					INSERT INTO NurCohortPrograms
								(
								CohortID,
								ProgramID,
								Active
								)
								VALUES
								(
								 @maxCohortId,
								 @ProgramID ,
								 1
								)	
				END

		END
END


GO

/****** Object:  StoredProcedure [dbo].[uspDeleteCohort]    Script Date: 06/02/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteCohort]
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteCohort]    Script Date: 06/02/2011  ******/

CREATE PROCEDURE [dbo].[uspDeleteCohort]
	 @CohortId int,
	 @CohortStatus int,
	 @CohortDeleteUser int
AS

	-- Prevent row count message
SET NOCOUNT ON;
	BEGIN
		UPDATE NurCohort
		SET	CohortDeleteUser = @CohortDeleteUser,
			CohortDeleteDate=getdate(),
			CohortStatus=@CohortStatus
		WHERE CohortID=@CohortID
	END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckPercentileRankExist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckPercentileRankExist]

GO

CREATE PROCEDURE [dbo].[uspCheckPercentileRankExist]
 @UserTestID int
AS
BEGIN
 DECLARE @TestId INT

 SELECT @TestId = TestId
 FROM UserTests
 WHERE UserTestID=@UserTestID

 SELECT PercentileRank
 FROM Norming
 WHERE TestID = @testId
END

Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetGroupsList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetGroupsList]

GO

CREATE PROCEDURE [dbo].[uspGetGroupsList]
@InstitutionId as int,
@CohortId as int
AS
BEGIN
	SELECT  dbo.NurGroup.GroupName,
	C.CohortName,
	dbo.NurGroup.GroupID,
	C.CohortStatus,
	C.CohortID,
	I.InstitutionName,
	I.InstitutionID,
	CP.ProgramID,
	P.ProgramName
	FROM dbo.NurGroup
	INNER JOIN  dbo.NurCohort C
	ON dbo.NurGroup.CohortID = C.CohortID
	INNER JOIN dbo.NurInstitution I
	ON C.InstitutionID = I.InstitutionID
	LEFT OUTER JOIN  dbo.NurCohortPrograms CP
	ON C.CohortID = CP.CohortID
	AND dbo.NurGroup.CohortID = CP.CohortID
	INNER JOIN  dbo.NurProgram P
	ON CP.ProgramID = P.ProgramID
	WHERE     (C.CohortStatus = 1)
	AND (CP.Active = 1)
	AND C.InstitutionID= @InstitutionId
	AND dbo.NurGroup.CohortID = @CohortId

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetListOfInstitution]

GO

CREATE PROCEDURE dbo.uspGetListOfInstitution
@institutionIds VARCHAR(400)
AS
BEGIN
  SELECT
  InstitutionID,
  InstitutionName,
  Description,
  Status,
  ContactName,
  ContactPhone,
  DefaultCohortID,
  CenterID,
  TimeZone,
  IP,UpdateDate,
  UpdateUser,
  CreateUser,
  DeleteUser,
  DeleteDate,
  FacilityID,
  ProgramID
  FROM NurInstitution WHERE Status=1
  AND (@institutionIds = '' OR @institutionIds = '0' OR InstitutionID IN (SELECT VALUE  FROM  dbo.funcListToTableInt(@institutionIds,',')))
  --OR (@institutionIds = '0' OR InstitutionID IN (SELECT VALUE  FROM  dbo.funcListToTableInt(@institutionIds,',')))
  ORDER BY InstitutionName

END

Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNorms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNorms]

GO

CREATE PROCEDURE [dbo].[uspGetNorms]
@TestId as int,
@ChartType as nVarchar(50),
@TestIds as Varchar(4000)
AS
BEGIN
 select * from Norm
 where (TestID = @TestId or @TestId = 0) and (ChartType = @ChartType or @ChartType='')
 and ( (TestID in (select * from dbo.funcListToTableInt(@TestIds,'|'))) or @TestIds='')
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckProbabilityExist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCheckProbabilityExist]

GO

CREATE PROCEDURE [dbo].[uspCheckProbabilityExist]
 @UserTestId int
AS
BEGIN
 DECLARE @TestId INT

 SELECT @TestId = TestId
 FROM UserTests
 WHERE UserTestID=@UserTestID

    SELECT Probability
    FROM Norming
    WHERE TestID = @testId

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteCohort]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteCohort]

GO

CREATE PROCEDURE [dbo].[uspDeleteCohort]
  @CohortId int,
  @CohortStatus int,
  @CohortDeleteUser int
AS

 -- Prevent row count message
SET NOCOUNT ON;
 BEGIN
  UPDATE NurCohort
  SET CohortDeleteUser = @CohortDeleteUser,
   CohortDeleteDate=getdate(),
   CohortStatus=@CohortStatus
  WHERE CohortID=@CohortID
 END

 GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteInstitution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteInstitution]

GO

CREATE PROCEDURE [dbo].[uspDeleteInstitution]
@InstitutionId as int ,
@DeleteUserId as int
AS
BEGIN
	SET NOCOUNT ON
	UPDATE NurInstitution
		SET DeleteUser=@DeleteUserId,
		DeleteDate=getdate(),
		Status=0
	WHERE InstitutionID=@InstitutionId
	SET NOCOUNT OFF
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnAllQCountByClientNeedCategoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnAllQCountByClientNeedCategoryID]
GO

CREATE function [dbo].[ReturnAllQCountByClientNeedCategoryID]
(@ClientNeedCategoryID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT @Result=COUNT(*)
FROM  dbo.Tests INNER JOIN
dbo.TestQuestions ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN
dbo.Questions ON dbo.TestQuestions.QID = dbo.Questions.QID
WHERE dbo.Questions.ClientNeedsCategoryID=@ClientNeedCategoryID
AND dbo.Questions.ClientNeedsCategoryID > 0
AND (dbo.Tests.ProductID = 4) AND (dbo.Tests.TestSubGroup = 3)

RETURN @Result
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnIncorrectQCountByClientNeedCategoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnIncorrectQCountByClientNeedCategoryID]
GO

CREATE function [dbo].[ReturnIncorrectQCountByClientNeedCategoryID]
(@ClientNeedCategoryID int,@UserID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT  @Result=count(*)
FROM  dbo.Tests INNER JOIN
dbo.TestQuestions ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN
dbo.Questions ON dbo.TestQuestions.QID = dbo.Questions.QID
WHERE dbo.Questions.ClientNeedsCategoryID=@ClientNeedCategoryID
AND dbo.Questions.ClientNeedsCategoryID > 0
AND (dbo.Tests.ProductID = 4) AND (dbo.Tests.TestSubGroup = 3)
AND (dbo.TestQuestions.QID  IN
(
(SELECT   dbo.UserQuestions.QID
FROM  dbo.UserQuestions INNER JOIN
dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN
dbo.Tests AS Tests_1 ON dbo.UserTests.TestID = Tests_1.TestID
WHERE dbo.UserTests.UserID=@UserID
AND   (Tests_1.ProductID = 4) AND (Tests_1.TestSubGroup = 3)
AND (UserQuestions.Correct = 0 or UserQuestions.Correct = 2)
group by dbo.UserQuestions.QID

Except

SELECT dbo.UserQuestions.QID
FROM  dbo.UserQuestions INNER JOIN
dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN
dbo.Tests AS Tests_1 ON dbo.UserTests.TestID = Tests_1.TestID
WHERE  dbo.UserTests.UserID=@UserID
AND   (Tests_1.ProductID = 4) AND (Tests_1.TestSubGroup = 3) and (UserQuestions.Correct = 1)
group by dbo.UserQuestions.QID)
--"correct" is 1 for correct answer, 0 for incorrect answer,2 if question is suspended.
))

RETURN @Result
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]
GO

CREATE function [dbo].[ReturnUnUsedIncorrectQCountByClientNeedCategoryID]
(@ClientNeedCategoryID int,@UserID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT  @Result=count(*)
FROM  dbo.Tests INNER JOIN
dbo.TestQuestions ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN
dbo.Questions ON dbo.TestQuestions.QID = dbo.Questions.QID
WHERE dbo.Questions.ClientNeedsCategoryID=@ClientNeedCategoryID
AND dbo.Questions.ClientNeedsCategoryID > 0
AND (dbo.Tests.ProductID = 4) AND (dbo.Tests.TestSubGroup = 3) AND (dbo.TestQuestions.QID NOT IN
(SELECT   dbo.UserQuestions.QID
FROM  dbo.UserQuestions INNER JOIN
dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN
dbo.Tests AS Tests_1 ON dbo.UserTests.TestID = Tests_1.TestID
WHERE (dbo.UserQuestions.Correct=1) AND  dbo.UserTests.UserID=@UserID
AND (Tests_1.ProductID = 4) AND (Tests_1.TestSubGroup = 3)))
group by dbo.Questions.ClientNeedsCategoryID

RETURN @Result
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnUnUsedQCountByClientNeedCategoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnUnUsedQCountByClientNeedCategoryID]
GO

CREATE function [dbo].[ReturnUnUsedQCountByClientNeedCategoryID]
(@ClientNeedCategoryID int,@UserID int)
RETURNS INT
As
BEGIN
DECLARE @Result int

SELECT  @Result=count(*)
 FROM  dbo.Tests INNER JOIN
 dbo.TestQuestions ON dbo.Tests.TestID = dbo.TestQuestions.TestID INNER JOIN
 dbo.Questions ON dbo.TestQuestions.QID = dbo.Questions.QID
 WHERE dbo.Questions.ClientNeedsCategoryID=@ClientNeedCategoryID
 AND dbo.Questions.ClientNeedsCategoryID > 0
 AND (dbo.Tests.ProductID = 4) AND (dbo.Tests.TestSubGroup = 3) AND (dbo.TestQuestions.QID NOT IN
 (SELECT   dbo.UserQuestions.QID
 FROM  dbo.UserQuestions INNER JOIN
 dbo.UserTests ON dbo.UserQuestions.UserTestID = dbo.UserTests.UserTestID INNER JOIN
 dbo.Tests AS Tests_1 ON dbo.UserTests.TestID = Tests_1.TestID
 WHERE dbo.UserTests.UserID=@UserID  AND   (Tests_1.ProductID = 4) AND (Tests_1.TestSubGroup = 3)))

RETURN @Result
END

GO


IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'USPCreateUserInfoEmailToAdmin' AND TYPE = 'P')
DROP PROCEDURE [dbo].[USPCreateUserInfoEmailToAdmin]

GO

CREATE PROCEDURE dbo.USPCreateUserInfoEmailToAdmin
@EmailMissionId INT OUTPUT,
@AdminId INT,
@SendTime DATETIME,
@EmailId INT,
@ToAdminOrStudent int,
@PersonIds VARCHAR(MAX),
@InstitutionIds VARCHAR(MAX)
AS
BEGIN


 INSERT INTO [EmailMission]
           ([EmailID]
           ,[AdminID]
           ,[CreatedTime]
           ,[SendTime]
           ,[State]
           ,[RetryTimes]
           ,[ToAdminOrStudent]
           )
     VALUES( @EmailId,@AdminId,GETDATE(),@SendTime,1,0,@ToAdminOrStudent)

 SET @EmailMissionId = SCOPE_IDENTITY()

 INSERT INTO [EmailPerson]
           ([EmailMissionID]
           ,[PersonID])
 SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@PersonIds,'|')


 INSERT INTO EmailInstitution
           ([EmailMissionID]
           ,[InstitutionId])
 SELECT @EmailMissionId,value FROM dbo.funcListToTableInt(@InstitutionIds,'|')

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetNounAdjForPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetNounAdjForPassword]

GO

CREATE PROCEDURE [dbo].[uspGetNounAdjForPassword]
AS
BEGIN
	SET NOCOUNT ON;
		 SELECT TOP 1 Noun,Adj
		 FROM KapA_PassList
		 ORDER BY NEWID()
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCheckIfUserExists]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspCheckIfUserExists]

GO

CREATE PROCEDURE [dbo].[uspCheckIfUserExists]
@UserId VARCHAR
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT UserID FROM NurStudentInfo
   WHERE UserID=@UserId and
   UserPass='NURSING'

   SET NOCOUNT OFF;
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUserID]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetUserID]

GO

CREATE PROCEDURE [dbo].[uspGetUserID]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select UserID=MAX(UserID)
	FROM NurStudentInfo

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionIDByFacilityID]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetInstitutionIDByFacilityID]

GO

CREATE PROCEDURE [dbo].[uspGetInstitutionIDByFacilityID]
@FID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select InstitutionID
	FROM NurInstitution
	WHERE FacilityID=@FID

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLastUserByUserName]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetLastUserByUserName]

GO

CREATE PROCEDURE [dbo].[uspGetLastUserByUserName]
@UserName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT UserName FROM NurStudentInfo
   WHERE UserName like '%' + @UserName + '%'
   ORDER BY UserID desc

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetCohortIdByCohortDesc]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetCohortIdByCohortDesc]

GO

CREATE PROCEDURE [dbo].[uspGetCohortIdByCohortDesc]
@ClassCode varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CohortID
	FROM NurCohort
	WHERE CohortDescription= @ClassCode
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateEnrollmentId]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspUpdateEnrollmentId]

GO

CREATE PROCEDURE [dbo].[uspUpdateEnrollmentId]
@EnrollmentID varchar(50),
@UserID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE NurStudentInfo
	SET EnrollmentID=@EnrollmentID
	WHERE UserID=@UserID
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetUser]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetUser]

GO

CREATE PROCEDURE [dbo].[uspGetUser]
@UserId VARCHAR(100),
@UserName VARCHAR(100)
AS
IF (@UserID!='')
  BEGIN
        SELECT UserID FROM NurStudentInfo WHERE KaplanUserID = @UserId
  END
ELSE
  BEGIN
        SELECT UserName FROM NurStudentInfo
        WHERE UserName like '%' + @UserName + '%'
        AND UserPass='NURSING'
  END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateIntegratedusers]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspUpdateIntegratedusers]
GO

CREATE PROCEDURE uspUpdateIntegratedusers
 @StudentId int,
 @CohortId int
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 UPDATE NusStudentAssign
 SET CohortID=@CohortId,Access=1
 WHERE StudentID= @StudentId
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AppSettings](
	[SettingsId] [smallint] NOT NULL,
	[Value] [varchar](200) NOT NULL,
 CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED
(
	[SettingsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO
SET ANSI_PADDING OFF
GO

IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE SettingsId = 1)
BEGIN
	INSERT INTO [dbo].[AppSettings] ([SettingsId], [Value])
	VALUES (1, 'False')
END
GO

IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE SettingsId = 2)
BEGIN
	INSERT INTO [dbo].[AppSettings] ([SettingsId], [Value])
	VALUES (2, '235')
END
GO

IF NOT EXISTS(SELECT 1 FROM [dbo].[AppSettings] WHERE SettingsId = 3)
BEGIN
	INSERT INTO [dbo].[AppSettings] ([SettingsId], [Value])
	VALUES (3, '235')
END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAppSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAppSettings]
GO

CREATE PROCEDURE [dbo].[uspGetAppSettings]
AS

SELECT [SettingsId], [Value] FROM [dbo].[AppSettings]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAppSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAppSettings]
GO

CREATE PROCEDURE [dbo].[uspSaveAppSettings]
	@settingsId int,
	@value varchar(200)
AS
	UPDATE [dbo].[AppSettings]
	SET [Value] = @value
	WHERE [SettingsId] = @settingsId

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionByStudentID]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetInstitutionByStudentID]
GO

CREATE PROCEDURE uspGetInstitutionByStudentID
 @UserID INT
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

 SELECT dbo.NurStudentInfo.UserID,
		   dbo.NurInstitution.InstitutionID,
		   dbo.NurInstitution.InstitutionName
 FROM   dbo.NurCohort INNER JOIN
           dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID INNER JOIN
           dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID INNER JOIN
           dbo.NurInstitution ON dbo.NurCohort.InstitutionID = dbo.NurInstitution.InstitutionID
 WHERE (dbo.NurStudentInfo.UserID = @UserID)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteHelpfulDoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteHelpfulDoc]
GO

CREATE PROCEDURE [dbo].[uspDeleteHelpfulDoc]
	@Id INT,
	@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
SET NOCOUNT ON;

	UPDATE HelpfulDocuments
		SET [Status] = 0,
		DeletedDate = GETDATE(),
		DeletedBy = @UserId
	WHERE Id=@Id

SET NOCOUNT OFF

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchHelpfulDocs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchHelpfulDocs]
GO
CREATE PROCEDURE [dbo].[uspSearchHelpfulDocs]
	@SearchKeyword VARCHAR(200),
	@Status INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT  [Id],
		[Title],
		[Description],
		[GUID],
		[FileName],
		[Type],
		[Size],
		[Status],
		[CreatedDate],
		[CreatedBy],
		[LastName],
		[FirstName]
FROM HelpfulDocuments SD
	INNER JOIN NurAdmin A ON SD.CreatedBy = A.UserId
WHERE [Status] = @Status
	AND (@SearchKeyword = ''
	OR [Title] LIKE '%'+ @SearchKeyword + '%'
	OR [Description] LIKE '%'+ @SearchKeyword + '%'
	OR A.FirstName LIKE '%'+ @SearchKeyword + '%'
	OR A.LastName like '%'+ @SearchKeyword + '%')

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveHelpfulDocuments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveHelpfulDocuments]

GO

CREATE PROC [dbo].[uspSaveHelpfulDocuments]
(
	 @Id int OUTPUT,
	 @FileName nVarchar(100),
	 @Title nVarchar(100),
	 @Type nVarchar(50),
	 @Size float,
	 @CreatedDate DateTime,
	 @Description nVarchar(1000),
	 @Status int,
	 @GUID as nVarchar(100),
	 @CreatedBy int
)
AS
 IF ISNULL(@Id, 0) = 0
 BEGIN
  INSERT INTO HelpfulDocuments
	 ([FileName],
	 [Title],
	 [Type],
	 [Size],
	 CreatedDate,
	 [Description],
	 [Status],
	 [GUID],
	 CreatedBy)
  VALUES
	 (@FileName,
	  @Title,
	  @Type,
	  @Size,
	  @CreatedDate,
	  @Description,
	  @Status,
	  @GUID,
	  @CreatedBy)
   SET @Id = CONVERT(int, SCOPE_IDENTITY())
 END
 ELSE
 Begin
  UPDATE HelpfulDocuments
  SET [Title] = @Title,
      [Description] = @Description
  WHERE Id = @Id
 End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetHelpfulDocuments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetHelpfulDocuments]
GO

CREATE PROCEDURE [dbo].[uspGetHelpfulDocuments]
	@Id as int,
	@Guid as nVarchar(100)
AS

BEGIN
 SELECT Id,
	Title,
	[Description],
	[FileName],
	[Type],
	[Size],
	[Status],
	CreatedDate,
	[GUID],
	[CreatedBy],
	FirstName,
	LastName
 FROM  dbo.HelpfulDocuments sd
	INNER JOIN dbo.NurAdmin n
	ON  n.UserID = sd.CreatedBy
 WHERE (Id = @Id OR @Id = 0)
	AND ([GUID] = @Guid OR @Guid = '')
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetDeletedTestListForStudents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetDeletedTestListForStudents]
GO

CREATE PROCEDURE [dbo].[uspGetDeletedTestListForStudents]
(
	@InstitutionId int,
	@FirstName varchar(100),
	@LastName varchar(100),
	@UserName varchar(100),
	@TestName varchar(100),
	@ShowIncompleteOnly bit
)
AS
BEGIN
	SET NOCOUNT ON
	SELECT TOP 3001 U.UserTestID,
		S.FirstName,
		S.LastName,
		S.UserName,
		T.TestName,
		U.TestStatus,
        U.DeletedBy,
        U.DeletedDate,
		DATEADD(hour, TZ.[Hour], TestStarted) as TestStarted
	FROM dbo.NurStudentInfo S
		INNER JOIN dbo.UserTestsHistory U ON S.UserID = U.UserID
		INNER JOIN dbo.NurCohort ON U.CohortID = dbo.NurCohort.CohortID
		INNER JOIN dbo.Tests T ON U.TestID = T.TestID
		INNER JOIN dbo.NurInstitution AS I
		ON I.InstitutionId = S.InstitutionId
		INNER JOIN TimeZones TZ
		ON I.TimeZone = TZ.TimeZoneID
	WHERE
	(@InstitutionId = 0
			OR	S.InstitutionId = @InstitutionId)
	AND (@FirstName = ''
			OR	S.FirstName  like +'%'+ @FirstName + '%' )
	AND (@LastName = ''
			OR	S.LastName like +'%'+ @LastName + '%'  )
	AND (@UserName = ''
			OR	S.UserName like +'%'+ @UserName + '%'  )
	AND (@TestName = ''
			OR	T.TestName like +'%'+ @TestName + '%'  )
	AND (@ShowIncompleteOnly = 0
		OR (@ShowIncompleteOnly = 1 AND ISNULL(U.TestStatus, 0) != 1))
	SET NOCOUNT OFF
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetTestScheduleByDate' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetTestScheduleByDate]

GO

CREATE PROCEDURE [dbo].[uspGetTestScheduleByDate]
    @InstitutionId INT ,
    @CohortIds NVARCHAR(2000) ,
    @GroupIds NVARCHAR(2000) ,
    @StartDate DATETIME ,
    @EndDate DATETIME
AS
    BEGIN
        SELECT  V.InstitutionName ,
                V.CohortName ,
                V.TestName ,
                V.ProductName ,
                V.StartDate_All AS StartDate ,
                COUNT(V.UserID) AS Students
        FROM    ( SELECT    cohort_V.InstitutionName ,
                            cohort_V.CohortName ,
                            cohort_V.ProductName ,
                            Cohort_V.Type ,
                            Cohort_V.TestName ,
                            Cohort_V.StartDate ,
                            Cohort_V.EndDate ,
                            Cohort_V.TestID ,
                            Cohort_V.ProgramID ,
                            Cohort_V.UserID ,
                            dbo.NurProductDatesStudent.StartDate AS Student_StartDate ,
                            dbo.NurProductDatesStudent.EndDate AS Student_EndDate ,
                            Cohort_V.CohortID ,
                            Cohort_V.GroupID ,
                            dbo.NurProductDatesGroup.StartDate AS Group_StartDate ,
                            dbo.NurProductDatesGroup.EndDate AS Group_EndDate ,
                            Cohort_V.ProductID ,
                            COALESCE(dbo.NurProductDatesStudent.StartDate,
                                     dbo.NurProductDatesGroup.StartDate,
                                     Cohort_V.StartDate) AS StartDate_All ,
                            COALESCE(dbo.NurProductDatesStudent.EndDate,
                                     dbo.NurProductDatesGroup.EndDate,
                                     Cohort_V.EndDate) AS EndDate_All
                  FROM      dbo.NurProductDatesGroup
                            RIGHT OUTER JOIN ( SELECT   NurInstitution.InstitutionName ,
                                                        NurCohort.CohortName ,
                                                        Products.ProductName ,
                                                        dbo.NurStudentInfo.UserID ,
                                                        dbo.NurProgramProduct.Type ,
                                                        dbo.NurProgramProduct.ProductID ,
                                                        dbo.Tests.TestName ,
                                                        dbo.NurProductDatesCohort.StartDate ,
                                                        dbo.NurProductDatesCohort.EndDate ,
                                                        dbo.Tests.TestID ,
                                                        dbo.Tests.TestNumber ,
                                                        dbo.Tests.TestSubGroup ,
                                                        dbo.Tests.ProductID AS TestProductID ,
                                                        dbo.NusStudentAssign.CohortID ,
                                                        dbo.NusStudentAssign.GroupID ,
                                                        dbo.NurCohortPrograms.ProgramID
                                               FROM     dbo.NurInstitution
                                                        INNER JOIN dbo.NurCohort ON NurInstitution.InstitutionID = NurCohort.InstitutionID
                                                        INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
                                                        INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
                                                        INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
                                                        INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
                                                        INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
                                                        INNER JOIN Products ON Tests.ProductId = Products.ProductId
                                                        INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID
                                                              AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
                                                              AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
                                               WHERE    NurCohort.Institutionid = @InstitutionId
                                                        AND ( dbo.NurProgramProduct.Type = 0 )
                                                        AND ( dbo.NurCohortPrograms.Active = 1 )
                                                        AND ( @CohortIds = ''
                                                              OR NusStudentAssign.CohortID IN (
                                                              SELECT
                                                              *
                                                              FROM
                                                              dbo.funcListToTableInt(@CohortIds,
                                                              '|') )
                                                            )
                                                        AND ( @GroupIds = ''
                                                              OR NusStudentAssign.GroupID IN (
                                                              SELECT
                                                              *
                                                              FROM
                                                              dbo.funcListToTableInt(@GroupIds,
                                                              '|') )
                                                            )
                                                        AND ( dbo.Tests.ActiveTest = 1 )
                                             ) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID
                                                              AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID
                                                              AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID
                                                              AND NurProductDatesGroup.Type = 0
                            LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID
                                                              AND dbo.NurProductDatesStudent.Type = 0
                                                              AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID
                                                              AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID
                                                              AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID
                ) AS V
        WHERE   ( @StartDate IS NULL
                  OR V.StartDate_All > CONVERT(DATETIME, @StartDate, 101)
                )
                AND ( @EndDate IS NULL
                      OR DATEADD(D, 0, DATEDIFF(D, 0, V.EndDate_All)) <= CONVERT(DATETIME, @EndDate, 101)
                    )
        GROUP BY V.InstitutionName ,
                V.CohortName ,
                V.TestName ,
                V.ProductName ,
                V.StartDate_All
        UNION
        SELECT  V.InstitutionName ,
                V.CohortName ,
                V.ProductName AS TestName ,
                V.ProductName ,
                V.StartDate_All AS StartDate ,
                COUNT(V.UserID) AS Students
        FROM    ( SELECT    cohort_V.InstitutionName ,
                            cohort_V.CohortName ,
                            cohort_V.ProductName ,
                            Cohort_V.Type ,
                            Cohort_V.TestName ,
                            Cohort_V.StartDate ,
                            Cohort_V.EndDate ,
                            Cohort_V.TestID ,
                            Cohort_V.ProgramID ,
                            Cohort_V.UserID ,
                            dbo.NurProductDatesStudent.StartDate AS Student_StartDate ,
                            dbo.NurProductDatesStudent.EndDate AS Student_EndDate ,
                            Cohort_V.CohortID ,
                            Cohort_V.GroupID ,
                            dbo.NurProductDatesGroup.StartDate AS Group_StartDate ,
                            dbo.NurProductDatesGroup.EndDate AS Group_EndDate ,
                            Cohort_V.ProductID ,
                            COALESCE(dbo.NurProductDatesStudent.StartDate,
                                     dbo.NurProductDatesGroup.StartDate,
                                     Cohort_V.StartDate) AS StartDate_All ,
                            COALESCE(dbo.NurProductDatesStudent.EndDate,
                                     dbo.NurProductDatesGroup.EndDate,
                                     Cohort_V.EndDate) AS EndDate_All
                  FROM      dbo.NurProductDatesGroup
                            RIGHT OUTER JOIN ( SELECT   NurInstitution.InstitutionName ,
                                                        NurCohort.CohortName ,
                                                        Products.ProductName ,
                                                        dbo.NurStudentInfo.UserID ,
                                                        dbo.NurProgramProduct.Type ,
                                                        dbo.NurProgramProduct.ProductID ,
                                                        '' AS TestName ,
                                                        dbo.NurProductDatesCohort.StartDate ,
                                                        dbo.NurProductDatesCohort.EndDate ,
                                                        dbo.Tests.TestID ,
                                                        dbo.Tests.TestNumber ,
                                                        dbo.Tests.TestSubGroup ,
                                                        dbo.Tests.ProductID AS TestProductID ,
                                                        dbo.NusStudentAssign.CohortID ,
                                                        dbo.NusStudentAssign.GroupID ,
                                                        dbo.NurCohortPrograms.ProgramID
                                               FROM     dbo.NurInstitution
                                                        INNER JOIN dbo.NurCohort ON NurInstitution.InstitutionID = NurCohort.InstitutionID
                                                        INNER JOIN dbo.NusStudentAssign ON dbo.NurCohort.CohortID = dbo.NusStudentAssign.CohortID
                                                        INNER JOIN dbo.NurStudentInfo ON dbo.NusStudentAssign.StudentID = dbo.NurStudentInfo.UserID
                                                        INNER JOIN dbo.NurCohortPrograms ON dbo.NurCohort.CohortID = dbo.NurCohortPrograms.CohortID
                                                        INNER JOIN dbo.NurProgramProduct ON dbo.NurCohortPrograms.ProgramID = dbo.NurProgramProduct.ProgramID
                                                        INNER JOIN dbo.Tests ON dbo.NurProgramProduct.ProductID = dbo.Tests.TestID
                                                        INNER JOIN Products ON NurProgramProduct.ProductId = Products.ProductId
                                                        INNER JOIN dbo.NurProductDatesCohort ON dbo.NurCohort.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NusStudentAssign.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NurCohortPrograms.CohortID = dbo.NurProductDatesCohort.CohortID
                                                              AND dbo.NurProgramProduct.ProductID = dbo.NurProductDatesCohort.ProductID
                                                              AND dbo.Tests.TestID = dbo.NurProductDatesCohort.ProductID
                                                              AND dbo.NurProgramProduct.Type = dbo.NurProductDatesCohort.Type
                                               WHERE    NurCohort.Institutionid = @InstitutionId
                                                        AND ( dbo.NurProgramProduct.Type = 1 )
                                                        AND ( dbo.NurCohortPrograms.Active = 1 )
                                                        AND ( @CohortIds = ''
                                                              OR NusStudentAssign.CohortID IN (
                                                              SELECT
                                                              *
                                                              FROM
                                                              dbo.funcListToTableInt(@CohortIds,
                                                              '|') )
                                                            )
                                                        AND ( @GroupIds = ''
                                                              OR NusStudentAssign.GroupID IN (
                                                              SELECT
                                                              *
                                                              FROM
                                                              dbo.funcListToTableInt(@GroupIds,
                                                              '|') )
                                                            )
                                                        AND ( dbo.Tests.ActiveTest = 1 )
                                             ) AS Cohort_V ON dbo.NurProductDatesGroup.CohortID = Cohort_V.CohortID
                                                              AND dbo.NurProductDatesGroup.GroupID = Cohort_V.GroupID
                                                              AND dbo.NurProductDatesGroup.ProductID = Cohort_V.ProductID
                                                              AND NurProductDatesGroup.Type = 1
                            LEFT OUTER JOIN dbo.NurProductDatesStudent ON Cohort_V.CohortID = dbo.NurProductDatesStudent.CohortID
                                                              AND dbo.NurProductDatesStudent.Type = 1
                                                              AND Cohort_V.GroupID = dbo.NurProductDatesStudent.GroupID
                                                              AND Cohort_V.UserID = dbo.NurProductDatesStudent.StudentID
                                                              AND Cohort_V.ProductID = dbo.NurProductDatesStudent.ProductID
                ) AS V
        WHERE   ( @StartDate IS NULL
                  OR V.StartDate_All > CONVERT(DATETIME, @StartDate, 101)
                )
                AND ( @EndDate IS NULL
                      OR DATEADD(D, 0, DATEDIFF(D, 0, V.EndDate_All)) <= CONVERT(DATETIME, @EndDate, 101)
                    )
        GROUP BY V.InstitutionName ,
                V.CohortName ,
                V.TestName ,
                V.ProductName ,
                V.StartDate_All
    END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetCountries' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetCountries]

GO
/****** Object:  StoredProcedure [dbo].[uspGetCountries]    Script Date: 07/27/2011 15:26:02 ******/

CREATE PROCEDURE [dbo].[uspGetCountries]
(
 @CountryId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			CountryID,
			CountryName,
			Status
		FROM dbo.Country
		WHERE (@CountryId = 0
			  OR(CountryID = @CountryId))
	END
GO
/****** Object:  StoredProcedure [dbo].[uspGetAddress]    Script Date: 07/27/2011 15:26:02 ******/

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetAddress' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetAddress]

GO
CREATE PROCEDURE [dbo].[uspGetAddress]
(
 @AddressId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			A.AddressID,
			A.Address1,
			A.Address2,
			A.Address3,
			A.CountryID,
			A.StateName,
			A.Zip,
			C.CountryName,
			A.[Status]
		FROM dbo.[Address] A
		INNER JOIN Country C
		ON A.CountryID = C.CountryID		
		WHERE AddressID = @AddressId
			
	END
GO
/****** Object:  StoredProcedure [dbo].[uspGetStates]    Script Date: 07/27/2011 15:26:02 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetStates' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspGetStates]
GO

CREATE PROCEDURE [dbo].[uspGetStates]
(
 @CountryId INT,
 @StateId INT
)
AS
SET NOCOUNT ON;
	BEGIN
		SELECT
			StateID,
			CountryID,
			StateName,
			StateStatus
		FROM dbo.CountryState
		WHERE (@CountryId = 0
			  OR(CountryID = @CountryId))
			  AND (@StateId = 0
			  OR (StateID = @StateId))
			
	END
GO
/****** Object:  StoredProcedure [dbo].[uspSaveAddress]    Script Date: 07/27/2011 15:26:02 ******/
IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspSaveAddress' AND TYPE = 'P')
    DROP PROCEDURE [dbo].[uspSaveAddress]
GO

CREATE PROCEDURE [dbo].[uspSaveAddress]
	@AddressId INT,
	@Address1 VARCHAR(50),
	@Address2 VARCHAR(50),
	@Address3 VARCHAR(50),
	@CountryId INT,
	@StateName VARCHAR(100),
	@Zip VARCHAR(100),
	@Status SMALLINT,
	@NewAddressId INT OUTPUT
AS

SET NOCOUNT ON

BEGIN
	IF @AddressId = 0
		BEGIN
			INSERT INTO [Address]
			(
			Address1,
			Address2,
			Address3,
			CountryID,
			StateName,
			Zip,
			[Status]
			)
			VALUES
			(
			@Address1,
			@Address2,
			@Address3,
			@CountryID,
			@StateName,
			@Zip,
			@Status
			)
			
			SELECT @NewAddressId = SCOPE_IDENTITY()
		END
ELSE
		BEGIN
			UPDATE [Address]
			SET Address1 = @Address1,
			Address2	 = @Address2,
			Address3	 = @Address3,
			CountryID	 = @CountryID,
			StateName    = @StateName,
			Zip			 = @Zip,
			[Status]	 = @Status
            WHERE AddressID = @AddressId			
			SELECT @NewAddressId = @AddressId
		END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveInstitutionContact]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveInstitutionContact]

GO

CREATE PROC [dbo].[uspSaveInstitutionContact]
(
 @ContactId int OUTPUT,
 @InstitutionId int,
 @ContactType smallint,
 @Name varchar(100),
 @PhoneNumber varchar(50),
 @Email varchar(100),
 @Status int,
 @CreatedBy int,
 @CreatedDate smalldateTime,
 @DeletedBy int,
 @DeletedDate smalldateTime
)
AS
 IF ISNULL(@ContactId, 0) = 0
 BEGIN
  INSERT INTO
   dbo.InstitutionContacts
   (
     InstitutionId,
     ContactType,
     Name,
     PhoneNumber,
     Email,
     Status,
     CreatedBy,
     CreatedDate
    )
  VALUES
   (
   @InstitutionId,
   @ContactType,
   @Name,
   @PhoneNumber,
   @Email,
   @Status,
   @CreatedBy,
   @CreatedDate
   )

  SET @ContactId = CONVERT(int, SCOPE_IDENTITY())
 END
 ELSE
 Begin
  UPDATE dbo.InstitutionContacts
  SET InstitutionId = @InstitutionId,
   ContactType = @ContactType,
   Name = @Name,
   PhoneNumber = @PhoneNumber,
   Email = @Email,
   Status = @Status,
   DeletedBy = @DeletedBy,
   DeletedDate = @DeletedDate
  WHERE ContactID = @ContactId
 END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetInstitutionContacts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetInstitutionContacts]

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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateStudentsADA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspUpdateStudentsADA]

GO

CREATE PROCEDURE [dbo].[uspUpdateStudentsADA]
  @Students as varchar(2000) ,
  @Ada bit
 AS
 BEGIN
  Update  dbo.NurStudentInfo
  set ADA = @Ada
  where UserID in
        (select *
         from dbo.funcListToTableInt(@Students,'|'))
 END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAdhocGroup]

GO

CREATE PROCEDURE [dbo].[uspSaveAdhocGroup]
 @AdhocGroupID INT OUTPUT ,
 @AdhocGroupName VARCHAR(50),
 @IsADAGroup BIT,
 @ADA BIT,
 @CreatedBy INT ,
 @CreatedDate DATETIME
AS

BEGIN
 INSERT INTO [AdhocGroup]
 (
	 AdhocGroupName,
	 IsAdaGroup,
	 ADA,
	 CreatedDate,
	 CreatedBy
 )
 VALUES
 (
	 @AdhocGroupName,
	 @IsADAGroup,
	 @ADA,
	 @CreatedDate,
	 @CreatedBy
 )

 SELECT @AdhocGroupID = SCOPE_IDENTITY()
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSearchStudentForTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSearchStudentForTest]

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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroupStudent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAdhocGroupStudent]

GO
CREATE PROCEDURE [dbo].[uspSaveAdhocGroupStudent]
	@AdhocGroupID INT ,
	@StudentID INT
AS
BEGIN
 INSERT INTO [StudentAdhocGroup]
 (
	 AdhocGroupID,
	 StudentID
 )
 VALUES
 (
	 @AdhocGroupID,
	 @StudentID
 )
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspSaveAdhocGroupTestDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspSaveAdhocGroupTestDate]

GO
CREATE PROCEDURE [dbo].[uspSaveAdhocGroupTestDate]
(
@TestID INT,
@AdhocGroupID INT,
@StartDate DATETIME,
@EndDate DATETIME,
@CreatedBy INT,
@CreatedDate DATETIME
)
 AS

 BEGIN
  INSERT INTO AdhocGroupTestDetail
  (
   TestID,
   AdhocGroupID,
   StartDate,
   EndDate,
   CreatedBy,
   CreatedDate
  )
  VALUES
  (
   @TestID,
   @AdhocGroupID,
   @StartDate,
   @EndDate,
   @CreatedBy,
   @CreatedDate
  )
 END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAdhocGroupTests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetAdhocGroupTests]

GO
CREATE PROCEDURE [dbo].[uspGetAdhocGroupTests]
  @AdhocGroupId as int
AS
Begin
 SELECT
  AdhocGroupTestDetailID,
  T.TestID,
  AdhocGroupID,
  StartDate,
  EndDate,
  CreatedBy,
  CreatedDate,
  TestName
 FROM AdhocGroupTestDetail AG inner join
      Tests T on AG.TestID = T.TestId
 where AdhocGroupID = @AdhocGroupId
End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspAssignAdhocStudentTestDates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspAssignAdhocStudentTestDates]
GO
CREATE PROCEDURE [dbo].[uspAssignAdhocStudentTestDates]
(
	@CohortId INT,
	@GroupId INT,
	@TestId INT,
	@StudentId Int,
	@Type INT,
	@StartDate DATETIME,
	@EndDate DATETIME
)
AS
BEGIN
  INSERT INTO NurProductDatesStudent
   (
		CohortID,
		ProductID,
		StartDate,
		EndDate,
		UpdateDate,
		Type,
		GroupID,
		StudentID
   )
  VALUES
   (
		@CohortID,
		@TestId,
		@StartDate,
		@EndDate,
		getdate(),
		@Type,
		@GroupId,
		@StudentId
   )
END

GO

IF EXISTS (SELECT name FROM sysobjects WHERE  name = N'uspGetAdhocGroupStudentDetail' AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetAdhocGroupStudentDetail]

GO
Create PROCEDURE [dbo].[uspGetAdhocGroupStudentDetail]
  @AdhocGroupId as int
AS
Begin
 SELECT
  SAG.StudentId,
  NSA.CohortId,
  NSA.GroupId
 FROM nusstudentassign NSA inner join StudentAdhocGroup SAG
      on NSA.StudentID = SAG.StudentID
 where AdhocGroupID = @AdhocGroupId
End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetListOfQuestionsShowUnique]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetListOfQuestionsShowUnique]
GO

CREATE PROCEDURE [dbo].[uspGetListOfQuestionsShowUnique]
	@ProductID as int,
	@TestID as int,
	@ClientNeedID as int,
	@ClientNeedsCategoryID as int,
	@ClinicalConceptID as int,
	@CognitiveLevelID as int,
	@CriticalThinkingID as int,
	@DemographicID as int,
	@LevelOfDifficultyID as int,
	@NursingProcessID as int,
	@RemediationID as int,
	@SpecialtyAreaID as int,
	@SystemID as int,
	@QuestionID as varchar(100),
	@TypeOfFileID as varchar(10),
	@QuestionType as char(2),
	@Text as varchar(max),
	@ReleaseStatus as bit,
	@Active as bit
AS
BEGIN
 SET NOCOUNT ON;

if (@ProductID !=0)
 BEGIN
	SELECT
			Distinct Q.QuestionID,Q.QID,Q.QID AS QN,Q.Stem,Q.ReleaseStatus,Q.TypeOfFileID,
			T.ProductID,
			CN.ClientNeeds,
			CNC.ClientNeedCategory ,
			R.TopicTitle ,
			NP.NursingProcess,
			S.System
	FROM dbo.Tests T
	INNER JOIN dbo.TestQuestions TQ ON T.TestID = TQ.TestID
	RIGHT OUTER JOIN dbo.Questions Q ON TQ.QID = Q.QID
	LEFT OUTER JOIN dbo.ClientNeeds CN ON Q.ClientNeedsID = CN.ClientNeedsID
	LEFT OUTER JOIN dbo.ClientNeedCategory CNC ON Q.ClientNeedsCategoryID = CNC.ClientNeedCategoryID
	LEFT OUTER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID
	LEFT OUTER JOIN dbo.NursingProcess NP ON Q.NursingProcessID = NP.NursingProcessID
	LEFT OUTER JOIN dbo.Systems S ON Q.SystemID = S.SystemID
	where T.ProductID=@ProductID
		AND (@TestID =0 OR T.TestID=@TestID)
		AND (@ClientNeedID = 0 OR Q.ClientNeedsID=@ClientNeedID)
		AND (@ClientNeedsCategoryID = 0  OR Q.ClientNeedsCategoryID=@ClientNeedsCategoryID)
		AND (@ClinicalConceptID  = 0 OR Q.ClinicalConceptsID=@ClinicalConceptID)
		AND (@CognitiveLevelID = 0 OR Q.CognitiveLevelID=@CognitiveLevelID)
		AND (@CriticalThinkingID = 0 OR Q.CriticalThinkingID=@CriticalThinkingID)
		AND (@DemographicID = 0 OR Q.DemographicID=@DemographicID)
		AND (@LevelOfDifficultyID = 0 OR Q.LevelOfDifficultyID=@LevelOfDifficultyID)
		AND (@NursingProcessID = 0 OR Q.NursingProcessID=@NursingProcessID)
		AND (@RemediationID = 0 OR Q.RemediationID=@RemediationID)
		AND (@SpecialtyAreaID = 0 OR Q.SpecialtyAreaID=@SpecialtyAreaID)
		AND (@SystemID = 0 OR Q.SystemID=@SystemID)
		AND ((@QuestionID = '' OR @QuestionID is null) OR Q.QuestionID like '%' + @QuestionID + '%')
		AND ((@TypeOfFileID = '' OR @TypeOfFileID is null) OR Q.TypeOfFileID=@TypeOfFileID)
		AND ((@QuestionType ='0' OR @QuestionType is null) OR Q.QuestionType= @QuestionType)
		AND ((@Text = '' OR @Text is null) OR (Q.Stem like '%' + @Text + '%' OR Q.Stimulus like '%' + @Text + '%' OR Q.Explanation like '%' + @Text + '%' OR Q.ItemTitle like '%' + @Text + '%'))
		AND (@ReleaseStatus = 0 OR Q.ReleaseStatus IS NOT NULL)
		AND (@Active = 1 OR Q.Active = 0)
		AND (@Active = 0 OR (Q.Active IS NULL OR Q.Active = 1))
	ORDER BY T.ProductID, Q.TypeOfFileID
 END
ELSE
 BEGIN
	SELECT Q.QuestionID,Q.QID,Q.QID AS QN,Q.Stem,Q.ReleaseStatus,Q.TypeOfFileID,
	 CN.ClientNeeds, NP.NursingProcess,
	 R.TopicTitle,  CNC.ClientNeedCategory,S.System
	 FROM  dbo.Questions Q
	 LEFT OUTER JOIN dbo.ClientNeedCategory CNC ON Q.ClientNeedsCategoryID = CNC.ClientNeedCategoryID
	 LEFT OUTER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID
	 LEFT OUTER JOIN dbo.NursingProcess NP ON Q.NursingProcessID = NP.NursingProcessID
	 LEFT OUTER JOIN dbo.ClientNeeds CN ON Q.ClientNeedsID = CN.ClientNeedsID
	 LEFT OUTER JOIN dbo.Systems S ON Q.SystemID = S.SystemID
	  where
	 (@ClientNeedID = 0 OR Q.ClientNeedsID=@ClientNeedID)
	AND (@ClientNeedsCategoryID = 0  OR Q.ClientNeedsCategoryID=@ClientNeedsCategoryID)
	AND (@ClinicalConceptID  = 0 OR Q.ClinicalConceptsID=@ClinicalConceptID)
	AND (@CognitiveLevelID = 0 OR Q.CognitiveLevelID=@CognitiveLevelID)
	AND (@CriticalThinkingID = 0 OR Q.CriticalThinkingID=@CriticalThinkingID)
	AND (@DemographicID = 0 OR Q.DemographicID=@DemographicID)
	AND (@LevelOfDifficultyID = 0 OR Q.LevelOfDifficultyID=@LevelOfDifficultyID)
	AND (@NursingProcessID = 0 OR Q.NursingProcessID=@NursingProcessID)
	AND (@RemediationID = 0 OR Q.RemediationID=@RemediationID)
	AND (@SpecialtyAreaID = 0 OR Q.SpecialtyAreaID=@SpecialtyAreaID)
	AND (@SystemID = 0 OR Q.SystemID=@SystemID)
	AND ((@QuestionID = '' OR @QuestionID is null) OR Q.QuestionID like '%' + @QuestionID + '%')
	AND ((@TypeOfFileID = '' OR @TypeOfFileID is null) OR Q.TypeOfFileID=@TypeOfFileID)
	AND ((@QuestionType ='0' OR @QuestionType is null) OR Q.QuestionType= @QuestionType)
	AND ((@Text = '' OR @Text is null) OR (Q.Stem like '%' + @Text + '%' OR Q.Stimulus like '%' + @Text + '%' OR Q.Explanation like '%' + @Text + '%' OR Q.ItemTitle like '%' + @Text + '%'))
	AND (@ReleaseStatus = 0 OR Q.ReleaseStatus IS NOT NULL)
	AND (@Active = 1 OR Q.Active = 0)
	AND (@Active = 0 OR (Q.Active IS NULL OR Q.Active = 1))
	Order By QuestionID
 END

SET NOCOUNT OFF;
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationExplanations]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetRemediationExplanations]

GO

CREATE PROCEDURE [dbo].[uspGetRemediationExplanations] @RemReviewId AS INT
AS
    BEGIN
        SELECT DISTINCT  RRQ.RemReviewId ,
						 RRQ.RemReviewQuestionId,
						 R.Explanation,
						 RRQ.RemediationNumber,
						 RR.Name,
						 RRQ.RemediatedTime,
						 R.TopicTitle
        FROM    dbo.RemediationReviewQuestions RRQ
        INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId
        INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId
        WHERE RRQ.RemReviewId = @RemReviewId

    END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateReviewRemediation]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspUpdateReviewRemediation]

GO

CREATE PROCEDURE [dbo].[uspUpdateReviewRemediation]
 @RemReviewQuestionId int,
 @RemTime int
AS
BEGIN
 SET NOCOUNT ON;
  UPDATE RemediationReviewQuestions
		 SET RemediatedTime=@RemTime
  WHERE RemReviewQuestionId=@RemReviewQuestionId
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetLippincottForReviewRemediation]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetLippincottForReviewRemediation]

GO

CREATE PROCEDURE [dbo].[uspGetLippincottForReviewRemediation]
    @RemReviewQuestionId AS INT
AS
    BEGIN
        SELECT  L.LippincottTitle ,
                L.LippincottExplanation ,
                L.LippincottTitle2 ,
                L.LippincottExplanation2 ,
                L.LippincottID
        FROM    Questions Q
                INNER JOIN Lippincot L ON Q.RemediationID = L.RemediationID
                INNER JOIN dbo.RemediationReviewQuestions RRQ ON Q.RemediationID = RRQ.RemediationId
        WHERE   RRQ.RemReviewQuestionId = @RemReviewQuestionId

    END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetFRRemediations]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetFRRemediations]

GO


CREATE PROCEDURE [dbo].[uspGetFRRemediations]
    @SystemIds VARCHAR(MAX) ,
    @TopicIds VARCHAR(MAX) ,
    @StudentId INT ,
    @TestId INT ,
    @NoOfRemediations INT
AS
    BEGIN
        SET ROWCOUNT @NoOfRemediations

        SELECT DISTINCT
                Questions.RemediationID ,
                Questions.SystemID
        FROM    Questions
                INNER JOIN TestQuestions ON TestQuestions.QID = Questions.QID
        WHERE   TestQuestions.TestID = @TestId
                AND ( Questions.SystemID IN (
                        SELECT    *
                        FROM      funcListToTableInt(@SystemIds, '|') ) )
                AND ( @TopicIds = ''
                        OR Questions.RemediationID IN (
                        SELECT    *
                        FROM      funcListToTableInt(@TopicIds, '|') )
                    )

		SET ROWCOUNT 0
    END
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetAvailableRemediations]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspGetAvailableRemediations]

GO

CREATE PROCEDURE [dbo].[UspGetAvailableRemediations]
    @SystemIds VARCHAR(100) ,
    @TopicIds VARCHAR(1000) ,
    @StudentId INT ,
    @TestId INT
AS
    SET NOCOUNT ON ;

    BEGIN
        SELECT  COUNT(*)
        FROM    ( SELECT DISTINCT
                            Questions.RemediationID ,
                            Questions.SystemID
                    FROM      Questions
                            INNER JOIN TestQuestions ON TestQuestions.QID = Questions.QID
                    WHERE     TestQuestions.TestID = @TestId
                            AND ( Questions.SystemID IN (
                                    SELECT    *
                                    FROM      funcListToTableInt(@SystemIds,
                                                    '|') ) )
                            AND ( @TopicIds = ''
                                    OR Questions.RemediationID IN (
                                    SELECT    *
                                    FROM      funcListToTableInt(@TopicIds,
                                                    '|') )
                                )
                ) AS v
    END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateFRRemediations]') AND TYPE = 'P')
DROP PROCEDURE [dbo].[uspCreateFRRemediations]

GO

CREATE PROCEDURE [dbo].[uspCreateFRRemediations]
    @RemReviewId INT OUTPUT ,
    @Name NVARCHAR(500) ,
    @StudentId INT ,
    @RemediatedTime INT ,
    @CreateDate DATETIME ,
    @NoOfRemediations INT ,
    @SystemIds VARCHAR(MAX) ,
    @TopicIds VARCHAR(MAX) ,
    @TestId INT
AS
   DECLARE @RemCount AS INT ,
        @count AS INT ,
        @RId AS INT ,
        @SystemId AS INT,
        @Id AS INT

    DECLARE @PrevName AS NVARCHAR(500)
    BEGIN
        IF ( @RemReviewId = 0 )
            BEGIN

                CREATE TABLE #RemediationTbl
                    (
                      RemediationId INT IDENTITY(1, 1)
                                        NOT NULL ,
                      RID INT ,
                      SystemId INT
                    )

                INSERT  INTO #RemediationTbl
                        EXEC uspGetFRRemediations @SystemIds, @TopicIds,@StudentId, @TestId, @NoOfRemediations

                SET @Name =  @Name + '.'
                SET @Id = 1

                SELECT TOP 1 @PrevName = [name]
                FROM remediationreview
                WHERE name like '%' + @Name + '%'
                ORDER BY RemReviewId DESC

                IF(ISNULL(@PrevName,'') != '')
                  BEGIN
					SET @Id = CONVERT(int,rtrim(ltrim(Replace(@PrevName,@Name,''))))
                    SET @Id = @Id + 1
                  END

                SET @Name = @Name + CONVERT(Nvarchar(10),@Id)

                SELECT  @RemCount = COUNT(RemediationId)
                FROM    #RemediationTbl
                INSERT  INTO dbo.RemediationReview
                        ( Name ,
                          StudentId ,
                          NoOfRemediations ,
                          CreatedDate
                        )
                VALUES  ( @Name ,
                          @StudentId ,
                          @RemCount ,
                          @CreateDate
                        )

                SET @RemReviewId = SCOPE_IDENTITY()
                SET @count = 1
                WHILE ( @RemCount >= @count )
                    BEGIN
                        SELECT  @RId = RID ,
                                @SystemId = SystemId
                        FROM    #RemediationTbl
                        WHERE   RemediationId = @count

                        INSERT  INTO dbo.RemediationReviewQuestions
                                ( RemReviewId ,
                                  RemediationId ,
                                  SystemId,
                                  RemediationNumber
                                )
                        VALUES  ( @RemReviewId ,
                                  @RId ,
                                  @SystemId,
                                  @count
                                )
                        SET @count = @count + 1
                    END

            END
        ELSE
            BEGIN
                UPDATE  RemediationReview
                SET     RemediatedTime = @RemediatedTime
                WHERE   RemReviewId = @RemReviewId
            END

    END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetRemediationsForTheUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetRemediationsForTheUser]

GO

Create PROCEDURE [dbo].[uspGetRemediationsForTheUser]
 @studentId int
AS
BEGIN
 SET NOCOUNT ON;
    SELECT
	RemReviewId,
	Name,
	StudentId,
	NoOfRemediations,
	DATEADD(hour, TZ.[Hour], CreatedDate) as CreatedDate

	FROM RemediationReview AS RR
			Inner Join NurStudentInfo AS NS ON RR.StudentId = NS.UserId
			Inner Join NurInstitution AS I ON I.InstitutionId = NS.InstitutionId
			Inner Join TimeZones TZ ON I.TimeZone = TZ.TimeZoneID
	WHERE StudentId=@studentId
	AND isnull(RR.status,0)=0

SET NOCOUNT Off;
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDeleteRemediationReview]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspDeleteRemediationReview]

GO

CREATE PROCEDURE [dbo].[uspDeleteRemediationReview]
 @RemediationReviewId int
AS
BEGIN
 UPDATE RemediationReview
		SET status = 1
 WHERE RemReviewId = @RemediationReviewId
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetPrevNextRemediations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetPrevNextRemediations]
GO

CREATE PROCEDURE [dbo].[uspGetPrevNextRemediations]
    @RemReviewId INT ,
    @RemediationNumber INT ,
    @Action CHAR(1)
AS
    BEGIN
        SET NOCOUNT ON ;
        IF ( @Action = 'N' )
            BEGIN
                SELECT TOP 1
                        RRQ.RemReviewQuestionId ,
                        RRQ.RemediationNumber ,
                        R.Explanation ,
                        RRQ.RemediatedTime ,
                        RR.Name
                FROM    dbo.RemediationReviewQuestions RRQ
                        INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId
                        INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId
                WHERE   RRQ.RemReviewId = @RemReviewId
                        AND RemediationNumber > @RemediationNumber
                ORDER BY RemediationNumber
            END
        ELSE
            IF ( @Action = 'P' )
                BEGIN
                    SELECT TOP 1
                            RRQ.RemReviewQuestionId ,
                            RRQ.RemediationNumber ,
                            R.Explanation ,
                            RRQ.RemediatedTime ,
                            RR.Name
                    FROM    dbo.RemediationReviewQuestions RRQ
                            INNER JOIN dbo.Remediation R ON R.RemediationID = RRQ.RemediationId
                            INNER JOIN dbo.RemediationReview RR ON RR.RemReviewId = RRQ.RemReviewId
                    WHERE   RRQ.RemReviewId = @RemReviewId
                            AND RemediationNumber < @RemediationNumber
                    ORDER BY RemediationNumber DESC
                END

        SET NOCOUNT OFF ;
    END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspUpdateReviewRemediationTotalTime]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspUpdateReviewRemediationTotalTime]
GO

CREATE PROCEDURE [dbo].[uspUpdateReviewRemediationTotalTime]
 @RemReviewId int
AS
BEGIN
 SET NOCOUNT ON;
   Declare @TotalTime as int

  select  @TotalTime = sum(RemediatedTime)
  from dbo.RemediationReviewQuestions
  where RemReviewId = @RemReviewId
	
  UPDATE RemediationReview
		 SET RemediatedTime=@TotalTime
  WHERE RemReviewId=@RemReviewId
  SET NOCOUNT OFF
END


GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCreateFRRemediationReview]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspCreateFRRemediationReview]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetTopicForSystem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetTopicForSystem]
GO

CREATE PROCEDURE [dbo].[uspGetTopicForSystem]
    @SystemsID VARCHAR(100)
AS
    BEGIN
        SELECT  DISTINCT R.RemediationID ,
								  R.TopicTitle
        FROM    Questions Q
                INNER JOIN dbo.Remediation R ON Q.RemediationID = R.RemediationID
        WHERE   Q.SystemID IN (
                SELECT  value
                FROM    dbo.funcListToTableInt(@SystemsID, '|') )
                AND R.TopicTitle <> ''
                AND R.TopicTitle IS NOT NULL
		ORDER BY R.TopicTitle
    END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetSystems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[uspGetSystems]
GO

CREATE PROCEDURE [dbo].[uspGetSystems]
AS
BEGIN
	select SystemID,System from Systems
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='qId_levelId_index')
CREATE NONCLUSTERED INDEX [qId_levelId_index] ON [dbo].[Questions]
(
  [QID] ASC
)
INCLUDE ( [LevelOfDifficultyID]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='qid_index')
CREATE INDEX qid_index ON UserQuestions(QID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='levelofDifficulty_index')
CREATE INDEX levelofDifficulty_index ON Questions(LevelOfDifficultyID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='testId_index')
CREATE INDEX testId_index ON CategoryPercentage(TestID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='chartId_index')
CREATE INDEX chartId_index ON Norm(ChartID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='testID_index')
CREATE INDEX testID_index ON Norm(TestID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='testCompleted_index')
CREATE INDEX testCompleted_index ON UserTests(TestComplited)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='teststarted_index')
CREATE INDEX teststarted_index ON UserTests(TestStarted)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='nursingProcessId_index')
CREATE INDEX nursingProcessId_index ON Questions(NursingProcessID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='clinicalId_index ')
CREATE INDEX clinicalId_index ON Questions(ClinicalConceptsID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='clientNeedsId_index ')
CREATE INDEX clientNeedsId_index ON Questions (ClientNeedsID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='clientNeedCategoryId_index ')
CREATE INDEX clientNeedCategoryId_index ON Questions (ClientNeedsCategoryID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='demographicId_index ')
CREATE INDEX demographicId_index ON Questions(DemographicID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='CognitiveLevelId_index ')
CREATE INDEX CognitiveLevelId_index ON Questions(CognitiveLevelID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='criticalThinkingId_index ')
CREATE INDEX criticalThinkingId_index ON Questions(CriticalThinkingID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='specialtyAreaId_index ')
CREATE INDEX specialtyAreaId_index ON Questions (SpecialtyAreaID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name ='systemsId_index ')
CREATE INDEX systemsId_index ON Questions (SystemID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'institutionId_index')
CREATE INDEX institutionId_index ON UserTests(InsitutionID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'userId_index')
CREATE INDEX userId_index ON UserTests(UserID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'testId_index')
CREATE INDEX testId_index ON UserTests(TestID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'usertestId_index')
CREATE INDEX usertestId_index ON UserQuestions(UserTestId)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'productId_index')
CREATE INDEX productId_index ON Tests(ProductID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON UserTests(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON NusStudentAssign(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'groupId_index')
CREATE INDEX groupId_index ON NusStudentAssign(GroupID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON NurGroup(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'productId_index')
CREATE INDEX productId_index ON NurProgramProduct(ProductID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'programId_index')
CREATE INDEX programId_index ON NurProgramProduct(ProgramID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON NurProductDatesCohort(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON NurCohortPrograms(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'institutionId_index')
CREATE INDEX institutionId_index ON NurCohort(InstitutionID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'addressId_index')
CREATE INDEX addressId_index ON NurInstitution(AddressId)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'cohortId_index')
CREATE INDEX cohortId_index ON NusStudentAssign(CohortID)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'startDate_index')
CREATE INDEX startDate_index ON NurProductDatesCohort(StartDate)
GO

IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'endDate_index')
CREATE INDEX endDate_index ON NurProductDatesCohort(EndDate)
GO


IF NOT EXISTS (SELECT name FROM sys.indexes
         WHERE name = 'testStatus_index')
CREATE INDEX testStatus_index ON UserTests (TestStatus)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserTests]') AND name = N'UserTests_CohortID_TestStatus')
CREATE NONCLUSTERED INDEX [UserTests_CohortID_TestStatus] ON [dbo].[UserTests]
(
      [CohortID] ASC,
      [TestStatus] ASC
)
INCLUDE ( [UserTestID],
[UserID],
[TestID]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserTests]') AND name = N'UserTests_TestStatus')
CREATE NONCLUSTERED INDEX [UserTests_TestStatus] ON [dbo].[UserTests]
(
 [TestStatus] ASC
)
INCLUDE ( [UserTestID],
[UserID],
[TestID],
[CohortID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesGroup]') AND name = N'idx_NPDG_Cohort_Group_Product')
CREATE NONCLUSTERED INDEX [idx_NPDG_Cohort_Group_Product] ON [dbo].[NurProductDatesGroup]
(
 [CohortID] ASC,
 [GroupID] ASC,
 [ProductID] ASC
)
INCLUDE ( [StartDate],
[EndDate]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NusStudentAssign]') AND name = N'ncls_NurStudentAssign_CohortID')
CREATE NONCLUSTERED INDEX [ncls_NurStudentAssign_CohortID] ON [dbo].[NusStudentAssign]
(
 [CohortID] ASC,
 [GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProgramProduct]') AND name = N'ncls_NurProgramProduct_CohortID')
CREATE NONCLUSTERED INDEX [ncls_NurProgramProduct_CohortID] ON [dbo].[NurProgramProduct]
(
 [ProgramID] ASC,
 [ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesCohort]') AND name = N'ncls_NurProductDatesCohort_ProductID')
CREATE NONCLUSTERED INDEX [ncls_NurProductDatesCohort_ProductID] ON [dbo].[NurProductDatesCohort]
(
 [CohortID] ASC,
 [ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesStudent]') AND name = N'ncls_NurProductDatesStudent_Dates')
CREATE NONCLUSTERED INDEX [ncls_NurProductDatesStudent_Dates] ON [dbo].[NurProductDatesStudent]
(
 [StartDate] ASC,
 [EndDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesGroup]') AND name = N'ncls_NurProductDatesGroup_Dates')
CREATE NONCLUSTERED INDEX [ncls_NurProductDatesGroup_Dates] ON [dbo].[NurProductDatesGroup]
(
 [StartDate] ASC,
 [EndDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesCohort]') AND name = N'ncls_NurProductDatesCohort_Dates')
CREATE NONCLUSTERED INDEX [ncls_NurProductDatesCohort_Dates] ON [dbo].[NurProductDatesCohort]
(
 [StartDate] ASC,
 [EndDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NurProductDatesStudent]') AND name = N'ncls_NurProductDatesStudent_ID')
CREATE NONCLUSTERED INDEX [ncls_NurProductDatesStudent_ID] ON [dbo].[NurProductDatesStudent]
(
 [CohortID] ASC,
 [GroupID] ASC,
 [StudentID] ASC,
 [ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO