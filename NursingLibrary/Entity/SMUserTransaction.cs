using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class SMUserTransaction
    {
        public int SMUserId { get; set; }

        public int UserId { get; set; }

        public int SkillsModuleId { get; set; }

        public bool Status { get; set; }

        public int Count { get; set; }
    }
}
