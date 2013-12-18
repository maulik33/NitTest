using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IValidateNursingView
    {
        ValidateNursingParams GetQueryParameters();

        void SendHttpresponse();
        
        void SendRStatus(string rStatus);
    }
}
