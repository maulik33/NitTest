using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

/// <summary>
/// Class for Handling "Select All", "Not Selected" etc items in a Web list control.
/// </summary>
public class KTPWebList : IKTPWebList
{
    private ListControl _control;

    public KTPWebList(ListControl control)
    {
        ShowNotSelected = true;
        ShowSelectAll = true;
        _control = control;
        NotSelectedItem = new ListItem(Constants.LIST_NOT_SELECTED_TEXT, Constants.LIST_NOT_SELECTED_VALUE);
        SelectAllItem = new ListItem(Constants.LIST_SELECT_ALL_TEXT, Constants.LIST_SELECT_ALL_VALUE);
        NotAssignedItem = new ListItem(Constants.LIST_NOT_ASSIGNED_TEXT, Constants.LIST_NOT_ASSIGNED_VALUE);
    }

    #region IKTPList Members

    public ListControl Control
    {
        get
        {
            return _control;
        }
    }

    public bool ShowSelectAll { get; set; }

    public bool ShowNotSelected { get; set; }

    public bool ShowNotAssigned { get; set; }

    public string NotSelectedText { get; set; }

    public ListItem NotSelectedItem { get; set; }

    public ListItem SelectAllItem { get; set; }

    public ListItem NotAssignedItem { get; set; }
    
    public void PopulateAdditionalItems()
    {
        if (ShowNotAssigned)
        {
            _control.Items.Insert(0, NotAssignedItem);
        }

        int countToCheck = ShowNotAssigned ? 1 : 0;
        if (ShowSelectAll && _control.Items.Count > countToCheck)
        {
            _control.Items.Insert(0, SelectAllItem);
        }

        if (ShowNotSelected)
        {
            if (!string.IsNullOrWhiteSpace(NotSelectedText))
            {
                NotSelectedItem.Text = NotSelectedText;
            }

            _control.Items.Insert(0, NotSelectedItem);
        }
    } 

    public string GetSelectedItemsText()
    {
        return string.Join(",", GetSelectedItems().Select(i => i.Text).ToArray());
    }

    public string GetSelectedItemsValue()
    {
        return string.Join("|", GetSelectedItems().Select(i => i.Value).ToArray());
    }

    public IEnumerable<ListItem> GetSelectedItems()
    {
        IEnumerable<ListItem> selectedItems = new List<ListItem>();

        ListItem notSelectedItem = _control.Items.FindByValue(Constants.LIST_NOT_SELECTED_VALUE);
        if (notSelectedItem != null && notSelectedItem.Selected)
        {
            return selectedItems;
        }

        ListItem selectAllItem = _control.Items.FindByValue(Constants.LIST_SELECT_ALL_VALUE);
        bool selectAll = selectAllItem != null && selectAllItem.Selected;

        selectedItems = _control.Items.OfType<ListItem>()
            .Where(i => (i.Selected || selectAll) && ControlHelper.IsInternalItem(i) == false);

        return selectedItems;
    }

    public void MarkSelectedValues(string selectedValuesText)
    {
        _control.ClearSelection();
        var matchedItems = from i in _control.Items.OfType<ListItem>()
                           join s in selectedValuesText.Split('|')
                           on i.Value equals s
                           select i;
        matchedItems.ToList().ForEach(p => p.Selected = true);
    }

    #endregion
}