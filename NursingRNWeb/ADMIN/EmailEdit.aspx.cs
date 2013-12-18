using System;
using System.Collections.Generic;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_EmailEdit : ReportPageBase<IEmailReceiverView, EmailReceiverPresenter>, IEmailReceiverView
{
    public bool IsEmailEdit
    {
        get
        {
            return true;
        }
    }

    public bool IsEmailIdExistInQueryString
    {
        get;
        set;
    }

    public string EmailId
    {
        get
        {
            object o = this.ViewState["EmailId"];
            int number = 0;
            if (o == null || !int.TryParse(o.ToString(), out number))
            {
                return "-1";
            }
            else
            {
                return o.ToString();
            }
        }

        set
        {
            this.ViewState["EmailId"] = value;
        }
    }

    public string EmailTo
    {
        get
        {
            return string.Empty;
        }
    }

    public string EmailToStudentOrAdmin
    {
        get
        {
            return string.Empty;
        }
    }

    public bool CustomEmailToAdmins
    {
        get { throw new NotImplementedException(); }
    }

    public bool CustomEmailToStudents
    {
        get { throw new NotImplementedException(); }
    }

    public bool UserEmailToStudents
    {
        get { throw new NotImplementedException(); }
    }

    public bool UserEmailLocalAdmins
    {
        get { throw new NotImplementedException(); }
    }

    public bool UserEmailTechAdmins
    {
        get { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl();
    }

    #region IEmailReceiverView Methods


    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programOfStudies)
    {
    }

    public void PopulateCustomEmails(IEnumerable<Email> emails)
    {
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
    }

    public void PopulateAdmin(IEnumerable<Admin> admins)
    {
    }

    public void ShowSendEmailResult(string msg)
    {
    }

    public void ShowEmailDetails(Email email)
    {
        if (email != null)
        {
            TextBox1.Text = email.Title;
            TextBox2.Text = email.Body;
        }
        else
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
        }
    }
    #endregion

    public void SetControlsSpecial()
    {
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (!string.IsNullOrEmpty(Page.Request.QueryString["EmailId"]))
        {
            IsEmailIdExistInQueryString = true;
            EmailId = Page.Request.QueryString["EmailId"];
        }
        else
        {
            IsEmailIdExistInQueryString = false;
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Email Edit Page")
                                .Add("Email Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.GetEmailDetails();
        }
    }
   
    protected void Button2_Click(object sender, System.EventArgs e)
    {
        Presenter.SaveEmail(EmailId.ToInt(), TextBox1.Text, TextBox2.Text);
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        Presenter.NavigateToEmailReceiver();
    }
}
