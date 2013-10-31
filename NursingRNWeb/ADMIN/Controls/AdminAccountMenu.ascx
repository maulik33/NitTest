<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminAccountMenu" CodeBehind="AdminAccountMenu.ascx.cs" %>
<script src="../../JS/jquery-1.4.3.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $('#tblSubAcnt > tr:even').css('background', '#E1E2F7');

    });

</script>
<style type="text/css">
    #tblSubAcnt td
    {
        width: 100%;
        font-size: 14px;
        font-weight: bold;
        padding-left: 5px;
    }
    #tblSubAcnt a
    {
        color: #372B91;
        font-size: 12px;
    }
    .smalldes
    {
        width: 100%;
        font-size: 12px;
        font-weight: lighter;
        font-style: normal;
        padding-left: 20px;
    }
    #tblSub a
    {
        color: #372b91;
        width: 100%;
        font-size: 12px;
        font-weight: lighter;
        font-style: normal;
    }
    .layerAdminAccountbg
    {
        background: url('../Images/longlayerbg.bmp');
    }
    .admacnttd
    {
        padding-top: 10px;
    }
    .admacnttd1
    {
        padding-top: 0px;
        padding-bottom: 10px;
        vertical-align: super;
    }
    .MenuViewEditlnk
    {
        color: #372B91;
        font-size: small;
    }
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
        padding-left: 10px;
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
    .menuEven1
    {
        background-color: #f4f4fe;
    }
    .menuOdd1
    {
        background-color: #E1E2F7;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {

        $('#navigation ul:eq(0) > li').removeClass('menuOdd1');
        $('#navigation ul:eq(0) > li').removeClass('menuEven1');
        $('#navigation ul:eq(0)> li:nth-child(odd)').addClass('menuOdd1');
        $('#navigation ul:eq(0)> li:nth-child(even)').addClass('menuEven1');

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
<fieldset style="border: 0px;">
    <%--<legend>Administer Account</legend>--%>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <img src="../Images/purpleline.bmp" width="100%" height="1px" />
            </td>
        </tr>
        <tr>
            <td class="headerstyle" align="center" style="height: 45px; padding-top: 10px;">
                Administer Account
            </td>
        </tr>
        <tr>
            <td class="headerstyle" align="center">
            </td>
        </tr>
    </table>
    <table id="tblAccountInformation" border="0" cellpadding="0" cellspacing="0" width="100%"
        class="layerAdminAccountbg">
        <tr>
            <td width="18%" align="right" valign="top" style="padding-top: 15px; padding-right: 10px;">
                <img src="../Images/adminacntlady.bmp" />
            </td>
            <td width="82%">
                &nbsp;&nbsp;<br />
                <table id="tblMain" width="96%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td align="left">
                            <table id="tblSubAcnt" width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tbody>
                                    <tr class="menuOdd" id="trInsitution" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout.bmp" alt="" runat="server" id="imgInsitution" />&nbsp;&nbsp;Institution
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add new Institution, edit Institution details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkViewInstitutionList" runat="server" NavigateUrl="~/Admin/InstitutionList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkAddInstitution" runat="server" NavigateUrl="~/Admin/InstitutionEdit.aspx?actionType=1"
                                                Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuEven" id="trAdministrator" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout_1.bmp" alt="" runat="server" id="imgAdministrator" />&nbsp;&nbsp;Administrator
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add new Administrator, edit Administrator details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkViewAdminList" runat="server" NavigateUrl="~/Admin/AdminList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkAddAdmin" runat="server" NavigateUrl="~/Admin/AdminEdit.aspx?actionType=1"
                                                Text="Add" Font-Underline="True"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuOdd" id="trProgam" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout.bmp" alt="" runat="server" id="imgProgam" />&nbsp;&nbsp;Program
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add new Program, edit Programs details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding-right: 15px;">
                                            <asp:HyperLink ID="hyplnkBulkModify" runat="server" NavigateUrl="~/Admin/BulkModifyProgram.aspx"
                                                Text="Bulk&nbsp;&nbsp;Modify" Font-Underline="True"  Visible="false"/>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkViewProgramList" runat="server" NavigateUrl="~/Admin/ProgramList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkAddProgram" runat="server" NavigateUrl="~/Admin/ProgramEdit.aspx?actionType=1"
                                                Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuEven" id="trCohort" runat="server">
                                        <td style="width: 40%;">
                                            <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout_1.bmp" alt="" runat="server" id="imgCohort" />&nbsp;&nbsp;Cohort
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add new Cohort, edit Cohort details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:HyperLink ID="hyplnkViewCohortList" runat="server" NavigateUrl="~/Admin/CohortList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />&nbsp;
                                        </td>
                                        <td style="width: 10%;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:HyperLink ID="hyplnkAddCohort" runat="server" NavigateUrl="~/Admin/CohortEdit.aspx?actionType=1"
                                                Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuOdd" id="trGroup" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout.bmp" alt="" runat="server" id="imgGroup" />&nbsp;&nbsp;Group
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add new Group, edit Group details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkViewGroupList" runat="server" NavigateUrl="~/Admin/GroupList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkAddGroup" runat="server" NavigateUrl="~/Admin/GroupEdit.aspx?actionType=1"
                                                Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuEven" id="trStudent" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout_1.bmp" alt="" runat="server" id="imgStudent" />&nbsp;&nbsp;Student
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Edit Student Details</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyplnkViewUserList" runat="server" NavigateUrl="~/Admin/UserList.aspx"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuOdd" id="trAssignStudents" runat="server">
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout.bmp" alt="" runat="server" id="imgAssignStudents" />&nbsp;&nbsp;Assign
                                                        Students
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Assign Students</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:HyperLink ID="hyplnkEditUserListXML" runat="server" NavigateUrl="~/Admin/UserListXML.aspx"
                                                Text="Edit" Font-Underline="True" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuEven" id="trHelpfulDocument" runat="server">
                                        <td style="width: 50%;">
                                            <table width="80%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="admacnttd">
                                                        &nbsp;&nbsp;<img src="../../Images/arrow_mouseout_1.bmp" alt="" runat="server" id="img1" />&nbsp;&nbsp;Documents/Links
                                                        <p class="smalldes" style="text-indent: 8px;">
                                                            Add, Edit and Delete Documents/Links</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:HyperLink ID="hlViewEditHelpfullDoc" runat="server" NavigateUrl="~/ADMIN/SearchHelpfulDocuments.aspx?IsLink=0"
                                                Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />&nbsp;
                                        </td>
                                        <td style="width: 10%;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:HyperLink ID="hlUploadHelpfulDocument" runat="server" NavigateUrl="~/ADMIN/UploadHelpfulDocument.aspx?Id=0"
                                                Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr class="menuOdd" id="trReportTestsScheduledbyDate" runat="server">
                                        <td align="left">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <div id="navigation" align="left">
                                                            <ul style="margin-top: 0px; list-style: none; padding-left: 0px;">
                                                                <li class="menuOdd1" id="liNursingFaculty" runat="server">
                                                                    <img src="../../Images/arrow_mouseout.bmp" alt="" runat="server" id="img2" />
                                                                    &nbsp;Reports for Administrators
                                                                    <ul>
                                                                        <li>
                                                                            <asp:HyperLink ID="hlReportTestsScheduledbyDate" runat="server" NavigateUrl="~/Admin/ReportTestsScheduledbyDate.aspx"
                                                                                Text="Tests Scheduled by Date" Font-Underline="True" /></li>
                                                                    </ul>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10%">
                &nbsp;
            </td>
        </tr>
    </table>
</fieldset>
