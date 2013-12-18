using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ProgramAddAssign : PageBase<IProgramView, ProgramPresenter>, IProgramView
{
    private const string BUNDLE_PRODUCT_NAME = "Focused Review Tests";

    private const string PROGRAM_UPDATE_MESSAGE = " Program has been updated";

    private bool _canAssignProgram = false;

    private int _programofStudyId;

    public string SearchText { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int ProgramId { get; set; }

    public int ProductId { get; set; }

    public int TestId { get; set; }

    public int Bundle { get; set; }

    public int Type { get; set; }

    public IEnumerable<AssetGroup> AssetGroups
    {
        get { return ViewState["assets"] != null ? (IEnumerable<AssetGroup>)(ViewState["assets"]) : null; }
        set { ViewState["assets"] = value; }
    }

    public int ProgramOfStudyId
    {
        get { 
        if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
        {
            _programofStudyId = ddlProgramofStudy.SelectedValue.ToInt();
        }
        return _programofStudyId;
        }
        set { _programofStudyId = value; }
    }

    public string ProgramOfStudyName { get; set; }
    
    #region IProgramView
   
    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
       ddlProgramofStudy.NotSelectedText = "Selection Required";
        ControlHelper.PopulateProgramOfStudy(ddlProgramofStudy, programOfStudies);
        if (ProgramOfStudyName == ProgramofStudyType.PN.ToString())
        {
            ddlProgramofStudy.SelectedIndex = (int)ProgramofStudyType.PN;
            ddlProgramofStudy.Enabled = false;
            ProgramOfStudyId = (int) ProgramofStudyType.PN;
            Presenter.GetAssetGroups();
        }

    }

    public void PopulateAssets(IEnumerable<Asset> assets)
    {
        ddlAssetName.Items.Clear();
            ControlHelper.PopulateAssets(ddlAssetName, assets);
            switch (ddlAssetGroup.SelectedValue.ToInt())
            {

                case (int)AssetGroupType.DashboardPN:
                case (int)AssetGroupType.DashboardRN:
                case (int)AssetGroupType.CaseStudiesRn:
                    if (assets.Count() > 0)
                    {
                        ddlAssetName.Enabled = false;
                        rfvAssetName.Enabled = false;
                    }
                    else
                    {
                        ddlAssetName.Enabled = true;
                        rfvAssetName.Enabled = true;
                        ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
                    }
                    break;
                default:
                    if (assets.Count() < 0)
                    {
                        ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
                    }
                    break;
            }
       }

    public void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroups)
    {
        ddlAssetGroup.Items.Clear();
        ControlHelper.PopulateAssetGroup(ddlAssetGroup, assetGroups);
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        ddlAssetName.Items.Clear();
        var assignedTests = from t in tests
                            select new KeyValuePair<string, string>(MakeIDsUnique(t.TestId, true), t.TestName);
        if (tests.Count() > 0)
        {
            ddlAssetName.DataSource = assignedTests;
            ddlAssetName.DataTextField = "Value";
            ddlAssetName.DataValueField = "Key";
            ddlAssetName.DataBind();
        }
        else
        {
            ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
        }
       
       
         if (Bundle == 0)
         {
             if (ProductId == 0)
             {
                 ddlAssetName.Items.Clear();
                 ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
             }
         }
         else
         {
             var productAsTest = new KeyValuePair<string, string>(MakeIDsUnique(ProductId, false), ddlAssetGroup.SelectedItem.Text);
             ddlAssetName.Items.Insert(0, new ListItem(productAsTest.Value, productAsTest.Key));
         }
    }
    
    public void RefreshPage(Program program, UserAction action, string title, string subTitle, bool showUpdateMessage, bool hasAddPermission)
    {
    }

    public void PopulateAssignedTest(IEnumerable<ProgramTestDates> tests)
    {
        gvTests.DataSource = tests;
        gvTests.DataBind();
    }


    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new ArgumentException();
    }

    public void ShowBulkProgramResults(IEnumerable<Program> programs, string selectedProgramIds, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string errorMsg)
    {
        throw new NotImplementedException();
    }

    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var canAssign = Presenter.CanAssignTest();
        _canAssignProgram = canAssign;
        D_Tests.Visible = canAssign == true ? true : false;
        if (!IsPostBack)
        {
            Presenter.ShowProgramsToAssign();
            lblPName.Text = Name;
            lblDescriptiption.Text = Description;
            lblProgramOfStudyName.Text = ProgramOfStudyName;
            if (lblProgramOfStudyName.Text == ProgramofStudyType.RN.ToString())
            {
                ddlAssetGroup.Items.Insert(0, new ListItem("Not Selected", "-1"));
            }
            ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
            HiddenProgramId.Value = ProgramId.ToString();
        }
        else
        {
            lblM.Visible = false;
            lblM.Text = string.Empty;
        }
    }

   protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAssetName.Enabled = true;
        rfvAssetName.Enabled = true;
        ddlAssetName.Items.Clear();
        ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
        if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
        {
            Presenter.GetAssetGroups();
        }
        else
        {
            ddlAssetGroup.Items.Clear();
            ddlAssetGroup.Items.Insert(0, new ListItem("Not Selected", "-1"));
            
        }
    }

    protected void ddlAssetGroup_SelectedIndexChanged(object sender,EventArgs e)
    {
        ddlAssetName.Enabled = true;
        rfvAssetName.Enabled = true;
        if (ddlAssetGroup.SelectedValue.ToInt() > 0)
        {
            var selectedRow = from assetGrp in AssetGroups
                              where assetGrp.AssetGroupId == ddlAssetGroup.SelectedValue.ToInt()
                              select assetGrp;
            ProductId = selectedRow.Count() > 0 ? selectedRow.FirstOrDefault().ProductId : 0;
            GetAssetNames();
        }
        else
        {
            ddlAssetName.Items.Clear();
            ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
        }
    }

    protected void addbtn_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            var selectedRow = from assetGrp in AssetGroups
                              where assetGrp.AssetGroupId == ddlAssetGroup.SelectedValue.ToInt()
                              select assetGrp;
            ProductId = selectedRow.Count() > 0 ? selectedRow.FirstOrDefault().ProductId : 0;
            ProgramId = HiddenProgramId.Value.ToInt();
            var AssetGrpId = selectedRow.Count() > 0 ? selectedRow.FirstOrDefault().AssetGroupId : 0;
            var lstProgramTestDate = new List<ProgramTestDates>();
            //Incase of a Test
            if (ProductId != 0)
            {

                Presenter.GetProductDetails();
                var selectedProducts = ddlAssetName.SelectedItems;
                var product = selectedProducts
                    .Where(i => i.Value.StartsWith("P.")).FirstOrDefault();
                //Bundled test
                if (product != null)
                {
                    var objDate = new ProgramTestDates();
                    objDate.Program = new Program() {ProgramId = ProgramId};
                    var dotIndex = product.Value.IndexOf('.');
                    objDate.Test = new Test()
                                       {
                                           TestId =
                                               StringFunctions.Mid(product.Value, dotIndex + 1,
                                                                   (product.Value.Length - (dotIndex + 1))).ToInt()
                                       };
                    if (ddlProgramofStudy.SelectedItemsText == ProgramofStudyType.PN.ToString())
                    {
                        Bundle = 2;
                    }
                    objDate.Product = new Product() {Bundle = Bundle};
                    objDate.AssetGroupId = ddlAssetGroup.SelectedValue.ToInt();
                    lstProgramTestDate.Add(objDate);
                }
                //Not a Bundled test
                else
                {
                    foreach (var li in selectedProducts)
                    {
                        var objDate = new ProgramTestDates();
                        var _testId = li.Value.ToString();
                        var dotIndex = _testId.IndexOf('.');
                        objDate.Program = new Program() {ProgramId = ProgramId};
                        objDate.Test = new Test()
                                           {
                                               TestId =
                                                   StringFunctions.Mid(_testId, dotIndex + 1,
                                                                       (_testId.Length - (dotIndex + 1))).ToInt()
                                           };
                        objDate.Product = new Product() {Bundle = 0};
                        objDate.AssetGroupId = ddlAssetGroup.SelectedValue.ToInt();
                        lstProgramTestDate.Add(objDate);
                        objDate = null;
                    }
                }
                Presenter.AssignTestToProgram(lstProgramTestDate);
            }
            //Incase of any assets other than test
            else
            {
                var assetProgram = new ProgramTestDates
                                       {
                                           Program = new Program() {ProgramId = ProgramId},
                                           Product = new Product() {ProductId = ProductId, Bundle = 0},
                                            Test =new Test(){},
                                            AssetGroupId = AssetGrpId,
                                       };
                lstProgramTestDate.Add(assetProgram);
                Presenter.AssignAssetToProgram(lstProgramTestDate);
            }
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Assign Tests to Program")
                                .Add("Test Id", TestId.ToString())
                                .Add("Program Id", ProgramId.ToString())
                                .Add("AssetGroupId",AssetGrpId.ToString())
                                .Write();
            #endregion
            Presenter.ShowAssignedPrograms();
            lblM.Visible = true;
            lblM.Text = PROGRAM_UPDATE_MESSAGE;
        }
    }

    protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           if (!_canAssignProgram)
            {
                e.Row.Cells[2].Visible = false;
            }
        }
    }

    protected void gvTests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvTests.Rows[index];
        int id;
        if (e.CommandName == "Remove")
        {
            id = gvTests.DataKeys[row.RowIndex].Values["TestId"].ToString().ToInt();
            Type = gvTests.DataKeys[row.RowIndex].Values["Type"].ToString().ToInt();
            var grpId = gvTests.DataKeys[row.RowIndex].Values["AssetGroupId"].ToString().ToInt();
            ProductId = id;
            ProgramId = HiddenProgramId.Value.ToInt();
            Presenter.DeleteProgramFromProduct(grpId);
            lblM.Visible = true;
            lblM.Text = PROGRAM_UPDATE_MESSAGE;
            Presenter.ShowAssignedPrograms();
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

    private void GetAssetNames()
    {
        var assetGroupId = ddlAssetGroup.SelectedValue.ToInt();
        switch (assetGroupId)
        {
            case (int)AssetGroupType.DashboardPN:
            case (int)AssetGroupType.DashboardRN:
                Presenter.GetAssets(assetGroupId);
                break;
            case (int)AssetGroupType.IntegratedtestingRN:
            case (int)AssetGroupType.IntegratedtestingPN:
            case (int)AssetGroupType.FocussedReviewRN:
            case (int)AssetGroupType.FocussedReviewPN:
            case (int)AssetGroupType.NClexPrepRN:
            case (int)AssetGroupType.NClexPrepPN:
            case (int)AssetGroupType.EssentialNursingSkillsRN:
            case (int)AssetGroupType.EssentialNursingSkillsPN:
               Presenter.GetTests();
                break;
            case (int)AssetGroupType.CaseStudiesRn:
                PopulateCaseAssets(Presenter.GetCaseAssets());
                break;
            default:
                break;
        }
     }

    private void PopulateCaseAssets(IEnumerable<CaseStudy> caseAssets)
    {
        ddlAssetName.Items.Clear();
        ddlAssetName.DataSource = caseAssets;
        ddlAssetName.DataTextField = "CaseName";
        ddlAssetName.DataValueField = "CaseId";
        ddlAssetName.DataBind();
        if (caseAssets.Count() > 0)
        {
            rfvAssetName.Enabled = false;
            ddlAssetName.Enabled = false;
        }
        else
        {
            ddlAssetName.Enabled = true;
            rfvAssetName.Enabled = true;
            ddlAssetName.Items.Insert(0, new ListItem("Not Selected", "-1"));
        }
    }
}
