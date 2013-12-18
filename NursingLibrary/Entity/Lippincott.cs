namespace NursingLibrary.Entity
{
    public class Lippincott
    {
        private Remediation _remediation;

        public Lippincott()
        {
            _remediation = new Remediation();
        }
        
        public int QId { get; set; }
        
        public string LippincottTitle { get; set; }
        
        public string LippincottExplanation { get; set; }
        
        public string LippincottTitle2 { get; set; }
        
        public string LippincottExplanation2 { get; set; }
        
        public int LippincottID { get; set; }
        
        public int RemediationId { get; set; }
        
        public string ReleaseStatus { get; set; }
        
        public Remediation Remediation
        {
            get 
            { 
                return _remediation; 
            }

            set
            {
                if (_remediation != null)
                {
                    _remediation.TopicTitle = value.TopicTitle;
                }
            }
        }
        
        public Question Question { get; set; }
    }
}
