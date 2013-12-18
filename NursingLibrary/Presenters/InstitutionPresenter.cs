using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Text;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class InstitutionPresenter : AuthenticatedPresenterBase<IInstitutionView>
    {
        private const string QUERY_PARAM_ACTIONTYPE = "actionType";
        private const string PARAM_ASSETS = "assets";
        private const string QUERY_PARAM_ACTIONTYPE_COMPONENT_ID = "ComponentId";
        private readonly IAdminService _adminService;
        private ViewMode _mode;
        private const string ACTIVE_INSTITUTION_EXISTS = "Institution name must be Unique.";

        public InstitutionPresenter(IAdminService service)
            : base(Module.Institution)
        {
            _adminService = service;
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }

            RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
            RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, "2");
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void ShowInstitutionDetails()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _adminService.GetProgramofStudies();
            IEnumerable<TimeZones> timeZones = _adminService.GetTimeZones();
            IEnumerable<Program> nurPrograms = null;

            if (View.ActionType == 2)
            {
                var institutionDetails = _adminService.GetInstitution(Id, CurrentContext.UserId.ToInt());
                if (institutionDetails != null)
                {
                    var programodStudyId = institutionDetails.ProgramofStudyName == ProgramofStudyType.RN.ToString()
                                            ? (int)ProgramofStudyType.RN
                                            : (int)ProgramofStudyType.PN;
                    nurPrograms = _adminService.GetProgramsByProgramofStudyId(programodStudyId);
                }

            }
            else
            {
                nurPrograms = _adminService.GetProgramsByProgramofStudyId((int)ProgramofStudyType.RN);
            }
            View.BindData(timeZones, nurPrograms, programOfStudies);
        }

        public void OnIntitutionListInitialized()
        {
            //var _defaultAddressCountry = KTPApp.DefaultAddressCountry.ToInt();
            //var _countriesWithState = KTPApp.CountriesWithStates;
            Institution institution = _adminService.GetInstitution(Id, CurrentContext.UserId.ToInt());
            View.Institution = institution;
            View.TimeZones = _adminService.GetTimeZones();
            if (institution != null)
            {
                View.Program = _adminService.GetProgram(institution.ProgramID);
            }
            var _addressId = institution.InstitutionAddress.AddressId;
            View.PopulateCountry(_adminService.GetCountries(0));
            var _address = _adminService.GetAddress(_addressId);
            View.PopulateAddress(_address);
            View.BindData();
        }

        public void SearchInstitutions(string searchText, string sortMetaData, string status, string Programofstudy)
        {
            View.ShowInstitutionResults(_adminService.SearchInstitution(searchText, CurrentContext.UserId, status, Programofstudy),
                 SortHelper.Parse(sortMetaData));
        }

        public void PopulateStates(int countryId, int stateId)
        {
            View.PopulateState(_adminService.GetStates(countryId, stateId));
        }

        public int GetDefaultCountryForCountryList()
        {
            // var val = _adminService.GetAppSettings();
            return 0;
        }

        public void PopulateCountryList()
        {
            var countryList = _adminService.GetCountries(0);
            View.PopulateCountry(countryList);
        }

        public Institution GetInstitutionById()
        {
            var _populateState = true;
            var _countryId = 0;
            var _defaultAddressCountry = KTPApp.DefaultAddressCountry.ToInt();
            var _countriesWithState = KTPApp.CountriesWithStates;
            var _institutionDetails = _adminService.GetInstitution(Id, CurrentContext.UserId.ToInt());
            var _addressId = _institutionDetails.InstitutionAddress.AddressId;
            var _address = _adminService.GetAddress(_addressId);

            if (_address.AddressId == 0 || _address.AddressCountry == null || _address.AddressCountry.CountryId == _defaultAddressCountry)
            {
                _countryId = _defaultAddressCountry;
            }
            else if (_countriesWithState.Contains(_address.AddressCountry.CountryId.ToString()))
            {
                _countryId = _address.AddressCountry.CountryId;
            }
            else
            {
                _populateState = false;
            }

            if (_populateState)
            {
                PopulateStates(_countryId, 0);
            }

            if (_addressId == 0)
            {
                _address.AddressCountry = new Country { CountryId = _defaultAddressCountry };
            }

            View.PopulateAddress(_address);
            return _institutionDetails;
        }

        public void SaveInstitution(Institution institution)
        {
            _adminService.SaveInstitution(institution);
            NavigateToView(institution.InstitutionId);
        }

        public void DeleteInstitution()
        {
            _adminService.DeleteInstitution(Id, CurrentContext.UserId);
            NavigateToList();
        }

        public void NavigateToEdit(UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.InstitutionEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else if (actionType == UserAction.Edit)
            {
                Navigator.NavigateTo(AdminPageDirectory.InstitutionEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, Id.ToString()));
            }
        }

        public void NavigatetoAssetList(int id)
        {
            Navigator.NavigateTo(AdminPageDirectory.ComponentAsset, string.Empty, string.Format("{0}={1}&{2}={3}",
                   QUERY_PARAM_ACTIONTYPE, PARAM_ASSETS, QUERY_PARAM_ACTIONTYPE_COMPONENT_ID, id.ToString()));
        }

        public void NavigateToList()
        {
            Navigator.NavigateTo(AdminPageDirectory.InstitutionList, string.Empty, string.Empty);
        }

        public void NavigateToView(int id)
        {
            Navigator.NavigateTo(AdminPageDirectory.InstitutionView, string.Empty, string.Format("{0}={1}",
                   QUERY_PARAM_ID, id.ToString()));
        }

        public void NavigateToEditPage(int id)
        {
            Navigator.NavigateTo(AdminPageDirectory.InstitutionEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                   QUERY_PARAM_ID, id.ToString(), QUERY_PARAM_ACTION_TYPE, ((int)UserAction.Edit).ToString()));
        }

        public void InitializeProperties()
        {
            View.ActionType = GetParameterValue(QUERY_PARAM_ACTION_TYPE).ToInt();
            View.IID = Id;
            View.UserTypeValue = CurrentContext.UserType;
        }

        public void ExportInstitutions(string searchText, ReportAction reportAction, string status, string ProgramOfStudyName)
        {
            View.ExportInstitutions(_adminService.SearchInstitution(searchText, CurrentContext.UserId, status, ProgramOfStudyName), reportAction);
        }

        public string GetAnnotation()
        {
            IEnumerable<Institution> institutions = _adminService.GetInstitutions(Id, string.Empty, CurrentContext.UserId.ToInt());
            return institutions.SingleOrDefault().Annotation;
        }

        public void SaveInstitutionContact(InstitutionContact institutionConatct)
        {
            institutionConatct.CreatedDate = DateTime.Now;
            institutionConatct.InstitutionId = Id;
            institutionConatct.CreatedBy = CurrentContext.UserId;
            _adminService.SaveInstitutionContact(institutionConatct);
        }

        public void PopulateInstitutionContacts()
        {
            if (GetParameterValue(QUERY_PARAM_ACTION_TYPE).ToInt() == (int)UserAction.Edit)
            {
                View.PopulateInstitutionContacts(_adminService.GetInstitutionContacts(Id));
            }
        }

        public void DeleteInstitution(int contactId)
        {
            InstitutionContact institutionContact = _adminService.GetInstitutionContactsByContactId(contactId);
            if (institutionContact != null)
            {
                institutionContact.DeletedBy = CurrentContext.UserId;
                institutionContact.DeletedDate = DateTime.Now;
                institutionContact.Status = 0;
                UpdateInstitutionContact(institutionContact);
            }
        }

        public void UpdateInstitutionContact(InstitutionContact institutionContact)
        {
            _adminService.SaveInstitutionContact(institutionContact);
            PopulateInstitutionContacts();
        }

        public void PopulateProgram(int programofStudyId)
        {
            View.PopulateProgram(_adminService.GetProgramsByProgramofStudyId(programofStudyId));
        }

        public void GetInstitutionList(string sortMetaData)
        {
            View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
        }

        public bool ValidateInstitution()
        {
            bool validateinstitution = false;
            IEnumerable<Institution> institutions = _adminService.GetAllInstitutions();
            if (ActionType == UserAction.Edit)
            {
                Institution currentInstitution = institutions.Where(r => r.InstitutionId == Id).SingleOrDefault();
                View.ProgramOfStudy = currentInstitution.ProgramofStudyName;
            }

            Institution institution = institutions.Where(i => (Id != i.InstitutionId && i.InstitutionName.ToLower() == View.Name.ToLower() && i.ProgramofStudyName.ToLower() == View.ProgramOfStudy.ToLower())).FirstOrDefault();
            if (institution != null)
            {
                View.ErrorMessage = ACTIVE_INSTITUTION_EXISTS;
                validateinstitution = true;
            }
            return validateinstitution;
        }
    }
}
