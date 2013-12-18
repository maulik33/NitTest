using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportRemediationByTestView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void PopulateStudent(IEnumerable<StudentEntity> students);

        void RenderReport(IEnumerable<TestRemediationExplainationDetails> reportData);
        
        void GenerateReport(IEnumerable<TestRemediationExplainationDetails> reportData, ReportAction printActions);
    }
}
