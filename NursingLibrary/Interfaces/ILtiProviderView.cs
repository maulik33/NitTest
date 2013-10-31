using System.Collections.Generic;

using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ILtiProviderView
    {
        void BindData(List<LtiProvider> ltiProviders);
        void BindDataOnEdit(LtiProvider ltiProvider);
        void BindDataOnView(LtiProvider ltiProvider);
    }
}
