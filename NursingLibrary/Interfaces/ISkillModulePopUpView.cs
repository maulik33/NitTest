using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ISkillModulePopUpView
    {
        bool IsProductionApplication { get; }

        int OrderNumber { get; }

        int TestId { get; set; }

        bool EnableBackButton { set; }

        bool EnableNextButton { set; }

        bool FromIntroReview { get; set; }

        void DisplayVideo(Entity.SMUserVideoTransaction VideoTransDetails);

        void ShowSMPage(int retValue);
    }
}
