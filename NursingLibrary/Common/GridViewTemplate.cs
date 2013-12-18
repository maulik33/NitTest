using System.Web.UI;
using System.Web.UI.WebControls;

namespace NursingLibrary
{
    public class GridViewTemplate : System.Web.UI.Page, ITemplate
    {
        private ListItemType templateType;
        private string columnName;
        private int columntype;
        private string stringData;

        public GridViewTemplate(ListItemType type, string colname, int c_type)
        {
            templateType = type;
            columnName = colname;
            columntype = c_type;
        }

        public GridViewTemplate(ListItemType type, string colname, int c_type, string data)
        {
            templateType = type;
            columnName = colname;
            columntype = c_type;
            stringData = data;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            LinkButton lb1 = new LinkButton();
            lb1.ID = "lbE";

            Image img = new Image();

            img.ID = "im_correct";

            Label lb_header = new Label();

            switch (templateType)
            {
                case ListItemType.Header:
                    lb_header.Text = columnName;
                    container.Controls.Add(lb_header);
                    break;

                case ListItemType.Item:
                    if (columntype == 2)
                    {
                        container.Controls.Add(lb1);
                    }
                    else if (columntype == 3)
                    {
                        Label test = new Label();
                        test.Text = stringData;
                        container.Controls.Add(test);
                    }
                    else
                    {
                        container.Controls.Add(img);
                    }

                    break;
                case ListItemType.EditItem:
                    lb1.CommandArgument = "RemediationID";
                    lb1.CommandName = "Continue";
                    container.Controls.Add(lb1);
                    break;

                case ListItemType.Footer:
                    break;
            }
        }
    }
}