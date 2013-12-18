namespace NursingLibrary.Entity
{
    public class CaseSubCategory
    {
        public int Id { get; set; }

        public int ModuleStudentId { get; set; }
        
        public int SubCategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public int Correct { get; set; }
        
        public int Total { get; set; }
        
        public int CategoryId { get; set; }
    }
}
