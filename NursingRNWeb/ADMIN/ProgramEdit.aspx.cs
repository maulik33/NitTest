using System;
using System.Collections.Generic;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingLibrary.DTC;

public partial class ADMIN_ProgramEdit : PageBase<IProgramView, ProgramPresenter>, IProgramView
{
    public string Name
    {
        get
        {
            return txtProgramName.Text;
        }

        set
        {
            txtProgramName.Text = value;
        }
    }

    public string Description
    {
        get
        {
            return txtProgramD.Text;
        }

        set
        {
            txtProgramD.Text = value;
        }
    }

    public int ProgramId { get; set; }

    public string SearchText { get; set; }

    public int ProductId { get; set; }

    public int TestId { get; set; }

    public int Bundle { get; set; }

    public int Type { get; set; }

    public int ProgramOfStudyId
    {
        get
        {
            return ddlProgramOfStudy.Visible ? ddlProgramOfStudy.SelectedValue.ToInt() : hfProgramOfStudyId.Value.ToInt();
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


    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region IProgramView Members

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateProducts(IEnumerable<Product> products)
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

    public void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroup)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Program program, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {

        lblTitle.Text = title;
        lblSubTitle.Text = subTitle;
        btDelete.Visible = hasDeletePermission;
        addbtn.Visible = hasAddPermission;

        if (action == UserAction.Edit)
        {
            btDelete.Attributes.Add("onclick", " return confirm('Are you sure that you want to delete the program?')");
            lblProgramOfStudyName.Visible = true;
            lblProgramOfStudyName.Text = program.ProgramOfStudyName;
            hfProgramOfStudyId.Value = program.ProgramOfStudyId.ToString();
            rfvProgramOfStudy.Visible = ddlProgramOfStudy.Visible = false;
            txtProgramName.Text = program.ProgramName;
            txtProgramD.Text = program.Description;
            
            btnCopy.Visible = false;
        }
        else if (action == UserAction.Copy)
        {
            lblProgramOfStudyName.Visible = true;
            lblProgramOfStudyName.Text = program.ProgramOfStudyName;
            hfProgramOfStudyId.Value = program.ProgramOfStudyId.ToString();
            rfvProgramOfStudy.Visible = ddlProgramOfStudy.Visible = false;
            lblCopyDetail.Text = "Copying Program '" + program.ProgramName + "' to a new Program.";
            btDelete.Visible = false;
            addbtn.Visible = false;
            trCopy.Visible = true;
            btnCopy.Visible = hasAddPermission;
        }

    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateAssignedTest(IEnumerable<ProgramTestDates> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ddlProgramOfStudy.DataSource = programOfStudies;
        ddlProgramOfStudy.DataTextField = "ProgramOfStudyName";
        ddlProgramOfStudy.DataValueField = "ProgramOfStudyId";
        ddlProgramOfStudy.DataBind();
    }

    public void ShowMessage(string errorMsg)
    {
        lblErrorMsg.Text = errorMsg;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Program Edit Page")
                    .Add("Program Id", Presenter.Id.ToString())
                    .Write();
            #endregion
            Presenter.ShowProgramDetails();
        }
    }

    protected void btDelete_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.DeleteProgram();
    }

    protected void addbtn_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.SaveProgram();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        Presenter.CopyProgram();
    }


}
