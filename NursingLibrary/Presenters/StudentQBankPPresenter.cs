using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentQBankPPresenter : StudentPresenter<IQBankPView>
    {
        #region Constructors

        public StudentQBankPPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser(int testSubgroup)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "NCLEX Test Info Cumulative performance")
                .Add("Test Sub Group ", testSubgroup.ToString())
                .Write();
            #endregion
            return AppController.GetTestsNCLEXInfoForTheUser().Where(tst => tst.TestSubGroup == testSubgroup);
        }

        public virtual void OnAnalysisClick()
        {
            AppController.ShowPage(PageDirectory.QbankP, null, null);
        }

        public virtual void OnListReviewClick()
        {
            AppController.ShowPage(PageDirectory.QbankR, null, null);
        }

        public virtual void OnQBankCreateClick()
        {
            AppController.ShowPage(PageDirectory.Qbank, null, null);
        }

        public virtual void OnSampleClick()
        {
            Student.QuizOrQBank = TestType.Quiz; //// Assuming Q means Quiz.
            Student.TestSubGroup = 2; //// No enum for this!
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            ////View.SetTestResultinks();
            IEnumerable<ProgramResults> perfList = AppController.GetQBankTestPerformanceByProductIDChartType(2, 1); //// no idea what 2 stands for -- from the static code study of the existing page.
            IEnumerable<ProgramResults> detailsList4 = AppController.GetQBankTestPerformanceByProductIDChartType(4, 0);
            IEnumerable<ProgramResults> detailsList5 = AppController.GetQBankTestPerformanceByProductIDChartType(5, 0);

            View.BindData(perfList, detailsList4, detailsList5);
        }

        #endregion Methods
    }
}