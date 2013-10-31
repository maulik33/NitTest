<%@ Page Language="C#" AutoEventWireup="true" Inherits="STUDENT.QBank" EnableViewState="false"
    CodeBehind="QBank.aspx.cs" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript">

        function EnableAll(NumOfCat) {


            // iterate through listitems that need to be enabled or disabled
            objItem = document.getElementById('chAll');
            var isChecked = objItem.checked;
            var SumQ = 0;

            var Mode = 0;
            objMode0 = document.getElementById('rblMode' + '_' + 0);
            if (objMode0 != null) { if (objMode0.checked) { Mode = 0; } }

            objMode1 = document.getElementById('rblMode' + '_' + 1);
            if (objMode1 != null) { if (objMode1.checked) { Mode = 1; } }

            objMode2 = document.getElementById('rblMode' + '_' + 2);
            if (objMode2 != null) { if (objMode2.checked) { Mode = 2; } }

            objMode3 = document.getElementById('rblMode' + '_' + 3);
            if (objMode3 != null) { if (objMode3.checked) { Mode = 3; } }


            for (i = 1; i <= NumOfCat; i++) {
                objItem = document.getElementById('ch_' + i);

                if (objItem != null) {
                    objItem.checked = isChecked;
                    for (j = 1; j <= 6; j++) {
                        objSubItem = document.getElementById('ch_' + i + '_' + j);
                        if (objSubItem != null) {
                            objSubItem.checked = isChecked;
                            if (isChecked) {


                                if (Mode == 0) {
                                    objNumSubQ = document.getElementById('txUnUsed' + i + '_' + j);
                                }
                                if (Mode == 1) {
                                    objNumSubQ = document.getElementById('txUnUsedIn' + i + '_' + j);
                                }

                                if (Mode == 3) {
                                    objNumSubQ = document.getElementById('txtAll' + i + '_' + j);
                                }

                                if (Mode == 2) {
                                    objNumSubQ = document.getElementById('txtIn' + i + '_' + j);
                                }



                                if (objNumSubQ != null) {
                                    SumQ = SumQ + parseInt(objNumSubQ.value);
                                }
                            }
                        }
                    }
                }
            }

            objAll = document.getElementById('chAll');
            objAll.checked = isChecked;

            objQAll = document.getElementById('lblQNumber');
            if (objQAll != null) {
                objQAll.value = SumQ;
            }


        }

        function CategoryChecked(NumOfCat) {
            objItem = document.getElementById('chAll');
            if (objItem != null && objItem.checked)
                return true;
            for (i = 1; i <= NumOfCat; i++) {
                objItem = document.getElementById('ch_' + i);
                if (objItem != null) {
                    if ((objItem.checked))
                        return true;
                    for (j = 1; j <= 6; j++) {
                        objSubItem = document.getElementById('ch_' + i + '_' + j);
                        if (objSubItem != null && objSubItem.checked) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        function Validate(NumOfCat) {
            // iterate through All the check boxes to check at least one of them is checked to create a test

            if (CategoryChecked(NumOfCat)) {
                Page_ClientValidate()
                return true;
            }
            else {
                alert('Please select test content');
                return false;
            }
        }

        function EnableCategory(NumOfCat) {


            // iterate through listitems that need to be enabled or disabled
            objItem = document.getElementById('ch_' + NumOfCat);
            var isChecked = objItem.checked;


            objItem.checked = isChecked;
            for (j = 1; j <= 6; j++) {
                objSubItem = document.getElementById('ch_' + NumOfCat + '_' + j);
                if (objSubItem != null) {
                    objSubItem.checked = isChecked;
                }
            }
            CheckChecked()
            CalculateNum()

        }

        function CalculateNum() {
            var SumQ = 0;

            var Mode = 0;
            objMode0 = document.getElementById('rblMode' + '_' + 0);
            if (objMode0 != null) { if (objMode0.checked) { Mode = 0; } }

            objMode1 = document.getElementById('rblMode' + '_' + 1);
            if (objMode1 != null) { if (objMode1.checked) { Mode = 1; } }

            objMode2 = document.getElementById('rblMode' + '_' + 2);
            if (objMode2 != null) { if (objMode2.checked) { Mode = 2; } }

            objMode3 = document.getElementById('rblMode' + '_' + 3);
            if (objMode3 != null) { if (objMode3.checked) { Mode = 3; } }

            for (i = 1; i <= 6; i++) {
                objNumMainItem = document.getElementById('ch_' + i);

                if (objNumMainItem != null) {

                    for (j = 1; j <= 6; j++) {
                        objNumSubItem = document.getElementById('ch_' + i + '_' + j);
                        if (objNumSubItem != null) {


                            if (objNumSubItem.checked) {

                                if (Mode == 0) {
                                    objNumSubQ = document.getElementById('txUnUsed' + i + '_' + j);
                                }
                                if (Mode == 1) {
                                    objNumSubQ = document.getElementById('txUnUsedIn' + i + '_' + j);
                                }

                                if (Mode == 3) {
                                    objNumSubQ = document.getElementById('txtAll' + i + '_' + j);
                                }

                                if (Mode == 2) {
                                    objNumSubQ = document.getElementById('txtIn' + i + '_' + j);
                                }

                                if (objNumSubQ != null) {
                                    SumQ = SumQ + parseInt(objNumSubQ.value);
                                }
                            }
                        }
                    }
                }
            }
            objQAll = document.getElementById('lblQNumber');
            if (objQAll != null) {
                objQAll.value = SumQ;
            }
        }
        function EnableSubCategory(NumOfCat, NumofSubCat) {



            CheckChecked()
            CalculateNum()

        }
        function CheckChecked() {

            var ButtonEnabled = false;
            var AllChecked = true;
            for (i = 1; i <= 6; i++) {
                var MainChecked = true;
                objManiCat = document.getElementById('ch_' + i);
                if (objManiCat != null) {

                    for (j = 1; j <= 6; j++) {
                        objSubCat = document.getElementById('ch_' + i + '_' + j);
                        if (objSubCat != null) {
                            MainChecked = MainChecked & objSubCat.checked

                            if (objSubCat.checked) {
                                ButtonEnabled = true;
                            }
                        }



                    }



                    objManiCat.checked = MainChecked;
                    AllChecked = AllChecked & MainChecked
                }

            }
            objAll = document.getElementById('chAll');
            objAll.checked = AllChecked;

            /*
            objButton= document.getElementById('btnCreate');
            if(objButton != null)
            {
            if(ButtonEnabled)
            {
            objButton.disabled=false;
            }
            else
			              
            {
            objButton.disabled=true;
            }
            }
            */


        }


    </script>
    <script src="../JS/google.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                            NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg; Prep> <%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %> Qbank > Create a New Test</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0"></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0"></a></div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear: both;">
                            <tr>
                                <td width="150" valign="top" bgcolor="#F6F6F9">
                                    <!-- left section -->
                                    <div id="med_left_banner1">
                                       <%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %> QBANK Navigation</div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Create"
                                            runat="server" OnClick="lb_Create_Click" CssClass="s8">Create Test</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_ListReview"
                                            runat="server" OnClick="lb_ListReview_Click" CssClass="s8">Previous Tests</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Analysis"
                                            runat="server" OnClick="lb_Analysis_Click" CssClass="s8">Cumulative Performance</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Sample"
                                            runat="server" OnClick="lb_Sample_Click" CssClass="s8">NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg; Prep Sample Tests</asp:LinkButton></div>
                                </td>
                                <td width="8">
                                    &nbsp;
                                </td>
                                <td valign="top" align="left" style="height: 521px">
                                    <div id="med_center_banner5" style="padding-left: 15px; margin-bottom: 15px;">
                                        Test Style</div>
                                    <div style="margin-left: 15px; display: table; clear: both;">
                                        <asp:RadioButtonList ID="rblStyle" runat="server" RepeatLayouty="Flow" Width="501px">
                                            <asp:ListItem Value="1" Selected="True">Timed Test - Imposes a time limit on the test.</asp:ListItem>
                                            <asp:ListItem Value="2">Tutor Mode - Makes question explanations available during the test. </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
                                        Question Reuse Mode</div>
                                    <div style="margin-left: 16px; display: table; clear: both;">
                                        <asp:RadioButtonList ID="rblMode" runat="server" RepeatDirection="Horizontal" Width="500px">
                                            <asp:ListItem Value="3" Selected="True">Unused Only</asp:ListItem>
                                            <asp:ListItem Value="1">Unused + Incorrect</asp:ListItem>
                                            <asp:ListItem Value="2">Incorrect Only</asp:ListItem>
                                            <asp:ListItem Value="4">All Items</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
                                        Test Content</div>
                                    <div style="margin-top: 5px;">
                                        <b>
                                            <input id="txtNumberCat" type="hidden" runat="server" />
                                            <input id="txtNumberSubCat" type="hidden" runat="server" />
                                        </b>
                                        <asp:CheckBox ID="chAll" runat="server" Text="Select all Test Content " />&nbsp;
                                        <br />
                                        <br />
                                    </div>
                                    <div runat="server" id="Categories">
                                    </div>
                                    <div style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
                                        Available Questions:
                                        <input id="lblQNumber" runat="server" type="text" readonly style="width: 40px" /></div>
                                    <div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
                                        Create Test</div>
                                    <b>Number of Questions</b><asp:label id="lblMaxQuestion" runat="server" />
                                    <asp:TextBox ID="txtQNumber" MaxLength="2" runat="server" CssClass="inputbox_sm"
                                        Width="22px">
                                    </asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtQNumber" ErrorMessage="Please Enter Number of Questions"
                                        ValidationGroup="Form1"></asp:RequiredFieldValidator><br />
                                    <br />
                                    <asp:ImageButton ID="btnCreate" runat="server" ImageUrl="~/Temp/images/btn_ct.gif"
                                        OnClick="btnCreate_Click" Enabled="False" ValidationGroup="Form1" onmouseover="roll(this)"
                                        onmouseout="roll(this)" />                                   
                                    <br />
                                     <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQNumber"                                        
                                        Type="Integer" MinimumValue="1" ValidationGroup="Form1" Display="Dynamic">
                                    </asp:RangeValidator> <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQNumber"
                                        ControlToCompare="lblQNumber" Operator="LessThanEqual" Type="Integer" ErrorMessage="Number of questions should be less than or equal to number of available questions."
                                        ValidationGroup="Form1" Display="Dynamic" />
                                    <div align="right" style="margin-right: 0px;">
                                        <a href="questions_intro2.asp"></a>
                                    </div>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </div>
                </div>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
