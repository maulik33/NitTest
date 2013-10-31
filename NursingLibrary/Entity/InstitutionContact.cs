using System;
using System.Collections.Generic;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class InstitutionContact
    {
        public int ContactId { get; set; }

        public int InstitutionId { get; set; }

        public int ContactType { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactEmail { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int RecordSortOrder { get; set; }

        public Dictionary<string, string> ContactTypes
        {
            get { return UserTypeHelper.GetContactTypes(); }
        }

        public string ContactTypeText
        {
            get
            {
                string _contactTypeText = string.Empty;
                if (ContactType > 0)
                {
                    _contactTypeText = ((ContactType)ContactType).ToString();
                }

                return _contactTypeText;
            }
        }
    }
}
