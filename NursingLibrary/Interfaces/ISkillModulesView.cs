using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ISkillModulesView
    {
        ////String ProductName { set; }
        bool IsProductionApplication { get; }

        int OrderNumber { get; }

        int TestId { get; set; }

        bool EnableBackButton { set; }

        bool EnableNextButton { set; }

        void BindSkillsModulesGrid(IEnumerable<Test> skills);

        ////void CreateSkillsModulesDetails(string UserExist, int SkillsModuleId, int UserId);

        void DisplayVideo(Entity.SMUserVideoTransaction VideoTransDetails);

        void ShowSMPage();

        void BindAvailableQuizzesGrid(IEnumerable<UserTest> skillsAvailableQuizzes);

        void BindSuspendedQuizzesGrid(IEnumerable<UserTest> skillsSuspendedQuizzes);

        void BindViewQuizResultsGrid(IEnumerable<UserTest> tests);
    }
}
