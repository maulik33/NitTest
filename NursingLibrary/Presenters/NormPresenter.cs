using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class NormPresenter : AuthenticatedPresenterBase<INormView>
    {
        private readonly ICMSService _cmsService;
        private readonly IStudentService _studentService;

        public NormPresenter(ICMSService service, IStudentService studentService)
            : base(Module.CMS)
        {
            _cmsService = service;
            _studentService = studentService;
        }

        public void ShowNormingDetails()
        {
            IDictionary<int, CategoryDetail> category = null;
            string Category = ((CategoryName)View.Category.ToInt()).ToString();
            string categoryName = Category;
            if (Category.StartsWith("PN"))
            {
                categoryName = Category.Remove(0, 2);
            }

            int testId = View.TestId;
            if (!string.IsNullOrEmpty(Category) && Category != "-1")
            {
                CategoryName name = (CategoryName)Enum.Parse(typeof(CategoryName), Category);
                IDictionary<CategoryName, Category> allCategories = GetAllCategories(View.ProgramodStudy);
                if (allCategories.ContainsKey(name))
                {
                    category = allCategories[name].Details;
                    IEnumerable<Norm> overallNorm = _cmsService.GetNorms(testId, "OverAll");
                    IEnumerable<Norm> specificNorms = _cmsService.GetNorms(testId, categoryName);
                    View.PopulateNorm(category, overallNorm, specificNorms);
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _cmsService.GetListOfAllProducts();
        }

        public IEnumerable<Test> GetTestsByProduct(int productId)
        {
            return _cmsService.GetTests(productId);
        }

        public List<Norm> GetNorms(int testId, string category)
        {
            string Category = ((CategoryName)category.ToInt()).ToString();
            if (Category.StartsWith("PN"))
            {
                Category = Category.Remove(0, 2);
            }

            List<Norm> overallNorm = _cmsService.GetNorms(testId, "OverAll").ToList();
            List<Norm> Norms = _cmsService.GetNorms(testId, Category).ToList();
            if (overallNorm != null && overallNorm.Count > 0)
            {
                Norms.Add(overallNorm[0]);
            }

            return Norms;
        }

        public void SaveNorms(List<Norm> norms)
        {
            if (norms != null && norms.Count > 0)
            {
                foreach (Norm n in norms)
                {
                    _cmsService.SaveNorm(n);
                }

                _cmsService.UpdateTestsReleaseStatusById(norms[0].TestId, "E");
            }
        }

        public IDictionary<CategoryName, Category> GetAllCategories(int programofStudy)
        {
            return _cmsService.GetCategories(programofStudy);
        }

        public void InitializeNormDetails()
        {
            View.PopulateControls(_cmsService.GetListOfAllProducts(), _cmsService.GetProgramofStudies());
        }

        public void PopulateTest(int testType, int programofStudyId)
        {
            View.PopulateTest(_cmsService.GetTests(testType, programofStudyId));
        }

        public void GetCategories(int programofStudy)
        {
            View.PopulateCategories(_cmsService.GetCategories(programofStudy));
        }
    }
}
