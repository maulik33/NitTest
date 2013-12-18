using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class AssetGroup
    {
        public int AssetGroupId { get; set; }

        public string AssetGroupName { get; set; }

        public int ProgramofStudyId { get; set; }

        public int ProductId { get; set; }
    }
}
