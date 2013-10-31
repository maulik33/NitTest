<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FRBank.ascx.cs" Inherits="NursingRNWeb.STUDENT.ASCX.FRBank" %>
<script type="text/javascript">
    var xSysPos, ySysPos, xCatPos, yCatPos;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function BeginRequestHandler(sender, args) {
        var postBackElem = args.get_postBackElement();
        xSysPos = $get('FRBank_lbxTopic').scrollLeft;
        ySysPos = (postBackElem != null && postBackElem.id == 'FRBank_lbxCategories') ? 0 : $get('FRBank_lbxTopic').scrollTop;
        xCatPos = $get('FRBank_lbxCategories').scrollLeft;
        yCatPos = $get('FRBank_lbxCategories').scrollTop;
    }

    function EndRequestHandler(sender, args) {
        $get('FRBank_lbxTopic').scrollLeft = xSysPos;
        $get('FRBank_lbxTopic').scrollTop = ySysPos;

        $get('FRBank_lbxCategories').scrollLeft = xCatPos;
        $get('FRBank_lbxCategories').scrollTop = yCatPos;
    }
</script>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
</div>
<div class="clearAll"></div>
<table>
    <tr>
        <td>
            <b>CATEGORY&nbsp;[ctrl to select multiple]</b>
            <br />
            <KTP:KTPListBox ID="lbxCategories" runat="server" SelectionMode="Multiple" ShowSelectAll="False"
                ShowNotSelected="False" Width="300px" Height="225px" OnSelectedIndexChanged="lbxSystem_SelectedIndexChanged"
                AutoPostBack="true" />
        </td>
        <td width="100px">
        </td>
        <td>
            <b>TOPIC&nbsp;[ctrl to select multiple]</b>
            <br />
            <KTP:KTPListBox ID="lbxTopic" runat="server" SelectionMode="Multiple" ShowSelectAll="False"
                AutoPostBack="true" ShowNotSelected="False" Width="300px" Height="225px" OnSelectedIndexChanged="lbxTopic_SelectedIndexChanged" />
        </td>
    </tr>
</table>
<div runat="server" id="Categories">
</div>
<div style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
    <asp:Label ID="lblAvailableItems" runat="server" Text=""></asp:Label>
    <input id="lblQNumber" runat="server" type="text" readonly style="width: 40px" /></div>
<div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
    <asp:Label ID="lblCreateType" runat="server" Text=""></asp:Label></div>
<b>
    <asp:Label ID="lblNumberOfItems" runat="server" Text=""></asp:Label></b> (50
max.):
<asp:TextBox ID="txtQNumber" MaxLength="2" runat="server" CssClass="inputbox_sm"
    Width="22px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
        runat="server" ControlToValidate="txtQNumber" ErrorMessage="Please Enter Number of Questions"
        ValidationGroup="Form1"></asp:RequiredFieldValidator><br />
<br />
<asp:ImageButton ID="btnCreate" runat="server" ImageUrl="~/Images/btn_ct_create.gif"
    ValidationGroup="Form1" onmouseover="roll(this)" onmouseout="roll(this)" OnClick="btnCreate_Click" />
<br />
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQNumber"
    ErrorMessage="Number of questions should be between 1 and 50" MaximumValue="50"
    Type="Integer" MinimumValue="1" ValidationGroup="Form1" Display="Dynamic"></asp:RangeValidator>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ViewStateMode="Disabled" />
 <asp:HiddenField ID="hdnSystem" runat="server"/>