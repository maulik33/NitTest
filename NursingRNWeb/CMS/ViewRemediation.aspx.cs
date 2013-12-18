using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_ViewRemediation : PageBase<IRemediationView, RemediationPresenter>, IRemediationView
{
    public string DisplayMessage
    {
        set { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region Unimplemented methods
    public void PopulateControls(Remediation remediation)
    {
        throw new NotImplementedException();
    }

    public void PopulateQuestions(System.Collections.Generic.IEnumerable<Question> questions)
    {
        throw new NotImplementedException();
    }

    public void DisplayUploadedTopics(IEnumerable<Remediation> validRemediations, IEnumerable<Remediation> invalidRemediations, IEnumerable<Remediation> duplicateTopics, string filePath)
    {
        throw new NotImplementedException();
    }
    #endregion

    public void PopulateLippincotts()
    {
        switch (Presenter.ComeFrom)
        {
            case "EditQuestion.aspx":
                if (Presenter.QId > 0)
                {
                    ShowRemediations();
                }
                else
                {
                    Presenter.NavigateToComeFromPage();
                }

                break;
            case "EditR.aspx":
                ShowRemediations();
                break;
            default:
                Presenter.NavigateToComeFromPage();
                break;
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to View Remediation Page")
                                .Add("Remediation Id", Presenter.Id.ToString())
                                .Write();
            #endregion
        }

        Presenter.ShowRemediationDetails();
    }

    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        Presenter.NavigateToComeFromPage();
    }

    private void ShowRemediations()
    {
        int qId = Presenter.QId;
        Question question = Presenter.GetQuestionById(qId);
        int remediationId = question.RemediationId;
        Remediation remediation = Presenter.GetRemediationById(remediationId);
        Table table = new Table();
        IEnumerable<Lippincott> lippincotts = Presenter.GetLippincotts(qId);
        if (remediation != null)
        {
            table = GetLippincottTableControl(qId, remediation.Explanation, lippincotts);
        }

        Lippincott.Controls.Add(table);
    }

    private Table GetLippincottTableControl(int QID, string remediationHtml, IEnumerable<Lippincott> lippincotts)
    {
        Table tb = new Table();
        if (remediationHtml != string.Empty && lippincotts.Count() != 0)
        {
            AddImagesToTable(tb, "~/Temp/images/min.jpg", "~/Temp/Images/exp01.gif");
            AddTextToTable(tb, remediationHtml);

            foreach (Lippincott lc in lippincotts)
            {
                AddTextToTable(tb, "&nbsp");
                AddImagesToTable(tb, "~/Temp/images/min.jpg", "~/Temp/Images/exp02.gif");
                AddTextToTable(tb, lc.LippincottTitle);
                AddTextToTable(tb, lc.LippincottExplanation);
                AddTextToTable(tb, "&nbsp");
                AddImagesToTable(tb, "~/Temp/images/min.jpg", "~/Temp/Images/exp03.gif");
                AddTextToTable(tb, lc.LippincottTitle2);
                AddTextToTable(tb, lc.LippincottExplanation2);
            }
        }

        return tb;
    }

    private void AddTextToTable(Table tb, string text)
    {
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Text = text;
        tr.Cells.Add(tc);
        tb.Rows.Add(tr);
    }

    private void AddImagesToTable(Table tb, string imageUrl1, string imageUrl2)
    {
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        Image img = new Image();
        img.ImageUrl = imageUrl1;
        tc.Controls.Add(img);
        Image img1 = new Image();
        img1.ImageUrl = imageUrl2;
        tc.Controls.Add(img);
        tr.Cells.Add(tc);
        tb.Rows.Add(tr);
    }
}
