Imports NursingLibrary

Partial Class CMS_QuestionCustomTset
    Inherits System.Web.UI.Page
    '    Private Property TestID() As Integer
    '        Get
    '            Dim o As Object = Me.ViewState("TestID")
    '            If o Is Nothing Then
    '                Return -1
    '            Else
    '                Return o
    '            End If
    '        End Get
    '        Set(ByVal value As Integer)
    '            Me.ViewState("TestID") = value
    '        End Set
    '    End Property
    '    Private Property SearchCondition() As String
    '        Get
    '            Dim o As Object = Me.ViewState("SearchCondition")
    '            If o Is Nothing Then
    '                SearchCondition = ""
    '            Else
    '                SearchCondition = o.ToString
    '            End If
    '        End Get
    '        Set(ByVal value As String)
    '            Me.ViewState("SearchCondition") = value
    '        End Set
    '    End Property
    '    Protected Property Sort() As String
    '        Get
    '            Dim o As Object = Me.ViewState("Sort")
    '            If o Is Nothing Then
    '                Return "TestID"
    '            Else
    '                Return o.ToString
    '            End If
    '        End Get
    '        Set(ByVal value As String)
    '            Me.ViewState("Sort") = value
    '        End Set
    '    End Property

    '    Private Sub Ini()
    '        Dim Cp As New cProduct
    '        Cp.PopulateProducts(ddTestCategory)
    '        ddTestCategory.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        Cp.PopulateProducts(ddTestType)
    '        ddTestType.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        '  Initialize ProductID from query
    '        If (Request.QueryString("ProductId") <> "") Then
    '            ddTestCategory.SelectedValue = Request.QueryString("ProductId")
    '        End If

    '        Dim Ct As New cTest
    '        Ct.PopulateTests(ddTest, Convert.ToInt32(ddTestType.SelectedValue))
    '        ddTest.Items.Insert(0, New ListItem("Not Selected", "0"))

    '        Dim cc As New cClientNeed()
    '        cc.PopulateClientNeeds(ddClientNeeds)
    '        ddClientNeeds.Items.Insert(0, New ListItem("Not Selected", "0"))

    '        Dim ccc As New cClientNeedsCategory()
    '        ccc.PopulateClientNeedCategory(ddClientNeedsCategory, Convert.ToInt32(ddClientNeeds.SelectedValue))
    '        ddClientNeedsCategory.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddClientNeedsCategory.SelectedIndex = 0

    '        Dim Cn As New cNursingProcess()
    '        Cn.PopulateNursingProcess(ddNursingProcess)
    '        ddNursingProcess.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddNursingProcess.SelectedIndex = 0

    '        Dim cl As New cLevelOfDifficulty()
    '        cl.PopulateLevelOfDifficulty(ddLevelOfDifficulty)
    '        ddLevelOfDifficulty.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddLevelOfDifficulty.SelectedIndex = 0


    '        Dim cd As New cDemographic()
    '        cd.PopulateDemography(ddDemography)
    '        ddDemography.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddDemography.SelectedIndex = 0


    '        Dim cco As New cCognitiveLevel()
    '        cco.PopulateCognitiveLevel(ddBloom)
    '        ddBloom.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddBloom.SelectedIndex = 0


    '        Dim cs As New cSpecialtyArea()
    '        cs.PopulateSpecialtyArea(ddScpecialitArea)
    '        ddScpecialitArea.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddScpecialitArea.SelectedIndex = 0

    '        Dim csy As New cSystem()
    '        csy.PopulateSystem(ddSystem)
    '        ddSystem.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddSystem.SelectedIndex = 0


    '        Dim ccr As New cCriticalThinking()
    '        ccr.PopulateCriticalThinking(ddCriticalThinking)
    '        ddCriticalThinking.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddCriticalThinking.SelectedIndex = 0


    '        Dim ccl As New cClinicalConcept()
    '        ccl.PopulateClinicalConcept(ddClinicalConcepts)
    '        ddClinicalConcepts.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddClinicalConcepts.SelectedIndex = 0

    '        Dim cr As New cRemediation()
    '        cr.PopulateDistinctTitles(ddTopicTitle)
    '        ddTopicTitle.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddTopicTitle.SelectedIndex = 0
    '        Dim a = IsDate("")

    '        Dim ds As Data.DataSet = DataLayer.GetCustomTest("TestID=" & Me.TestID)
    '        If (ds.Tables(0).Rows.Count <> 0) Then
    '            Me.TextBox1.Text = ds.Tables(0).Rows(0)("TestName").ToString
    '        End If


    '        Dim dv As Data.DataView = DataLayer.GetQuestionListInTest(Me.TestID)
    '        dv.Sort = "TypeOfFileID ASC,QuestionNumber1 ASC"

    '        ListBox2.Items.Clear()
    '        ' GridView1.DataSource = dv
    '        ' GridView1.DataBind()

    '        For Each r As Data.DataRowView In dv
    '            Dim H As New Web.UI.WebControls.HiddenField
    '            H.Value = r("stem").ToString
    '            'H.ID = r("QID")
    '            Me.Ph1.Controls.Add(H)

    '            Dim it As New ListItem(r("QuestionID"), r("QID") & "|" & H.ClientID)
    '            ListBox2.Items.Add(it)
    '        Next


    '        Me.Label4.Text = "Number of questions to be included: 0"
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count

    '        Me.Button1.Attributes.Add("onclick", "return confirm('Are you sure that you want to add all listed question into this test?')")
    '        Me.Button3.Attributes.Add("onclick", "return confirm('Are you sure that you want to remove all question from this test?')")
    '    End Sub

    '    Private Sub Ref()
    '        For Each i As ListItem In Me.ListBox1.Items
    '            Dim qid As Integer = i.Value.Split("|"c)(0)
    '            Dim r As Data.DataRow = DataLayer.GetQuestionByID(qid)
    '            Dim H As New Web.UI.WebControls.HiddenField
    '            H.Value = r("stem").ToString
    '            'H.ID = r("QID")
    '            Me.Ph1.Controls.Add(H)
    '            i.Value = r("QID") & "|" & H.ClientID
    '        Next
    '        For Each i As ListItem In Me.ListBox2.Items
    '            Dim qid As Integer = i.Value.Split("|"c)(0)
    '            Dim r As Data.DataRow = DataLayer.GetQuestionByID(qid)
    '            Dim H As New Web.UI.WebControls.HiddenField
    '            H.Value = r("stem").ToString
    '            'H.ID = r("QID")
    '            Me.Ph1.Controls.Add(H)
    '            i.Value = r("QID") & "|" & H.ClientID
    '        Next

    '    End Sub

    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '        If Not Me.IsPostBack Then
    '            Me.TestID = IIf(IsNumeric(Me.Request.QueryString("TestID")), Me.Request.QueryString("TestID"), -1)
    '            Me.SearchCondition = IIf(Me.Request.QueryString("SearchCondition") Is Nothing, "", Server.UrlDecode(Me.Request.QueryString("SearchCondition")))
    '            Me.Sort = IIf(Me.Request.QueryString("Sort") Is Nothing, "", Me.Request.QueryString("Sort"))
    '            If Me.TestID = -1 Then
    '                Me.Page.Response.Redirect("CustomTest.aspx?CMS=1")
    '            End If

    '            Ini()
    '        Else
    '            Ref()
    '        End If
    '        Me.ListBox1.Attributes.Add("onchange", "ShowStem(this);")
    '        Me.ListBox2.Attributes.Add("onchange", "ShowStem(this);")

    '    End Sub

    '    Protected Sub ddTestType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTestType.SelectedIndexChanged
    '        Dim ct As New cTest()
    '        ct.PopulateTests(ddTest, Convert.ToInt32(ddTestType.SelectedValue))
    '        ddTest.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddTest.SelectedIndex = 0

    '    End Sub

    '    Protected Sub ddClientNeeds_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddClientNeeds.SelectedIndexChanged
    '        Dim cc As New cClientNeedsCategory()
    '        cc.PopulateClientNeedCategory(ddClientNeedsCategory, Convert.ToInt32(ddClientNeeds.SelectedValue))
    '        ddClientNeedsCategory.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        ddClientNeedsCategory.SelectedIndex = 0

    '    End Sub

    '    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
    '        Dim obj As New cSearch()
    '        obj.Product = Convert.ToInt32(ddTestType.SelectedValue)
    '        obj.Test = Convert.ToInt32(ddTest.SelectedValue)
    '        obj.ClientNeed = Convert.ToInt32(ddClientNeeds.SelectedValue)
    '        obj.ClientNeedsCategory = Convert.ToInt32(ddClientNeedsCategory.SelectedValue)
    '        obj.ClinicalConcept = Convert.ToInt32(ddClinicalConcepts.SelectedValue)
    '        obj.CognitiveLevel = Convert.ToInt32(ddBloom.SelectedValue)
    '        obj.CriticalThinking = Convert.ToInt32(ddCriticalThinking.SelectedValue)
    '        obj.Demographic = Convert.ToInt32(ddDemography.SelectedValue)
    '        obj.LevelOfDifficulty = Convert.ToInt32(ddLevelOfDifficulty.SelectedValue)
    '        obj.NursingProcess = Convert.ToInt32(ddNursingProcess.SelectedValue)
    '        obj.Remediation = Convert.ToInt32(ddTopicTitle.SelectedValue)
    '        obj.SpecialtyArea = Convert.ToInt32(ddScpecialitArea.SelectedValue)
    '        obj.System = Convert.ToInt32(ddSystem.SelectedValue)
    '        obj.QuestionID = txtQuestionID.Text
    '        obj.Text = txtText.Text
    '        obj.ItemType = ddTypeOfFile.SelectedValue
    '        obj.Qtype = ddQuestionType.SelectedValue
    '        obj.Active = Convert.ToInt32(ddActive.SelectedValue)

    '        Dim dv As Data.DataView = obj.GetListOfQuestionsShowUnique(obj).DefaultView()
    '        '  Dim dv As Data.DataView = obj.GetListOfQuestionsForCustomTest(obj).DefaultView

    '        SV.Scramble(dv, Me.DropDownList1.SelectedValue)
    '        dv.Sort = "QuestionID"
    '        'Me.ListBox1.DataSource = dv
    '        'ListBox1.DataTextField = "QuestionID"
    '        'ListBox1.DataValueField = "QID"
    '        'ListBox1.DataBind()
    '        ListBox1.Items.Clear()
    '        For Each r As Data.DataRowView In dv
    '            Dim H As New Web.UI.WebControls.HiddenField
    '            H.Value = r("stem").ToString
    '            'H.ID = r("QID")
    '            Me.Ph1.Controls.Add(H)

    '            Dim it As New ListItem(r("QuestionID"), r("QID") & "|" & H.ClientID)
    '            ListBox1.Items.Add(it)
    '        Next
    '        Me.Label4.Text = "Number of questions to be included: " & Me.ListBox1.Items.Count
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count
    '    End Sub


    '    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    '        Dim dAr As New ArrayList
    '        For Each L As ListItem In Me.ListBox1.Items
    '            If Me.ListBox2.Items.Count >= 265 Then
    '                Me.Messenger1.Message.Add("This test has 265 questions already. Can not add more.")
    '                Exit For
    '            End If
    '            If L.Selected Then
    '                For Each T As ListItem In Me.ListBox2.Items
    '                    If L.Value.Split("|"c)(0) = T.Value.Split("|"c)(0) Then
    '                        Me.Messenger1.Message.Add("Question " & L.Text & " in this test already!")
    '                        'Me.ListBox1.Items.Remove(L)
    '                        'dAr.Add(L)
    '                        GoTo LL
    '                    End If
    '                Next
    '                Me.ListBox2.Items.Add(L)
    '                'Me.ListBox1.Items.Remove(L)
    '                L.Selected = False
    '                dAr.Add(L)
    '            End If
    'LL:
    '        Next
    '        For Each i As ListItem In dAr
    '            Me.ListBox1.Items.Remove(i)
    '        Next

    '        Me.Label4.Text = "Number of questions to be included: " & Me.ListBox1.Items.Count
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count
    '        If Me.Messenger1.Message.Count > 0 Then
    '            Me.Messenger1.ShowMessage()
    '        End If
    '    End Sub

    '    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '        Dim dAr As New ArrayList
    '        For Each L As ListItem In Me.ListBox1.Items
    '            If Me.ListBox2.Items.Count >= 265 Then
    '                Me.Messenger1.Message.Add("This test has 265 questions already. Can not add more.")
    '                Exit For
    '            End If
    '            For Each T As ListItem In Me.ListBox2.Items
    '                If L.Value.Split("|"c)(0) = T.Value.Split("|"c)(0) Then
    '                    Me.Messenger1.Message.Add("Question " & L.Text & " in this test already!")
    '                    'Me.ListBox1.Items.Remove(L)
    '                    'dAr.Add(L)
    '                    GoTo LL
    '                End If
    '            Next
    '            Me.ListBox2.Items.Add(L)
    '            'Me.ListBox1.Items.Remove(L)
    '            L.Selected = False
    '            dAr.Add(L)
    'LL:
    '        Next
    '        For Each i As ListItem In dAr
    '            Me.ListBox1.Items.Remove(i)
    '        Next

    '        Me.Label4.Text = "Number of questions to be included: " & Me.ListBox1.Items.Count
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count
    '        If Me.Messenger1.Message.Count > 0 Then
    '            Me.Messenger1.ShowMessage()
    '        End If
    '    End Sub

    '    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    '        Dim dAr As New ArrayList
    '        For Each L As ListItem In Me.ListBox2.Items
    '            If L.Selected Then
    '                Me.ListBox1.Items.Add(L)
    '                'Me.ListBox1.Items.Remove(L)
    '                'L.Selected = False
    '                dAr.Add(L)
    '            End If
    '        Next
    '        For Each i As ListItem In dAr
    '            Me.ListBox2.Items.Remove(i)
    '        Next

    '        Me.Label4.Text = "Number of questions to be included: " & Me.ListBox1.Items.Count
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count
    '        If Me.Messenger1.Message.Count > 0 Then
    '            Me.Messenger1.ShowMessage()
    '        End If
    '    End Sub

    '    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
    '        Dim dAr As New ArrayList
    '        For Each L As ListItem In Me.ListBox2.Items
    '            Me.ListBox1.Items.Add(L)
    '            'Me.ListBox1.Items.Remove(L)
    '            'L.Selected = False
    '            dAr.Add(L)
    '        Next
    '        For Each i As ListItem In dAr
    '            Me.ListBox2.Items.Remove(i)
    '        Next

    '        Me.Label4.Text = "Number of questions to be included: " & Me.ListBox1.Items.Count
    '        Me.Label5.Text = "Number of questions included: " & Me.ListBox2.Items.Count
    '        If Me.Messenger1.Message.Count > 0 Then
    '            Me.Messenger1.ShowMessage()
    '        End If

    '    End Sub

    '    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
    '        If Not Confirm() Then
    '            Exit Sub
    '        End If

    '        Dim V As Integer = DataLayer.EditCustomTest(Me.TextBox1.Text, Me.TestID, Me.ddTestCategory.SelectedValue, 0)
    '        DataLayer.UpdateQuestionListInTest(Me.TestID, Me.ListBox2)
    '        Me.Page.Response.Redirect("CustomTest.aspx?SearchCondition=" & Me.SearchCondition & "&CMS=1&Sort=" & Me.Sort & "&NewValue=" & IIf(InStr(Me.Sort, "TestName"), Server.UrlEncode(TextBox1.Text), Me.TestID))

    '    End Sub

    '    Private Function Confirm() As Boolean
    '        If Me.TextBox1.Text = "" Then
    '            Me.Messenger1.Message.Add("Test Name is required.")
    '        End If
    '        If DataLayer.IsCustomTestExisted(Me.TextBox1.Text, TestID, ddTestType.SelectedIndex) Then
    '            '           If DataLayer.IsCustomTestExisted(Me.TextBox1.Text, TestID, ddTestCategory.SelectedIndex) Then
    '            Me.Messenger1.Message.Add("TestName existed; please use another.")
    '        End If
    '        If Me.Messenger1.Message.Count > 0 Then
    '            Me.Messenger1.ShowMessage()
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End Function

    '    Protected Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
    '        Dim ds As Data.DataSet
    '        ds = DataLayer.GetCustomTest("TestID=" & Me.TestID)
    '        Dim v As String = ds.Tables(0).Rows(0)("TestName").ToString
    '        Me.Page.Response.Redirect("CustomTest.aspx?SearchCondition=" & Me.SearchCondition & "&CMS=1&Sort=" & Me.Sort & "&NewValue=" & IIf(InStr(Me.Sort, "TestName"), Server.UrlEncode(v), Me.TestID))

    '    End Sub

    '    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
    '        SV.CheckSession(Me, 1)
    '    End Sub

    '    Protected Sub Button8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button8.Click
    '        Dim t As Integer = DataLayer.GetTest("TestID=" & Me.TestID).Tables(0).Rows(0)("ProductID").ToString
    '        Me.Page.Response.Redirect("TestCategories.aspx?TestID=" & Me.TestID & "&Mode=4&CMS=1&TestType=" & t)
    '    End Sub

    '    Protected Sub Button10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button10.Click
    '        If Me.ListBox2.SelectedItem Is Nothing Then
    '            Me.Messenger1.ShowMessage("Please select questions in the test to move.")
    '            Exit Sub
    '        End If
    '        If Me.ListBox2.Items.IndexOf(Me.ListBox2.SelectedItem) = 0 Then
    '            Me.Messenger1.ShowMessage("Can not move.")
    '            Exit Sub
    '        End If
    '        For i As Integer = 0 To Me.ListBox2.Items.Count - 1
    '            If Me.ListBox2.Items(i).Selected Then
    '                Dim it As ListItem = Me.ListBox2.Items(i - 1)
    '                Me.ListBox2.Items.RemoveAt(i - 1)
    '                '      Do Until (Not Me.ListBox2.Items(i).Selected) Or (i = Me.ListBox2.Items.Count - 1)
    '                'i += 1
    '                'Loop
    '                Me.ListBox2.Items.Insert(i, it)
    '            End If
    '        Next

    '    End Sub

    '    Protected Sub Button9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button9.Click
    '        If Me.ListBox2.SelectedItem Is Nothing Then
    '            Me.Messenger1.ShowMessage("Please select questions in the test to move.")
    '            Exit Sub
    '        End If
    '        If Me.ListBox2.Items(Me.ListBox2.Items.Count - 1).Selected Then
    '            Me.Messenger1.ShowMessage("Can not move.")
    '            Exit Sub
    '        End If
    '        For i As Integer = Me.ListBox2.Items.Count - 1 To 0 Step -1
    '            If Me.ListBox2.Items(i).Selected Then
    '                Dim it As ListItem = Me.ListBox2.Items(i + 1)
    '                Me.ListBox2.Items.RemoveAt(i + 1)
    '                Do Until (Not Me.ListBox2.Items(i).Selected) Or (i = 0)
    '                    i -= 1
    '                Loop
    '                Me.ListBox2.Items.Insert(i, it)
    '            End If
    '        Next
    '    End Sub
End Class
