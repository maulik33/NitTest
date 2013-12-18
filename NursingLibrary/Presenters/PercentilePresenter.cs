using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class PercentilePresenter : AuthenticatedPresenterBase<IPercentileView>
    {
        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public PercentilePresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public override void RegisterAuthorizationRules()
        {
            // throw new NotImplementedException();
        }

        public override void RegisterQueryParameters()
        {
            // throw new NotImplementedException();
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void InitializeNormingDetails()
        {
            View.NormingDetails = _cmsService.GetNormings(View.TestID).ToList();
        }

        public void SaveNorming()
        {
            _cmsService.SaveNorming(View.NormingProp);
            _cmsService.UpdateTestsReleaseStatusById(View.NormingProp.TestId, "E");
        }

        public void DeleteNorming()
        {
            _cmsService.DeleteNormingById(View.NormingID);
            _cmsService.UpdateTestsReleaseStatusById(View.TestID, "E");
            View.RefreshNormingDetails();
        }

        public void InitializePercentile()
        {
            IEnumerable<ProgramofStudy> programofStudies = _cmsService.GetProgramofStudies();
            IEnumerable<Product> products = _cmsService.GetListOfAllProducts();
            IEnumerable<Test> tests = new List<Test>();
            View.PopulateProducts(programofStudies, products, tests);
        }

        public IEnumerable<Test> GetTests(int productId, int programOfStudy)
        {
            return _cmsService.GetTests(productId, 0, false, programOfStudy);
        }
    }
}
