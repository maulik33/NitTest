using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;

namespace NursingRNWeb.STUDENT.ASCX
{
    public partial class FRBank : System.Web.UI.UserControl
    {
        private string _topics = string.Empty;

        public event EventHandler<FRBankEventArgs> OnCreateFRBankClick;

        public event EventHandler<FRBankEventArgs> OnCategoriesSelectionChange;

        public event EventHandler<FRBankEventArgs> OnTopicSelectionChange;

        public bool IsTopicClear { get; set; }

        public string FRTopics
        {
            get
            {
               if (Request.Form[lbxTopic.UniqueID] != null)
                {
                    _topics = Request.Form[lbxTopic.UniqueID];
                    return _topics.Replace(',', '|');
                }
                else
                {
                     _topics = lbxTopic.SelectedValuesText;
                }

                return _topics;
            }
        }

        public string FRCategories
        {
            get
            {
                return lbxCategories.SelectedValuesText;
            }
        }

        public bool IsCFRTest { get; set; }

        public void populateSystems(IEnumerable<Systems> systems)
        {
            ControlHelper.PopulateSystems(lbxCategories, systems);
        }

        public void SetHeader()
        {
            if (IsCFRTest == true)
            {
                lblHeader.Text = "Test Content";
                lblAvailableItems.Text = "Available Questions: ";
                lblCreateType.Text = "Create Test";
                lblNumberOfItems.Text = "Number of Questions";
            }
            else
            {
                lblHeader.Text = "Remediation Content";
                lblAvailableItems.Text = "Available Remediations: ";
                lblCreateType.Text = "Create Sequence";
                lblNumberOfItems.Text = "Number of Items";
            }
        }

        public void DisableTestCreation()
        {
            btnCreate.Visible = false;
        }

        public void SetTopicValues()
        {
            if (!IsTopicClear)
            {
                var splitval = FRTopics.Replace('|', ',').Split(',');
                for (int i = 0; i < splitval.Length; i++)
                {
                    var topicItems = lbxTopic.Items.FindByValue(splitval[i]);
                    if (topicItems != null)
                    {
                        lbxTopic.Items.FindByValue(splitval[i]).Selected = true;
                    }
                }
            }
        }

        internal void PopulateTopics(IEnumerable<Topic> topics)
        {
            ControlHelper.PopulateTopics(lbxTopic, topics);
        }

        internal void DisplayAvailableCount(int count)
        {
            lblQNumber.Value = count.ToString();
        }

        protected void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            if (ValidateFRBank())
            {
                string systemName = string.Empty;
                if (lbxCategories.SelectedItems.Count() == 1)
                {
                    systemName = lbxCategories.SelectedItem.ToString();
                }
                else
                {
                    systemName = "Multi-Category";
                }

                if (OnCreateFRBankClick != null)
                {
                    FRBankEventArgs args = new FRBankEventArgs()
                    {
                        Systems = lbxCategories.SelectedValuesText,
                        Topics = FRTopics,
                        QuestionCount = txtQNumber.Text,
                        SystemName = systemName,
                    };
                    OnCreateFRBankClick(sender, args);
                }
            }
        }

        protected void lbxSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnCategoriesSelectionChange != null)
            {
                lbxTopic.Items.Clear();
                FRBankEventArgs args = new FRBankEventArgs()
                                           {
                                               Systems = lbxCategories.SelectedValuesText,
                                               Topics =
                                                   hdnSystem.Value != lbxCategories.SelectedValuesText
                                                       ? string.Empty
                                                       : FRTopics,
                                           };
                hdnSystem.Value = args.Systems;
                if (args.Topics == string.Empty)
                {
                    IsTopicClear = true;
                }

                OnCategoriesSelectionChange(sender, args);
            }
        }

        protected void lbxTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnTopicSelectionChange != null)
            {
                FRBankEventArgs args = new FRBankEventArgs()
                {
                    Systems = lbxCategories.SelectedValuesText,
                    Topics = !IsTopicClear ? FRTopics : string.Empty,
            };
                OnTopicSelectionChange(sender, args);
            }
        }

        private bool ValidateFRBank()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(lbxCategories.SelectedValuesText))
            {
                lblError.Text = "Please select a category.";
                IsValid = false;
            }
            else if ((!string.IsNullOrEmpty(txtQNumber.Text.Trim())) &&
                   (txtQNumber.Text.Trim().ToInt() > lblQNumber.Value.ToInt()))
            {
                IsValid = false;
                if (IsCFRTest == false)
                {
                    lblError.Text = "Number of Items Should be less than or equal to number of remediations.";
                }
                else
                {
                    lblError.Text = "Number of questions Should be less than or equal to number of available questions.";
                }
            }

            return IsValid;
        }
    }
}