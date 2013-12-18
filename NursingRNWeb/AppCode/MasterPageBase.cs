using System.Collections.Generic;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Presenters.Navigation;

namespace NursingRNWeb
{
    public class MasterPageBase : MasterPage
    {
       //// Since page base is generic this is the simplest way to do this.
        private List<UserControlBase> _userControls;

        public MasterPageBase()
        {
            _userControls = new List<UserControlBase>();
        }

        public ExecutionContext CurrentContext { get; set; }

        public IEnumerable<UserControlBase> RegisteredControls
        {
            get
            {
                return _userControls;
            }
        }

        public IPageNavigator Navigator { get; set; }

        protected void RegisterUserControls(params UserControlBase[] controls)
        {
            _userControls.AddRange(controls);
        }
    }
}