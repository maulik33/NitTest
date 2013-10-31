using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;


public partial class LTIProviders : PageBase<ILtiProviderView, LtiProviderPresenter>, ILtiProviderView
{

    public override void PreInitialize()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsSuperAdmin();
        CheckIsKecUserValid();
        if (!Page.IsPostBack)
        {
            Presenter.GetLtiProviders();
        }
    }

    private void CheckIsSuperAdmin()
    {
        if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
        {
            Session.Abandon();
            Presenter.NavigateToLogin();
        }
    }

    private void CheckIsKecUserValid()
    {
        bool isValidUser = false;
        if (Session["kecUserName"] != null && Session["kecPassword"] != null)
        {
            isValidUser = Utilities.IsValidDomainUser(Session["kecUserName"].ToString(), Session["kecPassword"].ToString());

        }
        accountLoginDiv.Visible = !isValidUser;
        displayStringDiv.Visible = isValidUser;
    }

    protected void btnSubmit_Click(Object sender, EventArgs e)
    {
        var isValidUser = Utilities.IsValidDomainUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        if (isValidUser)
        {
            accountLoginDiv.Visible = false;
            displayStringDiv.Visible = true;
            errMsgLbl.Visible = false;
            Session["kecUserName"] = txtUserName.Text.Trim();
            Session["kecPassword"] = txtPassword.Text.Trim();
        }
        else
            errMsgLbl.Visible = true;
    }

    protected void SaveOrEdit(Object sender, EventArgs e)
    {
        string errorString;
        bool okToSave = OkToSave(out errorString);
        if (!okToSave)
        {
            ltiErrMsgLbl.Text = errorString;
            ltiErrMsgLbl.Visible = true;
            return;
        }
        LtiProvider ltiProvider = new LtiProvider();
        ltiProvider.ConsumerKey = ConsumerKeyTextBox.Text.Trim();
        ltiProvider.ConsumerSecret = ConsumerSecretTextBox.Text.Trim();
        ltiProvider.CustomParameters = CustomParametersTextBox.Text.Trim();
        ltiProvider.Description = DescriptionTextBox.Text.Trim();
        ltiProvider.Url = URLTextBox.Text.Trim();
        ltiProvider.Name = NameTextBox.Text.Trim();
        bool status;
        bool.TryParse(StatusDropDown.SelectedValue, out status);
        ltiProvider.Active = status;
        ltiProvider.Title = TitleTextBox.Text.Trim();

        Presenter.SaveLtiProvider(ltiProvider);
    }
    private bool OkToSave(out string errorString)
    {
        errorString = "";
        List<string> errors = new List<string>();
        if (String.IsNullOrWhiteSpace(NameTextBox.Text))
        {
            errors.Add("Name");
        }
        if (String.IsNullOrWhiteSpace(URLTextBox.Text))
        {
            errors.Add("Url");
        }
        if (String.IsNullOrWhiteSpace(ConsumerKeyTextBox.Text))
        {
            errors.Add("Consumer Key");
        }
        if (String.IsNullOrWhiteSpace(ConsumerSecretTextBox.Text))
        {
            errors.Add("Consumer Secret");
        }
        if (errors.Count > 0)
        {
            errorString = string.Concat("Please correct the following empty fields: ", string.Join(", ", errors));
            return false;
        }
        //now let's make sure the name isn't a duplicate
        if (Presenter.LtiNameExists(NameTextBox.Text.Trim()))
        {
            errorString = "LtiProvider name already exists. Please choose a different name.";
            return false;
        }
        return true;
    }

    protected void gvLTIProviders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton statusChangeButton = e.Row.FindControl("BtnStatusChange") as LinkButton;
            if (statusChangeButton != null)
            {
                statusChangeButton.Text = ((LtiProvider) e.Row.DataItem).Active == false ? "Activate" : "Inactivate";
            }
        }
    }


    protected void gvLTIProviders_RowEditing(object sender, GridViewEditEventArgs e)
    {
        var dataKey = gvLTIProviders.DataKeys[e.NewEditIndex];
        if (dataKey != null)
        {
            Presenter.NavigateToEdit(dataKey.Value.ToInt());
	}
    }

    protected void gvLTIProviders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (gvLTIProviders.DataKeys.Count > index)
        {
            var dataKey = gvLTIProviders.DataKeys[index];
            if (dataKey == null)
            {
                return;
            }
            switch (e.CommandName)
            {
                case "View":
                    Presenter.NavigateToView(dataKey.Value.ToInt());
                    break;
                case "ChangeStatus":
                    Presenter.ChangeLtiProviderStatus(dataKey.Value.ToInt());
                    break;
            }
        }
    }

    public void BindData(List<LtiProvider> ltiProviders)
    {
            gvLTIProviders.DataSource = ltiProviders;
            gvLTIProviders.DataBind();
            takeAction.Text = "Add Lti Provider";
            takeAction.Attributes["OnClick"] = "return confirm('Are you sure?')";
            goBackToListSubmit.Visible = false;
    }


    public void BindDataOnEdit(LtiProvider ltiProvider)
    {
        PopulateTextBoxes(ltiProvider);
        takeAction.Text = "Save Lti Provider";
        takeAction.Attributes["OnClick"] = "return confirm('Are you sure?')";
        ltiDivLabel.Text = "Edit Lti Provider";
    }

    public void BindDataOnView(LtiProvider ltiProvider)
    {
        PopulateTextBoxes(ltiProvider);

        NameTextBox.Enabled = false;
        URLTextBox.Enabled = false;
        ConsumerKeyTextBox.Enabled = false;
        ConsumerSecretTextBox.Enabled = false;
        CustomParametersTextBox.Enabled = false;
        DescriptionTextBox.Enabled = false;
        TitleTextBox.Enabled = false;
        StatusDropDown.Enabled = false;
        takeAction.Visible = false;
        ltiDivLabel.Text = "View Lti Provider";
    }

    private void PopulateTextBoxes(LtiProvider ltiProvider)
    {
        NameTextBox.Text = ltiProvider.Name;
        TitleTextBox.Text = ltiProvider.Title;
        URLTextBox.Text = ltiProvider.Url;
        ConsumerKeyTextBox.Text = ltiProvider.ConsumerKey;
        ConsumerSecretTextBox.Text = ltiProvider.ConsumerSecret;
        CustomParametersTextBox.Text = ltiProvider.CustomParameters;
        DescriptionTextBox.Text = ltiProvider.Description;
        StatusDropDown.SelectedValue = ltiProvider.Active.ToString().ToLower();
    }
    
        protected void GoBackToList(Object sender, EventArgs e)
        {
                Presenter.NavigateToList();
        }
    
    
    

}