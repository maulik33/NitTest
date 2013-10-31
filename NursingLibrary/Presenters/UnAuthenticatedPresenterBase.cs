using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    public class UnAuthenticatedPresenterBase<TView> : PresenterBase<TView>
    {
        public UnAuthenticatedPresenterBase()
            : base(Module.Others)
        {
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
        }

        public override void Authorize()
        {
        }
    }
}
