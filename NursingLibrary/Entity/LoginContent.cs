using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
   public class LoginContent
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int AdminUserId { get; set; }

        public string ReleaseStatus { get; set; }

        public string ReleasedContent { get; set; }
    }
}
