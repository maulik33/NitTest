using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using System.Text;

namespace STUDENT
{
    public partial class StudentUserHome : StudentBasePage<IStudentHomeView, StudentHomePresenter>, IStudentHomeView
    {

        public void SetheaderCss()
        {
            // load different page depending on cohort type_Min
            var css = new HtmlLink { Href = "../css/front.css" };
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Attributes["media"] = "all";
            Header.Controls.Add(css);
        }

        public void EnableIntegratedTestCtrls()
        {
            lb_IT.Enabled = Student.IsIntegratedTest;
            lb_IT.CssClass = Student.IsIntegratedTest ? lb_IT.CssClass : "s7gray";
            lbl_R_I.Enabled = Student.IsIntegratedTest;
            lbl_R_I.CssClass = Student.IsIntegratedTest ? lbl_R_I.CssClass : "s7gray";
        }

        public void EnableFocusedReviewTestCtrld()
        {
            lbFRCreateOwnTest.Enabled = Student.IsFocusedReviewTest;
            lbFRCreateOwnTest.CssClass = Student.IsFocusedReviewTest ? lbFRCreateOwnTest.CssClass : "s7gray";
            lbFRTest.Enabled = Student.IsFocusedReviewTest;
            lbFRTest.CssClass = Student.IsFocusedReviewTest ? lbFRTest.CssClass : "s7gray";
            lbl_R_F.Enabled = Student.IsFocusedReviewTest;
            lbl_R_F.CssClass = Student.IsFocusedReviewTest ? lbl_R_F.CssClass : "s7gray";
            lbCreateFRQBankTest.Enabled = Student.IsFocusedReviewTest;
            lbCreateFRQBankTest.CssClass = Student.IsFocusedReviewTest ? lbl_R_F.CssClass : "s7gray";
        }

        public void EnableNclexTestCtrls(bool enableNclexAvp)
        {
            if (Student.IsNclexTest)
            {
                lbl_QBank.Enabled = Student.IsQbankTest || Student.IsQbankSampleTest;
                lbl_QBank.CssClass = lbl_QBank.Enabled ? lbl_QBank.CssClass : "s7gray";
                lbl_QT.Enabled = Student.IsTimedQbankTest;
                lbl_QT.CssClass = Student.IsTimedQbankTest ? lbl_QT.CssClass : "s7gray";
                LinkButton2.Enabled = enableNclexAvp;
            }
            else
            {
                lbl_QBank.Enabled = false;
                lbl_QBank.CssClass = "s7gray";
                lbl_QT.Enabled = false;
                lbl_QT.CssClass = "s7gray";
                LinkButton2.Enabled = false; ////LP
                LinkButton2.CssClass = "s7gray";
            }
        }

        public void SetCaseStudiesControls(IEnumerable<CaseStudy> dtCase)
        {
            foreach (CaseStudy drCase in dtCase)
            {
                var img = new Image { ImageUrl = "../images/bull.gif", Width = Unit.Pixel(7), Height = Unit.Pixel(9) };
                CaseID.Controls.Add(img);
                var lbt = new LinkButton { Text = drCase.CaseName, ID = drCase.CaseId.ToString(), CssClass = "s7" };
                lbt.Click += ButtonClick;
                CaseID.Controls.Add(lbt);
                var ltc = new LiteralControl { Text = "<br />" };
                CaseID.Controls.Add(ltc);
            }
        }

        public void SetSkillsModulesControls(IEnumerable<Test> skills)
        {
            foreach (Test drSkills in skills)
            {
                var img = new Image { ImageUrl = "../images/bull.gif", Width = Unit.Pixel(7), Height = Unit.Pixel(9) };
                SkillsID.Controls.Add(img);
                var lbt = new LinkButton { Text = drSkills.TestName, ID = drSkills.TestId.ToString(), CssClass = "s7" };
                lbt.Click += new EventHandler(lb_SM_Click);
                SkillsID.Controls.Add(lbt);
                var ltc = new LiteralControl { Text = "<br />" };
                SkillsID.Controls.Add(ltc);
            }
        }

        public void PopulateSkills(IEnumerable<Test> skills)
        {
            if (!IsPostBack)
            {
                ControlHelper.PopulateTests(ddSkills, skills);
            }
        }

        public void PopulateCase(IEnumerable<CaseStudy> dtCase)
        {
            if (!IsPostBack)
            {
                ControlHelper.PopulateCase(ddCaseID, dtCase);
            }
        }

        public void CreateSkillsModulesDetails(int testId)
        {
            Presenter.CreateSkillsModulesDetails(testId);
            if (Student.SMUserId > 0)
            {
                popupFrame.Attributes.Add("src", "SkillModulePopUp.aspx?smId=" + testId);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "openPopUp('" + testId + "')", true);
            }
        }

        public void PopulateDashBoardLinks(IEnumerable<AssetDetail> assetDetails)
        {
            if (assetDetails.Count() > 0)
            {
                IEnumerable<AssetDetail> leftLinks = assetDetails.Where(r => r.AssetLocationType == (int)AssetLocationType.Left);
                IEnumerable<AssetDetail> rightLinks = assetDetails.Where(r => r.AssetLocationType == (int)AssetLocationType.Right);
                StringBuilder dashBoardHtml = new StringBuilder();
                dashBoardHtml.Append("<table width=\"100%\" ><tr><td><b>Welcome back,&nbsp;<span style=\"width:173px;\">" + Presenter.Student.FirstName + "</span></b><br /> What would you like to do?</td>");
                if (rightLinks.Count() > 0)
                {
                    AppendDashBoardLink(dashBoardHtml, rightLinks.ToList()[0]);
                    dashBoardHtml.Append("</tr>");
                }

                for (int i = 0; i < assetDetails.Count(); i++)
                {
                    dashBoardHtml.Append("<tr>");

                    if (leftLinks.Count() > i)
                    {
                        AppendDashBoardLink(dashBoardHtml, leftLinks.ToList()[i]);
                    }
                    else
                    {
                        dashBoardHtml.Append("<td></td>");
                    }

                    if (rightLinks.Count() > (i + 1))
                    {
                        AppendDashBoardLink(dashBoardHtml, rightLinks.ToList()[i + 1]);
                    }
                    else
                    {
                        dashBoardHtml.Append("<td></td>");
                    }
                    dashBoardHtml.Append("</tr>");
                }
                dashBoardHtml.Append("</table>");

                top_info.InnerHtml = dashBoardHtml.ToString();
            }
        }

        public void SetControlVisibility(IEnumerable<AssetGroup> assetGroups)
        {
            if (Presenter.Student.ProgramofStudyId == (int)ProgramofStudyType.RN && assetGroups.Where(a => a.AssetGroupId == (int)AssetGroupType.CaseStudiesRn).Count() == 0)
            {
                pnlCaseStudy.Visible = false;
                Presenter.Student.IsCaseStudyEnabled = false;
            }
            else
            {
                Presenter.Student.IsCaseStudyEnabled = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to User Home Page");
                #endregion
                Presenter.OnViewInitialized();
                SetControlStatus();
            }

            Presenter.OnViewLoaded();
            ddSkills.Visible = Presenter.Student.IsSkillsModuleTest;
            SkillsID.Visible = Presenter.Student.IsSkillsModuleTest;
        }

        protected void lbFRTest_Click(object sender, EventArgs e)
        {
            Presenter.OnlbFRTest_Click();
        }

        protected void lb_IT_Click(object sender, EventArgs e)
        {
            Presenter.Onlb_IT_Click();
        }

        protected void lbl_R_F_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_R_F_Click();
        }

        protected void lbl_R_I_Click(object sender, EventArgs e)
        {
            Presenter.lbl_R_F_Click();
        }

        protected void lbIntegratedTests_Click(object sender, EventArgs e)
        {
            Presenter.OnlbIntegratedTests_Click();
        }

        protected void lbFocusedReview_Click(object sender, EventArgs e)
        {
            Presenter.OnlbFocusedReview_Click();
        }

        protected void lbNCLEXPrepRezults_Click(object sender, EventArgs e)
        {
            Presenter.OnlbNCLEXPrepRezults_Click();
        }


        protected void lbl_QT_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_QT_Click();
        }

        protected void lbl_QBank_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_QBank_Click();
        }

        protected void lbl_Q6_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_Q6_Click();
        }

        protected void lbl_Q7_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_Q7_Click();
        }

        protected void lbl_Q8_Click(object sender, EventArgs e)
        {
            Presenter.Onlbl_Q8_Click();
        }


        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Presenter.OnLinkButton2_Click();
        }

        protected void lbFRCreateOwnTest_Click(object sender, EventArgs e)
        {
            Presenter.OnFRCreateOwnTest_Click();
        }

        protected void lbFRSearchRemediation_Click(object sender, EventArgs e)
        {
            Presenter.OnlbFRSearchRemediation_Click();
        }

        protected void lbCreateFRQBankTest_Click(object sender, EventArgs e)
        {
            Presenter.OnCreateFRQBank();
        }

        protected void lb_SM_Click(object sender, EventArgs e)
        {
            Presenter.Onlb_SM_Click();
        }

        protected void ddSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            var skillsId = Convert.ToInt32(ddSkills.SelectedValue).ToInt();
            Presenter.OnlbSMTest_Click();
            CreateSkillsModulesDetails(skillsId);
        }

        protected void ddCaseID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var caseId = ddCaseID.SelectedValue;
            if (caseId != "-1")
            {
                string javaScript = Presenter.PopupDxr(caseId);
                ClientScript.RegisterStartupScript(typeof(Page), string.Empty, javaScript);
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var lnbt = (LinkButton)sender;
            string javaScript = Presenter.PopupDxr(lnbt.ID);
            ClientScript.RegisterStartupScript(typeof(Page), string.Empty, javaScript);
        }

        private void SetControlStatus()
        {
            if (Presenter.Student.ProgramofStudyId == (int)ProgramofStudyType.RN)
            {
                pnQuestionTrainer.Visible = true;
                pnlCaseStudy.Visible = true;
            }
            else if (Presenter.Student.ProgramofStudyId == (int)ProgramofStudyType.PN)
            {
                pnlCaseStudy.Visible = false;
                Panel panelcaseStudy = (Panel)Head11.FindControl("pnlCaseStudy");
                if (panelcaseStudy != null)
                {
                    panelcaseStudy.Visible = false;
                }
            }
        }

        private void AppendDashBoardLink(StringBuilder dashboardHtml, AssetDetail assetDetail)
        {
            string align = string.Empty;
            if (assetDetail.AssetLocationType == (int)AssetLocationType.Right)
            {
                align = "right";
            }

            dashboardHtml.Append("<td align=\"" + align + "\">");
            if (assetDetail.AssetLocationType == (int)AssetLocationType.Left)
            {
                dashboardHtml.Append("<img src=\"../images/icon_info.gif\" width=\"16\" height=\"16\" alt=\"\">");
            }
            dashboardHtml.Append("<a ");
            if (assetDetail.AssetLocationType == (int)AssetLocationType.Left)
            {
                dashboardHtml.Append("class=\"s2\" ");
            }

            dashboardHtml.Append("href=\"javascript:;\" onclick=\"window.open('" + assetDetail.AssetValue + "','cal' ,'scrollbars=yes,width=740,height=630,status=yes,fullscreen=yes,toolbar=no,menubar=no,location=no,position=245,20')\">");
            dashboardHtml.Append("<b>" + assetDetail.AssetName + "</b></a></td>");
        }
    }
}
