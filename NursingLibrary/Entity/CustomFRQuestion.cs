using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class CustomFRQuestion
    {
        public CustomFRQuestion(int questionId,int programofstudyId)
        {
            Id = questionId;

            CategoryTopics = new Dictionary<LookupType, LookupMapping>()
            {
                {programofstudyId == 1? LookupType.CustomizedFRSystemCategory:LookupType.PNCustomizedFRSystemCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRPsychiatricCategory:LookupType.PNCustomizedFRPsychiatricCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRManagementofCareCategory:LookupType.PNCustomizedFRManagementofCareCategory, null }
            };

            RemediationMappings = new Dictionary<LookupType, LookupMapping>()
            {
               {programofstudyId == 1? LookupType.CustomizedFRSystemCategory:LookupType.PNCustomizedFRSystemCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRPsychiatricCategory:LookupType.PNCustomizedFRPsychiatricCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRManagementofCareCategory:LookupType.PNCustomizedFRManagementofCareCategory, null }
            };

            TestMappings = new Dictionary<LookupType, LookupMapping>()
            {
               {programofstudyId == 1? LookupType.CustomizedFRSystemCategory:LookupType.PNCustomizedFRSystemCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRPsychiatricCategory:LookupType.PNCustomizedFRPsychiatricCategory, null },
                { programofstudyId == 1?LookupType.CustomizedFRManagementofCareCategory:LookupType.PNCustomizedFRManagementofCareCategory, null }
            };
        }

        public int Id { get; private set; }

        public Lookup Topic { get; set; }

        public Lookup System { get; set; }

        public Lookup Psychiatric { get; set; }

        public Lookup ManagementOfCare { get; set; }

        public IDictionary<LookupType, LookupMapping> CategoryTopics { get; set; }

        public IDictionary<LookupType, LookupMapping> RemediationMappings { get; set; }

        public IDictionary<LookupType, LookupMapping> TestMappings { get; set; }
    }
}
