using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class Asset
    {
        public int AssetId { get; set; }

        public string AssetName { get; set; }

        public string AssetValue { get; set; }

        public int AssetGroupId { get; set; }

        public int AssetLocationType { get; set; }

        public string AssetLocationOrder { get; set; }
    }
}
