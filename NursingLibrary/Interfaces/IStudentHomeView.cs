using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentHomeView
    {
        void EnableIntegratedTestCtrls();
        
        void EnableFocusedReviewTestCtrld();
        
        void EnableNclexTestCtrls(bool enableNclexAvp);
        
        void SetCaseStudiesControls(IEnumerable<CaseStudy> dtCase);
        
        void SetheaderCss();

        void SetSkillsModulesControls(IEnumerable<Test> skills);

        void PopulateSkills(IEnumerable<Test> skills);

        void PopulateCase(IEnumerable<CaseStudy> dtCase);

        void CreateSkillsModulesDetails(int testId);

        void PopulateDashBoardLinks(IEnumerable<AssetDetail> assetDetails);

        void SetControlVisibility(IEnumerable<AssetGroup> assetGroups);
    }
}
