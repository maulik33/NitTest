using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentQBankView
    {
        string UserHostAddress { get; }
        
        string HTTP_X_FORWARDED_FOR { get; }

        int NumberOfCategory { get; set; }
        
        int AllSubCategory { get; set; }
        
        int TimedTest { get; set; }
        
        int TutorMode { get; set; }
        
        int ReuseMode { get; set; }
        
        int NumberOfQuestions { get; set; }
        
        string TimeRemaining { get; set; }
        
        int SuspendQuestionNumber { get; set; }
        
        int SuspendId { get; set; }
        
        string TestName { get; set; }
        
        int Correct { get; set; }
        
        string Options { get; set; }
        
        string CategoryList { get; set; }

        IEnumerable<ClientNeeds> ClientNeeds { get; set; }

        IEnumerable<ClientNeedsCategory> ClientNeedsCategory { get; set; }

        void Create_MainTable();
        
        void SetControls();
        
        void GetTestDetails();
    }
}
