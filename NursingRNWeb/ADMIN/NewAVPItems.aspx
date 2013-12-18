<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_NewAVPItems1" Codebehind="NewAVPItems.aspx.cs" %>
<%@ Register TagPrefix="asp" Namespace="WebControls" Assembly="NursingRNWeb" %>

<%--<%@ Register Assembly="ACT360_WebControl" Namespace="ACT360_WebControl.ZYH" TagPrefix="cc1" %>--%>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3);
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblHeaderText" runat="server" Text="Label"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="3">
                            Enter details in fields below&nbsp;<br />
                            <asp:Label ID="errorMessage" runat="server" Visible="False" 
                                Text="" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>
                        </td>
                        <td align="left" class="datatable" colspan="2">
                            <asp:KTPDropDownList ID="ddlProgramOfStudy" runat="server"></asp:KTPDropDownList>
                            <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label1" runat="server" Text="AVP Item Name:"></asp:Label>
                        </td>
                        <td align="left" class="datatable" colspan="2">
                            <asp:TextBox ID="txtAVPName" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label3" runat="server" Text="AVP Item Link:"></asp:Label>
                        </td>
                        <td align="left" class="datatable" colspan="2">
                            <asp:TextBox ID="txtUrl" runat="server" Width="513px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" class="datatable" colspan="3">
                            <asp:Label ID="Label7" runat="server" Text="Links will appear in the NCLEX landing page. Clicking on a link will create a popup window with the dimensions specified below."></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label4" runat="server" Text="Popup Window Size: "></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="Label5" runat="server" Text="Width: "></asp:Label>
                            <asp:TextBox ID="txtWidth" runat="server" Width="82px"></asp:TextBox>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="Label6" runat="server" Text="Height"></asp:Label>
                            <asp:TextBox ID="txtPopHeight" runat="server" Width="82px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="3">
                            <asp:Button ID="btnSave" runat="server" Text="  Save " OnClick="btnSave_Click" />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
