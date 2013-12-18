using NursingLibrary.Business;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class Test1
    {
        public int TestId { get; set; }
        public TestType TestType { get; set; }
        public TestTiming TestTime { get; set; }
        public TestReviewOptions TestReviewOption { get; set; }
        public bool AdaOption { get; set; }
        public int QuestionCount { get; set; }
        public bool Scramble { get; set; }

        public Test1() { }

        public Test1(int testId, bool adaOption)
        {
            TestId = testId;
            
            // get test type
            TestType = Core.GetTestType(testId);

            switch(TestType)
            {
                case TestType.Integrated:
                    TestTime = TestTiming.Timed;
                    TestReviewOption = TestReviewOptions.Remediations;
                    break;
                case TestType.Nclex:
                    TestTime = TestTiming.Untimed;
                    TestReviewOption = TestReviewOptions.Explanations;
                    break;
                case TestType.FocusedReview:
                    TestTime = TestTiming.Untimed;
                    TestReviewOption = TestReviewOptions.Explanations;
                    break;
            }

            // retrieve the current number of questions for this test
            QuestionCount = Core.GetTestQuestionCount(testId);
        }
    }

    public class SimpleTest
    {
        public string TestName { get; private set; }
        public int UserTestId { get; private set; }

        public SimpleTest(string testName, int userTestId)
        {
            TestName = testName;
            UserTestId = userTestId;
        }
    }
}
