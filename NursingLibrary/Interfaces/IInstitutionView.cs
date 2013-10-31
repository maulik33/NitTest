using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IInstitutionView
    {
        int IID { get; set; }

        string ErrorMessage { get; set; }

        string Name { get; set; }

        int ActionType { get; set; }

        UserType UserTypeValue { get; set; }

        Institution Institution { get; set; }

        Program Program { get; set; }

        IEnumerable<TimeZones> TimeZones { get; set; }

        string ProgramOfStudy { get; set; }

        void BindData();

        void BindData(IEnumerable<TimeZones> timeZones, IEnumerable<Program> nurPrograms, IEnumerable<ProgramofStudy> programOfStudies);

        void ShowInstitutionResults(IEnumerable<Institution> Institutions, SortInfo sortMetaData);

        void PopulateCountry(IEnumerable<Country> country);

        void PopulateState(IEnumerable<State> state);

        void PopulateAddress(Address address);

        void ExportInstitutions(IEnumerable<Institution> reportData, ReportAction reportAction);

        void PopulateInstitutionContacts(IEnumerable<InstitutionContact> institutionContacts);

        void PopulateProgram(IEnumerable<Program> programs);

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy);
    }
}
