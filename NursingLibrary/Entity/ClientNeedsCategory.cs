using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class ClientNeedsCategory
    {
        #region Properties

        public int Id { get; set; }
        
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public string Name { get; set; }
        
        public int TotQCount { get; set; }
        
        public int UnUsedIncorrectQCount { get; set; }
        
        public int UnUsedQCount { get; set; }
        
        public int InCorrectQCount { get; set; }
        
        #endregion Properties
    }
}
