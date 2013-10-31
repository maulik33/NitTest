using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for WebExtensions
/// </summary>
public static class WebExtensions
{
    public static string TextOrDefault(this DropDownList control)
    {
        if (control.SelectedItem == null)
        {
            return string.Empty;
        }

        return control.SelectedItem.Text;
    }
}