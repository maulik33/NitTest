using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IQuestionRepository
    {
        UserTest CreateTest(int userId, int productId, int programId, int timedTest, int testId);

        UserTest CreateQBankTest(int userId, int productId, int programId, int timedTest, int testId,
            int tutorMode, int reuseMode, int numberOfQuestions, int correct, string options);

        IEnumerable<Question> GetQuestionByTest(int testId);

        IEnumerable<Question> GetQuestionsByUserTest(int userTestId);

        IEnumerable<Lippincott> GetLippincottAssignedInQuestion(int questionId);

        IEnumerable<UserAnswer> GetAnswersForQuestion(int userTestId);

        QuestionExhibit GetQuestionExhibitByID(int questionId);

        IEnumerable<UserAnswer> GetUserAnswerByID(int questionId);

        IEnumerable<UserAnswer> GetHotSpotAnswerByID(int questionId);

        IEnumerable<Question> GetPrevNextQuestions(int userTestId, int questionId, string typeOfFileId, bool inCorrectOnly);

        IEnumerable<Question> GetPrevNextQuestionInTest(int testId, int questionId, string typeOfFileId);

        bool IsTest74Question(int questionId);

        void UpdateQuestionExplanation(int questionId, int userTestId, int timerValue);

        void UpdateQuestionRemediation(int questionId, int userTestId, int timerValue);

        int UpdateUserTest(Question obj, UserTest objTest);

        int CreateUserAnswers(Question obj, IList<UserAnswer> list);

        int UpdateUserQuestions(Question obj);

        UserTest CreateFRQBankTest(int userId, int productId, int programId, int timedTest, int testId, int tutorMode, int reUseMode, int numberOfQuestions, int correctAnswers, string systemIds, string topicIds, string name, int programofstudyId);

        int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode, int programofstudyId);

        UserTest CreateSkillsModuleTest(int userId, int productId, int programId, int timedTest, int tutorMode, int reUseMode, int skillModuleId);
    }
}
