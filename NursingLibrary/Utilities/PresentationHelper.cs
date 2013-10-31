using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Utilities
{
    // Since PresenterBase and other derived classes are generic, accessing Static methods from outside of this class hierarchy
    // is very tedious. This is a workaround for doing so.
    public class PresentationHelper
    {
        public static Func<IDictionary<UserType, IDictionary<Module, IList<UserAction>>>> AuthorizationRulesCallBackDelegate { get; set; }


        public static bool IsProctorTrackEnabled(int isProctorTrackEnabledForInstitution)
        {
            //the other business rule is that proctor track can only be enabled for IT tests, but we are checking for that before we call this method
            
            if (isProctorTrackEnabledForInstitution != 1)
            {
                return false;
            }
            //if we get down to here, let's check to make sure proctorTrack is enabled in web.config
            string enableProctorTrackSetting = ConfigurationManager.AppSettings["EnableProctorTrack"];
            if (enableProctorTrackSetting == null)
            {
                return false;
            }
            return enableProctorTrackSetting.Trim().ToLower() == "true";
        }
    }
}
