using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentGraphView
    {
        ProgramResults ResultsFromTheProgram { get; set; }

        void GenerateGraph();
    }
}
