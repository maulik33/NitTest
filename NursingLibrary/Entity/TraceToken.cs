using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class TraceToken
    {
        public TraceToken()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
