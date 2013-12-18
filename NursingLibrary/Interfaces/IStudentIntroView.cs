using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentIntroView
    {
        int TabIndex { get; set; }

        bool CheckIsQBankQuestion { get; }

        string TestName { set; }

        string Remaining { set; }

        bool EnableLabel { set; }

        bool SetTabVisibility { set; }

        bool SetTabAlternateVisibility { set; }

        string TestType { set; }

        string ADA { set; }

        string UserHostAddress { get; }

        string HTTP_X_FORWARDED_FOR { get; }

        UserTest UserTest { get; }

        string QuestionTypeText { get; }

        int CorrectQuestion { get; }

        string QuestionId { get; }

        string TimerCount { get; }

        string Timerup { get; }

        string AnswerTrack { get; }

        string RequireResponse { get; }

        string A1 { get; }

        bool Resume { get; set; }

        string TimedTestQB { get; set; }

        bool Postback { get; }

        int Timer { get; }

        string FileType { get; }

        string QuestionNumber { get; }

        bool IsNextVisible { get; }

        bool CheckIsFocused { get; set; }

        bool NextInCorrectButton { set; }

        string RemediationHtml { get; set; }

        string SecondPerQuestion { set; }

        string BrowserType { get; }

        void SetControls();

        void PopulateFields(Question question);

        void PopulateEndForAllPages();

        void PopulateEnd(Question question, bool IsLastQuestion);

        void PopulateQuestions(Question question, IEnumerable<Lippincott> lippincotts, bool IsFirstUserQuestion);

        void PopulateRemediation(Question question, bool IsFirstUserQuestion);

        void PopulateDisclamer(Question question);

        void PopulateTutorial(Question question, bool IsFirstQuestion);

        void PopulateIntroduction(Question question, bool IsLastQuestion);

        void ShowLippincott(IEnumerable<Lippincott> lippincotts, string remediationHtml);

        void SetAnswerTrack(Question userQuestions, bool AnswersForQuestionExists);

        void FillMultipleChoiceFields(IEnumerable<UserAnswer> answers, QuestionExhibit questionExhibit, IEnumerable<UserAnswer> userAnswers);

        void FillTheBlankFields(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers);

        void FillTheMatchFields(IEnumerable<UserAnswer> userAnswers, Question userQuestion);

        void FillMultipleChoiceMultiSelectFields(IEnumerable<UserAnswer> answers, QuestionExhibit questionExhibit, IEnumerable<UserAnswer> userAnswers);

        void FillHotSpotFields(IEnumerable<UserAnswer> userAnswers, Question userQuestion, IEnumerable<UserAnswer> hotSpotAnswers);

        void HideShowPreviousIncorrectButton(bool show);

        void ShowMessageCtrl(bool show);

        IList<UserAnswer> PopulateMultipleChoice();

        IList<UserAnswer> PopulateMultipleChoiceMultiSelect();

        IList<UserAnswer> PopulateFillIn();

        void SetRemediationCtrl(int userTestId, string action);

        void SetQBankCreateCtrl(UserTest suspendedTest);

        void SetRejoinResumeCtrl(UserTest suspendedTest);

        void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answersById, IEnumerable<UserAnswer> answers, Student student);

        void PopulateAlternateTextDetails(Question question);

        void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, Question question, IEnumerable<UserAnswer> hotSpotAnswers, Student student);

        void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, Question question, Student student);

        bool IsProctorTrackEnabled { get;}
 
    }
}