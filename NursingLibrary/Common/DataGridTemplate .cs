using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DataGridTemplate_
/// </summary>
namespace NursingLibrary
{
    public class DataGridTemplate : ITemplate, INamingContainer
    {

        ListItemType templateType;
        string columnName;

        public DataGridTemplate(ListItemType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<B>" + columnName + "</B>";
                    container.Controls.Add(lc);
                    break;
                case ListItemType.Item:
                    lc.Text = "Item " + columnName;
                    container.Controls.Add(lc);
                    break;
                case ListItemType.EditItem:
                    TextBox tb = new TextBox();
                    tb.Text = "";
                    container.Controls.Add(tb);
                    break;
                case ListItemType.Footer:
                    lc.Text = "<I>" + columnName + "</I>";
                    container.Controls.Add(lc);
                    break;
            }
        }
    }




}