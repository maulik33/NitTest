using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IQuestionView
    {
        string URLQuery { get; set; }

        string TestId { get; set; }

        string VType { get; set; }

        int ProgramOfStudyId { get; set; }

        void ShowSearchQuestionResults(IEnumerable<QuestionResult> searchQuestionResults, SortInfo sortMetaData);

        void ShowSearchRemediationResults(IEnumerable<Remediation> searchRemediationResults);

        void PopulateQuestion(Question question, int mode);

        void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories);

        void PopulateAnswers(IEnumerable<AnswerChoice> answers);

        void PopulateTests(IEnumerable<Test> tests);

        // void PopulateQuestionType(Dictionary<string, string> questionType);

        // void PopulateFileType(Dictionary<string, string> fileType);
        void RefreshPage(Question question, UserAction action, Dictionary<string, string> fileType, Dictionary<string, string> questionType, string mode, string testId, bool hasDeletePermission, bool hasAddPermission);

        void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categories, int programofStudy);

        void ShowErrorMessage(string errorMsg);

        void PopulateAlternateTextDetails(Question question, UserAction actionType);

        void DisplayUploadedQuestions(IEnumerable<UploadQuestionDetails> uploadedQuestions, int FileType, string UnZippedFolderPath);

        void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy);
    }
}
