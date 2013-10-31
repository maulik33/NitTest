using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ProgramPresenter : AuthenticatedPresenterBase<IProgramView>
    {
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public ProgramPresenter(IAdminService service)
            : base(Module.Program)
        {
            _adminService = service;
        }

        public override void RegisterAuthorizationRules()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }

            RegisterAuthorizationRule(UserAction.Add);
            RegisterAuthorizationRule(UserAction.Edit);
            RegisterAuthorizationRule(UserAction.Delete);
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
                RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, "2");
            }
            else if (_mode == ViewMode.View)
            {
                RegisterQueryParameter(QUERY_PARAM_ID);
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void SearchPrograms(int programOfStudyId, string searchText, string sortMetaData)
        {
            View.ShowProgramResults(_adminService.SearchPrograms(programOfStudyId, searchText),
                SortHelper.Parse(sortMetaData));
        }

        public void PopulateProgramOfStudies()
        {
            View.PopulateProgramOfStudies(_adminService.GetProgramofStudies());
        }

        public void GetBulkProgramDetails(int testId, string searchtext, string type, string sortMetaData)
        {
            IEnumerable<Program> programs = _adminService.GetBulkProgramDetails(testId, type, View.ProgramOfStudyId == (int)ProgramofStudyType.RN ? (int)ProgramofStudyType.RN : 0);
            string programIds = string.Join("|", programs.Where(r => (r.IsTestAssignedToProgram == true)).Select(s => s.ProgramId));
            if (!string.IsNullOrEmpty(searchtext))
            {
                programs = programs.Where(r => r.ProgramName.ToLower().Contains(searchtext.ToLower())).ToList();
            }

            View.ShowBulkProgramResults(programs, programIds, SortHelper.Parse(sortMetaData));
        }

        public void ShowProgramDetails()
        {
            Program program = _adminService.GetProgram(Id);
            string title = string.Empty;
            string subTitle = string.Empty;
            if (ActionType == UserAction.Edit)
            {
                title = "View/Edit >> Program List >> Edit a Program";
                subTitle = "To edit a Program, please complete the information below:";
            }
            else if (ActionType == UserAction.Add)
            {
                title = "Add >> Create a New Program";
                subTitle = "To create a Program, please complete the information below:";
                if (_mode != ViewMode.View)
                {
                    View.PopulateProgramOfStudies(_adminService.GetProgramofStudies());
                }
            }
            else
            {
                title = "Copy >> Program List >> Copy a Program";
                subTitle = "To copy a Program, please complete the information below:";
            }

            bool hasDeletePermission = false;
            bool hasAddPermission = false;
            if (ActionType != UserAction.Add && HasPermission(UserAction.Delete))
            {
                hasDeletePermission = true;
            }

            if ((ActionType == UserAction.Add && HasPermission(UserAction.Add))
            || (ActionType == UserAction.Edit && HasPermission(UserAction.Edit)) || (ActionType == UserAction.Copy && HasPermission(UserAction.Add)))
            {
                hasAddPermission = true;
            }

            View.RefreshPage(program, ActionType, title, subTitle, hasDeletePermission, hasAddPermission);
        }

        public void SaveProgram()
        {
            Program program = new Program()
            {
                ProgramId = Id,
                ProgramName = View.Name,
                Description = View.Description,
                ProgramOfStudyId = View.ProgramOfStudyId
            };
            if (ValidateProgram(program))
            {
                _adminService.SaveProgram(program);
                Navigator.NavigateTo(AdminPageDirectory.ProgramView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, program.ProgramId));
            }
        }

        private bool ValidateProgram(Program program)
        {
            bool isValid = true;
            IEnumerable<Program> programs = _adminService.SearchPrograms(0, program.ProgramName);
            bool isNewProgramNameDuplicate = (ActionType == UserAction.Add || ActionType == UserAction.Copy) && program.ProgramId == 0 && 
                                                programs.Any(
                                                    p => p.ProgramName.ToLower() == program.ProgramName.ToLower());

            bool isExistingProgramNameDuplicate = (ActionType == UserAction.Edit) &&
                                               programs.Any(
                                                   p =>
                                                   p.ProgramId != program.ProgramId &&
                                                   p.ProgramName.ToLower() == program.ProgramName.ToLower());

            if (isNewProgramNameDuplicate || isExistingProgramNameDuplicate)
            {
                isValid = false;
                View.ShowMessage("Another Program with same name already exists.");
            }

            return isValid;
        }

        public void CopyProgram()
        {
            Program program = new Program()
            {
                ReferenceProgramId = Id,
                ProgramName = View.Name,
                Description = View.Description,
                ProgramOfStudyId = View.ProgramOfStudyId
            };
            if (ValidateProgram(program))
            {
                _adminService.CopyProgram(program);
                Navigator.NavigateTo(AdminPageDirectory.ProgramView, string.Empty,
                                     string.Format("{0}={1}", QUERY_PARAM_ID, program.ProgramId));
            }
        }

        public void ShowProgramsToAssign()
        {
            Program program = _adminService.GetProgram(Id);

            View.Name = program.ProgramName;
            View.Description = program.Description;
            View.ProgramOfStudyName = program.ProgramOfStudyName;
            View.PopulateProgramOfStudies(_adminService.GetProgramofStudies());
            View.ProgramId = Id;
            ShowAssignedPrograms();
        }

        public void ShowAssignedPrograms()
        {
            View.SearchText = (View.SearchText == null) ? string.Empty : View.SearchText.ToString();
            View.PopulateAssignedTest(_adminService.GetTestsForProgram(Id, View.SearchText));
        }

        public bool CanAssignTest()
        {
            return HasPermission(UserAction.AssignTests);
        }

        public void DeleteProgram()
        {
            _adminService.DeleteProgram(Id);
            Navigator.NavigateTo(AdminPageDirectory.ProgramList);
        }

        public void GetTests()
        {
            GetProductDetails();
            View.PopulateTests(_adminService.GetTests(View.ProductId, View.ProgramOfStudyId));
        }

        public void GetProductDetails()
        {
            var product = _adminService.GetProduct(View.ProductId);
            View.Bundle = product.Bundle;
        }

        public void DeleteProgramFromProduct(int assetGroupId)
        {
            _adminService.DeleteProductFromProgram(View.ProgramId, View.ProductId, View.Type, assetGroupId);
            bool hasAddPermission = HasPermission(UserAction.AssignProgram);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, hasAddPermission);
        }

        public void AssignTestToProgram(List<ProgramTestDates> lstTestDates)
        {
            if (lstTestDates.Count() > 0)
            {
                _adminService.AssignTestToProgram(lstTestDates);
            }

            bool hasAddPermission = HasPermission(UserAction.AssignTests);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, hasAddPermission);
        }

        public void ShowTestCategories()
        {
            View.PopulateProducts(_adminService.GetProducts());
        }

        public void SaveBulkModifiedPrograms(int testId, int type, string programIds)
        {
            _adminService.SaveBulkModifiedPrograms(testId, type, programIds);
        }

        public void NavigateToEdit(string programId, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.ProgramEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.ProgramEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, programId));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), actionType);
        }

        public void NavigateToAssignTests(string programId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ProgramAddAssign, string.Empty, string.Format("{0}={1}",
                QUERY_PARAM_ID, programId));
        }

        public void GetAssetGroups()
        {
            View.AssetGroups = _adminService.GetAssetGroups(View.ProgramOfStudyId);
            View.PopulateAssetGroup(View.AssetGroups);
        }

        public void GetAssets(int assetGroupid)
        {
            View.PopulateAssets(_adminService.GetAssets(assetGroupid));
        }

        public IEnumerable<CaseStudy> GetCaseAssets()
        {
            return _adminService.GetCaseAssets();
        }


        public void AssignAssetToProgram(List<ProgramTestDates> assetList)
        {
            if (assetList.Count() > 0)
            {
                _adminService.AssignAssetsToProgram(assetList);
            }

            bool hasAddPermission = HasPermission(UserAction.AssignTests);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, hasAddPermission);
        }
    }
}
