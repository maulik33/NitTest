namespace NursingLibrary.Interfaces
{
    public interface ILaunchDxrView
    {
        string DxrKey { get; }
        
        string DxUrl { get; }
        
        string ContentId { get; }
        
        void RegisterScript(string response);
    }
}
