using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_ReadLippincottTemplate : PageBase<ILippincottView, LippioncottPresenter>, ILippincottView
{
    private const string FileFolder = "~/LippincottTemplate/";

    public string AddMessage
    {
        set
        {
            errorMessage.Text = value;
        }
    }

    public string SearchCondition
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

    public string Mode
    {
        set { throw new NotImplementedException(); }
    }

    public string SerachTextBox
    {
        set { throw new NotImplementedException(); }
    }

    public int PageIndex
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

    public void ShowMessage()
    {
        errorMessage.Visible = true;
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region UnImplemented Method/Props

    public void OnViewInitialized()
    {
        throw new NotImplementedException();
    }

    public void SearchLippincott(IEnumerable<Lippincott> lippinCotts, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateQuestionList(IEnumerable<Lippincott> lippinCotts)
    {
        throw new NotImplementedException();
    }

    public void PopulateControls(IEnumerable<Remediation> remediations, Lippincott lippincott)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Read Lippincott Page");
            #endregion
            ListFiles();
        }
    }

    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        foreach (ListItem it in this.ListBox1.Items)
        {
            it.Selected = true;
        }
    }

    protected void btnRead_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem it in this.ListBox1.Items)
            {
                if (it.Selected)
                {
                    Presenter.ReadAndSaveLippincott(it.Value);
                }
            }
        }
        catch (Exception ex)
        {
            errorMessage.Text = ex.Message;
            errorMessage.Visible = true;
        }
    }

    protected void btnBackToLippincott_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToLippinCott();
    }

    private void ListFiles()
    {
        string[] f = System.IO.Directory.GetFiles(Server.MapPath(FileFolder), "*.htm");
        foreach (string s in f)
        {
            string[] st = s.Split('\\');
            string s1 = st[st.Length - 1];
            ListItem it = new ListItem(s1, s);
            this.ListBox1.Items.Add(it);
        }
    }

    #endregion
}