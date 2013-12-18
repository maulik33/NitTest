using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportPerformanceByQuestionToExcelView : IReportView
    {
        void RenderReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData);
    }
}
