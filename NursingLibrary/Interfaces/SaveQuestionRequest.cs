using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public class SaveQuestionRequest
    {
        public Question Question { get; set; }

        public IList<UserAnswer> UserAnswers { get; set; }

        public UserTest UserTest { get; set; }
    }
}
