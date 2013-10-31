using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class STUDENT_CaseStudies : StudentBasePage<IStudentCaseStudiesView, StudentCaseStudiesPresenter>, IStudentCaseStudiesView
    {
        public void AddCaseTable(IEnumerable<CaseStudy> caseStudies)
        {
            var tb = new Table();
            tb.Attributes["class"] = "nctable_new2";
            tb.Width = Unit.Percentage(100);
            tb.CellSpacing = 0;
            var tr = new TableRow();
            var td = new TableCell();
            tr.Attributes["class"] = "Gridheader";
            td.Text = @"Case Name";
            td.Attributes["align"] = "center";
            td.Width = Unit.Percentage(40);
            tr.Controls.Add(td);

            td = new TableCell();
            tr.Controls.Add(td);
            tb.Controls.Add(tr);

            int i = 0;
            tr = new TableRow();
            ++i;
            if (i % 2 == 0)
            {
                tr.Attributes["class"] = "Gridrow1_b";
            }
            else
            {
                tr.Attributes["class"] = "Gridrow1_a";
            }

            ////td = new TableCell { Text = "Orientation Video" };
            ////td.Attributes["align"] = "left";
            ////tr.Controls.Add(td);
            ////td = new TableCell();
            ////var lbt1 = new LinkButton
            ////{
            ////    Text = @"View",
            ////    CssClass = "s2",
            ////    ID = "-1"
            ////};
            ////lbt1.Click += ButtonClick;
            ////td.Controls.Add(lbt1);
            ////td.Attributes["align"] = "center";
            ////tr.Controls.Add(td);
            ////tb.Controls.Add(tr);
            foreach (var caseStudy in caseStudies)
            {
                tr = new TableRow();
                ++i;
                if (i % 2 == 0)
                {
                    tr.Attributes["class"] = "Gridrow1_b";
                }
                else
                {
                    tr.Attributes["class"] = "Gridrow1_a";
                }

                td = new TableCell { Text = caseStudy.CaseName };
                td.Attributes["align"] = "left";
                tr.Controls.Add(td);

                td = new TableCell();
                if (caseStudy.CaseId != 100)
                {
                    var lbt = new LinkButton
                                  {
                                      Text = @"Take/View the Case",
                                      CssClass = "s2",
                                      ID = caseStudy.CaseId.ToString()
                                  };

                    lbt.Click += ButtonClick;
                    td.Controls.Add(lbt);
                }
                else
                {
                    var lbt = new LinkButton
                    {
                        Text = @"View",
                        CssClass = "s2",
                        ID = caseStudy.CaseId.ToString()
                    };
                    lbt.Click += ButtonClick;
                    td.Controls.Add(lbt);
                }

                td.Attributes["align"] = "center";
                tr.Controls.Add(td);
                tb.Controls.Add(tr);
            }

            tbCase.Controls.Add(tb);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Case Studies Page.");
                #endregion
            }

            Presenter.OnViewLoaded();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var lnbt = (LinkButton)sender;
           string javaScript = Presenter.PopupDxr(lnbt.ID);
                ClientScript.RegisterStartupScript(typeof(Page), string.Empty, javaScript); 
        }
    }
}
