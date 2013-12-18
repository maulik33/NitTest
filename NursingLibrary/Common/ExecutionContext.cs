using System;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Common
{
    [Serializable]
    public class ExecutionContext
    {
        public int UserId { get; set; }

        public UserType UserType { get; set; }

        public bool IsAdminLogin { get; set; }

        public Admin User { get; set; }

        public TraceToken TraceToken { get; set; }
    }
}
