using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
   public class AssetDetail
    {
        public int ProgramofStudyId { get; set; }
        public int order { get; set; }
        public string AssetName { get; set; }
        public string  AssetValue { get; set; }
        public int AssetLocationType { get; set; }
    }
}
