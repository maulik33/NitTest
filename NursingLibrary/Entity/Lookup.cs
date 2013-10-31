using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class Lookup
    {
        public Lookup(int id, LookupType type, int originalId, string displayText)
        {
            Id = id;
            Type = type;
            OriginalId = originalId;
            DisplayText = displayText;
        }

        public int Id { get; private set; }

        public LookupType Type { get; private set; }
        
        public int OriginalId { get; private set; }
        
        public string DisplayText { get; private set; }
    }
}
