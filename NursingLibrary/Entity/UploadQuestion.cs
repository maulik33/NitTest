using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
   public class UploadQuestionDetails
    {
        public bool IsValid { get; set; }

        public List<string> ErrorMessage { get; set; }

        public string FileName { get; set; }

        public Question Question { get; set; }

        public List<AnswerChoice> Answers { get; set; }
    }
}
