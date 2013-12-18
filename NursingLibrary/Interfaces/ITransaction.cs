using System;

namespace NursingLibrary.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
