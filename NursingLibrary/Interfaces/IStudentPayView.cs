using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentPayView
    {
        void AddPostToKaptestScript(string userId, string courseAccessId, string encryptedToken);
    }
}
