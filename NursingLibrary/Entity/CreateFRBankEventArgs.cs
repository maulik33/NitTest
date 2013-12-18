using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class FRBankEventArgs : EventArgs
    {
        public string Systems { get; set; }

        public string Topics { get; set; }

        public string QuestionCount { get; set; }

        public string SystemName { get; set; }
    }
}
