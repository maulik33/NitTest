<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.master"
    Inherits="ADMIN_InstitutionView" CodeBehind="InstitutionView.aspx.cs" %>

<%@ Register Src="~/ADMIN/Controls/Address.ascx" TagName="Address" TagPrefix="ucAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>Add/View/Edit > Institution Details</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="3">
                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Institution successfully updated ">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                         <td align="left" width="25%">
                            Program of Study:
                        </td>
                        <td align="left" width="45%" class="datatable">
                            &nbsp;<asp:Label ID="lblProgOfStudy" runat="server" Text="" Width="180px"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            Institution Name:
                        </td>
                        <td align="left" width="45%" class="datatable">
                            &nbsp;<asp:Label ID="lblIName" runat="server" Text="Text here" Width="180px"></asp:Label>
                        </td>
                        <td rowspan="13" align="left" valign="middle">
                            <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click" Width="101px">Edit</asp:LinkButton><br />
                            <br />
                            <asp:LinkButton ID="lbNew" runat="server" OnClick="lbNew_Click" Width="128px">Add Additional Institution</asp:LinkButton>
                            <asp:DropDownList ID="ddTimeZone" runat="server" Visible="False">
                            </asp:DropDownList><tr class="datatable2">
                                <td align="left">
                                    Institution Description:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblIDescription" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Center Name:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblCenterName" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Contact Name:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblContacName" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Contact Phone Number:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblPhone" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Contact EMAIL:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblEmail" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Default Program:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblProgram" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Time Zone:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblTimeZone" runat="server" Text="Text here" Width="180px"></asp:Label>
                                </td>
                            </tr>
                            <!-- VW added this here for IP address -->
                            <tr class="datatable2">
                                <td align="left">
                                    IP Address:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblIP" runat="server" Text="Text here" Width="180px" Height="103px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Facility ID:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblFacility" runat="server" Width="178px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Contractual Start Date:
                                </td>
                                <td align="left" class="datatable">
                                    &nbsp;<asp:Label ID="lblContractualStartDate" runat="server" Width="178px"></asp:Label>
                                </td>
                            </tr>
                            <tr class="datatable2">
                                <td align="left">
                                    Annotation:
                                </td>
                                <td align="left" class="datatable">                                   
                                    &nbsp;<asp:Label ID="lblAnnotation" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                            <ucAddress:Address ID="ucAddress" runat="server" />
                             <tr class="datatable2">
                               <div id="divSecurity" runat="server">
                                <td align="left" valign="top">
                                     <asp:Label ID="lblSecurity" runat="server" Text="Security:"></asp:Label>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblSecurityStatus" runat="server" Text=""></asp:Label>
                                </td>
                               </div>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
