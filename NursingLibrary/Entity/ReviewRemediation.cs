using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class ReviewRemediation
    {
        public int ReviewRemId { get; set; }

        public string ReviewRemName { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int TotalRemTime { get; set; }

        public int NoOfRemediations { get; set; }

        public int RemReviewQuestionId { get; set; }

        public int RemediationId { get; set; }

        public int SystemId { get; set; }

        public int RemediatedTime { get; set; }

        public string ReviewExplanation { get; set; }

        public int RemediationNumber { get; set; }

        public string TopicTitle { get; set; }
    }
}