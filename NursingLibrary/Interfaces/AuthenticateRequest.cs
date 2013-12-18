using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }

        public String SessionId { get; set; }
    }
}