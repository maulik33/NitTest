using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class StudentEntity
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string UserName { get; set; }

        public string EnrollmentId { get; set; }

        public string Email { get; set; }

        public bool InstitutionStatus { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}
