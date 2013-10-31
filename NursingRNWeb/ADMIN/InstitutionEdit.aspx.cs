using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class ADMIN_InstitutionEdit : PageBase<IInstitutionView, InstitutionPresenter>, IInstitutionView
{
    public int ActionType { get; set; }

    public int IID { get; set; }

    public UserType UserTypeValue { get; set; }

    public string ErrorMessage { get; set; }

    public string Name { get; set; }

    public string ProgramOfStudy
    {
        get;
        set;
    }

    public Institution Institution
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

    public Program Program
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

    public IEnumerable<TimeZones> TimeZones
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

    #region IInstitutionEditView Members

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramOfStudy(IEnumerable<Country> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void BindData(IEnumerable<TimeZones> timeZones, IEnumerable<Program> nurPrograms, IEnumerable<ProgramofStudy> programOfStudies)
    {
        btnDelete.Attributes.Add("onclick", " return confirm('Are you sure that you want to delete the institution?')");
        BindDropDownList(ddTimeZone, "Description", "TimeZoneID", timeZones);
        BindDropDownList(ddProgram, "ProgramName", "ProgramID", nurPrograms);
        ddProgram.Items.Insert(0, new ListItem("Not Selected", "0"));
        Presenter.PopulateCountryList();
        if (ActionType == (int)UserAction.Add)
        {
            lblTitle.Text = "Add/View/Edit > Institution Details";
            lblSubTitle.Text = "Add Additional Institution";
            btnDelete.Visible = false;
            btnManageContacts.Visible = false;
            lblProgOfStudyTxt.Visible = true;
            Presenter.PopulateStates(KTPApp.DefaultAddressCountry.ToInt(), 0);
            ddlProgramOfStudy.DataSource = programOfStudies;
            ddlProgramOfStudy.DataTextField = "ProgramofStudyName";
            ddlProgramOfStudy.DataValueField = "ProgramofStudyId";
            ddlProgramOfStudy.DataBind();
        }

        if (ActionType == (int)UserAction.Edit)
        {
            lblTitle.Text = "View/Edit >> Institution List >> Edit a Institution ";
            lblSubTitle.Text = "Use this page to edit an Institution: ";
            btnDelete.Visible = true;
            lblProgOfStudy.Visible = true;
            lblProgOfStudyTxt.Visible = false;
            ddlProgramOfStudy.Visible = false;
            Institution institution = Presenter.GetInstitutionById();
            lblProgOfStudy.Text = string.Format("- {0}", institution.ProgramofStudyName);
            txtName.Text = institution.InstitutionName;
            txtDescription.Text = institution.Description;
            txtCenterName.Text = institution.CenterId;
            txtContactName.Text = institution.ContactName;
            txtPhone.Text = institution.ContactPhone;
            ddTimeZone.SelectedValue = Convert.ToString(institution.TimeZone);
            txtIP.Text = institution.IP;
            if (!string.IsNullOrEmpty(institution.ContractualStartDate))
            {
                txtContractualStartDate.Text = institution.ContractualStartDate.ToDateTime().ToShortDateString();
            }

            txtAnnotation.Text = HttpUtility.HtmlDecode(institution.Annotation);
            txtFacility.Text = Convert.ToString(institution.FacilityID);
            ddProgram.SelectedValue = Convert.ToString(institution.ProgramID);
            rdInstitutionStatus.SelectedValue = institution.Status;
            rbStudentPayLink.SelectedValue = institution.PayLinkEnabled == true ? "1" : "0";
            txtEmail.Text = institution.Email;
            chkSecurity.Checked = Convert.ToBoolean(institution.ProctorTrackSecurityEnabled);
        }
    }
    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    public void BindData()
    {
        throw new NotImplementedException();
    }

    public void ShowInstitutionResults(IEnumerable<Institution> Institutions, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
        if (ActionType == (int)UserAction.Add)
        {
            ucAddress.PopulateCountry(country, true);
        }
        else
        {
            ucAddress.PopulateCountry(country, false);
        }
    }

    public void PopulateState(IEnumerable<State> state)
    {
        ucAddress.PopulateState(state);
    }

    public void PopulateAddress(Address address)
    {
        ucAddress.SetAddressInformation(address, false);
    }

    public void PopulateInstitutionContacts(IEnumerable<InstitutionContact> institutionContacts)
    {
        ucInstitutionContact.PopulateContacts(new InstitutionContact(), institutionContacts);
    }

    public void ExportInstitutions(IEnumerable<Institution> reportData, ReportAction reportAction)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgram(IEnumerable<Program> programs)
    {
        BindDropDownList(ddProgram, "ProgramName", "ProgramID", programs);
        ddProgram.Items.Insert(0, new ListItem("Not Selected", "0"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucInstitutionContact.OnSaveInstitutionContactClick += new EventHandler(ucInstitutionContact_OnSaveInstitutionContactClick);
        ucInstitutionContact.OnRefreshInstitutionContact += new EventHandler(ucInstitutionContact_OnRefreshInstitutionContact);
        ucInstitutionContact.OnDeleteInstitutionContact += new EventHandler(ucInstitutionContact_OnDeleteInstitutionContact);
        ucInstitutionContact.OnUpdateInstitutionContactClick += new EventHandler(ucInstitutionContact_OnUpdateInstitutionContactClick);
        ucAddress.OnCountrySelectionChange += new EventHandler<ItemSelectedEventArgs>(ucAddress_OnCountrySelectionChange);
        Label7.Visible = false;
        Presenter.InitializeProperties();
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Institution Edit Page")
                                .Add("Institution Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            ContractualStartDate.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtContractualStartDate.ClientID + "','cal','width=250,height=225,left=270,top=180')");
            ContractualStartDate.Style.Add("cursor", "pointer");
            Presenter.ShowInstitutionDetails();
            Presenter.PopulateInstitutionContacts();
        }

        if ((int)UserTypeValue == (int)UserType.InstitutionalAdmin)
        {
            txtName.Enabled = false;
            txtCenterName.Enabled = false;
            txtContactName.Enabled = false;
            txtDescription.Enabled = false;
            txtFacility.Enabled = false;
            txtIP.Enabled = false;
            txtPhone.Enabled = false;
            ddProgram.Enabled = false;
            ddTimeZone.Enabled = false;
            btnDelete.Visible = false;
            btnSave.Visible = false;
        }
        if ((int) UserTypeValue == (int) UserType.SuperAdmin)
            divSecurity.Visible = true;
        
    }

    protected void ddlProgramOfStudy_SelectedIndexChanged(Object sender, EventArgs e)
    {
        Presenter.PopulateProgram(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (!IsNumeric(txtFacility.Text))
        {
            Label7.Visible = true;
            return;
        }

        if (!checkIP())
        {
            lblErr.Text = "Wrong IP format.";
            return;
        }
        Name = txtName.Text.Trim();
       
        if (Presenter.ActionType == UserAction.Add)
        {
            ProgramOfStudy = ddlProgramOfStudy.SelectedItemsText;
        }
    
        if (ValidateContractualDate() && !Presenter.ValidateInstitution())
        {
            if (rf_name.IsValid && RequiredFieldValidator1.IsValid && (txtIP.Text == string.Empty || RegularExpressionValidator1.IsValid))
            {
                Institution institution = new Institution();
                institution.ProgramOfStudyId = ddlProgramOfStudy.SelectedValue.ToInt();
                institution.InstitutionName = txtName.Text.Trim();
                institution.Description = txtDescription.Text.Trim();
                institution.CenterId = txtCenterName.Text.Trim();
                institution.ContactName = txtContactName.Text.Trim();
                institution.ContactPhone = txtPhone.Text.Trim();
                institution.TimeZone = Convert.ToInt32(ddTimeZone.SelectedValue);
                institution.IP = txtIP.Text.Trim();
                institution.ProgramID = Convert.ToInt32(ddProgram.SelectedValue);
                institution.Status = rdInstitutionStatus.SelectedValue;
                institution.PayLinkEnabled = rbStudentPayLink.SelectedValue == "1" ? true : false;
                institution.AnnotationSave = txtAnnotation.Text;
                institution.ContractualStartDate = txtContractualStartDate.Text;
                var _addressInfo = ucAddress.GetAddressInformation();
                if (txtFacility.Text.Trim() == string.Empty)
                {
                    institution.FacilityID = 0;
                    _addressInfo.AddressId = 0;
                }
                else
                {
                    institution.FacilityID = Convert.ToInt32(txtFacility.Text);
                }

                if (ActionType == 2)
                {
                    institution.InstitutionId = IID;
                    institution.UpdateUser = Presenter.CurrentContext.UserId;
                }

                if (ActionType == 1)
                {
                    
                    institution.InstitutionId = 0;
                    institution.CreateUser = Presenter.CurrentContext.UserId;
                }

                institution.InstitutionAddress = _addressInfo;
                institution.Email = txtEmail.Text;
                institution.ProctorTrackSecurityEnabled = chkSecurity.Checked ? 1:0;
                Presenter.SaveInstitution(institution);
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.DeleteInstitution();
    }

    protected bool checkIP()
    {
        string[] IPs = txtIP.Text.Split((Char)10);
        for (int i = 0; i < IPs.Length; ++i)
        {
            if (IPs[i] == string.Empty)
            {
                return true;
            }

            string[] IP = IPs[i].Split('.');
            if (IP.Length != 4)
            {
                return false;
            }
            else
            {
                for (int j = 0; j < IP.Length - 1; ++j)
                {
                    if (IsNumeric(IP[j]))
                    {
                        int number = Convert.ToInt32(IP[j]);
                        if (number < 0 && number > 256)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                string LIP = IP[IP.Length - 1].Replace("\r", string.Empty);
                if (LIP != "*")
                {
                    string[] lastIP = LIP.Split('-');
                    for (int k = 0; k < lastIP.Length; ++k)
                    {
                        if (IsNumeric(lastIP[k]))
                        {
                            int lastNumber = Convert.ToInt32(lastIP[k] == string.Empty ? "0" : lastIP[k]);
                            if (lastIP[k] == string.Empty || lastNumber <= 0 || lastNumber > 255)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (lastIP.Length == 2)
                    {
                        if (Convert.ToInt32(lastIP[0].Trim() == string.Empty ? "0" : lastIP[0].Trim()) >= Convert.ToInt32(lastIP[1].Trim() == string.Empty ? "0" : lastIP[1].Trim()))
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    protected void btnManageContacts_Click(object sender, EventArgs e)
    {
        mdlPopup.Show();
    }

    private void ucInstitutionContact_OnSaveInstitutionContactClick(object sender, EventArgs e)
    {
        InstitutionContact institutionContact = ((SaveInstitutionContactEventArgs)e).InstitutionContactInfo;
        Presenter.SaveInstitutionContact(institutionContact);
        Presenter.PopulateInstitutionContacts();
    }

    private void ucInstitutionContact_OnRefreshInstitutionContact(object sender, EventArgs e)
    {
        Presenter.PopulateInstitutionContacts();
    }

    private void ucInstitutionContact_OnDeleteInstitutionContact(object sender, EventArgs e)
    {
        InstitutionContact institutionContact = ((SaveInstitutionContactEventArgs)e).InstitutionContactInfo;
        int contactId = institutionContact.ContactId;
        Presenter.DeleteInstitution(contactId);
    }

    private void ucInstitutionContact_OnUpdateInstitutionContactClick(object sender, EventArgs e)
    {
        InstitutionContact institutionContact = ((SaveInstitutionContactEventArgs)e).InstitutionContactInfo;
        institutionContact.InstitutionId = IID;
        Presenter.UpdateInstitutionContact(institutionContact);
        Presenter.PopulateInstitutionContacts();
    }

    private void BindDropDownList<T>(DropDownList dropDownList, string textField, string valueField, IEnumerable<T> listToBind)
    {
        dropDownList.DataSource = listToBind;
        dropDownList.DataTextField = textField;
        dropDownList.DataValueField = valueField;
        dropDownList.DataBind();
    }

    private bool IsNumeric(string number)
    {
        try
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (!char.IsNumber(number, i))
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private void ucAddress_OnCountrySelectionChange(object sender, ItemSelectedEventArgs e)
    {
        var _countriesWithState = KTPApp.CountriesWithStates;
        var _countryId = e.SelectedValue.ToInt();

        if (_countriesWithState.Contains(_countryId.ToString()))
        {
            ucAddress.ShowStateAsTextBox(false);
            Presenter.PopulateStates(_countryId, 0);
        }
        else
        {
            ucAddress.ShowStateAsTextBox(true);
        }
    }

    private bool ValidateContractualDate()
    {
        DateTime contractualdate;
        bool IsValid = true;
        if (!string.IsNullOrEmpty(txtContractualStartDate.Text))
        {
            if (!DateTime.TryParse(txtContractualStartDate.Text, out contractualdate))
            {
                IsValid = false;
                lblErrContractualdate.Text = "Please enter valid contractual start date.";
            }
        }

        return IsValid;
    }
}
