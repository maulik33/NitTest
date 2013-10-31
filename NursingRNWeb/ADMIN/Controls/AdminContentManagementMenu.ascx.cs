using System;
using System.Web.UI;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class AdminContentManagement : UserControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Global.IsProductionApp)
        {
            HyperLink6.Visible = false;
            hlUploadQuestions.Visible = false;
            hlUploadTopics.Visible = false;
        }
        else
        {
            HyperLink6.Visible = true;
            HyperLink6.Visible = true;
        }
    }
}