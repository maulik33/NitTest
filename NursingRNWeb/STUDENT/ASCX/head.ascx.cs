using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT.ASCX
{
    public partial class StudentHeader : StudentHeaderBaseControl<IStudentHeaderView, StudentHeaderPresenter>, IStudentHeaderView
    {
        public void SetHeaderControls()
        {
            switch (Presenter.Student.ProductId)
            {
                case -1:
                    IbHome.ImageUrl = "../../images/top_menu_home_overnew.jpg";
                    break;
                case 1:
                    IbIntegratedTest.ImageUrl = "../../images/top_menu_it_overnew.jpg";
                    break;
                case 3:
                    IbFocusedReview.ImageUrl = "../../images/top_menu_frt_overnew.jpg";
                    break;
                case 4:
                    IbNclex.ImageUrl = "../../images/top_menu_np_overnew.jpg";
                    break;
                case 5:
                    IbCaseStudies.ImageUrl = "../../images/top_menu_cs_overnew.jpg";            
                    break;
                case 6:
                    IbSkillsModule.ImageUrl = "../../images/top_menu_sm_overnew.jpg";                          
                    break;
                default:
                    IbResults.ImageUrl = "../../images/top_menu_tr_overnew.jpg";
                    break;
            }

            IbIntegratedTest.Enabled = Presenter.Student.IsIntegratedTest;
            IbFocusedReview.Enabled = Presenter.Student.IsFocusedReviewTest;
            IbNclex.Enabled = Presenter.Student.IsNclexTest;
            IbSkillsModule.Enabled = Presenter.Student.IsSkillsModuleTest;
            LbManageAcc.Visible = Presenter.Student.ManageAccount;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Presenter.OnPreRender();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LbManageAcc.Attributes.Add("onclick", "window.open('StudentPay.aspx','_blank');return false;");
            if (Presenter.Student.ProgramofStudyId == (int)ProgramofStudyType.PN)
            {
                pnlCaseStudy.Visible = false;
            }
            else
            {
                IbCaseStudies.Enabled = Presenter.Student.IsCaseStudyEnabled;
            }
        }

        #region Event Handlers
        protected void LbLogout_Click(object sender, EventArgs e)
        {
            TraceHelper.WriteTraceEnd(Presenter.TraceToken);
            Session.Clear();
            Session.Abandon();
            Presenter.OnLbLogout_Click();
        }

        protected void IbHome_Click(object sender, ImageClickEventArgs e)
        {
           Presenter.OnIbHome_Click();
        }

        protected void IbIntegratedTest_Click(object sender, ImageClickEventArgs e)
        {
          Presenter.OnIbIntegratedTest_Click();
        }

        protected void IbFocusedReview_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbFocusedReview_Click();
        }
      
        protected void IbNclex_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbNclex_Click();
        }

        protected void IbResults_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbResults_Click();
        }

        protected void IbCaseStudies_Click(object sender, ImageClickEventArgs e)
        {
           Presenter.OnIbCaseStudies_Click();
        }

        protected void IbSkillsModule_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbSkillsModule_Click();
        }
        #endregion
    }
}
