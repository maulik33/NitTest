using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class SkillsModuleVideos
    {
        public int SMVideoId { get; set; }

        public string MP4 { get; set; }

        public string F4V { get; set; }

        public string OGV { get; set; }

        public int Type { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public bool TextPosition { get; set; }
    }
}
