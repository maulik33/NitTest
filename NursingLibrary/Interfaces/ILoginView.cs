namespace NursingLibrary.Interfaces
{
    public interface ILoginView
    {
        void FailedLogin(LoginFailure loginFailure);

        void DisplayMessage(string message, bool IsResetMailsent);
    }
}
