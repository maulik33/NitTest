using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NursingLibrary.Entity
{
    public class NurProgram
    {
        public int ProgramId { get; set; }

        public string ProgramName { get; set; }
        
        public DateTime DeletedDate { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public int CreatedUser { get; set; }
        
        public int UpdatedUser { get; set; }
        
        public DateTime UpdateDate { get; set; }
    }
}
