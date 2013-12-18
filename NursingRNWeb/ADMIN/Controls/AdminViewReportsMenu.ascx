<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminViewReportsMenu"
    CodeBehind="AdminViewReportsMenu.ascx.cs" %>
<script src="../../JS/jquery-1.4.3.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $('#navigation ul:eq(0) > li').removeClass('menuOdd');
        $('#navigation ul:eq(0) > li').removeClass('menuEven');
        $('#navigation ul:eq(0)> li:nth-child(odd)').addClass('menuOdd');
        $('#navigation ul:eq(0)> li:nth-child(even)').addClass('menuEven');

        var preimg = "";
        $("#navigation ul li").mouseover(function () {
            preimg = $(this).find('img').attr('src');
            $(this).find('img').attr('src', '../Images/arrow_mousein.bmp');
        }).mouseleave(function () {
            var submenu = 0;
            $("#navigation ul li ul li").mouseenter(function () {
                preimg1 = $("#navigation ul li").find('img').attr('src');
                $(this).find('img').attr('src', '../Images/arrow_mousein.bmp');
                submenu = 1;
            }).mouseleave(function () {
                submenu = 1;
                //$(this).find('img').attr('src', '../Imaiges/arrow_mousein.bmp');
                //                  if (preimg == "../Images/arrow_mouseout.bmp") {
                //                      $(this).find('img').attr('src', '../Images/arrow_mouseout.bmp');
                //                  } else {
                //                      $(this).find('img').attr('src', '../Images/arrow_mouseout_1.bmp');
                //                  }
            });
            if (submenu == 0) {
                if (preimg == "../Images/arrow_mouseout.bmp") {
                    $(this).find('img').attr('src', '../Images/arrow_mouseout.bmp');
                } else {
                    $(this).find('img').attr('src', '../Images/arrow_mouseout_1.bmp');
                }
            } else {
                if (preimg == "../Images/arrow_mouseout.bmp") {
                    $(this).find('img').attr('src', '../Images/arrow_mouseout.bmp');
                } else {
                    $(this).find('img').attr('src', '../Images/arrow_mouseout_1.bmp');
                }
            }

        });


    }); 
      
</script>
<style type="text/css">
    #navigation
    {
        width: 100%;
        font-size: 14px;
        font-weight: bold;
        float: left;
    }
    #navigation ul
    {
        margin: 0px;
        padding: 0px;
        background-color: #E1E2F7;
        font-family: Arial;
    }
    #navigation ul li
    {
        height: 35px;
        line-height: 35px;
        list-style: none;
        padding-left: 20px;
        border-width: 1px;
        cursor: pointer;
        white-space: nowrap;
    }
    #navigation ul li:hover
    {
        background-color: #372B91;
        position: relative;
        color: White;
        font-weight: bold;
        width: 70%;
        font-family: Arial;
    }
    
    #navigation ul ul
    {
        display: none;
        position: absolute;
        left: 280px;
        top: 0px;
        width: 300px;
        padding-right: 0px;
        background-color: #372B91;
    }
    #navigation ul li ul li
    {
        color: White;
    }
    #navigation ul li:hover ul
    {
        display: block;
    }
    #navigation ul li:hover a
    {
        color: White;
        font-size: smaller;
    }
    .layerbg
    {
        background: url('../Images/layerbg.bmp');
    }
    .headerstyle
    {
        font-family: Arial;
        font-size: 24px;
        font-weight: bold;
        color: #372B91;
        background: url('../Images/graybg.bmp');
        vertical-align: text-top;
    }
    
    hr
    {
        color: #372B91;
        height: 1px;
    }
    .menuEven
    {
        background-color: #f4f4fe;
    }
    .menuOdd
    {
        background-color: #E1E2F7;
    }
</style>
<fieldset id="fldsetViewReport" runat="server" style="border: 0px;">
    <%--<legend>View Reports</legend>  --%>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headerstyle" align="center" style="height: 40px;">
                View Reports
            </td>
        </tr>
        <tr>
            <td class="headerstyle" align="center">
            </td>
        </tr>
    </table>
    <table id="tblViewReportsMenu" border="0" cellpadding="0" cellspacing="0" width="100%"
        class="layerbg">
        <tr>
            <td width="18%" align="right" valign="top" style="padding-top: 15px; padding-right: 8px;">
                <img src="../Images/reportslady.bmp" alt="" />
            </td>
            <td width="82%" valign="top">
                &nbsp;
                <br />
                <table width="96%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <div id="navigation">
                                <ul>
                                    <li class="menuOdd" id="liNursingFaculty" runat="server">
                                        <img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Reports for Nursing
                                        Faculty
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportStudentReportCard" runat="server" NavigateUrl="~/Admin/ReportStudentReportCard.aspx?Type=2"
                                                    Text="Student Report Card" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportRemediationByTest" runat="server" NavigateUrl="~/Admin/ReportRemediationByTest.aspx"
                                                    Text="Remediation Time" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortTestByQuestion" runat="server" NavigateUrl="~/Admin/ReportCohortTestByQuestion.aspx?Mode=1"
                                                    Text="Test Result by Question" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortByTest" runat="server" NavigateUrl="~/Admin/ReportCohortByTest.aspx?Mode=1"
                                                    Text="Cohort by Test" Font-Underline="True" /></li>
                                        </ul>
                                    </li>
                                    <li class="menuEven" id="liDeansDirectors" runat="server">
                                        <img src="../Images/arrow_mouseout_1.bmp" alt="" />&nbsp;&nbsp;Reports for Deans/Directors
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortByTest1" runat="server" NavigateUrl="~/Admin/ReportCohortByTest.aspx"
                                                    Text="Cohort by Test" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortTestByQuestion1" runat="server" NavigateUrl="~/Admin/ReportCohortTestByQuestion.aspx"
                                                    Text="Test Result by Question" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortComparisons" runat="server" NavigateUrl="~/Admin/ReportCohortComparisons.aspx"
                                                    Text="Category Comparisons" Font-Underline="True" /></li>
                                        </ul>
                                    </li>
                                    <li class="menuOdd" id="liInstitutionalAdmins" runat="server">
                                        <img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Reports for Multi-Campus
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportByInstitution" runat="server" NavigateUrl="~/Admin/ReportByInstitution.aspx"
                                                    Text="Tests By Institution" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportInstitutionPerformance" runat="server" NavigateUrl="~/ADMIN/ReportInstitutionPerformance.aspx"
                                                    Text="Institution Performance" Font-Underline="True" /></li>
                                                    <li>
                                                <asp:HyperLink ID="hyplnkReportMultiCampusReportCard" runat="server" NavigateUrl="~/ADMIN/ReportMultiCampusReportCard.aspx"
                                                    Text="Multi-Campus Report Card" Font-Underline="True" /></li>
                                                 <li>
                                                <asp:HyperLink ID="hyplnkReportInstitutionTestByQuestion" runat="server" NavigateUrl="~/ADMIN/ReportInstitutionTestByQuestion.aspx"
                                                    Text="Institutional Test Result by Question" Font-Underline="True" /></li>
                                        </ul>
                                    </li>
                                    <li class="menuEven" id="liNormingData" runat="server">
                                        <img src="../Images/arrow_mouseout_1.bmp" alt="" />&nbsp;&nbsp;Reports for Norming
                                        Data
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportPerformanceByQuestion" runat="server" NavigateUrl="~/ADMIN/ReportPerformanceByQuestion.aspx"
                                                    Text="Student Performance by Question" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportStudentSummaryByAnswerChoice" runat="server" NavigateUrl="~/ADMIN/ReportStudentSummaryByAnswerChoice.aspx"
                                                    Text="Student Summary by Answer Choice" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportStudentSummaryByQuestion" runat="server" NavigateUrl="~/ADMIN/ReportStudentSummaryByQuestion.aspx"
                                                    Text="Student Summary by Question" Font-Underline="True" /></li>
                                        </ul>
                                    </li>
                                    <li class="menuOdd" id="liCaseReports" runat="server">
                                        <img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Reports for Case Studies
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportStudentResultByCase" runat="server" NavigateUrl="~/Admin/ReportStudentResultByCase.aspx"
                                                    Text="Student Results by Case" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportStudentReportCardByModule" runat="server" NavigateUrl="~/Admin/ReportStudentReportCardByModule.aspx"
                                                    Text="Student Report Card by Module" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCasebyCohort" runat="server" NavigateUrl="~/Admin/ReportCasebyCohort.aspx"
                                                    Text="Case results by Cohort" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCohortResultByModule" runat="server" NavigateUrl="~/Admin/ReportCohortResultByModule.aspx"
                                                    Text="Cohort Results by Module" Font-Underline="True" /></li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkReportCaseComparisonbyModule" runat="server" NavigateUrl="~/Admin/ReportCaseComparisonbyModule.aspx"
                                                    Text="Case Comparison by Module" Font-Underline="True" /></li>
                                        </ul>
                                    </li>
                                    <li class="menuEven" id="liHelpfulDocument" runat="server">
                                        <img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Resources
                                        <ul>
                                            <li>
                                                <asp:HyperLink ID="hyplnkSearchHelpfulDoc" runat="server" NavigateUrl="~/ADMIN/SearchHelpfulDocuments.aspx?IsLink=0"
                                                    Text="Documents" Font-Underline="True" />
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkSearchVideoLink" runat="server" NavigateUrl="~/ADMIN/SearchHelpfulDocuments.aspx?IsLink=1"
                                                    Text="Video Links" Font-Underline="True" />
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</fieldset>
