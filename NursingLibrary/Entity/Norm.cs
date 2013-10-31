using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class Norm
    {
        public int Id { get; set; }

        public string ChartType { get; set; }
        
        public int ChartID { get; set; }
        
        public Single? NormValue { get; set; }
        
        public int TestId { get; set; }
    }
}
