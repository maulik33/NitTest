using System;
using System.Collections.Generic;

namespace NursingLibrary.Utilities
{
    public class ErrorAggregator
    {
        private readonly List<string> _errors;

        public ErrorAggregator()
        {
            _errors = new List<string>();
        }

        public void Add(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public void ThrowIfInvalid()
        {
            if (_errors.Count > 0)
            {
                throw new ApplicationException(String.Join("\r\n", _errors.ToArray()));
            }
        }

        public void ThrowIfInvalid(string title)
        {
            if (_errors.Count > 0)
            {
                throw new ApplicationException(title,
                    new ApplicationException(String.Join("\r\n", _errors.ToArray())));
            }
        }
    }
}
