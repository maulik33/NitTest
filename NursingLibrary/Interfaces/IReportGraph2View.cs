using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Interfaces
{
    public interface IReportGraph2View : IReportView
    {
        void ReturnGraphData(string graphData);
        string CategoryName { get; }
    }
}
