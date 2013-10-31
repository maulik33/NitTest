using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IEmailReceiverView
    {
        string EmailId { get; set; }

        string EmailTo { get; }
        
        string EmailToStudentOrAdmin { get; }
        
        bool IsEmailIdExistInQueryString { get; set; }
        
        bool IsEmailEdit { get; }

        bool CustomEmailToAdmins { get; }

        bool CustomEmailToStudents { get; }

        bool UserEmailToStudents { get; }

        bool UserEmailLocalAdmins { get; }

        bool UserEmailTechAdmins { get; }
        
        void PopulateCustomEmails(IEnumerable<Email> emails);
        
        void PopulateInstitutions(IEnumerable<Institution> institutions);
        
        void PopulateCohorts(IEnumerable<Cohort> cohorts);
        
        void PopulateGroup(IEnumerable<Group> groups);
        
        void PopulateStudent(IEnumerable<StudentEntity> students);
        
        void PopulateAdmin(IEnumerable<Admin> admins);
        
        void ShowSendEmailResult(string msg);
        
        void ShowEmailDetails(Email email);
        
        void SetControlsSpecial();

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programOfStudies);
    }
}
