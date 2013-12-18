<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SkillModulePopUp.aspx.cs"
    Inherits="NursingRNWeb.STUDENT.SkillModulePopUp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/jquery-1.4.3.min.js"></script>
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <title>Skill Module</title>
    <base target="_self" />
    <style type="text/css">
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
        
        .modalWindow
        {
            background: #F9F9FB;
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
            padding: 0px 31px 5px 31px;
        }
        
        .SMDescBottom
        {
            font-family: "Arial" , "Helvetica" , "sans-serif";
            font-size: 12px;
            text-align: left;
            display: inline-block;
            margin-bottom: 18px;
            margin-top: 0px;
            clear: both;
            padding: 31px 0px 0px 31px;
        }
        
        .Right
        {
            float: right;
            margin-right: 31px;
        }
        
        .RightDisbaled
        {
            float: right;
            margin-right: 31px;
            cursor: default;
        }
        .Left
        {
            float: left;
            margin-left: 31px;
        }
        
        .Close
        {
            padding-left: 2px;
            width: 20px;
            height: 17px;
        }
        #WizardLogo
        {
            height: 49px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            ChangePanelSize();
            $("a.ui-dialog-titlebar-close", window.parent.document).click(function () {
                var player = $('#SMVideo > object#Playback');
                if (player.length > 0) {
                    player.remove();
                }
            });
        });

        function NotifyVideoEnd() {
            var SMUserVideoId = $('#hfSMUserVideoId').val();
            PageMethods.UpdateSkillModuleVideoStatus(SMUserVideoId);
            $("#btnNext").css("display", "block");
            $("#btnNextDisabled").css("display", "none");
        }

        function ChangePanelSize() {
            var smTitle = $("#hfSMTitle").val();
            if (smTitle != null && smTitle.length != 0) {
                window.parent.setPopupTitle(smTitle);
            }

            var screenHeight = screen.availHeight;
            var newHeight = (screenHeight * 0.75);
            var videoHeight = screen.availHeight;
            var newVideoHeight = (videoHeight * 0.60);
            ////        $("#modalDiv").css("height", newHeight);
            $("#videoSection").css("height", newVideoHeight);
        }

        function SetTitle() {
            var obj = window.parent.document.getElementById('popContent');
            if (obj != null)
                obj.setAttribute('title', 'kamal');
        }

        function ReadData() {
            var obj = window.dialogArguments;
            var inVal = 0;
            document.getElementById('hfTestId').value = obj;
        }

        function DoUnload() {
            var redirectValue = document.getElementById('redirectIntro');
            window.returnValue = redirectValue.value;
        }

        function WinClose(retval) {
            if (retval == "1") {
                window.top.location.href = "Intro.aspx";
            }
            else if (retval == "2") {
                window.parent.closeIframe();
            }
            else {
                window.top.location.href = "SkillsModule.aspx";
            }
        }
    </script>
</head>
<body style="background: #F9F9FB;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:Panel ID="pnlPopup" runat="server" Style="background: #F9F9FB;">
        <div id="modalDiv" style="background: #F9F9FB;">
            <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow" Style="overflow: Auto;">
                <div id="Content">
                    <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="InnerDiv" runat="server">
                                <div id="WizardLogo">
                                </div>
                                <div id="videoSection">
                                    <div id="DescriptionTop" runat="server" class="SMDesc" style="">
                                    </div>
                                    <div id="SMVideo" runat="server" style="padding-left: 31px; padding-right: 31px;">
                                    </div>
                                    <div id="DescriptionBottom" runat="server" class="SMDescBottom" style="">
                                    </div>
                                    <div id="hiddenField">
                                        <asp:HiddenField ID="hfOrderNumber" runat="server" />
                                        <asp:HiddenField ID="hfSMUserVideoId" runat="server" />
                                        <asp:HiddenField ID="hfTestId" runat="server" />
                                        <asp:HiddenField ID="redirectIntro" runat="server" Value="0" />
                                        <asp:HiddenField ID="hfSMTitle" runat="server" Value="" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHeight" runat="server" />
    <asp:HiddenField ID="hfWidth" runat="server" />
    <asp:HiddenField ID="popTitle" runat="server" />
    <footer>
             <div style="padding-left: 0px; padding-bottom: 64px; background: #F9F9FB;">
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../images/sm_previous.jpg"
                OnClick="BtnBackClick" AlternateText="Previous" CssClass="Left" style=";"/>
                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../images/sm_next.jpg" OnClick="BtnNextClick"
                AlternateText="Next" CssClass="Right" style=""/>
                <asp:ImageButton ID="btnNextDisabled" runat="server" ImageUrl="../images/sm_nextdisabled.jpg" Enabled="false"
                CssClass="RightDisbaled" style=""/>                               
            </div>  
    </footer>
    </form>
</body>
</html>
