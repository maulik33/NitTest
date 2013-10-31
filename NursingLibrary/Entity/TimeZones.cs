using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class TimeZones
    {
        public int TimeZoneId { get; set; }

        public string TimeZoneName { get; set; }
        
        public string Description { get; set; }
    }
}
