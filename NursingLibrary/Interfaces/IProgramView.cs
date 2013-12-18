using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IProgramView : IWebView
    {
        string Name { get; set; }
        
        string Description { get; set; }
        
        string SearchText { get; set; }
        
        int ProgramId { get; set; }
        
        int ProductId { get; set; }
        
        int TestId { get; set; }
        
        int Bundle { get; set; }
        
        int Type { get; set; }

        int ProgramOfStudyId { get; }

        string ProgramOfStudyName { get; set; }

        IEnumerable<AssetGroup> AssetGroups { get; set; }

        void PopulateTests(IEnumerable<Test> tests);
        
        void PopulateAssignedTest(IEnumerable<ProgramTestDates> tests);
        
        void PopulateProducts(IEnumerable<Product> products);
        
        void RefreshPage(Program program, UserAction action, string title, string subTitle,
            bool hasDeletePermission, bool hasAddPermission);
    
        void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData);

        void ShowBulkProgramResults(IEnumerable<Program> programs, string selectedProgramIds, SortInfo sortMetaData);

        void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies);

        void PopulateAssets(IEnumerable<Asset> assets);

        void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroup);

        void ShowMessage(string errorMsg);
    }
}
