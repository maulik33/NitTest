using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN.Controls
{
    public partial class InstitutionContacts : System.Web.UI.UserControl
    {
        public event EventHandler OnSaveInstitutionContactClick;

        public event EventHandler OnRefreshInstitutionContact;

        public event EventHandler OnDeleteInstitutionContact;

        public event EventHandler OnUpdateInstitutionContactClick;

        public int InstitutionId { get; set; }

        public void PopulateContacts(InstitutionContact institution, IEnumerable<InstitutionContact> institutionContacts)
        {
            if (institutionContacts.Count() > 0)
            {
                gvInstitutionContact.DataSource = institutionContacts;
                gvInstitutionContact.DataBind();
            }
            else
            {
                List<InstitutionContact> instConatacts = new List<InstitutionContact>();
                instConatacts.Add(new InstitutionContact());
                gvInstitutionContact.DataSource = instConatacts;
                gvInstitutionContact.DataBind();

                int TotalColumns = gvInstitutionContact.Rows[0].Cells.Count;
                gvInstitutionContact.Rows[0].Cells.Clear();
                gvInstitutionContact.Rows[0].Cells.Add(new TableCell());
                gvInstitutionContact.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvInstitutionContact.Rows[0].Cells[0].Text = "No Record Found";
            }
        }

        public void HideControls()
        {
            if (gvInstitutionContact.FooterRow != null)
            {
                TextBox txtNewEmail = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewEmail");
                TextBox txtNewPhoneNumber = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewPhoneNumber");
                TextBox txtNewName = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewName");
                DropDownList ddlNewContactType = (DropDownList)gvInstitutionContact.FooterRow.FindControl("ddlNewContactType");
                RequiredFieldValidator rfvNewName = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewName");
                RequiredFieldValidator rfvNewPhone = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewPhone");
                RequiredFieldValidator rfvNewEmail = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewEmail");

                txtNewEmail.Visible = false;
                txtNewPhoneNumber.Visible = false;
                txtNewName.Visible = false;
                ddlNewContactType.Visible = false;
                rfvNewName.Visible = false;
                rfvNewPhone.Visible = false;
                rfvNewEmail.Visible = false;
            }
        }

        public void ShowControls()
        {
            TextBox txtNewEmail = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewEmail");
            TextBox txtNewPhoneNumber = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewPhoneNumber");
            TextBox txtNewName = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewName");
            DropDownList ddlNewContactType = (DropDownList)gvInstitutionContact.FooterRow.FindControl("ddlNewContactType");
            LinkButton lnkAdd = (LinkButton)gvInstitutionContact.FooterRow.FindControl("lnkAdd");
            RequiredFieldValidator rfvNewName = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewName");
            RequiredFieldValidator rfvNewPhone = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewPhone");
            RequiredFieldValidator rfvNewEmail = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewEmail");

            lnkAdd.Text = "Save";
            txtNewEmail.Visible = true;
            txtNewPhoneNumber.Visible = true;
            txtNewName.Visible = true;
            ddlNewContactType.Visible = true;
            rfvNewName.Visible = true;
            rfvNewPhone.Visible = true;
            rfvNewEmail.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HideControls();
            }
        }

        protected void gvInstitutionContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            InstitutionContact institutionContact = new InstitutionContact();
            SaveInstitutionContactEventArgs saveInstitutionContactObj = new SaveInstitutionContactEventArgs();
            TextBox txtName = (TextBox)gvInstitutionContact.Rows[e.RowIndex].FindControl("txtName");
            TextBox txtPhoneNumber = (TextBox)gvInstitutionContact.Rows[e.RowIndex].FindControl("txtPhoneNumber");
            TextBox txtEmail = (TextBox)gvInstitutionContact.Rows[e.RowIndex].FindControl("txtEmail");
            DropDownList ddlContactType = (DropDownList)gvInstitutionContact.Rows[e.RowIndex].FindControl("ddlContactType");
            RequiredFieldValidator rfvName = (RequiredFieldValidator)gvInstitutionContact.Rows[e.RowIndex].FindControl("rfvName");

            Page.Validate(rfvName.ValidationGroup);
            if (Page.IsValid)
            {
                institutionContact.ContactId = e.Keys[0].ToInt();
                institutionContact.ContactType = ddlContactType.SelectedValue.ToInt();
                institutionContact.Status = 1;
                institutionContact.Name = txtName.Text;
                institutionContact.PhoneNumber = txtPhoneNumber.Text;
                institutionContact.ContactEmail = txtEmail.Text;
                saveInstitutionContactObj.InstitutionContactInfo = institutionContact;
                gvInstitutionContact.EditIndex = -1;
                if (this.OnUpdateInstitutionContactClick != null)
                {
                    OnUpdateInstitutionContactClick(sender, saveInstitutionContactObj);
                    HideControls();
                }
            }
        }

        protected void gvInstitutionContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                InstitutionContact institutionContact = new InstitutionContact();
                SaveInstitutionContactEventArgs saveInstitutionContactObj = new SaveInstitutionContactEventArgs();
                TextBox txtNewName = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewName");
                TextBox txtNewPhoneNumber = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewPhoneNumber");
                TextBox txtNewEmail = (TextBox)gvInstitutionContact.FooterRow.FindControl("txtNewEmail");
                RequiredFieldValidator rfvNewName = (RequiredFieldValidator)gvInstitutionContact.FooterRow.FindControl("rfvNewName");
                LinkButton lnkAdd = (LinkButton)gvInstitutionContact.FooterRow.FindControl("lnkAdd");

                Page.Validate(rfvNewName.ValidationGroup);
                if (Page.IsValid)
                {
                    ShowControls();
                    DropDownList ddlContactType = (DropDownList)gvInstitutionContact.FooterRow.FindControl("ddlNewContactType");
                    institutionContact.ContactType = ddlContactType.SelectedValue.ToInt();
                    institutionContact.Status = 1;
                    institutionContact.Name = txtNewName.Text;
                    institutionContact.PhoneNumber = txtNewPhoneNumber.Text;
                    institutionContact.ContactEmail = txtNewEmail.Text;
                    if (!string.IsNullOrEmpty(txtNewEmail.Text) && !string.IsNullOrEmpty(txtNewName.Text) && !string.IsNullOrEmpty(txtNewPhoneNumber.Text))
                    {
                        saveInstitutionContactObj.InstitutionContactInfo = institutionContact;

                        if (this.OnSaveInstitutionContactClick != null)
                        {
                            OnSaveInstitutionContactClick(sender, saveInstitutionContactObj);
                            HideControls();
                        }
                    }
                }

                if (gvInstitutionContact.Rows.Count == 1 && gvInstitutionContact.DataKeys[0].Value.ToInt() == 0)
                {
                    int TotalColumns = gvInstitutionContact.Rows[0].Cells.Count;
                    gvInstitutionContact.Rows[0].Cells.Clear();
                    gvInstitutionContact.Rows[0].Cells.Add(new TableCell());
                    gvInstitutionContact.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    gvInstitutionContact.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
        }

        protected void gvInstitutionContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList dropDownList = (DropDownList)e.Row.FindControl("ddlNewContactType");
                dropDownList.DataTextField = "Value";
                dropDownList.DataValueField = "Key";
                dropDownList.DataSource = UserTypeHelper.GetContactTypes();
                dropDownList.DataBind();
            }
        }

        protected void gvInstitutionContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvInstitutionContact.EditIndex = e.NewEditIndex;
            if (OnRefreshInstitutionContact != null)
            {
                OnRefreshInstitutionContact(sender, e);
                HideControls();
            }
        }

        protected void gvInstitutionContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            InstitutionContact institutionContact = new InstitutionContact();
            institutionContact.ContactId = e.Keys[0].ToInt();
            SaveInstitutionContactEventArgs saveInstitutionContactObj = new SaveInstitutionContactEventArgs();
            saveInstitutionContactObj.InstitutionContactInfo = institutionContact;
            if (OnDeleteInstitutionContact != null)
            {
                OnDeleteInstitutionContact(sender, saveInstitutionContactObj);
                HideControls();
            }
        }

        protected void gvInstitutionContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvInstitutionContact.EditIndex = -1;
            if (OnRefreshInstitutionContact != null)
            {
                OnRefreshInstitutionContact(sender, e);
                HideControls();
            }
        }
    }
}