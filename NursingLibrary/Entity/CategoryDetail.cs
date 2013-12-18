using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class CategoryDetail
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int ProgramofStudy { get; set; }
    }
}
