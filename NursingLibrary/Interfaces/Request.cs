using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public class Request
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int TimeOffSet { get; set; }

        public int TestSubGroup { get; set; }

        public int Type { get; set; }

        public int TestId { get; set; }

        public int UserTestId { get; set; }

        public int ProgramId { get; set; }

        public int TimedTest { get; set; }

        public int QuestionId { get; set; }

        public int TimerValue { get; set; }

        public int ChartType { get; set; }

        public int CorrectAnswers { get; set; }

        public int OverViewOrDetails { get; set; }

        public string TypeOfFileId { get; set; }

        public string UserType { get; set; }

        public int TutorMode { get; set; }

        public int ReUseMode { get; set; }

        public int NumberOfQuestions { get; set; }

        public string Options { get; set; }

        public bool InCorrectOnly { get; set; }

        public string SystemIds { get; set; }

        public string TopicIds { get; set; }

        public string Name { get; set; }

        public int QbankProgramofStudyId { get; set; }

        public int ProgramofStudyId { get; set; }
    }
}
