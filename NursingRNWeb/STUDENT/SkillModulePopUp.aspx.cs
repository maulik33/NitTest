using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
    public partial class SkillModulePopUp : StudentBasePage<ISkillModulePopUpView, SkillModulePopUpPresenter>, ISkillModulePopUpView
    {
        public int OrderNumber
        {
            get { return hfOrderNumber.Value.ToInt(); }
        }

        public bool IsProductionApplication
        {
            get { return Global.IsProductionApp; }
        }

        public bool FromIntroReview { get; set; }

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

        public int ReturnIntro
        {
            get
            {
                return redirectIntro.Value.ToInt();
            }

            set
            {
                redirectIntro.Value = value.ToString();
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
        public static void UpdateSkillModuleVideoStatus(string SMUserVideoId)
        {
            var presenter = Resolve<SkillModulePopUpPresenter>() as SkillModulePopUpPresenter;
            if (presenter == null)
            {
                throw new InvalidOperationException("Presenter could not be resolved");
            }

            presenter.UpdateSkillModuleVideoStatus(SMUserVideoId.ToInt());
        }

        public void DisplayVideo(SMUserVideoTransaction VideoTransDetails)
        {
            int skillsModuleVideoTextPosition = VideoTransDetails.SkillsModuleVideo.TextPosition ? 1 : 0;
            hfOrderNumber.Value = VideoTransDetails.SMOrder.ToString();
            hfSMUserVideoId.Value = VideoTransDetails.SMUserVideoId.ToString();
            hfSMTitle.Value = VideoTransDetails.SkillsModuleVideo.Title;

            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), string.Empty, "ChangePanelSize();", true);

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

            if (skillsModuleVideoTextPosition == (int)SMTextPosition.Up)
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

        public void ShowSMPage(int retval)
        {
            ReturnIntro = retval;
            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), string.Empty, "WinClose('" + retval + "')", true);

            //// mdlPopup.Hide();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            if (Request.QueryString["FromIntroReview"] != null)
            {
                if (Request.QueryString["FromIntroReview"] != string.Empty && Request.QueryString["FromIntroReview"] == "1")
                {
                    FromIntroReview = true;
                }
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["SMID"] != null)
                {
                    if (Request.QueryString["SMID"] != string.Empty)
                    {
                        TestId = Convert.ToInt32(Request.QueryString["SMID"]);
                    }
                }

                #region Trace Information
                TraceHelper.Create(TraceToken, "Navigated to SkillModule PopUp")
                    .Add("Product ID", Student.ProductId.ToString())
                    .Write();
                Presenter.OnViewInitialized();
                #endregion
                Presenter.ViewCurrentVideo(Student.SMUserId);
            }

            Presenter.OnViewLoaded();
        }

        protected void BtnNextClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnNextButtonClick();
        }

        protected void BtnBackClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnBackButtonClick();
        }

        private string GetVideoObject(string videoLink, double height, double width)
        {
            string tokenId = GetToken();

            #region String builder format

            StringBuilder sbSwf = new StringBuilder();
            sbSwf.Append("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"653\" height=\"367\" id=\"Playback\" align=\"left\">");
            sbSwf.Append("<param name=\"movie\" value=\"/student/flash/Playback.swf\"/>");
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
            sbSwf.Append("<object type=\"application/x-shockwave-flash\" data=\"/student/flash/Playback.swf\" width=\"653\" height=\"367\">");
            sbSwf.Append("<param name=\"movie\" value=\"/student/flash/Playback.swf\" />");
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