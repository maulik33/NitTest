using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using NursingLibrary.Presenters;

namespace WebControls
{
    /// <summary>
    /// Summary description for KTPHyperLinkField
    /// </summary>
    public class KTPHyperLinkField : HyperLinkField
    {
        public AdminPageDirectory PageID { get; set; }
    }
}