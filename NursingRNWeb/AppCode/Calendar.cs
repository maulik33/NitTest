using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    public enum Languages
    {
        English = 1,
        Chinese = 2,
        Japanese = 3
    }

    public enum CalendarFormats
    {
        YYYYMMDDhm = 0,
        MMDDYYYYhm = 1,
        DDMMYYYYhm = 2,
        YYYYMMDD = 3,
        MMDDYYYY = 4,
        DDMMYYYY = 5
    }

    [ToolboxData("<{0}:KTPCalendar ID='CalendarId' runat=\"server\"> </{0}:KTPCalendar>")]
    [DefaultProperty("Text"), DefaultEvent("ValueChanged")]
    public class Calendar : System.Web.UI.WebControls.WebControl
    {
        private TextBox T = new TextBox();
        private System.Web.UI.HtmlControls.HtmlImage B = new System.Web.UI.HtmlControls.HtmlImage();

        public Calendar()
            : base(HtmlTextWriterTag.Table)
        {
            PreRender += Calendar_PreRender;
            Init += Calendar_Init;
        }

        public delegate void ValueChangedEventHandler(object sender, string Value);

        public event ValueChangedEventHandler ValueChanged;

        [System.ComponentModel.Bindable(true), System.ComponentModel.Category("Íâ¹Û"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.DefaultValue("~/ControlSupport/Calendar/cal_16.gif"), System.ComponentModel.Description("Button Image Url."), System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        public string ButtonImg
        {
            get
            {
                object o = this.ViewState["ButtonImg"];
                if (o == null)
                {
                    return "~/ControlSupport/Calendar/cal_16.gif";
                }

                return o.ToString();
            }

            set
            {
                this.ViewState["ButtonImg"] = value;
            }
        }

        [DefaultValue(""), Localizable(true), Bindable(true, BindingDirection.TwoWay)]
        public string Text
        {
            get { return this.T.Text; }
            set { T.Text = value; }
        }

        [DefaultValue("~/ControlSupport/Calendar/")]
        public string SupportFolder
        {
            get
            {
                object o = this.ViewState["SupportFolder"];
                if (o == null)
                {
                    return "~/ControlSupport/Calendar/";
                }

                return o.ToString();
            }

            set
            {
                this.ViewState["SupportFolder"] = value;
            }
        }

        [DefaultValue("CalendarFormats.MMDDYYYYhm")]
        public CalendarFormats CalendarFormat
        {
            get
            {
                object o = this.ViewState["CalendarFormat"];
                int format;
                if (o == null || !int.TryParse(o.ToString(), out format))
                {
                    return CalendarFormats.MMDDYYYYhm;
                }

                return StringToEnum(typeof(CalendarFormats), o.ToString()) == null ? CalendarFormats.MMDDYYYYhm : (CalendarFormats)StringToEnum(typeof(CalendarFormats), o.ToString());
            }
            
            set
            {
                this.ViewState["CalendarFormat"] = Convert.ToInt32(value);
            }
        }

        public override System.Web.UI.WebControls.Unit Width
        {
            get
            {
                return base.Width;
            }
            
            set
            {
                if (value.Value < 150)
                {
                    value = new Unit(180);
                }

                base.Width = value;
                T.Width = new Unit(this.Width.Value - 25);
                B.Src = this.ButtonImg;
            }
        }

        public override System.Web.UI.WebControls.Unit Height
        {
            get
            {
                return base.Height;
            }
            
            set
            {
                if (value.Value < 5)
                {
                    value = 0;
                }

                base.Height = value;
                T.Width = new Unit(this.Width.Value - 25);
                B.Src = this.ButtonImg;
            }
        }

        [DefaultValue("Languages.English")]
        public Languages Language
        {
            get
            {
                object o = this.ViewState["Language"];
                int lang;
                if (o == null || !int.TryParse(o.ToString(), out lang))
                {
                    return Languages.English;
                }

                return (Languages)StringToEnum(typeof(Languages), o.ToString());
            }
            
            set
            {
                this.ViewState["Language"] = Convert.ToInt32(value);
            }
        }

        [DefaultValue("False")]
        public bool AutoPostBack
        {
            get
            {
                object o = this.ViewState["AutoPostBack"];
                if (o == null)
                {
                    return false;
                }

                return Convert.ToBoolean(o);
            }
            
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        protected void T_TextChanged(object sender, System.EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, this.T.Text);
            }
        }

        private void AddJavascript()
        {
            StringBuilder st = new StringBuilder();
            this.Page.ClientScript.RegisterClientScriptInclude("calendar", this.Page.ResolveUrl(this.SupportFolder) + "calendar.js");
            switch (this.Language)
            {
                case Languages.English:
                    this.Page.ClientScript.RegisterClientScriptInclude("calendar-en", this.Page.ResolveUrl(this.SupportFolder) + "lang/calendar-en.js");
                    break;
                case Languages.Chinese:
                    this.Page.ClientScript.RegisterClientScriptInclude("calendar-zh", this.Page.ResolveUrl(this.SupportFolder) + "lang/calendar-zh.js");
                    break;
                case Languages.Japanese:
                    this.Page.ClientScript.RegisterClientScriptInclude("calendar-zh", this.Page.ResolveUrl(this.SupportFolder) + "lang/calendar-jp.js");
                    break;
            }

            this.Page.ClientScript.RegisterClientScriptInclude("calendar-setup", this.Page.ResolveUrl(this.SupportFolder) + "calendar-setup.js");
            st.Append("<style type=\"text/css\"> @import url(\"" + this.Page.ResolveUrl(this.SupportFolder) + "calendar-win2k-cold-1.css\"); </style>");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Calendar_jsb", st.ToString());
            st.Append("<script type=\"text/javascript\">");

            st.Append("Calendar.setup({");
            st.Append("inputField    : \"" + this.T.ClientID + "\", ");
            switch (this.CalendarFormat)
            {
                case CalendarFormats.DDMMYYYYhm:
                    st.Append("ifFormat       :    \"%d/%m/%Y %H:%M\", ");
                    break;
                case CalendarFormats.MMDDYYYYhm:
                    st.Append("ifFormat       :    \"%m/%d/%Y %H:%M\", ");
                    break;
                case CalendarFormats.YYYYMMDDhm:
                    st.Append("ifFormat       :    \"%Y/%m/%d %H:%M\", ");
                    break;
                case CalendarFormats.DDMMYYYY:
                    st.Append("ifFormat       :    \"%d/%m/%Y\", ");
                    break;
                case CalendarFormats.MMDDYYYY:
                    st.Append("ifFormat       :    \"%m/%d/%Y\", ");
                    break;
                case CalendarFormats.YYYYMMDD:
                    st.Append("ifFormat       :    \"%Y/%m/%d\", ");
                    break;
            }

            st.Append("showsTime      :    true,");
            st.Append("timeFormat     :    \"12\",");
            st.Append("button        : \"" + this.B.ClientID + "\",");
            st.Append("electric      : true,");
            st.Append("align         : \"Tr\"");

            st.Append("});");
            st.Append("</script>");

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "_jss", st.ToString());
        }

        private void Calendar_Init(object sender, System.EventArgs e)
        {
            if (this.Width.Value < 150)
            {
                this.Width = new Unit(180);
            }

            T.Width = new Unit(this.Width.Value - 25);
            B.Src = this.ButtonImg;
            B.Alt = "Show Calendar";
            B.Style.Add("cursor", "pointer");
            System.Web.UI.HtmlControls.HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
            this.Controls.Add(r);
            System.Web.UI.HtmlControls.HtmlTableCell c = new System.Web.UI.HtmlControls.HtmlTableCell();
            r.Cells.Add(c);
            c.Controls.Add(T);
            c = new System.Web.UI.HtmlControls.HtmlTableCell();
            r.Cells.Add(c);
            c.Controls.Add(B);

            if (this.AutoPostBack)
            {
                T.AutoPostBack = true;
                T.TextChanged += T_TextChanged;
            }
        }

        private void Calendar_PreRender(object sender, System.EventArgs e)
        {
            if (this.Enabled)
            {
                AddJavascript();
                }
        }

        private object StringToEnum(Type t, string Value)
        {
            foreach (FieldInfo fi in t.GetFields())
            {
                if (fi.Name == Value)
                {
                    return fi.GetValue(null);
                }
            }

            return null;
        }
    }
}