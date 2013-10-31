using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportGraphView : IReportView
    {
        void ReturnGraphData(string graphData);
    }
}
