<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.master"
    CodeBehind="QuestionCustomTset.aspx.cs" Inherits="NursingRNWeb.CMS.QuestionCustomTset"
    Title="Kaplan Nursing" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register src="Controls/SubCategories.ascx" tagname="SubCategories" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function ShowStem(str) {
            var temp = str.value;
            var qcont = temp.split("|");
            var steam = document.getElementById(qcont[1]);
            document.getElementById('stemd').innerHTML = steam.value;
        }
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr class="formtable">
            <td colspan="2" class="headfont">
                <b>Content Management - Edit Custom Test</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="Label6" runat="server"></asp:Label>
                <KTP:Messenger ID="Messenger1" runat="server"></KTP:Messenger>
            </td>
        </tr>
        <tr class="datatablelabels">
            <td colspan="2" align="left">
                Search Parameters
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>
            </td>
            <td align="left">
               <%-- <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddProgramofStudy_SelectedIndexChanged"  NotSelectedText="Selection Required" ShowNotSelected="true">
                </KTP:KTPDropDownList>--%>
                <asp:Label ID="lblProgramofStudyVal" runat="server" CssClass="programtype"></asp:Label>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" width="20%">
                <asp:Label ID="lblTestCategory" runat="server" Text="Test Category"></asp:Label>
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddTestType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTestType_SelectedIndexChanged">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left">
                <asp:Label ID="lblTest" runat="server" Text="Test"></asp:Label>
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddTest" runat="server" ShowNotSelected="true">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left">
                <asp:Label ID="lblTopicTitle" runat="server" Text="Topic Title"></asp:Label>
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddTopicTitle" runat="server">
                </KTP:KTPDropDownList>
            </td>
        </tr>
         <uc1:SubCategories ID="ucSubCategories" runat="server" />
     
        <tr class="datatable2">
            <td align="left">
                <asp:Label ID="lblQID" runat="server" Text="QuestionID"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtQuestionID" runat="server" class="inputbox_stu1"></asp:TextBox>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left">
                <asp:Label ID="lblT" runat="server" Text="Text" Width="62px"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtText" runat="server" class="inputbox_stu1"></asp:TextBox>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left">
                Question Type
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddQuestionType" runat="server">
                    <asp:ListItem Value="0">Not Selected</asp:ListItem>
                    <asp:ListItem Value="01">Multiple -choice,single-best-answer</asp:ListItem>
                    <asp:ListItem Value="02">Multiple-choice,multi-select</asp:ListItem>
                    <asp:ListItem Value="03">HotSpot</asp:ListItem>
                    <asp:ListItem Value="04">Numerical Fill-In</asp:ListItem>
                    <asp:ListItem Value="05">Order-Match (Drag &amp; Drop)</asp:ListItem>
                    <asp:ListItem Value="00">Item</asp:ListItem>
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" style="height: 23px">
                Item Type
            </td>
            <td align="left" style="height: 23px">
                <KTP:KTPDropDownList ID="ddTypeOfFile" runat="server">
                    <asp:ListItem Value="03">Question</asp:ListItem>
                    <asp:ListItem Value="00">Disclaimer</asp:ListItem>
                    <asp:ListItem Value="02">Tutorial Item</asp:ListItem>
                    <asp:ListItem Value="01">Intro</asp:ListItem>
                    <asp:ListItem Value="04">End Item</asp:ListItem>
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" style="height: 23px">
                Status
            </td>
            <td align="left" style="height: 23px">
                <KTP:KTPDropDownList ID="ddActive" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left" style="height: 23px">
                <asp:Label ID="Label1" runat="server" Text="Number of question to be include (Random return): "></asp:Label>
            </td>
            <td align="left" style="height: 23px">
                <KTP:KTPDropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem Selected="True" Value="265">265</asp:ListItem>
                </KTP:KTPDropDownList>
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search Question" />
            </td>
        </tr>
        <tr class="datatablelabels">
            <td colspan="2" align="left">
                Test Questions
            </td>
        </tr>
    </table>
    <table style="width: 280px">
        <tr>
            <td colspan="4" style="text-align: left">
                <asp:Label ID="Label7" runat="server" Text="Test Type: "></asp:Label>&nbsp;
                <asp:DropDownList ID="ddTestCategory" runat="server">
                </asp:DropDownList>
                &nbsp;
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="Test Name: "></asp:Label>
                <asp:TextBox ID="txtTestName" runat="server" Width="312px"></asp:TextBox>
                <asp:Button ID="btnCategory" OnClick="btnCategory_Click" runat="server" Text="Category"
                    Width="95px" />
                &nbsp;<asp:HiddenField runat="server" ID="hdnSecondsPerquestion" />
                <asp:HiddenField runat="server" ID="hdnDefaultGroup" />
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblQuestionsTobeIncluded" runat="server" Text="Label" Width="280px"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblQuestionIncluded" runat="server" Text="Label" Width="256px"></asp:Label>
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lbSelectedQuestions" runat="server" Height="400px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox>
            </td>
            <td style="width: 25px">
                <asp:Button ID="btnAddQuestions" OnClick="btnAddQuestions_Click" runat="server" Text="    >    "
                    ToolTip="Add Selected Question" />
                <asp:Button ID="btnAddAllQuestions" OnClick="btnAddAllQuestions_Click" runat="server"
                    Text="   >>   " ToolTip="Add All Question" /><br />
                <br />
                <asp:Button ID="btnRemoveQuestions" OnClick="btnRemoveQuestions_Click" runat="server"
                    Text="    <    " ToolTip="Remove Selected Question" />&nbsp;
                <asp:Button ID="btnRemoveAllQuestions" OnClick="btnRemoveAllQuestions_Click" runat="server"
                    Text="   <<   " ToolTip="Remove All Question" /><br />
                <br />
                <br />
                &nbsp;
                <asp:Button ID="btnDone" runat="server" Text=" Done " Width="60px" OnClick="btnDone_Click" /><br />
                <br />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
            <td style="width: 31px">
                &nbsp;
            </td>
            <td style="width: 82px">
                <asp:ListBox ID="lbQuestions" runat="server" Height="400px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox>
            </td>
            <td style="width: 82px">
                <asp:Button ID="btnMoveUp" runat="server" Text="Move Up" Width="100px" OnClick="btnMoveUp_Click" /><br />
                <br />
                <asp:Button ID="btnMoveDown" runat="server" Text="Move Down" Width="100px" OnClick="btnMoveDown_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="Label3" runat="server" Text="Question Stem: "></asp:Label>
                <div id="stemd" style="height: 200px; width: 100%; background-color: #eeeeee; text-align: left;
                    padding: 5px;">
                </div>
            </td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="Ph1" runat="server"></asp:PlaceHolder>
    <asp:HiddenField ID="hfProgramofStudyId" runat="server" />
</asp:Content>
