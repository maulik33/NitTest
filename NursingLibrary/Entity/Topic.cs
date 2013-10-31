using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Topic
    {
        public string TopicTitle { get; set; }

        public int RemediationId { get; set; }
    }
}
