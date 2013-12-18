using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.STUDENT
{
    public partial class SkillsModule : StudentBasePage<ISkillModulesView, SkillModulesPresenter>, ISkillModulesView
    {
        public int SkillsModuleId { get; set; }

        public int OrderNumber
        {
            get { return hfOrderNumber.Value.ToInt(); }
        }

        public bool IsProductionApplication
        {
            get { return Global.IsProductionApp; }
        }

        public int TestId
        {
            get
            {
                return hfTestId.Value.ToInt();
            }

            set
            {
                hfTestId.Value = value.ToString();
            }
        }

        public bool EnableBackButton
        {
            set
            {
                if (value == true)
                {
                    btnBack.Style.Add("display", "block");
                }
                else
                {
                    btnBack.Style.Add("display", "none");
                }
            }
        }

        public bool EnableNextButton
        {
            set
            {
                if (value == true)
                {
                    btnNext.Style.Add("display", "block");
                    btnNextDisabled.Style.Add("display", "none");
                }
                else
                {
                    btnNext.Style.Add("display", "none");
                    btnNextDisabled.Style.Add("display", "block");
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static void UpdateSkillModuleStatus(string SMUserVideoId)
        {
            var presenter = Resolve<SkillModulesPresenter>() as SkillModulesPresenter;
            if (presenter == null)
            {
                throw new InvalidOperationException("Presenter could not be resolved");
            }

            presenter.UpdateSkillModuleStatus(SMUserVideoId.ToInt());
        }

        public void DisplayVideo(SMUserVideoTransaction VideoTransDetails)
        {
            hfOrderNumber.Value = VideoTransDetails.SMOrder.ToString();
            hfSMUserVideoId.Value = VideoTransDetails.SMUserVideoId.ToString();
            lblSMTitle.Text = VideoTransDetails.SkillsModuleVideo.Title;
            double height = 432; // hfHeight.Value.ToDouble() * 0.40;
            double width = 768;  // hfWidth.Value.ToDouble() * 0.60;

            if (VideoTransDetails.SkillsModuleVideo.Type == (int)SMType.Video)
            {
                SMVideo.InnerHtml = GetVideoObject(VideoTransDetails.SkillsModuleVideo.MP4, height, width);
                SMVideo.Style.Add("display", "block");
            }
            else
            {
                SMVideo.Style.Add("display", "none");
            }

            if (VideoTransDetails.SkillsModuleVideo.TextPosition.ToInt() == (int)SMTextPosition.Up)
            {
                DescriptionTop.Visible = true;
                DescriptionTop.InnerHtml = ShowPicture(VideoTransDetails.SkillsModuleVideo.Text);
                DescriptionBottom.Visible = false;
            }
            else
            {
                DescriptionBottom.Visible = true;
                DescriptionBottom.InnerHtml = ShowPicture(VideoTransDetails.SkillsModuleVideo.Text);
                DescriptionTop.Visible = false;
            }

            updPnlDetail.Update();
        }

        public void BindAvailableQuizzesGrid(IEnumerable<UserTest> skillsAvailableQuizzes)
        {
            skillsAvailableQuizzes = skillsAvailableQuizzes.OrderByDescending(r => r.TestStarted);
            GridViewAvailableQuizzes.DataSource = skillsAvailableQuizzes;
            GridViewAvailableQuizzes.DataBind();
        }

        public void BindSkillsModulesGrid(IEnumerable<Test> skills)
        {
            GridViewSkillsModule.DataSource = skills;
            GridViewSkillsModule.DataBind();
        }

        public void BindSuspendedQuizzesGrid(IEnumerable<UserTest> skillsSuspendedQuizzes)
        {
            skillsSuspendedQuizzes = skillsSuspendedQuizzes.OrderByDescending(r => r.TestStarted);
            GridViewSuspendedQuizzes.DataSource = skillsSuspendedQuizzes;
            GridViewSuspendedQuizzes.DataBind();
        }

        public void BindViewQuizResultsGrid(IEnumerable<UserTest> tests)
        {
            tests = tests.OrderByDescending(r => r.TestStarted);
            GridViewQuizResults.DataSource = tests;
            GridViewQuizResults.DataBind();
        }

        public void ShowSMPage()
        {
            mdlPopup.Hide();
        }

        public void ShowPopUpPage(int SkillsModuleId)
        {
            Presenter.CreateSkillsModulesDetails(SkillsModuleId);
            if (Student.SMUserId > 0)
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), string.Empty, "openPopUpWithSize('" + SkillsModuleId + "')", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.Create(TraceToken, "Navigated to Test Review Page")
                    .Add("Product ID", Student.ProductId.ToString())
                    .Write();
                Presenter.OnViewInitialized();
                #endregion
            }

            Presenter.OnViewLoaded();
        }

        protected void GridViewSkillsModule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            SkillsModuleId = GridViewSkillsModule.DataKeys[index].Value.ToInt();
            //// check if the videos are present or not           
            ShowPopUpPage(SkillsModuleId);
        }

        protected void GridViewAvailableQuizzes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            SkillsModuleId = GridViewAvailableQuizzes.DataKeys[index].Value.ToInt();
            Presenter.RepeatQuizzes(Student.UserId, SkillsModuleId);
        }

        protected void GridViewSuspendedQuizzes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resume")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var row = GridViewSuspendedQuizzes.Rows[index];
                Presenter.OnGridViewSuspendedQuizzesRowCommand((GridViewSuspendedQuizzes.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0, (GridViewSuspendedQuizzes.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0, GridViewSuspendedQuizzes.DataKeys[row.RowIndex].Values["SuspendType"] as string, true);
            }
        }

        protected void GridViewQuizResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gv = (GridView)e.CommandSource;
            var rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            Presenter.OnGridViewQuizResultsRowCommand((gv.DataKeys[rowIndex].Values["UserTestId"] as int?) ?? 0, (gv.DataKeys[rowIndex].Values["TestId"] as int?) ?? 0, e.CommandName == "Review");
        }

        protected void BtnNextClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnNextButtonClick();
        }

        protected void BtnBackClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnBackButtonClick();
        }

        protected void btnViewTextItemHide_PreRender(object sender, EventArgs e)
        {
        }

        protected void GridViewAvailableQuizzes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.Cells[1].Text = @"Available";
                    break;
            }
        }

        private string GetVideoObject(string videoLink, double height, double width)
        {
            string tokenId = GetToken();

            #region String builder format

            StringBuilder sbSwf = new StringBuilder();
            sbSwf.Append("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"768\" height=\"432\" id=\"Playback\" align=\"left\">");
            sbSwf.Append("<param name=\"movie\" value=\"flash/Playback.swf\"/>");
            sbSwf.Append("<param name=\"quality\" value=\"high\" />");
            sbSwf.Append("<param name=\"bgcolor\" value=\"#cccccc\" />");
            sbSwf.Append("<param name=\"play\" value=\"true\" />");
            sbSwf.Append("<param name=\"loop\" value=\"true\" />");
            sbSwf.Append("<param name=\"wmode\" value=\"window\" />");
            sbSwf.Append("<param name=\"scale\" value=\"showall\" />");
            sbSwf.Append("<param name=\"menu\" value=\"true\" />");
            sbSwf.Append("<param name=\"devicefont\" value=\"false\" />");
            sbSwf.Append("<param name=\"salign\" value=\"\" />");
            sbSwf.Append("<param name=\"allowScriptAccess\" value=\"sameDomain\" />");
            sbSwf.Append("<param name=\"allowFullScreen\" value=\"true\" />");

            sbSwf.Append("<param name=FlashVars value=\"vidPathVar=");
            sbSwf.Append(videoLink);
            sbSwf.Append("&vidBasePathVar=");
            sbSwf.Append(KTPApp.SMVideoBasePath);
            sbSwf.Append("&vidTokenKeyVar=");
            sbSwf.Append(tokenId);
            sbSwf.Append("\" />");
            sbSwf.Append("<object type=\"application/x-shockwave-flash\" data=\"flash/Playback.swf\" width=\"768\" height=\"432\">");
            sbSwf.Append("<param name=\"movie\" value=\"Playback.swf\" />");
            sbSwf.Append(" <param name=\"quality\" value=\"high\" />");
            sbSwf.Append("<param name=\"bgcolor\" value=\"#cccccc\" />");
            sbSwf.Append("<param name=\"play\" value=\"true\" />");
            sbSwf.Append("<param name=\"loop\" value=\"true\" />");
            sbSwf.Append("<param name=\"wmode\" value=\"window\" />");
            sbSwf.Append("<param name=\"scale\" value=\"showall\" />");
            sbSwf.Append("<param name=\"menu\" value=\"true\" />");
            sbSwf.Append("<param name=\"devicefont\" value=\"false\" />");
            sbSwf.Append("<param name=\"salign\" value=\"\" />");
            sbSwf.Append("<param name=\"allowScriptAccess\" value=\"sameDomain\" />");
            sbSwf.Append("<param name=\"allowFullScreen\" value=\"true\" />");

            sbSwf.Append("<param name=FlashVars value=\"vidPathVar=");
            sbSwf.Append(videoLink);
            sbSwf.Append("&vidBasePathVar=");
            sbSwf.Append(KTPApp.SMVideoBasePath);
            sbSwf.Append("&vidTokenKeyVar=");
            sbSwf.Append(tokenId);
            sbSwf.Append("\" />");
            sbSwf.Append("  <a href=\"http://www.adobe.com/go/getflash\">");
            sbSwf.Append("  <img src=\"http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif\" alt=\"Get Adobe Flash player\" />");
            sbSwf.Append("   </a>");
            sbSwf.Append("  </object>");
            sbSwf.Append(" </object>");
            return sbSwf.ToString();
            #endregion
        }

        private string GetToken()
        {
            string akamaiTokenServiceURL = KTPApp.AkamaiTokenServiceUrl;
            string token = string.Empty;
            try
            {
                HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(akamaiTokenServiceURL);
                XmlDocument tokenDoc = new XmlDocument();

                using (HttpWebResponse response = (HttpWebResponse)tokenRequest.GetResponse())
                {
                    tokenDoc.Load(response.GetResponseStream());
                }

                XmlNode node = tokenDoc.SelectSingleNode("//response/payload/token");

                if (node != null && node.InnerText != null)
                {
                    token = node.InnerText;
                }
            }
            catch (Exception)
            {
                ////throw new Exception("");
            }

            return token;
        }

        private string ShowPicture(string smText)
        {
            smText = smText.Trim();
            int intQ = smText.Trim().IndexOf("<Picture=", 0);
            if (intQ > 0)
            {
                int intE = smText.Trim().IndexOf("/>", intQ);
                string leftPart = smText.Substring(0, intQ);
                string pictureName = smText.Substring(intQ + 9, intE - 9 - intQ);
                string rightPart = smText.Substring(intE + 2);

                smText = string.Format("{0}<P><img src=\"..\\Content\\{1}\"/></P>{2}", leftPart, pictureName, rightPart);
            }

            return smText;
        }
    }
}