using System;
using System.Collections.Generic;
using System.Linq;

namespace NursingLibrary.Utilities
{
    public static class CollectionHelper
    {
        public static bool HasElements<T>(this IEnumerable<T> collection)
        {
            return (collection != null && collection.Count() != 0);
        }
    }
}
