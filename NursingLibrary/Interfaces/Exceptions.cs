using System;

namespace NursingLibrary.Interfaces
{
    public class LoginException : Exception
    {
        public LoginException(LoginFailure loginFailure, string details)
        {
            LoginFailureType = loginFailure;
            Details = details;
        }

        public LoginFailure LoginFailureType { get; set; }

        public string Details { get; set; }
    }
}