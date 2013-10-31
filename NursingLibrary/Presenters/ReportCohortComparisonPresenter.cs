using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportCohortComparisonPresenter : ReportPresenterBase<IReportCohortComparisonView>
    {
        private readonly IReportDataService _reportDataService;
        private const string QUERY_PARAM_PROGRAMOFSTUDYID = "ProgramofStudy";
        private const string QUERY_PARAM_INSTITUTIONID = "Institution";
        private const string QUERY_PARAM_COHORTS = "Cohorts";
        private const string QUERY_PARAM_TESTTYPES = "TestTypes";
        private const string QUERY_PARAM_TESTS = "Tests";
        private const string QUERY_PARAM_CATEGORIES = "Categories";
        private const string QUERY_PARAM_SUBCATEGORIES = "SubCategories";
        private const string QUERY_PARAM_PROGRAMOFSTUDYID_FOR_TESTS_CATEGORIES = "ProgramOfStudyForTestsAndCategories";

        public ReportCohortComparisonPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }

        public bool IsPrintInterface { get; set; }

        public override void RegisterAuthorizationRules()
        {
            // throw new NotImplementedException();
        }

        public override void RegisterQueryParameters()
        {
            // throw new NotImplementedException();
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);
            ReportParameter categoryParam = new ReportParameter(ReportParamConstants.PARAM_CATEGORY, PopulateCategory, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter subCategoryParam = new ReportParameter(ReportParamConstants.PARAM_SUB_CATEGORY, PopulateSubCategory);
            ReportParameter programOfStudyForTestsAndCategoriesParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES, PopulateProgramOfStudiesForTestsAndCategories, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(programOfStudyForTestsAndCategoriesParam, institutionParam, programOfStudyParam);
            AddParameter(testTypeParam);
            AddParameter(testsParam, testTypeParam, cohortParam, programOfStudyForTestsAndCategoriesParam);
            AddParameter(categoryParam, programOfStudyForTestsAndCategoriesParam);
            AddParameter(subCategoryParam, categoryParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (!View.PostBack && programOfStudies.HasElements())
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = ((ReportAction)View.Act) == ReportAction.PrintInterface ? Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues : programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
            }
            View.PopulateProgramOfStudies(programOfStudies);
        }

        public void PopulateProgramOfStudiesForTestsAndCategories()
        {
            int? selectedInstitutionProgamOfStudy = null;
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (programOfStudies.HasElements())
            {
                if (!View.PostBack)
                {
                    Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues = ((ReportAction)View.Act) == ReportAction.PrintInterface ? Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues : programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
                }
                else
                {
                    string selectedInstitutionId = Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues;
                    if (!string.IsNullOrWhiteSpace(selectedInstitutionId))
                    {
                        Institution selectedInstitution = _reportDataService.GetInstitutionDetails(selectedInstitutionId).FirstOrDefault();
                        selectedInstitutionProgamOfStudy = selectedInstitution.ProgramOfStudyId;
                    }
                }

                if (selectedInstitutionProgamOfStudy.HasValue)
                {
                    Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues = selectedInstitutionProgamOfStudy.Value.ToString();
                    View.PopulateProgramOfStudiesForTestsAndCategories(programOfStudies, selectedInstitutionProgamOfStudy);
                }
                else
                {
                    View.PopulateProgramOfStudiesForTestsAndCategories(programOfStudies);
                }

                if (View.PostBack)
                {
                    RefreshChildren(Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES], true);
                }
            }
        }

        public void PopulateInstitutions()
        {
            IEnumerable<Institution> institutions = _reportDataService.GetInstitutions(CurrentContext.UserId, IsProgramOfStudyInstitutionFilterVisible() ? Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt() : 0, string.Empty);
            View.PopulateInstitutions(institutions);
        }

        public void PopulateProducts()
        {
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
        }

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt());
            View.PopulateCohorts(cohorts);
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues.ToInt());
            View.PopulateTests(tests);
        }

        public void PopulateCategory()
        {
            IDictionary<CategoryName, Category> categories = _reportDataService.GetCategories(Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues.ToInt());
            View.PopulateCategories(categories.Values.ToList());
        }

        public void PopulateSubCategory()
        {
            int count = 0;
            int categoryId = 0;
            IDictionary<int, CategoryDetail> categoryDetails = new Dictionary<int, CategoryDetail>();
            string seletedValues = Parameters[ReportParamConstants.PARAM_CATEGORY].SelectedValues;
            string[] categoryIds = seletedValues.Split('|');
            if (categoryIds.Length == 1)
            {
                count = categoryIds.Length - 1;
                categoryId = categoryIds[count].ToInt();
                CategoryName name = (CategoryName)categoryId;
                IDictionary<CategoryName, Category> categories = _reportDataService.GetCategories();
                categoryDetails = categories[name].Details;
            }

            View.PopulateSubCategories(categoryDetails.Values.ToList());
        }

        public List<CategoryDetail> GetSubCategories(int categoryId)
        {
            CategoryName name = (CategoryName)categoryId;
            IDictionary<CategoryName, Category> categories = _reportDataService.GetCategories();
            IDictionary<int, CategoryDetail> indexedCategoryDetails = categories[name].Details;
            List<CategoryDetail> categoryDetails = new List<CategoryDetail>(indexedCategoryDetails.Values);
            return categoryDetails;
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsFromTheCohotForChart(int institutionId, int subcategoryId, int categoryId, string productList, string testList, string cohortList)
        {
            return _reportDataService.GetResultsFromTheCohotForChart(institutionId, subcategoryId, categoryId, productList, testList, cohortList);
        }

        public override void InitParamValues()
        {
            ReportAction printAction = (ReportAction)View.Act;
            if (printAction == ReportAction.PrintInterface)
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDYID);
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_INSTITUTIONID);
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues = GetParameterValue(QUERY_PARAM_COHORTS);
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_TESTS);
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_TESTTYPES);
                Parameters[ReportParamConstants.PARAM_CATEGORY].SelectedValues = GetParameterValue(QUERY_PARAM_CATEGORIES);
                Parameters[ReportParamConstants.PARAM_SUB_CATEGORY].SelectedValues = GetParameterValue(QUERY_PARAM_SUBCATEGORIES);
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_STUDY_FOR_TESTS_AND_CATEGORIES].SelectedValues = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDYID_FOR_TESTS_CATEGORIES);
            }
        }

        public void GenerateReport()
        {
            View.RenderReport();
        }

        public void GenerateReport(ReportAction printActions)
        {
            View.GenerateReport(printActions);
        }

        public string GenerateCohortGraphXML(int CategoryID, int SubCategory, string CohortList, string Name, string ProductList, string TestList, int InstitutionID)
        {
            string strXml = string.Empty;
            strXml = "<graph xaxisname=\"\" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\" yAxisValueDecimals=\"0\" showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"" + Name + " \" subcaption=\"\">";
            IEnumerable<ResultsFromTheCohortForChart> result = _reportDataService.GetResultsFromTheCohotForChart(InstitutionID, SubCategory, CategoryID, ProductList, TestList, CohortList);
            if (result != null)
            {
                int NumberOfCohorts = result.Count();
                string[] cohort = new string[NumberOfCohorts];
                string[] per = new string[NumberOfCohorts];
                int i = 0;
                foreach (ResultsFromTheCohortForChart r in result)
                {
                    cohort[i] = r.CohortName;
                    per[i] = r.Correct.ToString();
                    i++;
                }

                string cat = "<categories font=\"Arial\" fontSize=\"11\" fontColor=\"000000\">";
                for (int j = 0; j < NumberOfCohorts; j++)
                {
                    cat = cat + "<category name=\"" + cohort[j] + "\" /> ";
                }

                cat = cat + "</categories>";

                string strd = "<dataset color=\"FDC12E\" alpha=\"70\">";
                for (int j = 0; j < NumberOfCohorts; j++)
                {
                    strd = strd + "<set value=\" " + per[j] + "\" color=' " + ReturnColor(j) + "'/> ";
                }

                strd = strd + "</dataset>";

                strXml = strXml + cat + strd + "</graph>";
            }

            return strXml;
        }

        protected string ReturnColor(int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "#CC99FF";
            }

            if (i % 3 == 2)
            {
                result = "#99CCFF";
            }

            if (i % 3 == 0)
            {
                result = "#FDC12E";
            }

            return result;
        }


        public bool IsProgramOfStudyInstitutionFilterVisible()
        {
            return (CurrentContext.UserType == UserType.SuperAdmin || IsMultipleProgramofStudyAssignedToAdmin(CurrentContext.UserId));

        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
