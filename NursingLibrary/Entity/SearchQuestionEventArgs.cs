using System;

namespace NursingLibrary.Entity
{
    public class SearchQuestionEventArgs : EventArgs
    {
        public QuestionCriteria SearchCriteria { get; set; }

        public string UrlQuery { get; set; }
    }
}
