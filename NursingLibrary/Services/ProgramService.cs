using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Utilities;

namespace NursingLibrary.Services
{
    public class ProgramService
    {
        public void Validate(Program program)
        {
            ErrorAggregator errors = new ErrorAggregator();

            if (string.IsNullOrEmpty(program.ProgramName))
            {
                errors.Add("Program Name cannot be empty");
            }

            errors.ThrowIfInvalid("Validation Rules for Program failed.");
        }
    }
}
