namespace NursingLibrary.Interfaces
{
   public interface ISessionManagement 
   {
       T Get<T>(string key) where T : class;

       void Set<T>(string key, T item);
   }
}
