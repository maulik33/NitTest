<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstitutionContacts.ascx.cs"
    Inherits="NursingRNWeb.ADMIN.Controls.InstitutionContacts" %>
<div>
    <p style="text-align: left">
        <asp:Label ID="lblError" EnableViewState="false" runat="server" ForeColor="Red"></asp:Label>
    </p>
    <asp:GridView ID="gvInstitutionContact" runat="server" AutoGenerateColumns="False"
        ShowFooter="true" DataKeyNames="ContactId" PageSize="50" Width="98%" OnRowCommand="gvInstitutionContact_RowCommand"
        OnRowUpdating="gvInstitutionContact_RowUpdating" OnRowDataBound="gvInstitutionContact_RowDataBound"
        OnRowDeleting="gvInstitutionContact_RowDeleting" OnRowEditing="gvInstitutionContact_RowEditing"
        OnRowCancelingEdit="gvInstitutionContact_RowCancelingEdit">
        <RowStyle CssClass="datatable2a" />
        <EditRowStyle Width="850px" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:TemplateField HeaderText="Contact Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Width="100px"
                        MaxLength="100"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*Required Field" ValidationGroup="formupdate" Font-Size="Small" Display="Dynamic" ForeColor="Red"  Width="111px">
                            </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNewName" runat="server" Width="100px" MaxLength="100"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvNewName" runat="server" ControlToValidate="txtNewName"
                                ErrorMessage="*Required Field" ValidationGroup="formAdd" Font-Size="Small" Display="Dynamic" ForeColor="Red" Width="111px">
                            </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Admin Type">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="Value" DataValueField="Key"
                        DataSource='<%# Bind("ContactTypes") %>' SelectedValue='<%# Bind("ContactType") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlNewContactType" runat="server">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblContactType" runat="server" Text='<%# Bind("ContactTypeText") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone Number">
                <EditItemTemplate>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" Text='<%# Eval("PhoneNumber") %>'
                        Width="120px" MaxLength="50"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhoneNumber"
                                ErrorMessage="*Required Field" ValidationGroup="formupdate" Font-Size="Small" Display="Dynamic" ForeColor="Red"  Width="111px">
                            </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNewPhoneNumber" runat="server" Width="120px" MaxLength="50"></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvNewPhone" runat="server" ControlToValidate="txtNewPhoneNumber"
                                ErrorMessage="*Required Field" ValidationGroup="formAdd" Font-Size="Small" Display="Dynamic" ForeColor="Red"  Width="111px">
                            </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("ContactEmail") %>' Width="120px"
                        MaxLength="80"></asp:TextBox><br />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Wrong Email Format" Font-Size="Small" ValidationExpression="\w+([!@#$%^&*()-+.']\w+)*@\w+([!@#$%^&*()-+.']\w+)*\.\w+([!@#$%^&*()-+.']\w+)*"
                                Width="160px" Display="Dynamic" ForeColor="Red"  ValidationGroup="formupdate"></asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*Required Field" ValidationGroup="formupdate" Font-Size="Small" Display="Dynamic" ForeColor="Red"  Width="100px">
                            </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
              
                    <asp:TextBox ID="txtNewEmail" runat="server" Width="140px" MaxLength="80"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="revNewEmail" runat="server" ControlToValidate="txtNewEmail"
                                ErrorMessage="Wrong Email Format" Font-Size="Small" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([!@#$%^&*()-+.']\w+)*@\w+([!@#$%^&*()-+.']\w+)*\.\w+([!@#$%^&*()-+.']\w+)*"
                                Width="160px" ValidationGroup="formAdd"></asp:RegularExpressionValidator>
                                 <asp:RequiredFieldValidator ID="rfvNewEmail" runat="server" ControlToValidate="txtNewEmail"
                                ErrorMessage="*Required Field" ValidationGroup="formAdd" Font-Size="Small" Display="Dynamic" ForeColor="Red"  Width="100px">
                            </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("ContactEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="ID" Visible="False" />
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="False" CommandName="AddNew"
                        Text="Add New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
        </Columns>
    </asp:GridView>
</div>
