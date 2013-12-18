using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public class Response<T>
    {
        public T Result { get; set; }
    }
}
