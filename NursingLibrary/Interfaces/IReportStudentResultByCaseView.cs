using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentResultByCaseView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateCases(IEnumerable<CaseStudy> cases);

        void RenderReport(IEnumerable<StudentEntity> reportData);
    }
}
