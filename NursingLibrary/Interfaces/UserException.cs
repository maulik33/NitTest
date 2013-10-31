using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public class UserException : Exception
    {
        public UserException(int userId, string message)
        {
            UserId = userId;
            UserExceptionMessage = message;
        }

        public int UserId { get; set; }

        public string UserExceptionMessage { get; set; }
    }
}
