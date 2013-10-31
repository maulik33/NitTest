using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class AVPItemPresenter : AuthenticatedPresenterBase<IAVPItemView>
    {
        private const string Query_PARAM_TESTID = "TestID";
        private const string Query_PARAM_SEARCH_CONDITION = "SearchCondition";
        private const string Query_PARAM_SORT = "Sort";
        private const string Query_PARAM_Mode = "Mode";
        private const string Query_PARAM_ProgramOfStudy = "ProgramOfStudy";
        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public AVPItemPresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(Query_PARAM_TESTID);
            }
            RegisterQueryParameter(Query_PARAM_Mode);
            RegisterQueryParameter(Query_PARAM_SEARCH_CONDITION, Query_PARAM_Mode, "4");
            RegisterQueryParameter(Query_PARAM_SORT, Query_PARAM_Mode, "4");
            RegisterQueryParameter(Query_PARAM_ProgramOfStudy, Query_PARAM_Mode,"4");           
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void InitializePropValues()
        {
            View.TestId = GetParameterValue(Query_PARAM_TESTID).ToInt();
        }

        public void ShowAVPItemDetails()
        {
            int testId = View.TestId;
            bool programOfStudiesDropDownEnabled;
            int programOfStudyId = -1; //not selected which is desired for add
            if (testId > 0)
            {
                Test test = _cmsService.GetAVPItemByTestId(testId);
                View.TestName = test.TestName;
                View.URL = test.URL;
                View.PopWidth = Convert.ToString(test.PopupWidth);
                View.PopHeight = Convert.ToString(test.PopupHeight);
                View.HeaderLabelText = "Edit > AVP Items";
                programOfStudiesDropDownEnabled = false;
                programOfStudyId = test.ProgramofStudyId;
            }
            else
            {
                View.URL = "http://";
                View.HeaderLabelText = "Add > AVP Items";
                programOfStudiesDropDownEnabled = true;
            }
            View.RenderProgramOfStudyUI(_cmsService.GetProgramofStudies(), programOfStudyId, programOfStudiesDropDownEnabled);
        }

        public void SaveAVPItems(int programOfStudyId)
        {
            if (View.Confirm())
            {
                int testId = View.TestId;
                if (testId == -1)
                {
                    testId = 0;
                }

                Test test = new Test();
                test.TestId = testId;
                test.TestName = View.TestName;
                test.URL = View.URL;
                test.PopupWidth = View.PopWidth.ToInt();
                test.PopupHeight = View.PopHeight.ToInt();
                test.ProgramofStudyId = programOfStudyId;
                _cmsService.SaveAVPItems(test);
                NavigateToItemsPage();
            }
        }

        public void SearchAVPItems(string sortMetaData, int programOfStudyId)
        {
            IEnumerable<Test> tests = _cmsService.SearchAVPItems(View.TestName, programOfStudyId);
            View.RefreshPage(tests, SortHelper.Parse(sortMetaData));
        }

        public void DeleteAVPItemById(int id)
        {
            _cmsService.DeleteTestById(id);
        }

        public bool IsCustomTestExisted(string testName, int programOfStudyId, int testId)
        {
            bool customTestExist = false;
            IEnumerable<Test> tests = _cmsService.SearchAVPItems(testName, programOfStudyId);
            int count = (from t in tests
                         where t.TestName == testName
                         && t.TestId != testId
                         select t).Count();
            if (count > 0)
            {
                customTestExist = true;
            }

            return customTestExist;
        }

        public void InitializeAVPItemsItemProps()
        {
            int selectedProgramOfStudiesId = (int) ProgramofStudyType.RN;
            if (GetParameterValue(Query_PARAM_Mode) == "4")
            {
                View.TestName = GetParameterValue(Query_PARAM_SEARCH_CONDITION);
                selectedProgramOfStudiesId = GetParameterValue(Query_PARAM_ProgramOfStudy).ToInt();
                if (!string.IsNullOrEmpty(GetParameterValue(Query_PARAM_SORT)))
                {
                    View.Sort = GetParameterValue(Query_PARAM_SORT);
                }
            }
            View.RenderProgramOfStudyUI(_cmsService.GetProgramofStudies(), selectedProgramOfStudiesId, true);
        }

        public void NavigateToEdit(int programOfStudyId)  //used on add item button click on items page (available in cms only)
        {
            Navigator.NavigateTo(AdminPageDirectory.NewAVPItems, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}",
Query_PARAM_TESTID, "-1", Query_PARAM_Mode, 1, "CMS", 1, Query_PARAM_SEARCH_CONDITION, View.TestName, Query_PARAM_SORT, View.Sort, Query_PARAM_ProgramOfStudy, programOfStudyId));

        }

        public void NavigateToEdit(string testId, UserAction actionType, int programOfStudy)  //used on edit
        {
            Navigator.NavigateTo(AdminPageDirectory.NewAVPItems, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), Query_PARAM_TESTID, testId, Query_PARAM_Mode, 4, "CMS", 1, Query_PARAM_SEARCH_CONDITION, View.TestName, Query_PARAM_SORT, View.Sort, Query_PARAM_ProgramOfStudy, programOfStudy));
        }

        public void NavigateToItemsPage()
        {
            string  searchCondition = GetParameterValue(Query_PARAM_SEARCH_CONDITION);
            string  sort = GetParameterValue(Query_PARAM_SORT);
            string programOfStudyId = GetParameterValue(Query_PARAM_ProgramOfStudy);
            Navigator.NavigateTo(AdminPageDirectory.AVPItems, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&CMS=1", Query_PARAM_SORT, sort, Query_PARAM_SEARCH_CONDITION, searchCondition, Query_PARAM_Mode, "4", Query_PARAM_ProgramOfStudy, programOfStudyId.ToString()));

        }
    }
}
