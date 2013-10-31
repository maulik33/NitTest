using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Presenters.Navigation;

namespace NursingRNWeb
{
    public class UserControlBase : UserControl
    {
        public ExecutionContext CurrentContext { get; set; }

        public IPageNavigator Navigator { get; set; }
    }
}