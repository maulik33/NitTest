using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ProgramView : PageBase<IProgramView, ProgramPresenter>, IProgramView
{
    public string Name
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            lblProgramName.Text = value;
        }
    }

    public string Description
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            lblDescription.Text = value;
        }
    }

    public string SearchText { get; set; }

    public int ProgramId { get; set; }

    public int ProductId { get; set; }

    public int TestId { get; set; }

    public int Bundle { get; set; }

    public int Type { get; set; }

    public int ProgramOfStudyId
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string ProgramOfStudyName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<AssetGroup> AssetGroups
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroup)
    {
        throw new NotImplementedException();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region IProgramView Members

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void ShowBulkProgramResults(IEnumerable<Program> programs, string selectedProgramIds, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateAssets(IEnumerable<Asset> assets)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string errorMsg)
    {
        throw new NotImplementedException();
    }
    public void RefreshPage(Program program, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        lblProgramName.Text = program.ProgramName;
        lblDescription.Text = program.Description;
        lblProgramOfStudyName.Text = program.ProgramOfStudyName;
        ProgramId = program.ProgramId;
    }

    public void PopulateAssignedTest(IEnumerable<ProgramTestDates> tests)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.ShowProgramDetails();
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Program View Page")
                    .Add("Program Id", Presenter.Id.ToString())
                    .Write();
            #endregion
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Edit);
    }

    protected void lnkTests_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToAssignTests(ProgramId.ToString());
    }

    protected void lnkNew_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Add);
    }
}
