<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS_Controls_SearchQuestionCriteria"
    CodeBehind="SearchQuestionCriteria.ascx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="SubCategories.ascx" TagName="SubCategories" TagPrefix="uc1" %>
<table width="100%" cellspacing="0" cellpadding="3">
    <tr class="datatablelabels">
        <td colspan="2" align="left">
            Search Parameters
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <KTP:Messenger ID="ktpMessage" runat="server"></KTP:Messenger>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left" width="20%">
            <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged"
                NotSelectedText="Selection Required" ShowNotSelected="true">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left" width="20%">
            <asp:Label ID="lblTestCategory" runat="server" Text="Test Category"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddTestCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTestCategory_SelectedIndexChanged">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblTest" runat="server" Text="Test"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddTest" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblTopicTitle" runat="server" Text="Topic Title"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddTopicTitle" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <uc1:SubCategories ID="ucSubCategories" runat="server" />
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblQID" runat="server" Text="QuestionID"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtQuestionID" runat="server" class="inputbox_stu1"></asp:TextBox>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblT" runat="server" Text="Text" Width="62px"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtText" runat="server" class="inputbox_stu1"></asp:TextBox>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            Question Type
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddQuestionType" runat="server">
                <asp:ListItem Value="0">Not Selected</asp:ListItem>
                <asp:ListItem Value="01">Multiple -choice,single-best-answer</asp:ListItem>
                <asp:ListItem Value="02">Multiple-choice,multi-select</asp:ListItem>
                <asp:ListItem Value="03">HotSpot</asp:ListItem>
                <asp:ListItem Value="04">Numerical Fill-In</asp:ListItem>
                <asp:ListItem Value="05">Order-Match (Drag &amp; Drop)</asp:ListItem>
                <asp:ListItem Value="00">Item</asp:ListItem>
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left" style="height: 23px">
            Item Type
        </td>
        <td align="left" style="height: 23px">
            <KTP:KTPDropDownList ID="ddTypeOfFile" runat="server">
                <asp:ListItem Value="03">Question</asp:ListItem>
                <asp:ListItem Value="00">Disclaimer</asp:ListItem>
                <asp:ListItem Value="02">Tutorial Item</asp:ListItem>
                <asp:ListItem Value="01">Intro</asp:ListItem>
                <asp:ListItem Value="04">End Item</asp:ListItem>
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left" style="height: 23px">
            Status
        </td>
        <td align="left" style="height: 23px">
            <KTP:KTPDropDownList ID="ddActive" runat="server">
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="0">Inactive</asp:ListItem>
            </KTP:KTPDropDownList>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search Questions" OnClick="btnSearch_Click" />
        </td>
        <td>
            <asp:Button ID="btnRem" runat="server" OnClick="btnRem_Click" Text="Search Remediation"
                Width="156px" />
        </td>
        <td>
            &nbsp; &nbsp; &nbsp; &nbsp;
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Lippincott"
                Width="153px" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Question "
                Width="155px" />
        </td>
        <td>
            <asp:Button ID="btnAddR" runat="server" OnClick="btnAddR_Click" Text="Add Remediation"
                Width="153px" />
        </td>
        <td>
        </td>
        <td>
            <asp:Button ID="btnCategory" runat="server" Text="Assign Category" OnClick="btnCategory_Click" />
        </td>
    </tr>
</table>
