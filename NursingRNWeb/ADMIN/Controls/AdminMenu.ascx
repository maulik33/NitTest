<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminMenu" Codebehind="AdminMenu.ascx.cs" %>

<script type="text/javascript">
    

    function ExpandContextMenu(currentMenu) {
        $('#<%=menuOptions.ClientID %>').hide();
    }

</script>

<%--<asp:Panel ID="Div100" runat="server" CssClass="parta1">
    <img src="../images/ln-bullet-on.gif" alt="" />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/AdminHome.aspx" 
        Text="Home" Font-Underline="True" />
</asp:Panel>--%>
<div id="menuOptions" style="display:none" runat="server" >
        <asp:Panel ID="Div101" runat="server" CssClass="parta4">
    <asp:Panel ID="Addp" runat="server">
        <img src="../Temp/images/ln-bullet.gif" alt="" />Add
    </asp:Panel>
</asp:Panel>

        <asp:Panel ID="Div2" runat="server" CssClass="parta1">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/InstitutionEdit.aspx?actionType=1"
                Text="Institution" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div3" runat="server" CssClass="parta1">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/AdminEdit.aspx?actionType=1"
                Text="Admin" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div4" runat="server" CssClass="parta1">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/ProgramEdit.aspx?actionType=1"
                Text="Program" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div5" runat="server" CssClass="parta1">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Admin/CohortEdit.aspx?actionType=1"
                Text="Cohort" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div6" runat="server" CssClass="parta1">
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Admin/GroupEdit.aspx?actionType=1"
                Text="Groups" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div7" runat="server" CssClass="parta1">
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/CMS/NewCustomTest.aspx?TestID=-1&SearchCondition=&Sort="
                Text="Custom Tests" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div8" runat="server" CssClass="parta1">
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Admin/NewAVPItems.aspx?TestID=-1&Mode=1"
                Text="AVP Items" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div102" runat="server" CssClass="parta4">
            <img src="../images/ln-bullet.gif" alt="" />View/Edit
        </asp:Panel>

        <asp:Panel ID="Div9" runat="server" CssClass="parta1">
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Admin/InstitutionList.aspx" 
                Text="Institution" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div10" runat="server" CssClass="parta1">
            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Admin/AdminList.aspx" 
                Text="Admin" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div11" runat="server" CssClass="parta1">
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Admin/ProgramList.aspx" 
                Text="Program" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div12" runat="server" CssClass="parta1">
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Admin/CohortList.aspx" 
                Text="Cohort" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div13" runat="server" CssClass="parta1">
            <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Admin/GroupList.aspx" 
                Text="Groups" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div14" runat="server" CssClass="parta1">
            <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/Admin/UserList.aspx" 
                Text="Students" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div15" runat="server" CssClass="parta1">
            <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/Admin/UserListXML.aspx" 
                Text="Assign Students" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div16" runat="server" CssClass="parta1">
            <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/CMS/CustomTest.aspx?mode=4" 
                Text="Custom Tests" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div17" runat="server" CssClass="parta1">
            <asp:Image ID="Image17" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/Admin/AVPItems.aspx?CMS=1&Mode=1" 
                Text="AVP Items" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div21" runat="server" CssClass="parta1">
            <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="~/Admin/EmailReceiver1.aspx" 
                Text="Email" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div22" runat="server" CssClass="parta1">
            <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink22" runat="server" NavigateUrl="~/Admin/Norm.aspx" 
                Text="Norming" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div23" runat="server" CssClass="parta1">
            <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="~/Admin/Percentile.aspx" 
                Text="Probability" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div103" runat="server" CssClass="parta4">
            <asp:Panel ID="Rpt" runat="server">
                <img src="../images/ln-bullet.gif" alt="" />Reports
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="Div18" runat="server" CssClass="parta1">
            <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/Admin/ReportCohortByTest.aspx" 
                Text="View" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div19" runat="server" CssClass="parta1">
            <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/CMS/SearchQuestion.aspx?searchback=0"
                Text="CMS" Font-Underline="True" />
        </asp:Panel>

        <asp:Panel ID="Div20" runat="server" CssClass="parta1">
            <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/bull.gif" />
            <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/CMS/ReleaseChoice.aspx" 
                Text="Release" Font-Underline="True" />
        </asp:Panel>

        <%--<asp:Panel ID="Div104" runat="server" CssClass="spacer" />

        <asp:Panel ID="Div105" runat="server" align="center">
            <asp:ImageButton ID="BtnLogout" runat="server" ImageUrl="~/Images/logout.gif"
                OnClick="BtnLogout_Click" />
        </asp:Panel>--%>

</div>