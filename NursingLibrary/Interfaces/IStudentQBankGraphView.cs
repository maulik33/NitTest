namespace NursingLibrary.Interfaces
{
    public interface IStudentQBankGraphView
    {
        int AType { get; }

        void RefreshGraph(string xmlData);
    }
}
