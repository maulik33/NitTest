using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Linq;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportCaseComparisonsPresenter : ReportPresenterBase<IReportCaseComparisonsView>
    {
        private readonly IReportDataService _reportDataService;
        private const string QUERY_PARAM_INSTITUTIONID = "Institution";
        private const string QUERY_PARAM_COHORTS = "Cohorts";
        private const string QUERY_PARAM_CASEIDS = "CaseIDs";
        private const string QUERY_PARAM_MODULEIDS = "ModuleIDs";
        private const string QUERY_PARAM_CATEGORIES = "Categories";
        private const string QUERY_PARAM_SUBCATEGORIES = "SubCategories";

        public ReportCaseComparisonsPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }

        public bool IsPrintInterface { get; set; }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            ReportAction printAction = (ReportAction)View.Act;
            if (printAction == ReportAction.PrintInterface)
            {
                RegisterQueryParameter(QUERY_PARAM_INSTITUTIONID);
                RegisterQueryParameter(QUERY_PARAM_COHORTS);
                RegisterQueryParameter(QUERY_PARAM_CASEIDS);
                RegisterQueryParameter(QUERY_PARAM_MODULEIDS);
                RegisterQueryParameter(QUERY_PARAM_CATEGORIES);
                RegisterQueryParameter(QUERY_PARAM_SUBCATEGORIES);
            }
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter caseParam = new ReportParameter(ReportParamConstants.PARAM_CASE, PopulateCase);
            ReportParameter moduleParam = new ReportParameter(ReportParamConstants.PARAM_MODULE, PopulateModule);
            ReportParameter categoryParam = new ReportParameter(ReportParamConstants.PARAM_CATEGORY, PopulateCategory);
            ReportParameter subCategoryParam = new ReportParameter(ReportParamConstants.PARAM_SUB_CATEGORY, PopulateSubCategory);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(caseParam);
            AddParameter(moduleParam);
            AddParameter(categoryParam);
            AddParameter(subCategoryParam, categoryParam);
        }

        public override void InitParamValues()
        {
            ReportAction printAction = (ReportAction)View.Act;
            if (printAction == ReportAction.PrintInterface)
            {
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_INSTITUTIONID);
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues = GetParameterValue(QUERY_PARAM_COHORTS);
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues = GetParameterValue(QUERY_PARAM_CASEIDS);
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues = GetParameterValue(QUERY_PARAM_MODULEIDS);
                Parameters[ReportParamConstants.PARAM_CATEGORY].SelectedValues = GetParameterValue(QUERY_PARAM_CATEGORIES);
                Parameters[ReportParamConstants.PARAM_SUB_CATEGORY].SelectedValues = GetParameterValue(QUERY_PARAM_SUBCATEGORIES);
            }
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (!View.PostBack && programOfStudies.HasElements())
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
            }
            View.PopulateProgramOfStudies(programOfStudies);
        }

        public void PopulateInstitutions()
        {
            int programofStudyId = 0;
            if (View.IsProgramofStudyVisible)
            {
                programofStudyId = Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt();
            }
            IEnumerable<Institution> institutions = _reportDataService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty);
            View.PopulateInstitutions(institutions);
        }


        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt());
            View.PopulateCohorts(cohorts);
        }

        public void PopulateCase()
        {
            IEnumerable<CaseStudy> caseStudies = _reportDataService.GetCaseStudies();
            View.PopulateCase(caseStudies);
        }

        public void PopulateModule()
        {
            IEnumerable<Modules> modules = _reportDataService.GetModule();
            View.PopulateModule(modules);
        }

        public void PopulateCategory()
        {
            IEnumerable<Category> categories = new List<Category> 
            { 
                new Category { CategoryID = 2, TableName = "Nursing Process" },
                new Category { CategoryID = 3, TableName = "Critical Thinking" }
            };
            View.PopulateCategories(categories);
        }

        public void PopulateSubCategory()
        {
            IDictionary<string, string> categoryDetails = new Dictionary<string, string>();
            string[] categoryIds =
                Parameters[ReportParamConstants.PARAM_CATEGORY].SelectedValues.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            IDictionary<CategoryName, Category> categories = _reportDataService.GetCategories();
            foreach (string categoryId in categoryIds)
            {
                CategoryName name = (CategoryName)categoryId.ToInt();
                categories[name].Details.Values.ToList().ForEach(p => AddToDictionary(categoryDetails, p, categoryId));
            }

            View.PopulateSubCategories(categoryDetails);
        }

        public IEnumerable<CategoryDetail> GetSubCategories(int categoryId)
        {
            CategoryName name = (CategoryName)categoryId;
            IDictionary<CategoryName, Category> categories = _reportDataService.GetCategories();
            IDictionary<int, CategoryDetail> indexedCategoryDetails = categories[name].Details;
            List<CategoryDetail> categoryDetails = new List<CategoryDetail>(indexedCategoryDetails.Values);
            return categoryDetails;
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsForCohortsBySubCategoryChart(string cohorts, int categoryId, int subCategoryId, string cases, string modules, int institutionId)
        {
            return _reportDataService.GetResultsForCohortsBySubCategoryChart(cohorts, categoryId, subCategoryId, cases, modules, institutionId);
        }

        public void GenerateReport()
        {
            View.RenderReport();
        }

        public void GenerateReport(ReportAction printActions)
        {
            View.ExportReport(printActions);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

        private void AddToDictionary(IDictionary<string, string> categoryDetails, CategoryDetail category, string parent)
        {
            categoryDetails.Add(string.Format("{0}.{1}", parent, category.Id), category.Description);
        }   
    }
}
