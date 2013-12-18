using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_InstitutionView : PageBase<IInstitutionView, InstitutionPresenter>, IInstitutionView
{
    public Institution Institution { get; set; }

    public Program Program { get; set; }

    public IEnumerable<TimeZones> TimeZones { get; set; }

    int IInstitutionView.IID
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

    int IInstitutionView.ActionType
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

    public UserType UserTypeValue
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

    public string ErrorMessage
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

    public string Name
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

    public string ProgramOfStudy
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
    #region IInstitutionView Members

    public void BindData()
    {
        if (Institution != null)
        {
            lblProgOfStudy.Text = Institution.ProgramofStudyName;
            lblIName.Text = Institution.InstitutionName;
            lblIDescription.Text = Institution.Description;
            lblCenterName.Text = Institution.CenterId;
            lblContacName.Text = Institution.ContactName;
            lblPhone.Text = Institution.ContactPhone;
            lblEmail.Text = Institution.Email;
            lblContractualStartDate.Text = Institution.ContractualStartDate;
            lblAnnotation.Text = Institution.Annotation;
            if (Program != null)
            {
                lblProgram.Text = Program.ProgramName;
            }

            lblIP.Text = Institution.IP.ToString().Replace(Convert.ToString((char)13), "<br/>");
            ddTimeZone.DataSource = TimeZones;
            ddTimeZone.DataTextField = "Description";
            ddTimeZone.DataValueField = "TimeZoneID";
            ddTimeZone.DataBind();
            ddTimeZone.SelectedValue = Institution.TimeZone.ToString();
            lblTimeZone.Text = ddTimeZone.SelectedItem.Text;

            lblFacility.Text = Convert.ToString(Institution.FacilityID);

            lblSecurityStatus.Text = (Institution.ProctorTrackSecurityEnabled == 1) ? "Enabled" : "Disabled";
        }
    }

    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region UNImplemented Method/Properties
    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void BindData(IEnumerable<TimeZones> timeZones, IEnumerable<Program> nurPrograms, IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void ShowInstitutionResults(IEnumerable<Institution> Institutions, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
        ucAddress.PopulateCountry(country, false);
    }

    public void PopulateState(IEnumerable<State> state)
    {
    }

    public void PopulateAddress(Address address)
    {
        ucAddress.SetAddressInformation(address, true);
    }

    public void ExportInstitutions(IEnumerable<Institution> reportData, ReportAction reportAction)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutionContacts(IEnumerable<InstitutionContact> institutionContacts)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgram(IEnumerable<Program> programs)
    {
        throw new ArgumentException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Institution View Page")
                                .Add("Institution Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.OnIntitutionListInitialized();
        }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Edit);
    }

    protected void lbNew_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Add);
    }
}
