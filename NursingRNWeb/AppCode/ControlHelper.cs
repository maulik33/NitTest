using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using WebControls;

/// <summary>
/// Summary description for ControlHelper
/// </summary>
public class ControlHelper
{
    #region Public Methods

    public static void AssignSelectedValue(DropDownList list, string value)
    {
        ListItem selectedItem = list.Items.FindByValue(value) as ListItem;
        if (selectedItem != null)
        {
            list.SelectedValue = selectedItem.Value;
        }
    }

    public static bool IsInternalItem(ListItem item)
    {
        return item.Value == Constants.LIST_SELECT_ALL_VALUE
            || item.Value == Constants.LIST_NOT_SELECTED_VALUE
            || item.Value == Constants.LIST_NOT_ASSIGNED_VALUE;
    }

    /// <summary>
    /// Use this method to select "Not Selected" item when "Not Selected" item value is different from what
    /// gets stored in repository.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="value"></param>
    public static void SetSelectedValue(ListControl control, string valueToSelect, string notSelectedItemValue)
    {
        string value = (valueToSelect == notSelectedItemValue) ? Constants.LIST_NOT_SELECTED_VALUE : valueToSelect;
        FindByValue(value, control);
    }

    /// <summary>
    /// Use this method to retrieve "Not Selected" item when "Not Selected" item value is different from what
    /// gets stored in repository.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="value"></param>
    public static string GetSelectedValue(ListControl control, string notSelectedItemValue)
    {
        if (control.SelectedItem == null)
        {
            return notSelectedItemValue;
        }

        return (control.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE) ? notSelectedItemValue : control.SelectedItem.Value;
    }

    public static void FindByValue(string valueToFind, ListControl controlToSet, bool insertIfNotFound)
    {
        ListItem itm = controlToSet.Items.FindByValue(valueToFind);
        if (itm != null)
        {
            controlToSet.SelectedValue = valueToFind;
        }
        else
        {
            if (string.IsNullOrEmpty(valueToFind))
            {
                //// try finding by 0, if empty string is passed
                itm = controlToSet.Items.FindByValue("0");
                if (itm != null)
                {
                    controlToSet.SelectedValue = "0";
                }
                else
                {
                    throw new ApplicationException(string.Format("Missing value \"{0}\" in Control {1}", valueToFind, controlToSet.ID));
                }
            }
            else
            {
                if (insertIfNotFound)
                {
                    itm = new ListItem();
                    itm.Text = valueToFind + " ** Missing **";
                    itm.Value = valueToFind;
                    controlToSet.Items.Add(itm);
                    controlToSet.SelectedValue = valueToFind;
                }
                else
                {
                    throw new ApplicationException(string.Format("Missing value \"{0}\" in Control {1}", valueToFind, controlToSet.ID));
                }
            }
        }
    }

    public static void FindByValue(string valueToFind, ListControl controlToSet)
    {
        FindByValue(valueToFind, controlToSet, true);
    }

    public static void PopulateInstitutions(ListControl listControl, IEnumerable<Institution> institutions, bool useNameWithProgramOfStudyAsTextField = false)
    {
        listControl.DataSource = institutions;
        listControl.DataTextField = useNameWithProgramOfStudyAsTextField ? "InstitutionNameWithProgOfStudy" : "InstitutionName";
        listControl.DataValueField = "InstitutionID";
        listControl.DataBind();
    }

    public static void PopulateProducts(ListControl listControl, IEnumerable<Product> products)
    {
        listControl.DataSource = products;
        listControl.DataTextField = "ProductName";
        listControl.DataValueField = "ProductID";
        listControl.DataBind();
    }

    public static void PopulateCohorts(ListControl listControl, IEnumerable<Cohort> cohorts)
    {
        listControl.DataSource = cohorts;
        listControl.DataTextField = "CohortName";
        listControl.DataValueField = "CohortID";
        listControl.DataBind();
    }

    public static void PopulateTests(ListControl listControl, IEnumerable<Test> tests)
    {
        listControl.DataSource = tests;
        listControl.DataTextField = "TestName";
        listControl.DataValueField = "TestID";
        listControl.DataBind();
    }

    public static void PopulateTestsByTestId(ListControl listControl, IEnumerable<UserTest> tests)
    {
        listControl.DataSource = tests;
        listControl.DataTextField = "TestName";
        listControl.DataValueField = "TestID";
        listControl.DataBind();
    }

    public static void PopulateTests(DropDownList listControl, IEnumerable<UserTest> tests)
    {
        listControl.DataSource = tests;
        listControl.DataTextField = "TestName";
        listControl.DataValueField = "TestID";
        listControl.DataBind();
    }

    public static void PopulateTest(DropDownList listControl, IEnumerable<UserTest> tests)
    {
        listControl.DataSource = tests;
        listControl.DataTextField = "TestName";
        listControl.DataValueField = "UserTestID";
        listControl.DataBind();
    }

    public static void PopulateTests(ListControl listControl, IEnumerable<UserTest> tests)
    {
        PopulateTests(listControl, tests, "UserTestID");
    }

    public static void PopulateTests(ListControl listControl, IEnumerable<UserTest> tests, string valueFieldName)
    {
        listControl.DataSource = tests;
        listControl.DataTextField = "TestName";
        listControl.DataValueField = valueFieldName;
        listControl.DataBind();
    }

    public static void PopulateGroups(ListControl listControl, IEnumerable<Group> groups)
    {
        listControl.DataSource = groups;
        listControl.DataTextField = "GroupName";
        listControl.DataValueField = "GroupID";
        listControl.DataBind();
    }

    public static void PopulateStudents(ListControl listControl, IEnumerable<StudentEntity> students)
    {
        listControl.Items.Clear();
        listControl.DataSource = students;
        listControl.DataTextField = "StudentName";
        listControl.DataValueField = "StudentId";
        listControl.DataBind();
    }

    public static void PopulateCase(DropDownList dropDownList, IEnumerable<CaseStudy> caseStudies)
    {
        dropDownList.DataSource = caseStudies;
        dropDownList.DataTextField = "CaseName";
        dropDownList.DataValueField = "CaseID";
        dropDownList.DataBind();
    }

    public static void PopulateCase(ListControl listControl, IEnumerable<CaseStudy> caseStudies)
    {
        listControl.DataSource = caseStudies;
        listControl.DataTextField = "CaseName";
        listControl.DataValueField = "CaseID";
        listControl.DataBind();
    }

    public static void PopulateModule(ListControl listControl, IEnumerable<Modules> module)
    {
        listControl.DataSource = module;
        listControl.DataTextField = "ModuleName";
        listControl.DataValueField = "ModuleID";
        listControl.DataBind();
    }

    public static void PopulateCategories(ListControl listControl, IEnumerable<Category> categories)
    {
        listControl.DataSource = categories;
        listControl.DataTextField = "TableName";
        listControl.DataValueField = "CategoryID";
        listControl.DataBind();
    }

    public static void PopulateSubCategories(ListControl listControl, IEnumerable<CategoryDetail> categoryDetails)
    {
        listControl.DataSource = categoryDetails;
        listControl.DataTextField = "Description";
        listControl.DataValueField = "Id";
        listControl.DataBind();
    }

    public static void PopulateTopicTitle(DropDownList dropDownList, IEnumerable<Topic> titles)
    {
        dropDownList.DataSource = titles;
        dropDownList.DataTextField = "TopicTitle";
        dropDownList.DataValueField = "RemediationId";
        dropDownList.DataBind();
    }

    public static void PopulateProducts(ListControl ddTestType, IEnumerable<Topic> topics)
    {
        ddTestType.DataSource = topics;
        ddTestType.DataTextField = "TopicTitle";
        ddTestType.DataValueField = "RemediationId";
        ddTestType.DataBind();
    }

    public static void PopulateClientNeedsCategory(DropDownList dropDownList, IEnumerable<ClientNeedsCategory> clientNeedsCategory)
    {
        dropDownList.DataSource = clientNeedsCategory;
        dropDownList.DataTextField = "Name";
        dropDownList.DataValueField = "Id";
        dropDownList.DataBind();
    }

    public static void PopulateCustomEmails(DropDownList dropDownList, IEnumerable<Email> emails)
    {
        dropDownList.DataSource = emails;
        dropDownList.DataTextField = "Title";
        dropDownList.DataValueField = "EmailId";
        dropDownList.DataBind();
    }

    public static void PopulateAdmin(ListControl dropDownList, IEnumerable<Admin> admins)
    {
        dropDownList.DataSource = admins;
        dropDownList.DataTextField = "UserName";
        dropDownList.DataValueField = "UserId";
        dropDownList.DataBind();
    }

    public static void PopulateQid(ListControl listControl, IEnumerable<Question> qid)
    {
        listControl.DataSource = qid;
        listControl.DataTextField = "QuestionId";
        listControl.DataValueField = "ID";
        listControl.DataBind();
    }

    public static void PopulateProgramOfStudy(ListControl dropDownList, IEnumerable<ProgramofStudy> programofstudies)
    {
        dropDownList.DataSource = programofstudies;
        dropDownList.DataTextField = "ProgramofStudyName";
        dropDownList.DataValueField = "ProgramofStudyId";
        dropDownList.DataBind();
    }

    public static string FormatDate(string inputdate)
    {
        var returnDate = string.Empty;
        if (inputdate.Trim().Equals(string.Empty))
        {
            return string.Empty;
        }

        DateTime _date;
        bool validDate = DateTime.TryParse(inputdate, out _date);

        if (validDate)
        {
            var _month = _date.Month;
            var _day = _date.Day;
            var _year = _date.Year;
            DateTime dt = new DateTime(_year, _month, _day);
            returnDate = dt.ToString("MM/dd/yyyy").ToString();
        }

        return returnDate;
    }

    public static bool CompareDates(string FirstDate, string SecondDate)
    {
        DateTime _dateStart;
        DateTime _dateEnd;
        bool isValidFirst = false;
        bool isValidSecond = false;
        bool results = false;

        isValidFirst = DateTime.TryParse(FirstDate, out _dateStart);
        if (isValidFirst)
        {
            isValidSecond = DateTime.TryParse(SecondDate, out _dateEnd);
            if (isValidSecond)
            {
                int _validDifference = _dateEnd.CompareTo(_dateStart);

                if (_validDifference > 0)
                {
                    results = true;
                }
            }
        }

        return results;
    }

    public static string TranslateHours(string hour)
    {
        string _hour;
        switch (hour)
        {
            case "1":
            case "13":
                _hour = "1";
                break;
            case "2":
            case "14":
                _hour = "2";
                break;
            case "3":
            case "15":
                _hour = "3";
                break;
            case "4":
            case "16":
                _hour = "4";
                break;
            case "5":
            case "17":
                _hour = "5";
                break;
            case "6":
            case "18":
                _hour = "6";
                break;
            case "7":
            case "19":
                _hour = "7";
                break;
            case "8":
            case "20":
                _hour = "8";
                break;
            case "9":
            case "21":
                _hour = "9";
                break;
            case "10":
            case "22":
                _hour = "10";
                break;
            case "11":
            case "23":
                _hour = "11";
                break;
            case "12":
            case "24":
                _hour = "12";
                break;
            default:
                _hour = hour;
                break;
        }

        return _hour;
    }

    public static void PopulateEmails(ListControl listControl, IEnumerable<Email> emails)
    {
        listControl.DataSource = emails;
        listControl.DataTextField = "Title";
        listControl.DataValueField = "EmailId";
        listControl.DataBind();
    }

    public static void PopulateSystems(ListControl listControl, IEnumerable<Systems> systems)
    {
        listControl.DataSource = systems;
        listControl.DataTextField = "System";
        listControl.DataValueField = "SystemID";
        listControl.DataBind();
    }

    public static void DisablePageControlCollectionControls(ControlCollection controlCollection, bool enable)
    {
        if (controlCollection != null)
        {
            foreach (Control c in controlCollection)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Enabled = enable;
                }
                else if (c is ImageButton)
                {
                    ((ImageButton)c).Enabled = enable;
                }
                else if (c is KTPDropDownList)
                {
                    ((KTPDropDownList)c).Enabled = enable;
                }
                else if (c is RadioButtonList)
                {
                    ((RadioButtonList)c).Enabled = enable;
                }
                else if (c is LinkButton)
                {
                    ((LinkButton)c).Enabled = enable;
                }
                else if (c is Image)
                {
                    ((Image)c).Enabled = enable;
                }
            }
        }
    }

    public static void PopulateAssets(ListControl listControl, IEnumerable<Asset> assets)
    {
        listControl.DataSource = assets;
        listControl.DataTextField = "AssetName";
        listControl.DataValueField = "AssetId";
        listControl.DataBind();
    }

    public static void PopulateAssetGroup(ListControl listControl, IEnumerable<AssetGroup> assetsGroups)
    {
        listControl.DataSource = assetsGroups;
        listControl.DataTextField = "AssetGroupName";
        listControl.DataValueField = "AssetGroupId";
        listControl.DataBind();
    }
    #endregion

    #region Internal Methods

    internal static void PopulateProgramofStudy(KTPDropDownList ddlProgramofStudy, IEnumerable<ProgramofStudy> programOfStudies)
    {
        ddlProgramofStudy.DataSource = programOfStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
    }

    internal static void PopulateStates(DropDownList dropDownList, IEnumerable<State> states)
    {
        dropDownList.DataSource = states;
        dropDownList.DataTextField = "StateName";
        dropDownList.DataValueField = "StateId";
        dropDownList.DataBind();
    }

    internal static void PopulateTopics(ListControl listControl, IEnumerable<Topic> topics)
    {
        listControl.DataSource = topics;
        listControl.DataTextField = "TopicTitle";
        listControl.DataValueField = "RemediationId";
        listControl.DataBind();
    }

    internal static string GetDragDropFormatedHtml(IEnumerable<UserAnswer> userAnswers, bool isAlternateQuestion)
    {
        StringBuilder orderMatchTag = new StringBuilder();
        int minimumHeight = userAnswers.Count() * 40;
        orderMatchTag.Append("<div><p style=\"width:310px;float:left;padding-left:100px\">Unordered Options</p><p style=\"width:200px;float:left;padding-left:100px\">Ordered Response</p></div>");
        orderMatchTag.Append("<div style=\"clear:both;\"><ul id=\"sortable1\" class=\"connectedSortable\" style=\"float:left;height:" + minimumHeight + "px\">");
        userAnswers = userAnswers.OrderBy(o => o.initialPos);
        foreach (UserAnswer u in userAnswers)
        {
            orderMatchTag.Append("<li initialPos=\"" + u.initialPos + "\"><div style=\"cursor:pointer\">");
            if (isAlternateQuestion)
            {
                orderMatchTag.Append(u.AlternateAText);
            }
            else
            {
                orderMatchTag.Append(u.AText);
            }

            orderMatchTag.Append("</div></li>");
        }

        orderMatchTag.Append("</ul>");
        orderMatchTag.Append("<div style=\"float:left; padding:70px 20px 0px 10px; vertical-align:middle;height:" + minimumHeight + "px\">");
        orderMatchTag.Append("<img id=\"btnLeft\" src=\"../images/inactive-left.png\"/><br\\>");
        orderMatchTag.Append("<img id=\"btnRight\" src=\"../images/inactive-right.png\"/>");
        orderMatchTag.Append("</div>");
        orderMatchTag.Append("<ul id=\"sortable2\" class=\"connectedSortable\" style=\"float:left;height:" + minimumHeight + "px\">");
        orderMatchTag.Append("</ul>");
        orderMatchTag.Append("<div style=\"float:left; padding:70px 0px 0px 10px;width:100px; vertical-align:middle;\">");
        orderMatchTag.Append("<table><tbody><tr><td><img id=\"btnUp\" src=\"../images/inactive-up.png\"/></td></tr>");
        orderMatchTag.Append("<tr><td><img id=\"btnDown\" src=\"../images/inactive-down.png\"/></td></tr></tbody></table>");
        orderMatchTag.Append("</div></div>");
        return orderMatchTag.ToString();
    }

    internal static string GetDragDropFormatedHtmlForReview(IEnumerable<UserAnswer> userAnswers, Question userQuestion, bool isAlternateQuestion)
    {
        StringBuilder orderMatchTag = new StringBuilder();

        int index = 0;
        int minimumHeight = userAnswers.Count() * 40;
        orderMatchTag.Append("<div><p style=\"width:210px;float:left;padding-left:100px\">Your Response</p>");
        if (userQuestion.Correct == 0 || userQuestion.Correct == 2)
        {
            orderMatchTag.Append("<p style=\"width:100px;float:left;color:red;font-weight:bold;\">INCORRECT</p>");
        }
        else
        {
            orderMatchTag.Append("<p style=\"width:100px;float:left;color:green;font-weight:bold;\">CORRECT</p>");
        }

        orderMatchTag.Append("<p style=\"width:200px;float:left;padding-left:100px\">Correct Response</p></div>");
        orderMatchTag.Append("<ul id=\"sortableR1\" style=\"float:left;clear:both;height:" + minimumHeight + "px\">");
        List<UserAnswer> userAnswerlist = userAnswers.OrderBy(r => r.initialPos).ToList();
        string[] answerIndexes = userQuestion.OrderedIndexes.Split(',');
        foreach (string s in answerIndexes)
        {
            if (!string.IsNullOrEmpty(s))
            {
                UserAnswer ua = new UserAnswer();
                index = s.ToInt() - 1;
                ua = userAnswerlist[index];
                orderMatchTag.Append("<li class=\"dragdroprem\"  initialPos=\"" + ua.initialPos + "\"><div style=\"cursor:default\">");
                if (isAlternateQuestion)
                {
                    orderMatchTag.Append(ua.AlternateAText);
                }
                else
                {
                    orderMatchTag.Append(ua.AText);
                }

                orderMatchTag.Append("</div></li>");
            }
        }

        orderMatchTag.Append("</ul>");
        orderMatchTag.Append("<div style=\"float:left; padding:70px 20px 0px 10px; vertical-align:middle;height:" + minimumHeight + "px\">");
        orderMatchTag.Append("<img id=\"btnLeftR\" src=\"../images/inactive-left.png\"/><br\\>");
        orderMatchTag.Append("<img id=\"btnRightR\" src=\"../images/inactive-right.png\"/>");
        orderMatchTag.Append("</div>");
        orderMatchTag.Append("<ul id=\"sortableR2\" style=\"float:left;height:" + minimumHeight + "px\">");
        userAnswers = userAnswers.OrderBy(o => o.AIndex);
        foreach (UserAnswer u in userAnswers)
        {
            orderMatchTag.Append("<li class=\"dragdroprem\" initialPos=\"" + u.initialPos + "\"><div style=\"cursor:default\">");
            if (isAlternateQuestion)
            {
                orderMatchTag.Append(u.AlternateAText);
            }
            else
            {
                orderMatchTag.Append(u.AText);
            }

            orderMatchTag.Append("</div></li>");
        }

        orderMatchTag.Append("</ul>");
        orderMatchTag.Append("<div style=\"float:left; padding:70px 0px 0px 0px; vertical-align:middle;\">");
        orderMatchTag.Append("<img id=\"btnUpR\" src=\"../images/inactive-up.png\"/><br\\>");
        orderMatchTag.Append("<img id=\"btnDownR\" src=\"../images/inactive-down.png\"/>");
        orderMatchTag.Append("</div>");
        return orderMatchTag.ToString();
    }
    #endregion
}