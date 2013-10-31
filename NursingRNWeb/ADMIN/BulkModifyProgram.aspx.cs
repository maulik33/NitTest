using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class BulkModifyProgram : PageBase<IProgramView, ProgramPresenter>, IProgramView
    {
        private const string BUNDLE_PRODUCT_NAME = "Focused Review Tests";

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SearchText
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ProgramId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<AssetGroup> AssetGroups
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ProductId { get; set; }

        public int TestId { get; set; }

        public int Bundle { get; set; }

        public int Type { get; set; }

        public bool IsBindingSelectedPrograms { get; set; }

        public int ProgramOfStudyId
        {
            get { return ddlProgramOfStudy.Visible ? ddlProgramOfStudy.SelectedValue.ToInt() : (int)ProgramofStudyType.RN; }
        }

        public string ProgramOfStudyName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroup)
        {
            throw new NotImplementedException();
        }

        public override void PreInitialize()
        {
            ////
        }

        public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
        {
            throw new NotImplementedException();
        }

        public void PopulateTests(IEnumerable<NursingLibrary.Entity.Test> tests)
        {
            var assignedTests = from t in tests
                                select new KeyValuePair<string, string>(MakeIDsUnique(t.TestId, true), t.TestName);
            ddlTest.DataSource = assignedTests;
            ddlTest.DataTextField = "Value";
            ddlTest.DataValueField = "Key";
            ddlTest.DataBind();
        }

        public void PopulateAssignedTest(IEnumerable<NursingLibrary.Entity.ProgramTestDates> tests)
        {
            throw new NotImplementedException();
        }

        public void PopulateProducts(IEnumerable<Product> products)
        {
            ControlHelper.PopulateProducts(ddProducts, products);
            ddProducts.Items.Insert(0, new ListItem("Select Category", "-1"));
        }

        public void RefreshPage(Program program, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
        {
            throw new NotImplementedException();
        }

        public void ShowBulkProgramResults(IEnumerable<Program> programs, string selectedProgramIds, SortInfo sortMetaData)
        {
            gridPrograms.DataSource = KTPSort.Sort<Program>(programs, sortMetaData);
            gridPrograms.DataBind();
            StringBuilder programIds = new StringBuilder();
            if (!IsBindingSelectedPrograms)
            {
                hdnAssignedProgramIds.Value = selectedProgramIds;
            }

            if (programs.Count() == 0)
            {
                lblM.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {
                lblM.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
            }
        }

        public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
        {
            ControlHelper.PopulateProgramOfStudy(ddlProgramOfStudy, programOfStudies);
        }

        public void PopulateAssets(IEnumerable<Asset> assets)
        {
            throw new NotImplementedException();
        }
        public void ShowMessage(string errorMsg)
        {
            throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
                {
                    Presenter.PopulateProgramOfStudies();
                }
                ddlTest.Items.Insert(0, new ListItem("Select Test", "-1"));
                Presenter.ShowTestCategories();
                ResetDropDown(ddProducts, false);
                ResetDropDown(ddlTest, false);
            }

            if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
            {
                ddlTest.Enabled = false;
                ddProducts.Enabled = false;
                btnSearch.Visible = false;
                ddlProgramOfStudy.Enabled = false;
            }

            Form.SubmitDisabledControls = true;
        }

        protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddProducts.SelectedValue != "-1")
            {
                ProductId = ddProducts.SelectedValue.ToInt();
                ddlTest.Items.Clear();
                Presenter.GetTests();
                if (Bundle == 1)
                {
                    var productAsTest = new KeyValuePair<string, string>(MakeIDsUnique(ddProducts.SelectedValue.ToInt(), false), ddProducts.SelectedItem.Text);
                    ddlTest.Items.Insert(0, new ListItem(productAsTest.Value, productAsTest.Key));
                }

                ddlTest.Items.Insert(0, new ListItem("Select Test", "-1"));
            }
            else
            {
                ddlTest.Items.Clear();
                ddlTest.Items.Insert(0, new ListItem("Select Test", "-1"));
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
            ResetDropDown(ddlTest, ddProducts.SelectedIndex != 0);
            ddlTest_SelectedIndexChanged(null, null);
        }

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearHiddenFields();
            if (ddlTest.SelectedValue != "-1")
            {
                GetBulkProgramDetails();
            }
            else
            {
                gridPrograms.DataSource = null;
                gridPrograms.DataBind();
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            GetBulkProgramDetails();
        }

        protected void gridPrograms_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            IsBindingSelectedPrograms = true;
            GetBulkProgramDetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var selectedProducts = ddlTest.SelectedValue;
            var product = ddlTest.SelectedValue.StartsWith("P.") && string.Compare(ddlTest.SelectedItemsText, BUNDLE_PRODUCT_NAME, true) == 0;
            var _testId = ddlTest.SelectedValue;
            var dotIndex = _testId.IndexOf('.');
            _testId = StringFunctions.Mid(_testId, dotIndex + 1, (_testId.Length - (dotIndex + 1)));
            SetSelectedCheckBoxes();
            if (!string.IsNullOrEmpty(hdnModifiedProgramIds.Value))
            {
                if (product)
                {
                    Presenter.SaveBulkModifiedPrograms(_testId.ToInt(), 1, hdnAssignedProgramIds.Value);
                }
                else
                {
                    Presenter.SaveBulkModifiedPrograms(_testId.ToInt(), 0, hdnAssignedProgramIds.Value);
                }

                lblMessage.Text = "Program(s) have been modified.";
            }

            ClearHiddenFields();
            GetBulkProgramDetails();
        }

        protected void gridPrograms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (GridView)sender;
                int rowIndex = Convert.ToInt32(e.Row.RowIndex);
                bool status = false;

                if (!gv.DataKeys[rowIndex].Values["IsTestAssignedToProgram"].ToString().Equals(string.Empty))
                {
                    status = gv.DataKeys[rowIndex].Values["IsTestAssignedToProgram"].ToBool();
                }

                CheckBox ch = (CheckBox)e.Row.FindControl("chkAssigned");
                if (ch != null)
                {
                    if (status)
                    {
                        ch.Checked = true;
                    }
                    else
                    {
                        ch.Checked = false;
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearHiddenFields();
            GetBulkProgramDetails();
        }

        protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDropDown(ddProducts, ddlProgramOfStudy.SelectedIndex != 0);
            ResetDropDown(ddlTest, false);
            ddlTest_SelectedIndexChanged(null, null);
        }

        private void ClearHiddenFields()
        {
            hdnModifiedProgramIds.Value = string.Empty;
            hdnAssignedProgramIds.Value = string.Empty;
        }

        private void SetSelectedCheckBoxes()
        {
            List<string> assignedProgramIds = hdnAssignedProgramIds.Value.Split('|').ToList();
            List<string> modifiedProgramIds = hdnModifiedProgramIds.Value.Split('|').ToList();

            foreach (GridViewRow di in gridPrograms.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chkAssigned");
                if (chkBx != null)
                {
                    AddChangedProgramIds(assignedProgramIds, di, chkBx);
                }

                CheckBox chkBoxModified = (CheckBox)di.FindControl("chkModified");
                if (chkBoxModified != null)
                {
                    AddChangedProgramIds(modifiedProgramIds, di, chkBoxModified);
                }
            }

            hdnAssignedProgramIds.Value = string.Join("|", assignedProgramIds.ToArray()).Trim('|');
            hdnModifiedProgramIds.Value = string.Join("|", modifiedProgramIds.ToArray()).Trim('|');
        }

        private void AddChangedProgramIds(List<string> programIds, GridViewRow di, CheckBox chkBx)
        {
            string programId = di.Cells[0].Text.Trim();
            object o = Request.Form[chkBx.UniqueID];
            if (chkBx.Checked && !programIds.Contains(programId))
            {
                programIds.Add(programId);
            }
            else if (!chkBx.Checked && programIds.Contains(programId))
            {
                programIds.Remove(programId);
            }
        }

        private void GetBulkProgramDetails()
        {
            string type = "0";
            var product = ddlTest.SelectedValue.StartsWith("P.") && string.Compare(ddlTest.SelectedItemsText, BUNDLE_PRODUCT_NAME, true) == 0;
            if (product)
            {
                type = "1";
            }

            var _testId = ddlTest.SelectedValue;
            var dotIndex = _testId.IndexOf('.');
            _testId = StringFunctions.Mid(_testId, dotIndex + 1, (_testId.Length - (dotIndex + 1)));
            SetSelectedCheckBoxes();
            Presenter.GetBulkProgramDetails(_testId.ToInt(), txtProgramListFilter.Text, type, hdnGridConfig.Value);
            if (IsBindingSelectedPrograms)
            {
                MarkPrograms();
            }
        }

        private void MarkPrograms()
        {
            List<string> programIds = hdnAssignedProgramIds.Value.Split('|').ToList();
            List<string> modifiedProgramIds = hdnModifiedProgramIds.Value.Split('|').ToList();
            string programId = string.Empty;
            foreach (GridViewRow di in gridPrograms.Rows)
            {
                MarkCheckBox(programIds, di, "chkAssigned");
                MarkCheckBox(modifiedProgramIds, di, "chkModified");
            }
        }

        private void MarkCheckBox(List<string> programIds, GridViewRow di, string checkboxid)
        {
            CheckBox chkBx = (CheckBox)di.FindControl(checkboxid);
            if (chkBx != null)
            {
                string programId = di.Cells[0].Text.Trim();
                if (programIds.Contains(programId))
                {
                    chkBx.Checked = true;
                }
                else
                {
                    chkBx.Checked = false;
                }
            }
        }

        private string MakeIDsUnique(int test, bool isTest)
        {
            if (isTest)
            {
                return string.Format("T.{0}", test);
            }
            else
            {
                return string.Format("P.{0}", test);
            }
        }

        private void ResetDropDown(DropDownList ddl, bool enabled)
        {
            ddl.SelectedIndex = 0;
            ddl.Enabled = enabled;
        }
    }
}