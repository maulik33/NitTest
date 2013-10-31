using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentLoginRepository
    {
        Student GetStudent(string userName, string password);

        Student GetStudent(int userId);
        
        void SaveUserSession(int userId, string sessionId);
    }
}
