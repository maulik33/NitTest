using NursingLibrary.Common;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class SysAdminPresenter : AuthenticatedPresenterBase<ISysAdminView>
    {
        private readonly IAdminService _adminService;

        public SysAdminPresenter(IAdminService service)
            : base(Module.Others)
        {
            _adminService = service;
        }

        public void CheckSystem()
        {
            View.DisplayCheckSystemResults(_adminService.CheckSystem(KTPApp.IsProductionApp));
        }
    }
}
