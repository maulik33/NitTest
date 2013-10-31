using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Utilities;

namespace WebControls
{
    public class LippincottCard : WebControl
    {
        private string remediationHtml;
        private string[] LippincottTitle1;
        private string[] LippincottExplanation1;
        private string[] LippincottTitle2;
        private string[] LippincottExplanation2;
        private string _rowIdPrefix;
        private string _toggleButtonIdPrefix;
        private bool isFRRemediation;

        public LippincottCard(string[] _LippincottTitle1, string[] _LippincottExplanation1, string[] _LippincottTitle2, string[] _LippincottExplanation2, string _remediationHtml, bool _isFrRemediation = false)
            : base(System.Web.UI.HtmlTextWriterTag.Table)
        {
            Load += LippincottCard_Load;
            if (_LippincottTitle1.Length != _LippincottExplanation1.Length ||
                _LippincottTitle1.Length != _LippincottExplanation2.Length ||
                _LippincottTitle2.Length != _LippincottExplanation1.Length)
            {
                throw new Exception("Lippincott Title's Length is not as same as Lippincott Explanation's Length.");
            }

            remediationHtml = _remediationHtml;
            LippincottTitle1 = _LippincottTitle1;
            LippincottExplanation1 = _LippincottExplanation1;
            LippincottTitle2 = _LippincottTitle2;
            LippincottExplanation2 = _LippincottExplanation2;
            isFRRemediation = _isFrRemediation;
        }

        [System.ComponentModel.Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgRemediationUrl
        {
            get
            {
                object o = ViewState["ImgRemediationUrl"];
                if (o == null)
                {
                    return string.Empty;
                }

                return o.ToString();
            }

            set
            {
                ViewState["ImgRemediationUrl"] = value;
            }
        }

        [System.ComponentModel.Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgLippincottUrl
        {
            get
            {
                object o = ViewState["ImgLippincottUrl"];
                if (o == null)
                {
                    return string.Empty;
                }

                return o.ToString();
            }

            set
            {
                ViewState["ImgLippincottUrl"] = value;
            }
        }

        [System.ComponentModel.Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgLippincott2Url
        {
            get
            {
                object o = ViewState["ImgLippincott2Url"];
                if (o == null)
                {
                    return string.Empty;
                }

                return o.ToString();
            }

            set
            {
                ViewState["ImgLippincott2Url"] = value;
            }
        }

        [System.ComponentModel.Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgPlus
        {
            get
            {
                object o = ViewState["ImgPlus"];
                return o == null ? "~/images/plu.jpg" : o.ToString();
            }

            set
            {
                ViewState["ImgPlus"] = value;
            }
        }

        [System.ComponentModel.Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgMinus
        {
            get
            {
                object o = ViewState["ImgMinus"];
                if (o == null)
                {
                    return "~/images/min.jpg";
                }

                return o.ToString();
            }

            set
            {
                ViewState["ImgMinus"] = value;
            }
        }

        private void LippincottCard_Load(object sender, EventArgs e)
        {
            var Count = 0;
            bool LippinCottCollapsed = true;
            _toggleButtonIdPrefix = Utilities.EscapeIds(ClientID, "btnToggle");
            _rowIdPrefix = Utilities.EscapeIds(ClientID, "lpnExpRow");
            if (isFRRemediation)
            {
                LippinCottCollapsed = false;
            }
            //// Remediation
            CreateLippincott(ImgRemediationUrl, string.Empty, remediationHtml, ++Count, false);

            for (int i = 0; i <= LippincottTitle1.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(LippincottTitle1[i]))
                {
                    CreateLippincott(ImgLippincottUrl, LippincottTitle1[i], LippincottExplanation1[i], ++Count, LippinCottCollapsed);
                }

                if (!string.IsNullOrEmpty(LippincottTitle2[i]))
                {
                    CreateLippincott(ImgLippincott2Url, LippincottTitle2[i], LippincottExplanation2[i], ++Count, LippinCottCollapsed);
                }
            }

            RegisterScript();
        }

        private void RegisterScript()
        {
            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder.AppendLine("function toggle2(index) {");
            scriptBuilder.AppendLine("$('.LpnExpRow').css('display', 'none');");
            scriptBuilder.AppendLine("$('#" + _rowIdPrefix + "_' + index).css('display', '');");
            scriptBuilder.AppendLine("$('.LpnToggleImage').attr('src', '../images/plu.jpg');");
            scriptBuilder.AppendLine("$('#" + _toggleButtonIdPrefix + "_' + index).attr('src', '../images/min.jpg');");
            scriptBuilder.AppendLine("}");

            if (!Page.ClientScript.IsClientScriptBlockRegistered("Lippincott"))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Lippincott", scriptBuilder.ToString(), true);
            }
        }

        private void CreatePlusMinusButton(HtmlTableCell cell, bool isPlusButton, int index)
        {
            Image ImgPlusMinus = new Image();
            cell.Controls.Add(ImgPlusMinus);
            ImgPlusMinus.ID = Utilities.EscapeIds(_toggleButtonIdPrefix, index.ToString());
            ImgPlusMinus.Attributes.Add("value", index.ToString());
            ImgPlusMinus.Attributes.Add("onClick", "toggle2('" + index.ToString() + "');");
            ImgPlusMinus.Attributes.Add("class", "LpnToggleImage");
            ImgPlusMinus.Style.Add("cursor", "pointer");
            ImgPlusMinus.ImageUrl = isPlusButton ? ImgPlus : ImgMinus;
        }

        private void CreateTitle(HtmlTableCell cell, string titleUrl, bool isCollapsed, int index)
        {
            CreatePlusMinusButton(cell, isCollapsed, index);

            Image ImgBut = new Image();
            cell.Controls.Add(ImgBut);
            ImgBut.ID = ClientID + "_btnTitle_" + index;
            ImgBut.Attributes.Add("value", index.ToString());
            ImgBut.Attributes.Add("onClick", "toggle2('" + index.ToString() + "');");
            ImgBut.Style.Add("cursor", "pointer");
            ImgBut.Style.Add("margin-top", "10px");
            ImgBut.ImageUrl = titleUrl;
        }

        private void CreateLippincott(string titleUrl, string footerText, string explanation, int index, bool collapsed)
        {
            HtmlTableCell currentCell = CreateRow(string.Empty, false);

            CreateTitle(currentCell, titleUrl, collapsed, index);

            currentCell = CreateRow(Utilities.EscapeIds(_rowIdPrefix, index.ToString()), collapsed);

            var explanationControl = new Literal();
            currentCell.Controls.Add(explanationControl);
            explanationControl.Text = explanation;

            if (false == string.IsNullOrEmpty(footerText))
            {
                var l = new Literal
                {
                    Text = @"<div style='margin:10px;display:table;'></div>"
                };
                currentCell.Controls.Add(l);

                var Tit = new Label();
                currentCell.Controls.Add(Tit);
                Tit.Style.Add("font-style", "Italic");
                Tit.Text = footerText;
            }
        }

        private HtmlTableCell CreateRow(string rowId, bool collapsed)
        {
            HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
            Controls.Add(r);
            if (false == string.IsNullOrEmpty(rowId))
            {
                r.ID = rowId;
                r.Attributes.Add("class", "LpnExpRow");
                if (collapsed)
                {
                    r.Style.Add("display", "none");
                }
            }

            HtmlTableCell c = new System.Web.UI.HtmlControls.HtmlTableCell();
            r.Controls.Add(c);
            return c;
        }
    }
}
