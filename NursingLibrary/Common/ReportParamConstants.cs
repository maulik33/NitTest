using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Common
{
    public enum ParamRefreshType
    {
        None,
        Clear,
        RefreshData
    }

    public class ReportParamConstants
    {
        public const string PARAM_INSTITUTION = "Institution";
        public const string PARAM_COHORT = "Cohort";
        public const string PARAM_TESTTYPE = "TestType";
        public const string PARAM_TEST = "Test";
        public const string PARAM_GROUP = "Group";
        public const string PARAM_STUDENT = "Student";
        public const string PARAM_CASE = "Case";
        public const string PARAM_MODULE = "Module";
        public const string PARAM_CATEGORY = "Category";
        public const string PARAM_SUB_CATEGORY = "SubCategory";
        public const string PARAM_TOPIC_TITLE = "TopicTitle";
        public const string PARAM_CLIENTNEEDS = "ClientNeeds";
        public const string PARAM_CLIENTNEEDS_CATEGORY = "ClientNeedsCategory";
        public const string PARAM_NURSING_PROCESS = "NursingProcess";
        public const string PARAM_LEVEL_OF_DIFFICULTY = "LevelOfDifficulty";
        public const string PARAM_DEMOGRAPHY = "Demography";
        public const string PARAM_COGNITIVE_LEVEL = "CognitiveLevel";
        public const string PARAM_SPECIALIT_AREA = "SpecialitArea";
        public const string PARAM_SYSTEM = "System";
        public const string PARAM_CRITICAL_THINKING = "CriticalThinking";
        public const string PARAM_CLINICAL_CONCEPTS = "ClinicalConcepts";
        public const string PARAM_CUSTOM_EMAILS = "CustomEmails";
        public const string PARAM_QID = "Qid";
        public const string PARAM_PROGRAM_OF_Study = "ProgramofStudy";
        public const string PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES = "ProgramOfStudyForTestsAndCategories";
    }
}
