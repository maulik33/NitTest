<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubCategories.ascx.cs" Inherits="SubCategories" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblClientNeeds" runat="server" Text="Client Needs"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddClientNeeds" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddClientNeeds_SelectedIndexChanged" NotSelectedText="Not Selected" ShowNotSelected="true">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblClientNeedsCategory" runat="server" Text="Client Needs Category"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddClientNeedsCategory" runat="server" NotSelectedText="Not Selected" ShowNotSelected="true">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left" style="height: 1px">
            <asp:Label ID="lblClinicalNursing" runat="server" Text="Nursing Process"  NotSelectedText="Not Selected" ShowNotSelected="true"></asp:Label>
        </td>
        <td align="left" style="height: 1px">
            <KTP:KTPDropDownList ID="ddNursingProcess" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblLevelOfDifficulty" runat="server" Text="Level Of Difficulty"  NotSelectedText="Not Selected" ShowNotSelected="true"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddLevelOfDifficulty" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblDemography" runat="server" Text="Demography"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddDemography" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblBloom" runat="server" Text="Cognitive Level"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddCognitiveLevel" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblScpecialityArea" runat="server" Text="Speciality Area"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddSpecialityArea" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblSystem" runat="server" Text="System"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddSystem" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblCriticalThinking" runat="server" Text="Critical Thinking"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddCriticalThinking" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblClinicalConcepts" runat="server" Text="Clinical Concepts"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddClinicalConcepts" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <asp:Panel ID="NonPNCategories" runat="server">
    <tr class="datatable2">
      <td align="left" style="Width:25%">
            <asp:Label ID="lblAccreditationCategories" runat="server" Text="Accreditation Categories"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddAccreditationCategories" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblQSENKSACompetencies" runat="server" Text="QSEN KSA Competencies"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddQSENKSACompetencies" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>
</asp:Panel>

    <tr class="datatable2">
        <td align="left">
            <asp:Label ID="lblConcepts" runat="server" Text="Concepts"></asp:Label>
        </td>
        <td align="left">
            <KTP:KTPDropDownList ID="ddConcepts" runat="server">
            </KTP:KTPDropDownList>
        </td>
    </tr>