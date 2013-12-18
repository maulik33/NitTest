using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentFRQBankView
    {
        int TutorMode { get; }

        int ReuseMode { get; }

        int Correct { get; set; }

        string CategoryIds { get; set; }

        string TopicIds { get; set; }

        string SystemIds { get; set; }

        string SystemName { get; set; }

        void PopulateSystems(IEnumerable<Systems> systems);

        void PopulateTopics(IEnumerable<Topic> topics);

        void DisplayAvailableQuestions(int count);
        }
}
