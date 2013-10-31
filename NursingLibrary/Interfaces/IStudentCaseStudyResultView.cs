using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentCaseStudyResultView
    {
        #region Properties
        CaseModuleScore CaseModuleScoreDetails
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        void ReadXml();

        #endregion Methods
    }
}
