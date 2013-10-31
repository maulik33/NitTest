using System;
using System.Web;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Institution
    {
        private string _annotation = string.Empty;

        public int InstitutionId { get; set; }

        public string InstitutionName { get; set; }

        public string InstitutionNameWithProgOfStudy { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string CenterId { get; set; }

        public int TimeZone { get; set; }

        public string IP { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUser { get; set; }

        public int CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int FacilityID { get; set; }

        public int ProgramID { get; set; }

        public TimeZones TimeZones { get; set; }

        public int DeleteUser { get; set; }

        public int Active { get; set; }

        public int AddressId { get; set; }

        public Address InstitutionAddress { get; set; }

        public string ContractualStartDate { get; set; }

        public DateTime? ContractualStartDateReport { get; set; }

        public string Email { get; set; }

        public bool PayLinkEnabled { get; set; }

        public string Annotation
        {
            get
            {
                return _annotation;
            }

            set
            {
                _annotation = value;
            }
        }

        public string AnnotationSave
        {
            get
            {
                return HttpUtility.HtmlEncode(_annotation);
            }

            set
            {
                _annotation = value;
            }
        }

        public int ProgramOfStudyId { get; set; }

        public string ProgramofStudyName { get; set; }

        public int ProctorTrackSecurityEnabled { get; set; }
    }
}
