using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentQuestionsView : IReportView
    {
        void SetControlValues();

        void RenderReportForIntegratedTest(IEnumerable<TestCategory> testAssignment, IEnumerable<TestRemediationTimeDetails> reportData);

        void RenderReportForNCLX(IEnumerable<TestRemediationTimeDetails> reportData);
        
        void RenderReportForFocusedReview(IEnumerable<TestRemediationTimeDetails> reportData);

        void ExportReport(string institutionNames, string cohortNames, IEnumerable<TestCategory> testAssignment, IEnumerable<TestRemediationTimeDetails> reportData, ReportAction act);
    }
}
