using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.Adapters;

namespace NursingLibrary.Presenters.StateManagement
{
    public class ViewStateAdapter : PageAdapter
    {
        public override PageStatePersister GetStatePersister()
        {
            return new SessionPageStatePersister(this.Page);
        }
    }
}
