using System.Collections.Generic;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    public class UserTypeHelper
    {
        public static Dictionary<string, string> GetContactTypes()
        {
            Dictionary<string, string> _contactType = new Dictionary<string, string>();
            _contactType.Add("1", "Administrative");
            _contactType.Add("2", "Academic");
            _contactType.Add("3", "Finance");
            _contactType.Add("4", "Technical");

            return _contactType;
        }

        public IEnumerable<UserType> GetUserTypes(UserType currentUserType)
        {
            switch (currentUserType)
            {
                case UserType.LocalAdmin:
                    {
                        yield return UserType.TechAdmin;
                        break;
                    }

                case UserType.AcademicAdmin:
                    {
                        yield return UserType.LocalAdmin;
                        yield return UserType.TechAdmin;
                        break;
                    }

                case UserType.SuperAdmin:
                    {
                        yield return UserType.InstitutionalAdmin;
                        yield return UserType.SuperAdmin;
                        yield return UserType.AcademicAdmin;
                        yield return UserType.LocalAdmin;
                        yield return UserType.TechAdmin;
                        break;
                    }

                case UserType.InstitutionalAdmin:
                    {
                        yield return UserType.InstitutionalAdmin;
                        yield return UserType.LocalAdmin;
                        yield return UserType.TechAdmin;
                        break;
                    }
            }
        }

        public string GetUserForDisplay(UserType currentUserType)
        {
            string _displayUser = string.Empty;
            switch (currentUserType)
            {
                case UserType.LocalAdmin:
                    {
                        _displayUser = "Local";
                        break;
                    }

                case UserType.AcademicAdmin:
                    {
                        _displayUser = "Academic";
                        break;
                    }

                case UserType.SuperAdmin:
                    {
                        _displayUser = "Super";
                        break;
                    }

                case UserType.InstitutionalAdmin:
                    {
                        _displayUser = "Institutional";
                        break;
                    }

                case UserType.TechAdmin:
                    {
                        _displayUser = "Tech";
                        break;
                    }

                default:
                    {
                        _displayUser = "Invalid User";
                        break;
                    }
            }

            return _displayUser;
        }
    }
}
