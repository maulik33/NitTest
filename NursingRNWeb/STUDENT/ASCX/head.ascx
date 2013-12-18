<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="true" Inherits="STUDENT.ASCX.StudentHeader" Codebehind="head.ascx.cs" %>

<div id="med_header" class="header">
    <asp:LinkButton ID="LbManageAcc" CssClass="s2" runat="server">MANAGE ACCOUNT</asp:LinkButton>&nbsp;
    <a href="ChangePassword.aspx" class="s2">CHANGE PASSWORD</a>
    <a href="help.html" class="s2" target="_blank">HELP</a>&nbsp;
    <asp:LinkButton ID="LbLogout" CssClass="s2" runat="server" OnClick="LbLogout_Click">LOG OUT</asp:LinkButton>&nbsp;&nbsp;
</div>
<div id="med_menu" class="menu">
    <div style="margin-left: 20px;">
        <asp:ImageButton ID="IbHome" ImageUrl="../../images/top_menu_homenew.jpg" runat="server" OnClick="IbHome_Click" />
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />
        <asp:ImageButton ID="IbIntegratedTest" ImageUrl="../../images/top_menu_itnew.jpg" runat="server" OnClick="IbIntegratedTest_Click" />
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />
        <asp:ImageButton ID="IbFocusedReview" ImageUrl="../../images/top_menu_frtnew.jpg" runat="server" OnClick="IbFocusedReview_Click" />
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />
        <asp:ImageButton ID="IbNclex" ImageUrl="../../images/top_menu_npnew.jpg" runat="server" OnClick="IbNclex_Click" />
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />
        <asp:ImageButton ID="IbSkillsModule" ImageUrl="../../Images/top_menu_smnew.jpg" runat="server" OnClick="IbSkillsModule_Click" />
        <asp:Panel ID="pnlCaseStudy" runat="server" CssClass="headerCaseStudy">
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />
        <asp:ImageButton ID="IbCaseStudies" ImageUrl="../../Images/top_menu_csnew.jpg" runat="server" OnClick="IbCaseStudies_Click" />
        </asp:Panel>
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" />        
        <asp:ImageButton ID="IbResults" ImageUrl="../../images/top_menu_trnew.jpg"  runat="server" OnClick="IbResults_Click" />
        <img alt="" src="../images/menu_sep.jpg" class="headerButtonSeperator" /> 
    </div>
</div>
