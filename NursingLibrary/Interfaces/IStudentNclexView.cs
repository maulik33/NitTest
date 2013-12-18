using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentNclexView
    {
        void EnableNClexLinks();
        
        void CreateAvpContentLink();

        void OnTestAssign(int productId, int testSubgroup);
    }
}
