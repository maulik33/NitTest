using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public class CollectionResponse<T>
    {
        public IEnumerable<T> Result { get; set; }
    }
}
