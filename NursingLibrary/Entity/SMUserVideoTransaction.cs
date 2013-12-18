using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class SMUserVideoTransaction
    {
        public int SMUserVideoId { get; set; }

        public int SMUserId { get; set; }

        public int SkillsModuleId { get; set; }

        public bool IsPageFullyViewed { get; set; }

        public int SMOrder { get; set; }

        public int SMCount { get; set; }

        public bool IsVideoFullyViewed { get; set; }

        public SkillsModuleVideos SkillsModuleVideo { get; set; }
    }
}
