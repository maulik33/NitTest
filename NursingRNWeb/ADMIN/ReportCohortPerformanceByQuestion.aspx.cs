using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NursingLibrary;


public partial class ADMIN_ReportCohortPerformanceByQuestion : System.Web.UI.Page
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        SV.CheckSession(this.Page, SV.AdiminTypes.Academic);//zyh
        if (Session["InstitutionID"] != null)
        {
            if (Session["InstitutionID"].ToString().Trim().Equals(""))
            {
                Response.Redirect("../A_login.aspx");
            }
        }
        else
        {
            Response.Redirect("../A_login.aspx");
        }

        if (!IsPostBack)
        {

            if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
            {
                int Inst = Convert.ToInt32(Session["InstitutionID"].ToString());
                new cStudent().PopulateInstitutionByID(ddInstitution, Inst);
            }
            else if (Session["UserType"].ToString() == "5")
            {
                string strInst = Session["InstitutionID"].ToString();
                new cStudent().PopulateInstitutionByID(ddInstitution, strInst);
            }
            else
            {
                new cStudent().PopulateInstitution(ddInstitution);
                ddInstitution.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
            ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);



            new cCohort().PopulateCohort(ddCohorts, Convert.ToInt32(ViewState["InstitutionID"].ToString()));
            ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));

            new cProduct().PopulateProducts(ddProducts);
            ddProducts.Items.Insert(0, new ListItem("Not Selected", "0"));

            ddTests.Items.Insert(0, new ListItem("All Tests", "-1"));
            ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        }
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);

        new cCohort().PopulateCohort(ddCohorts, Convert.ToInt32(ViewState["InstitutionID"].ToString()));
        ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));

    }

    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        new cTest().PopulateTestsByCohortIDAndProductID(ddTests, Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        ddTests.Items.Insert(0, new ListItem("All Test", "-1"));
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
    }

    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(SV.PrintActions.ShowInterface);
    }

    protected void BindData(SV.PrintActions Act)
    {
        if (ddTests.SelectedValue == "0") { return; }

        if (Act == SV.PrintActions.ShowInterface)
        {
            if (ddProducts.SelectedValue == "1")
            {
                FillGrid_I("I");
            }
            if (ddProducts.SelectedValue == "3")
            {
                FillGrid_I("F");
            }
            if (ddProducts.SelectedValue == "4")
            {
                FillGrid_I("N");
            }
        }
        else
        {
            if (ddProducts.SelectedValue == "1")
            {
                FillRpt("I", Act);
            }
            if (ddProducts.SelectedValue == "3")
            {
                FillRpt("F", Act);
            }
            if (ddProducts.SelectedValue == "4")
            {
                FillRpt("N", Act);
            }
        }
    }

    private void FillRpt(string p_type, SV.PrintActions Act)
    {
        DataTable tdt = null;
        DataTable dt = new cXML().GetRezultsFromTheCohortByQ(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        foreach (DataRow r in dt.Rows)
        {
            switch (r["AIndex"].ToString().Trim())
            {
                case "A":
                    r["AIndex"] = 1;
                    break;
                case "B":
                    r["AIndex"] = 2;
                    break;
                case "C":
                    r["AIndex"] = 3;
                    break;
                case "D":
                    r["AIndex"] = 4;
                    break;
                case "E":
                    r["AIndex"] = 5;
                    break;
                case "F":
                    r["AIndex"] = 6;
                    break;
                default:
                    break;
            }
        }
        CohortPerformanceByQuestion ds = new CohortPerformanceByQuestion();
        CohortPerformanceByQuestion.HeadRow hr = ds.Head.NewHeadRow();
        hr.ReportName = "Cohort Performance by Question";
        hr.InstitutionName = ddInstitution.SelectedItem.Text;
        hr.CohortName = ddCohorts.SelectedItem.Text;
        hr.TestType = ddProducts.SelectedItem.Text;
        hr.TestName = "";
        ds.Head.Rows.Add(hr);

        foreach (DataRow r in dt.Rows)
        {
            CohortPerformanceByQuestion.DetailRow dr = ds.Detail.NewDetailRow();
            dr.HeadID = hr.HeadID;
            dr.QuestionID = r["QuestionID"].ToString();
            dr.TestName = r["TestName"].ToString();
            Decimal tol = System.Convert.ToDecimal(r["Sum_A"].ToString()) + System.Convert.ToDecimal(r["Sum_B"].ToString()) + System.Convert.ToDecimal(r["Sum_C"].ToString()) + System.Convert.ToDecimal(r["Sum_D"].ToString()) + System.Convert.ToDecimal(r["Sum_E"].ToString());
            //if (tol == 0) { dr.PercentageCorrect = "0"; } else { dr.PercentageCorrect = System.Convert.ToString (((Decimal)System.Convert.ToDecimal(r["N_Correct"].ToString()) / tol) * 100); }
            dr.PercentageCorrect = System.Convert.ToDecimal(r["N_Correct"].ToString());
            dr.CorrectAnswer = System.Convert.ToString(r["AIndex"].ToString());
            dr.A = System.Convert.ToDecimal(r["Sum_A"].ToString());
            dr.B = System.Convert.ToDecimal(r["Sum_B"].ToString());
            dr.C = System.Convert.ToDecimal(r["Sum_C"].ToString());
            dr.D = System.Convert.ToDecimal(r["Sum_D"].ToString());
            dr.E = System.Convert.ToDecimal(r["Sum_E"].ToString());

            if (p_type == "I")
            {
                dr.SecondsUsed = System.Convert.ToInt32(r["Sum_R"].ToString());
            }
            if (p_type == "I" || p_type == "N")
            {
                string QID = r["QID"].ToString();
                string TestID = r["TestID"].ToString();
                ArrayList list = new ArrayList();
                list = new cTest().GetTestCaracteristics(Convert.ToInt32(TestID), "A");
                tdt = new cTest().GetListOfSubCategoryForTheQuestion(list, Convert.ToInt32(QID));

                for (int i = 0; i < tdt.Columns.Count - 1; i++)
                {
                    dr["Category" + (i + 1).ToString()] = tdt.Rows[0][i + 1].ToString();
                }
            }
            ds.Detail.Rows.Add(dr);
        }



        rpt.Load(Server.MapPath("Report/CohortPerformanceByQuestion.rpt"));
        rpt.SetDataSource(ds);

        string FieldString = "";
        string[] FieldArray;

        if (p_type == "I")
        {
            FieldString = "SecondsUsed|";
        }
        if (p_type == "I" || p_type == "N")
        {
            for (int i = 1; i <= 4; i++)
            {
                FieldString += "Category" + (i).ToString() + "|";
            }
        }
        if (FieldString.Length > 0)
        {
            FieldString = FieldString.Remove(FieldString.Length - 1, 1);
            FieldArray = FieldString.Split('|');
        }
        else
        {
            FieldArray = new string[0];
        }
        int ii = 0;
        for (ii = 0; ii < FieldArray.Length; ii++)
        {
            if (p_type == "I")
            {
                if (ii == 0)
                {
                    rpt.ParameterFields["P" + (ii + 1).ToString()].CurrentValues.AddValue("Seconds Used");
                    rpt.DataDefinition.FormulaFields["F1"].Text = "{Detail." + FieldArray[ii] + "}";
                }
                else
                {
                    rpt.ParameterFields["P" + (ii + 1).ToString()].CurrentValues.AddValue("Category" + (ii).ToString());
                    rpt.DataDefinition.FormulaFields["F" + (ii + 1).ToString()].Text = "{Detail." + FieldArray[ii] + "}";
                }
            }
            if (p_type == "N")
            {
                rpt.ParameterFields["P" + (ii + 1).ToString()].CurrentValues.AddValue("Category" + (ii).ToString());
                rpt.DataDefinition.FormulaFields["F" + (ii + 1).ToString()].Text = "{Detail." + FieldArray[ii] + "}";
            }
        }
        for (int j = ii; j < 5; j++)
        {
            rpt.ParameterFields["P" + (j + 1).ToString()].CurrentValues.AddValue("");
        }

        switch (Act)
        {
            case SV.PrintActions.DirectPrint:
                break;
            case SV.PrintActions.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CohortComparisons");
                break;
            case SV.PrintActions.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CohortComparisons");
                break;
            case SV.PrintActions.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CohortComparisons");
                break;
            case SV.PrintActions.PrintInterface:
                break;
            case SV.PrintActions.ShowInterface:
                break;
            case SV.PrintActions.ShowPreview:
                break;
        }

    }

    protected void FillGrid_I(string p_type)
    {
        DataTable dt = new cXML().GetRezultsFromTheCohortByQ(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        GridView1.DataSource = dt;
        foreach (DataRow r in dt.Rows)
        {
            switch (r["AIndex"].ToString().Trim())
            {
                case "A":
                    r["AIndex"] = "1";
                    break;
                case "B":
                    r["AIndex"] = "2";
                    break;
                case "C":
                    r["AIndex"] = "3";
                    break;
                case "D":
                    r["AIndex"] = "4";
                    break;
                case "E":
                    r["AIndex"] = "5";
                    break;
                case "F":
                    r["AIndex"] = "6";
                    break;
                default:
                    break;
            }
        }
        GridView1.DataBind();

        gvIntegrated.RowDataBound += new GridViewRowEventHandler(gvIntegrated_RowDataBound);
        gvIntegrated.RowCommand += new GridViewCommandEventHandler(gvIntegrated_RowCommand);

        gvIntegrated.AutoGenerateColumns = false;
        gvIntegrated.Columns.Clear();

        BoundField QuestionIDConceptBoundField = new BoundField();
        QuestionIDConceptBoundField.DataField = "QuestionID";
        QuestionIDConceptBoundField.HeaderText = "Q.ID";
        gvIntegrated.Columns.Add(QuestionIDConceptBoundField);

        BoundField TestNameBoundField = new BoundField();
        TestNameBoundField.DataField = "TestName";
        TestNameBoundField.HeaderText = "Test Name";
        gvIntegrated.Columns.Add(TestNameBoundField);


        BoundField CorrectBoundField = new BoundField();
        CorrectBoundField.DataField = "N_Correct";
        CorrectBoundField.HeaderText = "% Correct";
        gvIntegrated.Columns.Add(CorrectBoundField);

        BoundField CorrectAnswerBoundField = new BoundField();
        CorrectAnswerBoundField.DataField = "AIndex";
        CorrectAnswerBoundField.HeaderText = "Correct Answer";
        gvIntegrated.Columns.Add(CorrectAnswerBoundField);



        BoundField CorrectABoundField = new BoundField();
        CorrectABoundField.HeaderText = "1";
        CorrectABoundField.DataField = "Sum_A";
        gvIntegrated.Columns.Add(CorrectABoundField);


        BoundField CorrectBBoundField = new BoundField();
        CorrectBBoundField.HeaderText = "2";
        CorrectBBoundField.DataField = "Sum_B";
        gvIntegrated.Columns.Add(CorrectBBoundField);

        BoundField CorrectCBoundField = new BoundField();
        CorrectCBoundField.HeaderText = "3";
        CorrectCBoundField.DataField = "Sum_C";
        gvIntegrated.Columns.Add(CorrectCBoundField);

        BoundField CorrectDBoundField = new BoundField();
        CorrectDBoundField.HeaderText = "4";
        CorrectDBoundField.DataField = "Sum_D";
        gvIntegrated.Columns.Add(CorrectDBoundField);


        BoundField CorrectEBoundField = new BoundField();
        CorrectEBoundField.HeaderText = "5";
        CorrectEBoundField.DataField = "Sum_E";
        gvIntegrated.Columns.Add(CorrectEBoundField);




        if (p_type == "I")
        {
            BoundField SecondsUsedBoundField = new BoundField();
            SecondsUsedBoundField.DataField = "Sum_R";
            SecondsUsedBoundField.HeaderText = "Seconds Used";
            gvIntegrated.Columns.Add(SecondsUsedBoundField);
        }
        if (p_type == "I" || p_type == "N")
        {
            BoundField Cat1BoundField = new BoundField();
            Cat1BoundField.HeaderText = "SubCategory";
            gvIntegrated.Columns.Add(Cat1BoundField);

            BoundField Cat2BoundField = new BoundField();
            Cat2BoundField.HeaderText = "SubCategory";
            gvIntegrated.Columns.Add(Cat2BoundField);

            BoundField Cat3BoundField = new BoundField();
            Cat3BoundField.HeaderText = "SubCategory";
            gvIntegrated.Columns.Add(Cat3BoundField);

            BoundField Cat4BoundField = new BoundField();
            Cat4BoundField.HeaderText = "SubCategory";
            gvIntegrated.Columns.Add(Cat4BoundField);
        }
        gvIntegrated.DataSource = dt;
        gvIntegrated.DataBind();
    }

    protected void gvIntegrated_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);
            string QID = gv.DataKeys[rowIndex].Values["QID"].ToString();
            string TestID = gv.DataKeys[rowIndex].Values["TestID"].ToString();
            //string ProductID = gv.DataKeys[rowIndex].Values["ProductID"].ToString();

            ArrayList list = new ArrayList();
            list = new cTest().GetTestCaracteristics(Convert.ToInt32(TestID), "A");
            //int i = 1;
            DataTable dt = new cTest().GetListOfSubCategoryForTheQuestion(list, Convert.ToInt32(QID));
            DataRow dr = dt.Rows[0];

            //zyh
            int Col = dt.Columns.Count - 1;
            if (Col > 4) { Col = 4; }
            for (int i = 0; i < Col; i++)
            {
                e.Row.Cells[i + gvIntegrated.Columns.Count - 4].Text = dr[i + 1].ToString();
            }
            //if (i<list.Count)
            //{
            //   i++;
            //   e.Row.Cells[9].Text = dr["Cat_1"].ToString();
            //}
            //if (i < list.Count)
            //{
            //    i++;
            //    e.Row.Cells[10].Text = dr["Cat_2"].ToString();
            //}
            //if (i < list.Count)
            //{
            //    i++;
            //    e.Row.Cells[11].Text = dr["Cat_3"].ToString();
            //}
            //if (i < list.Count)
            //{
            //    i++;
            //    e.Row.Cells[12].Text = dr["Cat_4"].ToString();
            //}
            //zyh
        }

    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close(); rpt.Dispose();
    }

    protected void gvIntegrated_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void ddCohorts_SelectedIndexChanged(object sender, EventArgs e)
    {
        new cTest().PopulateTestsByCohortIDAndProductID(ddTests, Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        ddTests.Items.Insert(0, new ListItem("All Test", "-1"));
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
    }

    protected void gvIntegrated_Sorting(object sender, GridViewSortEventArgs e)
    {

    }












    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        this.BindData(SV.PrintActions.PDFPrint);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        this.BindData(SV.PrintActions.ExportExcel);

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        this.BindData(SV.PrintActions.ExportExcelDataOnly);

    }

}
