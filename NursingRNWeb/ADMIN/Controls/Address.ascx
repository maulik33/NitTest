<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Address.ascx.cs" Inherits="NursingRNWeb.ADMIN.Controls.Address" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Panel ID="Panel1" runat="server">
    <div id="editAddress" runat="server">
        <tr class="datatable2">
            <td align="left" valign="top">
                Address:
            </td>
            <td align="left">
                &nbsp;<asp:TextBox runat="server" ID="txtAddress1" MaxLength="50" Width="400px">
                </asp:TextBox>
                <br />
                &nbsp;<asp:TextBox runat="server" ID="txtAddress2" MaxLength="50" Width="400px">
                </asp:TextBox>
                <br />
                &nbsp;<asp:TextBox runat="server" ID="txtAddress3" MaxLength="50" Width="400px">
                </asp:TextBox>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                Country:
            </td>
            <td align="left">
                &nbsp;<KTP:KTPDropDownList ID="ddCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCountry_SelectedIndexChanged"
                    ShowNotSelected="false" ShowSelectAll="false" ShowNotAssigned="false">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <div id="stateDropDown" runat="server">
            <tr class="datatable2">
                <td align="left" width="20%">
                    State:
                </td>
                <td align="left">
                    &nbsp;<KTP:KTPDropDownList ID="ddState" runat="server">
                    </KTP:KTPDropDownList>
                </td>
            </tr>
        </div>
        <div id="stateTextBox" runat="server">
            <tr class="datatable2">
                <td align="left" width="20%">
                    State:
                </td>
                <td align="left">
                    &nbsp;<asp:TextBox runat="server" ID="txtState" MaxLength="100" Width="275px">
                    </asp:TextBox>
                </td>
            </tr>
        </div>
        <tr class="datatable2">
            <td align="left" width="20%">
                Zip:
            </td>
            <td align="left">
                &nbsp;<asp:TextBox runat="server" ID="txtZip" MaxLength="100" Width="120px">
                </asp:TextBox>
                <asp:HiddenField runat="server" ID="hdnAddressId"></asp:HiddenField>
            </td>
        </tr>
    </div>
    <div id="displayAddress" runat="server">
        <tr class="datatable2">
            <td align="left" valign="top">
                Address:
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblAddress1" Text=""></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" ID="lblAddress2" Text=""></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" ID="lblAddress3" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                State:
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblState" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                Country:
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblCountry" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                Zip:
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblZip" Text=""></asp:Label>
            </td>
        </tr>
    </div>
</asp:Panel>
