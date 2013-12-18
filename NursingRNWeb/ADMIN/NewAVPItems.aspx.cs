using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_NewAVPItems1 : PageBase<IAVPItemView, AVPItemPresenter>, IAVPItemView
{
    public string TestName
    {
        get
        {
            return txtAVPName.Text;
        }

        set
        {
            txtAVPName.Text = value;
        }
    }

    public int TestId { get; set; }

    public string URL
    {
        get
        {
            return txtUrl.Text;
        }

        set
        {
            txtUrl.Text = value;
        }
    }

    public string PopWidth
    {
        get
        {
            return txtWidth.Text;
        }

        set
        {
            txtWidth.Text = value;
        }
    }

    public string PopHeight
    {
        get
        {
            return txtPopHeight.Text;
        }

        set
        {
            txtPopHeight.Text = value;
        }
    }

    public string HeaderLabelText
    {
        get
        {
            return lblHeaderText.Text;
        }

        set
        {
            lblHeaderText.Text = value;
        }
    }

    public string Sort
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

    public bool Confirm()
    {
        StringBuilder message = new StringBuilder();

        if (!Enum.IsDefined(typeof (ProgramofStudyType), ddlProgramOfStudy.SelectedValue.ToInt()))
        {
            message.Append("You must choose a program of study.<br/>");
        }

        if (string.IsNullOrEmpty(txtAVPName.Text))
        {
            message.AppendLine("AVP Item Name is required.<br/>");
        }

        if (string.IsNullOrEmpty(txtUrl.Text))
        {
            message.AppendLine("AVP Item Link is required.<br/>");
        }
        //now we need to always check this in case in editing they've changed the name to one that already exists
        if (Presenter.IsCustomTestExisted(txtAVPName.Text, ddlProgramOfStudy.SelectedValue.ToInt(), TestId))
        {
            message.AppendLine("AVP Item Name already exists -- please use another name.<br/>");
        }

        if (!IsIntegerType(txtPopHeight.Text))
        {
            message.AppendLine("Dimension must be a integer.<br/>");
        }

        if (!IsIntegerType(txtWidth.Text))
        {
            message.AppendLine("Dimension must be a integer.<br/>");
        }

        if (message.Length > 0)
        {
            errorMessage.Text = message.ToString();
            errorMessage.Visible = true;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RefreshPage(System.Collections.Generic.IEnumerable<Test> tests, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        Presenter.InitializePropValues();
        
        if (!IsPostBack)
        {
            Presenter.ShowAVPItemDetails();
        }
    }
    public void RenderProgramOfStudyUI(IEnumerable<ProgramofStudy> programofStudies, int selectedProgramOfStudyId,
                                       bool programOfStudiesDropDownEnabled)
    {
        if (programOfStudiesDropDownEnabled)
        {
            ddlProgramOfStudy.ShowNotSelected = true;
            ddlProgramOfStudy.NotSelectedText = "Selection Required";

        }
        else
        {
            if (Enum.IsDefined(typeof(ProgramofStudyType), selectedProgramOfStudyId))
            {
                lblProgramofStudyVal.Text = ((ProgramofStudyType)selectedProgramOfStudyId).ToString();
            }
            else
            {
                throw new InvalidEnumArgumentException("Unexpected Program of Study Type " + selectedProgramOfStudyId.ToString());
            }
        }
        ddlProgramOfStudy.Visible = programOfStudiesDropDownEnabled;
        lblProgramofStudyVal.Visible = !programOfStudiesDropDownEnabled;
        ddlProgramOfStudy.DataSource = programofStudies;
        ddlProgramOfStudy.DataTextField = "ProgramofStudyName";
        ddlProgramOfStudy.DataValueField = "ProgramofStudyId";
        ddlProgramOfStudy.DataBind();
        ddlProgramOfStudy.SelectedValue = selectedProgramOfStudyId.ToString();

    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit AVP Item")
                            .Add("Test Id", TestId.ToString())
                            .Add("Test Name", TestName.ToString())
                            .Add("URL", URL)
                            .Write();
        #endregion
        Presenter.SaveAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        Presenter.NavigateToItemsPage();
    }

    private bool IsIntegerType(string value)
    {
        int tempValue = 0;
        bool isInteger = false;
        if (int.TryParse(value, out tempValue))
        {
            isInteger = true;
        }

        return isInteger;
    }
}
