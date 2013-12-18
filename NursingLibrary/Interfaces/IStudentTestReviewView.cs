using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentTestReviewView 
    {
        String ProductName { set; }

        bool EnableProctorTrack { get; set; }

        string ProctorTrackStartUrl { get; set; }

        void SetResponseProperties();
        
        void BindAllTestsGrid(IEnumerable<Test> tests);
        
        void BindSuspendedTestsGrid(IEnumerable<UserTest> tests);
        
        void BindTakenTestsGrid(IEnumerable<UserTest> tests);
        
        void BindAllCustomizedTestsGrid(IEnumerable<UserTest> tests);
    }
}
