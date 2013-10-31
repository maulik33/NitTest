<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="True"
    Inherits="ADMIN_EmailReceiver" Title="Kaplan Nursing" CodeBehind="EmailReceiver1.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2);
        });

        function ValidateNewEmail() {
            var ddCustomEmails = document.getElementById('<%=ddCustomEmails.ClientID%>');
            if (ddCustomEmails.options[ddCustomEmails.selectedIndex].value == '-1') {
                alert('Please select a custom Email to edit.');
                return false;
            }
            return true;
        }

        function ValidatePopulate() {
            var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var lbxGroup = document.getElementById('<%=lbxGroup.ClientID %>');

            if (lbxInstitution.options.selectedIndex == -1
                && lbxCohort.options.selectedIndex == -1
                && lbxGroup.options.selectedIndex == -1) {
                alert('Please select any Institution, Cohort or Group to populate Student and Admin in those selected groups.');
                $('Panel1').hide();
                return false;
            }
            return true;
        }

        function ValidateSend() {
            var ddCustomEmails = document.getElementById('<%=ddCustomEmails.ClientID%>');
            var cbxEmailTo = document.getElementById('<%=cbxEmailTo.ClientID %>');
            var CheckBoxList2 = document.getElementById('<%=CheckBoxList2.ClientID %>');

            if (ddCustomEmails.options[ddCustomEmails.selectedIndex].value == '-1' && !IsEmailToSelected()) {
                alert('Please select Email to send.');
                return false;
            }
            return true;
        }

        function IsEmailToSelected() {
            //Get the checkboxlist object
            var checkBoxList = document.getElementById('<%=cbxEmailTo.ClientID %>');
            //Get the number of checkboxes in the checkboxlist
            var numCheckBoxItems = checkBoxList.cells.length;
            for (i = 0; i < numCheckBoxItems; i++) {
                //Get the checkboxlist item
                var checkBox = document.getElementById(checkBoxList.id + '_' + [i]);
                //Check if the checkboxlist item exists, and if it is checked
                if (checkBox != null && checkBox.checked) { return true; }
            }
            return false;
        }

        function clearModalCtrls() {
            var txtKeyword = $get('<%=txtEmailKeyword.ClientID%>');
            if (txtKeyword != null)
                txtKeyword.value = "";
            var errorMsg = $get('<%=lblMessage.ClientID%>');
            if (errorMsg != null)
                errorMsg.innerText = "";
            var lstItems = $get('<%=lbxSearchItems.ClientID%>');
            if (lstItems != null)
                lstItems.innerText = "";
        }
        function setCursor() {
            document.body.style.cursor = "wait";
        }
        function restoreCursor() {
            document.body.style.cursor = "default";
        }
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: #666699;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .modalWindow
        {
            min-width: 500px;
            min-height: 200px;
            background: #f0f0fe;
            border-width: 3px;
        }
    </style>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnEmailSearch"
        PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnCancelScript="clearModalCtrls();" />
    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="black" Radius="6" Corners="All" />
    <asp:Panel ID="pnlPopup" runat="server" Style="display: none">
        <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow">
            <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <br />
                            <br />
                        </tr>
                        <tr>
                            <td>
                                <strong>Keyword :</strong>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmailKeyword" runat="server" width="350px"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnKeyword" runat="server" Text="Search" OnClick="btnKeyword_Click"
                                    OnClientClick="setCursor();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMessage" Visible="false" ForeColor="Red" ViewStateMode="Disabled"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;&nbsp;
                                <KTP:KTPListBox ID="lbxSearchItems" runat="server" AutoPostBack="True" Height="300px"
                                    Width="500px" ShowSelectAll="false" ShowNotSelected="false" SelectionMode="Single" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
                &nbsp&nbsp&nbsp
                <asp:Button ID="btnClose" runat="server" Text="Close" />
                <br />
                <br />
            </div>
        </asp:Panel>
    </asp:Panel>
    <table width="100%" border="0" cellspacing="2" cellpadding="3">
        <tr>
            <td colspan="3" align="left">
                <div style="margin-left: 10px">
                    <asp:Label ID="Messenger" runat="server" ForeColor="Red" /></div>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="headfont">
                <b>View > Email</b>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left" style="border-bottom: 1px solid #330099;">
                <strong>
                    <br />
                    Step 1: Select Email Type</strong>
            </td>
        </tr>
        <tr class="datatable1">
            <td colspan="2">
                <strong>Send Custom Emails:<br />
                </strong>
                <br />
                <KTP:KTPDropDownList ID="ddCustomEmails" runat="server" Width="250px" ShowSelectAll="false"
                    ShowNotSelected="true">
                </KTP:KTPDropDownList>
                <asp:Button ID="btnEmailSearch" runat="server" OnClick="btnEmailSearch_Click" Text="Search" />
                &nbsp;
                <asp:Button ID="btnNewEmail" runat="server" Text="New Email" OnClick="btnNewEmail_Click" />
                &nbsp;
                <asp:Button ID="btnEditEMail" runat="server" Text="Edit" Width="88px" OnClientClick="return ValidateNewEmail();"
                    OnClick="btnEditEMail_Click" />
                <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Email Admins</asp:ListItem>
                    <asp:ListItem Value="1">Email Students</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td rowspan="2" bgcolor="#eeeeee" align="center">
                <asp:Button ID="btnReset" runat="server" Text="Reset" Width="150px" OnClick="Reset_Click" />
            </td>
        </tr>
        <tr class="datatable1">
            <td colspan="2" style="border-top: solid 3px #ffffff; height: 26px">
                <strong>Send User Information:<br />
                </strong>
                <br />
                <asp:CheckBoxList ID="cbxEmailTo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Email Students</asp:ListItem>  
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="30" style="text-align: left">
                <strong>
                    <br />
                    <br />
                    Step 2: Select Users to Receive Emails</strong>
            </td>
        </tr>
        <tr><td colspan="3" align="left" bgcolor="#eeeeee">
          <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>
           <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" ShowNotSelected="false"></KTP:KTPDropDownList>
        </td>
        </tr>
        <tr>
            <td style="height: 18px">
                <div style="background-color: #f0f0fe; width: 200px; padding: 3px;">
                    <asp:Label ID="Label1" runat="server" Text="Institution"></asp:Label></div>
            </td>
            <td style="height: 18px;">
                <div style="background-color: #f0f0fe; width: 200px; padding: 3px;">
                    <asp:Label ID="Label2" runat="server" Text="Cohort" BackColor="#eeeeee"></asp:Label></div>
            </td>
            <td style="height: 18px">
                <div style="background-color: #f0f0fe; width: 200px; padding: 3px;">
                    <asp:Label ID="Label3" runat="server" Text="Group" BackColor="#eeeeee"></asp:Label></div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <KTP:KTPListBox ID="lbxInstitution" runat="server" Height="250px" Width="200px" AutoPostBack="True"
                    ShowSelectAll="false" ShowNotSelected="false" SelectionMode="Multiple">
                </KTP:KTPListBox>
            </td>
            <td align="left">
                <KTP:KTPListBox ID="lbxCohort" runat="server" Height="250px" Width="200px" AutoPostBack="True"
                    ShowSelectAll="false" ShowNotSelected="false" SelectionMode="Multiple">
                </KTP:KTPListBox>
            </td>
            <td align="left">
                <KTP:KTPListBox ID="lbxGroup" runat="server" Height="250px" Width="200px" AutoPostBack="True"
                    ShowSelectAll="false" ShowNotSelected="false" SelectionMode="Multiple">
                </KTP:KTPListBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left" bgcolor="#eeeeee" height="30">
                <asp:Button ID="btnPopulate" runat="server" Text="Populate Student and Admin List"
                    OnClientClick="return ValidatePopulate();" OnClick="btnPopulate_Click" />
                If you do not click this button, it will use the info above.
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left" bgcolor="#eeeeee" height="30">
                <asp:Label ID="Label15" runat="server" Text="Search Student: "></asp:Label><asp:TextBox
                    ID="TextBox11" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSearchStudent"
                        runat="server" Text="Search" OnClick="SearchStudent_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label16" runat="server" Text="Search Admins: "></asp:Label><asp:TextBox
                    ID="TextBox12" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSearchAdmin"
                        runat="server" Text="Search" OnClick="SearchAdmin_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: left">
                This will clear selection above<asp:Panel ID="Panel1" runat="server" Width="100%"
                    Visible="False">
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="background-color: #f0f0fe; width: 200px; padding: 3px;">
                                    <asp:Label ID="Label14" runat="server" Text="Student"></asp:Label></div>
                            </td>
                            <td>
                                <div style="background-color: #f0f0fe; width: 200px; padding: 3px;">
                                    <asp:Label ID="Label17" runat="server" Text="Admin"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <KTP:KTPListBox ID="lbxStudent" runat="server" Height="250px" Width="200px" SelectionMode="Multiple"
                                    ShowSelectAll="false" ShowNotSelected="false">
                                </KTP:KTPListBox>
                            </td>
                            <td>
                                <KTP:KTPListBox ID="lbxAdmin" runat="server" Height="250px" Width="200px" SelectionMode="Multiple"
                                    ShowSelectAll="false" ShowNotSelected="false">
                                </KTP:KTPListBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <strong>
                    <br />
                    <br />
                    <br />
                    Step 3: Schedule Email</strong>
            </td>
        </tr>
        <tr class="datatable1">
            <td>
                <asp:Button ID="btnSendNow" runat="server" Text="Send Now" OnClick="SendNow_Click" />
            </td>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSendLater" runat="server" Text="Send Later" OnClick="btnSendLater_Click" />
                        </td>
                        <td>
                            <KTP:Calendar ID="dtSendLater" runat="server" CalendarFormat="MMDDYYYYhm" Width="180px"
                                Visible="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
