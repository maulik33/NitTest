using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class UploadQuestionError
    {
        public string ErrorMessage { get; set; }

        public string FileName { get; set; }

        public string QuestionId { get; set; }
    }
}
