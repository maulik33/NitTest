using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    [ToolboxData("<{0}:KTPDropDownList ID='KTPDropDownListId' runat=\"server\"> </{0}:KTPDropDownList>")]
    public class KTPDropDownList : DropDownList
    {
        private readonly IKTPWebList _webList;

        public KTPDropDownList()
        {
            _webList = ObjectFactory.GetKTPWebListObject(this);
            ShowSelectAll = false;
        }

        public string SelectedValuesText
        {
            get
            {
                return _webList.GetSelectedItemsValue();
            }
        }

        public string SelectedItemsText
        {
            get
            {
                return _webList.GetSelectedItemsText();
            }
        }

        public IEnumerable<ListItem> SelectedItems
        {
            get
            {
                return _webList.GetSelectedItems();
            }
        }

        public bool ShowSelectAll
        {
            get
            {
                return _webList.ShowSelectAll;
            }

            set
            {
                _webList.ShowSelectAll = value;
            }
        }

        public bool ShowNotSelected
        {
            get
            {
                return _webList.ShowNotSelected;
            }

            set
            {
                _webList.ShowNotSelected = value;
            }
        }

        public bool ShowNotAssigned
        {
            get
            {
                return _webList.ShowNotAssigned;
            }

            set
            {
                _webList.ShowNotAssigned = value;
            }
        }

        public string NotSelectedText
        {
            get
            {
                return _webList.NotSelectedText;
            }

            set
            {
                _webList.NotSelectedText = value;
            }
        }

        public void ClearData()
        {
            Items.Clear();

            _webList.PopulateAdditionalItems();
        }

        public void MarkSelectedItems(string selectedValuesText)
        {
            _webList.MarkSelectedValues(selectedValuesText);
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);

            _webList.PopulateAdditionalItems();
        }
    }
}