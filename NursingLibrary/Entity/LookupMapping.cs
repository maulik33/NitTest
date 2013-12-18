using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class LookupMapping
    {
        public LookupMapping(int id, LookupType type, int lookupId, int mappedTo)
        {
            Id = id;
            Type = type;
            LookupId = lookupId;
            MappedTo = mappedTo;
        }

        public int Id { get; private set; }

        public LookupType Type { get; private set; }
        
        public int LookupId { get; private set; }
        
        public int MappedTo { get; private set; }

        public LookupType? CategoryType { get; private set; }

        public void SetCategoryType(LookupType type)
        {
            if (CategoryType.HasValue)
            {
                throw new ApplicationException("Cannot overwrite Category Type value.");
            }

            CategoryType = type;
        }
    }
}
