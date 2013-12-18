using System;
using System.Collections.Generic;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using _address = NursingLibrary.Entity;

namespace NursingRNWeb.ADMIN.Controls
{
    public partial class Address : UserControl
    {
        private _address.Address _addressInfo;

        public event EventHandler<ItemSelectedEventArgs> OnCountrySelectionChange;

        public _address.Address GetAddressInformation()
        {
            var _stateName = string.Empty;
            var _stateId = 0;
            _addressInfo = new _address.Address();
            _addressInfo.AddressId = hdnAddressId.Value.ToInt();
            _addressInfo.Address1 = txtAddress1.Text;
            _addressInfo.Address2 = txtAddress2.Text;
            _addressInfo.Address3 = txtAddress3.Text;
            _addressInfo.AddressCountry = new Country { CountryId = ddCountry.SelectedValue.ToInt(), CountryName = ddCountry.SelectedItemsText };

            if (true == stateDropDown.Visible)
            {
                _stateName = ddState.SelectedItemsText;
                _stateId = ddState.SelectedValue.ToInt();
            }
            else
            {
                _stateName = txtState.Text;
                _stateId = 0;
            }

            _addressInfo.AddressState = new State
            {
                StateName = _stateName,
                StateId = _stateId
            };
            _addressInfo.Zip = txtZip.Text;
            _addressInfo.Status = 1;
            return _addressInfo;
        }

        public void SetAddressInformation(_address.Address _address, bool forView)
        {
            if (_address.AddressId > 0)
            {
                ddCountry.SelectedValue = _address.AddressCountry.CountryId.ToString();
            }

            if (false == forView)
            {
                displayAddress.Visible = false;
                editAddress.Visible = true;
                hdnAddressId.Value = _address.AddressId.ToString();
                txtAddress1.Text = _address.Address1;
                txtAddress2.Text = _address.Address2;
                txtAddress3.Text = _address.Address3;
                if (_address.AddressId > 0)
                {
                    if (KTPApp.CountriesWithStates.Contains(_address.AddressCountry.CountryId.ToString()))
                    {
                        if (!string.IsNullOrEmpty(_address.AddressState.StateName))
                        {
                            var _stateId = ddState.Items.FindByText(_address.AddressState.StateName.ToString()).Value;
                            ddState.SelectedValue = _stateId;
                        }

                        ShowStateAsTextBox(false);
                    }
                    else
                    {
                        txtState.Text = _address.AddressState.StateName;
                        ShowStateAsTextBox(true);
                    }
                }
                else
                {
                    ddCountry.SelectedValue = KTPApp.DefaultAddressCountry;
                    ShowStateAsTextBox(false);
                }

                txtZip.Text = _address.Zip;
            }
            else
            {
                if (_address.AddressId != 0)
                {
                    displayAddress.Visible = true;
                    editAddress.Visible = false;
                    lblState.Text = _address.AddressState.StateName;
                    lblCountry.Text = ddCountry.SelectedItemsText;
                    lblAddress1.Text = _address.Address1;
                    lblAddress2.Text = _address.Address2;
                    lblAddress3.Text = _address.Address3;
                    lblZip.Text = _address.Zip;
                }
                else
                {
                    displayAddress.Visible = true;
                    editAddress.Visible = false;
                }
            }
        }

        public void PopulateCountry(IEnumerable<Country> CountryList, bool isNewAddress)
        {
            ddCountry.DataSource = CountryList;
            ddCountry.DataTextField = "CountryName";
            ddCountry.DataValueField = "CountryId";
            ddCountry.DataBind();
            if (true == isNewAddress)
            {
                ddCountry.SelectedValue = KTPApp.DefaultAddressCountry;
                ShowStateAsTextBox(false);
                displayAddress.Visible = false;
                editAddress.Visible = true;
            }
        }

        public void PopulateState(IEnumerable<State> StateList)
        {
            ddState.DataSource = StateList;
            ddState.DataTextField = "StateName";
            ddState.DataValueField = "StateId";
            ddState.DataBind();
        }

        public void ShowStateAsTextBox(bool showTextBox)
        {
            if (showTextBox == true)
            {
                stateDropDown.Visible = false;
                stateTextBox.Visible = true;
            }
            else
            {
                stateDropDown.Visible = true;
                stateTextBox.Visible = false;
            }
        }

        public void DisableControls()
        {
            Panel1.Enabled = false;
        }

        protected void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnCountrySelectionChange != null)
            {
                ItemSelectedEventArgs args = new ItemSelectedEventArgs()
                {
                    SelectedText = ddCountry.SelectedItem.Text,
                    SelectedValue = ddCountry.SelectedItem.Value
                };
                OnCountrySelectionChange(sender, args);
            }
        }
    }
}