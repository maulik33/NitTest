using System;

namespace NursingLibrary.Interfaces
{
    public interface ILogManagement
    {
        void SetLogger(string logger);

        void LogDebug(string message);
        
        void LogDebug(string message, Exception ex);
        
        void LogError(string message);
        
        void LogError(string message, Exception ex);
        
        void LogFatal(string message);
        
        void LogFatal(string message, Exception ex);
        
        void LogInfo(string message);
        
        void LogInfo(string message, Exception ex);
        
        void LogWarning(string message);
        
        void LogWarning(string message, Exception ex);
    }
}
