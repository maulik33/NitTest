using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using NursingLibrary;

public partial class ADMIN_Upload : System.Web.UI.Page
{
    static SqlConnection cn;
    static SqlDataAdapter adapterQuestion;
    static DataSet dsQuestion;
    static SqlDataAdapter adapterTestQuestion;
    static DataSet dsTestQuestion;
    static SqlDataAdapter adapterAnswerChoice;
    static DataSet dsAnswerChoice;
    private string uploadFileName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TestID"] == null || Request.QueryString["TestID"] == "")
        {
            lblTestID.Text = "";
            lblTestName.Text = "";
            //      lblTestType.Text = "";
            //        Label3.Visible = false;
            //        Label4.Visible = false;
            //        Label5.Visible = false;
        }
        else
        {
            lblTestName.Text = Request.QueryString["TestName"];
            lblTestID.Text = Request.QueryString["TestID"];
            //           lblTestType.Text = Request.QueryString["TestType"];
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        //Upload file to server D:\upload directory
        if (FileUpload1.HasFile)
            try
            {
              //  string serverpath = 
                FileUpload1.SaveAs("\\Upload\\" + FileUpload1.FileName);
                uploadFileName = FileUpload1.FileName;

                FileStream fs1 = null;
                FileStream fs2 = null;
                File.Copy("\\Upload\\xmlFileHead.xml", "..\\Upload\\xmlFile.xml", true);
                fs1 = File.Open("\\Upload\\xmlFile.xml", FileMode.Append);
                fs2 = File.Open("\\Upload\\" + uploadFileName, FileMode.Open);
                byte[] fs2Content = new byte[fs2.Length];
                fs2.Read(fs2Content, 0, (int)fs2.Length);
                fs1.Write(fs2Content, 0, (int)fs2.Length);
                fs1.Close();
                fs2.Close();
                if (updateXMLtoDatabase())
                    Label1.Text = "File name: " +
         FileUpload1.PostedFile.FileName + "<br>" +
         FileUpload1.PostedFile.ContentLength + " kb<br>" +
         "Content type: " + FileUpload1.PostedFile.ContentType + "<br> Has uploaded and databse was updated";
                else
                    Label1.Text = "Upload failed.";
                File.Delete("\\Upload\\xmlFile.xml");
                File.Delete("\\Upload\\" + uploadFileName);

            }
            catch (Exception ex)
            {
                Label1.Text = "ERROR: " + ex.Message.ToString();

            }
        else
        {
            Label1.Text = "You have not specified a file.";
        }
    }

    public bool updateXMLtoDatabase()
    {
        // Access uploaded file, update XML to databse
        try
        {
            // cn = new SqlConnection(ConfigurationManager.ConnectionStrings["NursingTestConnectionString"].ConnectionString);
            cn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cn.Open();


            // Create an adapter for questions
            adapterQuestion = new SqlDataAdapter(
                              "SELECT * FROM dbo.Questions WHERE  QID = IDENT_CURRENT('dbo.Questions')", cn);
            //                "SELECT QID, QuestionID, QuestionType, ClientNeedsID, ClientNeedsCategoryID, NursingProcessStimulus, Stem, Explanation FROM dbo.Questions WHERE  QID = IDENT_CURRENT('dbo.Questions')", cn);
            SqlCommandBuilder cbQuestion = new
                         SqlCommandBuilder(adapterQuestion);

            // Create and fill a dataset for questions
            dsQuestion = new DataSet();
            adapterQuestion.Fill(dsQuestion, "Questions");
            int lastQID = 0;
            if (dsQuestion.Tables["Questions"].Rows.Count != 0)
            {
                DataRow dr = dsQuestion.Tables["Questions"].Rows[0];
                lastQID = (int)dr["QID"];
            }

            //Create adapter for TestQuestions
            adapterTestQuestion = new SqlDataAdapter(
                 "SELECT * FROM dbo.TestQuestions ", cn);
            SqlCommandBuilder cbTestQuestion = new SqlCommandBuilder(adapterTestQuestion);
            dsTestQuestion = new DataSet();
            adapterTestQuestion.Fill(dsTestQuestion, "TestQuestion");

            //Create adapter for AnswerChoice
            adapterAnswerChoice = new SqlDataAdapter(
                           "SELECT QID, AIndex, Atext, Correct, AnswerConnectID, AType, initialPos FROM dbo.AnswerChoices", cn);
            SqlCommandBuilder cbAnswerChoice = new
                         SqlCommandBuilder(adapterAnswerChoice);

            // Create and fill a dataset for answerChoice
            dsAnswerChoice = new DataSet();
            adapterAnswerChoice.Fill(dsAnswerChoice, "AnswerChoices");

            // Get the input from the XML document
            //
            // Read in the XML document
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\Upload\\xmlFile.xml");


            // Collect target nodes from the XML document
            XmlNodeList gradItemsList;
            XmlElement root = doc.DocumentElement;
            gradItemsList = root.SelectNodes("/gradItems/question");

            int questionNumber = 0;

            // Process target nodes in XML node list
            // Send XML content and DataSet to loadDesigner method
            foreach (XmlNode d in gradItemsList)
            {
                // Send dataset and XML content to method for loading into dataset
                // loadReading(dsQuestion, questionID, questionStem);
                DataRow rQuestion = dsQuestion.Tables["Questions"].NewRow();
                DataRow rTestQuestion = dsTestQuestion.Tables["TestQuestion"].NewRow();
                DataRow rAnswer;

                // Fill the new row
                lastQID = lastQID + 1;
                rQuestion["QID"] = lastQID;
                rQuestion["QuestionID"] = d.Attributes["contentItemName"].Value;
                rQuestion["ClientNeedsID"] = "0";
                rQuestion["ClientNeedsCategoryID"] = "0";
                rQuestion["NursingProcessID"] = "0";
                rQuestion["LevelOfDifficultyID"] = "0";
                rQuestion["DemographicID"] = "0";
                rQuestion["CognitiveLevelID"] = "0";
                rQuestion["CriticalThinkingID"] = "0";
                rQuestion["IntegratedConceptsID"] = "0";
                rQuestion["ClinicalConceptsID"] = "0";
                rQuestion["RemediationID"] = "0";
                rQuestion["SpecialtyAreaID"] = "0";
                rQuestion["SystemID"] = "0";
                rQuestion["ReadingCategoryID"] = "0";
                rQuestion["ReadingID"] = "0";
                rQuestion["TypeOfFileID"] = "03";
                rQuestion["WritingCategoryID"] = "0";
                rQuestion["WritingID"] = "0";
                rQuestion["MathCategoryID"] = "0";
                rQuestion["Stem"] = "";
                rQuestion["Explanation"] = "";

                int i = 0;
                while (i < d.ChildNodes[1].ChildNodes.Count)
                {
                    if (d.ChildNodes[1].ChildNodes[i].Name == "para")
                    {
                        rQuestion["Stem"] += "<P>" + d.ChildNodes[1].ChildNodes[i].InnerText + "</P>";
                    }
                    if (d.ChildNodes[1].ChildNodes[i].Name == "figure")
                    {
                        if (d.ChildNodes[1].ChildNodes[i].HasChildNodes)
                        {
                            rQuestion["Stem"] += "<P><Picture=" + d.ChildNodes[1].ChildNodes[i].ChildNodes[0].Attributes["graphic-ref"].Value + "/></P>";
                        }
                    }
                    i = i + 1;
                }
                string a = d.ChildNodes[2].Name;
                if (d.ChildNodes[2].Name == "answer-choice-set")
                {
                    i = 0;
                    int choiceType = 0;
                    while (i < d.ChildNodes[2].ChildNodes.Count)
                    {
                        rAnswer = dsAnswerChoice.Tables["AnswerChoices"].NewRow();
                        rAnswer["AType"] = 1;
                        rAnswer["QID"] = lastQID;
                        rAnswer["AIndex"] = Convert.ToChar(65 + i);
                        rAnswer["AText"] = d.ChildNodes[2].ChildNodes[i].InnerText;
                        if (d.ChildNodes[2].ChildNodes[i].Attributes.Count != 0)
                        {
                            rAnswer["Correct"] = 1;
                            ++choiceType;
                        }
                        else
                            rAnswer["Correct"] = 0;
                        rAnswer["AnswerConnectID"] = 0;
                        i = i + 1;
                        dsAnswerChoice.Tables["AnswerChoices"].Rows.Add(rAnswer);
                    }
                    if (choiceType == 1)
                    {
                        rQuestion["QuestionType"] = "01";
                    }
                    if (choiceType > 1)
                    {
                        rQuestion["QuestionType"] = "02";
                    }
                }
                if (d.ChildNodes[2].Name == "hotspot-answer-input")
                {
                    rQuestion["Stimulus"] = d.ChildNodes[2].Attributes["imageFile"].Value;
                    rAnswer = dsAnswerChoice.Tables["AnswerChoices"].NewRow();
                    rAnswer["AType"] = 1;
                    rAnswer["QID"] = lastQID;
                    rAnswer["AIndex"] = "A";
                    rAnswer["AText"] = d.ChildNodes[2].InnerText;
                    rAnswer["Correct"] = 1;
                    rQuestion["QuestionType"] = "03";

                    rAnswer["AnswerConnectID"] = 0;
                    dsAnswerChoice.Tables["AnswerChoices"].Rows.Add(rAnswer);
                }

                if (d.ChildNodes[2].Name == "answerBlank")
                {
                    rAnswer = dsAnswerChoice.Tables["AnswerChoices"].NewRow();
                    rAnswer["AType"] = 1;
                    rAnswer["QID"] = lastQID;
                    rAnswer["AIndex"] = "A";
                    rAnswer["AText"] = d.ChildNodes[2].Attributes["correctValue"].Value;
                    rAnswer["Correct"] = 1;
                    rQuestion["QuestionType"] = "04";

                    rAnswer["AnswerConnectID"] = 0;
                    dsAnswerChoice.Tables["AnswerChoices"].Rows.Add(rAnswer);
                }

                if (d.ChildNodes[2].Name == "orderingAnswerSet")
                {
                    i = 0;
                    while (i < d.ChildNodes[2].ChildNodes.Count)
                    {
                        rAnswer = dsAnswerChoice.Tables["AnswerChoices"].NewRow();
                        rAnswer["AType"] = 1;
                        rAnswer["QID"] = lastQID;
                        rAnswer["AIndex"] = Convert.ToChar(65 + i);
                        rAnswer["AText"] = d.ChildNodes[2].ChildNodes[i].InnerText;
                        rAnswer["Correct"] = 0;
                        rQuestion["QuestionType"] = "05";

                        rAnswer["AnswerConnectID"] = 0;
                        rAnswer["initialPos"] = d.ChildNodes[2].ChildNodes[i].Attributes["initialPos"].Value;
                        i = i + 1;
                        dsAnswerChoice.Tables["AnswerChoices"].Rows.Add(rAnswer);
                    }
                }


                if (d.ChildNodes[3].Name == "explanation")
                {
                    i = 0;
                    while (i < d.ChildNodes[3].ChildNodes.Count)
                    {
                        if (d.ChildNodes[3].ChildNodes[i].HasChildNodes)
                        {
                            if (d.ChildNodes[3].ChildNodes[i].ChildNodes[0].Name == "graphic")
                            {
                                rQuestion["Explanation"] += "<P><Picture=" + d.ChildNodes[3].ChildNodes[i].ChildNodes[0].Attributes["graphic-ref"].Value + "/></P>";
                            }
                            else if (d.ChildNodes[3].ChildNodes[i].ChildNodes[0].Name == "emphasis" && d.ChildNodes[3].ChildNodes[i].ChildNodes[0].Attributes["emphasis-type"].Value.ToString() == "bold")
                            {
                                rQuestion["Explanation"] += "<P><b>" + d.ChildNodes[3].ChildNodes[i].ChildNodes[0].InnerText + "</b> " + d.ChildNodes[3].ChildNodes[i].InnerText + "</P>";
                            }
                            else
                            {
                                rQuestion["Explanation"] += "<P>" + d.ChildNodes[3].ChildNodes[i].InnerText + "</P>";
                            }
                        }
                        i = i + 1;
                    }
                }

                // Add the new row to the Designers table
                dsQuestion.Tables["Questions"].Rows.Add(rQuestion);
                if (lblTestID.Text != "")
                {
                    rTestQuestion["TestID"] = Convert.ToInt32(lblTestID.Text);
                    rTestQuestion["QuestionID"] = d.Attributes["contentItemName"].Value;
                    rTestQuestion["QID"] = lastQID;
                    rTestQuestion["QuestionNumber"] = ++questionNumber;
                    dsTestQuestion.Tables["TestQuestion"].Rows.Add(rTestQuestion);
                }

            }

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex);
            return false;
        }

        // Update the database table 
        adapterQuestion.Update(dsQuestion, "Questions");
        adapterAnswerChoice.Update(dsAnswerChoice, "AnswerChoices");
        if (lblTestID.Text != "")
        {
            adapterTestQuestion.Update(dsTestQuestion, "TestQuestion");
        }

        cn.Close();
        return true;
    }
}