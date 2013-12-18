using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Utilities
{
    public static class QuestionHelper
    {
        public static Dictionary<string, string> GetQuestionTypes()
        {
            Dictionary<string, string> _questionType = new Dictionary<string, string>();
            ////_questionType.Add("-1", "Not Selected");
            _questionType.Add("01", "Multiple -choice,single-best-answer");
            _questionType.Add("02", "Multiple-choice,multi-select");
            _questionType.Add("03", "HotSpot");
            _questionType.Add("04", "Numerical Fill-In");
            _questionType.Add("05", "Order-Match (Drag & Drop)");
            _questionType.Add("00", "Item");
            return _questionType;
        }

        public static Dictionary<string, string> GetFileTypes()
        {
            Dictionary<string, string> _fileType = new Dictionary<string, string>();
            //// key fields is not added in sequence to keep sync with old code.
            _fileType.Add("03", "Question");
            _fileType.Add("02", "Tutorial Item");
            _fileType.Add("01", "Intro");
            _fileType.Add("04", "End Item");
            _fileType.Add("05", "Disclaimer");
            return _fileType;
        }
    }
}
