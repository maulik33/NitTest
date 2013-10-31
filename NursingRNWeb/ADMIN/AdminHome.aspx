<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="Admin_AdminHome" Codebehind="AdminHome.aspx.cs" %>

<%--*--%>
<%@ Register Src="~/ADMIN/Controls/AdminAccountMenu.ascx" TagName="AdminAccountMenu"
    TagPrefix="ucAdminAccountMenu" %>
<%@ Register Src="~/ADMIN/Controls/AdminContentManagementMenu.ascx" TagName="AdminContentManagementMenu"
    TagPrefix="ucAdminContentManagementMenu" %>
<%@ Register Src="~/ADMIN/Controls/AdminSendMailMenu.ascx" TagName="AdminSendMailMenu"
    TagPrefix="ucAdminSendMailMenu" %>
<%@ Register Src="~/ADMIN/Controls/AdminViewReportsMenu.ascx" TagName="AdminViewReportsMenu"
    TagPrefix="ucAdminViewReportsMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ucAdminViewReportsMenu:AdminViewReportsMenu ID="ucAdminViewReportsMenu" runat="server" />
<br />
    <ucAdminAccountMenu:AdminAccountMenu ID="ucAdminAccountMenu" runat="server" />
    <br />
    <div id="divAdminContentManagementMenu" runat="server" visible="false">
        <ucAdminContentManagementMenu:AdminContentManagementMenu ID="ucAdminContentManagementMenu"
            runat="server" />
    </div>
    <br />
</asp:Content>
