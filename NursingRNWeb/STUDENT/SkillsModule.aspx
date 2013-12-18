<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SkillsModule.aspx.cs" Inherits="NursingRNWeb.STUDENT.SkillsModule" %>

<%@ Register Src="ASCX/head.ascx" TagName="NursingHeader" TagPrefix="ucHead" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../JS/jquery-1.4.3.min.js"></script>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/main.js"></script>
    <script src="../JS/google.js" type="text/javascript"></script>
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <link href="../css/jquery-ui-1.css" type="text/css" rel="stylesheet" />
    <link href="../css/ui_002.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        #GridViewSkillsModule
        {
            border: 1px solid black;
        }
        
        #GridViewSkillsModule tr, td
        {
            border: 0px none;
        }
        #GridViewQuizResults > * > tr > td,
        #GridViewAvailableQuizzes > * > tr > td, 
        #GridViewSuspendedQuizzes > * > tr > td
        {
            border: 1px solid black;
        }
        
        #Title
        {
            color: #201069;
            font-family: "Arial" , "Arial" , "Helvetica" , "sans-serif";
            font-size: 14px;
            margin-bottom: 31px;
            margin-left: 1px;
            text-align: left;
            float: none;
            border-bottom: 1px solid black;
        }
        
        .style1
        {
            width: 882px;
        }
        
        .modalBackground
        {
            background-color: #666699;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalWindow
        {
            background: #f0f0fe;
            height: inherit;
            width: inherit;
        }
        
        .SMDesc
        {
            font-family: "Arial" , "Helvetica" , "sans-serif";
            font-size: 12px;
            text-align: left;
            margin-bottom: 18px;
            margin-top: 15px;
        }
        
        .Right
        {
            float: right;
            margin-right: 5px;
        }
        
        .RightDisbaled
        {
            float: right;
            margin-right: 5px;
            cursor: default;
        }
        .Left
        {
            float: left;
            margin-left: 3px;
        }
        
        .Close
        {
            padding-left: 2px;
            width: 20px;
            height: 17px;
        }
        .EmptyRowText
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        var globalTestId = null;
        function NotifyVideoEnd() {
            var SMUserVideoId = $('#hfSMUserVideoId').val();
            $("#btnNext").css("display", "block");
            $("#btnNextDisabled").css("display", "none");
        }

        function openPopUpWithSize(testId) {
            var frame = $get('popFrame');
            frame.src = "SkillModulePopUp.aspx?smId=" + testId;
            var NewHeight = screen.availHeight;
            var NewWidth = screen.availWidth;
            $('#hfHeight').val(NewHeight);
            $('#hfWidth').val(NewWidth);
            var winHeight = NewHeight * 0.79;
            var winWidth = NewWidth * 0.48;
            var retVal = $("#popContent").dialog({ modal: true, width: 719, height: winHeight, resizable: false, draggable: false, position: "245,20" });
        }

        function setPopupTitle(smScreenTitle) {
            $('#ui-dialog-title-popContent').html(smScreenTitle);
        }

        function initialize() {
            if (window.location != null && window.location.search.length > 1) {
                var urlParameters = window.location.search.substring(1);
                var parameterPair = urlParameters.split('&');
                var smTestId;
                $.each(parameterPair, function (idx, prm) {
                    var pos = parameterPair[idx].indexOf('=');
                    var argName = parameterPair[idx].substring(0, pos);
                    smTestId = parameterPair[idx].substring(pos + 1);
                });
                openPopUpWithSize(smTestId);
            }
        }
    </script>
</head>
<body onload="initialize();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table align="center" border="0" cellspacing="0" cellpadding="0" style="margin-top: 0px;">
        <tr>
            <td>
                <ucHead:NursingHeader ID="StudentHeader" runat="server" />
                <table id="med_main_f" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center" valign="top" class="style1">
                            <h2>
                                Essential Nursing Skills
                            </h2>
                            <div>
                                <div id="topbutton" style="margin-right: 12px;">
                                    <a href="javascript:history.back();">
                                        <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                            onmouseout="roll(this)" alt="" border="0" /></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                                src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                                onmouseout="roll(this)" border="0" alt="" /></a>
                                </div>
                                <div id="med_center_banner2_skillmodule">
                                    <img src="../images/icon_type3.gif" width="13" height="13" alt="" style="padding-left: 15px;" />
                                    Videos</div>
                                <div>
                                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView Style="clear: both;" ID="GridViewSkillsModule" runat="server" AutoGenerateColumns="False"
                                                Width="97%" DataKeyNames="TestId" CellPadding="3" BorderColor="#503792" ShowHeader="False"
                                                OnRowCommand="GridViewSkillsModule_RowCommand">
                                                <RowStyle CssClass="Gridrow2" />
                                                <AlternatingRowStyle CssClass="Gridrow1" />
                                                <Columns>
                                                    <asp:BoundField DataField="TestName" ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:ButtonField CommandName="Select" Text="Go" ControlStyle-CssClass="s2">
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="med_center_banner3_skillmodule" runat="server">
                                    <img src="../images/icon_type3.gif" width="13" height="13" alt="" style="padding-left: 15px;" />
                                    Repeat Available Quizzes
                                </div>
                                <asp:GridView Style="clear: both;" ID="GridViewAvailableQuizzes" runat="server" AutoGenerateColumns="False"
                                    Width="97%" DataKeyNames="TestId" EmptyDataText="No Available Quizzes" CellPadding="3"
                                    BorderColor="#503792" GridLine="Both" RowStyle-BorderColor="Black" OnRowDataBound="GridViewAvailableQuizzes_RowDataBound"
                                    OnRowCommand="GridViewAvailableQuizzes_RowCommand" EmptyDataRowStyle-HorizontalAlign="Left">
                                    <RowStyle CssClass="Gridrow2" />
                                    <HeaderStyle CssClass="Gridheader" />
                                    <AlternatingRowStyle CssClass="Gridrow1" />
                                    <Columns>
                                        <asp:BoundField DataField="TestName" HeaderText="Quiz Name" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TestStarted" HeaderText="Date & Time" ItemStyle-HorizontalAlign="left" />
                                        <asp:ButtonField Text="Take the Quiz" CommandName="TakeQuiz" ControlStyle-CssClass="s2" />
                                    </Columns>
                                </asp:GridView>
                                <div id="med_center_banner4_skillmodule" runat="server">
                                    <img src="../images/icon_type3.gif" width="13" height="13" alt="" style="padding-left: 15px;" />
                                    View Suspended Quizzes
                                </div>
                                <asp:GridView Style="clear: both;" ID="GridViewSuspendedQuizzes" runat="server" AutoGenerateColumns="False"
                                    Width="97%" DataKeyNames="TestId,UserTestId,SuspendType" EmptyDataText="No Available Quizzes"
                                    CellPadding="3" BorderColor="#503792" GridLine="Both" RowStyle-BorderColor="Black"
                                    OnRowCommand="GridViewSuspendedQuizzes_RowCommand" EmptyDataRowStyle-HorizontalAlign="Left">
                                    <RowStyle CssClass="Gridrow2" />
                                    <HeaderStyle CssClass="Gridheader" />
                                    <AlternatingRowStyle CssClass="Gridrow1" />
                                    <Columns>
                                        <asp:BoundField DataField="TestName" HeaderText="Quiz Name" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="TestStarted" HeaderText="Date & Time" ItemStyle-HorizontalAlign="left" />
                                        <asp:ButtonField Text="Resume" CommandName="Resume" ControlStyle-CssClass="s2" />
                                    </Columns>
                                </asp:GridView>
                                <div id="med_center_banner5_skillmodule" runat="server">
                                    <img src="../images/icon_type3.gif" width="13" height="13" alt="" style="padding-left: 15px;" />
                                    View Quiz Results
                                </div>
                                <asp:GridView Style="clear: both;" ID="GridViewQuizResults" runat="server" AutoGenerateColumns="False"
                                    Width="97%" DataKeyNames="TestId,UserTestId,SuspendType" EmptyDataText="No Available Quizzes"
                                    CellPadding="3" BorderColor="#503792" GridLine="Both" RowStyle-BorderColor="Black"
                                    OnRowCommand="GridViewQuizResults_RowCommand" EmptyDataRowStyle-HorizontalAlign="Left">
                                    <RowStyle CssClass="Gridrow2" />
                                    <HeaderStyle CssClass="Gridheader" />
                                    <AlternatingRowStyle CssClass="Gridrow1" />
                                    <Columns>
                                        <asp:BoundField DataField="TestName" HeaderText="Quiz Name" ItemStyle-HorizontalAlign="left">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestStarted" HeaderText="Date & Time" ItemStyle-HorizontalAlign="left">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:ButtonField Text="Review Results" CommandName="Review" ControlStyle-CssClass="s2">
                                            <ControlStyle CssClass="s2"></ControlStyle>
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" PopupControlID="pnlPopup"
        TargetControlID="btnHidden" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        Y="20" X="245" />
    <asp:Panel ID="pnlPopup" runat="server" Style="overflow: Auto; display: none">
        <div id="modalDiv" style="overflow: auto; overflow-x: hidden;">
            <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow" Style="overflow: Auto;
                display: none">
                <%--overflow-x: hidden;--%>
                <div id="Content">
                    <p style="margin: -3px 5px 0px 0px; padding-left: 31px; padding-right: 2px; padding-top: -1px;
                        float: right;">
                        <asp:ImageButton runat="server" ID="btnClose" ImageUrl="~/Images/Close.png" CssClass="Close" /></p>
                    <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="InnerDiv" runat="server">
                                <%--<input type="button" onclick="NotifyVideoEnd()" value="Completed watching Video" />--%>
                                <div>
                                    <h2 id="Title" style="padding-left: 31px; padding-right: 31px;">
                                        <asp:Label ID="lblSMTitle" runat="server"></asp:Label></h2>
                                </div>
                                <div id="videoSection">
                                    <div id="DescriptionTop" runat="server" class="SMDesc" style="padding-left: 31px;
                                        padding-right: 31px; padding-bottom: 5px;">
                                    </div>
                                    <div id="SMVideo" runat="server" style="padding-left: 31px; padding-right: 31px;">
                                    </div>
                                    <div id="DescriptionBottom" runat="server" class="SMDesc" style="padding-left: 31px;
                                        padding-right: 31px;">
                                    </div>
                                </div>
                                <div style="padding-top: 31px; padding-left: 25px; padding-right: 25px; padding-bottom: 31px;
                                    margin-left: 31px;">
                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../images/sm_previous.jpg"
                                        OnClick="BtnBackClick" AlternateText="Previous" CssClass="Left" Style="padding-top: 31px;
                                        padding-bottom: 31px;" />
                                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../images/sm_next.jpg" OnClick="BtnNextClick"
                                        AlternateText="Next" CssClass="Right" Style="padding-top: 31px; padding-bottom: 31px;" />
                                    <asp:ImageButton ID="btnNextDisabled" runat="server" ImageUrl="../images/sm_nextdisabled.jpg"
                                        Enabled="false" CssClass="RightDisbaled" Style="padding-top: 31px; padding-bottom: 31px;" />
                                </div>
                                <div id="hiddenField">
                                    <asp:HiddenField ID="hfOrderNumber" runat="server" />
                                    <asp:HiddenField ID="hfSMUserVideoId" runat="server" />
                                    <asp:HiddenField ID="hfTestId" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                        <br />
                        <asp:Button ID="btnHidden" runat="server" Text="Button" Style="display: none" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div id="popContent" title="" style="display: none">
            <iframe frameborder='0' width='100%' height='100%' id='popFrame' />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHeight" runat="server" />
    <asp:HiddenField ID="hfWidth" runat="server" />
    </form>
</body>
</html>
