<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlternateEditQuestion.ascx.cs"
    Inherits="CMS_Controls_AlternateEditQuestion" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<table>
    <tr>
        <td>
            <asp:Label ID="lblAltStem" runat="server" Text="Alternate Text for Stem" Width="400px"
                BackColor="Silver" />
        </td>
    </tr>
    <tr>
        <td>
            <div id="D_AltStem" runat="server" title="Alternate Text for Stem">
            </div>
            <asp:TextBox ID="txtAltStem" runat="server" Rows="5" TextMode="MultiLine" Width="400px" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblAltAnswerChoices" runat="server" BackColor="Silver" Text="Alternate Text for Answer Choices"
                Width="400px" />
        </td>
    </tr>
    <tr>
        <td>
            <div id="D_AltAnswers" runat="server" title="Alternate Text for Answers">
            <br />
            </div>
        </td>
    </tr>
</table>
