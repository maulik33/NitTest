using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportEnglishForNursingTrackingView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateStudent(IEnumerable<StudentEntity> students);

        void RenderReport(IEnumerable<EnglishForNursingTracking> reportData);

        void GenerateReport(IEnumerable<EnglishForNursingTracking> reportData, ReportAction printActions);

        void PopulateQid(IEnumerable<Question> qid);

        void DisableAccess();
    }
}
