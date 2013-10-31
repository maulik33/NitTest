using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NursingRNWeb
{
    public class ItemSelectedEventArgs : EventArgs
    {
        public string SelectedText { get; set; }

        public string SelectedValue { get; set; }

        public int ProgramofStudyId { get; set; }
    }
}