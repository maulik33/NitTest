using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_SetOverride : PageBase<IOverrideView, OverridePresenter>, IOverrideView
{
    public string CohortIds
    {
        get { return lbxCohort.SelectedValuesText; }
    }

    public string AssignIds { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void PopulateProgramOfStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions,true);
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void ShowStudentTests(IList<UserTest> studentTests, SortInfo sortMetaData)
    {
        if (studentTests.Count > 3000)
        {
            RowCountWarningLabel.Visible = true;
            RowCountLabel.Visible = false;
            studentTests.RemoveAt(3000);
        }
        else
        {
            RowCountWarningLabel.Visible = false;
            RowCountLabel.Visible = true;
            RowCountLabel.Text = string.Format("Search returned {0} Tests for the specified filter options.", studentTests.Count);
        }

        TimezoneHintLabel.Visible = (studentTests.Count > 0) ? true : false;
        if (SearchMultiView.ActiveViewIndex == 0)
        {
            if (hdResume.Value != string.Empty)
            {
                foreach (var item in hdResume.Value.Split('|'))
                {
                    if (item != string.Empty)
                    {
                        var _activeUserTestId = from ai in studentTests
                                                where (ai.UserTestId == Convert.ToInt32(item))
                                                select ai;
                        if (_activeUserTestId.FirstOrDefault() != null)
                        {
                            _activeUserTestId.FirstOrDefault().Active = true;
                        }
                    }
                }
            }

            hdResumePagelevel.Value = string.Empty;

            gvUsers.DataSource = KTPSort.Sort<UserTest>(studentTests, sortMetaData);
            gvUsers.DataBind();
            gvUsers.Visible = true;

            if (Convert.ToInt32(hdActiveCount.Value) > 0 && Convert.ToInt32(hdCheckListCount.Value) > 0 && Convert.ToInt32(hdActiveCount.Value) == Convert.ToInt32(hdCheckListCount.Value))
            {
                CheckBox headerchk = (CheckBox)gvUsers.HeaderRow.FindControl("headerSelectAll");
                headerchk.Checked = true;
            }

             btnResume.Visible = true;
        }
        else
        {
            gvDisplayDeletedTests.DataSource = KTPSort.Sort<UserTest>(studentTests, sortMetaData);
            gvDisplayDeletedTests.DataBind();
            gvDisplayDeletedTests.Visible = true;
            btnResume.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Presenter.PopulateProgramOfStudies();
            ddProgramofStudy.ShowNotSelected = true;
        }
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetInstitutionByProgramofStudy(Convert.ToInt32(ddProgramofStudy.SelectedValue));
        lbxCohort.ClearData();
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvUsers.Visible = false;
        lbxCohort.ClearData();
        if (ddInstitution.SelectedValuesText != string.Empty)
        {
            Presenter.GetActiveCohortList(ddInstitution.SelectedValuesText);
            SearchUsers();
        }
    }

    protected void lbxCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchUsers();
    }

    protected void grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (SearchMultiView.ActiveViewIndex == 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                var productId = ((NursingLibrary.Entity.UserTest)e.Row.DataItem).Test.ProductId.ToString();
                var remaining = ((NursingLibrary.Entity.UserTest)e.Row.DataItem).TimeRemaining.ToString();
                var timeRemaining = Convert.ToInt32(remaining == string.Empty ? "0" : remaining);
                 e.Row.Cells[13].Text = e.Row.Cells[13].Text == "1" ? "Completed" : "In Complete";
                 var cResume = (CheckBox)e.Row.FindControl("ch");
                 CheckBox headerchk = (CheckBox)gvUsers.HeaderRow.FindControl("headerSelectAll");
                 cResume.Visible = false;
                 cResume.ToolTip = string.Empty;
               
               if (e.Row.Cells[13].Text == "Completed" && productId.Equals("1") && timeRemaining > 0) 
               {
                   var numberOfQuestions = Convert.ToInt32(((NursingLibrary.Entity.UserTest)e.Row.DataItem).NumberOfQuestions.ToString());
                   var suspendQuestion = Convert.ToInt32(((NursingLibrary.Entity.UserTest)e.Row.DataItem).SuspendQuestionNumber.ToString());
                   var answertrack = ((NursingLibrary.Entity.UserTest)e.Row.DataItem).Test.Question.AnswserTrack;
                   var orderedIndexes = ((NursingLibrary.Entity.UserTest)e.Row.DataItem).Test.Question.OrderedIndexes;
                   var lastAnswer = Convert.ToInt32(((NursingLibrary.Entity.UserTest)e.Row.DataItem).LastQuestionAnswer.ToString());
                   var userTestId = ((NursingLibrary.Entity.UserTest)e.Row.DataItem).UserTestId;
                   StringBuilder _strUserIds = new StringBuilder();
                   if (suspendQuestion < numberOfQuestions)
                   {
                       cResume.Visible = true;
                       cResume.ToolTip = "Resume";
                       headerchk.Visible = true;
                       hdCheckListCount.Value = Convert.ToString(Convert.ToInt32(hdCheckListCount.Value) + 1);
                       _strUserIds.Append(userTestId + "|");
                       hdResumePagelevel.Value = hdResumePagelevel.Value + _strUserIds.ToString();
                   }
                   else if (suspendQuestion == numberOfQuestions && string.IsNullOrEmpty(answertrack) && string.IsNullOrEmpty(orderedIndexes) && (lastAnswer == 0))
                   {
                       cResume.Visible = true;
                       cResume.ToolTip = "Resume";
                       headerchk.Visible = true;
                       hdCheckListCount.Value = Convert.ToString(Convert.ToInt32(hdCheckListCount.Value) + 1);
                       _strUserIds.Append(userTestId + "|");
                       hdResumePagelevel.Value = hdResumePagelevel.Value + _strUserIds.ToString();
                   }
                   else
                   {
                       cResume.Visible = false;
                       cResume.ToolTip = string.Empty;
                   }

                   var _isActive = ((HiddenField)e.Row.FindControl("Active")).Value.ToBool();

                   if (cResume != null)
                   {
                       if (_isActive)
                       {
                           cResume.Checked = true;
                           hdActiveCount.Value = Convert.ToString(Convert.ToInt32(hdActiveCount.Value) + 1);
                       }
                       else
                       {
                           cResume.Checked = false;
                       }

                       cResume.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + headerchk.ClientID + "')");
                   }
               }

                WebControl _delTest = (WebControl)e.Row.Cells[0].Controls[0];
                _delTest.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete the Test?')");
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[12].Text = e.Row.Cells[12].Text == "1" ? "Completed" : "In Complete";
            }
        }
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var _id = this.gvUsers.Rows[e.RowIndex].Cells[1].Text.ToInt();
        Presenter.DeleteTest(_id);
        SearchMultiView.ActiveViewIndex = 0;
        SearchUsers();
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchUsers();
    }

    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        SearchUsers();
    }

    protected void grid_PageIndexChanged(Object sender, EventArgs e)
    {
        if (SearchMultiView.ActiveViewIndex == 0)
        {
            gvUsers.Visible = true;
        }
        else
        {
            gvDisplayDeletedTests.Visible = true;
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        SearchMultiView.ActiveViewIndex = 0;
        hdActiveCount.Value = "0";
        hdCheckListCount.Value = "0";
        SearchUsers();
    }

    protected void SearchDeletedTests_Click(object sender, EventArgs e)
    {
        SearchMultiView.ActiveViewIndex = 1;
        hdActiveCount.Value = "0";
        hdCheckListCount.Value = "0";
        SearchUsers();
    }

    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
            BindResumeHiddenValue();
            hdActiveCount.Value = "0";
            hdCheckListCount.Value = "0";
        }
        else if (e.CommandName == "Sort" || e.CommandName == "Delete")
        {
            hdActiveCount.Value = "0";
            hdCheckListCount.Value = "0";
            hdResume.Value = string.Empty;
            hdResumePagelevel.Value = string.Empty;
        }
    }

    protected void Resume_Click(object sender, EventArgs e)
    {
       BindResumeHiddenValue();
       if (hdResume.Value != string.Empty)
       {
           Presenter.ResumeTest(hdResume.Value.ToString());
           SearchMultiView.ActiveViewIndex = 0;
           hdResume.Value = string.Empty;
           hdResumePagelevel.Value = string.Empty;
           hdActiveCount.Value = "0";
           hdCheckListCount.Value = "0";
           SearchUsers();
       }
    }

    protected void UpdateAssignedItem(object sender, EventArgs e)
    {
        CheckBox chkSampleStatus = sender as CheckBox;
        bool sample = chkSampleStatus.Checked;
        GridViewRow row = chkSampleStatus.NamingContainer as GridViewRow;
        var userTestId = row.Cells[1].Text;
        if (sample)
        {
            BindResumeHiddenValue();
        }
        else
        {
            if (hdResume.Value != string.Empty)
            {
                hdResume.Value = hdResume.Value.Replace(userTestId + "|", string.Empty);
                hdActiveCount.Value = Convert.ToString(Convert.ToInt32(hdActiveCount.Value) - 1);
            }
        }

        if (Convert.ToInt32(hdActiveCount.Value) > 0 && Convert.ToInt32(hdCheckListCount.Value) > 0 && Convert.ToInt32(hdActiveCount.Value) == Convert.ToInt32(hdCheckListCount.Value))
        {
            CheckBox headerchk = (CheckBox)gvUsers.HeaderRow.FindControl("headerSelectAll");
            headerchk.Checked = true;
        }
     }

    private void BindResumeHiddenValue()
    {
        StringBuilder _strAssignIds = new StringBuilder();
        var assignedIDs = from GridViewRow msgRow in gvUsers.Rows
                          where ((CheckBox)msgRow.FindControl("ch")).Checked
                          select msgRow;
        foreach (var item in assignedIDs)
        {
            var userTestId = item.Cells[1].Text;
            bool isUserTestId = false;
            if (hdResume.Value != string.Empty)
            {
                isUserTestId = hdResume.Value.Contains(userTestId);
            }

            if (!isUserTestId)
            {
                _strAssignIds.Append(userTestId + "|");
                hdActiveCount.Value = Convert.ToString(Convert.ToInt32(hdActiveCount.Value) + 1);
            }
        }

        hdResume.Value = hdResume.Value + _strAssignIds.ToString();
    }

    private void SearchUsers()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Set Override.")
                            .Add("FirstName", txtFirstName.Text)
                            .Add("Last Name", txtLastName.Text)
                            .Add("User Name", txtUserName.Text)
                            .Add("Test Name", txtTestName.Text)
                            .Add("Active View Index", SearchMultiView.ActiveViewIndex.ToString())
                            .Write();
        #endregion

        var _cohorts = string.Empty;
        if (CohortIds != null)
        {
            _cohorts = CohortIds.Replace(",", "|");
        }

        _cohorts = lbxCohort.SelectedValue == "0" ? string.Empty : _cohorts.ToString();

        Presenter.ShowList(
            (ddInstitution.SelectedValue.ToInt() > 0) ? ddInstitution.SelectedValue.ToInt() : 0,
            txtFirstName.Text.Trim(),
            txtLastName.Text.Trim(),
            txtUserName.Text.Trim(),
            txtTestName.Text.Trim(),
            ShowOnlyIncompleteCheckBox.Checked,
            SearchMultiView.ActiveViewIndex,
            hdnGridConfig.Value, _cohorts);
    }
}