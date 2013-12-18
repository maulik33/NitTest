using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    [System.ComponentModel.Designer(typeof(Messenger_Design))]
    public sealed class Messenger : WebControl
    {
        private Label Msg;
        private MessageItem _Message = new MessageItem();

        public Messenger()
            : base(HtmlTextWriterTag.Table)
        {
            EnableViewState = false;
        }

        public Messenger(string Message)
            : base(HtmlTextWriterTag.Table)
        {
            this.Message.Add(Message);
            EnableViewState = false;
        }

        public MessageItem Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        
        public void ShowMessage()
        {
            for (Int16 i = 0; i <= Message.Count - 1; i++)
            {
                Msg.Text += Message[i];
            }
        }

        public void ShowMessage(string aMessage)
        {
            Message.Add(aMessage);
            ShowMessage();
        }

        internal void ShowAuthorInf_Design()
        {
            var AuthorInf = new WebControl(HtmlTextWriterTag.Tr);
            var td = new WebControl(HtmlTextWriterTag.Td);
            var l = new Label
                        {
                            Text = "Messenger Version: Study 1.0<br>Author: Yu Heng Zhou<br>2005 July Canada",
                            ForeColor = System.Drawing.Color.RoyalBlue
                        };
            l.Font.Size = new FontUnit(FontSize.Small);
            AuthorInf.ID = string.Format("{0}AuthorInf", ClientID);
            AuthorInf.Controls.Add(td);
            td.Controls.Add(l);
            Controls.Add(AuthorInf);
        }

        internal void ShowInterface_Design()
        {
            var AuthorInf = new WebControl(HtmlTextWriterTag.Tr);
            var td = new WebControl(HtmlTextWriterTag.Td);
            var l = new Label
                        {
                            Text =
                                "<li>Message 1. To add a message item, please use \"Obj.Message.Add(msg as String)\"<li>Message 2. Use \"Obj.ShowMessage()\" to show messages.<li>Message 3",
                            ForeColor = System.Drawing.Color.Salmon
                        };

            AuthorInf.ID = string.Format("{0}AuthorInf", ClientID);
            AuthorInf.Controls.Add(td);
            td.Controls.Add(l);
            Controls.Add(AuthorInf);
        }

        protected override void OnInit(EventArgs e)
        {
            var tr = new WebControl(HtmlTextWriterTag.Tr);
            var td = new WebControl(HtmlTextWriterTag.Td);
            Msg = new Label
            {
                ForeColor = ForeColor.Name == "0" ? System.Drawing.Color.Red : ForeColor
            };
            Controls.Add(tr);
            tr.Controls.Add(td);
            td.Controls.Add(Msg);
        }
    }
}
