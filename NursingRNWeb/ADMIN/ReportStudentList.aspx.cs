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

public partial class ADMIN_ReportStudentList : System.Web.UI.Page
{
    protected int ActionType;
    public static int numberDiv;
    protected string cohortQuery="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || Session["UserID"].ToString().Trim().Equals(""))
            Response.Redirect("../A_login.aspx");

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

        if (!Page.IsPostBack)
        {
            gridUsers.Visible = true;

            ViewState["ActionType"] = Convert.ToInt32(Request.QueryString["Type"]);
            if (ViewState["ActionType"].ToString() == "2")
            {
                lblTitle.Text = "Student Reports >  Student Results by Question";
            }

            else
            {
                lblTitle.Text = "Student Reports >  Student Performance Report";
            }

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



            //        new cCohort().PopulateCohort(ddCohort, Convert.ToInt32(ddInstitution.SelectedValue));
            //        ddCohort.Items.Insert(0, new ListItem("Not Selected", "0"));
            new cCohort().PopulateCohort(lbxCohort, Convert.ToInt32(ddInstitution.SelectedValue));
            if (lbxCohort.Items.Count > 0)
            {
                lbxCohort.Visible = true;

                //            new cGroup().PopulateGroup(ddGroup, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddCohort.SelectedValue));
                new cGroup().PopulateGroup(ddGroup, Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery);
            }
            ddGroup.Items.Insert(0, new ListItem("Not Selected", "0"));

            Session["SortExp"] = "LastName";
            Session["SortDir"] = "ASC";
            // BindData(Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddCohort.SelectedValue), Convert.ToInt32(ddGroup.SelectedValue), "", "UserName", "ASC");

        }


    }
    protected void BindData(int IID, string CID, int GID, string text, string sortExpression, string sortDirection)
    {


        Session["SortExp"] = sortExpression;
        Session["SortDir"] = sortDirection;



        //       DataTable dt = new cStudent().GetListOfStudents(IID, CID, GID, text);
        DataTable dt = new cStudent().GetListOfStudents(IID, CID, GID, text);
        DataView dv = new DataView(dt);


        dv.Sort = sortExpression + " " + sortDirection;


        gridUsers.DataSource = dv;
        gridUsers.DataBind();

        if (dt.Rows.Count == 0)
        {
            lblM.Visible = true;
        }
        else
        {
            lblM.Visible = false;
        }

    }
    protected void gridUsers_SelectedIndexChanged(object sender, EventArgs e)
    {


        //Response.Redirect("UserDetails.aspx?actionType=edit&USER_ID=" + gridUsers.SelectedDataKey.Values["USER_ID"].ToString());

    }

    protected void gridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gridUsers.Rows[index];


            string id = Server.HtmlDecode(row.Cells[0].Text);


            switch (e.CommandName)
            {

                case "View":
                    if (ViewState["ActionType"].ToString() == "1")
                    {
                        Response.Redirect("ReportTestStudent.aspx?id=" + id + "&UserTestID=0&ProductID=0");

                    }
                    if (ViewState["ActionType"].ToString() == "2")
                    {
                        Response.Redirect("ReportStudentQuestion.aspx?id=" + id + "&TestID=0&ProductID=0");

                    }
                    break;



            }
        }
    }



    protected void gridUsers_PageIndexChanged(Object sender, EventArgs e)
    {
        gridUsers.Visible = true;
        //DetailsView1.Visible = false;
    }

    protected void gridUsers_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        string sortDirection = (string)Session["SortDir"];



        if ((numberDiv % 2) == 0)

            sortDirection = "ASC";

        else

            sortDirection = "DESC";

        numberDiv++;

        Session["SortExp"] = sortExpression;
        Session["SortDir"] = sortDirection;
        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);
    }
    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        new cCohort().PopulateCohort(lbxCohort, Convert.ToInt32(ddInstitution.SelectedValue));
        //    ddCohort.Items.Insert(0, new ListItem("Not Selected", "0"));
        lbxCohort.Visible = true;
        //    new cGroup().PopulateGroup(ddGroup, Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery);
        //    ddGroup.Items.Insert(0, new ListItem("Not Selected", "0"));

        string sortExpression;
        string sortDirection;
        if (Session["SortExp"] == null)
        {

            sortExpression = "LastName";
            sortDirection = " ASC";
        }
        else
        {
            sortExpression = Session["SortExp"].ToString();
            sortDirection = Session["SortDir"].ToString();
        }

        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);

    }
    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sortExpression;
        string sortDirection;
        if (Session["SortExp"] == null)
        {

            sortExpression = "LastName";
            sortDirection = " ASC";
        }
        else
        {
            sortExpression = Session["SortExp"].ToString();
            sortDirection = Session["SortDir"].ToString();
        }


        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);

    }
    protected void seabtn_Click(object sender, ImageClickEventArgs e)
    {
        string sortExpression;
        string sortDirection;
        if (Session["SortExp"] == null)
        {

            sortExpression = "LastName";
            sortDirection = " ASC";
        }
        else
        {
            sortExpression = Session["SortExp"].ToString();
            sortDirection = Session["SortDir"].ToString();
        }


        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);

    }
    protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string sortExpression;
        string sortDirection;
        if (Session["SortExp"] == null)
        {

            sortExpression = "LastName";
            sortDirection = " ASC";
        }
        else
        {
            sortExpression = Session["SortExp"].ToString();
            sortDirection = Session["SortDir"].ToString();
        }


        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);

        gridUsers.PageIndex = e.NewPageIndex;
        gridUsers.DataBind();


    }



    protected void gridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            if (ViewState["ActionType"].ToString() == "1")
            {
                e.Row.Cells[4].Text = "Student Performance Report";

            }

            if (ViewState["ActionType"].ToString() == "2")
            {
                e.Row.Cells[4].Text = "Student Results by Question ";
            }
        }
    }

    protected void lbxCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem itemCohort in lbxCohort.Items)
        {
            if (itemCohort.Selected == true)
            {
                if (cohortQuery == "")
                {
                    cohortQuery += Convert.ToInt32(itemCohort.Value);
                }
                else
                {
                    cohortQuery += "|" + Convert.ToInt32(itemCohort.Value);
                }
            }
        }

        new cGroup().PopulateGroup(ddGroup, Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery);
        ddGroup.Items.Insert(0, new ListItem("Not Selected", "0"));


        string sortExpression;
        string sortDirection;
        if (Session["SortExp"] == null)
        {

            sortExpression = "LastName";
            sortDirection = " ASC";
        }
        else
        {
            sortExpression = Session["SortExp"].ToString();
            sortDirection = Session["SortDir"].ToString();
        }


        BindData(Convert.ToInt32(ddInstitution.SelectedValue), cohortQuery, Convert.ToInt32(ddGroup.SelectedValue), txtSearch.Text.Trim(), sortExpression, sortDirection);

    }
}

