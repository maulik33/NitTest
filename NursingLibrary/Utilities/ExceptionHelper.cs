using System;
using System.Text;

namespace NursingLibrary.Utilities
{
    // This is just the first cut. Improvize as needed.
    public class ExceptionHelper
    {
        public static string Handle(Exception ex)
        {
            if (ex is ApplicationException || true)
            {
                return ex.Message;
            }
            else
            {
                // TO DO : Log
                return "Some internal error has occurred and the operation did not complete successfully.";
            }
        }

        public static string GetAllErrorMessages(Exception ex)
        {
            StringBuilder strBuilder = new StringBuilder();
            bool firstPass = true;
            Exception currentException = ex;
            while (true)
            {
                strBuilder.AppendLine(currentException.Message);
                if (firstPass)
                {
                    strBuilder.AppendLine(currentException.StackTrace);
                }
                
                if (currentException.InnerException == null)
                {
                    break;
                }
                
                firstPass = false;
                currentException = currentException.InnerException;
            }

            return strBuilder.ToString();
        }
    }
}
