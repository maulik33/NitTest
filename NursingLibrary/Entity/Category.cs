using System.Collections.Generic;

namespace NursingLibrary.Entity
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string TableName { get; set; }

        public int OrderNumber { get; set; }

        public int ProgramofStudyId { get; set; }

        public string ProgramofStudyName { get; set; }

        public IDictionary<int, CategoryDetail> Details { get; set; }
    }
}