using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class CustomFRLookupData
    {
        public CustomFRLookupData()
        {
            TestMappings = new Dictionary<int, LookupMapping>();
            RemediationMappings = new Dictionary<int, LookupMapping>();
            CategoryTopicMappings = new Dictionary<LookupMapping, KeyValuePair<Lookup, Lookup>>();
            SystemCategories = new Dictionary<int, Lookup>();
            PsychiatricCategory = new Dictionary<int, Lookup>();
            ManagementOfCareCategory = new Dictionary<int, Lookup>();
            Topics = new Dictionary<int, Lookup>();
        }

        public IDictionary<int, LookupMapping> TestMappings { get; set; }

        public IDictionary<int, LookupMapping> RemediationMappings { get; set; }

        public IDictionary<LookupMapping, KeyValuePair<Lookup, Lookup>> CategoryTopicMappings { get; set; }

        public IDictionary<int, Lookup> SystemCategories { get; set; }

        public IDictionary<int, Lookup> PsychiatricCategory { get; set; }

        public IDictionary<int, Lookup> ManagementOfCareCategory { get; set; }

        public IDictionary<int, Lookup> Topics { get; set; }

        public IList<LookupMapping> TopicMappings { get; set; }

        public bool IsRemediationMapped { get; set; }

        public bool IsTestMapped { get; set; }
    }
}
