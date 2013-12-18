using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using WebControls;
using Action = NursingLibrary.Presenters.Action;

namespace STUDENT
{
    public partial class RemediationIntro : StudentBasePage<IStudentFRRemediationView, StudentFRRemediationPresenter>, IStudentFRRemediationView
    {
        #region Properties
        public string SystemId { get; set; }

        public string TopicId { get; set; }

        public int NumberOfRemediations { get; set; }

        public string CategoryIds { get; set; }

        public int ReuseMode { get; set; }

        public string ReviewRemName { get; set; }

        public int RemTime { get; set; }

        public string SystemName { get; set; }

        public string RemediationNumber
        {
            get { return txtRemediationNumber.Text.Trim(); }
        }

        public int Timer
        {
            get
            {
                var timer = 0;
                var time = mytimer.Value.Trim().Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt32(t));
                switch (time.Count())
                {
                    case 3:
                        timer = (int)new TimeSpan(time.ElementAtOrDefault(0), time.ElementAtOrDefault(1),
                                                    time.ElementAtOrDefault(2)).TotalSeconds;
                        break;
                    case 2:
                        timer =
                            (int)new TimeSpan(0, time.ElementAtOrDefault(0), time.ElementAtOrDefault(1)).TotalSeconds;
                        break;
                    case 1:
                        timer = (int)new TimeSpan(0, 0, time.ElementAtOrDefault(0)).TotalSeconds;
                        break;
                }

                return timer;
            }
        }

        public string Remaining
        {
            get;
            set;
        }

        public string TestName
        {
            set { lblTestName.Text = value; }
        }

        #endregion

        #region Public methods
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static void saveRemediationTime(string action, string timer, string qid, string userTestId)
        {
            switch (action)
            {
                case "Remediation":
                case "Review":
                    {
                        var presenter = Resolve<StudentIntroPresenter>() as StudentIntroPresenter;
                        if (presenter == null)
                        {
                            throw new InvalidOperationException("Presenter could not be resolved");
                        }

                        presenter.UpdateQuestionRemediation(timer, qid, userTestId, action);
                    }

                    break;
            }
        }

        public void PopulateSystems(IEnumerable<Systems> systems)
        {
        }

        public void PopulateTopics(IEnumerable<Topic> topics)
        {
        }

        public void SetModeDetails()
        {
        }

        public void HideShowPreviousIncorrectButton(bool p)
        {
        }

        public void BindRemediationList(IEnumerable<ReviewRemediation> getTestsForTheUser, SortInfo sortMetaData)
        {
        }

        public void PopulateFields(ReviewRemediation remediation)
        {
            txtRemediationNumber.Text = remediation.RemediationNumber.ToString();
            Label1.Text = remediation.TopicTitle;
            TestName = remediation.ReviewRemName;
            var lippinCotts = Presenter.GetLippinCott();

            if (Student.ReviewRemediation.RemReviewQuestionId != 0)
            {
                ShowLippincott(lippinCotts, remediation.ReviewExplanation);
            }
        }

        public void PopulateEnd(ReviewRemediation remediation, bool IsLastQuestion)
        {
            lblQNumber.Visible = false;

            btnQuit.Visible = false;
            if (IsLastQuestion)
            {
                ibCalc.Visible = false;
                CalBar.Visible = false;
                timer_up.Value = remediation.RemediatedTime.ToString();
                lbltimer.Visible = true;
                lbltimer.Text = @"Time:";
                lbltimer.Width = Unit.Pixel(30);
                imtimer.Visible = true;

                int minS = remediation.RemediatedTime / 60;
                int secS = remediation.RemediatedTime - (minS * 60);
                body.Attributes.Add("onload", "javascript:Up('" + minS + ":" + secS + "');");
            }
        }

        public void SetRemediationCtrl(int revRemId, string action)
        {
            body.Attributes.Add("onbeforeunload", "saveRemediationTime();");
            txtAction.Value = action;
            txtRemID.Value = revRemId.ToString();
        }

        public void PopulateRemediation(ReviewRemediation remReview)
        {
            timer_up.Value = remReview.RemediatedTime.ToString();
            lbltimer.Visible = true;
            lbltimer.Text = @"Time:";
            lbltimer.Width = Unit.Pixel(30);
            imtimer.Visible = true;

            int minS = remReview.RemediatedTime / 60;
            int secS = remReview.RemediatedTime - (minS * 60);
            body.Attributes.Add("onload", "javascript:Up('" + minS + ":" + secS + "');");

            lblQNumber.Text = txtRemediationNumber.Text + @" of " + Student.NumberOfQuestions;
            lblQNumber.Visible = true;
            question_main.Visible = true;
            intro_main.Visible = false;
            ibCalc.Visible = false;
            CalBar.Visible = false;
            btnQuit.Visible = true;
            btnQuit.ImageUrl = "../images/btn_btr.gif";

            btnBack.Visible = true;
            btnNext.Visible = true;
            btnNext.Enabled = true;
            btnNext.ToolTip = string.Empty;
            btnQuit.Visible = true;

            if (remReview.RemediationNumber == 1)
            {
                btnBack.Visible = false;
                btnBack.Enabled = false;
                btnNext.Enabled = true;
            }
            else
            {
                btnBack.Visible = true;
                btnBack.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        public void ShowLippincott(IEnumerable<Lippincott> lippincotts, string remediationHtml)
        {
            var ca = new LippincottCard(lippincotts.Select(lp => lp.LippincottTitle).ToArray(), lippincotts.Select(lp => lp.LippincottExplanation).ToArray(), lippincotts.Select(lp => lp.LippincottTitle2).ToArray(), lippincotts.Select(lp => lp.LippincottExplanation2).ToArray(), remediationHtml, true)
            {
                ImgRemediationUrl = "~/Images/exp01.gif",
                ImgLippincottUrl = "~/Images/exp02.gif",
                ImgLippincott2Url = "~/Images/exp03.gif"
            };
            Lippincott.Controls.Add(ca);

            if (Student.TestType == NursingLibrary.Interfaces.TestType.Integrated)
            {
                Explanation.InnerHtml = string.Empty;
                Explanation.Visible = false;
                remediation.Visible = true;
            }
        }

        public void PopulateEndForAllPages()
        {
            lblQNumber.Visible = false;
            btnBack.Visible = false;
            btnNext.Visible = false;
            btnQuit.Visible = false;
            imtimer.Visible = false;
            lbltimer.Visible = false;
            Image4.Visible = false;
            question_main.Visible = false;
            intro_title.InnerHtml = "<h3>End of Remediation</h3>";
            intro_text.Visible = true;
            intro_text.InnerHtml = "<div style='text-align:center;'>Congratulations, your remediations are completed. Click End Remediation to continue.</div>";

            ibIntro_S.Visible = true;
            ibIntro_S.ImageUrl = "../images/btn_end.gif";
            ibIntro_S.AlternateText = @"End Remediation";
            intro_main.Visible = true;
            ibCalc.Visible = false;
            CalBar.Visible = false;
        }

        #endregion

        #region Protected methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtAction.Value = string.Empty;
            Remaining = remaining.Value;
            Presenter.OnViewLoaded();
            if (ContinueTiming.Value == "1")
            {
                remaining.Value = Remaining;
            }
        }

        protected void BtnQuitClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnQuitClick();
        }

        protected void BtnBackClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnBackClick();
        }

        protected void BtnNextClick(object sender, ImageClickEventArgs e)
        {
            body.Attributes.Clear();
            BlockCopyPaste();
            Presenter.OnNextClick(btnNext.Visible);
        }

        protected void IbIntroSClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbIntroSClick();
        }

        protected void LnkExplanationClick(object sender, EventArgs e)
        {
            var literal = new Literal
            {
                Text = @"<div><script type=""text/javascript"">EnableNextButtonexp();</script></div>"
            };
            Page.Controls.Add(literal);
            Explanation.Visible = false;
        }

        #endregion

        #region Private methods

        private void BlockCopyPaste()
        {
            body.Attributes.Add("onselectstart", "return   false");
            body.Attributes.Add("onpaste", "return   false");
            body.Attributes.Add("oncopy", "return   false");
            body.Attributes.Add("oncut", "return   false");
        }

        #endregion
    }
}