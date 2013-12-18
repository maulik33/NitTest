using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IAdminView
    {
        int UserId { get; set; }

        string Instituions { get; set; }
        
        string SearchText { get; set; }
        
        int AdminId { get; set; }

        int ProgramofStudyId { get; set; }

        void ShowErrorMessage(string message);

        void ShowAdminList(IEnumerable<Admin> admins, SortInfo sortMetaData);

        void PopulateAdmin(Admin admin);

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies);
        
        void PopulateInstitutions(IEnumerable<Institution> institutions);
        
        void RefreshPage(Admin admin, UserAction action, Dictionary<int, string> securityLevel, string title,
            string subTitle, bool hasDeletePermission, bool hasAddPermission);

        void ExportAdminUserList(IEnumerable<Admin> reportData, ReportAction printActions);
    }
}
