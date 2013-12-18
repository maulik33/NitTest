using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentCaseStudyResultPresenter : StudentPresenter<IStudentCaseStudyResultView>
    {
        #region Constructors

        public StudentCaseStudyResultPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual bool CheckExistCaseModuleStudent(int CID, int MID, string SID)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "Case Study Result : Check Exist Case Module Student")
                .Add("CID ", CID.ToString())
                .Add("MID ", MID.ToString())
                .Add("SID ", SID.ToString())
                .Write();
            #endregion
            return AppController.CheckExistCaseModuleStudent(CID, MID, SID);
        }

        public virtual CaseSubCategory GetSubCategoryDetails()
        {
            return AppController.GetCaseSubCategory();
        }

        public virtual void NewCaseModuleScore(CaseModuleScore caseModuleScore)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "Case Study Result : New Case Module Score")
                .Add("Case Id ", caseModuleScore.CaseId.ToString())
                .Add("Correct ", caseModuleScore.Correct.ToString())
                .Add("Module Id ", caseModuleScore.ModuleId.ToString())
                .Add("Module StudentId ", caseModuleScore.ModuleStudentId.ToString())
                .Add("StudentId ", caseModuleScore.StudentId)
                .Add("Total ", caseModuleScore.Total.ToString())
                .Write();
            #endregion
            AppController.InsertModuleScore(caseModuleScore);
        }

        public virtual void NewCaseSubCategory(CaseSubCategory caseSubCategory)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "Case Study Result : New Case SubCategory")
                .Add("Case Id ", caseSubCategory.CategoryId.ToString())
                .Add("Category Name ", caseSubCategory.CategoryName.ToString())
                .Add("Module StudentId ", caseSubCategory.ModuleStudentId.ToString())
                .Add("SubCategory Id ", caseSubCategory.SubCategoryId.ToString())
                .Add("Correct ", caseSubCategory.Correct.ToString())
                .Add("Total ", caseSubCategory.Total.ToString())
                .Add("Category Id ", caseSubCategory.CategoryId.ToString())
                .Write();
            #endregion
            AppController.InsertSubCategory(caseSubCategory);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.CaseModuleScoreDetails = AppController.GetCaseModuleScore();
            View.ReadXml();
        }

        #endregion Methods
    }
}