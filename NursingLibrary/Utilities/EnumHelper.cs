using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class EnumHelper
    {
        public static string GetQuestionFileTypeValue(string questionFileType)
        {
            var type = (QuestionFileType)Enum.Parse(typeof(QuestionFileType), questionFileType);
            return GetQuestionFileTypeValue(type);
        }

        public static string GetQuestionFileTypeValue(QuestionFileType questionFileType)
        {
            return string.Format("0{0}", (int)questionFileType);
        }

        public static LookupType GetLookupType(int typeId)
        {
            return (LookupType)Enum.Parse(typeof(LookupType), typeId.ToString());
        }

        public static UserAction GetUserAction(string actionType)
        {
            switch (actionType)
            {
                case "0":
                    return UserAction.View;
                case "1":
                    return UserAction.Add;
                case "2":
                    return UserAction.Edit;
                case "17":
                    return UserAction.Copy;
                default:
                    throw new InvalidOperationException("Unknown User Action value encountered.");
            }
        }

        public static UserType GetUserType(int userType)
        {
            switch (userType)
            {
                case 1:
                    return UserType.AcademicAdmin;
                case 2:
                    return UserType.LocalAdmin;
                case 3:
                    return UserType.TechAdmin;
                case 0:
                    return UserType.SuperAdmin;
                case 5:
                    return UserType.InstitutionalAdmin;
                case 6:
                    return UserType.Student;
                default:
                    throw new InvalidOperationException(string.Format("{0} is not a valid User Type.", userType));
            }
        }
    }
}
