using System.Collections.Generic;

namespace NursingLibrary.Presenters
{
    public enum PageDirectory
    {
        StudentLogin,
        Error,
        StudentHome,
        TestReview,
        ListReview,
        Qbank,
        Nclex,
        CaseStudies,
        KaplanReceiveCustomer,
        Review,
        Analysis,
        Resume,
        AccessDenied,
        IntroError,
        QbankR,
        QbankP,
        ReportCohortTestByQuestion,
        KaplanInstanceError,
        KaplanUserError,
        FRRemediation,
        FRQBankR,
        FRIntroRemediation,
        FRQBank,
        SkillsModules
    }

    public enum AdminPageDirectory
    {
        AdminLogin,
        ProgramView,
        ProgramEdit,
        ProgramList,
        ProgramAddAssign,
        AVPItems,
        GroupView,
        GroupEdit,
        GroupAdd,
        StudentListForGroup,
        GroupTestDates,
        GroupList,
        UserView,
        UserEdit,
        UserAdd,
        UserTestDates,
        UserList,
        NewAVPItems,
        InstitutionEdit,
        InstitutionList,
        InstitutionView,
        Lippincott,
        ReadLippincottTemplate,
        NewLippincott,
        Error,
        ReportStudentQuestion,
        ReportCohortTestByQuestion,
        ReportInstitutionPerformance,
        CohortView,
        CohortEdit,
        CohortList,
        CohortProgramAssign,
        StudentsInCohort,
        CohortTestDates,
        SearchQuestion,
        ViewRemediation,
        EditQuestion,
        EditR,
        ReportCohortResultByModule,
        ReportTestStudent,
        ReportCohortPerformance,
        AdminList,
        AdminView,
        AdminEdit,
        AssignInstitute,
        CustomTest,
        NewCustomTest,
        CopyCustomTest,
        TestCategories,
        ViewQuestion,
        AdminHome,
        ReleaseReview,
        BackupData,
        ComponentAsset,
        EmailEdit,
        EmailReceiver,
        SearchHelpfulDocuments,
        OpenHelpfulDocuments,
        ViewHelpfulDocument,
        UploadHelpfulDocument,
        AssignStudentTest,
        AdhocGroup,
        UploadQuestions,
        LtiProviders,
    }

    public enum Action
    {
        Undefined,
        Resume,
        QBankCreate,
        Remediation,
        Review,
        ContinueTest,
        NewTest,
        Rejoin
    }

    public enum Status
    {
        InActive,
        Active
    }

    public static class SessionKeys
    {
        public const string LoggedInStudent = "LoggedInStudent";
        public const string TRACE_TOKEN = "TT";
        public const string EXECUTION_CONTEXT = "EC";
    }
}
