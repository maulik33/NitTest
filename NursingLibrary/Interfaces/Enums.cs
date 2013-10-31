namespace NursingLibrary.Interfaces
{
    public enum LoginFailure
    {
        AuthenticationFailed,
        UserDetailsNotComplete,
        InvalidCohortId,
        InvalidGroupId,
        SystemFailure
    }

    public enum TestType : byte
    {
        Undefined,
        Integrated,
        FocusedReview,
        Ada,
        Nclex,
        Qbank,
        TimedQbank,
        Diagnostic,
        DiagnosticResult,
        Readiness,
        ReadinessResult,
        Quiz,
        FRRemediation,
        SkillsModules
    }

    public enum ProductType : int
    {
        IntegratedTesting = 1,
        FocusedReviewTests = 3,
        NCLEXRNPrep = 4,
        SkillsModules = 6
    }

    public enum CategoryName
    {
        ClientNeeds = 1,
        NursingProcess = 2,
        CriticalThinking = 3,
        ClinicalConcept = 4,
        Demographic = 5,
        CognitiveLevel = 6,
        SpecialtyArea = 7,
        Systems = 8,
        LevelOfDifficulty = 9,
        ClientNeedCategory = 10,
        AccreditationCategories = 11,
        QSENKSACompetencies = 12,
        Concepts =23,
        PNClientNeeds = 13,
        PNNursingProcess = 14,
        PNCriticalThinking = 15,
        PNClinicalConcept = 16,
        PNDemographic = 17,
        PNCognitiveLevel = 18,
        PNSpecialtyArea = 19,
        PNSystems = 20,
        PNLevelOfDifficulty = 21,
        PNClientNeedCategory = 22,
        PNConcepts =24,
    }

    public enum TestTiming
    {
        Timed,
        Untimed
    }

    public enum TestReviewOptions
    {
        Remediations,
        Explanations
    }

    public enum QuestionType
    {
        Unknown,
        Item = 0,
        MultiChoiceSingleAnswer = 1,
        MultiChoiceMultiAnswer = 2,
        Hotspot = 3,
        Number = 4,
        Order = 5
    }

    public enum QuestionFileType
    {
        Unknown,
        Intro,
        TutorialItem,
        Question,
        EndItem,
        Disclaimer
    }

    public enum QuestionPointer
    {
        Current,
        Previous,
        Next,
        Unknown
    }

    public enum TraceType
    {
        Init = 1,
        Event = 2,
        End = 3,
        Error = 4
    }

    public enum UserType
    {
        // Default option is not to be used. Since in DB, Super Admin is assigned 0, I'm adding for backward compatability.
        // While Saving and retriving back from DB, SecurityLevel 0 is to be assigned as SuperAdmin (4).
        AcademicAdmin = 1,
        LocalAdmin = 2,
        TechAdmin = 3,
        SuperAdmin = 4,
        InstitutionalAdmin = 5,
        Student = 6
    }

    public enum UserAction
    {
        View,
        Add,
        Edit,
        Delete,
        AccessDatesEdit,
        AssisgnStudents,
        EditTestDates,
        AssignProgram,
        AssignStudents,
        AssignToCohort,
        AssignToGroup,
        AssignTests,
        InstitutionResults,
        CohortResults,
        GroupResults,
        StudentResults,
        KaplanReport,
        Copy
    }

    public enum Module
    {
        Institution,
        Cohort,
        Group,
        Program,
        Student,
        UserManagement,
        CMS,
        Reports,
        Others
    }

    public enum ViewMode
    {
        List,
        Edit,
        View
    }

    public enum DropdownType
    {
        Institution,
        Product,
        Cohort,
        Test,
        Group,
        Student
    }

    public enum ReportAction
    {
        ShowPreview = 0,
        PDFPrint = 1,
        ExportExcel = 2,
        ExportExcelDataOnly = 3,
        DirectPrint = 4,
        PrintInterface = 5,
        ShowInterface = 6,
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public enum AuthenticationRequest
    {
        Success,
        InValidUser,
        NoInstitution
    }

    public enum AppSettings
    {
        TraceEnabled = 1,
        DefaultAddressCountry = 2,
        CountryWithStates = 3,
        CFRTestIdOffset = 4,
        EnvironmentId = 5,
        PsychiatricCategoryId = 6,
        ManagementOfCareCategoryId = 7,
        PNPsychiatricCategoryId =8,
        PNManagementOfCareCategoryId=9,
    }

    public enum ContactType
    {
        Administrative = 1,
        Academic = 2,
        Finance = 3,
        Technical = 4,
    }

    public enum LookupType
    {
        CustomizedFRSystemCategory = 1,
        CustomizedFRPsychiatricCategory = 2,
        CustomizedFRManagementofCareCategory = 3,
        PNCustomizedFRSystemCategory = 4,
        PNCustomizedFRPsychiatricCategory = 5,
        PNCustomizedFRManagementofCareCategory = 6,
        CustomizedFRTestsCovered = 10,
        PNCustomizedFRTestsCovered = 11,
        CustomizedFRTopics = 15,
        Lookup123TopicMapping = 17,
        Type17QuestionMappingforRemediation = 18,
        Type17QuestionMappingforTests = 19,
        Lookup456TopicMapping = 20,
        Type20QuestionMappingforRemediation = 21,
        Type20QuestionMappingforTests = 22
    }

    public enum EntityAction : byte
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        List = 4,
        Search = 5,
        Deactivate = 6,
        None = 254,
        Unknown = 255
    }

    public enum OpenDocumentType
    {
        UploadQuestionInstruction = 1,
        UploadTopicInstruction = 2
    }

    public enum ViewDirection
    {
        Next,
        Back,
        Current
    }

    public enum SMType
    {
        Text = 1,
        Video = 2,
    }

    public enum SMTextPosition
    {
        Up = 0,
        Down = 1
    }

    public enum LoginContents
    {
        Admin = 1,
        Student = 2
    }

    public enum ProgramofStudyType
    {
        Both = 0,
        RN = 1,
        PN = 2,
        None = 3
    }

    public enum AssetLocationType
    {
        Left = 1,
        Right = 2
    }

    public enum AssetGroupType
    {
        DashboardRN = 1,
        IntegratedtestingRN = 2,
        FocussedReviewRN = 3,
        NClexPrepRN = 4,
        EssentialNursingSkillsRN = 5,
        CaseStudiesRn = 6,
        DashboardPN = 7,
        IntegratedtestingPN = 8,
        FocussedReviewPN = 9,
        NClexPrepPN = 10,
        EssentialNursingSkillsPN = 11
    }

    public enum BundleType : int
    {
        RN = 1,
        PN = 2
    }
}

