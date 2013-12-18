using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class EmailMission
    {
        public string EmailId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
