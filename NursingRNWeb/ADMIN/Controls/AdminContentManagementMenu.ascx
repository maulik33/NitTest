<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminContentManagement"
    CodeBehind="AdminContentManagementMenu.ascx.cs" %>
<script src="../../JS/jquery-1.4.3.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $('#tblSub tr:even').css('background', '#E1E2F7'); //

    });

</script>
<style type="text/css">
    #tblSub td
    {
        width: 100%;
        font-size: 14px;
        font-weight: bold;
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
    
    #tblMain a
    {
        color: #372b91;
        width: 100%;
        padding-bottom: 5px;
        font-size: 12px;
        font-weight: lighter;
        font-style: normal;
    }
</style>
<fieldset style="border: 0px;">
    <%--<legend>Content Management</legend>--%>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <img src="../Images/purpleline.bmp" width="100%" height="1px" />
            </td>
        </tr>
        <tr>
            <td class="headerstyle" align="center" style="height: 45px; padding-top: 10px;">
                Content Management
                <div style="float: right; padding-right: 10px;">
                    <asp:HyperLink ID="HyperLink6" ImageUrl="../../Images/realeasebtn.bmp" runat="server"
                        NavigateUrl="~/CMS/ReleaseChoice.aspx?CMS=1" Text="Release" Font-Underline="True" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="headerstyle" align="center">
            </td>
        </tr>
    </table>
    <table id="tblContentManagement" border="0" cellpadding="0" cellspacing="0" width="100%"
        class="layerAdminAccountbg">
        <tr>
            <td width="18%" align="right" valign="top" style="padding-top: 30px; padding-right: 10px;">
                <img src="../Images/contentlady.bmp" />
            </td>
            <td width="82%">
                &nbsp;<br />
                <br />
                <table id="tblMain" width="96%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <table id="tblSub" width="100%" cellpadding="10" cellspacing="0">
                                <tr class="menuOdd" id="trCms" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;CMS
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlUploadQuestions" runat="server" NavigateUrl="~/CMS/UploadQuestions.aspx?CMS=1"
                                            Text="Upload Questions" Font-Underline="True" Width="110px"/>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlUploadTopics" runat="server" NavigateUrl="~/CMS/UploadTopics.aspx?CMS=1"
                                            Text="Upload Topics" Font-Underline="True" Width="90px"/>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CMS/SearchQuestion.aspx?searchback=0&CMS=1"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/CMS/EditQuestion.aspx?CMS=1&ActionType=1"
                                            Text="Add" Font-Underline="True" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr class="menuEven" id="trCustomTests" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout_1.bmp" alt="" />&nbsp;&nbsp;Custom
                                        Tests
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/CMS/CustomTest.aspx?CMS=1&mode=4"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink25" runat="server" NavigateUrl="~/CMS/NewCustomTest.aspx?TestID=-1&CMS=1&SearchCondition=&Sort="
                                            Text="Add" Font-Underline="True" />
                                    </td>
                                </tr>
                                <tr class="menuOdd" id="trTestCategories" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Test Categories
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td align="right">
                                        <asp:HyperLink ID="HyperLink27" runat="server" NavigateUrl="~/CMS/TestCategories.aspx?CMS=1&mode=1"
                                            Text="Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="menuEven" id="trHtmlLinks" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout_1.bmp" alt="" />&nbsp;&nbsp;Html
                                        Links
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/Admin/AVPItems.aspx?CMS=1&Mode=1"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink31" runat="server" NavigateUrl="~/Admin/NewAVPItems.aspx?TestID=-1&CMS=1&Mode=1"
                                            Text="Add" Font-Underline="True" />
                                    </td>
                                </tr>
                                <tr class="menuOdd" id="trLippincott" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Lippincott
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink32" runat="server" NavigateUrl="~/CMS/Lippincott.aspx?CMS=1&Mode=1"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink34" runat="server" NavigateUrl="~/CMS/NewLippincott.aspx?CMS=1&IID=-1&Mode=1"
                                            Text="Add" Font-Underline="True" />
                                    </td>
                                </tr>
                                <tr class="menuEven" id="trNorming" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout_1.bmp" alt="" />&nbsp;&nbsp;Norming
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink35" runat="server" NavigateUrl="~/Admin/Norm.aspx?CMS=1"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/Norm.aspx?CMS=1"
                                            Text="Add" Font-Underline="True" />
                                    </td>
                                </tr>
                                <tr class="menuOdd" id="trProbability" runat="server">
                                    <td>
                                        &nbsp;&nbsp;<img src="../Images/arrow_mouseout.bmp" alt="" />&nbsp;&nbsp;Probability
                                    </td>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/Percentile.aspx?CMS=1"
                                            Text="View&nbsp;/&nbsp;Edit" Font-Underline="True" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <%--<tr align="center" >
                        <td style="padding-bottom:10px;"><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/CMS/ReleaseChoice.aspx?CMS=1" Text="Release" Font-Underline="True" /></td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
</fieldset>
