using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IKTPList
/// </summary>
public interface IKTPWebList
{
    ListControl Control { get; }

    bool ShowSelectAll { get; set; }

    bool ShowNotSelected { get; set; }

    bool ShowNotAssigned { get; set; }

    string NotSelectedText { get; set; }

    void PopulateAdditionalItems();

    string GetSelectedItemsValue();

    string GetSelectedItemsText();

    IEnumerable<ListItem> GetSelectedItems();

    void MarkSelectedValues(string selectedValuesText);
}