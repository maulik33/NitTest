namespace NursingLibrary.Interfaces
{
    public interface ICookieManagement
    {
        bool CookieExists();

        string GetCookieValue();
        
        int GetUserId();
        
        void SetCookie(string value);
        
        void SetCookie(string value, bool nonpersistent);
    }
}
