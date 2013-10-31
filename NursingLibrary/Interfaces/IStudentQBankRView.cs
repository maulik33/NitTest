using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentQBankRView
    {
        string EndQuery { get; }

        void BindViewSample(IEnumerable<FinishedTest> getTestsNCLEXInfoForTheUser);

        void BindViewList(IEnumerable<FinishedTest> getTestsNCLEXInfoForTheUser);
    }
}
