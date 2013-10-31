using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    internal class Messenger_Design : System.Web.UI.Design.ControlDesigner
    {
        private Messenger _Instance;

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            _Instance = (Messenger)component;
            base.Initialize(component);
        }

        public override string GetDesignTimeHtml()
        {
            var sw = new System.IO.StringWriter();
            var hw = new HtmlTextWriter(sw);
            var _with1 = _Instance;
            _with1.ShowAuthorInf_Design();
            _with1.ShowInterface_Design();
            _with1.RenderControl(hw);
            _with1.Controls.Clear();
            return sw.ToString();
        }
    }
}