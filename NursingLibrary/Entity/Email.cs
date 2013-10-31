using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Email
    {
        public int EmailId { get; set; }

        public string Title { get; set; }
        
        public string Body { get; set; }
        
        public int EmailType { get; set; }
    }
}
